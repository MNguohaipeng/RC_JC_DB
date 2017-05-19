using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using JuCheap.Core.Extentions;
using JuCheap.Service.Abstracts;
using JuCheap.Service.Dto;
using JuCheap.Service.Enum;
using JuCheap.Core;
using System.Data;
using static JuCheap.Core.SqlSugerHelper;
using SqlSugarRepository;
using System.Text.RegularExpressions;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class HandleDataController : AdmBaseController
    {
        public IDCL_DataService DCL_DataService { get; set; }

        public ISleeveService SleeveService { get; set; }

        public IXF_SY_NAN_ChiMaService XF_SY_NAN_ChiMaService { get; set; }

        public IXF_KZ_CodeSizeService XF_KZ_CodeSizeService { get; set; }

        // GET: Adm/HandleData
        public ActionResult Handle(int moudleId, int menuId, int btnId)
        {
            return View();
        }

        [HttpPost]
        public JsonResult ImportHandleData(FormCollection fm)
        {
            using (MyRepository db = new MyRepository())
                try
                {
                    DataTable GDData = db.Database.GetDataTable(string.Format("select * from pytbulkgh where GDH='{0}'", fm["GDH"]));

                    List<DCL_DataDto> DCLList = new List<DCL_DataDto>();

                    for (int i = 0; i < GDData.Rows.Count; i++)
                    {
                        #region 将数据处理存放  准备导入到待处理库

                        DCL_DataDto dl = new DCL_DataDto();

                        dl.Orderid = GDData.Rows[i]["ORDERCODE"].ToString();

                        dl.Option = Convert.ToInt32(GDData.Rows[i]["ORDERXC"]);

                        dl.Name = GDData.Rows[i]["Name"].ToString();

                        dl.ReCodeSize = GDData.Rows[i]["SIZECODE"].ToString();

                        dl.Number = Convert.ToInt32(GDData.Rows[i]["SL"]);

                        dl.Note = "";

                        dl.GDH = fm["GDH"];

                        dl.Gender = GDData.Rows[i]["XB"].ToString();

                        DCLList.Add(dl);

                        #endregion

                    }

                    #region 导入到待处理库

                    DCL_DataService.Add(DCLList);

                    #endregion

                    return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json(new { state = 0, msg = ex.Message }, JsonRequestBehavior.AllowGet);

                }

        }

        [HttpPost]
        public JsonResult Handle(FormCollection fm)
        {

            using (MyRepository db = new MyRepository())
                try
                {

                    List<XF_SY_NAN_ChiMaDto> list = null;

                    #region 获取待处理数据

                    string GDH = fm["GDH"];

                    var GDData = DCL_DataService.Query(T => T.GDH == GDH, O => O.Id, false);

                    #endregion

                    string Action ="";

                    List<DCL_DataDto> DCLList = new List<DCL_DataDto>();

                    List<handleBool> boollist = new List<handleBool>();

                    DataTable table = new DataTable();

                    table.Columns.Add("姓名");
                    table.Columns.Add("归码后尺码");
                    table.Columns.Add("身高");
                    table.Columns.Add("数量");
                    table.Columns.Add("单褶臀围");
                    table.Columns.Add("双褶臀围");
                    table.Columns.Add("腰");
                    table.Columns.Add("备注");
                    DataRow rowsTitle = table.NewRow();

                    rowsTitle["姓名"] = "出排尺码";

                    table.Rows.Add(rowsTitle);

                    foreach (DCL_DataDto item in GDData)
                    {

                        boollist.Add(isSYorKZ(item.ReCodeSize.ToString()));

                        int trueCount = 0;

                        foreach (handleBool boolitem in boollist)
                        {
                            if (boolitem.sizezz1)
                            {
                                Action = "T_XF_SY_" + gender(item.Gender.ToString());
                                trueCount++;
                            }

                            if (boolitem.sizezz2)
                            {
                                Action = "T_XF_KZ_" + gender(item.Gender.ToString());
                                trueCount++;
                            }

                            if (boolitem.sizezz3)
                            {
                                Action = "XF_SY_" + gender(item.Gender.ToString());
                                trueCount++;
                            }

                            if (boolitem.sizezz4)
                            {
                                Action = "XF_KZ_" + gender(item.Gender.ToString());
                                trueCount++;
                            }

                            if (trueCount > 1)
                            {

                                throw new Exception("数据出错，出现多个匹配代码。");
                            }
                            else {
                                trueCount = 0;
                            }

                        }
                        switch (Action)
                        {

                            case "XF_SY_NAN":
                                List<HanderDataForXF_SYDto> SYlist = new List<HanderDataForXF_SYDto>();

                                foreach (DCL_DataDto itemsynan in GDData)
                                {
                                    decimal recodenan = Convert.ToDecimal(item.ReCodeSize.ToString().Split('/')[0]);

                                    string NetBustNan = item.ReCodeSize.ToString().Split('/')[1];

                                    XF_SY_NAN_ChiMaDto dtonan = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == recodenan && T.NetBust == NetBustNan && T.Size_Code == fm["Size_Code"]);

                                    SleeveDto sledto = SleeveService.GetOne(T => T.FK_CoatSize_ID == dtonan.Id && T.Code == dtonan.NetBust.Substring(dtonan.NetBust.Length - 1, 1));

                                    if (dtonan != null)
                                    {
                                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();
                                        sy.Height = dtonan.Height;
                                        sy.RtnQCode = item.ReCodeSize.ToString();
                                        sy.OrderCode = item.Orderid.ToString();
                                        sy.Name = item.Name.ToString();
                                        sy.RtnHCode = NetBustNan;
                                        sy.Number = item.Number;
                                        sy.Yichang = Convert.ToDecimal(dtonan.FrontLength);
                                        sy.Bust = dtonan.FinishedBust;
                                        sy.Sleeve = sledto.Length;
                                        SYlist.Add(sy);
                                    }
                                }


                                if (list.Count > 0)
                                {
                                    return Json(new { state = 1, msg = SYlist }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    throw new Exception("解析失败，请检查尺码表编号是否对应");
                                }

                            case "XF_SY_NU":
                                break;
                            case "XF_KZ_NAN":
                                break;
                            case "XF_KZ_NU":
                                Web.GenerateExcel gx = new GenerateExcel();
                                DataRow row = gx.GenerateExcelForXF_SY_NAN(item, table, GDH);
                                if (row != null)
                                {
                                    table.Rows.Add(row);
                                }

                                break;
                            default:
                                throw new Exception("没有对应的处理程序。");

                        }

                    }

                    if (table.Rows.Count > 0)
                    {

                        ExcelHelper.BuildExcel(table);

                        return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        throw new Exception("解析失败，请检查尺码表编号是否对应");
                    }

                    throw new Exception("系统错误 函数未中断。");

                }
                catch (Exception ex)
                {
                    return Json(new { state = 0, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

        }

        //尺码表编号下拉
        public JsonResult SizeCodeSelect(string Action)
        {

            try
            {

                switch (Action)
                {
                    case "XF_SY_NAN"://西服上衣  男


                        var xxx = XF_SY_NAN_ChiMaService.Query(T => 1 == 1, Q => Q.Size_Code).GroupBy(x => new { x.Size_Code }).ToList();
                        return Json(new { state = 1, msg = xxx }, JsonRequestBehavior.AllowGet);

                    case "XF_SY_NU":

                    //List<XF_SY_NU_CodeSize> XF_SY_NU = db.Queryable<XF_SY_NU_CodeSize>().Where(c => c.Status == 1).GroupBy(it => it.Size_Code).Select<XF_SY_NU_CodeSize>("Size_Code").ToList();


                    //return Json(new { state = 1, msg = XF_SY_NU }, JsonRequestBehavior.AllowGet);

                    default:
                        throw new Exception("系统出错：没有对应的Action");

                }



            }
            catch (Exception ex)
            {
                return Json(new { state = -1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //验证数据格式  上衣  裤子  特殊上衣  特殊裤子
        public handleBool isSYorKZ(string size)
        {

            string TSY = "[0-9]{2,3}/[0-9A-z]{2,4}/[0-9]{2,3}[\u4e00-\u9fa5]{2,3}";//特殊上衣

            string TKZ = "[0-9]{2,3}/[0-9A-z]{2,4}[\u4e00-\u9fa5]{2,3}";//特殊裤子

            string PTSY = "[0-9]{2,3}/[0-9A-z]{2,4}/[0-9]{2,3}";//普通上衣

            string PTKZ = "[0-9]{2,3}/[0-9A-z]{2,4}";//普通裤子

            bool[] zzArrey = new bool[4];

            handleBool hbool = new handleBool();

            Regex RegexTSY = new Regex(TSY);
            hbool.sizezz1 = RegexTSY.IsMatch(size);

            Regex RegexTKZ = new Regex(TKZ);
            hbool.sizezz2 = RegexTKZ.IsMatch(size);

            Regex RegexPTSY = new Regex(PTSY);
            hbool.sizezz3 = RegexPTSY.IsMatch(size);

            Regex RegexPTKZ = new Regex(PTKZ);
            hbool.sizezz4 = RegexPTKZ.IsMatch(size);

            return hbool;
        }

        //男  女  转换
        public string gender(string gd)
        {
            string retst = "";
            switch (gd)
            {
                case "男":
                    retst = "NAN";
                    break;
                case "女":
                    retst = "NU";
                    break;
                default:
                    retst = "ERROR";
                    break;
            }
            return retst;
        }

        #region Ajax

        public JsonResult GetList(int moudleId, int menuId, int btnId)
        {
            var queryBase = new QueryBase
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                Draw = Request["draw"].ToInt(),
                SearchKey = Request["keywords"],

            };
            string Dcl_sizecode = Request["dcl_sizecode"];


            Expression<Func<DCL_DataDto, bool>> exp = item => !item.IsDeleted && item.GDH == Dcl_sizecode;
            if (!queryBase.SearchKey.IsBlank())
                exp = exp.And(item => item.Name.Contains(queryBase.SearchKey));

            var dto = DCL_DataService.GetWithPages(queryBase, exp, Request["orderBy"], Request["orderDir"]);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public class handleBool {
            public bool sizezz1 { get; set; }
            public bool sizezz2 { get; set; }
            public bool sizezz3 { get; set; }
            public bool sizezz4 { get; set; }

        }


    }
}