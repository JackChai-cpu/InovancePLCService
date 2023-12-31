﻿using System;
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

        /// <summary>
        /// 退出网络连接
        /// </summary>
        /// <param name="nNetId"></param>
        /// <returns></returns>
        [DllImport("StandardModbusApi.dll", EntryPoint = "Exit_ETH", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Exit_ETH(int nNetId = 0);

        ///// <summary>
        ///// 检查网络是否连接
        ///// </summary>
        ///// <param name="nNetId"></param>
        ///// <returns></returns>
        //[DllImport("StandardModbusApi.dll", EntryPoint = "IS_ETH_Online", CallingConvention = CallingConvention.Cdecl)]
        //public static extern bool IS_ETH_Online(int nNetId = 0);


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
        ErrReadFail = 0,   //读取失败
        //ER_READ_WRITE_SUCCEED = 1,  //读写成功
        ErrNotConnect = 2,  //未连接
        ErrElemTypeWrong=3, //元件类型错误
        ErrElemAddrOver=4,//元件地址溢出
        ErrElemCountOver=5, //元件个数超限
        ErrCommExcept=6, //通讯异常

        ErrWriteFail=7,//写入失败

        ErrAdsWrong=8,//地址错误
    }

    /// <summary>
    /// 工作方式
    /// </summary>
    public enum WorkingMode
    {
        /// <summary>
        /// 自动
        /// </summary>
        Auto=2,
        /// <summary>
        /// 手动
        /// </summary>
        Manual=1,
    }


    /// <summary>
    /// 取料区工件类型
    /// </summary>
    public enum FeedingWorkpieceType
    {
        Big=1,
        Mid=2,
        Small=3,
    }

    /// <summary>
    /// 取料区工作口分配
    /// </summary>
    public enum FeedingWorkportAssignment
    {
        /// <summary>
        /// 入库料口
        /// </summary>
        Feedport=1,
        /// <summary>
        /// 出库料口
        /// </summary>
        Dischargeport=2,
        /// <summary>
        /// 异常料口
        /// </summary>
        AbnormalPort=3,
    }

    public enum StockerWorkingMode
    {
        /// <summary>
        /// 取料模式
        /// </summary>
        FeedingMode=1,
        /// <summary>
        /// 放料模式
        /// </summary>
        BlowingMode=2,
        /// <summary>
        /// 回原点
        /// </summary>
        Home=3,
        /// <summary>
        /// 急停
        /// </summary>
        Stop=4,
        /// <summary>
        /// 取消急停
        /// </summary>
        CancelStop=5,
        /// <summary>
        /// 取消任务
        /// </summary>
        CancelQuest=6,
        /// <summary>
        /// 复位
        /// </summary>
        Reset=7,
        /// <summary>
        /// 移动模式
        /// </summary>
        MoveMode=8,
    }
}
