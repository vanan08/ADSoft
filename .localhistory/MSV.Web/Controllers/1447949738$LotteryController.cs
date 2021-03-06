﻿using ADSoft.Core.Models;
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
using CMC.Northwind.Models;

namespace ADSoft.Web.Controllers
{
    public class LotteryController : System.Web.Http.ApiController
    {
        private IRepository<Lottery> LotteryRepository = new Repository<Lottery>();

       
        // POST: api/Lottery/GetLottery
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetLottery(LotteryModel model)
        {
            Lottery lottery = await LotteryRepository.FindOneByAsync(l => l.JackpotsDate.DayOfYear == model.LotteryDate.DayOfYear);
            return Ok(lottery);
        }

        
    }
}
