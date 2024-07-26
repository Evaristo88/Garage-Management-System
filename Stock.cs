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
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
            displayStock();
        }

        private void Stock_Load(object sender, EventArgs e)
        {

        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\Documents\MyGarageDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void displayStock()
        {
            Con.Open();
            String Query = "select * from StockTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PartsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "Price Name" || PQtyTb.Text == "Quantity" || PPriceTb.Text == "Price" || PNameTb.Text == "" || PQtyTb.Text == "" || PPriceTb.Text == "" )
            {
                MessageBox.Show("Wrong Input");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StockTbl(PartName,PartQty,PartPrice) values(@PN,@PQ,@PP)", Con);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", PQtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PPriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Parts Registered");
                    Con.Close();
                    displayStock();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        string PartKey = "";
       private void PartsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
               
                DataGridViewRow row = this.PartsDGV.Rows[e.RowIndex];

                PartKey = row.Cells[0].Value.ToString();
                PNameTb.Text = row.Cells[1].Value.ToString();
                PQtyTb.Text = row.Cells[2].Value.ToString();
                PPriceTb.Text = row.Cells[3].Value.ToString();


                if (PNameTb.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(row.Cells[0].Value.ToString());
                }

            //}
          
            //MessageBox.Show(PartsDGV.SelectedRows[1].Cells[1].Value.ToString());

            //PNameTb.Text = PartsDGV.SelectedRows[0].Cells[1].Value.ToString();
            //PQtyTb.Text = PartsDGV.SelectedRows[0].Cells[2].Value.ToString();
            //PPriceTb.Text = PartsDGV.SelectedRows[0].Cells[3].Value.ToString();

        }
       
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (PartKey == "")
            {
                MessageBox.Show("Select The Part");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from StockTbl where PartId=@PId", Con);
                    cmd.Parameters.AddWithValue("@PId", PartKey);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Part Deleted");
                    Con.Close();
                    displayStock();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("ex.Message");
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "Price Name" || PQtyTb.Text == "Quantity" || PPriceTb.Text == "Price" || PNameTb.Text == "" || PQtyTb.Text == "" || PPriceTb.Text == "")
            {
                MessageBox.Show("Wrong Input");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update StockTbl set PartName=@PN,PartQty=@PQ,PartPrice=@PP where PartId=@PId", Con);
                    cmd.Parameters.AddWithValue("@PId", PartKey);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", PQtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PPriceTb.Text); 
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Part Updated");
                    Con.Close();
                    displayStock();
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

        private void PNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

           