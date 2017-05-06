using Entity;
using JuCheap.Core;
using JuCheap.Service;
using JuCheap.Service.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service
{
	public static class import
	{

		//上传西服上衣尺码表
		public static bool Import_Excel_jacket(DataTable table,string size_code, string gender, out string errmsg)
		{

			using (var db = SugarDao.GetInstance())
				try
				{

					db.CommandTimeOut = 3000;//设置超时时间

					List<CoatSize> list = new List<CoatSize>();

					db.BeginTran();

					for (int i = 0; i < table.Rows.Count; i++)
					{
						#region 处理不符合要求得数据

						DataRow row = table.Rows[i];

						row.BeginEdit();

						for (int a = 0; a < table.Columns.Count; a++)
						{

							if (row[a].ToString().IndexOf("....") > 0)
							{
								row[a] = row[a].ToString().Replace("....", ".");
							}
							if (row[a].ToString().IndexOf("...") > 0)
							{
								row[a] = row[a].ToString().Replace("...", ".");
							}
							if (row[a].ToString().IndexOf("..") > 0)
							{
								row[a] = row[a].ToString().Replace("..", ".");
							}

						}

						row.EndEdit();

						#endregion
						if (!string.IsNullOrEmpty(gender))
						{

							object ruid;
							if (gender == "男")
							{

								XF_SY_NAN_CodeSize cs = new XF_SY_NAN_CodeSize();

								cs.Height = Convert.ToDecimal(table.Rows[i]["Height"]);

								cs.FrontLength = table.Rows[i]["FrontLength"] + "";

								cs.NetBust = table.Rows[i]["NetBust"] + "";

								cs.FinishedBust = Convert.ToDecimal(table.Rows[i]["FinishedBust"]);

								cs.InWaist = Convert.ToDecimal(table.Rows[i]["InWaist"]);

								cs.FinishedHem_NoFork = Convert.ToDecimal(table.Rows[i]["FinishedHem_NoFork"]);

								cs.FinishedHem_SplitEnds = Convert.ToDecimal(table.Rows[i]["FinishedHem_SplitEnds"]);

								cs.ShoulderWidth = Convert.ToDecimal(table.Rows[i]["ShoulderWidth"]);

								cs.Size_Code = size_code;

								cs.Height = Convert.ToDecimal(table.Rows[i]["Height"]);

                                cs.Sleecve_Show = table.Rows[i]["FK_Sleeve_ID"] + "";

                                cs.CreateDateTime = DateTime.Now;

                                cs.IsDeleted = false;

                                cs.Status = 1;

								ruid = db.Insert(cs, true);

							}
							else
							{


								XF_SY_NU_CodeSize cs = new XF_SY_NU_CodeSize();

								cs.Height = Convert.ToDecimal(table.Rows[i]["Height"]);

								cs.FrontLength = table.Rows[i]["FrontLength"] + "";

								cs.NetBust = table.Rows[i]["NetBust"] + "";

								cs.FinishedBust = Convert.ToDecimal(table.Rows[i]["FinishedBust"]);

								cs.InWaist = Convert.ToDecimal(table.Rows[i]["InWaist"]);

								cs.FinishedHem_NoFork = Convert.ToDecimal(table.Rows[i]["FinishedHem_NoFork"]);

								cs.SleeveWidth = Convert.ToDecimal(table.Rows[i]["SleeveWidth"]);

								cs.ShoulderWidth = Convert.ToDecimal(table.Rows[i]["ShoulderWidth"]);

								cs.Size_Code = size_code;
 
                                cs.Sleecve_Show = table.Rows[i]["FK_Sleeve_ID"] + "";

                                cs.CreateDateTime = DateTime.Now;

                                cs.IsDeleted = false;

                                cs.Status = 1;

                                ruid = db.Insert(cs, true);
							}


							string req = table.Rows[i]["FK_Sleeve_ID"] + "";

							if (req.IndexOf("      ") > 0)
							{
								req = req.Replace("      ", "^");
							}

							if (req.IndexOf("     ") > 0)
							{
								req = req.Replace("     ", "^");
							}

							if (req.IndexOf("    ") > 0)
							{
								req = req.Replace("    ", "^");
							}

							if (req.IndexOf("   ") > 0)
							{
								req = req.Replace("   ", "^");
							}

							if (req.IndexOf("  ") > 0)
							{
								req = req.Replace("  ", "^");
							}

							req = req.Replace(" ", "^");

							req = req.Replace(":", ";");

							string[] sleeve_arrey = req.Split('^');

							Sleeve see = new Sleeve();
							see.FK_CoatSize_ID = Convert.ToInt32(ruid);
							see.Code = sleeve_arrey[0].Split(';')[0];
							see.Length = Convert.ToDecimal(sleeve_arrey[0].Split(';')[1]);
							db.Insert(see);
						}

					}

					db.CommitTran();
					errmsg = "";
					return true;

				}
				catch (Exception ex)
				{

					db.RollbackTran();//回滚事务
					errmsg = ex.Message;
					return false;

				}
		}



		//上传西服裤子尺码表
		public static bool Import_Excel_trousrs(DataTable table, string Size_Code, out string errmsg)
		{

			using (var db = SugarDao.GetInstance())
				try
				{
					List<TrousersSize> tszie_list = new List<TrousersSize>();

					for (int i = 0; i < table.Rows.Count; i++)
					{
						#region 处理不符合要求得数据
						DataRow row = table.Rows[i];
						row.BeginEdit();
						for (int a = 0; a < table.Columns.Count; a++)
						{


							if (row[a].ToString().IndexOf("....") > 0)
							{
								row[a] = row[a].ToString().Replace("....", ".");
							}
							if (row[a].ToString().IndexOf("...") > 0)
							{
								row[a] = row[a].ToString().Replace("...", ".");
							}
							if (row[a].ToString().IndexOf("..") > 0)
							{
								row[a] = row[a].ToString().Replace("..", ".");
							}

							if (row[a].ToString().IndexOf("null") > 0)
							{
								row[a] = row[a].ToString().Replace("null", "0");
							}
							if (string.IsNullOrEmpty(row[a].ToString()))
							{
								row[a] = 0;
							}
						}
						row.EndEdit();
						#endregion

						#region 保存尺码




						TrousersSize tszie = new TrousersSize();
						foreach (DataRow item in table.Rows)
						{


							tszie.Code = item["Code"].ToString();
							decimal ty = 0;
							if (decimal.TryParse(item["DZ_HipLength_CP"].ToString(), out ty))
							{
								tszie.DZ_HipLength_CP = ty;
							}
							else
							{
								tszie.DZ_HipLength_CP = 0;
							}


							if (decimal.TryParse(item["SZ_HipLength_CP"].ToString(), out ty))
							{
								tszie.SZ_HipLength_CP = ty;
							}
							else
							{
								tszie.SZ_HipLength_CP = 0;
							}

							if (decimal.TryParse(item["Crosspiece"].ToString(), out ty))
							{
								tszie.Crosspiece = ty;
							}
							else
							{
								tszie.Crosspiece = 0;
							}

							if (decimal.TryParse(item["LegWidth_UnderTheWaves"].ToString(), out ty))
							{
								tszie.LegWidth_UnderTheWaves = ty;
							}
							else
							{
								tszie.LegWidth_UnderTheWaves = 0;
							}

							if (decimal.TryParse(item["FrontRise_EvenWaist"].ToString(), out ty))
							{
								tszie.FrontRise_EvenWaist = ty;
							}
							else
							{
								tszie.FrontRise_EvenWaist = 0;
							}

							if (decimal.TryParse(item["AfterTheWaves_EvenWaist"].ToString(), out ty))
							{
								tszie.AfterTheWaves_EvenWaist = ty;
							}
							else
							{
								tszie.AfterTheWaves_EvenWaist = 0;
							}

							tszie.NetHip = item["NetHip"].ToString();
							tszie.CP_WaistWidth = item["CP_WaistWidth"].ToString();
							tszie.Height = item["Height"].ToString();
							tszie.LongPants = item["LongPants"].ToString();
							tszie.NetWaist = item["NetWaist"].ToString();
							tszie.Size_Code = Size_Code;



						}
						tszie_list.Add(tszie);

						#endregion


					}
					db.InsertRange(tszie_list);
					db.CommitTran();
					errmsg = "";
					return true;

				}
				catch (Exception ex)
				{

					db.RollbackTran();//回滚事务
					errmsg = ex.Message;
					return false;

				}
		}


	}
}
