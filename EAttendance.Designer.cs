
namespace EAttendance
{
    partial class Form_EAttendance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_WelcomeMsg = new System.Windows.Forms.Label();
            this.btn_AddSubject = new System.Windows.Forms.Button();
            this.btn_MarkAttendance = new System.Windows.Forms.Button();
            this.btn_RegisterStudent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_WelcomeMsg
            // 
            this.Label_WelcomeMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_WelcomeMsg.AutoSize = true;
            this.Label_WelcomeMsg.Font = new System.Drawing.Font("Segoe Script", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_WelcomeMsg.Location = new System.Drawing.Point(-11, 9);
            this.Label_WelcomeMsg.Name = "Label_WelcomeMsg";
            this.Label_WelcomeMsg.Size = new System.Drawing.Size(631, 57);
            this.Label_WelcomeMsg.TabIndex = 0;
            this.Label_WelcomeMsg.Text = "Welcome to E-Attendance System";
            this.Label_WelcomeMsg.Click += new System.EventHandler(this.Label_WelcomeMsg_Click);
            // 
            // btn_AddSubject
            // 
            this.btn_AddSubject.BackColor = System.Drawing.Color.Navy;
            this.btn_AddSubject.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddSubject.ForeColor = System.Drawing.Color.White;
            this.btn_AddSubject.Location = new System.Drawing.Point(61, 109);
            this.btn_AddSubject.Name = "btn_AddSubject";
            this.btn_AddSubject.Size = new System.Drawing.Size(242, 75);
            this.btn_AddSubject.TabIndex = 1;
            this.btn_AddSubject.Text = "Add New Subject";
            this.btn_AddSubject.UseVisualStyleBackColor = false;
            this.btn_AddSubject.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_MarkAttendance
            // 
            this.btn_MarkAttendance.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_MarkAttendance.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_MarkAttendance.ForeColor = System.Drawing.Color.White;
            this.btn_MarkAttendance.Location = new System.Drawing.Point(191, 218);
            this.btn_MarkAttendance.Name = "btn_MarkAttendance";
            this.btn_MarkAttendance.Size = new System.Drawing.Size(286, 105);
            this.btn_MarkAttendance.TabIndex = 1;
            this.btn_MarkAttendance.Text = "Mark Attendance";
            this.btn_MarkAttendance.UseVisualStyleBackColor = false;
            this.btn_MarkAttendance.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_RegisterStudent
            // 
            this.btn_RegisterStudent.BackColor = System.Drawing.Color.Navy;
            this.btn_RegisterStudent.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RegisterStudent.ForeColor = System.Drawing.Color.White;
            this.btn_RegisterStudent.Location = new System.Drawing.Point(364, 109);
            this.btn_RegisterStudent.Name = "btn_RegisterStudent";
            this.btn_RegisterStudent.Size = new System.Drawing.Size(242, 75);
            this.btn_RegisterStudent.TabIndex = 1;
            this.btn_RegisterStudent.Text = "Register Students";
            this.btn_RegisterStudent.UseVisualStyleBackColor = false;
            this.btn_RegisterStudent.Click += new System.EventHandler(this.btn_RegisterStudent_Click);
            // 
            // Form_EAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(651, 370);
            this.Controls.Add(this.btn_MarkAttendance);
            this.Controls.Add(this.btn_RegisterStudent);
            this.Controls.Add(this.btn_AddSubject);
            this.Controls.Add(this.Label_WelcomeMsg);
            this.Name = "Form_EAttendance";
            this.Text = "EAttendance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_WelcomeMsg;
        private System.Windows.Forms.Button btn_AddSubject;
        private System.Windows.Forms.Button btn_MarkAttendance;
        private System.Windows.Forms.Button btn_RegisterStudent;
    }
}