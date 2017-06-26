
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugarRepository;
namespace JuCheap.Core
{
    public class SqlSugerHelper
    {
        public static string SqlConnString = @"server=DESKTOP-OV0I35V\SQLEXPRESS;uid=sa;pwd=123456;database=GUI_MA_DB";

        private static string MySqlConnString ="server=localhost;Database=SqlSugarTest;Uid=root;Pwd=root";

        private static string PlSqlConnString ="Data Source=172.16.173.131/orcl.kutesmart.cn;User ID=system;Password=123456;";

        private static string SqliteSqlConnString =@"DataSource=F:\SugarForOne\OrmTest\OrmTest\Database\demo.sqlite";

        static void Main(string[] args)
        {

            using (MyRepository db = new MyRepository())
            {
                //当前DB是SqlConnection1
                var list = db.Database.Queryable<Student>().ToList(); //使用和其它一模一样只是多了个Database

            }
        }

        public class MyRepository : DbRepository
        {
            //public ConnectionConfig SqlConnection1 =
            //new ConnectionConfig() { ConnectionString = SqlConnString, DbType = DbType.SqlServer };

            public ConnectionConfig Sqlite3 = new ConnectionConfig() { ConnectionString = PlSqlConnString, DbType = DbType.Oracle };

        }
        public class MySqlServer : DbRepository
        {
            //public ConnectionConfig SqlConnection1 =
            //new ConnectionConfig() { ConnectionString = SqlConnString, DbType = DbType.SqlServer };

            public ConnectionConfig Sqlite3 = new ConnectionConfig() { ConnectionString = SqlConnString, DbType = DbType.SqlServer };

        }
        public class Student
        {
            public int id { get; set; }
            public string name { get; set; }
        }


    }

}
