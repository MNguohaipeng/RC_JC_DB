using JuCheap.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JuCheap.Entity //修改名字空间
{
    public class XF_KZ_CodeSizeEntity:BaseEntity
    {

        public string Code { get; set; }//代号

        public decimal DZ_HipLength_CP { get; set; }//单褶臀围

        public decimal SZ_HipLength_CP { get; set; }//双褶臀围

        public decimal Crosspiece { get; set; }//横档

        public decimal LegWidth_UnderTheWaves { get; set; }//腿肥    浪下10CM

        public decimal FrontRise_EvenWaist { get; set; }//前浪连腰

        public decimal AfterTheWaves_EvenWaist { get; set; }//后浪连腰

        public string NetHip { get; set; }//净臀围

        public string CP_WaistWidth { get; set; }//成品腰围

        public string Height { get; set; }//身高

        public string LongPants { get; set; }//裤长

        public string NetWaist { get; set; }//净腰围

        public string Size_Code { get; set; }//尺码编号

        public int Status { get; set; }//状态

        public string RowData { get; set; }//拼接单行数据

    }
}