using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Garage_Management_System
{
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();
            displayCars();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\Documents\MyGarageDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void displayCars()
        {
            Con.Open();
            String Query = "select * from CarTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CarDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(CarNumTb.Text == "Car Number" || CarBrandTb.Text == "Car Brand" || CarModelTb.Text == "Car Model" || ColorTb.Text == "Color" || OwnerNameTb.Text == "Owner Name" || CarNumTb.Text==""||CarBrandTb.Text==""||CarModelTb.Text==""||ColorTb.Text==""||OwnerNameTb.Text=="")
            {
                MessageBox.Show("Wrong Input");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CarTbl(CNum,CBrand,CModel,CDate,CColor,OwnerName) values(@CN,@CB,@CM,@CD,@CC,@ON)",Con);
                    cmd.Parameters.AddWithValue("@CN", CarNumTb.Text);
                    cmd.Parameters.AddWithValue("@CB", CarBrandTb.Text);
                    cmd.Parameters.AddWithValue("@CM", CarModelTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CC", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@ON", OwnerNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Register");
                    Con.Close();
                    displayCars();
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);    
                }
            }
        }

        //int Key = 0;
        string CarKey = "";
        private void CarDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if(CarKey == "")
            {
                MessageBox.Show("Select The Car");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CarTbl where CNum=@CN",Con);
                    cmd.Parameters.AddWithValue("@CN", CarKey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Deleted");
                    Con.Close();
                    displayCars();
                }catch(Exception Ex)
                {
                    MessageBox.Show("ex.Message");
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CarNumTb.Text == "Car Number" || CarBrandTb.Text == "Car Brand" || CarModelTb.Text == "Car Model" || ColorTb.Text == "Color" || OwnerNameTb.Text == "Owner Name" || CarNumTb.Text == "" || CarBrandTb.Text == "" || CarModelTb.Text == "" || ColorTb.Text == "" || OwnerNameTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update CarTbl set CBrand=@CB,CModel=@CM,CDate=@CD,CColor=@CC,OwnerName=@ON where CNum=@CN", Con);
                    cmd.Parameters.AddWithValue("@CN", CarKey);
                    cmd.Parameters.AddWithValue("@CB", CarBrandTb.Text);
                    cmd.Parameters.AddWithValue("@CM", CarModelTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CC", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@ON", OwnerNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Updated");
                    Con.Close();
                    displayCars();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void label6_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void CarDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Basically e.RowIndex is used in Row_Deleting Event as well as Row_Command Event to find control ID within Gridview.
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.CarDGV.Rows[e.RowIndex];
                CarNumTb.Text = row.Cells[0].Value.ToString();
                CarKey = row.Cells[0].Value.ToString();
                CarBrandTb.Text = row.Cells[1].Value.ToString();
                CarModelTb.Text = row.Cells[2].Value.ToString();
                CDate.Text = row.Cells[3].Value.ToString();
                ColorTb.Text = row.Cells[4].Value.ToString();
                OwnerNameTb.Text = row.Cells[5].Value.ToString();

            }

            /* CarNumTb.Text = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
            CarKey = CarDGV.SelectedRows[0].Cells[0].Value.ToString();
            CarBrandTb.Text = CarDGV.SelectedRows[0].Cells[1].Value.ToString();
            CarModelTb.Text = CarDGV.SelectedRows[0].Cells[2].Value.ToString();
            CDate.Text = CarDGV.SelectedRows[0].Cells[3].Value.ToString();
            ColorTb.Text = CarDGV.SelectedRows[0].Cells[4].Value.ToString();
            OwnerNameTb.Text = CarDGV.SelectedRows[0].Cells[5].Value.ToString();
            if(CarNumTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CarDGV.SelectedRows[0].Cells[1].Value.ToString());
            }*/
        }
    }
}
