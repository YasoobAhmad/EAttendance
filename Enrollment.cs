using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DPUruNet;
using EAttendance;
using UareUSampleCSharp;

namespace EAttendance
{
    public partial class Enrollment : Form
    {
        /// <summary>
        /// Holds the main form with many functions common to all of SDK actions.
        /// </summary>
        public Helper helper;
        
        List<Fmd> preenrollmentFmds;
        private RichTextBox txtEnroll;
        int count;
        Form_RegisterStudents sender;
        public Enrollment(Form_RegisterStudents form_RegisterStudents)
        {
            helper = new Helper();
            
            //sender = form_RegisterStudents;
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enrollment_Load(object sender, System.EventArgs e)
        {
            txtEnroll.Text = string.Empty;
            preenrollmentFmds = new List<Fmd>();
            count = 0;
            SendMessage(Action.SendMessage, "Place a finger on the reader.");
            
            if (!helper.OpenReader())
            {
                
                this.Close();
            }

            if (!helper.StartCaptureAsync(this.OnCaptured))
            {
                this.Close();
            }
        }

        /// <summary>
        /// Handler for when a fingerprint is captured.
        /// </summary>
        /// <param name="captureResult">contains info and data on the fingerprint capture</param>
        private void OnCaptured(CaptureResult captureResult)
        {

            try
            {
                // Check capture quality and throw an error if bad.
                if (!helper.CheckCaptureResult(captureResult)) return;

                count++;

                //Form2 f2 = new Form2();
                //byte[] bt = captureResult.Data.Bytes;


                // int ConversionFormate = Convert.ToInt32(Constants.Formats.Fmd.ANSI);
                // Fmd obj = new Fmd(bt, ConversionFormate, Constants.WRAPPER_VERSION);

                // foreach (Fid.Fiv fiv in  captureResult.Data.Views)
                // {
                //      f2.setPicture(_sender.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                //      // _sender.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                // }

                //  f2.ShowDialog();


                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                SendMessage(Action.SendMessage, "A finger was captured.  \r\nCount:  " + (count));

                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    helper.Reset = true;
                    throw new Exception(resultConversion.ResultCode.ToString());
                }


                preenrollmentFmds.Add(resultConversion.Data);

                if (count >= 4)
                {

                    this.Hide();
                    DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preenrollmentFmds);
                    if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                    {
                        SendMessage(Action.SendMessage, "An enrollment FMD was successfully created.");
                        SendMessage(Action.SendMessage, "Place a finger on the reader.");

                        //helper.fingerPrintHND.addFingerPrint(preenrollmentFmds[0]);

                        sender.fingerPrint = preenrollmentFmds[0];

                        //helper.fingerPrintHND.addDataToDatabase("Name", preenrollmentFmds[0]);
                        //_sender.fingerPrintHND.verification(preenrollmentFmds[0]);
                        preenrollmentFmds.Clear();
                        count = 0;
                        return;
                    }
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                        SendMessage(Action.SendMessage, "Enrollment was unsuccessful.  Please try again.");
                        SendMessage(Action.SendMessage, "Place a finger on the reader.");
                        preenrollmentFmds.Clear();
                        count = 0;
                        return;
                    }
                }

                SendMessage(Action.SendMessage, "Now place the same finger on the reader.");
            }
            catch (Exception ex)
            {
                // Send error message, then close form
                SendMessage(Action.SendMessage, "Error:  " + ex.Message);
            }
        }

        /// <summary>
        /// Close window.
        /// </summary>
        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Close window.
        /// </summary>

        #region SendMessage
        private enum Action
        {
            SendMessage
        }

        private delegate void SendMessageCallback(Action action, string payload);
        private void SendMessage(Action action, string payload)
        {
            try
            {
                if (this.txtEnroll.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendMessage:
                            txtEnroll.Text += payload + "\r\n\r\n";
                            txtEnroll.SelectionStart = txtEnroll.TextLength;
                            txtEnroll.ScrollToCaret();
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void InitializeComponent()
        {
            this.txtEnroll = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtEnroll
            // 
            this.txtEnroll.Location = new System.Drawing.Point(12, 12);
            this.txtEnroll.Name = "txtEnroll";
            this.txtEnroll.Size = new System.Drawing.Size(365, 241);
            this.txtEnroll.TabIndex = 0;
            this.txtEnroll.Text = "";
            this.txtEnroll.TextChanged += new System.EventHandler(this.txtEnroll_TextChanged);
            // 
            // Enrollment
            // 
            this.ClientSize = new System.Drawing.Size(389, 301);
            this.Controls.Add(this.txtEnroll);
            this.Name = "Enrollment";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Enrollment_Closed);
            this.Load += new System.EventHandler(this.Enrollment_Load);
            this.ResumeLayout(false);

        }

        
        private void txtEnroll_TextChanged(object sender, EventArgs e)
        {

        }

        private void Enrollment_Closed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("closedclosedclosedclosed");
            helper.CancelCaptureAndCloseReader(this.OnCaptured);
        }
    }
}
