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
        private int jogFwdAddress = 1130;
        private int jogRevAddress = 1131;
        private int cSTRAddress = 1132;
        private int relativeSTRAddress = 1133;
        private int homeAddress = 1134;
        private int hLTAddress = 1135;
        private int rESAddress = 1136;

        /// <summary>
        /// 返回的为实数类型，当前位置
        /// </summary>
        private int positionMonitorAddress = 1160;
        /// <summary>
        /// 返回的为实数类型
        /// </summary>
        private int speedMonitorAddress = 1162;
        private int encoderDirectionAddress = 1164;
        private int hENDAddress = 1165;
        private int pENDAddress = 1166;
        private int movingAddress = 1167;
        private int svoingAddress = 1168;


        private int overAccAddress = 1190;
        private int overDecAddress = 1192;
        private int speedSetAddress = 1194;
        private int positionAddress = 1196;
        private int uppermotortravelAddress = 1198;
        private int lowermotortravelAddress = 1200;
        private int sevoAddress = 1202;
        private int autoAddress = 1203;
        private int brakeAddress = 1204;
        private int homeDirAddress = 1205;

        #region
        private int jogFwdVaule ;
        private int jogRevVaule ;
        private int cSTRVaule ;
        private int relativeSTRVaule ;
        private int homeVaule ;
        private int hLTVaule;
        private int rESVaule;

        /// <summary>
        /// 返回的为实数类型，当前位置
        /// </summary>
        private float positionMonitorVaule;
        /// <summary>
        /// 返回的为实数类型
        /// </summary>
        private float speedMonitorVaule;
        private int encoderDirectionVaule;
        private int hENDVaule;
        private int pENDVaule;
        private int movingVaule;
        private int svoingVaule;


        private float overAccVaule;
        private float overDecVaule;
        private float speedSetVaule;
        private float positionVaule;
        private float uppermotortravelVaule;
        private float lowermotortravelVaule;
        private int sevoVaule;
        private int autoVaule;
        private int brakeVaule;
        private int homeDirVaule;
        #endregion



        #endregion
        public PLCMoto(int numindex) 
        {
            index=numindex;
        }

        public int JogFwdAddress { get => jogFwdAddress + index*100;  }
        public int JogRevAddress { get => jogRevAddress + index * 100;  }
        public int CSTRAddress { get => cSTRAddress + index * 100; }
        public int RelativeSTRAddress { get => relativeSTRAddress + index * 100; }
        public int HomeAddress { get => homeAddress + index * 100; }
        public int HLTAddress { get => hLTAddress + index * 100; }
        public int RESAddress { get => rESAddress + index * 100;   }

        public int PositionMonitorAddress { get => positionMonitorAddress + index * 100; }
        public int SpeedMonitorAddress { get => speedMonitorAddress + index * 100;  }
        public int EncoderDirectionAddress { get => encoderDirectionAddress + index * 100; }
        public int HENDAddress { get => hENDAddress + index * 100; }
        public int PENDAddress { get => pENDAddress + index * 100; }
        public int MovingAddress { get => movingAddress + index * 100; }
        public int SvoingAddress { get => svoingAddress + index * 100; }


        public int OverAccAddress { get => overAccAddress + index * 100; }
        public int OverDecAddress { get => overDecAddress + index * 100;  }
        public int SpeedSetAddress { get => speedSetAddress + index * 100; }
        public int PositionAddress { get => positionAddress + index * 100; }
        public int UppermotortravelAddress { get => uppermotortravelAddress + index * 100; }
        public int LowermotortravelAddress { get => lowermotortravelAddress + index * 100; }
        public int SevoAddress { get => sevoAddress + index * 100; }
        public int AutoAddress { get => autoAddress + index * 100; }
        public int BrakeAddress { get => brakeAddress + index * 100; }
        public int HomeDirAddress { get => homeDirAddress + index * 100; }


        public int JogFwdVaule { get => jogFwdVaule; set => jogFwdVaule = value; }
        public int JogRevVaule { get => jogRevVaule; set => jogRevVaule = value; }
        public int CSTRVaule { get => cSTRVaule; set => cSTRVaule = value; }
        public int RelativeSTRVaule { get => relativeSTRVaule; set => relativeSTRVaule = value; }
        public int HomeVaule { get => homeVaule; set => homeVaule = value; }
        public int HLTVaule { get => hLTVaule; set => hLTVaule = value; }
        public int RESVaule { get => rESVaule; set => rESVaule = value; }
        /// <summary>
        /// 伺服当前位置
        /// </summary>
        public float PositionMonitorVaule { get => positionMonitorVaule; set => positionMonitorVaule = value; }
        /// <summary>
        /// 伺服当前速度
        /// </summary>
        public float SpeedMonitorVaule { get => speedMonitorVaule; set => speedMonitorVaule = value; }
        /// <summary>
        /// 伺服当前运行方向
        /// </summary>
        public int EncoderDirectionVaule { get => encoderDirectionVaule; set => encoderDirectionVaule = value; }
        /// <summary>
        /// 伺服回零完成信号
        /// </summary>
        public int HENDVaule { get => hENDVaule; set => hENDVaule = value; }
        /// <summary>
        /// 伺服定位完成信号
        /// </summary>
        public int PENDVaule { get => pENDVaule; set => pENDVaule = value; }
        /// <summary>
        /// 伺服移动中
        /// </summary>
        public int MovingVaule { get => movingVaule; set => movingVaule = value; }
        /// <summary>
        /// 上电反馈
        /// </summary>
        public int SvoingVaule { get => svoingVaule; set => svoingVaule = value; }
        /// <summary>
        /// 伺服加速度
        /// </summary>
        public float OverAccVaule { get => overAccVaule; set => overAccVaule = value; }
        /// <summary>
        /// 伺服减速度
        /// </summary>
        public float OverDecVaule { get => overDecVaule; set => overDecVaule = value; }
        /// <summary>
        /// 伺服速度设置
        /// </summary>
        public float SpeedSetVaule { get => speedSetVaule; set => speedSetVaule = value; }
        /// <summary>
        /// 伺服为位置设置
        /// </summary>
        public float PositionVaule { get => positionVaule; set => positionVaule = value; }
        /// <summary>
        /// 行程上限位
        /// </summary>
        public float UppermotortravelVaule { get => uppermotortravelVaule; set => uppermotortravelVaule = value; }
        /// <summary>
        /// 形成下限位
        /// </summary>
        public float LowermotortravelVaule { get => lowermotortravelVaule; set => lowermotortravelVaule = value; }
        /// <summary>
        /// 伺服使能状态
        /// </summary>
        public int SevoVaule { get => sevoVaule; set => sevoVaule = value; }
        /// <summary>
        /// 伺服工作模式，手自动
        /// </summary>
        public int AutoVaule { get => autoVaule; set => autoVaule = value; }
        /// <summary>
        /// 伺服抱闸控制
        /// </summary>
        public int BrakeVaule { get => brakeVaule; set => brakeVaule = value; }
        /// <summary>
        /// 回零方向，true正方向寻炸零点
        /// </summary>
        public int HomeDirVaule { get => homeDirVaule; set => homeDirVaule = value; }
    }
}
