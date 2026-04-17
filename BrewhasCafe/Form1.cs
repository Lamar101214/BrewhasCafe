using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BrewhasCafe

{

    public partial class Form1 : Form
    {

    SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
    public Form1()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();


        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.Show();

            this.Hide();


        }

        private void Login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            Login_Password.PasswordChar = Login_showPass.Checked ? '\0' : '*';
        }

        public bool emptyFields()
        {
            if(Login_Username.Text =="" || Login_Password.Text =="")
            {
                return true;

            }

            else
            {
                return false;

            }
        }

        private void btnLgn_Click(object sender, EventArgs e)
        {
            if (emptyFields())

            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK);

            }
            else 
            { 
              
                if(connect.State == ConnectionState.Closed)

                {
                    try
                    {
                        connect.Open();

                        string SelectAccount = "SELECT * FROM users WHERE username = @usern AND password = @pass AND status = @status";

                    using (SqlCommand cmd = new SqlCommand(SelectAccount, connect)) 
                        {
                            cmd.Parameters.AddWithValue(@"usern", Login_Username.Text.Trim());
                            cmd.Parameters.AddWithValue(@"pass", Login_Password.Text.Trim());
                            cmd.Parameters.AddWithValue(@"Status", "Active");

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            int rowCount = (int) cmd.ExecuteScalar();

                            if (dt.Rows.Count > 0) 
                            
                            {
                                string selectRole = "SELECT role FROM users WHERE username = @usern AND password = @pass";

                                using (SqlCommand getRole = new SqlCommand(selectRole, connect))
                                {
                                    getRole.Parameters.AddWithValue(@"usern", Login_Username.Text.Trim());
                                    getRole.Parameters.AddWithValue(@"pass", Login_Password.Text.Trim());

                                    string userRole = getRole.ExecuteScalar() as string;

                                    MessageBox.Show("Login successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (userRole == "Admin")
                                    {
                                        AdminMainForm adminForm = new AdminMainForm();
                                        adminForm.Show();

                                        this.Hide();
                                    }
                                    else if (userRole == "Cashier")
                                    {
                                       

                                        this.Hide();

                                    }
                                        
                                
                                }


                            }

                            else
                            {
                                MessageBox.Show("Incorrect Username/Password or there's no Admin's approval.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }

            }
        }
    }
}