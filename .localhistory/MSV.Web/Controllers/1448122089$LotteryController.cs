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
using CMC.Northwind.Models;
using ADSoft.Web.Common;

namespace ADSoft.Web.Controllers
{
    public class LotteryController : System.Web.Http.ApiController
    {
        private IRepository<Lottery> LotteryRepository = new Repository<Lottery>();
        private IRepository<Bank> BankRepository = new Repository<Bank>();
        private IRepository<ChatAccount> ChatAccountRepository = new Repository<ChatAccount>();
        private IRepository<Discuss> DiscussRepository = new Repository<Discuss>();

        // POST: api/Lottery/GetLottery
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> GetLottery(LotteryModel model)
        {
            Lottery lottery = await LotteryRepository.FindOneByAsync(l => l.JackpotsDate.DayOfYear == model.LotteryDate.DayOfYear);
            return Ok(lottery);
        }

        // POST: api/Lottery/GetLottery
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SendDiscussMessage(DiscussMessageModel model)
        {
            Discuss discuss = new Discuss();
            discuss.Id = Utils.GenerateRandomId();
            discuss.Username = 
            await DiscussRepository.SaveAsync(discuss);
            return Ok(discuss);
        }

        [System.Web.Http.HttpGet]
        public async Task<IQueryable<BankModel>> GetBanks()
        {
            var banks = await BankRepository.FindAllAsync();
            return banks.Select(x => new BankModel
            {
                Id = x.Id,
                Ten = x.Ten,
                Logo = x.Logo,
                CTK = x.CTK,
                STK = x.STK
            });
        }

        [System.Web.Http.HttpGet]
        public async Task<IQueryable<ChatAcountModel>> GetNickName()
        {
            var nicks = await ChatAccountRepository.FindAllAsync();
            return nicks.Select(x => new ChatAcountModel
            {
                Id = x.Id,
                Logo = x.Logo,
                NickName = x.NickName
            });
        }
    }
}
