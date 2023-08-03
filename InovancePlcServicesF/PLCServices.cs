using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static InovancePLCService.PLCServices;

namespace InovancePLCService
{
    public class PLCServices
    {
        #region 字段
        public InovancePlc InovancePlc;
        private List<short> errcode=new List<short>();
        private List<string> errmsg=new List<string>();
        private PLCMoto travelMotor = new PLCMoto(0);
        private PLCMoto liftMotor = new PLCMoto(1);
        private PLCMoto forkMotor = new PLCMoto(2);

        /// <summary>
        /// 报警获取定时器
        /// </summary>
        private System.Timers.Timer errtimer = new System.Timers.Timer();

        private System.Timers.Timer motostatustimer = new System.Timers.Timer();
        /// <summary>
        /// 做访问属性或者变量使用，主要是防止跨线程对错误信息进行操作
        /// </summary>
        private static object obj=new object();
        
        //private System.Timers.Timer timer2 = new System.Timers.Timer();
        #endregion

        #region 属性
        /// <summary>
        /// 当前报警代码，数量为0时无报警
        /// </summary>
        public List<short> Errcode { get => errcode; }
        public List<string> Errmsg { get => errmsg; }
        public PLCMoto TravelMotor { get => travelMotor;  }
        public PLCMoto LiftMotor { get => liftMotor;  }
        public PLCMoto ForkMotor { get => forkMotor; }

        #endregion


        #region 委托
        public delegate void Geterrorcode(List<short> shorts);
        public delegate void GeterrorcodeAndMsg(List<short> shorts, List<string> Errmsg);
        #endregion

        #region 事件
        public event Geterrorcode GetErrorCode;
        public event GeterrorcodeAndMsg GetErrorCodeAndMsg;
        #endregion
       

        


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

        #region 公共方法

        /// <summary>
        /// 开启PLC通信服务
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool OpenService()
        {
            try
            {
                InovancePlc.PlcOpen();
                //绑定获取PLC报警代码
                errtimer.Elapsed += new System.Timers.ElapsedEventHandler(GetAlarmStatus);
                errtimer.Interval = 500;
                errtimer.AutoReset = true;
                errtimer.Enabled = false;
                errtimer.Start();
                //绑定获取PLC电机参数
                //motostatustimer.Elapsed += new System.Timers.ElapsedEventHandler(GetMotoStatus);
                //motostatustimer.Interval = 100;
                //motostatustimer.AutoReset = true;
                //motostatustimer.Enabled = false;
                //motostatustimer.Start();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("PLC服务打开失败");
            }
        }

        /// <summary>
        /// 关闭PLC服务
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool CloseService()
        {
            try
            {
                InovancePlc.PlcClose();
                errtimer.Stop();
                //motostatustimer.Stop();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("PLC服务关闭出错");
            }
        }

        /// <summary>
        /// 检查出入料口对应位置是否有物料
        /// </summary>
        /// <param name="portId">出入料口的编号</param>
        /// <param name="loc">出入料口</param>
        /// <returns></returns>
        public bool IsMaterialAtPort(int portId,int loc)
        {
            if (portId < 0 || loc < 0|| portId>5 || loc >3)
                throw new Exception("检查出入料口对应位置是否有物料函数索引错误");
            int address = PLCVariable.MaterialAtPort[portId-1][loc-1];
            return GetBoolSingel(address);
        }


        /// <summary>
        /// 出入料口均为3个work_position设计。根据上位机控制指令下发，驱动对应进出料口将物料驱动移送至指定工作位置
        /// </summary>
        /// <param name="portId">出入料口的编号</param>
        /// <param name="loc">出入料口</param>
        /// <returns></returns>
        public bool MovePort(int portId,string loc)
        {
            return false;
        }

        /// <summary>
        /// 出入料口测距传感器，执行测距动作，返回测距值
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public double MeasureDisAtPort(int portId)
        {
            return 0.0;
        }

        /// <summary>
        /// 行车模组执行运动，运动至指定进出料口的预设等待位置
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public bool MoveCrane2PortHolding(int portId)
        {
            return false;
        }

        /// <summary>
        /// 行车模组执行运动，运动至指定进出料口的拍照位姿
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public bool MoveCrane2PhotoPose(int portId)
        {
            return false;
        }

        /// <summary>
        /// 行车Fork顶部相机（二维码）拍照，并识别二维码信息
        /// </summary>
        /// <returns>info：二维码包含的字符串信息，string类型</returns>
        public string PhotoAndRead2DCodeInfo()
        {
            return string.Empty;
        }

        /// <summary>
        /// 行车执行物料插取动作，将物料收至行车模块的Fork仓位中；动作执行完成后，复位至入料口等待位置
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public bool MoveCranePickAtPort(int portId)
        {
            return false;
        }

        /// <summary>
        /// 行车执行物料插取动作，将物料从Fork上放至进出料口中；动作执行完成后，复位至入料口等待位置
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public bool MoveCranePlaceAtPort(int portId)
        {
            return false;
        }

        /// <summary>
        /// 行车模组执行插补运动，运动至指定仓库库位位置前的预设等待位置
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public bool MoveCrane2StockLocation(string locationId)
        {
            return false;
        }

        /// <summary>
        /// 行车模组执行运动，从指定仓库库位取料至Fork中；动作执行完成后，复位至入料口等待位置
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public bool MoveCranePickAtStockLoc(string locationId)
        {
            return false;
        }

        /// <summary>
        /// 行车模组执行运动，从Fork将物料放置指定仓位中；动作执行完成后，复位至入料口等待位置
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public bool MoveCranePlaceAtStockLoc(string locationId)
        {
            return false;
        }

        /// <summary>
        /// 回Home
        /// </summary>
        public void MoveCrane2Home()
        {

        }

        
        /// <summary>
        /// 读取报警状态，通过计时器来读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="elapsedEventArgs"></param>
        public void GetAlarmStatus(object sender, System.Timers.ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                List<short> errorcode = new List<short>();

                short[] status1 = InovancePlc.PlcReadWords(2000, 123);
                short[] status2 = InovancePlc.PlcReadWords(2123, 8);
                short[] status3 = InovancePlc.PlcReadWords(3000, 30);
                for (short i = 0; i < status1.Length; i++)
                {
                    if (status1[i] !=0)
                        errorcode.Add((short)(i + 1));
                }
                for (short i = 0; i < status2.Length; i++)
                {
                    if (status2[i] != 0)
                        errorcode.Add((short)(i + 124));
                }
                for (short i = 0; i < status3.Length; i++)
                {
                    if (status3[i] != 0)
                        errorcode.Add((short)(i + 200));
                }
                errcode = errorcode;
                errmsg = GetErrMsg(errorcode);
                GetErrorCode(Errcode);
            }
            catch
            {
                throw new Exception("读取报警状态错误");
            }
        }
        /// <summary>
        /// 读取报警状态和报警信息，通过计时器来读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="elapsedEventArgs"></param>
        public void GetAlarmStatusAndInfo(object sender, System.Timers.ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                List<short> errorcode = new List<short>();

                short[] status1 = InovancePlc.PlcReadWords(2000, 123);
                short[] status2 = InovancePlc.PlcReadWords(2123, 8);
                short[] status3 = InovancePlc.PlcReadWords(3000, 30);
                for (short i = 0; i < status1.Length; i++)
                {
                    if (status1[i] != 0)
                        errorcode.Add((short)(i + 1));
                }
                for (short i = 0; i < status2.Length; i++)
                {
                    if (status2[i] != 0)
                        errorcode.Add((short)(i + 124));
                }
                for (short i = 0; i < status3.Length; i++)
                {
                    if (status3[i] != 0)
                        errorcode.Add((short)(i + 200));
                }
                errcode = errorcode;
                errmsg = GetErrMsg(errorcode);

                GetErrorCode(Errcode);                
                GetErrorCodeAndMsg(Errcode, Errmsg);

            }
            catch
            {
                throw new Exception("读取报警状态错误");
            }
        }
        /// <summary>
        /// 开放给上层读取报警状态
        /// </summary>
        /// <returns></returns>
        public List<short> GetAlarmStatus()
        {
            try
            {
                List<short> errorcode = new List<short>();

                short[] status1 = InovancePlc.PlcReadWords(2000, 123);
                short[] status2 = InovancePlc.PlcReadWords(2123, 8);
                short[] status3 = InovancePlc.PlcReadWords(3000, 30);
                for (short i = 0; i < status1.Length; i++)
                {
                    if (status1[i] != 0)
                        errorcode.Add((short)(i + 1));
                }
                for (short i = 0; i < status2.Length; i++)
                {
                    if (status2[i] != 0)
                        errorcode.Add((short)(i + 124));
                }
                for (short i = 0; i < status3.Length; i++)
                {
                    if (status3[i] != 0)
                        errorcode.Add((short)(i + 200));
                }
                GetErrorCode(errorcode);
                var res=GetErrMsg(errorcode);
                return errorcode;
            }
            catch
            {
                throw new Exception("读取报警状态错误");
            }

        }
        #endregion

        #region 私有方法，暂时不提供上层调用，需要时开放

        #region 取料区工作数据
        /// <summary>
        /// 设置工作模式
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private bool SetWorkingMode(WorkingMode mode)
        {
            return SetInt16tosingel(1070, (short)mode);
        }

        /// <summary>
        /// 设置取料区工件类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private bool SetFeedingWorkpieceType(FeedingWorkpieceType type)
        {
            return SetInt16tosingel(1071, (short)type);
        }


        /// <summary>
        /// 设置人工测高检测区放行信号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool SetFeedingManualPuthighpass(bool value)
        {
            return SetBooltoSingel(1072, value);
        }

        /// <summary>
        /// 设置自动测高检测区放行信号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool SetFeedingAutoPuthighpass(bool value)
        {
            return SetBooltoSingel(1073, value);
        }

        /// <summary>
        /// 设置人工入库口放行信号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool SetFeedingManualPutStoragepass(bool value)
        {
            return SetBooltoSingel(1074, value);
        }

        /// <summary>
        /// 设置自动入库口放行信号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool SetFeedingAutoPutStoragepass(bool value)
        {
            return SetBooltoSingel(1075, value);
        }

        /// <summary>
        /// 设置取料区工作数据
        /// </summary>
        /// <param name="workportAssignment"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private bool SetFeedingWorkportAssignment(FeedingWorkportAssignment workportAssignment)
        {
            return SetInt16tosingel(1076, (short)workportAssignment);
        }


        #endregion

        #region 垛机工作数据
        /// <summary>
        /// 垛机工作给定模式
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private bool StockerCmdMode(WorkingMode mode)
        {
            return SetInt16tosingel(1000, (short)mode);
        }

        /// <summary>
        /// 设置垛机工作模式
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private bool StockerCmdMode(StockerWorkingMode mode)
        {
            return SetInt16tosingel(1001, (short)mode);
        }

        /// <summary>
        /// 设置库区号，0为单库区
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private bool StockerCmdStoreID(StockerWorkingMode mode)
        {
            return SetInt16tosingel(1002, (short)mode);
        }

        /// <summary>
        /// 设置自动取料垛机移动列号
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool StockerCmdAutoGetColumn(int column)
        {
            return SetInt16tosingel(1003, (short)column);
        }

        /// <summary>
        /// 设置自动取料垛机移动行号
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool StockerCmdAutoGetRow(int row)
        {
            return SetInt16tosingel(1004, (short)row);
        }

        /// <summary>
        /// 设置自动取料货叉伸出位置
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool StockerCmdAutoGetFork(int position)
        {
            return SetInt16tosingel(1005, (short)position);
        }

        /// <summary>
        /// 设置自动放料垛机移动列号
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool StockerCmdAutoPutColumn(int column)
        {
            return SetInt16tosingel(1006, (short)column);
        }

        /// <summary>
        /// 设置自动放料垛机移动行号
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool StockerCmdAutoPutRow(int row)
        {
            return SetInt16tosingel(1007, (short)row);
        }

        /// <summary>
        /// 设置自动放料货叉伸出位置
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool StockerCmdAutoPutFork(int position)
        {
            return SetInt16tosingel(1008, (short)position);
        }

        /// <summary>
        /// 设置手动取料垛机移动列号
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool StockerCmdManualGetColumn(int column)
        {
            return SetInt16tosingel(1009, (short)column);
        }

        /// <summary>
        /// 设置手动取料垛机移动行号
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool StockerCmdManulaGetRow(int row)
        {
            return SetInt16tosingel(1010, (short)row);
        }

        /// <summary>
        /// 设置手动取料货叉伸出位置，根据行号进行确认，1向左，2向右
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool StockerCmdManualGetFork(int position)
        {
            return SetInt16tosingel(1011, (short)position);
        }

        /// <summary>
        /// 设置手动放料垛机移动列号
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool StockerCmdManualPutColumn(int column)
        {
            return SetInt16tosingel(1012, (short)column);
        }

        /// <summary>
        /// 设置自动放料垛机移动行号
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool StockerCmdManualPutRow(int row)
        {
            return SetInt16tosingel(1013, (short)row);
        }

        /// <summary>
        /// 设置自动放料货叉伸出位置
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool StockerCmdManualPutFork(int position)
        {
            return SetInt16tosingel(1014, (short)position);
        }

        /// <summary>
        /// 货叉选择，PLC自主选择，每次只使用一个货叉，不为0时为软件指定货叉
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool StockerCmdForkData(int data)
        {
            return SetInt16tosingel(1015, (short)data);
        }

        /// <summary>
        /// 获取垛机状态
        /// </summary>
        /// <returns></returns>
        private int GetStockerStatusOnline()
        {
            return GetIntSingel(1040);
        }

        /// <summary>
        /// 判断货叉有无料
        /// </summary>
        /// <param name="index">1-8(包括)</param>
        /// <returns></returns>
        private bool GetStockerForkExits(int index)
        {
            if(index < 1 || index > 8)
            {
                throw new Exception("查看货叉有无料索引错误");
            }
            return Convert.ToBoolean(InovancePlc.PlcReadWords(1040+ index, 1)[0]);
        }

        /// <summary>
        /// 获取垛机时间
        /// </summary>
        /// <returns></returns>
        private int GetStockerStatusWorkTime()
        {
            return GetIntSingel(1049);
        }

        /// <summary>
        /// 获取命令状态
        /// </summary>
        /// <returns></returns>
        private int GetStockerCmdStatusWork()
        {
            return GetIntSingel(1050);
        }

        /// <summary>
        /// 获取设备总状态
        /// </summary>
        /// <returns></returns>
        private int GetWorkStatus()
        {
            return GetIntSingel(1051);
        }

        /// <summary>
        /// 获取错误码/正常码
        /// </summary>
        /// <returns></returns>
        private int GetRetCode()
        {
            return GetIntSingel(1052);
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private int GetCreateTime()
        {
            return GetIntSingel(1053);
        }

        #endregion

        #region 电机控制类
        /// <summary>
        /// 电机正转
        /// </summary>
        /// <param name="Motor"></param>
        public void MotoJogFwd(PLCMoto Motor)
        {
            if (!InovancePlc.PlcReadWordsBool(Motor.SevoAddress))
                throw new Exception("电机使能未打开");
            if (!InovancePlc.PlcReadWordsBool(Motor.MovingAddress))
                throw new Exception("电机正在运动中，请勿进行其他操作");
            InovancePlc.PlcWriteWords(Motor.JogFwdAddress, new short[1] { 1 });
            Thread.Sleep(50);
            InovancePlc.PlcWriteWords(Motor.JogFwdAddress, new short[1] { 0 });
        }

        /// <summary>
        /// 电机反转
        /// </summary>
        /// <param name="Motor"></param>
        public void MotoJogRec(PLCMoto Motor)
        {
            if (!InovancePlc.PlcReadWordsBool(Motor.SevoAddress))
                throw new Exception("电机使能未打开");
            if (!InovancePlc.PlcReadWordsBool(Motor.MovingAddress))
                throw new Exception("电机正在运动中，请勿进行其他操作");
            InovancePlc.PlcWriteWords(Motor.JogRevAddress, new short[1] { 1 });
            Thread.Sleep(50);
            InovancePlc.PlcWriteWords(Motor.JogRevAddress, new short[1] { 0 });
        }

        /// <summary>
        /// 电机定位启动（绝对）,需要给定位置信息
        /// </summary>
        /// <param name="Motor"></param>
        /// <param name="position"></param>
        /// <exception cref="Exception"></exception>
        public void MotoAbsMovw(PLCMoto Motor,float position)
        {
            if (!InovancePlc.PlcReadWordsBool(Motor.SevoAddress))
                throw new Exception("电机使能未打开");
            if (!InovancePlc.PlcReadWordsBool(Motor.MovingAddress))
                throw new Exception("电机正在运动中，请勿进行其他操作");
            InovancePlc.PlcWriteFloats(Motor.PositionAddress, new float[1] { position });

            InovancePlc.PlcWriteWords(Motor.CSTRAddress, new short[1] { 1 });
            Thread.Sleep(50);
            InovancePlc.PlcWriteWords(Motor.CSTRAddress, new short[1] { 0 });
        }

        /// <summary>
        /// 电机回零
        /// </summary>
        /// <param name="Motor"></param>
        public void MotoHome(PLCMoto Motor)
        {
            if (!InovancePlc.PlcReadWordsBool(Motor.SevoAddress))
                throw new Exception("电机使能未打开");
            if (!InovancePlc.PlcReadWordsBool(Motor.MovingAddress))
                throw new Exception("电机正在运动中，请勿进行其他操作");
            InovancePlc.PlcWriteWords(Motor.HomeAddress, new short[1] { 1 });
            Thread.Sleep(50);
            InovancePlc.PlcWriteWords(Motor.HomeAddress, new short[1] { 0 });
        }
        /// <summary>
        /// 电机报警复位
        /// </summary>
        /// <param name="Motor"></param>
        public void MotoReset(PLCMoto Motor)
        {
            //if (!InovancePlc.PlcReadWordsBool(Motor.SevoAddress))
            //    throw new Exception("电机使能未打开");
            //if (!InovancePlc.PlcReadWordsBool(Motor.MovingAddress))
            //    throw new Exception("电机正在运动中，请勿进行其他操作");
            InovancePlc.PlcWriteWords(Motor.RESAddress, new short[1] { 1 });
            Thread.Sleep(50);
            InovancePlc.PlcWriteWords(Motor.RESAddress, new short[1] { 0 });
        }
        /// <summary>
        /// 电机停止
        /// </summary>
        /// <param name="TravelMotor"></param>
        public void MotoStop(PLCMoto TravelMotor)
        {
            InovancePlc.PlcWriteWords(TravelMotor.JogFwdAddress, new short[1] { 0 });
            InovancePlc.PlcWriteWords(TravelMotor.JogRevAddress, new short[1] { 0 });
            InovancePlc.PlcWriteWords(TravelMotor.CSTRAddress, new short[1] { 0 });
            InovancePlc.PlcWriteWords(TravelMotor.RelativeSTRAddress, new short[1] { 0 });
            InovancePlc.PlcWriteWords(TravelMotor.HomeAddress, new short[1] { 0 });
            InovancePlc.PlcWriteWords(TravelMotor.RESAddress, new short[1] { 0 });
            InovancePlc.PlcWriteWords(TravelMotor.HLTAddress, new short[1] { 1 });
            Thread.Sleep(50);
            InovancePlc.PlcWriteWords(TravelMotor.HLTAddress, new short[1] { 0 });
        }

        /// <summary>
        /// 电机状态获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="elapsedEventArgs"></param>
        public void GetMotoStatus(object sender, System.Timers.ElapsedEventArgs elapsedEventArgs)
        {
            PLCMoto[] pLCMotos= new PLCMoto[3] { travelMotor, liftMotor, forkMotor };
            for(int i=0; i<pLCMotos.Length; i++)
            {
                pLCMotos[i].PositionMonitorVaule = GetRealData(pLCMotos[i].PositionMonitorAddress);
                pLCMotos[i].SpeedMonitorVaule = GetRealData(pLCMotos[i].SpeedMonitorAddress);

                pLCMotos[i].OverAccVaule = GetRealData(pLCMotos[i].OverAccAddress);
                pLCMotos[i].OverDecVaule = GetRealData(pLCMotos[i].OverDecAddress);
                pLCMotos[i].SpeedSetVaule = GetRealData(pLCMotos[i].SpeedSetAddress);
                pLCMotos[i].PositionVaule = GetRealData(pLCMotos[i].PositionAddress);
                pLCMotos[i].UppermotortravelVaule = GetRealData(pLCMotos[i].UppermotortravelAddress);
                pLCMotos[i].LowermotortravelVaule = GetRealData(pLCMotos[i].LowermotortravelAddress);

                pLCMotos[i].EncoderDirectionVaule = GetBoolSingel(pLCMotos[i].EncoderDirectionAddress);
                pLCMotos[i].HENDVaule = GetBoolSingel(pLCMotos[i].HENDAddress);
                pLCMotos[i].PENDVaule = GetBoolSingel(pLCMotos[i].PENDAddress);
                pLCMotos[i].MovingVaule = GetBoolSingel(pLCMotos[i].MovingAddress);
                pLCMotos[i].SvoingVaule = GetBoolSingel(pLCMotos[i].SvoingAddress);

                pLCMotos[i].SevoVaule = GetBoolSingel(pLCMotos[i].SevoAddress);
                pLCMotos[i].AutoVaule = GetBoolSingel(pLCMotos[i].AutoAddress);
                pLCMotos[i].BrakeVaule = GetBoolSingel(pLCMotos[i].BrakeAddress);
                pLCMotos[i].HomeDirVaule = GetBoolSingel(pLCMotos[i].HomeDirAddress);
            }
        }

        #endregion



        private bool GetBoolSingel(int address)
        {
            return Convert.ToBoolean( InovancePlc.PlcReadWords(address, 1)[0]);
        }

        private int GetIntSingel(int address)
        {
            return InovancePlc.PlcReadWords(address, 1)[0];
        }

        private float GetRealData(int address)
        {
            return InovancePlc.PlcReadFloat(address, 1)[0];
        }

        private bool SetBooltoSingel(int ads, bool value)
        {
            try
            {
                short res = Convert.ToInt16(value);
                InovancePlc.PlcWriteWords(ads, new short[1] { res });
                if (InovancePlc.PlcReadWords(ads, 1)[0] == res)
                    return true;
                return false;
            }
            catch
            {
                throw new Exception("plc错误，请检查连接状态");

            }
        }
        private bool SetInt16tosingel(int ads, short value)
        {
            try
            {
                InovancePlc.PlcWriteWords(ads, new short[1] { (short)value });
                if (InovancePlc.PlcReadWords(ads, 1)[0] == (short)value)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw new Exception("plc错误，请检查连接状态");
            }
        }

        private List<string> GetErrMsg(List<short> errcode)
        {
            lock(obj)
            {
                errmsg.Clear();
                for (int i = 0; i < errcode.Count; i++)
                {
                    string res = PlcAlarmInfo.AlarmInfo[errcode[i]];
                    if (!errmsg.Contains(res))
                        errmsg.Add(res);
                }
                return errmsg;
            }            
        }
        #endregion
    }
}
