using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InovancePLCService
{
    public class PLCMoto
    {
        #region 字段
        public int index = 0;
        private int jogFwd=1130;
        private int jogRev=1131;
        private int cSTR=1132;
        private int relativeSTR = 1133;
        private int home = 1134;
        private int hLT = 1135;
        private int rES = 1136;

        /// <summary>
        /// 返回的为实数类型，当前位置
        /// </summary>
        private int positionMonitor = 1160;
        /// <summary>
        /// 返回的为实数类型
        /// </summary>
        private int speedMonitor = 1162;
        private int encoderDirection = 1164;
        private int hEND = 1165;
        private int pEND = 1166;
        private int moving = 1167;
        private int svoing = 1168;


        private int overAcc = 1190;
        private int overDec = 1192;
        private int speedSet = 1194;
        private int position = 1196;
        private int uppermotortravel = 1198;
        private int lowermotortravel = 1200;
        private int sevo = 1202;
        private int auto = 1203;
        private int brake = 1204;
        private int homeDir = 1205;

        #endregion
        public PLCMoto(int numindex) 
        {
            index=numindex;
        }

        public int JogFwd { get => jogFwd + index*100;  }
        public int JogRev { get => jogRev + index * 100;  }
        public int CSTR { get => cSTR + index * 100; }
        public int RelativeSTR { get => relativeSTR + index * 100; }
        public int Home { get => home + index * 100; }
        public int HLT { get => hLT + index * 100; }
        public int RES { get => rES + index * 100;   }

        public int PositionMonitor { get => positionMonitor + index * 100; }
        public int SpeedMonitor { get => speedMonitor + index * 100;  }
        public int EncoderDirection { get => encoderDirection + index * 100; }
        public int HEND { get => hEND + index * 100; }
        public int PEND { get => pEND + index * 100; }
        public int Moving { get => moving + index * 100; }
        public int Svoing { get => svoing + index * 100; }


        public int OverAcc { get => overAcc + index * 100; }
        public int OverDec { get => overDec + index * 100;  }
        public int SpeedSet { get => speedSet + index * 100; }
        public int Position { get => position + index * 100; }
        public int Uppermotortravel { get => uppermotortravel + index * 100; }
        public int Lowermotortravel { get => lowermotortravel + index * 100; }
        public int Sevo { get => sevo + index * 100; }
        public int Auto { get => auto + index * 100; }
        public int Brake { get => brake + index * 100; }
        public int HomeDir { get => homeDir + index * 100; }
    }
}
