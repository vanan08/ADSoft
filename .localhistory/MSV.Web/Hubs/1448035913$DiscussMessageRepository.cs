using ADSoft.Web.Models;
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
        public List<DiscussMessageModel> GetDiscussMessages()
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
                                                      FROM [dbo].[Discuss] ORDER BY [dbo].[Discuss].CreatedDate desc", connection))
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
                            Username = reader["Username"].ToString(),
                            Message = reader["Message"].ToString(),
                            FilePath = reader["FilePath"].ToString(),
                            Status = reader["Status"].ToString(),
                            CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString())
                        });
                    }
                }
            }

            return messageChat;
        }



        public void RegisterDiscussMessages()
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id]
                                                          ,[Username]
                                                          ,[Message]
                                                          ,[FilePath]
                                                          ,[Status]
                                                          ,[CreatedDate]
                                                      FROM [dbo].[Discuss] ORDER BY [dbo].[Discuss].CreatedDate desc", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        receive.Add(item: new Receive
                        {
                            Id = reader["Id"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            MsgId = reader["MsgId"].ToString(),
                            ReadDate = reader["ReadDate"] == System.DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["ReadDate"].ToString()),
                            UnRead = reader["UnRead"].ToString(),
                            GroupId = reader["GroupId"].ToString()
                        });
                    }
                }

            }
            return receive;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change && e.Info == SqlNotificationInfo.Insert)
            {
                MessagesChat[] messages = GetMessagesChat().ToArray();
                List<string> users = null;

                foreach (MessagesChat mess in messages)
                {
                    MessagesChat arr = GetMessagesChatFull(mess.Id);
                    if (mess.Type.Equals("G", StringComparison.OrdinalIgnoreCase))
                    {
                        users = GetListUserOfGroup(arr.GroupId);
                    }
                    else
                    {
                        users = GetListUser(arr.Id);
                    }
                    MobiChatHub.lsUsers = users;
                    MobiChatHub.SendMessages(new MessagesChat { Id = arr.Id, Content = arr.Content, SenderId = arr.SenderId, UserName = arr.UserName, SendDate = arr.SendDate, Type = arr.Type, GroupId = arr.GroupId });

                    WebChatHub.lsUsers = users;
                    WebChatHub.SendMessages(new MessagesChat { Id = arr.Id, Content = arr.Content, SenderId = arr.SenderId, UserName = arr.UserName, SendDate = arr.SendDate, Type = arr.Type, GroupId = arr.GroupId });

                    UpdateStatusMessage(mess.Id, DateTime.Now);

                }
            }

            RegisterMessages();
        }
    }



}