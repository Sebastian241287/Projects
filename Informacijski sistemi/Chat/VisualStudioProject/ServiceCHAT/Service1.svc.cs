using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceCHAT
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        private bool AuthenticateUser()
        {
            WebOperationContext ctx = WebOperationContext.Current;
            string authHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (authHeader == null)
                return false;

            string[] loginData = authHeader.Split(':');
            if (loginData.Length == 2 && Login(loginData[0], loginData[1]) == 1)
                return true;
            return false;
        }

        public string Id()
        {
            string connStr = ConfigurationManager.ConnectionStrings["chatdbpbConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string ret = "";
                using (SqlCommand command = new SqlCommand("SELECT Id FROM Pogovor", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret = (reader.GetInt32(0)).ToString() + "," + ret;
                        //return reader.GetString(0);//ret;
                        //return (totMes).ToString();
                    }
                    //return (reader.GetInt32(0)).ToString();
                }
                return ret;

                con.Close();
            }
        }

        public int Login(string username, string geslo)
        {
            //throw new NotImplementedException();
            string connStr = ConfigurationManager.ConnectionStrings["chatdbpbConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string exists = "";
                string sql = "SELECT COUNT(*) FROM [Uporabnik] WHERE (([username] = @username) AND ([geslo] = @geslo))";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@geslo", SqlDbType.VarChar, 50).Value = geslo;

                using (cmd)
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exists = (reader.GetInt32(0)).ToString();
                    }
                }
                if (exists.Equals("1"))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

                con.Close();
            }


        }

        public string Messages()
        {
            if (AuthenticateUser())
            {
                string connStr = ConfigurationManager.ConnectionStrings["chatdbpbConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    string ret = "";
                    using (SqlCommand command = new SqlCommand("SELECT besedilo FROM Pogovor", con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = ret + reader.GetString(0) + "spaceespaciopresledek";
                            //return reader.GetString(0);//ret;
                            //return (totMes).ToString();
                        }
                        //return (reader.GetInt32(0)).ToString();
                    }
                    return ret;


                    con.Close();
                }
            }else
            {
                return "Uporabnik nima pravice.";
            }
            ///////////////////////////////////////////////////////////////////////////////////////
            //throw new NotImplementedException();
            /*string connStr = ConfigurationManager.ConnectionStrings["chatdbpbConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string ret = "";
                using (SqlCommand command = new SqlCommand("SELECT besedilo FROM Pogovor", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret = ret + reader.GetString(0) + "spaceespaciopresledek";
                        //return reader.GetString(0);//ret;
                        //return (totMes).ToString();
                    }
                    //return (reader.GetInt32(0)).ToString();
                }
                return ret;
                con.Close();
            }*/
        }

        public string Messages2(string id)
        {
            //throw new NotImplementedException();
            if (AuthenticateUser())
            {
                string connStr = ConfigurationManager.ConnectionStrings["chatdbpbConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    int id2 = Int32.Parse(id);
                    string ret = "";
                    string sql = "SELECT besedilo FROM Pogovor WHERE id>@id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id2;
                    //using (SqlCommand command = new SqlCommand("SELECT besedilo FROM Pogovor WHERE id>@id", con))
                    using (cmd)
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = ret + reader.GetString(0) + "spaceespaciopresledek";
                            //return reader.GetString(0);//ret;
                            //return (totMes).ToString();
                        }
                        //return (reader.GetInt32(0)).ToString();
                    }
                    return ret;


                    con.Close();
                }
            }else
            {
                return "Uporabnik nima pravice.";
            }
            ////////////////////////////////////////////////////////////////////////////////////////
            /*int id2 = Int32.Parse(id);
            string connStr = ConfigurationManager.ConnectionStrings["chatdbpbConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string ret = "";
                string sql = "SELECT besedilo FROM Pogovor WHERE id>@id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id2;
                //using (SqlCommand command = new SqlCommand("SELECT besedilo FROM Pogovor WHERE id>@id", con))
                using(cmd)
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret = ret + reader.GetString(0) + "spaceespaciopresledek";
                        //return reader.GetString(0);//ret;
                        //return (totMes).ToString();
                    }
                    //return (reader.GetInt32(0)).ToString();
                }
                return ret;
                con.Close();
            }
            */
        }

        public void Send(string username,string message)
        {
            string message2 = message.Replace("%20", " ");

            //throw new NotImplementedException();
            if (AuthenticateUser())
            {
                string connStr = ConfigurationManager.ConnectionStrings["chatdbpbConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string date = DateTime.Now.ToString("HH:mm tt");
                    int totMes = 0;
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Pogovor", con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totMes = reader.GetInt32(0);
                        }
                    }
                    string sql = "INSERT INTO Pogovor(username,besedilo,cas) VALUES(@username,@besedilo,@cas)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    //cmd.Parameters.Add("@id", SqlDbType.Int).Value = totMes + 1;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@besedilo", SqlDbType.VarChar).Value = username + ": " + message2;
                    cmd.Parameters.Add("@cas", SqlDbType.DateTime).Value = DateTime.Now.ToString("HH:mm tt");
                    //cmd.Parameters.AddWithValue("@cas", DateTime.Now);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();


                    con.Close();
                }
            }
            /*
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            string connStr = ConfigurationManager.ConnectionStrings["chatdbpbConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string date = DateTime.Now.ToString("HH:mm tt");
                int totMes = 0;
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Pogovor", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        totMes = reader.GetInt32(0);
                    }
                }
                string sql = "INSERT INTO Pogovor(username,besedilo,cas) VALUES(@username,@besedilo,@cas)";
                SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.Parameters.Add("@id", SqlDbType.Int).Value = totMes + 1;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@besedilo", SqlDbType.VarChar).Value = message;
                cmd.Parameters.Add("@cas", SqlDbType.DateTime).Value = DateTime.Now.ToString("HH:mm tt");
                //cmd.Parameters.AddWithValue("@cas", DateTime.Now);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                con.Close();
                
            }
            */
        }
        
    }
}
