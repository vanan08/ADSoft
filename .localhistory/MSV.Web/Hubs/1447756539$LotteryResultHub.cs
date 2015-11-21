﻿using ADSoft.Core.Models;
using ADSoft.Core.Repositories;
using ADSoft.Web.Common;
using HtmlAgilityPack;
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
            string content = "";
            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    content += sLine;
                //Clients.All.ClientUpdateLotteryResult(name, sLine);
            }

            content = "<table>" + GetSubstringByString("<table>", "</table>", content) + "</table>";

            parseLottery(content);
           
            Clients.All.ClientUpdateLotteryResult("soxo", content);


            //Loto
            wrGETURL = WebRequest.Create("http://ketqua.net/");
            objStream = wrGETURL.GetResponse().GetResponseStream();

            objReader = new StreamReader(objStream);

            sLine = "";
            i = 0;
            content = "";
            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    content += sLine;
                //Clients.All.ClientUpdateLotteryResult(name, sLine);
            }

            content = "<div id=\"ketqua\">" + GetSubstringByString("<div id=\"ketqua\">", "<div id=\"ketqua2\">", content);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                //Console.WriteLine("Found: " + table.Id);
                HtmlNodeCollection hnc = table.SelectNodes("tr");
                if (hnc != null)
                {
                    foreach (HtmlNode row in hnc)
                    {
                        bool address_found = false;
                        bool address_stored = true;
                        foreach (HtmlNode cell in row.SelectNodes("td"))
                        {
                            // get address
                            if (cell.InnerText.Contains("") && !cell.InnerText.Contains(""))
                            {
                                address_found = true;
                                address_stored = false;
                            }
                            else if (address_found && address_stored == false)
                            {
                                //address = cell.InnerText.Trim();
                                address_stored = true;
                            }

                        }
                    }
                }
            }

            Clients.All.ClientUpdateLotteryResult("loto", content);
        }

        private string GetSubstringByString(string a, string b, string c)
        {
            return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
        }

        private void parseLottery(string tableContent)
        {
            Lottery lottery = new Lottery();
            lottery.Html = tableContent;
            XElement table = XElement.Parse(tableContent);
            XElement headings = table.Elements("tr").First();

            foreach (XElement th in headings.Elements("th"))
            {
                string heading = th.Value;
                lottery.Title = heading;
            }

            int row = 0;
            foreach (XElement tr in table.Elements("tr").Skip(1))
            {
                int i = -1;

                foreach (XElement td in tr.Elements("td"))
                {
                    i++;
                    string value = td.Value;
                    if (i == 0) continue;
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
                }
                row++;
            }
            lottery.JackpotsDate = DateTime.Now;
            lottery.Id = Utils.GenerateRandomId();

            Lottery checkLottery = LotteryRepository.FindOneBy(l => l.JackpotsDate == DateTime.Now);
            if (checkLottery == null)
                LotteryRepository.Save(lottery);
            else
            {
                lottery.Id = checkLottery.Id;
                lottery.JackpotsDate = checkLottery.JackpotsDate;
                LotteryRepository.Update(lottery);
            }  
        }

    }
}