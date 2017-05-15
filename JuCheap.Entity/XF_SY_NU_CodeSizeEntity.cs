using JuCheap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuCheap.Entity //ÐÞ¸ÄÃû×Ö¿Õ¼ä
{
    public class XF_SY_NU_CodeSizeEntity :BaseEntity
    {

        public decimal Height { get; set; }

        public string FrontLength { get; set; }

        public string NetBust { get; set; }

        public decimal FinishedBust { get; set; }

        public decimal InWaist { get; set; }

        public decimal FinishedHem_NoFork { get; set; }

        public decimal SleeveWidth { get; set; }

        public decimal ShoulderWidth { get; set; }

        public string Sleecve_Show { get; set; }

        public string Size_Code { get; set; }

        public int Status { get; set; }

        public string RowData { get; set; }

    }
}