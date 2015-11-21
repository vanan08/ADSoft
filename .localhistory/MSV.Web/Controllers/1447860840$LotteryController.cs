using ADSoft.Core.Models;
using ADSoft.Core.Repositories;
using ADSoft.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace ADSoft.Web.Controllers
{
    public class LotteryController : System.Web.Http.ApiController
    {
        private IRepository<Lottery> LotteryRepository = new Repository<Lottery>();

       
        // GET: api/Lottery/GetLottery
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetLottery(LotteryModel model)
        {
            Lottery lottery = LotteryRepository.FindOneBy(l => l.JackpotsDate.DayOfYear == DateTime.Now.DayOfYear);
           
        }

        
    }
}
