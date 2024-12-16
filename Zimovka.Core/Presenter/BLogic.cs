using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Zimovka.Model;
using Zimovka.Data;

namespace Zimovka.Presenter
{
  public class BLogic : IBlogic
    {
        private IStorage _storage;

        public List<RegionOutput> CurrentSearch {get; set;}
        public List<RegionOutput> Favorites {get; set;}
        
        public BLogic(IStorage storage)
        {
          _storage = storage;
        }

        public List<RegionOutput> Search(DateTime startDate, DateTime endDate, List<string> RegList)
        {
          CurrentSearch = new List<RegionOutput>();

          foreach (var Region in RegList)
          {
            var RegionItems = _storage.Search(startDate, endDate, Region);
            if (!RegionItems.Any()) return CurrentSearch;

            var RegionOut = new RegionOutput() {
              Region = Region,
              avgTemp = (int)RegionItems.Average(x => x._temp),
              avgPrice = (int)RegionItems.Average(x => x._price)
            };

            CurrentSearch.Add(RegionOut);  
          }

          return CurrentSearch;
        }


        public void AddFav(int index)
        {
          if (Favorites == null)
            Favorites = new List<RegionOutput>();

          Favorites.Add(CurrentSearch[index-1]);
        }
        
        public void RemoveFav(int index)
        {
          Favorites.RemoveAt(index-1);
        }

        public List<string> Analyse()
        {
          return CurrentSearch.OrderBy(x => x.avgPrice).Take(2).Select(x => x.Region).ToList();
        } 

    }
}