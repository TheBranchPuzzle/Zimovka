using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zimovka.Presenter;

namespace Zimovka.Data
{
    public interface ISaveManager
    {
      async Task Load() {}
      async Task Save() {}
    }
}