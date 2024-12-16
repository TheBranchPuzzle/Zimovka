using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZimovkaApi
{
    public class SearchReqest
    {
        public List<string> Regions {get; set;}
        public DateTime sDate {get; set;}
        public DateTime eDate {get; set;}

        public SearchReqest(List<string> regs, string sdate, string edate)
        {
          Regions = regs;
          sDate = DateTime.Parse(sdate);
          eDate = DateTime.Parse(edate);
        }
    }
}