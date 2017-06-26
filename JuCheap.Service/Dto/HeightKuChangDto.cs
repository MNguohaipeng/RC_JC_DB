using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service.Dto
{
  public  class HeightKuChangDto:BaseDto
    {

        public double Height { get; set; }//身高

        public double KuChang { get; set; }//裤长

        public string Size_Code { get; set; }//尺码表ID

    }
}
