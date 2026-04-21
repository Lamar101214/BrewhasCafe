using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BrewhasCafe
{
    class CashierOrderFormProdData
    {
        // Connection string from your App.config
        static string conn = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

        // Properties that represent the columns in your 'products' table
        public int ID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }

        public List<CashierOrderFormProdData> availableProductsData()
        {
            List<CashierOrderFormProdData> listData = new List<CashierOrderFormProdData>();

            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                // Querying only products that are Available and NOT deleted
                string selectData = "SELECT * FROM products WHERE prod_status = @status AND date_delete IS NULL";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    cmd.Parameters.AddWithValue("@status", "Available");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CashierOrderFormProdData prodData = new CashierOrderFormProdData();
                        prodData.ID = (int)reader["prod_id"];
                        prodData.ProductID = reader["prod_id"].ToString();
                        prodData.ProductName = reader["prod_name"].ToString();
                        prodData.Type = reader["prod_type"].ToString();
                        prodData.Stock = (int)reader["prod_stock"];
                        prodData.Price = Convert.ToDouble(reader["prod_price"]);
                        prodData.Status = reader["prod_status"].ToString();

                        listData.Add(prodData);
                    }
                }
            }
            return listData;
        }
    }
}