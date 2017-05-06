using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JuCheap.Core
{
    public class Common
    {
        //解析Excel
        public static DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {

                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                DataSet ds = null;
                strExcel = "select * from [sheet1$]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds, "table1");
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }
        //

        //验证汉字
        public static bool IsChinses(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"[\u4e00-\u9fa5]");
            return reg1.IsMatch(str);
        }
 
        //验证是否是纯英文 并且长度只有1
        public static bool IsEnglish_Length_One(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]{1,1}$");
            return reg1.IsMatch(str);
        }

        //验证是否是数字  浮点数  英文
        public static bool IsNatural_Number(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9\.\0-9a-zA_Z]+$");
            return reg1.IsMatch(str);
        }

 		 

        //拼接sql语句
        //添加   parameter 存字段名和值  field_name不用管
        //查询   parameter 存条件字段名和值【=值 and/or】   field_name查询字段  可以为空  
        public static string splicedSQL(Dictionary<string,string> parameter,string type,string table_name,string[] field_name=null) {
            string sql = "";
            try
            {
                switch (type)
                {
                    case "insert":
                        sql += "insert into " + table_name;
                        sql+="(";
                        string values = "(";
                        foreach (KeyValuePair<String, String> kvp in parameter)
                        {
                            sql += kvp.Key+",";
                            values += kvp.Value+",";
                        }
                        values.TrimEnd(',');
                        sql.TrimEnd(',');
                        values += ")";
                        sql += ")";
                        sql += values;
                        break;
                    case "select":
                        sql += " SELECT ";
                        if (field_name != null)
                        {
                            for (int i = 0; i < field_name.Length; i++)
                            {

                                sql += field_name + ",";
                            }
                            sql.TrimEnd(',');
                        }
                        else {
                            sql += " * ";
                        }

                        sql += " FROM ";
                        sql += table_name;
                        sql += " WHERE ";
                        foreach (KeyValuePair<String, String> kvp in parameter)
                        {
                            sql += kvp.Key + kvp.Value;
               
                        }

                        break;
                }


                return sql;
            }
            catch (Exception)
            {
                throw;
            }


        }

        /// <summary>
        /// 上传文件 
        /// </summary>
        /// <param name="FormFile">文件</param>
        /// <param name="Url">路径</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="_FileExtension">文件允许类型</param>
        /// <returns></returns>
        public static bool UpLoadFile(HttpPostedFileBase formFile, string fileName, string dict, out string uploadPath, out string errMsg)
        {

            try
            {
                HttpPostedFileBase postedFile = formFile;
                if (postedFile == null)
                {
                    errMsg = "文件不能为空";
                    uploadPath = "";
                    return false;
                }
                #region 拼接文件路径
                string uploadDir = dict + "/";
                uploadPath = uploadDir + fileName;//拼接文件的完整路径
                #endregion
                #region 判断路径是否存在
                if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(uploadDir)) == false)
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(uploadDir));
                }
                #endregion
                #region Save
                postedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(uploadPath));
                #endregion
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                uploadPath = "";
                return false;
            }
        }
    }
}
