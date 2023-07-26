using S7.Net.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InovancePLCService
{
    public class H5UPLC : InovancePlc
    {
        public H5UPLC(string IP, int Port, int Netid) : base(IP, Port, Netid)
        {
        }


        public override object PlcReadBytes(int startByteAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }

            int countindex = (int)((double)count / 2 + 0.5);
            //最多读取123个字或者246个字节
            if (count >=246)
            {
                countindex = 123;
            }            
            var beginresult = new byte[count];
            int nRet = StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, countindex, beginresult, NNetId);
            if (nRet == 1) { }
            else if (nRet == 1) { }
            else if (nRet == 1) { }
            var result = new byte[count];
            Array.Copy(beginresult, result, count);
            return result;
        }

        public override object PlcReadWords(int startAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            if (count >= 123)
            {
                count = 123;
            }
            var result = new byte[count * 2];
            int nget=StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count, result, NNetId);
            if (nget==1)
            {
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
            else if(nget==2)
            {
                throw new PLCExcpetion(Errcode.ErrNotConnect,"未连接");
            }
            else if(nget==3)
            {
                throw new PLCExcpetion(Errcode.ErrElemTypeWrong, "元件类型错误");
            }
            else if (nget == 4)
            {
                throw new PLCExcpetion(Errcode.ErrElemAddrOver, "元件地址溢出");
            }
            else if (nget == 5)
            {
                throw new PLCExcpetion(Errcode.ErrElemCountOver, "元件个数超限");
            }
            else if (nget == 6)
            {
                throw new PLCExcpetion(Errcode.ErrCommExcept, "通讯异常");
            }
            else
            {
                throw new PLCExcpetion(Errcode.ErrNotConnect, "读取失败");
            }

        }

        /// <summary>
        /// 读取有符号的32位整形，最多一次读取61个
        /// </summary>
        /// <param name="startAdr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override object PlcReadDoublewords(int startAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            if(count >= 61)
            {
                count = 61;
            }
            var result = new byte[count * 4];
            StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count*2 , result, NNetId);
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

        public override object PlcReadFloat(int startAdr, int count)
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
            StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count*2, result, NNetId);
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

        public override async Task<byte[]> PlcReadBytesAsync(int startByteAdr, int count)
        {
            return await Task.Run(() =>
            {
                if (!IsConnect)
                {
                    PlcOpen();
                }
                int countindex = (int)((double)count / 2 + 0.5);

                //最多读取123个字或者246个字节
                if (count >= 246)
                {
                    countindex = 123;
                }
                var beginresult = new byte[count];
                int nRet = StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, countindex, beginresult, NNetId);
                //if (nRet == (int)Errcode.ER_READ_WRITE_FAIL) { }
                //else if (nRet == 1) { }
                //else if (nRet == 1) { }
                var result = new byte[count];
                Array.Copy(beginresult, result, count);

                return result;
            });

        }


        public override async Task<object> PlcReadWordsAsync(int startAdr, int count)
        {
            return await Task.Run(() =>
            {
                if (!IsConnect)
                {
                    PlcOpen();
                }
                if (count >= 123)
                {
                    count = 123;
                }
                var result = new byte[count * 2];
                StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, count, result, NNetId);
                var wordresult = new int[count];
                for (int i = 0; i < count; i++)
                {
                    byte[] databuf = new byte[2] { 0, 0 };
                    databuf[0] = result[i * 2];
                    databuf[1] = result[i * 2 + 1];
                    wordresult[i] = BitConverter.ToInt16(databuf, 0);
                }
                return wordresult;
            });
            
        }

        public override async Task<object> PlcReadDoubleWordsAsync(int startAdr, int count)
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

        public override async Task<object> PlcReadFloatsAsync(int startAdr, int count)
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

        public override void PlcWriteBytes(int startAdr, byte[] value)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            int ncount = 1;
            int nRet = 0;
            if (0==value.Length % 2)
            {
                ncount=value.Length / 2;
                nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount, value, NNetId);
            }
            else 
            { 
                byte[] newbyte = new byte[value.Length+1];
                ncount = (int)((double)value.Length / 2 + 0.5);
                byte[] nRets = (byte[])this.PlcReadBytes(startAdr+ncount-1 , 2);
                for(int i = 0;i< value.Length; i++)
                {
                    newbyte[i] = value[i];
                }
                newbyte[value.Length] = nRets[1];

                nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount, newbyte, NNetId);
            }            

            if (nRet == 0) { }
        }

        public override void PlcWriteWords(int startAdr, short[] value)
        {
            if(!IsConnect)
            {
                PlcOpen();
            }
            int ncount = value.Length;
            byte[] pBuf=new byte[ncount*2];

            for (int i = 0;i < ncount;i++)
            {
                byte[] dataBuf = BitConverter.GetBytes(value[i]);
                pBuf[2 * i] = dataBuf[0];
                pBuf[2 * i + 1] = dataBuf[1];
            }
            int nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount, pBuf, NNetId);
            if (nRet == 0) { }
        }

        public override void PlcWriteDoubleWords(int startAdr, int[] value)
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
            int nRet = StandardModbusApi.H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_D, startAdr, ncount*2, pBuf, NNetId);
            if (nRet == 0) { }
        }

        public override void PlcWriteFloats(int startAdr, float[] value)
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
        }

        public override async Task PlcWriteBytesAsync(int startAdr, byte[] value)
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
        public override async Task PlcWriteWordsAsync(int startAdr, byte[] value)
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

        public override async Task PlcWriteDoubleWordsAsync(int startAdr, byte[] value)
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

        public override async Task PlcWriteFloatsAsync(int startAdr, byte[] value)
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

        private bool isbword(SoftElemType dataType)
        {
            switch (dataType)
            {
                case SoftElemType.REGI_H5U_R:
                    return true;
                case SoftElemType.REGI_H5U_D:
                    return true;
                default:
                    return false;
            }
        }
    }
}
