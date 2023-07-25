using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovancePlcServices
{
    internal class H5UPLC : InovancePlc
    {
        public H5UPLC(string IP, int Port, int Netid) : base(IP, Port, Netid)
        {
        }


        public override object? PlcReadBytes(int startByteAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            //if (isbword(dataType))
            //{
            var beginresult = new byte[count + 1];
            int nRet = StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, count, beginresult, NNetId);
            if (nRet == (int)Errcode.ER_READ_WRITE_FAIL) { }
            else if (nRet == 1) { }
            else if (nRet == 1) { }
            var result = new byte[count];
            Array.Copy(beginresult, result, count);
            return result;
        }

        public override object? PlcReadWords(int startByteAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            var result = new byte[count * 2];
            StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, count, result, NNetId);
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

        public override async Task<object?> PlcReadBytesAsync(int startByteAdr, int count)
        {
            if (!IsConnect)
            {
                PlcOpen();
            }
            var beginresult = new byte[count + 1];

            // 模拟耗时操作
            int nRet = await new Task<int>(() => StandardModbusApi.H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_D, startByteAdr, count, beginresult, NNetId));

            if (nRet == (int)Errcode.ER_READ_WRITE_FAIL) { }
            else if (nRet == 1) { }
            else if (nRet == 1) { }
            var result = new byte[count];
            Array.Copy(beginresult, result, count);
            return result;
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
