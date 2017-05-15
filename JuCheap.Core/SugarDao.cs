using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace JuCheap.Core
{
    /// <summary>
    /// SqlSugar
    /// </summary>
    public class SugarDao
    {
        //禁止实例化
        private SugarDao()
        {

        }
        public static string ConnectionString
        {
            get
            {//;
                string reval = @"Data Source=172.16.172.140/orcl;  User ID=system;Password=123456"; //这里可以动态根据cookies或session实现多库切换
                return reval;
            }
        }
      public static object GetInstance()
        {
 
        //    var db = new SqlSugarClient(ConnectionString);
        //    db.IsEnableLogEvent = true;//启用日志事件
        //    db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par+"\r\n"); };
           return null;
          }
    }
}