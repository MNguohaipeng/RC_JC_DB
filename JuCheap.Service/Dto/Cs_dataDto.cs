using JuCheap.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuCheap.Service.Dto //ÐÞ¸ÄÃû×Ö¿Õ¼ä
{
	public class Cs_dataDto:BaseDto
	{
        public int ID { get; set; }

        public string Orderid { get; set; }

        public int Option { get; set; }

        public string Name { get; set; }

        public string ReCodeSize { get; set; }

        public int Number { get; set; }

        public string Note { get; set; }
    }
}