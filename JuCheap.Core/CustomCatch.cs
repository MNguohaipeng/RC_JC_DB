using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Core
{
  public  class CustomCatch: ApplicationException
    {
        public int Level { get; set; }//异常级别

        public string ExceptionString { get; set; }//异常内容

        public CustomCatch(int level,string exceptionString) {

            Level = level;

            ExceptionString = exceptionString;

        }

  

    }
}
