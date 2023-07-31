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

        /// <summary>
        /// 检查出入料口对应位置是否有物料
        /// </summary>
        /// <param name="portId">出入料口的编号</param>
        /// <param name="loc">出入料口</param>
        /// <returns></returns>
        public bool IsMaterialAtPort(int portId,string loc)
        {
            return false;
        }

        /// <summary>
        /// 出入料口均为3个work_position设计。根据上位机控制指令下发，驱动对应进出俩口将物料驱动移送至指定工作位置
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

        public List<short> GetAlarmStatus()
        {
            try
            {
                List<short> errorcode = new List<short>();

                short[] status1 = (short[])InovancePlc.PlcReadWords(2000, 123);
                short[] status2 = (short[])InovancePlc.PlcReadWords(2123, 8);
                short[] status3 = (short[])InovancePlc.PlcReadWords(3000, 30);
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
                        errorcode.Add((short)(i + 133));
                }
                return errorcode;
            }
            catch
            {
                return new List<short>();
            }

        }
    }
}
