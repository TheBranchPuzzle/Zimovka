using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zimovka.Data
{
    public class RegionOutput
    {
      public string Region { get; set; }
      public int avgTemp  { get; set; }
      public int avgPrice { get; set; }

      public override string ToString()
      {
        return $"Регион: {Region}\n- Средняя температура: {avgTemp}C\n- Средняя цена: {avgPrice}$ в сутки";
      }

    }
}