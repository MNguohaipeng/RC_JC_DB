using System;
using System.Collections.Generic;
using System.Text;

namespace Entity //ÐÞ¸ÄÃû×Ö¿Õ¼ä
{
	public class User
	{
		private int iD;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		private string user_name;
		public string User_name
		{
			get { return user_name; }
			set { user_name = value; }
		}
	
		private string password;
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
	}
}