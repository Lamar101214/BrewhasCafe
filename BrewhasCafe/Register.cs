using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BrewhasCafe
{
    public partial class RegisterForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();

            this.Hide();

        }
        private void Register_showPassword_CheckedChanged(object sender, EventArgs e)
        {
            Register_Password.PasswordChar = Register_showPassword.Checked ? '\0' : '*';
            Register_cPassword.PasswordChar = Register_showPassword.Checked ? '\0' : '*';
        }

        public bool emptyFields()
        {
            if (Register_Username.Text == "" || Register_Password.Text == "" || Register_cPassword.Text == "") 
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
                    MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
               
                else

                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        try
                        {
                            connect.Open();

                            string selectUsername = "SELECT * FROM users WHERE username =@usern";

                            using (SqlCommand checkUsername = new SqlCommand(selectUsername, connect))

                            {
                                checkUsername.Parameters.AddWithValue("@usern", Register_Username.Text.Trim());

                                SqlDataAdapter adapter = new SqlDataAdapter(checkUsername);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                if (table.Rows.Count >= 1)
                                {
                                    string usern = Register_Username.Text.Substring(0, 1).ToUpper() + Register_Username.Text.Substring(1);
                                    MessageBox.Show(usern + " is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                                else if (Register_Password.Text != Register_cPassword.Text)
                                {

                                    MessageBox.Show("Password does not match", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                               else if (Register_Password.Text.Length < 8)
                               {
                                MessageBox.Show("Password must be at least 8 characters.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                              }
                            else
                                {
                                    string insertData = "INSERT INTO users (username, password, profile_image, role, status, date_reg)" +
                                    " VALUES (@usern, @pass, @image, @role, @status, @date)";
                                    DateTime today = DateTime.Today;

                                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                    {
                                        cmd.Parameters.AddWithValue("@usern", Register_Username.Text.Trim());
                                        cmd.Parameters.AddWithValue("@pass", Register_Password.Text.Trim());
                                        cmd.Parameters.AddWithValue("@image", "");
                                        cmd.Parameters.AddWithValue("@role", "Cashier");
                                        cmd.Parameters.AddWithValue("@status", "Approval");
                                        cmd.Parameters.AddWithValue("@date", today);

                                        cmd.ExecuteNonQuery();

                                        MessageBox.Show("Registered Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                         Form1 loginForm = new Form1();
                                            loginForm.Show();

                                         this.Hide();

                                     }
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


