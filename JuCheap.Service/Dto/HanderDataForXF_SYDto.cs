using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service.Dto
{
   public class HanderDataForXF_SYDto:BaseDto
    {

        public string OrderCode { get; set; }//订单编号

        public int option { get; set; }//项次

        public string Name { get; set; }//姓名

        public string RtnQCode { get; set; }//归码前尺码

        public string RtnHCode { get; set; }//归码后尺码

        public decimal Height { get; set; }//高度

        public int Number { get; set; }//数量

        public decimal Yichang { get; set; }//衣长

        public decimal Bust { get; set; }//胸围

        public decimal Sleeve { get; set; }//袖长

        public string Note { get; set; }//备注

    }
}
