﻿using ADSoft.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADSoft.Web.Hubs
{
    public class DiscussMessageRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["NORTHWNDContext"].ConnectionString;

        // Lấy Messages cần gửi
        public List<DiscussMessageModel> GetMessagesChat()
        {
            var messageChat = new List<DiscussMessageModel>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id]
      ,[Username]
      ,[Message]
      ,[FilePath]
      ,[Status]
      ,[CreatedDate]
  FROM [dbo].[Discuss] ORDER BY dbo.Messages.CreatedDate ASC", connection))
                {
                    command.Notification = null;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messageChat.Add(item: new DiscussMessageModel
                        {
                            Id = reader["Id"].ToString(),
                            Content = reader["Content"].ToString(),
                            SenderId = reader["SenderId"].ToString(),
                            UserName = "",
                            SendDate = reader["SendDate"].ToString(),
                            Type = reader["Type"].ToString(),
                            GroupId = ""
                        });
                    }
                }
            }

            return messageChat;
        }
    }
}