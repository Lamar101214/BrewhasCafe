using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; 

namespace BrewhasCafe
{
        class AdminAddUsersData
        {
            // Use the connection from your App.config instead of hardcoding the path
            static string conn = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;
            SqlConnection connect = new SqlConnection(conn);

            public int ID { set; get; }
            public string Username { set; get; }
            public string Password { set; get; }
            public string Role { set; get; }
            public string Status { set; get; }
            public string Image { set; get; }
            public string DateRegistered { set; get; }

            public List<AdminAddUsersData> usersListData()
            {
                List<AdminAddUsersData> listData = new List<AdminAddUsersData>();

                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();
                        string selectData = "SELECT * FROM users";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                AdminAddUsersData userData = new AdminAddUsersData();
                                userData.ID = (int)reader["id"];
                                userData.Username = reader["username"].ToString();
                                userData.Password = reader["password"].ToString();
                                userData.Role = reader["role"].ToString();
                                userData.Status = reader["status"].ToString();
                                userData.Image = reader["profile_image"].ToString();
                                userData.DateRegistered = reader["date_reg"].ToString();

                                listData.Add(userData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Using MessageBox is better for WinForms than Console.WriteLine
                        System.Windows.Forms.MessageBox.Show("Data Load Failed: " + ex.Message);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                return listData;
            }
        }
    }