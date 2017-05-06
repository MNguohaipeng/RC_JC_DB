

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;



namespace JuCheap.Core
{
	public static class Analysis
	{

		//解析西服裤子excel
		public static DataTable Excel_analysis_trousrs(HttpFileCollectionBase files)
		{

			try
			{

				DataTable excelTable = new DataTable();
				excelTable.Columns.Add("order_no");
				excelTable.Columns.Add("order_xc");
				excelTable.Columns.Add("name");
				excelTable.Columns.Add("ret_code_size");
				excelTable.Columns.Add("number");
				excelTable.Columns.Add("remarks");

				#region 上传excel到服务器 并处理  返回datatable
				string url;
				string errmsg;


				Common.UpLoadFile(files[0], "File" + DateTime.Now.ToString("sshhffffff") + ".xls", "~/Data", out url, out errmsg);


				DataTable table = ExcelHelper.InputFromExcel(HttpContext.Current.Server.MapPath(url), "原计划");



				#endregion

				string gdbh = "";//工单编号  1
				string cplh = "";//成品料号  2
				string scsl = "";//生产数量  3
				string pm = "";//品    名  4
				string gg = ""; //规 格  5
				int index = 0;
				#region 解析返回得excel数据
				for (int i = 0; i < table.Rows.Count; i++)
				{
					#region 获取表头变量
					for (int b = 0; b < table.Columns.Count; b++)
					{
						if (gg != "")//前5个变量都取完了  关掉循环  
						{
							break;
						}


						if (!string.IsNullOrEmpty(table.Rows[i][b].ToString()))
						{

							if (index != 0)
							{
								switch (index)
								{
									case 1:
										gdbh = table.Rows[i][b].ToString();
										break;
									case 2:
										cplh = table.Rows[i][b].ToString();
										break;
									case 3:
										scsl = table.Rows[i][b].ToString();
										break;
									case 4:
										pm = table.Rows[i][b].ToString();
										break;
									case 5:
										gg = table.Rows[i][b].ToString();

										break;

								}

								index = 0;
							}



							switch (table.Rows[i][b].ToString().Trim())
							{
								case "工单编号:":
									index = 1;
									break;
								case "成品料号:":
									index = 2;
									break;
								case "生产数量:":
									index = 3;
									break;
								case "品    名:":
									index = 4;
									break;
								case "规    格:":
									index = 5;
									break;
							}
						}

					}
					#endregion

					#region 主体数据部分

					if (table.Rows[i]["F2"].ToString() == "制单人")
						break;


					if (table.Rows[i]["F2"].ToString() != "" && table.Rows[i]["F2"].ToString() != "订单编号" && table.Rows[i]["F2"].ToString() != "工单尺码汇总表")
					{
						DataRow row = excelTable.NewRow();
						row["order_no"] = table.Rows[i]["F2"].ToString();
						row["order_xc"] = table.Rows[i]["F6"].ToString();
						row["name"] = table.Rows[i]["F8"].ToString();
						row["ret_code_size"] = table.Rows[i]["F10"].ToString();
						row["number"] = table.Rows[i]["F16"].ToString();
						row["remarks"] = table.Rows[i]["F19"].ToString();
						excelTable.Rows.Add(row);

					}


					#endregion
				}



				#endregion

				return excelTable;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		//解析西服上衣尺码（男）
		public static DataTable Excel_analysis_NAN(HttpFileCollectionBase files, string gender)
		{
			Dictionary<string, string> callback_json = new Dictionary<string, string>();

			DataTable execltable;

			try
			{

				string url;

				string errmsg;

				Common.UpLoadFile(files[0], "File" + DateTime.Now.ToString("sshhffffff") + ".xls", "~/Data", out url, out errmsg);

				DataTable execl_table = ExcelHelper.InputFromExcel(HttpContext.Current.Server.MapPath(url), "sheet1");

				if (!string.IsNullOrEmpty(gender))
				{

					if (gender == "男")
					{
						Verification.Verification_XF_SY_NAN(execl_table);
					}
					else
					{
						Verification.Verification_XF_SY_NU(execl_table);
					}
				}
				string height_o = "";

				string height_t = "";

				string length_o = "";

				string length_t = "";

				#region 创建最终table
				execltable = new DataTable();
				execltable.Columns.Add("Height");
				execltable.Columns.Add("FrontLength");
				execltable.Columns.Add("NetBust");
				execltable.Columns.Add("FinishedBust");
				execltable.Columns.Add("InWaist");
				execltable.Columns.Add("FinishedHem_NoFork");
				execltable.Columns.Add("FinishedHem_SplitEnds");
				execltable.Columns.Add("ShoulderWidth");
				execltable.Columns.Add("FK_Sleeve_ID");
				execltable.Columns.Add("Size_Code");
				#endregion

				#region 处理身高数据和袖长数据

				int[] remove_arrey = new int[execl_table.Rows.Count];

				for (int i = 0; i < execl_table.Rows.Count; i++)
				{

					if (!Common.IsNatural_Number(execl_table.Rows[i][0] + ""))
					{

						remove_arrey[i] = i;

						continue;

					}

					#region 处理身高数据不全
					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F2"] + "") && execl_table.Rows[i]["F2"] + "" != "身高")
					{

						height_o = execl_table.Rows[i]["F2"] + "";

					}
					else
					{

						execl_table.Rows[i]["F2"] = height_o;

					}

					#endregion

					#region 处理袖长数据不全

					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F10"] + ""))
					{

						length_o = execl_table.Rows[i]["F10"] + "";

					}
					else
					{

						execl_table.Rows[i]["F10"] = length_o;

					}

					#endregion

				}
				int del_index = 0;

				for (int i = 0; i < remove_arrey.Length; i++)
				{

					if (remove_arrey[i] != 0)
					{

						execl_table.Rows.RemoveAt(remove_arrey[i] - del_index);

						del_index++;

					}
				}

				execl_table.Rows.RemoveAt(0);

				for (int i = 0; i < execl_table.Rows.Count; i++)
				{

					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F13"] + "") && execl_table.Rows[i]["F13"] + "" != "身高")
					{

						height_t = execl_table.Rows[i]["F13"].ToString();

					}
					else
					{

						execl_table.Rows[i]["F13"] = height_t;

					}
					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F21"] + ""))
					{
						length_t = execl_table.Rows[i]["F21"] + "";
					}
					else
					{
						execl_table.Rows[i]["F21"] = length_t;
					}
				}
				#region  处理非数据部分
				#endregion
				for (int i = 0; i < execl_table.Rows.Count; i++)

				{
					#region 将数据剥离并填入datatable
					if (!string.IsNullOrEmpty(execl_table.Rows[i][0] + ""))
					{
						DataRow row01 = execltable.NewRow();
						row01["Height"] = execl_table.Rows[i]["F2"] + "";
						row01["FrontLength"] = execl_table.Rows[i]["F3"] + "";
						row01["NetBust"] = execl_table.Rows[i]["F4"] + "";
						row01["FinishedBust"] = execl_table.Rows[i]["F5"] + "";
						row01["InWaist"] = execl_table.Rows[i]["F6"] + "";
						row01["FinishedHem_NoFork"] = execl_table.Rows[i]["F7"] + "";
						row01["FinishedHem_SplitEnds"] = execl_table.Rows[i]["F8"] + "";
						row01["ShoulderWidth"] = execl_table.Rows[i]["F9"] + "";
						row01["FK_Sleeve_ID"] = execl_table.Rows[i]["F10"] + "";

						execltable.Rows.Add(row01);
					}
				}



				for (int i = 0; i < execl_table.Rows.Count; i++)
				{
					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F12"] + ""))
					{
						DataRow row02 = execltable.NewRow();
						row02["Height"] = execl_table.Rows[i]["F13"] + "";
						row02["FrontLength"] = execl_table.Rows[i]["F14"] + "";
						row02["NetBust"] = execl_table.Rows[i]["F15"] + "";
						row02["FinishedBust"] = execl_table.Rows[i]["F16"] + "";
						row02["InWaist"] = execl_table.Rows[i]["F17"] + "";
						row02["FinishedHem_NoFork"] = execl_table.Rows[i]["F18"] + "";
						row02["FinishedHem_SplitEnds"] = execl_table.Rows[i]["F19"] + "";
						row02["ShoulderWidth"] = execl_table.Rows[i]["F20"] + "";
						row02["FK_Sleeve_ID"] = execl_table.Rows[i]["F21"] + "";
						//      row02["Size_Code"] = execl_table.Rows[i]["F10"] + "";
						execltable.Rows.Add(row02);
					}
				}
				#endregion

				#endregion
				//execltable

				return execltable;
			}
			catch (Exception ex)
			{
				throw;
			}


		}

		//解析西服上衣尺码（女）
		public static DataTable Excel_analysis_NU(HttpFileCollectionBase files, string gender)
		{
			Dictionary<string, string> callback_json = new Dictionary<string, string>();

			DataTable execltable;

			try
			{

				string url;

				string errmsg;

				Common.UpLoadFile(files[0], "File" + DateTime.Now.ToString("sshhffffff") + ".xls", "~/Data", out url, out errmsg);

				DataTable execl_table = ExcelHelper.InputFromExcel(HttpContext.Current.Server.MapPath(url), "sheet1");


				Verification.Verification_XF_SY_NU(execl_table);

				string height_o = "";

				string height_t = "";

				string length_o = "";

				string length_t = "";

				#region 创建最终table
				execltable = new DataTable();
				execltable.Columns.Add("Height");
				execltable.Columns.Add("FrontLength");
				execltable.Columns.Add("NetBust");
				execltable.Columns.Add("FinishedBust");
				execltable.Columns.Add("InWaist");
				execltable.Columns.Add("FinishedHem_NoFork");
				execltable.Columns.Add("SleeveWidth");
				execltable.Columns.Add("ShoulderWidth");
				execltable.Columns.Add("FK_Sleeve_ID");
				execltable.Columns.Add("Size_Code");
				#endregion

				#region 处理身高数据和袖长数据

				int[] remove_arrey = new int[execl_table.Rows.Count];

				for (int i = 0; i < execl_table.Rows.Count; i++)
				{

					if (!Common.IsNatural_Number(execl_table.Rows[i][0] + ""))
					{

						remove_arrey[i] = i;

						continue;

					}

					#region 处理身高数据不全
					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F2"] + "") && execl_table.Rows[i]["F2"] + "" != "身高")
					{

						height_o = execl_table.Rows[i]["F2"] + "";

					}
					else
					{

						execl_table.Rows[i]["F2"] = height_o;

					}

					#endregion

					#region 处理袖长数据不全

					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F10"] + ""))
					{

						length_o = execl_table.Rows[i]["F10"] + "";

					}
					else
					{

						execl_table.Rows[i]["F10"] = length_o;

					}

					#endregion

				}
				int del_index = 0;

				for (int i = 0; i < remove_arrey.Length; i++)
				{

					if (remove_arrey[i] != 0)
					{

						execl_table.Rows.RemoveAt(remove_arrey[i] - del_index);

						del_index++;

					}
				}

				execl_table.Rows.RemoveAt(0);

				for (int i = 0; i < execl_table.Rows.Count; i++)
				{

					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F13"] + "") && execl_table.Rows[i]["F13"] + "" != "身高")
					{

						height_t = execl_table.Rows[i]["F13"].ToString();

					}
					else
					{

						execl_table.Rows[i]["F13"] = height_t;

					}
					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F21"] + ""))
					{
						length_t = execl_table.Rows[i]["F21"] + "";
					}
					else
					{
						execl_table.Rows[i]["F21"] = length_t;
					}
				}
				#region  处理非数据部分
				#endregion
				for (int i = 0; i < execl_table.Rows.Count; i++)

				{
					#region 将数据剥离并填入datatable
					if (!string.IsNullOrEmpty(execl_table.Rows[i][0] + ""))
					{
						DataRow row01 = execltable.NewRow();
						row01["Height"] = execl_table.Rows[i]["F2"] + "";
						row01["FrontLength"] = execl_table.Rows[i]["F3"] + "";
						row01["NetBust"] = execl_table.Rows[i]["F4"] + "";
						row01["FinishedBust"] = execl_table.Rows[i]["F5"] + "";
						row01["InWaist"] = execl_table.Rows[i]["F6"] + "";
						row01["FinishedHem_NoFork"] = execl_table.Rows[i]["F7"] + "";
						row01["SleeveWidth"] = execl_table.Rows[i]["F8"] + "";
						row01["ShoulderWidth"] = execl_table.Rows[i]["F9"] + "";
						row01["FK_Sleeve_ID"] = execl_table.Rows[i]["F10"] + "";

						execltable.Rows.Add(row01);
					}
				}



				for (int i = 0; i < execl_table.Rows.Count; i++)
				{
					if (!string.IsNullOrEmpty(execl_table.Rows[i]["F12"] + ""))
					{
						DataRow row02 = execltable.NewRow();
						row02["Height"] = execl_table.Rows[i]["F13"] + "";
						row02["FrontLength"] = execl_table.Rows[i]["F14"] + "";
						row02["NetBust"] = execl_table.Rows[i]["F15"] + "";
						row02["FinishedBust"] = execl_table.Rows[i]["F16"] + "";
						row02["InWaist"] = execl_table.Rows[i]["F17"] + "";
						row02["FinishedHem_NoFork"] = execl_table.Rows[i]["F18"] + "";
			 
						row02["ShoulderWidth"] = execl_table.Rows[i]["F19"] + "";
						row02["SleeveWidth"] = execl_table.Rows[i]["F20"] + "";

						row02["FK_Sleeve_ID"] = execl_table.Rows[i]["F21"] + "";

						execltable.Rows.Add(row02);
					}
				}
				#endregion

				#endregion


				return execltable;
			}
			catch (Exception ex)
			{
				throw;
			}


		}

		//解析西服裤子尺码
		public static DataTable Excel_trousrs_Code(HttpFileCollectionBase files)
		{
			DataTable execltable = new DataTable();
			try
			{


				string url;
				string errmsg;


				Common.UpLoadFile(files[0], "File" + DateTime.Now.ToString("sshhffffff") + ".xls", "~/Data", out url, out errmsg);


				DataTable execl_table = ExcelHelper.InputFromExcel(HttpContext.Current.Server.MapPath(url), "sheet1");


				#region 创建最终table
				execltable = new DataTable();
				execltable.Columns.Add("Code");
				execltable.Columns.Add("DZ_HipLength_CP");
				execltable.Columns.Add("SZ_HipLength_CP");
				execltable.Columns.Add("Crosspiece");
				execltable.Columns.Add("LegWidth_UnderTheWaves");
				execltable.Columns.Add("FrontRise_EvenWaist");
				execltable.Columns.Add("AfterTheWaves_EvenWaist");
				execltable.Columns.Add("NetHip");
				execltable.Columns.Add("CP_WaistWidth");
				execltable.Columns.Add("Height");
				execltable.Columns.Add("LongPants");
				execltable.Columns.Add("NetWaist");

				#endregion

				#region 处理数据

				int[] del_index = new int[execl_table.Rows.Count];
				for (int i = 0; i < execl_table.Rows.Count; i++)
				{
					if (Common.IsChinses(execl_table.Rows[i][0].ToString().Trim()))
					{
						if (execl_table.Rows[i][0].ToString().Trim() != "代号")
						{
							del_index[i] = i;
							continue;
						}
					}



					if (string.IsNullOrEmpty(execl_table.Rows[i]["F3"].ToString()))
					{
						del_index[i] = i;

					}
					else
					{

						double jyw;
						if (double.TryParse(execl_table.Rows[i][0] + "", out jyw))
						{
							execl_table.Rows[i][0] += "~" + (jyw + 2);
						}
						double cpyw;
						if (double.TryParse(execl_table.Rows[i]["F2"] + "", out cpyw))
						{
							execl_table.Rows[i]["F2"] += "~" + (cpyw + 2);
						}
					}


				}

				int del_count = 0;
				for (int i = 0; i < del_index.Length; i++)
				{
					if (del_index[i] != 0)
					{
						execl_table.Rows.RemoveAt(del_index[i] - del_count);
						del_count++;
					}
				}
				execl_table.Rows.RemoveAt(0);





				#endregion

				#region 填充数据
				string[] code = new string[4];
				for (int i = 0; i < execl_table.Rows.Count; i++)
				{
					if (Common.IsEnglish_Length_One(execl_table.Rows[i]["F3"].ToString()))
						code[0] = execl_table.Rows[i]["F3"].ToString();
					if (Common.IsEnglish_Length_One(execl_table.Rows[i]["F10"].ToString()))
						code[1] = execl_table.Rows[i]["F10"].ToString();
					if (Common.IsEnglish_Length_One(execl_table.Rows[i]["F17"].ToString()))
						code[2] = execl_table.Rows[i]["F17"].ToString();
					if (Common.IsEnglish_Length_One(execl_table.Rows[i]["F24"].ToString()))
						code[3] = execl_table.Rows[i]["F24"].ToString();

					if (Common.IsChinses(execl_table.Rows[i][0].ToString()))
						continue;

					DataRow row0 = execltable.NewRow();
					row0["Code"] = code[0];
					row0["NetWaist"] = execl_table.Rows[i][0];
					row0["CP_WaistWidth"] = execl_table.Rows[i][1];
					row0["NetHip"] = execl_table.Rows[i][2];
					row0["DZ_HipLength_CP"] = execl_table.Rows[i][3];
					row0["SZ_HipLength_CP"] = execl_table.Rows[i][4];
					row0["Crosspiece"] = execl_table.Rows[i][5];
					row0["LegWidth_UnderTheWaves"] = execl_table.Rows[i][6];
					row0["FrontRise_EvenWaist"] = execl_table.Rows[i][7];
					row0["AfterTheWaves_EvenWaist"] = execl_table.Rows[i][8];
					execltable.Rows.Add(row0);
					DataRow row1 = execltable.NewRow();
					row1["Code"] = code[1];
					row1["NetWaist"] = execl_table.Rows[i][0];
					row1["CP_WaistWidth"] = execl_table.Rows[i][1];
					row1["NetHip"] = execl_table.Rows[i][9];
					row1["DZ_HipLength_CP"] = execl_table.Rows[i][10];
					row1["SZ_HipLength_CP"] = execl_table.Rows[i][11];
					row1["Crosspiece"] = execl_table.Rows[i][12];
					row1["LegWidth_UnderTheWaves"] = execl_table.Rows[i][13];
					row1["FrontRise_EvenWaist"] = execl_table.Rows[i][14];
					row1["AfterTheWaves_EvenWaist"] = execl_table.Rows[i][15];
					execltable.Rows.Add(row1);
					DataRow row2 = execltable.NewRow();
					row2["Code"] = code[2];
					row2["NetWaist"] = execl_table.Rows[i][0];
					row2["CP_WaistWidth"] = execl_table.Rows[i][1];
					row2["NetHip"] = execl_table.Rows[i][16];
					row2["DZ_HipLength_CP"] = execl_table.Rows[i][17];
					row2["SZ_HipLength_CP"] = execl_table.Rows[i][18];
					row2["Crosspiece"] = execl_table.Rows[i][19];
					row2["LegWidth_UnderTheWaves"] = execl_table.Rows[i][20];
					row2["FrontRise_EvenWaist"] = execl_table.Rows[i][21];
					row2["AfterTheWaves_EvenWaist"] = execl_table.Rows[i][22];
					execltable.Rows.Add(row2);
					DataRow row3 = execltable.NewRow();
					row3["Code"] = code[3];
					row3["NetWaist"] = execl_table.Rows[i][0];
					row3["CP_WaistWidth"] = execl_table.Rows[i][1];
					row3["NetHip"] = execl_table.Rows[i][23];
					row3["DZ_HipLength_CP"] = execl_table.Rows[i][24];
					row3["SZ_HipLength_CP"] = execl_table.Rows[i][25];
					row3["Crosspiece"] = execl_table.Rows[i][26];
					row3["LegWidth_UnderTheWaves"] = execl_table.Rows[i][27];
					row3["FrontRise_EvenWaist"] = execl_table.Rows[i][28];
					row3["AfterTheWaves_EvenWaist"] = execl_table.Rows[i][29];
					execltable.Rows.Add(row3);
				}

				#endregion

				return execltable;

			}
			catch (Exception ex)
			{
				return null;

			}



		}

	}
}
