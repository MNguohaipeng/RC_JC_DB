using JuCheap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuCheap.Entity //修改名字空间
{
    public class XF_SY_NU_CodeSizeEntity :BaseEntity
    {

        public decimal Height { get; set; }//身高

        public string FrontLength { get; set; }//前身长

        public string NetBust { get; set; }//净胸围

        public decimal FinishedBust { get; set; }//成品胸围

        public decimal InWaist { get; set; }//中腰

        public decimal FinishedHem_NoFork { get; set; }//下摆

        public decimal SleeveWidth { get; set; }//袖肥

        public decimal ShoulderWidth { get; set; }//肩宽

        public string Sleecve_Show { get; set; }//袖长

        public string Size_Code { get; set; }//尺码编号

        public int Status { get; set; }//状态

        public string RowData { get; set; }

    }
}