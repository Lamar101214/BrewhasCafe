using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BrewhasCafe
{
    class AdminAddProductsData
    {
        // Use the connection string from your App.config
        static string conn = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

        public int ID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string Date { get; set; }

        public List<AdminAddProductsData> productsListData()
        {
            List<AdminAddProductsData> listData = new List<AdminAddProductsData>();

            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                // We only select items that have NOT been soft-deleted
                string selectData = "SELECT * FROM products WHERE date_delete IS NULL";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AdminAddProductsData prodData = new AdminAddProductsData();
                        prodData.ProductID = reader["prod_id"].ToString();
                        prodData.ProductName = reader["prod_name"].ToString();
                        prodData.Type = reader["prod_type"].ToString();
                        prodData.Stock = (int)reader["prod_stock"];
                        prodData.Price = Convert.ToDouble(reader["prod_price"]);
                        prodData.Status = reader["prod_status"].ToString();
                        prodData.Image = reader["prod_image"].ToString();
                        prodData.Date = reader["date_insert"].ToString();

                        listData.Add(prodData);
                    }
                }
            }
            return listData;
        }
    }
}