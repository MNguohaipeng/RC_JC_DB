using JuCheap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Entity
{
   public class HanderDataForXF_SYEntity:BaseEntity
    {

        public string OrderCode { get; set; }

        public int option { get; set; }//项次

        public string Name { get; set; }

        public string RtnQCode { get; set; }//归码前尺码

        public string RtnHCode { get; set; }//归码前尺码

        public decimal Height { get; set; }

        public int Number { get; set; }

        public decimal Yichang { get; set; }//衣长

        public decimal Bust { get; set; }//胸围

        public decimal Sleeve { get; set; }//袖长

        public string Note { get; set; }

    }
}
