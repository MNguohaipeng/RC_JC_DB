using System;
using System.Collections.Generic;
using System.Text;

namespace Entity //ÐÞ¸ÄÃû×Ö¿Õ¼ä
{
	public class SizeCheck
	{
		private int iD;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		private string size_code;
		public string Size_code
		{
			get { return size_code; }
			set { size_code = value; }
		}
	
		private string size_table_name;
		public string Size_table_name
		{
			get { return size_table_name; }
			set { size_table_name = value; }
		}
	
		private int start;
		public int Start
		{
			get { return start; }
			set { start = value; }
		}
	
		private int operator_id;
		public int Operator_id
		{
			get { return operator_id; }
			set { operator_id = value; }
		}
	
		private DateTime create_date;
		public DateTime Create_date
		{
			get { return create_date; }
			set { create_date = value; }
		}
	}
}