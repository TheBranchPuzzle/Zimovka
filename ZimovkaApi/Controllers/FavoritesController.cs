using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zimovka.Data;
using Zimovka.Presenter;

namespace ZimovkaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        
      private readonly IBlogic _blogic;
      private readonly ISaveManager _sm;

        public FavoritesController(IBlogic blogic, ISaveManager sm)
        {
          _sm = sm;
          _blogic = blogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionOutput>>> GetFavs()
        {
          await _sm.Load();
          return Ok(_blogic.Favorites);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavbyId(int id)
        {
          await _sm.Load();
          return Ok(_blogic.Favorites[id-1]);
        }

        [HttpPost]
        public async Task<ActionResult<RegionOutput>> AddFav([FromBody] RegionOutput item)
        {
          await _sm.Load();
          _blogic.Favorites.Add(item);
          await _sm.Save();
          return CreatedAtAction(nameof(GetFavbyId), new { id = _blogic.Favorites.IndexOf(item) }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFav(int id)
        {
          await _sm.Load();
          _blogic.Favorites.RemoveAt(id-1);
          await _sm.Save();
          return NoContent();
        }

    }
}