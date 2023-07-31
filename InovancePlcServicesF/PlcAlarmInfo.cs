﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InovancePLCService
{
    public class PlcAlarmInfo
    {
        public static Dictionary<int,string> AlarmInfo=new Dictionary<int, string>() 
        {
            { 1, "左超差"},
            { 2,"右超差" },
            { 3,"前超差" },
            { 4,"后超差" },
            { 5,"高超差" },
            { 6,"取货时载货台有货" },
            { 7,"放货时载货台无货" },
            { 8,"取货时左货格无货" },
            { 9,"取货时右货格无货" },
            { 10,"放货时左货格有货" },
            { 11,"放货时右货格有货" },
            { 12,"取左二货格无货" },
            { 13,"取右二货格无货" },
            { 14,"放左二货格有货" },
            { 15,"放右二货格有货" },
            { 16,"取左货格货时前面货格有货 " },
            { 17,"取右货格货时前面货格有货 " },
            { 18,"左取货失败" },
            { 19,"右取货失败" },
            { 20,"左放货失败" },
            { 21,"右放货失败" },
            { 22,"货叉断路器跳闸" },
            { 23,"货叉变频器报警" },
            { 24,"货叉编码器故障" },
            { 25,"货叉伸叉故障" },
            { 26,"层运行货叉不在中位" },
            { 27,"列运行货叉不在中位" },
            { 28,"货叉中位开关故障" },
            { 29,"货叉变频器通讯故障" },
            { 30,"货叉编码器通讯故障" },
            { 31,"伸叉超出左极限" },
            { 32,"伸叉超出右极限" },
            { 33,"入左货格货时前面货格有货" },
            { 34,"入右货格货时前面货格有货" },
            { 35,"货叉右侧回中停止位置错误" },
            { 36,"货叉左侧回中停止位置错误" },
            { 37,"货叉左侧极限停止位置错误" },
            { 38,"货叉右侧极限停止位置错误" },
            { 39,"面板急停" },
            { 40,"控制软件远程急停" },
            { 41,"超重保护" },
            { 42,"松绳保护" },
            { 43,"限速保护" },
            { 44,"层停止位置错误" },
            { 45,"列停止位置错误" },
            { 46,"接收自动任务错误" },
            { 47,"接收列超" },
            { 48,"接收层超" },
            { 49,"到达行走前进极限" },
            { 50,"到达行走后退极限" },
            { 51,"到达提升上极限" },
            { 52,"到达提升下极限" },
            { 53,"行走运行超时" },
            { 54,"提升运行超时" },
            { 55,"行走断路器跳闸" },
            { 56,"提升断路器跳闸" },
            { 57,"行走变频器故障" },
            { 58,"提升变频器故障" },
            { 59,"行走测距/条码故障 " },
            { 60,"提升测距/条码故障 " },
            { 61,"行走测距/条码被挡 " },
            { 62,"提升测距/条码被挡 " },
            { 63,"提升变频器通讯故障" },
            { 64,"行走变频器通讯故障" },
            { 65,"行走激光测距通讯故障" },
            { 66,"提升激光测距通讯故障" },
            { 67,"提升超出正向软限位" },
            { 68,"提升超出反向软限位" },
            { 69,"行走超出正向软限位" },
            { 70,"行走超出反向软限位" },
            { 71,"接收货叉位置错误" },
            { 72,"起始站货格被禁用" },
            { 73,"目的站货格被禁用" },
            { 74,"主控软件心跳断开" },
            { 75,"外围PLC心跳断开" },
            { 76,"探货传感器通讯网关故障 " },
            { 77,"探货传感器1故障" },
            { 78,"探货传感器2故障" },
            { 79,"探货传感器3故障" },
            { 80,"探货传感器4故障" },
            { 81,"执行提升轴索引计算出错 " },
            { 82,"执行水平轴索引计算出错 " },
            { 83,"执行货叉索引计算出错 " },
            { 84,"货叉1运行超时" },
            { 85,"货叉2运行超时" },
            { 86,"货叉3运行超时" },
            { 87,"货叉4运行超时" },
            { 88,"货叉5运行超时" },
            { 89,"货叉6运行超时" },
            { 90,"货叉7运行超时" },
            { 91,"货叉8运行超时" },
            { 92,"外围PLC给定急停" },
            { 93,"货叉轴使能异常 " },
            { 94,"提升轴使能异常 " },
            { 95,"行走轴使能异常 " },
            { 96,"货叉轴被锁定 " },
            { 97,"提升轴被锁定 " },
            { 98,"行走轴被锁定 " },
            { 99,"接收手动任务错误 " },
            { 101,"货叉1超出正向软限位" },
            { 102,"货叉1超出反向软限位" },
            { 103,"货叉2超出正向软限位" },
            { 104,"货叉2超出反向软限位" },
            { 105,"货叉3超出正向软限位" },
            { 106,"货叉3超出反向软限位" },
            { 107,"货叉4超出正向软限位" },
            { 108,"货叉4超出反向软限位" },
            { 109,"货叉5超出正向软限位" },
            { 110,"货叉5超出反向软限位" },
            { 111,"货叉6超出正向软限位" },
            { 112,"货叉6超出反向软限位" },
            { 113,"货叉7超出正向软限位" },
            { 114,"货叉7超出反向软限位" },
            { 115,"货叉8超出正向软限位" },
            { 116,"货叉8超出反向软限位" },
            {117 ,"货叉1自动回等待位超时" },
            { 118,"货叉2自动回等待位超时" },
            { 119,"货叉3自动回等待位超时" },
            { 120,"货叉4自动回等待位超时" },
            { 121,"货叉5自动回等待位超时" },
            { 122,"货叉6自动回等待位超时" },
            { 123,"货叉7自动回等待位超时" },
            { 124,"货叉8自动回等待位超时" },
            { 125,"提升结束位置超过当前允许高度差 " },
            { 126,"货叉1夹抱超时" },
            { 127,"货叉2夹抱超时" },
            { 128,"货叉3夹抱超时" },
            { 129,"货叉4夹抱超时" },
            { 130,"货叉5夹抱超时" },
            { 131,"货叉6夹抱超时" },
            { 132,"货叉7夹抱超时" },
            { 133,"货叉8夹抱超时" },

            { 200,"人工入料口顶升气缸缩回超时" },
            { 201,"人工入料口顶升气缸伸出超时" },
            { 202,"人工入料口顶升气缸磁开紊乱（伸出缩回磁开都有信号）" },
            { 203,"人工入料口顶升气缸磁开紊乱（伸出缩回磁开都无信号）" },
            { 204,"人工出料口顶升气缸缩回超时" },
            { 205,"人工出料口顶升气缸伸出超时" },
            { 206,"人工出料口顶升气缸磁开紊乱（伸出缩回磁开都有信号）" },
            { 207,"人工出料口顶升气缸磁开紊乱（伸出缩回磁开都无信号）" },
            { 208,"人工异常口顶升气缸缩回超时" },
            { 209,"人工异常口顶升气缸伸出超时" },
            { 210,"人工异常口顶升气缸磁开紊乱（伸出缩回磁开都有信号）" },
            { 211,"人工异常口顶升气缸磁开紊乱（伸出缩回磁开都无信号）" },
            { 212,"人工入料口超高" },
            { 213,"人工出料口超高" },
            { 214,"人工异常口超高" },
            { 215,"人工入料口卡料" },
            { 216,"人工出料口卡料" },
            { 217,"人工异常口卡料" },
            { 218,"自动入料口顶升气缸缩回超时" },
            { 219,"自动入料口顶升气缸伸出超时" },
            { 220,"自动入料口顶升气缸磁开紊乱（伸出缩回磁开都有信号）" },
            { 221,"自动入料口顶升气缸磁开紊乱（伸出缩回磁开都无信号）" },
            { 222,"自动出料口顶升气缸缩回超时" },
            { 223,"自动出料口顶升气缸伸出超时" },
            { 224,"自动出料口顶升气缸磁开紊乱（伸出缩回磁开都有信号）" },
            { 225,"自动出料口顶升气缸磁开紊乱（伸出缩回磁开都无信号）" },
            { 226,"自动入料口超高" },
            { 227,"自动出料口超高" },
            { 228,"自动入料口卡料" },
            { 229,"自动出料口卡料" },


        };
    }
}
