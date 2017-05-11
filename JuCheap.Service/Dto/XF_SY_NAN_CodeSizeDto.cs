﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service.Dto
{
    public class XF_SY_NAN_CodeSizeDto: BaseDto
    {
 

        public decimal Height { get; set; }

        public string FrontLength { get; set; }

        public string NetBust { get; set; }

        public decimal FinishedBust { get; set; }

        public decimal InWaist { get; set; }

        public decimal FinishedHem_NoFork { get; set; }

        public decimal FinishedHem_SplitEnds { get; set; }

        public decimal ShoulderWidth { get; set; }

        public string Size_Code { get; set; }
 
        public int Status { get; set; }


       


    }
}
