using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management_System
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
            CountCars();
            CountEmployees();
            CountSpares();
            SumAmount();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\Documents\MyGarageDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountCars()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count (*) from CarTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CarLbl.Text = dt.Rows[0][0].ToString();
        }
        private void CountEmployees()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count (*) from EmployeeTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            EmpLbl.Text = dt.Rows[0][0].ToString();
        }
        private void CountSpares()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select count (*) from StockTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SpareLbl.Text = dt.Rows[0][0].ToString();
        }
        private void SumAmount()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(TotFees) from BillTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            AmountLbl.Text = dt.Rows[0][0].ToString();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

            Cars Obj = new Cars();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

            Stock Obj = new Stock();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
