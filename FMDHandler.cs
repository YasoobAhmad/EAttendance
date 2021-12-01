using DPUruNet;
using EAttendance;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace UareUSampleCSharp
{
    public class FMDHandler
    {
        List<Fmd> fingerPrints;
        public FMDHandler()
        {
            fingerPrints = new List<Fmd>();
        }

        public void addFingerPrint(Fmd fingerPrint)
        {
            Console.WriteLine(fingerPrint);
            fingerPrints.Add(fingerPrint);
        }

        public void addDataToDatabase(string str, Fmd fPrint)
        {
            //string connStr = "server=localhost;user=root;database=biometrics;port=3306;password=";
            //MySqlConnection conn = new MySqlConnection(connStr);

            //try
            //{
            //    Console.WriteLine("Connecting to MySQL...");
            //    conn.Open();
            //    string query = "Insert into fingerprints (Name, fingerprint) values(" + str + "," + fPrint.Bytes + ")"; 
            //    MySqlCommand command = new MySqlCommand(query, conn);
            //    command.CommandTimeout = 10;
            //    MySqlDataReader rdr = command.ExecuteReader();
            //    // Perform database operations
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //conn.Close();
            //Console.WriteLine("Done.");

            //byte[] fprintXML = fPrint.Bytes;
            //t.Serialize(ref(fprintXML));
            //MemoryStream fingerprintData = new MemoryStream();

            //DPFP.Template t = new DPFP.Template();
            //Serialize(fingerprintData);
            //fingerprintData.Position = 0;

            //BinaryReader br = new BinaryReader(fingerprintData);
            //Byte[] bytes = br.ReadBytes((Int32)fingerprintData.Length);

            //Insert the file into database
            //DPFP.Template t = new DPFP.Template();
            MySqlConnection cn = new MySqlConnection("server=localhost;user=root;database=biometrics;port=3306;password=");
            MySqlCommand cmd = new MySqlCommand("INSERT INTO fingerprints(name,fingerprint) VALUES(@name, @FINGERPRINT)", cn);
            cmd.Parameters.Add("name", MySqlDbType.VarChar).Value = str;
            cmd.Parameters.Add("FINGERPRINT", MySqlDbType.LongBlob).Value = fPrint.Bytes;

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();


        }


        public void addStudentToDatabase(Student s)
        { 
            //DPFP.Template t = new DPFP.Template();
            MySqlConnection cn = new MySqlConnection("server=localhost;user=root;database=EAttendance;port=3306;password=");
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Students(roll_no,name,subject, section, fingerprint) VALUES(@rollNo, @name, @subject, @section ,@fingerprint)", cn);
            cmd.Parameters.Add("name", MySqlDbType.VarChar).Value = s.Name;
            cmd.Parameters.Add("fingerprint", MySqlDbType.LongBlob).Value = s.Fingerprint.Bytes;
            cmd.Parameters.Add("rollNo", MySqlDbType.VarChar).Value = s.RollNo;
            cmd.Parameters.Add("subject", MySqlDbType.VarChar).Value = s.Subject;
            cmd.Parameters.Add("section", MySqlDbType.VarChar).Value = s.Section;
            
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            
        }



        public bool verification(Fmd fingerPrint)
        {
            MySqlConnection cn = new MySqlConnection("server=localhost;user=root;database=biometrics;port=3306;password=");
            MySqlCommand cmd = new MySqlCommand("Select * from fingerprints", cn);
            cn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                byte[] resultFromDB = (byte[])reader["fingerPrint"];// get bytes from database
                int ConversionFormate = Convert.ToInt32(Constants.Formats.Fmd.ANSI);
                Fmd obj = new Fmd(resultFromDB, ConversionFormate, Constants.WRAPPER_VERSION);
                CompareResult compareResult = Comparison.Compare(obj, 0, fingerPrint, 0);
                if (compareResult.Score.ToString().Equals("0"))
                {
                    return true;
                }
            }

            cn.Close();
            return false;

        }
    }
}