using System;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace CRUD_operations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=.;Integrated Security=True");
        public int StudentID;

        private void label1_Click (object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("pepe"); 
            GetSudentsRecord();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        { 
        
        }
        private void GetSudentsRecord()
        {
            SqlCommand cmd = new SqlCommand("Select * From StudentsTb", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentRecordDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES (@name, @FatherName, @Roll, @Address, @Mobile");
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Student is successfully saved in the database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                GetSudentsRecord();
                ResetFormControls();
            }
        }

        private bool IsValid()
        {
            if (txtStudentName.Text == String.Empty)
            {
                MessageBox.Show("Student Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;            
            }

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControls();

        }

        private void ResetFormControls()
        {
            StudentID = 0;
            txtStudentName.Clear();
            txtFatherName.Clear();
            txtRollNumber.Clear();
            txtMobile.Clear();
            txtAddress.Clear();

            txtStudentName.Focus();
        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNumber.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = StudentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name = @name, FatherName = @FatherName, Roll= @Roll, Address = @Address, Mobile = @Mobile WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is update successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetSudentsRecord();
                ResetFormControls();

            }
            else
            {
                MessageBox.Show("Please Select an student to update his  information", "select?", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsRb WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
              
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student is deleted from the system", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetSudentsRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please Select an student to delete", "select?", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
