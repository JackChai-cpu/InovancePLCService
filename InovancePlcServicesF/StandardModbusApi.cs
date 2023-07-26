using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InovancePLCService
{
    public class StandardModbusApi
    {
        #region //标准库
        /// <summary>
        /// 初始化网络
        /// </summary>
        /// <param name="sIpAddr">PLCIP地址</param>
        /// <param name="nNetId"></param>
        /// <param name="IpPort">PLCip端口</param>
        /// <returns></returns>
        [DllImport("StandardModbusApi.dll", EntryPoint = "Init_ETH_String", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Init_ETH_String(string sIpAddr, int nNetId = 0, int IpPort = 502);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Exit_ETH", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Exit_ETH(int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);


        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Device_Block", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Device_Block(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Device_Block", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Device_Block(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);


        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);



        #endregion
    }
    public enum SoftElemType
    {
        //AM600
        ELEM_QX = 0,     //QX元件
        ELEM_MW = 1,     //MW元件
        ELEM_X = 2,		 //X元件(对应QX200~QX300)
        ELEM_Y = 3,		 //Y元件(对应QX300~QX400)

        //H3U
        REGI_H3U_Y = 0x20,       //Y元件的定义	
        REGI_H3U_X = 0x21,		//X元件的定义							
        REGI_H3U_S = 0x22,		//S元件的定义				
        REGI_H3U_M = 0x23,		//M元件的定义							
        REGI_H3U_TB = 0x24,		//T位元件的定义				
        REGI_H3U_TW = 0x25,		//T字元件的定义				
        REGI_H3U_CB = 0x26,		//C位元件的定义				
        REGI_H3U_CW = 0x27,		//C字元件的定义				
        REGI_H3U_DW = 0x28,		//D字元件的定义				
        REGI_H3U_CW2 = 0x29,	    //C双字元件的定义
        REGI_H3U_SM = 0x2a,		//SM
        REGI_H3U_SD = 0x2b,		//
        REGI_H3U_R = 0x2c,		//
        //H5u
        REGI_H5U_Y = 0x30,       //Y元件的定义	
        REGI_H5U_X = 0x31,		//X元件的定义							
        REGI_H5U_S = 0x32,		//S元件的定义				
        REGI_H5U_M = 0x33,		//M元件的定义	
        REGI_H5U_B = 0x34,       //B元件的定义
        REGI_H5U_D = 0x35,       //D字元件的定义
        REGI_H5U_R = 0x36,       //R字元件的定义

    }

    /// <summary>
    /// CPU类型
    /// </summary>
    public enum CpuType
    {
        H3u = 0,
        H5u = 1,
        A600,
    }

    public enum Errcode
    {
        ER_READ_WRITE_FAIL = 0,   //读写失败
        ER_READ_WRITE_SUCCEED = 1,  //读写成功
        ER_NOT_CONNECT = 2,  //未连接
        ER_ELEM_TYPE_WRONG = 3,  //元件类型错误
        ER_ELEM_ADDR_OVER = 4,  //元件地址溢出
        ER_ELEM_COUNT_OVER = 5,  //元件个数超限
        ER_COMM_EXCEPT = 6,  //通讯异常
    }

}
