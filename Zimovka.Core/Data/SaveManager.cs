using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Zimovka.Presenter;

namespace Zimovka.Data
{
    public class SaveManager : ISaveManager
    {

      private IBlogic _blogic;

      public SaveManager(IBlogic b)
      {
        _blogic = b;
      }

      public async Task Load()
        {
          _blogic.CurrentSearch = JsonSerializer.Deserialize<List<RegionOutput>>(File.ReadAllText("./search.json"));
          _blogic.Favorites = JsonSerializer.Deserialize<List<RegionOutput>>(File.ReadAllText("./favorites.json"));
        }


        //Записать пути в readonly констаннты 
        public async Task Save()
        {
          await using FileStream createStream1 = File.Create(@"./search.json");
          await JsonSerializer.SerializeAsync(createStream1, _blogic.CurrentSearch);

          await using FileStream createStream2 = File.Create(@"./favorites.json");
          await JsonSerializer.SerializeAsync(createStream2, _blogic.Favorites);
        }
        
    }
}