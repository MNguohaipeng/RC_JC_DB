
using JuCheap.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlSugar;
using JuCheap.Service.Dto;
namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class SizeController : Controller
    {
        // GET: Adm/Size
        public ActionResult Import()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Examine()
        {
            using (var db = SugarDao.GetInstance())
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
            using (var db = SugarDao.GetInstance())
                try
                {
                    List<Examine> List = new List<Examine>();

                    List<Examine> list = new List<Examine>();
                    switch (Action)
                    {
                        case "XF_SY_NAN":
                            List<XF_SY_NAN_CodeSize> qudate = db.Queryable<XF_SY_NAN_CodeSize>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_SY_NAN_CodeSize>("Size_Code, MAX(CreateDateTime) as   CreateDateTime").ToList();

                            foreach (XF_SY_NAN_CodeSize item in qudate)
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":
                            List<XF_SY_NU_CodeSize> XF_SY_NU = db.Queryable<XF_SY_NU_CodeSize>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_SY_NU_CodeSize>("Size_Code, MAX(CreateDateTime) as   CreateDateTime").ToList();

                            foreach (XF_SY_NU_CodeSize item in XF_SY_NU)
                            {
                                Examine ex = new Examine();

                                ex.CreateTime = item.CreateDateTime.ToString("yyyy-MM-dd");
                                ex.Code = item.Size_Code;
                                list.Add(ex);
                            }

                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);

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
        //修改尺码表状态
        public JsonResult UpdateState(string Code,string state, string Action)
        {
            using (var db = SugarDao.GetInstance())
                try
                {
                    switch (Action)
                    {
                        case "XF_SY_NAN":
                            db.Update<XF_SY_NAN_CodeSize>(new { status = state }, it => it.Size_Code == Code);

                            return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":

                            db.Update<XF_SY_NU_CodeSize>(new { status = state }, it => it.Size_Code == Code);  

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
            using (var db = SugarDao.GetInstance())
                try
                {
                    switch (Action)
                    {
                        case "XF_SY_NAN":
                            List<XF_SY_NAN_CodeSize> list = db.Queryable<XF_SY_NAN_CodeSize>().Where(T => T.Status == 1 && T.Size_Code == Size_Code).ToList();
                            return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);
                        case "XF_SY_NU":
                            return Json(new { state = 1, msg = db.Queryable<XF_SY_NU_CodeSize>().Where(T => T.Status == 1 && T.Size_Code == Size_Code).ToList() }, JsonRequestBehavior.AllowGet);
                        default:
                            throw new Exception("系统出错：没有对应的Action");
          
                    }

                }
                catch (Exception ex)
                {
                    return Json(new { state = -1, msg = ex.Message}, JsonRequestBehavior.AllowGet);
                }


        }

        //更新
        [HttpPost]
        public JsonResult UpdateCode(FormCollection fm)
        {
            using (var db = SugarDao.GetInstance())
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

                                db.Update<XF_SY_NAN_CodeSize, int>(dic, upid);
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

                                db.Update<XF_SY_NU_CodeSize, int>(dic2, upid);
                                dic2.Clear();
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

        #region 解析
        //西服尺码表解析
        public static object Ret_Excel(DataTable table)
        {



            List<Service.Dto.XF_SY_NAN_CodeSize> cslist = new List<Service.Dto.XF_SY_NAN_CodeSize>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                decimal ty = 0;
                Service.Dto.XF_SY_NAN_CodeSize cs = new Service.Dto.XF_SY_NAN_CodeSize();
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



            List<Service.Dto.XF_SY_NU_CodeSize> cslist = new List<Service.Dto.XF_SY_NU_CodeSize>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                decimal ty = 0;
                Service.Dto.XF_SY_NU_CodeSize cs = new Service.Dto.XF_SY_NU_CodeSize();
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
        #endregion
        #endregion

        #region 管理
        // GET: Adm/Size
        public ActionResult Manage()
        {
      
            return View();
        }
        [HttpPost]
        public JsonResult Manage(string Code,string Action)
        {
            using (var db=SugarDao.GetInstance())
                try
                {

                    switch (Action)
                    {
                        case "XF_SY_NAN"://西服上衣  男
                            return Json(new { state = 1, msg = db.Queryable<XF_SY_NAN_CodeSize>().Where(it => it.Size_Code == Code.ObjToString()).ToList() }, JsonRequestBehavior.AllowGet);


                        case "XF_SY_NU":
 
                            return Json(new { state = 1, msg = db.Queryable<XF_SY_NU_CodeSize>().Where(it => it.Size_Code == Code.ObjToString()).ToList() }, JsonRequestBehavior.AllowGet);
                 
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
            using (var db=SugarDao.GetInstance())
                try
                {

                    switch (Action)
                    {
                        case "XF_SY_NAN"://西服上衣  男

                            List<XF_SY_NAN_CodeSize> XF_SY_NAN = db.Queryable<XF_SY_NAN_CodeSize>().Where(c => c.Status == 0).GroupBy(it => it.Size_Code).Select<XF_SY_NAN_CodeSize>("Size_Code").ToList();


                            return Json(new { state = 1, msg = XF_SY_NAN }, JsonRequestBehavior.AllowGet);

                        case "XF_SY_NU":

                            List<XF_SY_NU_CodeSize> XF_SY_NU = db.Queryable<XF_SY_NU_CodeSize>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_SY_NU_CodeSize>("Size_Code").ToList();


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