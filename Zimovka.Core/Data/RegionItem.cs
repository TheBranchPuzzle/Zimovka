using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zimovka.Data
{
    public class RegionItem
    {
      public DateTime _date { get; set; }
      public string _regionName { get; set; }
      public int _temp { get; set; }
      public int _price { get; set; }
    }
}