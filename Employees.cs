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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            displayEmp();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CarDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\Documents\MyGarageDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void displayEmp()
        {
            Con.Open();
            String Query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "Employee Name" || EmpAddTb.Text == "Employee Address" || EmpPassTb.Text == "Employee Password" ||  empGenCb.SelectedIndex == -1 || EmpPassTb.Text == "" || EmpNameTb.Text == "" || EmpAddTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl(EmpName,EmpGen,EmpAdd,EmpPass) values(@EN,@EG,@EA,@EP)", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EG", empGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Register");
                    Con.Close();
                    displayEmp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
       // int Key = 0;
        string EmpKey = "";
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.EmployeeDGV.Rows[e.RowIndex];
                EmpKey = row.Cells[0].Value.ToString();
                EmpNameTb.Text = row.Cells[1].Value.ToString();
                empGenCb.SelectedItem = row.Cells[2].Value.ToString();
                EmpAddTb.Text = row.Cells[3].Value.ToString();
                EmpPassTb.Text = row.Cells[4].Value.ToString();

            }



            /*EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            empGenCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPassTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();

            if(EmployeeDGV.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }*/
        }
        
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (EmpKey == "")
            {
                MessageBox.Show("Select The Employee");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EmpId=@EId", Con);
                    cmd.Parameters.AddWithValue("@EId", EmpKey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted");
                    Con.Close();
                    displayEmp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("ex.Message");
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "Employee Name" || EmpAddTb.Text == "Employee Address" || EmpPassTb.Text == "Employee Password" || empGenCb.SelectedIndex == -1 || EmpPassTb.Text == "" || EmpNameTb.Text == "" || EmpAddTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update EmployeeTbl set EmpName=@EN,EmpGen=@EGen,EmpAdd=@EA,EmpPass=@EP where EmpId=@EId", Con);
                    cmd.Parameters.AddWithValue("@EId", EmpKey);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EGen", empGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated");
                    Con.Close();
                    displayEmp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
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

        private void label7_Click(object sender, EventArgs e)
        {

            DashBoard Obj = new DashBoard();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }
    }
}
