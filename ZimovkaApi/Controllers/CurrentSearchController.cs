using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Zimovka.Core.Data;
using Zimovka.Data;
using Zimovka.Presenter;

namespace ZimovkaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentSearchController : ControllerBase
    {
        private readonly IBlogic _blogic;
        private readonly ISaveManager _sm;

        public CurrentSearchController(IBlogic blogic, DBconnection db)
        {
          _sm = new DBSaveManager(blogic, db);
          _blogic = blogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionOutput>>> GetProducts()
        {
          await _sm.Load();
          return Ok(_blogic.CurrentSearch);
        }

        [HttpPut()]
        public ActionResult<IEnumerable<RegionOutput>> GetProducts([FromBody] SearchReqest request)
        {
          _blogic.Search(request.sDate, request.eDate, request.Regions);
          return Ok(_blogic.CurrentSearch);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionOutput>> GetProduct(int id)
        {
          await _sm.Load();
          return Ok(_blogic.CurrentSearch[id-1]);
        }

        
    }
}