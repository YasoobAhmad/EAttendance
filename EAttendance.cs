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
    public partial class Form_EAttendance : Form
    {
        public Form_EAttendance()
        {
            InitializeComponent();
        }

        private void Label_WelcomeMsg_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            Form_AddSubject formAddSubject = new Form_AddSubject();
            this.Hide();
            formAddSubject.ShowDialog();
            this.Show();
        }

        private void btn_RegisterStudent_Click(object sender, EventArgs e)
        {
            Form_RegisterStudents formRegisterStudents = new Form_RegisterStudents();
            this.Hide();
            formRegisterStudents.ShowDialog();
            this.Show();
        }
    }
}
