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
using System.Drawing.Imaging;
using System.Threading;
using DPUruNet;
using System.Reflection;
using UareUSampleCSharp;

namespace EAttendance
{
    public partial class Form_RegisterStudents : Form
    {
        ReaderCollection readersCollection;
        Reader reader;
        public Fmd fingerPrint;
        FMDHandler fHND;
        Student st;
        public int count = 0;
        public Form_RegisterStudents()
        {
            InitializeComponent();
            fHND = new FMDHandler();
            fingerPrint = null;
            st = new Student();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("abcdefghijklmnopqrstuvwxyz");
            try
            {
                readersCollection = ReaderCollection.GetReaders();
                if(readersCollection.Count<Reader>() == 0)
                {

                    MessageBox.Show("No Readers Found on your machine");
                }
                else
                {
                    Enrollment formRegisterStudents = new Enrollment(this);
                    this.Hide();
                    try
                    {
                        formRegisterStudents.ShowDialog();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(  ex.Message);
                    }
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                //message box:
                String text = ex.Message;
                text += "\r\n\r\nPlease check if DigitalPersona service has been started";
                String caption = "Cannot access readers";
                MessageBox.Show(text, caption);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(text_Name.Text == "" && text_RollNo.Text == "" && cb_sections.SelectedIndex < 0 && cb_subjects.SelectedIndex < 0)
            {
                MessageBox.Show("Please fill all fields");
            }
            if(fingerPrint != null)
            {
                st.Name = text_Name.Text;
                st.RollNo = text_RollNo.Text;
                st.Section = cb_sections.Text;
                st.Subject = cb_subjects.Text;
                st.Fingerprint = fingerPrint;
                fHND.addStudentToDatabase(st);
                MessageBox.Show("Student Successfully Added");
            }
            else
            {
                MessageBox.Show("Please add your Finger Print");
            }
        }
    }
}