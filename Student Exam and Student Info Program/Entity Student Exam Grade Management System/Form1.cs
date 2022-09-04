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

namespace Entity_Student_Exam_Grade_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DbStudentsAndExamsEntities db = new DbStudentsAndExamsEntities();
        private void btnSubjectList_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-RK293J7;Initial Catalog=DbStudentsAndExams;Integrated Security=True");
            SqlCommand command = new SqlCommand("Select * From tblsubjects",connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLSTUDENTS.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnGradeList_Click(object sender, EventArgs e)
        {


            var query = from item in db.TBLEXAM
                        select new { item.EXGRID, item.STD, item.SUB, item.EXAM1, item.EXAM2, item.EXAM3, item.AVRG, item.STAT };
            dataGridView1.DataSource = query.ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TBLSTUDENTS t = new TBLSTUDENTS();
            t.NAME = txtName.Text;
            t.SURNAME = txtSurName.Text;
            db.TBLSTUDENTS.Add(t);
            db.SaveChanges();
            MessageBox.Show("Student added!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            var x = db.TBLSTUDENTS.Find(id);
            db.TBLSTUDENTS.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Student deleted!");
        }
    }
}
