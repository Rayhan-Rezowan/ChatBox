using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChatBox.Data
{
    public class ChatMessageDataAccess
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ChatBoxConnection"].ToString();
            }
        }

        public DataTable GetAllMessage()
        {
            DataTable messageList = new DataTable();

            string sqlQuery = @"SELECT
                                  CM.ChatMessageId,
                                  U.UserId,
                                  U.UserName,
                                  CM.Message,
                                  CM.SendMessageTime
                                FROM ChatMessages AS CM
                                INNER JOIN Users AS U ON U.UserId = CM.UserId";

            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sqlQuery);

                messageList = ds.Tables[0];
            }
            catch (Exception ex)
            {
                return new DataTable();
            }

            return messageList;
        }

        public bool InsertMessage(int userId, string message)
        {
            string sqlQuery = @"INSERT INTO ChatMessages
                                   (UserId, Message, SendMessageTime)
                                 VALUES (@UserId, @Message, GETDATE())";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@UserId", userId));
            paramList.Add(new SqlParameter("@Message", message));

            try
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sqlQuery, paramList.ToArray());
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public int InsertUser(string userName)
        {
            string sqlQuery = @"Insert INTO Users
                                 (UserName)
                                VALUES (@UserName)

                               SELECT @@Identity";

            
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@UserName", userName));

            try
            {
                object result = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sqlQuery, paramList.ToArray());

                return result != null ? Convert.ToInt32(result) : -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}