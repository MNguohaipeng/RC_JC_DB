using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service.Dto
{
    public class XF_KZ_CodeSizeDto:BaseDto
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

        public decimal Height { get; set; }

        public string LongPants { get; set; }

        public string NetWaist { get; set; }

        public string Size_Code { get; set; }

        public int Status { get; set; }

        public string RowData { get; set; }
    }
}
