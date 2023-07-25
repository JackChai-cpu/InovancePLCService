using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovancePlcServices
{
    internal class PLCServices
    {
        public InovancePlc InovancePlc;

        public PLCServices(CpuType cputype, string IP, int Port, int Netid)
        {
            if (cputype.Equals(CpuType.H3u))
            {
                InovancePlc = new H5UPLC(IP, Port, Netid);
            }
            else
            {
                InovancePlc = new H3UPLC(IP, Port, Netid);
            }
        }
    }
}
