using JuCheap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Entity
{
  public class HeightKuChangEntity : BaseEntity
    {

        public double Height { get; set; }//身高

        public double KuChang { get; set; }//裤长

        public string Size_Code { get; set; }//尺码表

    }
}
