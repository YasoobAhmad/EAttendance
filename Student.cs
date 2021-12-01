using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EAttendance
{
    public class Student
    {
        public string Name { get; set; }
        public string RollNo { get; set; }
        public string Subject { get; set; }
        public string Section { get; set; }
        public Fmd Fingerprint { get; set; }
    }
}
