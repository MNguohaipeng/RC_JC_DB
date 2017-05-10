using System;
using System.Collections.Generic;
using System.Text;

namespace JuCheap.Service //ÐÞ¸ÄÃû×Ö¿Õ¼ä
{
	public class Cs_data
	{
		private int iD;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		private string orderid;
		public string Orderid
		{
			get { return orderid; }
			set { orderid = value; }
		}
	
		private int option;
		public int Option
		{
			get { return option; }
			set { option = value; }
		}
	
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	
		private string reCodeSize;
		public string ReCodeSize
		{
			get { return reCodeSize; }
			set { reCodeSize = value; }
		}
	
		private int number;
		public int Number
		{
			get { return number; }
			set { number = value; }
		}
	
		private string note;
		public string Note
		{
			get { return note; }
			set { note = value; }
		}
	}
}