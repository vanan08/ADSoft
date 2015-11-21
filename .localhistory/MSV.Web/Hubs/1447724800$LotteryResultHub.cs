using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSoft.Web.Hubs
{
    public class LotteryResultHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}