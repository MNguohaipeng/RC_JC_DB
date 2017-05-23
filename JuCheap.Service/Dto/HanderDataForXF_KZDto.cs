using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service.Dto
{
   public class HanderDataForXF_KZDto:BaseDto
    {
        public string OrderCode { get; set; }//订单编号

        public string Name { get; set; }

        public string RtnQCode { get; set; }//归码前尺码

        public decimal Height { get; set; }

        public decimal waistWidth { get; set; }//腰围

        public decimal SZ_Hipline { get; set; }//双褶成品臀围

        public decimal DZ_Hipline { get; set; }//单褶成品臀围

        public int Number { get; set; }

        public string Note { get; set; }
 
        public int Index { get; set; }//排序号

        public string GDH { get; set; }

    }
}
