using JuCheap.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JuCheap.Entity //ÐÞ¸ÄÃû×Ö¿Õ¼ä
{
    public class XF_KZ_CodeSizeEntity:BaseEntity
    {

        public string Code { get; set; }

        public decimal DZ_HipLength_CP { get; set; }

        public decimal SZ_HipLength_CP { get; set; }

        public decimal Crosspiece { get; set; }

        public decimal LegWidth_UnderTheWaves { get; set; }

        public decimal FrontRise_EvenWaist { get; set; }

        public decimal AfterTheWaves_EvenWaist { get; set; }

        public string NetHip { get; set; }

        public string CP_WaistWidth { get; set; }

        public string Height { get; set; }

        public string LongPants { get; set; }

        public string NetWaist { get; set; }

        public string Size_Code { get; set; }

        public int Status { get; set; }

        public string RowData { get; set; }

    }
}