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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            displayStock();
            GetCars();
            EmpNameLbl.Text = Login.Username;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\Documents\MyGarageDb.mdf;Integrated Security=True;Connect Timeout=30");
       private void GetCars()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CNum from CarTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable(); 
            dt.Columns.Add("CNum", typeof(string));
            dt.Load(rdr);
            CarNumCb.ValueMember = "CNum";
            CarNumCb.DataSource = dt;
            Con.Close();
        }
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
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        private void UpdateQty()
        {
            int newQty = Qty - Convert.ToInt32(QtyTb.Text);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update StockTbl set PartQty=@PQ where PartId=@PId", Con);
                cmd.Parameters.AddWithValue("@PId", Key);
                
                cmd.Parameters.AddWithValue("@PQ", newQty);
                
                cmd.ExecuteNonQuery();
                
                Con.Close();
                displayStock();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int n = 0, num;
        int tot = 0, GrdTot = 0;
        private void AddPartsBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0 || QtyTb.Text == "Quantity" || QtyTb.Text == "")
            {
                MessageBox.Show("Select Spare Part To Add");
            }
            else if (Convert.ToInt32(QtyTb.Text) > Qty)
            {
                MessageBox.Show("Not Enough In Stock");
            }
            else
            {
                num = Convert.ToInt32(QtyTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ChangedPartsDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = PartName;
                tot = num * Price;
                newRow.Cells[2].Value = num;
                newRow.Cells[3].Value = Price;
                newRow.Cells[4].Value = tot;
                ChangedPartsDGV.Rows.Add(newRow);
                n++;
               
                GrdTot = GrdTot + tot;
                PartFeesLbl.Text = "Ksh" + GrdTot;
                UpdateQty();
                QtyTb.Text = "";
            }
        }

        int Tf = 0;
        private void CalcBtn_Click(object sender, EventArgs e)
        {
            if(MFeesTb.Text == "Mechanics Fees" || MFeesTb.Text == "")
            {
                MessageBox.Show("Enter A Valid Amount");
            }
            else if(PartFeesLbl.Text == "Part Fees")
            {
                Tf = Convert.ToInt32(MFeesTb.Text);
                TotFeesLbl.Text = "Ksh" + Convert.ToString(MFeesTb.Text);
            }
            else
            {
                Tf = GrdTot + Convert.ToInt32(MFeesTb.Text);
                TotFeesLbl.Text = "Ksh" + Convert.ToString(GrdTot + Convert.ToInt32(MFeesTb.Text));
            }
        }
        

        private void SaveBillBtn_Click(object sender, EventArgs e)
        {

            if (CarNumCb.SelectedIndex == -1 || TotFeesLbl.Text == "Total Fees")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(CarNum,BDate,MechFees,PartFees,TotFees,EmpName) values(@CN,@BD,@MF,@PF,@TF,@EN)", Con);
                    cmd.Parameters.AddWithValue("@CN", CarNumCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BD", BDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MF", MFeesTb.Text);
                    cmd.Parameters.AddWithValue("@PF", GrdTot);
                    cmd.Parameters.AddWithValue("@TF", Tf);
                    cmd.Parameters.AddWithValue("@EN", EmpNameLbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved");
                    Con.Close();
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
       

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {
          
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
         
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
        int Qty = 0, Price = 0, Key = 0;

        private void ChangedPartsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        String PartName = "";
        private void PartsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // if (e.RowIndex >= 0)
           // {
                DataGridViewRow row = this.PartsDGV.Rows[e.RowIndex];

                //Num = row.Cells[0].Value.ToString();
                //Part = row.Cells[1].Value.ToString();
                //Quantity = row.Cells[2].Value.ToString();
                //Price = row.Cells[3].Value.ToString();

           // }
            
            
            //PartName = PartsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PartName = row.Cells[1].Value.ToString();
            //MessageBox.Show(row.Cells[0].Value.ToString());
            //MessageBox.Show(Key);
            //Qty = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[2].Value.ToString());
            Qty = Convert.ToInt32(row.Cells[2].Value.ToString());
            //Price = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[3].Value.ToString());
            Price = Convert.ToInt32(row.Cells[3].Value.ToString());

            if (PartName == "")
            {
                Key = 0;
            }
            else
            {
                // Key = Convert.ToInt32(PartsDGV.SelectedRows[0].Cells[0].Value.ToString());
                Key = Convert.ToInt32(row.Cells[0].Value.ToString());
                
            }
        }
    }
}
