using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovancePLCService
{
    public class PLCServices
    {
        public InovancePlc InovancePlc;

        /// <summary>
        /// 创建连接实例
        /// </summary>
        /// <param name="cputype">PLC类型，h3u/h5u</param>
        /// <param name="IP">PLcIP地址</param>
        /// <param name="Port">plcip对应的端口号</param>
        /// <param name="Netid">网络链接编号,用于标记是第几条网络链接,取值范围0~255</param>
        public PLCServices(CpuType cputype, string IP, int Port, int Netid)
        {
            if (cputype.Equals(CpuType.H5u))
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
