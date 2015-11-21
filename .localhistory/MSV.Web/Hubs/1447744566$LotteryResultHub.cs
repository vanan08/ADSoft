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

            //Clients.All.ClientUpdateLotteryResult(name, message);
        }

        private void parseLottery(string table)
        {
              var columns = new List<List<string>>();

              XElement table = XElement.Parse(table);
              XElement headings = table.Elements("tr").First(); 
    
              foreach(XElement th in headings.Elements("th"))
              {
                  string heading = th.Value;
                  var column = new List<string>{heading};
                  columns.Add(column);
              }  

              foreach(XElement tr in table.Elements("tr").Skip(1))
              {
                 int i = 0;

                 foreach(XElement td in tr.Elements("td"))
                 {
                    string value = td.Value;
                    columns[i].Add(value);
                    i++;
                 }
              }

              int rows = columns[0].Count;
              int cols = columns.Count;

              for(int i = 1; i < rows; i++)
              {
                 for(int j = 0; j < cols; j++)
                 {
                    Console.WriteLine("{0}/{1}", columns[j][0], columns[j][i]);
                 }

                 Console.WriteLine();         
              }


        }

    }
}