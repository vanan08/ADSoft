using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        

        public void SendLotteryResult(string name, string message)
        {
            //Get lottery result
            //

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create("http://ketqua.net/widget/xo-so-mien-bac.html");

            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;

            wrGETURL.Proxy = WebProxy.GetDefaultProxy();

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    Clients.All.ClientUpdateLotteryResult(name, sLine);
            }

            //Clients.All.ClientUpdateLotteryResult(name, message);
        }


    }
}