using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovancePlcServicesF
{
    internal class ceshi
    {
        public Plc plc;
        public void open()
        {
            plc = new Plc(CpuType.S71200, "127.0.0.1", 0, 1);
            plc.Open();
            plc.OpenAsync();
            plc.Read();
            plc.ReadAsync();
            plc.ReadBytes(DataType.);
            plc.read
            plc.ReadBytesAsync();
            plc.Close();
            
        }
    }
}
