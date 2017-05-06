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
            {
                string reval = @"server=DESKTOP-OV0I35V\SQLEXPRESS;uid=sa;pwd=123456;database=DJ_JuCheap"; //这里可以动态根据cookies或session实现多库切换
                return reval;
            }
        }
        public static SqlSugarClient GetInstance()
        {
            var db = new SqlSugarClient(ConnectionString);
            db.IsEnableLogEvent = true;//启用日志事件
            db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par+"\r\n"); };
            return db;
        }
    }
}