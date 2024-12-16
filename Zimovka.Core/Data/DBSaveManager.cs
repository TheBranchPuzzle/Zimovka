using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Dapper;
using Zimovka.Core.Data;
using Zimovka.Presenter;

namespace Zimovka.Data
{
    public class DBSaveManager : ISaveManager
    {
      private IBlogic _blogic;
      private readonly IDbConnection _dbConnection;
      private int _uid;

      public DBSaveManager( IBlogic blogic, DBconnection dbConnection)
      {
          _dbConnection = dbConnection.db;
          _blogic = blogic;
          _uid = 1;
      }

      public async Task Load()
      {
          var sql =  @"
            SELECT  region_name as Region, 
                        average_temperature as avgTemp,
                        average_price as avgPrice
            FROM sessions WHERE user_id = @UserId;

            SELECT  region_name as Region, 
                        average_temperature as avgTemp,
                        average_price as avgPrice
            FROM favorites WHERE user_id = @UserId;
          ";
          // Load the last session for the user
          using (var q = await _dbConnection.QueryMultipleAsync(sql, new {UserId = _uid}))
          {
            _blogic.CurrentSearch = q.Read<RegionOutput>().ToList();
            _blogic.Favorites = q.Read<RegionOutput>().ToList();
          }

      }

      public async Task Save()
      {

          using (var transaction = _dbConnection.BeginTransaction())
        {
            try
            {
                // Step 1: Remove existing entries for the given user ID
                await _dbConnection.ExecuteAsync(
                    @"DELETE FROM sessions WHERE user_id = @UserId", 
                    new { UserId = _uid }, 
                    transaction: transaction
                );

                // Step 2: Insert new entries
                foreach (var region in _blogic.CurrentSearch)
                {
                   var affected = await _dbConnection.ExecuteAsync(
                        @"INSERT INTO sessions
                          VALUES (@UserId, @Region, @AvgTemp, @AvgPrice)",
                        new 
                        { 
                            UserId = _uid, 
                            Region = region.Region, 
                            AvgTemp = region.avgTemp, 
                            AvgPrice = region.avgPrice 
                        },
                        transaction: transaction
                    )
;
                    //
              System.Console.WriteLine(affected.ToString());
                }


            try{
                await _dbConnection.ExecuteAsync(
                    @"DELETE FROM favorites WHERE user_id = @UserId", 
                    new { UserId = _uid }, 
                    transaction: transaction
                ).ContinueWith(x =>{
                  if(x.IsCompletedSuccessfully){
                   // System.Console.WriteLine("CS");
                  if(x.IsFaulted){
                   // System.Console.WriteLine("Fail");
                  }
                  }
                });

                foreach (var region in _blogic.Favorites)
                {
                    await _dbConnection.ExecuteAsync(
                        @"INSERT INTO favorites
                          VALUES (@UserId, @Region, @AvgTemp, @AvgPrice)",
                        new 
                        { 
                            UserId = _uid, 
                            Region = region.Region, 
                            AvgTemp = region.avgTemp, 
                            AvgPrice = region.avgPrice 
                        },
                        transaction: transaction
                    );
                }
            }
            catch (Exception ex){
              throw ex;
            }


                // Commit the transaction if everything is successful
                transaction.Commit();
            
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of error
                transaction.Rollback();
                // Log the exception (consider using a logging framework)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Optionally rethrow the exception
            }          
        }
      }  
   }
}