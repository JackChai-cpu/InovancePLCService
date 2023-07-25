using HslCommunication;
using InovancePlcServicesF;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InovancePlcServices
{
    public class InovancePlc
    {
        private readonly string ip;
        private readonly int port;
        private readonly int nNetId;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IP">以太网IP地址，</param>
        /// <param name="Port">以太网端口号,默认502(modbusTcp协议默认端口号为502)</param>
        /// <param name="Netid">网络链接编号,用于标记是第几条网络链接,取值范围0~255,默认0 </param>
        public InovancePlc(string IP, int Port, int Netid)
        {
            this.ip = IP;
            this.port = Port;
            this.nNetId = Netid;


        }

        public string Ip => ip;

        public int Port => port;

        public int NNetId => nNetId;


        public bool IsConnect = false;

        public bool PlcOpen()
        {
            try
            {
                if (string.IsNullOrEmpty(ip)) { IsConnect = false; return false; }
                bool result = StandardModbusApi.Init_ETH_String(ip, nNetId, port);
                if (result) { IsConnect = true; return true; }
                else { IsConnect = false; return false; }
            }
            catch
            {
                IsConnect = false;
                return false;
            }
        }

        public bool PlcClose()
        {
            return true;
        }

        /// <summary>
        /// 返回的值位每个byte的十进制数，需要转换为其他,需要自行转换
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startByteAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual object? PlcReadBytes(int startByteAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }

            var beginresult = new byte[count + 1];
            int nRet = StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, count, beginresult, nNetId);
            if (nRet == (int)Errcode.ER_READ_WRITE_FAIL) { }
            else if (nRet == 1) { }
            else if (nRet == 1) { }
            var result = new byte[count];
            Array.Copy(beginresult, result, count);
            return result;

        }

        public virtual async Task<object?> PlcReadBytesAsync(int startByteAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            var beginresult = new byte[count + 1];

            // 模拟耗时操作
            int nRet = await new Task<int>(() => StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, count, beginresult, nNetId));

            if (nRet == (int)Errcode.ER_READ_WRITE_FAIL) { }
            else if (nRet == 1) { }
            else if (nRet == 1) { }
            var result = new byte[count];
            Array.Copy(beginresult, result, count);
            return result;
        }

        private async Task<int> ReadBytesAsync(SoftElemType type, int startByteAdr, int count, byte[] beginresult)
        {
            return await new Task<int>(() => StandardModbusApi.H5u_Read_Soft_Elem(type, startByteAdr, count, beginresult, nNetId));
        }

        /// <summary>
        /// 返回一个16位的有符号整数
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startByteAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual object? PlcReadWords(int startByteAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            var result = new byte[count * 2];
            StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, count, result, nNetId);
            var wordresult = new int[count];
            for (int i = 0; i < count; i++)
            {
                byte[] databuf = new byte[2] { 0, 0 };
                databuf[0] = result[i * 2];
                databuf[1] = result[i * 2 + 1];
                wordresult[i] = BitConverter.ToInt16(databuf, 0);
            }
            return result;
        }

        public virtual bool PlcWrite()
        {
            return true;
        }







        public string ByteArrayTo2Base(Byte[] bytes)
        {
            // byte[]转为二进制字符串表示
            byte[] bytesTest = new byte[] { 192, 6 };
            string strResult = "";
            for (int i = 0; i < bytesTest.Length; i++)
            {
                string strTemp = System.Convert.ToString(bytesTest[i], 2);
                strTemp = strTemp.Insert(0, new string('0', 8 - strTemp.Length));

                strResult += strTemp;
            }
            return strResult;
        }

        /// <summary>
        /// 去除byte的最后一个数组
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] ByteArrayRemoveLast(Byte[] bytes)
        {
            if (bytes.Length == 0) { return bytes; }
            var result = new byte[bytes.Length];
            Array.Copy(bytes, result, bytes.Length);
            return result;
        }
    }
}