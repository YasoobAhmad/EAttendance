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
            cb_sections.DataSource = GetAllSections();
        }

        List<String> GetSubjects(string section)
        {
            List<String> subjectsList = new List<String>();
            string text = System.IO.File.ReadAllText(@"config.txt");
            MySqlConnection cn = new MySqlConnection(text);
            MySqlCommand cmd = new MySqlCommand("Select subject from Subjects where section = @section", cn);
            cmd.Parameters.Add("section", MySqlDbType.VarChar).Value = section;
            //cmd.Parameters.Add("subject", MySqlDbType.VarChar).Value = tb_Subject.Text;
            //cmd.Parameters.Add("section", MySqlDbType.VarChar).Value = tb_Section.Text;
            try
            {
                cn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    subjectsList.Add(rdr[0].ToString());
                }
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

            return subjectsList;
        }
        List<String> GetAllSections()
        {
            List<String> subjectsList = new List<String>();
            string text = System.IO.File.ReadAllText(@"config.txt");
            MySqlConnection cn = new MySqlConnection(text);
            MySqlCommand cmd = new MySqlCommand("Select distinct section from Subjects", cn);
            //cmd.Parameters.Add("instructor", MySqlDbType.VarChar).Value = tb_Instructor.Text;
            //cmd.Parameters.Add("subject", MySqlDbType.VarChar).Value = tb_Subject.Text;
            //cmd.Parameters.Add("section", MySqlDbType.VarChar).Value = tb_Section.Text;
            try
            {
                cn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    subjectsList.Add(rdr[0].ToString());
                }
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

            return subjectsList;
        }

        private void cb_sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_subjects.DataSource = GetSubjects(cb_sections.Text);
        }
    }

}