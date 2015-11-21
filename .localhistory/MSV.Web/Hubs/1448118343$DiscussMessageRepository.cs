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
                            Avatar = "dist/img/avatar.png",
                            Status = reader["Status"].ToString(),
                            CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString())
                        });
                    }
                }
            }

            return messageChat;
        }

        public void UpdateDiscussMessagesSent(string Id)
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [dbo].[Discuss] SET Status = 'S' WHERE id = '" + Id + "'";
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


        public void RegisterDiscussMessages()
        {
            var receive = new List<DiscussMessageModel>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id]
                                                          ,[Username]
                                                          ,[Message]
                                                          ,[FilePath]
                                                          ,[Status]
                                                          ,[CreatedDate]
                                                      FROM [dbo].[Discuss]", connection))
                {
                    command.Notification = null;
                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
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
                            Avatar = "dist/img/avatar.png",
                            Status = reader["Status"].ToString(),
                            CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString())
                        });
                    }
                }

            }
           
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change && e.Info == SqlNotificationInfo.Insert)
            {
                List<DiscussMessageModel> lsDiscussMessage = GetDiscussMessages();
                LotteryResultHub hub = new LotteryResultHub();
                foreach (DiscussMessageModel model in lsDiscussMessage)
                {
                    if (model.Status.Equals("A")){
                        hub.SendDiscussMessage(model.Username, model.FilePath, model.Message);
                        UpdateDiscussMessagesSent(model.Id);
                    }
                }
                RegisterDiscussMessages();
            }
        }
    }



}