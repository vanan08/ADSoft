using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSoft.Web.Hubs
{
    public class LotteryResultHub : Hub
    {
        public void ClientUpdateLotteryResult(string name, string message)
        {
            Clients.All.ClientUpdateLotteryResult(name, message);
        }
    }
}