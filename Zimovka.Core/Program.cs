// See https://aka.ms/new-console-template for more information
using Zimovka.Model;
using Zimovka.Data;
using Zimovka.Presenter;
using Zimovka.View;
using System.Data;
using Npgsql;
using Zimovka.Core.Data;

try
{

DBconnection DB = new DBconnection("Host=localhost;Username=postgres;Password=123123;Database=zimovkadb");

IStorage dbStorage = new DBStorage(DB); 
IStorage Storage = new Storage();
BLogic bLogic= new BLogic(dbStorage);
ISaveManager sManager = new SaveManager(bLogic);
ISaveManager dbsManager = new DBSaveManager(bLogic, DB);
UCli Cli = new UCli(bLogic, dbsManager);
await Cli.Start();
}
catch(Exception ex)
{
  throw ex;
}