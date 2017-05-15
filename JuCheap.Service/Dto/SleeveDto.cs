using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service.Dto
{
    public class SleeveDto : BaseDto
    {

        public string Code { get; set; }

        public decimal Length { get; set; }

        public int FK_CoatSize_ID { get; set; }

    }
}
