 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlSugar;
using JuCheap.Service.Dto;
using static JuCheap.Core.SqlSugerHelper;
using SqlSugarRepository;
using JuCheap.Service.Abstracts;
using System.Text.RegularExpressions;
using JuCheap.Core;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class SizeController : AdmBaseController
    {

        public  IXF_KZ_CodeSizeService XF_KZ_Service { get; set; }
        public ICs_dataService Cs_dataService { get; set; }


        // GET: Adm/Size
        public ActionResult Import()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Examine()
        {
            using (var db = new MySqlServer())
                try
                {


                }
                catch (Exception)
                {

                    throw;
                }

            return View();
        }

        #region 审核
        [HttpPost]
        public JsonResult Examine(string Action)
        {
            using (var db = new MySqlServer())
                try
                {
                    List<Examine> List = new List<Examine>();

                    List<Examine> list = new List<Examine>();
                    switch (Action)
                    {
                        case "XF_SY_NAN":
                            List<XF_SY_NAN_ChiMaDto> qudate = db.Database.Queryable<XF_SY_NAN_ChiMaDto>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_SY_NAN_ChiMaDto>("Size_Code, MAX(CreateDateTime) as   CreateDateTime").ToList();

                            foreach (XF_SY_NAN_ChiMaDto item in qudate)
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":
                            List<XF_SY_NU_CodeSizeDto> XF_SY_NU = db.Database.Queryable<XF_SY_NU_CodeSizeDto>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_SY_NU_CodeSizeDto>("Size_Code, MAX(CreateDateTime) as   CreateDateTime").ToList();

                            foreach (XF_SY_NU_CodeSizeDto item in XF_SY_NU)
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NAN":
                            List<XF_KZ_CodeSizeDto> XF_KZ_NAN = db.Database.Queryable<XF_KZ_CodeSizeDto>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_KZ_CodeSizeDto>("Size_Code, MAX(CreateDateTime) as   CreateDateTime").ToList();

                            foreach (XF_KZ_CodeSizeDto item in XF_KZ_NAN)
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NU":
                            List<XF_KZ_CodeSizeDto> XF_KZ_NU = db.Database.Queryable<XF_KZ_CodeSizeDto>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_KZ_CodeSizeDto>("Size_Code, MAX(CreateDateTime) as   CreateDateTime").ToList();

                            foreach (XF_KZ_CodeSizeDto item in XF_KZ_NU)
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);


                        default:
                            throw new Exception("系统出错：没有对应的Action");

                    }
                }
                catch (Exception ex)
                {
                    return Json(new { state = -1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

        }

        [HttpPost]
        //修改尺码表状态
        public JsonResult UpdateState(string Code, string state, string Action)
        {
            using (var db = new MySqlServer())
                try
                {
                    switch (Action)
                    {
                        case "XF_SY_NAN":
                            db.Database.Update<XF_SY_NAN_ChiMaDto>(new { status = state }, it => it.Size_Code == Code);

                            return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":

                            db.Database.Update<XF_SY_NAN_ChiMaDto>(new { status = state }, it => it.Size_Code == Code);

                            return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NAN":
                            throw new Exception("正在开发");

                        case "XF_KZ_NU":
                            throw new Exception("正在开发");

                        default:
                            throw new Exception("系统出错：没有对应的Action");

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { state = -1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }


        }

        [HttpPost]
        //加载尺码表详细信息
        public JsonResult BugCodeSize(string Size_Code, string Action)
        {
            using (var db = new MySqlServer())
                try
                {
                    switch (Action)
                    {
                        case "XF_SY_NAN":
                            List<XF_SY_NAN_ChiMaDto> list = db.Database.Queryable<XF_SY_NAN_ChiMaDto>().Where(T => T.Status == 1 && T.Size_Code == Size_Code).ToList();
                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);
                        case "XF_SY_NU":
                            return Json(new { state = 1, msg = db.Database.Queryable<XF_SY_NU_CodeSizeDto>().Where(T => T.Status == 1 && T.Size_Code == Size_Code).ToList() }, JsonRequestBehavior.AllowGet);
                        case "XF_KZ_NAN":
                            return Json(new { state = 1, msg = db.Database.Queryable<XF_SY_NU_CodeSizeDto>().Where(T => T.Status == 1 && T.Size_Code == Size_Code).ToList() }, JsonRequestBehavior.AllowGet);
                        case "XF_KZ_NU":
                            return Json(new { state = 1, msg = db.Database.Queryable<XF_SY_NU_CodeSizeDto>().Where(T => T.Status == 1 && T.Size_Code == Size_Code).ToList() }, JsonRequestBehavior.AllowGet);
                        default:
                            throw new Exception("系统出错：没有对应的Action");

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { state = -1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }


        }

        //更新
        [HttpPost]
        public JsonResult UpdateCode(FormCollection fm)
        {
            using (var db = new MySqlServer())
                try
                {

                    switch (fm["Action"])
                    {
                        case "XF_SY_NAN":

                            var dic = new Dictionary<string, string>();
                            int count = fm["Height"].Split(',').Count();
                            for (int i = 0; i < count; i++)
                            {
                                dic.Add("Height", fm["Height"].Split(',')[i]);
                                dic.Add("FrontLength", fm["FrontLength"].Split(',')[i]);
                                dic.Add("NetBust", fm["NetBust"].Split(',')[i]);
                                dic.Add("FinishedBust", fm["FinishedBust"].Split(',')[i]);
                                dic.Add("InWaist", fm["InWaist"].Split(',')[i]);
                                dic.Add("FinishedHem_NoFork", fm["FinishedHem_NoFork"].Split(',')[i]);
                                dic.Add("FinishedHem_SplitEnds", fm["FinishedHem_SplitEnds"].Split(',')[i]);
                                dic.Add("ShoulderWidth", fm["ShoulderWidth"].Split(',')[i]);
                                dic.Add("Sleecve_Show", fm["Sleecve_Show"].Split(',')[i]);
                                int upid = 0;
                                if (!int.TryParse(fm["ID"].Split(',')[i], out upid))
                                {
                                    throw new Exception("数据ID格式不正确");
                                }

                                db.Database.Update<XF_SY_NAN_ChiMaDto, int>(dic, upid);
                                dic.Clear();
                            }
                            break;
                        case "XF_SY_NU":
                            var dic2 = new Dictionary<string, string>();
                            int count2 = fm["Height"].Split(',').Count();
                            for (int i = 0; i < count2; i++)
                            {
                                dic2.Add("Height", fm["Height"].Split(',')[i]);
                                dic2.Add("FrontLength", fm["FrontLength"].Split(',')[i]);
                                dic2.Add("NetBust", fm["NetBust"].Split(',')[i]);
                                dic2.Add("FinishedBust", fm["FinishedBust"].Split(',')[i]);
                                dic2.Add("InWaist", fm["InWaist"].Split(',')[i]);
                                dic2.Add("FinishedHem_NoFork", fm["FinishedHem_NoFork"].Split(',')[i]);
                                dic2.Add("SleeveWidth", fm["SleeveWidth"].Split(',')[i]);
                                dic2.Add("ShoulderWidth", fm["ShoulderWidth"].Split(',')[i]);
                                dic2.Add("Sleecve_Show", fm["Sleecve_Show"].Split(',')[i]);
                                int upid = 0;
                                if (!int.TryParse(fm["ID"].Split(',')[i], out upid))
                                {
                                    throw new Exception("数据ID格式不正确");
                                }

                                db.Database.Update<XF_SY_NU_CodeSizeDto, int>(dic2, upid);
                                dic2.Clear();
                            }
                            break;
                        case "XF_KZ_NAN":
                            var dic3 = new Dictionary<string, string>();
                            int count3 = fm["Height"].Split(',').Count();
                            for (int i = 0; i < count3; i++)
                            {
                                dic3.Add("Code", fm["Code"].Split(',')[i]);
                                dic3.Add("DZ_HipLength_CP", fm["DZ_HipLength_CP"].Split(',')[i]);
                                dic3.Add("SZ_HipLength_CP", fm["SZ_HipLength_CP"].Split(',')[i]);
                                dic3.Add("Crosspiece", fm["Crosspiece"].Split(',')[i]);
                                dic3.Add("LegWidth_UnderTheWaves", fm["LegWidth_UnderTheWaves"].Split(',')[i]);
                                dic3.Add("FrontRise_EvenWaist", fm["FrontRise_EvenWaist"].Split(',')[i]);
                                dic3.Add("AfterTheWaves_EvenWaist", fm["AfterTheWaves_EvenWaist"].Split(',')[i]);
                                dic3.Add("NetHip", fm["NetHip"].Split(',')[i]);
                                dic3.Add("CP_WaistWidth", fm["CP_WaistWidth"].Split(',')[i]);
                                dic3.Add("Height", fm["Height"].Split(',')[i]);
                                dic3.Add("LongPants", fm["LongPants"].Split(',')[i]);
                                dic3.Add("NetWaist", fm["NetWaist"].Split(',')[i]);

                                int upid = 0;
                                if (!int.TryParse(fm["ID"].Split(',')[i], out upid))
                                {
                                    throw new Exception("数据ID格式不正确");
                                }

                                db.Database.Update<XF_SY_NU_CodeSizeDto, int>(dic3, upid);
                                dic3.Clear();
                            }

                            break;
                        default:
                            break;
                    }

                    return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { state = -1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
        }

        #endregion



        //比对
        [HttpPost]
        public JsonResult Contrast(FormCollection fm)
        {
            JsonResult json = null;

            switch (fm["action"])
            {
                case "XF_SY_NAN"://西服上衣  男
                    json = XF_SY(fm, "男");
                    break;
                case "XF_SY_NU":
                    json = XF_SY(fm, "女");
                    break;
                default:
                    break;
            }


            return json;
        }

        #region 导入
        //导入
        [HttpPost]
        public JsonResult Import(FormCollection fm)
        {
            JsonResult json = null;

            switch (fm["action"])
            {
                case "XF_SY_NAN"://西服上衣  男
                    json = XF_SY(fm, "男");
                    break;
                case "XF_SY_NU":
                    json = XF_SY(fm, "女");
                    break;
                case "XF_KZ_NAN":
                    json = XF_KZ(fm, "男");
                    break;
                case "XF_KZ_NU":
                    json = XF_KZ(fm, "女");
                    break;
                default:
                    break;
            }


            return json;
        }



        //西服  上衣 
        public JsonResult XF_SY(FormCollection fm, string gender)
        {

            string errMsg = "";
            DataTable table;

            try
            {
                if (gender == "男")
                {
                    table = Analysis.Excel_analysis_NAN(Request.Files, gender);
                }
                else
                {
                    table = Analysis.Excel_analysis_NU(Request.Files, gender);
                }

                if (fm["import"] == "false")
                {
                    if (gender == "男")
                    {
                        return Json(new { state = 1, msg = Ret_Excel(table) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {


                        return Json(new { state = 1, msg = Ret_Excel_NU(table) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (Service.import.Import_Excel_jacket(table, fm["Size_Code"], gender, out errMsg))
                    {
                        return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { state = -1, msg = errMsg }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        //西服  裤子
        public JsonResult XF_KZ(FormCollection fm, string gender)
        {

            string errMsg = "";
            DataTable table;

            try
            {
                if (gender == "男")
                {
                    table = Analysis.Excel_trousrs_NAN(Request.Files);
                }
                else
                {
                    table = Analysis.Excel_trousrs_NU(Request.Files);

                }




                if (fm["import"] == "false")
                {
                    if (gender == "男")

                    {

                        return Json(new { state = 1, msg = Ret_Excel_trousrs(table) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {


                        return Json(new { state = 1, msg = Ret_Excel_trousrs(table) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (Import_XF_KZ(table, fm["Size_Code"], out errMsg))
                    {
                        return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { state = -1, msg = errMsg }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        #region 解析
        //西服尺码表解析
        public static object Ret_Excel(DataTable table)
        {



            List<Service.Dto.XF_SY_NAN_ChiMaDto> cslist = new List<Service.Dto.XF_SY_NAN_ChiMaDto>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                decimal ty = 0;
                Service.Dto.XF_SY_NAN_ChiMaDto cs = new Service.Dto.XF_SY_NAN_ChiMaDto();
                if (decimal.TryParse(table.Rows[i]["Height"] + "", out ty))
                {
                    cs.Height = ty;
                }
                else
                {
                    cs.Height = 0;
                }
                cs.FrontLength = table.Rows[i]["FrontLength"] + "";
                cs.NetBust = table.Rows[i]["NetBust"] + "";

                if (decimal.TryParse(table.Rows[i]["FinishedBust"] + "", out ty))
                {
                    cs.FinishedBust = ty;
                }
                else
                {
                    cs.FinishedBust = 0;
                }


                if (decimal.TryParse(table.Rows[i]["InWaist"] + "", out ty))
                {
                    cs.InWaist = ty;
                }
                else
                {
                    cs.InWaist = 0;
                }



                if (decimal.TryParse(table.Rows[i]["FinishedHem_NoFork"] + "", out ty))
                {
                    cs.FinishedHem_NoFork = ty;
                }
                else
                {
                    cs.FinishedHem_NoFork = 0;
                }

                if (decimal.TryParse(table.Rows[i]["FinishedHem_SplitEnds"] + "", out ty))
                {
                    cs.FinishedHem_SplitEnds = ty;
                }
                else
                {
                    cs.FinishedHem_NoFork = 0;
                }
                if (decimal.TryParse(table.Rows[i]["ShoulderWidth"] + "", out ty))
                {
                    cs.ShoulderWidth = ty;
                }
                else
                {
                    cs.ShoulderWidth = 0;
                }

                cs.Size_Code = table.Rows[i]["Size_Code"] + "";

                cs.Sleecve_Show = table.Rows[i]["FK_Sleeve_ID"] + "";

                cslist.Add(cs);
            }

            return cslist;
        }


        public static object Ret_Excel_NU(DataTable table)
        {



            List<Service.Dto.XF_SY_NU_CodeSizeDto> cslist = new List<Service.Dto.XF_SY_NU_CodeSizeDto>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                decimal ty = 0;
                Service.Dto.XF_SY_NU_CodeSizeDto cs = new Service.Dto.XF_SY_NU_CodeSizeDto();
                if (decimal.TryParse(table.Rows[i]["Height"] + "", out ty))
                {
                    cs.Height = ty;
                }
                else
                {
                    cs.Height = 0;
                }
                cs.FrontLength = table.Rows[i]["FrontLength"] + "";
                cs.NetBust = table.Rows[i]["NetBust"] + "";

                if (decimal.TryParse(table.Rows[i]["FinishedBust"] + "", out ty))
                {
                    cs.FinishedBust = ty;
                }
                else
                {
                    cs.FinishedBust = 0;
                }


                if (decimal.TryParse(table.Rows[i]["InWaist"] + "", out ty))
                {
                    cs.InWaist = ty;
                }
                else
                {
                    cs.InWaist = 0;
                }



                if (decimal.TryParse(table.Rows[i]["FinishedHem_NoFork"] + "", out ty))
                {
                    cs.FinishedHem_NoFork = ty;
                }
                else
                {
                    cs.FinishedHem_NoFork = 0;
                }


                string xxxxx = table.Rows[i]["ShoulderWidth"] + "";
                if (decimal.TryParse(table.Rows[i]["ShoulderWidth"] + "", out ty))
                {
                    cs.ShoulderWidth = ty;
                }
                else
                {
                    cs.ShoulderWidth = 0;
                }

                cs.Size_Code = table.Rows[i]["Size_Code"] + "";

                cs.Sleecve_Show = table.Rows[i]["FK_Sleeve_ID"] + "";

                cslist.Add(cs);
            }

            return cslist;
        }

        //西裤尺码表解析
        public static object Ret_Excel_trousrs(DataTable table)
        {

            List<XF_KZ_CodeSizeDto> cslist = new List<XF_KZ_CodeSizeDto>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                decimal ty = 0;
                XF_KZ_CodeSizeDto ts = new XF_KZ_CodeSizeDto();

                ts.Code = table.Rows[i]["Code"] + "";

                if (decimal.TryParse(table.Rows[i]["DZ_HipLength_CP"] + "", out ty))
                {
                    ts.DZ_HipLength_CP = ty;
                }
                else
                {
                    ts.DZ_HipLength_CP = 0;
                }

                ts.SZ_HipLength_CP = 0;

                if (decimal.TryParse(table.Rows[i]["SZ_HipLength_CP"] + "", out ty))
                {
                    ts.SZ_HipLength_CP = ty;
                }
                else
                {
                    ts.SZ_HipLength_CP = 0;
                }


                if (decimal.TryParse(table.Rows[i]["Crosspiece"] + "", out ty))
                {
                    ts.Crosspiece = ty;
                }
                else
                {
                    ts.Crosspiece = 0;
                }



                if (decimal.TryParse(table.Rows[i]["LegWidth_UnderTheWaves"] + "", out ty))
                {
                    ts.LegWidth_UnderTheWaves = ty;
                }
                else
                {
                    ts.LegWidth_UnderTheWaves = 0;
                }

                if (decimal.TryParse(table.Rows[i]["FrontRise_EvenWaist"] + "", out ty))
                {
                    ts.FrontRise_EvenWaist = ty;
                }
                else
                {
                    ts.FrontRise_EvenWaist = 0;
                }

                if (decimal.TryParse(table.Rows[i]["AfterTheWaves_EvenWaist"] + "", out ty))
                {
                    ts.AfterTheWaves_EvenWaist = ty;
                }
                else
                {
                    ts.AfterTheWaves_EvenWaist = 0;
                }

                ts.NetHip = table.Rows[i]["NetHip"] + "";

                ts.CP_WaistWidth = table.Rows[i]["CP_WaistWidth"] + "";
                ts.Height = table.Rows[i]["Height"] + "";
                ts.LongPants = table.Rows[i]["LongPants"] + "";
                ts.NetWaist = table.Rows[i]["NetWaist"] + "";


                cslist.Add(ts);
            }

            return cslist;


        }


        #endregion


        //上传西服裤子尺码表
 
        public  bool Import_XF_KZ(DataTable table, string Size_Code, out string errmsg)
        {

      
                try
                {
                    List<XF_KZ_CodeSizeDto> tszie_list = new List<XF_KZ_CodeSizeDto>();

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

                        XF_KZ_CodeSizeDto tszie = new XF_KZ_CodeSizeDto();
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
                            tszie.CreateDateTime = DateTime.Now;
                            tszie.Status = 1;
                            tszie.RowData = item["Code"] + "-" + item["DZ_HipLength_CP"] + "" + item["SZ_HipLength_CP"] + "-" + item["Crosspiece"] + "-" + item["LegWidth_UnderTheWaves"] + "-" + item["FrontRise_EvenWaist"] + "-" + item["AfterTheWaves_EvenWaist"] + "-" + item["NetHip"] + "-" + item["CP_WaistWidth"] + "-" + item["CP_WaistWidth"] + "-" + item["Height"] + "-" + item["LongPants"] + "-" + item["NetWaist"] + "-" + Size_Code;
                        }
                        tszie_list.Add(tszie);

                        #endregion


                    }
 
                    XF_KZ_Service.Add(tszie_list[0]);

               
                //    db.Database.CommitTran();
                    errmsg = "";
                    return true;

                }
                catch (Exception ex)
                {

                   // db.Database.RollbackTran();//回滚事务
                    errmsg = ex.Message;
                    return false;

                }
        }

        #endregion

        #region 管理
        // GET: Adm/Size
        public ActionResult Manage()
        {

            return View();
        }
        [HttpPost]
        public JsonResult Manage(string Code, string Action)
        {
            using (var db = new MySqlServer())
                try
                {

                    switch (Action)
                    {
                        case "XF_SY_NAN"://西服上衣  男
                            return Json(new { state = 1, msg = db.Database.Queryable<XF_SY_NAN_ChiMaDto>().Where(it => it.Size_Code == Code).ToList() }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":

                            return Json(new { state = 1, msg = db.Database.Queryable<XF_SY_NU_CodeSizeDto>().Where(it => it.Size_Code == Code).ToList() }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NAN":
                        case "XF_KZ_NU":

                            return Json(new { state = 1, msg = db.Database.Queryable<XF_KZ_CodeSizeDto>().Where(it => it.Size_Code == Code).ToList() }, JsonRequestBehavior.AllowGet);
                        default:
                            throw new Exception("系统出错：没有对应的Action");

                    }


                }
                catch (Exception ex)
                {
                    return Json(new { state = 1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }



        }


        //尺码表编号下拉
        public JsonResult SizeCodeSelect(string Action)
        {
            using (var db = new MySqlServer())
                try
                {

                    switch (Action)
                    {
                        case "XF_SY_NAN"://西服上衣  男

                            List<XF_SY_NAN_ChiMaDto> XF_SY_NAN = db.Database.Queryable<XF_SY_NAN_ChiMaDto>().Where(c => c.Status == 0).GroupBy(it => it.Size_Code).Select<XF_SY_NAN_ChiMaDto>("Size_Code").ToList();


                            return Json(new { state = 1, msg = XF_SY_NAN }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":

                            List<XF_SY_NU_CodeSizeDto> XF_SY_NU = db.Database.Queryable<XF_SY_NU_CodeSizeDto>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_SY_NU_CodeSizeDto>("Size_Code").ToList();


                            return Json(new { state = 1, msg = XF_SY_NU }, JsonRequestBehavior.AllowGet);

                        default:
                            throw new Exception("系统出错：没有对应的Action");

                    }



                }
                catch (Exception ex)
                {
                    return Json(new { state = -1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
        }

        #endregion

    }

    public class Examine
    {

        public string Code { get; set; }
        public string CreateTime { get; set; }


    }

}