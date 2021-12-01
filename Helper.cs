using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UareUSampleCSharp;

namespace EAttendance
{
    public class Helper
    {

        public FMDHandler fingerPrintHND = new FMDHandler();
        public bool Reset
        {
            get { return reset; }
            set { reset = value; }
        }
        private bool reset;

        public Reader CurrentReader
        {
            get { return currentReader; }
            set
            {
                currentReader = value;
                //SendMessage(Action.UpdateReaderState, value);
            }
        }

        private Reader currentReader;

        public Helper()
        {
            ReaderCollection readersCollection;
            try
            {
                readersCollection = ReaderCollection.GetReaders();
                if (readersCollection.Count<Reader>() < 1)
                {

                    //MessageBox.Show("No Readers Found on your machine");
                }
                else
                {
                    currentReader = readersCollection[0];
                    //Enrollment formRegisterStudents = new Enrollment();
                    //this.Hide();
                    //formRegisterStudents.ShowDialog();
                    //this.Show();

                }
            }
            catch (Exception ex)
            {
                //message box:
                String text = ex.Message;
                text += "\r\n\r\nPlease check if DigitalPersona service has been started";
                String caption = "Cannot access readers";
                //MessageBox.Show(text, caption);
            }
        }
        public bool OpenReader()
        {
            //using (Tracer tracer = new Tracer("Form_Main::OpenReader"))
            //{
                reset = false;
                Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;

                // Open reader
                result = currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

                if (result != Constants.ResultCode.DP_SUCCESS)
                {
                    //MessageBox.Show("Error:  " + result);
                    reset = true;
                    return false;
                }

                return true;
            //}
        }

        public void GetStatus()
        { 
            //using (Tracer tracer = new Tracer("Form_Main::GetStatus"))
            //{
                Constants.ResultCode result = currentReader.GetStatus();
                
            if ((result != Constants.ResultCode.DP_SUCCESS))
                {
                    reset = true;
                    throw new Exception("" + result);
                }

                if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
                {
                    Thread.Sleep(50);
                }
                else if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
                {
                    currentReader.Calibrate();
                }
                else if ((currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
                {
                    throw new Exception("Reader Status - " + currentReader.Status.Status);
                }
            //}
        }

        public bool CheckCaptureResult(CaptureResult captureResult)
        {
            using (Tracer tracer = new Tracer("Form_Main::CheckCaptureResult"))
            {
                if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        reset = true;
                        throw new Exception(captureResult.ResultCode.ToString());
                    }

                    // Send message if quality shows fake finger
                    if ((captureResult.Quality != Constants.CaptureQuality.DP_QUALITY_CANCELED))
                    {
                        throw new Exception("Quality - " + captureResult.Quality);
                    }
                    return false;
                }

                return true;
            }
        }

        public bool CaptureFingerAsync()
        {
            Console.WriteLine("123");
            try
            {
                GetStatus();

                Constants.ResultCode captureResult = currentReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, currentReader.Capabilities.Resolutions[0]);
                if (captureResult != Constants.ResultCode.DP_SUCCESS)
                {
                    reset = true;
                    throw new Exception("" + captureResult);
                }

                Console.WriteLine("123");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:  " + ex.Message);
                return false;
            }

        }
        public bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {

            // Activate capture handler
            currentReader.On_Captured += new Reader.CaptureCallback(OnCaptured);

            // Call capture
            if (!CaptureFingerAsync())
            {
                return false;
            }

            return true;
        }

        public void CancelCaptureAndCloseReader(Reader.CaptureCallback OnCaptured)
        {
            //using (Tracer tracer = new Tracer("Form_Main::CancelCaptureAndCloseReader"))
            //{
                if (currentReader != null)
                {
                    currentReader.CancelCapture();

                    // Dispose of reader handle and unhook reader events.
                    currentReader.Dispose();

                    if (reset)
                    {
                        CurrentReader = null;
                    }
                }
            //}
        }

    }
}
