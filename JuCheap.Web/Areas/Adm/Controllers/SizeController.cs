﻿
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
using System.Collections;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class SizeController : AdmBaseController
    {

        #region Service

        public IXF_KZ_CodeSizeService XF_KZ_Service { get; set; }

        public IDCL_DataService Cs_dataService { get; set; }

        public IXF_SY_NAN_ChiMaService XF_SY_NAN_ChiMa { get; set; }

        public IXF_SY_NU_CodeSizeService XF_SY_NU_ChiMa { get; set; }

        public IXF_KZ_CodeSizeService XF_KZ_CodeSizeService { get; set; }

        public IHeightKuChangService HeightKuChangService { get; set; }

        #endregion

        #region Get
        // GET: Adm/Size
        public ActionResult Import()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Examine()
        {
   
            return View();
        }



        #endregion
 
        #region 审核
        [HttpPost]
        public JsonResult Examine(string Action)
        {
            using (var db = new MySqlServer())
                try
                {


                    List<Examine> list = new List<Examine>();
                    switch (Action)
                    {
                        case "XF_SY_NAN":

                            var qudate = XF_SY_NAN_ChiMa.Query(c => c.Status == 1, o => o.Id, false);


                            foreach (var item in qudate.GroupBy(it => it.Size_Code).Select(s => new { CreateDateTime = s.Max(l => l.CreateDateTime), Size_Code = s.Key }))
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":

                            var qudatenu = XF_SY_NU_ChiMa.Query(c => c.Status == 1, o => o.Id, false);

                            foreach (var item in qudatenu.GroupBy(it => it.Size_Code).Select(s => new { CreateDateTime = s.Max(l => l.CreateDateTime), Size_Code = s.Key }))
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NAN":

                            var qudatekznan = XF_KZ_Service.Query(c => c.Status == 1, o => o.Id, false);

                            foreach (var item in qudatekznan.GroupBy(it => it.Size_Code).Select(s => new { CreateDateTime = s.Max(l => l.CreateDateTime), Size_Code = s.Key }))
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NU":

                            var qudatekznu = XF_KZ_Service.Query(c => c.Status == 1, o => o.Id, false);

                            foreach (var item in qudatekznu.GroupBy(it => it.Size_Code).Select(s => new { CreateDateTime = s.Max(l => l.CreateDateTime), Size_Code = s.Key }))
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

                            db.Database.GetInt(string.Format("update   XF_SY_NAN_ChiMa set status='{0}' where Size_Code='{1}'", state, Code));
                            return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);
                        case "XF_SY_NU":
                            db.Database.GetInt(string.Format("update XF_SY_NU_ChiMa set status='{0}' where Size_Code='{1}'", state, Code));
                            return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);
                        case "XF_KZ_NAN":
                            db.Database.GetInt(string.Format("update XF_KZ_CodeSize set status='{0}' where Size_Code='{1}'", state, Code));
                            return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);
                        case "XF_KZ_NU":
                            db.Database.GetInt(string.Format("update XF_KZ_CodeSize set status='{0}' where Size_Code='{1}'", state, Code));
                            return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);
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

                            return Json(new { state = 1, msg = XF_SY_NAN_ChiMa.Query(T => T.Status == 1 && T.Size_Code == Size_Code, o => o.Id, false).ToList() }, JsonRequestBehavior.AllowGet);
                        case "XF_SY_NU":

                            return Json(new { state = 1, msg = XF_SY_NU_ChiMa.Query(T => T.Status == 1 && T.Size_Code == Size_Code, o => o.Id, false).ToList() }, JsonRequestBehavior.AllowGet);
                        case "XF_KZ_NAN":
                            return Json(new { state = 1, msg = XF_KZ_Service.Query(T => T.Status == 1 && T.Size_Code == Size_Code, o => o.Id, false).ToList() }, JsonRequestBehavior.AllowGet);
                        case "XF_KZ_NU":
                            return Json(new { state = 1, msg = XF_KZ_Service.Query(T => T.Status == 1 && T.Size_Code == Size_Code, o => o.Id, false).ToList() }, JsonRequestBehavior.AllowGet);
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

                            List<XF_SY_NAN_ChiMaDto> xfnanlist = new List<XF_SY_NAN_ChiMaDto>();
                            int count = fm["Height"].Split(',').Count();
                            for (int i = 0; i < count; i++)
                            {
                                int upid = 0;
                                if (!int.TryParse(fm["ID"].Split(',')[i], out upid))
                                {
                                    XF_SY_NAN_ChiMaDto tjdto = new XF_SY_NAN_ChiMaDto();
                                    tjdto.Height = Convert.ToInt32(fm["Height"].Split(',')[i]);
                                    tjdto.FrontLength = fm["FrontLength"].Split(',')[i];
                                    tjdto.NetBust = fm["NetBust"].Split(',')[i];
                                    tjdto.FinishedBust = Convert.ToDecimal(fm["FinishedBust"].Split(',')[i]);
                                    tjdto.InWaist = Convert.ToDecimal(fm["InWaist"].Split(',')[i]);
                                    tjdto.FinishedHem_NoFork = Convert.ToDecimal(fm["FinishedHem_NoFork"].Split(',')[i]);
                                    tjdto.FinishedHem_SplitEnds = Convert.ToDecimal(fm["FinishedHem_SplitEnds"].Split(',')[i]);
                                    tjdto.ShoulderWidth = Convert.ToDecimal(fm["ShoulderWidth"].Split(',')[i]);
                                    tjdto.Sleecve_Show = fm["Sleecve_Show"].Split(',')[i];
                                    tjdto.Id = upid;
                                    tjdto.Status = 0;
                                    tjdto.Size_Code = fm["Size_Code"];
                                    XF_SY_NAN_ChiMa.Update(tjdto);
                                }
                                else {
                                    XF_SY_NAN_ChiMaDto xfnan = XF_SY_NAN_ChiMa.GetOne(t => t.Id == upid);
                                    xfnan.Height = Convert.ToInt32(fm["Height"].Split(',')[i]);
                                    xfnan.FrontLength = fm["FrontLength"].Split(',')[i];
                                    xfnan.NetBust = fm["NetBust"].Split(',')[i];
                                    xfnan.FinishedBust = Convert.ToDecimal(fm["FinishedBust"].Split(',')[i]);
                                    xfnan.InWaist = Convert.ToDecimal(fm["InWaist"].Split(',')[i]);
                                    xfnan.FinishedHem_NoFork = Convert.ToDecimal(fm["FinishedHem_NoFork"].Split(',')[i]);
                                    xfnan.FinishedHem_SplitEnds = Convert.ToDecimal(fm["FinishedHem_SplitEnds"].Split(',')[i]);
                                    xfnan.ShoulderWidth = Convert.ToDecimal(fm["ShoulderWidth"].Split(',')[i]);
                                    xfnan.Sleecve_Show = fm["Sleecve_Show"].Split(',')[i];
                                    xfnan.Id = upid;
                                    xfnan.Status = 1;
                               
                                    XF_SY_NAN_ChiMa.Update(xfnan);
                                }
                               

                            }
                            
                            break;
                        case "XF_SY_NU":

                            int count2 = fm["Height"].Split(',').Count();
                            List<XF_SY_NU_CodeSizeDto> synulist = new List<XF_SY_NU_CodeSizeDto>();
                            for (int i = 0; i < count2; i++)
                            {
                                int upid = 0;
                                if (!int.TryParse(fm["ID"].Split(',')[i], out upid))
                                {
                                    XF_SY_NU_CodeSizeDto synu = new XF_SY_NU_CodeSizeDto();
                                    synu.Height = Convert.ToDecimal(fm["Height"].Split(',')[i]);
                                    synu.FrontLength = fm["FrontLength"].Split(',')[i];
                                    synu.NetBust = fm["NetBust"].Split(',')[i];
                                    synu.FinishedBust = Convert.ToDecimal(fm["FinishedBust"].Split(',')[i]);
                                    synu.InWaist = Convert.ToDecimal(fm["InWaist"].Split(',')[i]);
                                    synu.FinishedHem_NoFork = Convert.ToDecimal(fm["FinishedHem_NoFork"].Split(',')[i]);
                                    synu.SleeveWidth = Convert.ToDecimal(fm["SleeveWidth"].Split(',')[i]);
                                    synu.ShoulderWidth = Convert.ToDecimal(fm["ShoulderWidth"].Split(',')[i]);
                                    synu.Sleecve_Show = fm["FinishedHem_NoFork"].Split(',')[i];
                                    synu.Size_Code = fm["Size_Code"];
                                    XF_SY_NU_ChiMa.Add(synu);
                                }
                                else {
                                    XF_SY_NU_CodeSizeDto synu = XF_SY_NU_ChiMa.GetOne(T => T.Id == upid);
                                    synu.Height = Convert.ToDecimal(fm["Height"].Split(',')[i]);
                                    synu.FrontLength = fm["FrontLength"].Split(',')[i];
                                    synu.NetBust = fm["NetBust"].Split(',')[i];
                                    synu.FinishedBust = Convert.ToDecimal(fm["FinishedBust"].Split(',')[i]);
                                    synu.InWaist = Convert.ToDecimal(fm["InWaist"].Split(',')[i]);
                                    synu.FinishedHem_NoFork = Convert.ToDecimal(fm["FinishedHem_NoFork"].Split(',')[i]);
                                    synu.SleeveWidth = Convert.ToDecimal(fm["SleeveWidth"].Split(',')[i]);
                                    synu.ShoulderWidth = Convert.ToDecimal(fm["ShoulderWidth"].Split(',')[i]);
                                    synu.Sleecve_Show = fm["FinishedHem_NoFork"].Split(',')[i];
                          
                                    XF_SY_NU_ChiMa.Update(synu);
                                }
 
                            }
                    
                            break;

                        case "XF_KZ_NAN":
                        case "XF_KZ_NU":

                            int count4 = fm["DZ_HipLength_CP"].Split(',').Count();
                            List<XF_KZ_CodeSizeDto> xfkznulist = new List<XF_KZ_CodeSizeDto>();

                            for (int i = 0; i < count4; i++)
                            {
                                int upid = 0;
                                if (!int.TryParse(fm["ID"].Split(',')[i], out upid))
                                {
                                    XF_KZ_CodeSizeDto xfkz = new XF_KZ_CodeSizeDto();
                                    xfkz.Code = fm["Code"].Split(',')[i];
                                    xfkz.DZ_HipLength_CP = Convert.ToDecimal(fm["DZ_HipLength_CP"].Split(',')[i]);
                                    xfkz.SZ_HipLength_CP = Convert.ToDecimal(fm["SZ_HipLength_CP"].Split(',')[i]);
                                    xfkz.Crosspiece = Convert.ToDecimal(fm["Crosspiece"].Split(',')[i]);
                                    xfkz.LegWidth_UnderTheWaves = Convert.ToDecimal(fm["LegWidth_UnderTheWaves"].Split(',')[i]);
                                    xfkz.FrontRise_EvenWaist = Convert.ToDecimal(fm["FrontRise_EvenWaist"].Split(',')[i]);
                                    xfkz.AfterTheWaves_EvenWaist = Convert.ToDecimal(fm["AfterTheWaves_EvenWaist"].Split(',')[i]);
                                    xfkz.NetHip = fm["NetHip"].Split(',')[i];
                                    xfkz.CP_WaistWidth = fm["CP_WaistWidth"].Split(',')[i];

                                    xfkz.NetWaist = fm["NetWaist"].Split(',')[i];
                                    xfkz.Size_Code = fm["Size_Code"];
                                    XF_KZ_Service.Add(xfkz);
                                }
                                else {
                                    XF_KZ_CodeSizeDto xfkz = XF_KZ_Service.GetOne(T => T.Id == upid);
                                    xfkz.Code = fm["Code"].Split(',')[i];
                                    xfkz.DZ_HipLength_CP = Convert.ToDecimal(fm["DZ_HipLength_CP"].Split(',')[i]);
                                    xfkz.SZ_HipLength_CP = Convert.ToDecimal(fm["SZ_HipLength_CP"].Split(',')[i]);
                                    xfkz.Crosspiece = Convert.ToDecimal(fm["Crosspiece"].Split(',')[i]);
                                    xfkz.LegWidth_UnderTheWaves = Convert.ToDecimal(fm["LegWidth_UnderTheWaves"].Split(',')[i]);
                                    xfkz.FrontRise_EvenWaist = Convert.ToDecimal(fm["FrontRise_EvenWaist"].Split(',')[i]);
                                    xfkz.AfterTheWaves_EvenWaist = Convert.ToDecimal(fm["AfterTheWaves_EvenWaist"].Split(',')[i]);
                                    xfkz.NetHip = fm["NetHip"].Split(',')[i];
                                    xfkz.CP_WaistWidth = fm["CP_WaistWidth"].Split(',')[i];

                                    xfkz.NetWaist = fm["NetWaist"].Split(',')[i];
                                     XF_KZ_Service.Update(xfkz);
                                }
                             
                            }
                            int IsKc = 0;

                            for (int i = 0; i < fm.AllKeys.Length; i++)
                            {
                                if (fm.AllKeys[i] == "HK_Height") {
                                    IsKc++;
                                }

                            }
                            if (IsKc > 0)
                            {
                                int hkCount = fm["HK_Height"].Split(',').Count();
                                List<HeightKuChangDto> HKList = new List<HeightKuChangDto>();
                                for (int i = 0; i < hkCount; i++)
                                {
                                    HeightKuChangDto dto = new HeightKuChangDto();
                                    dto.Id = Convert.ToInt32(fm["HK_Id"].Split(',')[i]);
                                    dto.Height = Convert.ToDouble(fm["HK_Height"].Split(',')[i]);
                                    dto.KuChang = Convert.ToDouble(fm["HK_KuChang"].Split(',')[i]);
                                    dto.Size_Code = fm["Size_Code"];
                                    HKList.Add(dto);
                                }
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


        //删除
        [HttpPost]
        public JsonResult DeleteCode(FormCollection fm)
        {
            try
            {

                int Id;
                if (!int.TryParse(fm["Id"], out Id)) {
                    throw new Exception("系统出错：Id格式错误");
                }

                switch (fm["Action"])
                {
                    case "XF_SY_NAN":

                        XF_SY_NAN_ChiMaDto dto = XF_SY_NAN_ChiMa.GetOne(T=>T.Id==Id);
                        dto.IsDeleted = true;
                        XF_SY_NAN_ChiMa.Update(dto);

                        break;
                    case "XF_SY_NU":

                        XF_SY_NU_CodeSizeDto dtonu = XF_SY_NU_ChiMa.GetOne(T => T.Id == Id);
                        dtonu.IsDeleted = true;
                        XF_SY_NU_ChiMa.Update(dtonu);

                        break;
                    case "XF_KZ_NAN":
                    case "XF_KZ_NU":

                        XF_KZ_CodeSizeDto dtokz = XF_KZ_CodeSizeService.GetOne(T => T.Id == Id);
                        dtokz.IsDeleted = true;
                        XF_KZ_CodeSizeService.Update(dtokz);

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
            DataTable table = new DataTable();

            switch (fm["action"])
            {
                case "XF_SY_NAN"://西服上衣  男
                    table = Analysis.Excel_analysis_NAN(Request.Files, "男");
                    return Json(new { state = 1, msg = Ret_Excel(table) }, JsonRequestBehavior.AllowGet);
  
                case "XF_SY_NU":
                    table = Analysis.Excel_analysis_NU(Request.Files, "女");
                    return Json(new { state = 1, msg = Ret_Excel(table) }, JsonRequestBehavior.AllowGet);

                case "XF_KZ_NAN":
                case "XF_KZ_NU":
                    table = Analysis.Excel_trousrs_NAN(Request.Files,out double[] height,out double[] kuchang);
                    return Json(new { state = 1, msg = Ret_Excel_trousrs(table) }, JsonRequestBehavior.AllowGet);
  
                default:
                    break;
            }
 
            return json;
        }
 
  
        //初排处理
        [HttpPost]
        public JsonResult Import(FormCollection fm)
        {

            JsonResult json = null;

            DataTable table = new DataTable();

            string errMsg = "";

            switch (fm["action"])
            {

                case "XF_SY_NAN"://西服上衣  男

                    if (fm["import"] == "false")
                    {

                        table = Analysis.Excel_analysis_NAN(Request.Files, "男");

                        return Json(new { state = 1, msg = Ret_Excel(table) }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {

                        Import_XF_SY(Analysis.Excel_analysis_NAN(Request.Files, "男"), fm["Size_Code"], "男", out errMsg);

                        return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                    }

                case "XF_SY_NU":

                    if (fm["import"] == "false")
                    {

                        table = Analysis.Excel_analysis_NU(Request.Files, "女");

                        return Json(new { state = 1, msg = Ret_Excel_NU(table) }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {

                        Import_XF_SY(Analysis.Excel_analysis_NU(Request.Files, "男"), fm["Size_Code"], "男", out errMsg);

                        return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                    }

                case "XF_KZ_NAN":
                case "XF_KZ_NU":

                    if (fm["import"] == "false")
                    {
                        double[] HeightArrey = null;

                        double[] KuChangArrey = null;

                        table = Analysis.Excel_trousrs_NAN(Request.Files, out   HeightArrey, out  KuChangArrey);

                        return Json(new { state = 1, msg = Ret_Excel_trousrs(table),Height= string.Join(",", HeightArrey),KuChang= string.Join(",", KuChangArrey) }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        double[] HeightArrey = null;

                        double[] KuChangArrey = null;

                        Import_XF_KZ(Analysis.Excel_trousrs_NAN(Request.Files,out HeightArrey,out KuChangArrey), fm["Size_Code"], out errMsg);

                        for (int i = 0; i < HeightArrey.Length; i++)
                        {
                      
                            if (HeightArrey[i] > 0) {
                                HeightKuChangDto dto = new HeightKuChangDto();
                                dto.Size_Code = fm["Size_Code"];
                                dto.Height = HeightArrey[i];
                                dto.KuChang = KuChangArrey[i];
                                HeightKuChangService.Add(dto);
                            }
 
                        }

                        return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                    }
 
                default:
                    break;
            }

            return json;
        }
 
        #region Import

        //上传西服裤子尺码表
        public bool Import_XF_KZ(DataTable table, string Size_Code, out string errmsg)
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

                }

                foreach (DataRow item in table.Rows)
                {
                    XF_KZ_CodeSizeDto tszie = new XF_KZ_CodeSizeDto();
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
                    tszie.Height = Convert.ToDecimal(item["Height"]);
                    tszie.LongPants = item["LongPants"].ToString();
                    tszie.NetWaist = item["NetWaist"].ToString();
                    tszie.Size_Code = Size_Code;
                    tszie.CreateDateTime = DateTime.Now;
                    tszie.Status = 1;
                    tszie_list.Add(tszie);
                }

                XF_KZ_Service.Add(tszie_list);
 
                errmsg = "";
                return true;

            }
            catch (Exception ex)
            {
 
                errmsg = ex.Message;
                return false;

            }
        }

        //上传西服上衣尺码表
        public bool Import_XF_SY(DataTable table, string size_code, string gender, out string errmsg)
        {

            try
            {

                List<XF_SY_NAN_ChiMaDto> list = new List<XF_SY_NAN_ChiMaDto>();

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
                }

                if (gender == "男")
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        XF_SY_NAN_ChiMaDto cs = new XF_SY_NAN_ChiMaDto();

                        cs.Height = Convert.ToDecimal(table.Rows[i]["Height"]);

                        cs.FrontLength = table.Rows[i]["FrontLength"] + "";

                        cs.NetBust = table.Rows[i]["NetBust"] + "";

                        cs.FinishedBust = Convert.ToDecimal(table.Rows[i]["FinishedBust"]);

                        cs.InWaist = Convert.ToDecimal(table.Rows[i]["InWaist"]);

                        cs.FinishedHem_NoFork = Convert.ToDecimal(table.Rows[i]["FinishedHem_NoFork"]);

                        cs.FinishedHem_SplitEnds = Convert.ToDecimal(table.Rows[i]["FinishedHem_SplitEnds"]);

                        cs.ShoulderWidth = Convert.ToDecimal(table.Rows[i]["ShoulderWidth"]);

                        cs.Size_Code = size_code;

                        cs.Sleecve_Show = table.Rows[i]["FK_Sleeve_ID"] + "";

                        cs.CreateDateTime = DateTime.Now;

                        cs.IsDeleted = false;

                        cs.Status = 1;

                        list.Add(cs);

                    }

                    XF_SY_NAN_ChiMa.Add(list);
                }
                else
                {

                    List<XF_SY_NU_CodeSizeDto> list_NU = new List<XF_SY_NU_CodeSizeDto>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        XF_SY_NU_CodeSizeDto cs = new XF_SY_NU_CodeSizeDto();

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

                        list_NU.Add(cs);

                    }

                    XF_SY_NU_ChiMa.Add(list_NU);
                }
 
                errmsg = "";
                return true;

            }
            catch (Exception ex)
            {

                errmsg = ex.Message;
                return false;

            }
        }

        #endregion
 
      
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

                decimal Height = 0;

                if (!decimal.TryParse(table.Rows[i]["Height"] + "", out Height))
                {
                    Height = 0;
                }

                ts.NetHip = table.Rows[i]["NetHip"] + "";

                ts.CP_WaistWidth = table.Rows[i]["CP_WaistWidth"] + "";

                ts.Height = Height;

                ts.LongPants = table.Rows[i]["LongPants"] + "";

                ts.NetWaist = table.Rows[i]["NetWaist"] + "";

                cslist.Add(ts);

            }

            return cslist;

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

                            return Json(new { state = 1, msg = XF_SY_NAN_ChiMa.Query(T => T.Size_Code == Code && T.IsDeleted == false, O => O.Id, false) }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":
                            return Json(new { state = 1, msg = XF_SY_NU_ChiMa.Query(T => T.Size_Code == Code && T.IsDeleted == false, O => O.Id, false) }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NAN":
                        case "XF_KZ_NU":

                            return Json(new { state = 1, msg = XF_KZ_CodeSizeService.Query(T => T.Size_Code == Code&&T.IsDeleted==false, O => O.Id, false),HeightKuChang= HeightKuChangService.Query(T=>T.Size_Code==Code&&T.IsDeleted==false,O=>O.Id,false) }, JsonRequestBehavior.AllowGet);

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
 
                            List<XF_SY_NAN_ChiMaDto> XF_SY_NAN = db.Database.GetList<XF_SY_NAN_ChiMaDto>("select Size_Code from XF_SY_NAN_ChiMa where Status=0 Group By Size_Code");
                            return Json(new { state = 1, msg = XF_SY_NAN }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":
                            List<XF_SY_NU_CodeSizeDto> XF_SY_NU = db.Database.GetList<XF_SY_NU_CodeSizeDto>("select Size_Code from XF_SY_NU_CodeSize where Status=0 Group By Size_Code");
                          

                            return Json(new { state = 1, msg = XF_SY_NU }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NAN"://西服裤子
                        case "XF_KZ_NU":
      
                            List<XF_KZ_CodeSizeDto> XF_KZ = db.Database.GetList<XF_KZ_CodeSizeDto>("select Size_Code from XF_KZ_CodeSize where Status=0 Group By Size_Code");

                            return Json(new { state = 1, msg = XF_KZ }, JsonRequestBehavior.AllowGet);
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