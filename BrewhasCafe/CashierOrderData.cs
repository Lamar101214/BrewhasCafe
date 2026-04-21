using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BrewhasCafe
{
    class CashierOrdersData
    {
        // Pulls the connection string from your App.config for consistency
        static string conn = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

        // These properties match the columns in your 'orders' table
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string OrderDate { get; set; }

        public List<CashierOrdersData> ordersListData()
        {
            List<CashierOrdersData> listData = new List<CashierOrdersData>();

            using (SqlConnection connect = new SqlConnection(conn))
            {
                try
                {
                    connect.Open();

                    // This query retrieves all orders. 
                    // To show only the current customer's order, you could add: WHERE customer_id = @cID
                    string selectData = "SELECT * FROM orders";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CashierOrdersData orderData = new CashierOrdersData();

                            orderData.ID = (int)reader["id"];
                            orderData.CustomerID = (int)reader["customer_id"];
                            orderData.ProductID = reader["prod_id"].ToString();
                            orderData.ProductName = reader["prod_name"].ToString();
                            orderData.ProductType = reader["prod_type"].ToString();
                            orderData.Quantity = (int)reader["qty"];
                            orderData.Price = Convert.ToSingle(reader["prod_price"]);
                            orderData.OrderDate = Convert.ToDateTime(reader["order_date"]).ToString("MM-dd-yyyy");

                            listData.Add(orderData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return listData;
        }
    }
}