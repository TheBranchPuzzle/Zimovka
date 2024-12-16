using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Zimovka.Core.Data;
using Zimovka.Data;

namespace Zimovka.Model
{
  public class DBStorage : IStorage
  {
    private readonly IDbConnection _dbConnection;

    public DBStorage(DBconnection Db)
    {
      _dbConnection = Db.db;
    }  

    public List<RegionItem> Search(DateTime startDate, DateTime endDate, string region)
    {

      // SQL запрос
      var sql = @"
          SELECT  date as _date,
                  region_name as _regionName,
                  temperature as _temp,
                  price as _price
          FROM regions
          WHERE date BETWEEN @StartDate AND @EndDate
          AND region_name ILIKE @Region";

      var regionItems = _dbConnection.Query<RegionItem>(sql, new { StartDate = startDate, EndDate = endDate, Region = $"%{region}%" }).ToList();

      return regionItems;
    
    }

  }
}