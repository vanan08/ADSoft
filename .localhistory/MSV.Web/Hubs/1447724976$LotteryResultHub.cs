﻿using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSoft.Web.Hubs
{
    public class LotteryResultHub : Hub
    {
        /// <summary>
        /// Send lottery resoult to all clients
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            Clients.All.ClientUpdateLotteryResult(name, message);
        }
    }
}