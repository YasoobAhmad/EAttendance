using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAttendance
{
    public partial class Form_RegisterStudents : Form
    {
        public Form_RegisterStudents()
        {
            InitializeComponent();
        }

        private void Form_RegisterStudents_Load(object sender, EventArgs e)
        {

        }

        String GetAllSubjects()
        {
            
            string text = System.IO.File.ReadAllText(@"config.txt");
            MySqlConnection cn = new MySqlConnection(text);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO subjects(instructor,subject,section) VALUES(@instructor,@subject,@section)", cn);
            //cmd.Parameters.Add("instructor", MySqlDbType.VarChar).Value = tb_Instructor.Text;
            //cmd.Parameters.Add("subject", MySqlDbType.VarChar).Value = tb_Subject.Text;
            //cmd.Parameters.Add("section", MySqlDbType.VarChar).Value = tb_Section.Text;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Subject Added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cn.State.ToString() == "Open")
                {
                    cn.Close();
                }
            }
            this.Close();
            String x = "hello";
            return x;
        }
        
    }
    
}
