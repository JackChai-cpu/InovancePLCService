using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovancePLCService
{
    public class PLCVariable
    {
        /// <summary>
        /// 出入料口检测传感器的地址表
        /// </summary>
        public static int[][] MaterialAtPort = new int[][] {new int[] {1000,1002, 1003 }, new int[] { 1006, 1007, 1008 }, new int[] { 1011, 1012, 1013 }, new int[] { 1021, 1022, 1023 }, new int[] { 1031, 1032, 1033 },};
    }
}
