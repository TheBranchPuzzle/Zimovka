using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace Zimovka.Core.Data
{
    public class DBconnection
    {
      private string connectionString;
      public IDbConnection db;
      public DBconnection(string constr)
      {
        connectionString = constr;
        db = new NpgsqlConnection(connectionString);   
        db.Open();
      } 

    }
}