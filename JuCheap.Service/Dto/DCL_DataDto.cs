using JuCheap.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuCheap.Service.Dto //�޸����ֿռ�
{
	public class DCL_DataDto:BaseDto
	{
        public int ID { get; set; }

        public int Index { get; set; }

        public string Orderid { get; set; }

        public int Option { get; set; }

        public string Name { get; set; }

        public string ReCodeSize { get; set; }

        public int Number { get; set; }

        public string Note { get; set; }

        public string GDH { get; set; }

        public string Gender { get; set; }
    }
}