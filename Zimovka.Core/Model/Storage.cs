using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Zimovka.Data;

namespace Zimovka.Model
{
  public class Storage : IStorage
    {
        
      public List<RegionItem> Search(DateTime startDate, DateTime endDate, string Region)
      {
        var regLines = File.ReadLines("./TestData.csv")
                        .Select(ParceRegionData)
                        .Where(x => x._date >= startDate && x._date <= endDate && x._regionName == Region)
                        .ToList();

        return regLines;
      }

      private static RegionItem ParceRegionData(string line)
      {
        string[] parts = line.Split(',');
        return new RegionItem
        {
          _date = DateTime.Parse(parts[0]),
          _regionName = parts[1],
          _temp = int.Parse(parts[2]),
          _price = int.Parse(parts[3]),
        };
      }

    }
}