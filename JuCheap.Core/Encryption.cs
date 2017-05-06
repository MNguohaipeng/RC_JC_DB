using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace JuCheap.Core
{
	public class Encryption
	{

		/// <summary>
		/// 32位加密
		/// </summary>
		/// <param name="s"></param>
		/// <param name="_input_charset"></param>
		/// <returns></returns>
		public static string GetMD5_32(string s, string _input_charset)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
			StringBuilder sb = new StringBuilder(32);
			for (int i = 0; i < t.Length; i++)
			{
				sb.Append(t[i].ToString("x").PadLeft(2, '0'));
			}
			return sb.ToString();
		}

		/// <summary>
		/// 16位加密 
		/// </summary>
		/// <param name="ConvertString"></param>
		/// <returns></returns>
		public static string GetMd5_16(string ConvertString)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
			t2 = t2.Replace("-", "");
			return t2;
		}


		//RSA 加密
		public static string RSAEncrypt(string normaltxt)
		{
			var bytes = Encoding.Default.GetBytes(normaltxt);
			var encryptBytes = new RSACryptoServiceProvider(new CspParameters()).Encrypt(bytes, false);
			return Convert.ToBase64String(encryptBytes);
		}
		//RSA解密
		public static string RSADecrypt(string securityTxt)
		{
			try//必须使用Try catch,不然输入的字符串不是净荷明文程序就Gameover了  
			{
				var bytes = Convert.FromBase64String(securityTxt);
				var DecryptBytes = new RSACryptoServiceProvider(new CspParameters()).Decrypt(bytes, false);
				return Encoding.Default.GetString(DecryptBytes);
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}


		public Encryption()//密钥向量,8位就好了例如new byte[]{0x05,0x04,0x06,0x05,0x02,0x06,0x05,0x04}  
		{
			this.keyvi = new byte[] { 0x05, 0x04, 0x06, 0x05, 0x02, 0x06, 0x05, 0x04 };
		}
		private byte[] keyvi;
		public string DesEncrypt(string normalTxt, string EncryptKey)
		{
			var bytes = Encoding.Default.GetBytes(normalTxt);
			var key = Encoding.UTF8.GetBytes(EncryptKey.PadLeft(8, '0').Substring(0, 8));
			using (MemoryStream ms = new MemoryStream())
			{
				var encry = new DESCryptoServiceProvider();
				CryptoStream cs = new CryptoStream(ms, encry.CreateEncryptor(key, keyvi), CryptoStreamMode.Write);
				cs.Write(bytes, 0, bytes.Length);
				cs.FlushFinalBlock();
				return Convert.ToBase64String(ms.ToArray());
			}
		}
		public string DesDecrypt(string securityTxt, string EncryptKey)//解密  
		{
			try
			{
				var bytes = Convert.FromBase64String(securityTxt);
				var key = Encoding.UTF8.GetBytes(EncryptKey.PadLeft(8, '0').Substring(0, 8));
				using (MemoryStream ms = new MemoryStream())
				{
					var descrypt = new DESCryptoServiceProvider();
					CryptoStream cs = new CryptoStream(ms, descrypt.CreateDecryptor(key, keyvi), CryptoStreamMode.Write);
					cs.Write(bytes, 0, bytes.Length);
					cs.FlushFinalBlock();
					return Encoding.UTF8.GetString(ms.ToArray());
				}

			}
			catch (Exception)
			{
				return string.Empty;
			}
		}



	}
}
