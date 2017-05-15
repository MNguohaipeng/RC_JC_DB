using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service.Dto
{
   public class HanderDataForXF_KZDto:BaseDto
    {
        public string Name { get; set; }

        public string RtnQCode { get; set; }//归码前尺码

        public decimal Height { get; set; }

        public decimal waistWidth { get; set; }//腰围

        public decimal Hipline { get; set; }//臀围

        public int Number { get; set; }

        public string Note { get; set; }

    }
}
