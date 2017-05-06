using System;
using System.Collections.Generic;
using System.Text;

namespace Entity //ĞŞ¸ÄÃû×Ö¿Õ¼ä
{
	public class Ret_CodeTrousers
	{
		private int iD;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		private string code_size;
		public string Code_size
		{
			get { return code_size; }
			set { code_size = value; }
		}
	
		private decimal height;
		public decimal Height
		{
			get { return height; }
			set { height = value; }
		}
	
		private decimal waistline;
		public decimal Waistline
		{
			get { return waistline; }
			set { waistline = value; }
		}
	
		private decimal hipline;
		public decimal Hipline
		{
			get { return hipline; }
			set { hipline = value; }
		}
	
		private int number;
		public int Number
		{
			get { return number; }
			set { number = value; }
		}
	
		private string remarks;
		public string Remarks
		{
			get { return remarks; }
			set { remarks = value; }
		}
	}
}