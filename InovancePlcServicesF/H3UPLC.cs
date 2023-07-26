using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovancePLCService
{
    public class H3UPLC : InovancePlc
    {
        public H3UPLC(string IP, int Port, int Netid) : base(IP, Port, Netid)
        {

        }
    }
}
