using JuCheap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Entity
{
    public class XF_SY_NAN_ChiMaEntity:BaseEntity
    {
        public decimal Height { get; set; }//身高

        public string FrontLength { get; set; }//前身长
 
        public string NetBust { get; set; }//净胸围

        public decimal FinishedBust { get; set; }//成品胸围
 
        public decimal InWaist { get; set; }//中腰

        public decimal FinishedHem_NoFork { get; set; }//成品下摆（不开叉）
 
        public decimal FinishedHem_SplitEnds { get; set; }//成品下摆（开衩）

        public decimal ShoulderWidth { get; set; }//肩宽

        public string Size_Code { get; set; }//尺码表编号

        public string Sleecve_Show { get; set; }//袖长

        public int Status { get; set; }//状态

        public string RowData { get; set; }

    }
}
