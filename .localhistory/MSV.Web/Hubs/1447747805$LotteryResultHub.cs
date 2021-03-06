﻿using ADSoft.Core.Models;
using ADSoft.Core.Repositories;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace ADSoft.Web.Hubs
{
    public class LotteryResultHub : Hub
    {
        private IRepository<Lottery> LotteryRepository = new Repository<Lottery>();

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

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;
            string content="";
            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    content += sLine;
                    //Clients.All.ClientUpdateLotteryResult(name, sLine);
            }

            parseLottery(GetSubstringByString("<table>", "</table>", content));

            //Clients.All.ClientUpdateLotteryResult(name, message);
        }

        private string GetSubstringByString(string a, string b, string c)
        {
            return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
        }

        private void parseLottery(string tableContent)
        {
              var columns = new List<List<string>>();
           
              XElement table = XElement.Parse("<table>"+tableContent+"</table>");
              XElement headings = table.Elements("tr").First();
              Lottery lottery = new Lottery();
              foreach(XElement th in headings.Elements("th"))
              {
                  string heading = th.Value;
                  var column = new List<string>{heading};
                  columns.Add(column);
                  lottery.Title = heading;
              }

              int row = 0;
              foreach(XElement tr in table.Elements("tr").Skip(1))
              {
                 int i = 0;

                 foreach(XElement td in tr.Elements("td"))
                 {
                    string value = td.Value;
                    if (i != 0) continue;
                    switch (row)
                    {
                        case 0:
                            lottery.Jackpots = value;
                            break;
                        case 1:
                            lottery.First = value;
                            break;
                        case 2:
                            lottery.Second = value;
                            break;
                        case 3:
                            lottery.Third = value;
                            break;
                        case 4:
                            lottery.Fourth = value;
                            break;
                        case 5:
                            lottery.Fifth = value;
                            break;
                        case 6:
                            lottery.Six = value;
                            break;
                        case 7:
                            lottery.Seven = value;
                            break;
                    }

                    columns[i].Add(value);
                    i++;
                 }
                 row++;
             }

              LotteryRepository.Save(lottery);
        }

    }
}