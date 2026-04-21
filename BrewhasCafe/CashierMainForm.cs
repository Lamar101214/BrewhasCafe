using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrewhasCafe
{
    public partial class CashierMainForm : Form
    {
        public CashierMainForm()
        {
            InitializeComponent();

            // Displays the username of the person currently logged in
            displayUsername();
        }

        public void displayUsername()
        {
            // Capitalizes the first letter of the stored username for a professional look
            string getUsername = Data.username;
            if (!string.IsNullOrEmpty(getUsername))
            {
                username.Text = char.ToUpper(getUsername[0]) + getUsername.Substring(1);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to sign out?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void addProducts_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddProducts1.Visible = true;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = false;

            // Calls refreshData to ensure the product list is current
            AdminAddProducts aaProd = adminAddProducts1 as AdminAddProducts;
            if (aaProd != null)
            {
                aaProd.refreshData();
            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = true;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = false;

            // Updated to match the AdminDashboardForm class name
            AdminDashboardForm adForm = adminDashboardForm1 as AdminDashboardForm;
            if (adForm != null)
            {
                adForm.refreshData();
            }
        }

        private void order_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = true;
            cashierCustomersForm1.Visible = false;

            // Updates the order form data before displaying
            CashierOrderForm coForm = cashierOrderForm1 as CashierOrderForm;
            if (coForm != null)
            {
                coForm.refreshData();
            }
        }

        private void customer_btn_Click(object sender, EventArgs e)
        {
            adminDashboardForm1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierOrderForm1.Visible = false;
            cashierCustomersForm1.Visible = true;

            // Updated to use the singular 'CashierCustomerForm'
            CashierCustomersForm ccForm = cashierCustomersForm1 as CashierCustomersForm;
            if (ccForm != null)
            {
                ccForm.refreshData();
            }
        }
    }
}