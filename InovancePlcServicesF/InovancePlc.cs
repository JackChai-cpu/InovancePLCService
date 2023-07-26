using InovancePLCService;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InovancePLCService
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
        public virtual object PlcReadBytes(int startByteAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }

            int countindex = (int)(count / 2 + 0.5);
            //最多读取123个字或者246个字节
            if (count >= 246)
            {
                countindex = 123;
            }
            var beginresult = new byte[count];
            int nRet = StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, countindex, beginresult, nNetId);
            if (nRet == 1) { }
            else if (nRet == 1) { }
            else if (nRet == 1) { }
            var result = new byte[count];
            Array.Copy(beginresult, result, count);
            return result;

        }

        /// <summary>
        /// 同步获取一个16位的有符号整数
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startByteAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual object PlcReadWords(int startAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            var result = new byte[count * 2];
            StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count, result, nNetId);
            var wordresult = new int[count];
            for (int i = 0; i < count; i++)
            {
                byte[] databuf = new byte[2] { 0, 0 };
                databuf[0] = result[i * 2];
                databuf[1] = result[i * 2 + 1];
                wordresult[i] = BitConverter.ToInt16(databuf, 0);
            }
            return wordresult;
        }

        /// <summary>
        /// 读取有符号的32位整形数据
        /// </summary>
        /// <param name="startAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual object PlcReadDoublewords(int startAdr,int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            var result = new byte[count * 4];
            StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count*2, result, nNetId);
            var wordresult = new Int32[count];
            for (int i = 0; i < count; i++)
            {
                byte[] databuf = new byte[4] { 0, 0, 0, 0 };
                databuf[0] = result[i * 4];
                databuf[1] = result[i * 4 + 1];
                databuf[2] = result[i * 4 + 2];
                databuf[3] = result[i * 4 + 3];
                wordresult[i] = BitConverter.ToInt32(databuf, 0);
            }
            return wordresult;
        }

        /// <summary>
        /// 读取一个浮点数
        /// </summary>
        /// <param name="startAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual object PlcReadFloat(int startAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            var result = new byte[count * 4];
            StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count*2, result, nNetId);
            var wordresult = new float[count];
            for (int i = 0; i < count; i++)
            {
                byte[] databuf = new byte[4] { 0, 0, 0, 0 };
                databuf[0] = result[i * 4];
                databuf[1] = result[i * 4 + 1];
                databuf[2] = result[i * 4 + 2];
                databuf[3] = result[i * 4 + 3];
                wordresult[i] = BitConverter.ToSingle(databuf, 0);
            }
            return wordresult;
        }
        
        /// <summary>
        /// 读取byte类型数据（10进制）
        /// </summary>
        /// <param name="startByteAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual async Task<System.Byte[]> PlcReadBytesAsync(int startAdr, int count)
        {
            return  await Task.Run(() =>
                        {
                            if (!IsConnect)
                            {
                                PlcOpen();
                            }
                            int countindex = (int)(count / 2 + 0.5);
                            //最多读取123个字或者246个字节
                            if (count >= 246)
                            {
                                countindex = 123;
                            }
                            var beginresult = new byte[count];
                            Thread.Sleep(3000);
                            int nRet = StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, countindex, beginresult, NNetId);
                            //if (nRet == (int)Errcode.ER_READ_WRITE_FAIL) { }
                            //else if (nRet == 1) { }
                            //else if (nRet == 1) { }
                            var result = new byte[count];
                            Array.Copy(beginresult, result, count);

                            return result;
                        });

        }
        /// <summary>
        /// 异步获取一个16位有符号的整数（10进制）
        /// </summary>
        /// <param name="startByteAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual async Task<object> PlcReadWordsAsync(int startAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            var beginresult = new byte[count *2];

            // 进行异步读取
            int nRet = await new Task<int>(() => StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count, beginresult, nNetId));

            if (nRet == 1) { }
            else if (nRet == 1) { }
            else if (nRet == 1) { }
            var wordresult = new int[count];
            for (int i = 0; i < count; i++)
            {
                byte[] databuf = new byte[2] { 0, 0 };
                databuf[0] = beginresult[i * 2];
                databuf[1] = beginresult[i * 2 + 1];
                wordresult[i] = BitConverter.ToInt16(databuf, 0);
            }
            return wordresult;
        }


        /// <summary>
        /// 获取一个双字节类型的有符号整数
        /// </summary>
        /// <param name="startAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual async Task<object> PlcReadDoubleWordsAsync(int startAdr, int count)
        {
            return await Task.Run(() =>
            {
                if (!IsConnect)
                {
                    PlcOpen();
                }
                if (count >= 61)
                {
                    count = 61;
                }
                var result = new byte[count * 4];
                StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count * 2, result, NNetId);
                var wordresult = new Int32[count];
                for (int i = 0; i < count; i++)
                {
                    byte[] databuf = new byte[4] { 0, 0, 0, 0 };
                    databuf[0] = result[i * 4];
                    databuf[1] = result[i * 4 + 1];
                    databuf[2] = result[i * 4 + 2];
                    databuf[3] = result[i * 4 + 3];
                    wordresult[i] = BitConverter.ToInt32(databuf, 0);
                }
                return wordresult;
            });
        }

        /// <summary>
        /// 获取一个浮点型数
        /// </summary>
        /// <param name="startAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual async Task<object> PlcReadFloatsAsync(int startAdr, int count)
        {
            return await Task.Run(() =>
            {
                if (!IsConnect)
                {
                    PlcOpen();
                }
                if (count >= 61)
                {
                    count = 61;
                }
                var result = new byte[count * 4];
                StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count * 2, result, NNetId);
                var wordresult = new float[count];
                for (int i = 0; i < count; i++)
                {
                    byte[] databuf = new byte[4] { 0, 0, 0, 0 };
                    databuf[0] = result[i * 4];
                    databuf[1] = result[i * 4 + 1];
                    databuf[2] = result[i * 4 + 2];
                    databuf[3] = result[i * 4 + 3];
                    wordresult[i] = BitConverter.ToSingle(databuf, 0);
                }
                return wordresult;
            });
        }

        public virtual void PlcWriteBytes(int startAdr, byte[] value)
        {
            
        }

        /// <summary>
        /// 写一个字的数据，10进制
        /// </summary>
        /// <param name="startAdr"></param>
        /// <param name="value"></param>
        public virtual void PlcWriteWords(int startAdr, short[] value)
        {
            if (IsConnect)
            {
                PlcOpen();
            }
            int ncount = value.Length;
            byte[] pBuf = new byte[ncount * 2];

            for (int i = 0; i < ncount; i++)
            {
                byte[] dataBuf = BitConverter.GetBytes(value[i]);
                pBuf[2 * i] = dataBuf[0];
                pBuf[2 * i + 1] = dataBuf[1];
            }
            int nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount, pBuf, NNetId);
            if (nRet == 0) { }
        }

        /// <summary>
        /// 写入双字，10进制
        /// </summary>
        /// <param name="startAdr"></param>
        /// <param name="value"></param>
        public virtual void PlcWriteDoubleWords(int startAdr, int[] value)
        {

        }

        public virtual void PlcWriteFloats(int startAdr, float[] value)
        {

        }

        public virtual async Task PlcWriteBytesAsync(int startAdr, byte[] value)
        {
            await Task.Run(() =>
            {
                if (!IsConnect)
                {
                    PlcOpen();
                }
                int ncount = 1;
                int nRet = 0;
                if (0 == value.Length % 2)
                {
                    ncount = value.Length / 2;
                    nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount, value, NNetId);
                }
                else
                {
                    byte[] newbyte = new byte[value.Length + 1];
                    ncount = (int)((double)value.Length / 2 + 0.5);
                    byte[] nRets = (byte[])this.PlcReadBytes(startAdr + ncount - 1, 2);
                    for (int i = 0; i < value.Length; i++)
                    {
                        newbyte[i] = value[i];
                    }
                    newbyte[value.Length] = nRets[1];

                    nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount, newbyte, NNetId);
                }

                if (nRet == 0) { }

            });
        }

        public virtual async Task PlcWriteWordsAsync(int startAdr, byte[] value)
        {
            await Task.Run(() =>
            {
                if (!IsConnect)
                {
                    PlcOpen();
                }
                int ncount = value.Length;
                byte[] pBuf = new byte[ncount * 2];

                for (int i = 0; i < ncount; i++)
                {
                    byte[] dataBuf = BitConverter.GetBytes(value[i]);
                    pBuf[2 * i] = dataBuf[0];
                    pBuf[2 * i + 1] = dataBuf[1];
                }
                int nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount, pBuf, NNetId);
                if (nRet == 0) { }

            });
        }

        public virtual async Task PlcWriteDoubleWordsAsync(int startAdr, byte[] value)
        {
            await Task.Run(() =>
            {
                if (!IsConnect)
                {
                    PlcOpen();
                }
                int ncount = value.Length;
                byte[] pBuf = new byte[ncount * 4];

                for (int i = 0; i < ncount; i++)
                {
                    byte[] dataBuf = BitConverter.GetBytes(value[i]);
                    pBuf[4 * i] = dataBuf[0];
                    pBuf[4 * i + 1] = dataBuf[1];
                    pBuf[4 * i + 2] = dataBuf[2];
                    pBuf[4 * i + 3] = dataBuf[3];
                }
                int nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount * 2, pBuf, NNetId);
                if (nRet == 0) { }

            });
        }

        public virtual async Task PlcWriteFloatsAsync(int startAdr, byte[] value)
        {
            await Task.Run(() =>
            {
                if (!IsConnect)
                {
                    PlcOpen();
                }
                int ncount = value.Length;
                byte[] pBuf = new byte[ncount * 4];

                for (int i = 0; i < ncount; i++)
                {
                    byte[] dataBuf = BitConverter.GetBytes(value[i]);
                    pBuf[4 * i] = dataBuf[0];
                    pBuf[4 * i + 1] = dataBuf[1];
                    pBuf[4 * i + 2] = dataBuf[2];
                    pBuf[4 * i + 3] = dataBuf[3];
                }
                int nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount * 2, pBuf, NNetId);
                if (nRet == 0) { }

            });
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