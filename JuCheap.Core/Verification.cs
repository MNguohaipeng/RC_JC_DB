using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace JuCheap.Core
{
	public static  class Verification
	{
		//验证西服上衣格式(男)
		public static bool Verification_XF_SY_NAN(DataTable table) {
			using (var db=SugarDao.GetInstance())
				try
				{
			 
					var vc = db.GetString(@"  select  D_Value  from Dictionaries where d_Key='ExcelColumnsCount_XF_SY_NAN'   ");
 
					int count = 0;
					if (!int.TryParse(vc, out count))
					{
						throw new Exception("系统出错，没有配置excel验证条件。");
					}
					if (table.Columns.Count != count) {
						throw new Exception("Excel格式出错，请检查文件");
					}

					return true;

				}
				catch (Exception ex)
				{

					throw;
				}
 
		}


		//验证西服上衣格式(女)
		public static bool Verification_XF_SY_NU(DataTable table)
		{
			using (var db = SugarDao.GetInstance())
				try
				{

					var vc = db.GetString(@"  select  D_Value  from Dictionaries where d_Key='ExcelColumnsCount_XF_SY_NU'   ");

					int count = 0;

					if (!int.TryParse(vc, out count))
					{
						throw new Exception("系统出错，没有配置excel验证条件。");
					}

					if (table.Columns.Count != count)
					{
						throw new Exception("Excel格式出错，请检查文件");
					}

					return true;

				}
				catch (Exception ex)
				{

					throw;
				}

		}
	}
}
