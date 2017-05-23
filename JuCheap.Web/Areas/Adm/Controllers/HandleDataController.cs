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

        #region  创建接口对象
        public IDCL_DataService DCL_DataService { get; set; }

        public ISleeveService SleeveService { get; set; }

        public IXF_SY_NAN_ChiMaService XF_SY_NAN_ChiMaService { get; set; }

        public IXF_SY_NU_CodeSizeService XF_SY_NU_ChiMaService { get; set; }

        public IXF_KZ_CodeSizeService XF_KZ_CodeSizeService { get; set; }

        public IHanderDataForXF_SYService HanderDataForXF_SYService { get; set; }

        public IHanderDataForXF_KZService HanderDataForXF_KZService { get; set; }

        public DictionariesService DictionariesService { get; set; }

        #endregion

        #region Load
        // GET: Adm/HandleData
        public ActionResult Handle()
        {
            return View();
        }

        #endregion

        [HttpPost]
        public JsonResult ImportHandleData(FormCollection fm)
        {
            using (MyRepository db = new MyRepository())
                try
                {
                    DataTable GDData = db.Database.GetDataTable(string.Format("select * from pytbulkgh where GDH='{0}'", fm["GDH"]));

                    List<DCL_DataDto> DCLList = new List<DCL_DataDto>();

                    string GDH = fm["GDH"];

                    #region 删除相同工单号的数据
                    //var deleteDate = DCL_DataService.Query(T => T.GDH == GDH, O => O.Index, false);
                    //foreach (var item in deleteDate)
                    //{
                    //    DCL_DataService.Delete(item.Id);
                    //}
                    #endregion

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

                        dl.SizeCode = GDData.Rows[i]["GGBH"].ToString();

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
        public JsonResult Handle(string GDH, string SizeCode)
        {

            using (MyRepository db = new MyRepository())
                try
                {

                    List<XF_SY_NAN_ChiMaDto> list = null;

                    #region 获取待处理数据

                    List<DCL_DataDto> GDData = DCL_DataService.Query(T => T.GDH == GDH, O => O.Id, false);

                    #endregion

                    #region 开始处理数据

                    string Action = isSYorKZTo(GDData);

                    DeleteForHanderData(GDH, Action);

                    if (isSYorKZ(GDData) == "")
                        throw new Exception("此工单号数据不统一或是没有对应的处理程序。");

                    int index = 1;

                    decimal Height;//身高

                    string A02;//第二个斜杠内的值

                    string A02R;//第二个斜杠内的值最后一位

                    string A02S;//第二个斜杠内的值的前几位

                    string A03;//第三个斜杠内的值

                    foreach (DCL_DataDto item in GDData)
                    {

                        string isT = "[\u4e00-\u9fa5]";
                        Regex Regex = new Regex(isT);
                        bool xxxxx = Regex.IsMatch(item.ReCodeSize.ToString());
                        if (Regex.IsMatch(item.ReCodeSize.ToString()))//是否是特殊数据
                        {
                            switch (Action)
                            {

                                case "XF_SY_NAN":
                                    break;
                                case "XF_SY_NU":
                                    //Match

                                    List<HanderDataForXF_SYDto> SYlist = new List<HanderDataForXF_SYDto>();

                                    string NetBustNan = item.ReCodeSize.ToString().Split('/')[1];

                                    SizeCode = GDH;//TO DO

                                    Height = Convert.ToDecimal(item.ReCodeSize.ToString().Split('/')[0]);

                                    A02 = item.ReCodeSize.ToString().Split('/')[1];

                                    A02R = A02.Substring(A02.Length - 1, 1);

                                    A02S = A02.Substring(0, A02.Length - 1);

                                    A03 = item.ReCodeSize.ToString().Split('/')[2];

                                    XF_SY_NAN_ChiMaDto dtonan = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == Height && T.NetBust == A02 && T.Size_Code == SizeCode);

                                    SleeveDto sledto = SleeveService.GetOne(T => T.FK_CoatSize_ID == dtonan.Id && T.Code == dtonan.NetBust.Substring(dtonan.NetBust.Length - 1, 1));

                                    if (dtonan != null)
                                    {

                                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();

                                        sy.Height = dtonan.Height;

                                        sy.RtnQCode = item.ReCodeSize.ToString();

                                        sy.OrderCode = item.Orderid.ToString();

                                        sy.Name = item.Name.ToString();

                                        sy.RtnHCode = A02;

                                        sy.Number = item.Number;

                                        sy.Yichang = Convert.ToDecimal(dtonan.FrontLength);

                                        #region 处理袖长
                                        string[] Sleecve = dtonan.Sleecve_Show.Split(' ');
                                        for (int i = 0; i < Sleecve.Length; i++)
                                        {
                                            if (Sleecve[i].IndexOf(';') > 0)
                                            {
                                                if (Sleecve[i].Split(';')[0] == A03)
                                                {
                                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);
                                                }
                                            }
                                            else if (Sleecve[i].IndexOf(':') > 0)
                                            {
                                                if (Sleecve[i].Split(';')[0] == A03)
                                                {
                                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);
                                                }
                                            }
                                        }
                                        #endregion

                                        sy.Bust = dtonan.FinishedBust;

                                        sy.Index = index;

                                        HanderDataForXF_SYService.Add(sy);

                                    }
       
                                    break;
                                case "XF_KZ_NAN":
                                case "XF_KZ_NU":
                                    Height = Convert.ToDecimal(item.ReCodeSize.ToString().Split('/')[0]);

                                    A02 = item.ReCodeSize.ToString().Split('/')[1];

                                    A02R = A02.Substring(A02.Length - 1, 1);

                                    A02S = A02.Substring(0, A02.Length - 1);

                                    XF_KZ_CodeSizeDto t_dto = XF_KZ_CodeSizeService.GetOne(T => T.CP_WaistWidth.Contains(A02S) && T.Code == A02R && T.Size_Code == GDH);//TO DO

                                    if (t_dto != null)
                                    {

                                        HanderDataForXF_KZDto sy = new HanderDataForXF_KZDto();

                                        sy.Height = Convert.ToDecimal(item.ReCodeSize.Split('/')[0]);

                                        sy.RtnQCode = item.ReCodeSize.ToString();

                                        sy.OrderCode = item.Orderid.ToString();

                                        sy.Name = item.Name.ToString();

                                        sy.Number = item.Number;

                                        sy.SZ_Hipline = t_dto.SZ_HipLength_CP;

                                        sy.DZ_Hipline = t_dto.DZ_HipLength_CP;

                                        sy.waistWidth = Convert.ToDecimal(A02S);

                                        sy.Index = index;

                                        sy.GDH = item.GDH;

                                        HanderDataForXF_KZService.Add(sy);

                                    }
                                    break;
                            }
                        }
                        else
                        {

                            switch (Action)
                            {

                                case "XF_SY_NAN":

                                    List<HanderDataForXF_SYDto> SYlist = new List<HanderDataForXF_SYDto>();

                                    string NetBustNan = item.ReCodeSize.ToString().Split('/')[1];

                                    SizeCode = GDH;//TO DO

                                    Height = Convert.ToDecimal(item.ReCodeSize.ToString().Split('/')[0]);

                                    A02 = item.ReCodeSize.ToString().Split('/')[1];

                                    A02R = A02.Substring(A02.Length - 1, 1);

                                    A02S = A02.Substring(0, A02.Length - 1);

                                    A03 = item.ReCodeSize.ToString().Split('/')[2];

                                    XF_SY_NAN_ChiMaDto dtonan = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == Height && T.NetBust == A02 && T.Size_Code == SizeCode);

                                    SleeveDto sledto = SleeveService.GetOne(T => T.FK_CoatSize_ID == dtonan.Id && T.Code == dtonan.NetBust.Substring(dtonan.NetBust.Length - 1, 1));

                                    if (dtonan != null)
                                    {

                                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();

                                        sy.Height = dtonan.Height;

                                        sy.RtnQCode = item.ReCodeSize.ToString();

                                        sy.OrderCode = item.Orderid.ToString();

                                        sy.Name = item.Name.ToString();

                                        sy.RtnHCode = A02;

                                        sy.Number = item.Number;

                                        sy.Yichang = Convert.ToDecimal(dtonan.FrontLength);

                                        #region 处理袖长
                                        string[] Sleecve = dtonan.Sleecve_Show.Split(' ');
                                        for (int i = 0; i < Sleecve.Length; i++)
                                        {
                                            if (Sleecve[i].IndexOf(';') > 0)
                                            {
                                                if (Sleecve[i].Split(';')[0] == A03)
                                                {
                                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);
                                                }
                                            }
                                            else if (Sleecve[i].IndexOf(':') > 0)
                                            {
                                                if (Sleecve[i].Split(';')[0] == A03)
                                                {
                                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);
                                                }
                                            }
                                        }
                                        #endregion

                                        sy.Bust = dtonan.FinishedBust;

                                        sy.Index = index;

                                        HanderDataForXF_SYService.Add(sy);

                                    }
                                    break;
                                case "XF_SY_NU":

                                    List<HanderDataForXF_SYDto> SYNUlist = new List<HanderDataForXF_SYDto>();

                                    decimal recodenu = Convert.ToDecimal(item.ReCodeSize.ToString().Split('/')[0]);

                                    string NetBustNu = item.ReCodeSize.ToString().Split('/')[1];

                                    XF_SY_NU_CodeSizeDto dtonu = XF_SY_NU_ChiMaService.GetOne(T => T.Height == recodenu && T.NetBust == NetBustNu && T.Size_Code == SizeCode);

                                    SleeveDto sledtonu = SleeveService.GetOne(T => T.FK_CoatSize_ID == dtonu.Id && T.Code == dtonu.NetBust.Substring(dtonu.NetBust.Length - 1, 1));

                                    if (sledtonu != null)
                                    {

                                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();

                                        sy.Height = dtonu.Height;

                                        sy.RtnQCode = item.ReCodeSize.ToString();

                                        sy.OrderCode = item.Orderid.ToString();

                                        sy.Name = item.Name.ToString();

                                        sy.RtnHCode = NetBustNu;

                                        sy.Number = item.Number;

                                        sy.Yichang = Convert.ToDecimal(dtonu.FrontLength);

                                        sy.Bust = dtonu.FinishedBust;

                                        sy.Sleeve = sledtonu.Length;

                                        sy.Index = index;

                                        HanderDataForXF_SYService.Add(sy);

                                    }
                                    break;
                                case "XF_KZ_NAN":
                                case "XF_KZ_NU":

                                    decimal height_kz_nu = Convert.ToDecimal(item.ReCodeSize.ToString().Split('/')[0]);

                                    string a02 = item.ReCodeSize.ToString().Split('/')[1];

                                    string temp = a02.Substring(a02.Length - 1, 1);

                                    string Code = a02.Substring(0, a02.Length - 1);

                                    XF_KZ_CodeSizeDto dto = XF_KZ_CodeSizeService.GetOne(T => T.CP_WaistWidth.Contains(Code) && T.Code == temp && T.Size_Code == GDH);//TO DO

                                    if (dto != null)
                                    {

                                        HanderDataForXF_KZDto sy = new HanderDataForXF_KZDto();

                                        sy.Height = Convert.ToDecimal(item.ReCodeSize.Split('/')[0]);

                                        sy.RtnQCode = item.ReCodeSize.ToString();

                                        sy.OrderCode = item.Orderid.ToString();

                                        sy.Name = item.Name.ToString();

                                        sy.Number = item.Number;

                                        sy.SZ_Hipline = dto.SZ_HipLength_CP;

                                        sy.DZ_Hipline = dto.DZ_HipLength_CP;

                                        sy.waistWidth = Convert.ToDecimal(Code);

                                        sy.Index = index;

                                        sy.GDH = item.GDH;

                                        HanderDataForXF_KZService.Add(sy);


                                    }
                                    break;
                                default:
                                    throw new Exception("没有对应的处理程序。");

                            }
                            index++;
                        }
                    }
                    #endregion
                    switch (Action)
                    {

                        case "XF_SY_NAN":
                        case "XF_SY_NU":
                            return Json(new { state = 1, msg = HanderDataForXF_SYService.Query(T => T.GDH == GDH, o => o.Index, false), action = "HanderDataForXF_SY" }, JsonRequestBehavior.AllowGet);

                        case "XF_KZ_NAN":
                        case "XF_KZ_NU":
                            return Json(new { state = 1, msg = HanderDataForXF_KZService.Query(T => T.GDH == GDH, o => o.Index, false), action = "HanderDataForXF_KZ" }, JsonRequestBehavior.AllowGet);
                    }

                    throw new Exception("系统出错:没有中断方法。");

                }
                catch (Exception ex)
                {
                    return Json(new { state = 0, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

        }

        [HttpPost]
        public JsonResult GetListForHanderData(string GDH)
        {
            try
            {

                if (HanderDataForXF_SYService.GetOne(T => T.GDH == GDH) != null)
                {

                    return Json(new { state = 1, msg = HanderDataForXF_SYService.Query(T => T.GDH == GDH, O => O.Index, false) }, JsonRequestBehavior.AllowGet);

                }
                else if (HanderDataForXF_KZService.GetOne(T => T.GDH == GDH) != null)
                {

                    return Json(new { state = 1, msg = HanderDataForXF_KZService.Query(T => T.GDH == GDH, O => O.Index, false) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception("系统出错：数据库中找不到对应数据。");
                }

            }
            catch (Exception ex)
            {
                return Json(new { state = 0, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public void DeleteForHanderData(string GDH, string Action)
        {
            try
            {

                #region 删除重复数据
                switch (Action)
                {
                    case "XF_SY_NAN":
                    case "XF_SY_NU":
                        var SY_NU = HanderDataForXF_SYService.Query(T => T.GDH == GDH, O => O.Index, false);
                        foreach (var item in SY_NU)
                        {
                            HanderDataForXF_SYService.Delete(item.Id);
                        }
                        break;
                    case "XF_KZ_NAN":
                    case "XF_KZ_NU":
                        var KZ_NU = HanderDataForXF_KZService.Query(T => T.GDH == GDH, O => O.Index, false);
                        foreach (var item in KZ_NU)
                        {
                            HanderDataForXF_KZService.Delete(item.Id);
                        }
                        break;

                }
                #endregion
            }
            catch (Exception)
            {

                throw;
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
        public string isSYorKZ(List<DCL_DataDto> data)
        {
            try
            {
                string[] Zz = new string[4];

                Zz[0] = @"[\u4e00-\u9fa5]\d{2,3}/\w{2,4}/[A-Z_a-z][\u4e00-\u9fa5]{2,4}\+\d+";//特殊上衣

                Zz[1] = "[0-9]{2,3}/[0-9A-z]{2,4}[\u4e00-\u9fa5]{2,3}";//特殊裤子

                Zz[2] = @"^[0-9]{2,3}/\w{2,4}/[A-Z_a-z]$";//普通上衣

                Zz[3] = "^[0-9]{2,3}/[0-9A-z]{2,4}$";//普通裤子

                string action = "";

                handleBool hbool = new handleBool();

                foreach (var item in data)
                {
                    for (int i = 0; i < Zz.Length; i++)
                    {

                        Regex Regex = new Regex(Zz[i]);

                        string gender = "";
                        string SizeCode = item.SizeCode;
                        if (!string.IsNullOrEmpty(SizeCode))
                        {
                            if (XF_SY_NAN_ChiMaService.GetOne(T => T.Size_Code == SizeCode) != null)
                            {
                                gender = "NAN";
                            }
                            else if (XF_SY_NU_ChiMaService.GetOne(T => T.Size_Code == SizeCode) != null)
                            {
                                gender = "NU";
                            }
                            else if (XF_KZ_CodeSizeService.GetOne(T => T.Size_Code == SizeCode) != null)
                            {
                                gender = "NAN";
                            }
                            else
                            {
                                throw new Exception("没有对应的尺码数据。");
                            }
                        }
                        else
                        {
                            throw new Exception("尺码表编号不能为空。");
                        }

                        if (Regex.IsMatch(item.ReCodeSize))
                        {

                            string lisaction = "";

                            switch (i)
                            {
                                case 0://特殊上衣
                                    action = "T_XF_SY_" + gender;
                                    lisaction = "T_XF_SY_" + gender;
                                    break;
                                case 1://特殊裤子
                                    action = "T_XF_KZ_" + gender; ;
                                    lisaction = "T_XF_KZ_" + gender;
                                    break;
                                case 2://普通上衣
                                    action = "XF_SY_" + gender;
                                    lisaction = "XF_SY_" + gender;
                                    break;
                                case 3://普通裤子
                                    action = "XF_KZ_" + gender;
                                    lisaction = "XF_KZ_" + gender;
                                    break;
                                default:
                                    action = "";
                                    break;
                            }

                            if (action != lisaction && action != "")
                            {
                                return "";
                            }

                        }
                    }
                }
                return action;
            }
            catch (Exception)
            {
                throw;
            }

        }

        //验证数据格式  上衣  裤子  特殊上衣  特殊裤子
        public string isSYorKZTo(List<DCL_DataDto> data)
        {
            try
            {
                string[] Zz = new string[2];


                Zz[0] = "^[0-9]{2,3}/[0-9A-z]{2,4}$";//普通裤子
                Zz[1] = @"[0-9]{2,3}/\w{2,4}/[A-Z_a-z]";//普通上衣



                string action = "";

                handleBool hbool = new handleBool();

                foreach (var item in data)
                {
                    for (int i = 0; i < Zz.Length; i++)
                    {

                        Regex Regex = new Regex(Zz[i]);

                        string gender = "";
                        string SizeCode = item.SizeCode;
                        if (!string.IsNullOrEmpty(SizeCode))
                        {
                            if (XF_SY_NAN_ChiMaService.GetOne(T => T.Size_Code == SizeCode) != null)
                            {
                                gender = "NAN";
                            }
                            else if (XF_SY_NU_ChiMaService.GetOne(T => T.Size_Code == SizeCode) != null)
                            {
                                gender = "NU";
                            }
                            else if (XF_KZ_CodeSizeService.GetOne(T => T.Size_Code == SizeCode) != null)
                            {
                                gender = "NAN";
                            }
                            else
                            {
                                throw new Exception("没有对应的尺码数据。");
                            }
                        }
                        else
                        {
                            throw new Exception("尺码表编号不能为空。");
                        }

                        if (Regex.IsMatch(item.ReCodeSize))
                        {

                            string lisaction = "";

                            switch (i)
                            {
             
                                case 1://普通上衣
                                    action = "XF_SY_" + gender;
                                    lisaction = "XF_SY_" + gender;
                                    break;
                                case 0://普通裤子
                                    action = "XF_KZ_" + gender;
                                    lisaction = "XF_KZ_" + gender;
                                    break;
                                default:
                                    action = "";
                                    break;
                            }

                            if (action != lisaction && action != "")
                            {
                                return "";
                            }

                        }
                    }
                }
                return action;
            }
            catch (Exception)
            {
                throw;
            }

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

        //加载列表
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
        //调整顺序
        public JsonResult UpdateIndex(int startIndex, int stopIndex, string GDH)
        {
            try
            {

                if (HanderDataForXF_SYService.GetOne(T => T.GDH == GDH) != null)
                {
                    HanderDataForXF_SYDto sydto = HanderDataForXF_SYService.GetOne(T => T.Index == startIndex && T.GDH == GDH);
                    sydto.Index = stopIndex;
                    HanderDataForXF_SYService.Update(sydto);
                    HanderDataForXF_SYDto sydto2 = HanderDataForXF_SYService.GetOne(T => T.Index == stopIndex && T.GDH == GDH);
                    sydto.Index = startIndex;
                    HanderDataForXF_SYService.Update(sydto2);
                    return Json(new { state = 1, msg = HanderDataForXF_SYService.Query(T => T.GDH == GDH, O => O.Index, false), action = "HanderDataForXF_SY" }, JsonRequestBehavior.AllowGet);
                }
                else if (HanderDataForXF_KZService.GetOne(T => T.GDH == GDH) != null)
                {
                    HanderDataForXF_KZDto kzdto = HanderDataForXF_KZService.GetOne(T => T.Index == startIndex && T.GDH == GDH);
                    kzdto.Index = stopIndex;
                    HanderDataForXF_KZService.Update(kzdto);
                    HanderDataForXF_KZDto kzdto2 = HanderDataForXF_KZService.GetOne(T => T.Index == stopIndex && T.GDH == GDH);
                    kzdto2.Index = startIndex;
                    HanderDataForXF_KZService.Update(kzdto2);

                    return Json(new { state = 1, msg = HanderDataForXF_KZService.Query(T => T.GDH == GDH, O => O.Index, false), action = "HanderDataForXF_KZ" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception("系统出错：数据库中找不到对应数据。");
                }

            }
            catch (Exception ex)
            {
                return Json(new { state = 0, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        //导出裁单
        public JsonResult ExportCaiDan(FormCollection fm)
        {

            string GDH = fm["GDH_ZH"];

            try
            {
                if (HanderDataForXF_SYService.GetOne(T => T.GDH == GDH) != null)
                {
                    var data = HanderDataForXF_SYService.Query(T => T.GDH == GDH, O => O.Index, false);
                    DataTable table = new DataTable();
                    table.Columns.Add("姓名");
                    table.Columns.Add("归码后尺码");
                    table.Columns.Add("备注");
                    table.Columns.Add("数量");
                    table.Columns.Add("流水号");
                    table.Columns.Add("至流水号");

                    int liushui = Convert.ToInt32(fm["caidanindex"]);

                    foreach (var item in data)
                    {
                        DataRow row = table.NewRow();
                        row["姓名"] = item.Name;
                        row["归码后尺码"] = item.RtnHCode;
                        row["备注"] = item.Note;
                        row["数量"] = item.Number;
                        row["流水号"] = liushui;
                        liushui += item.Number;
                        row["至流水号"] = liushui;
                        table.Rows.Add(row);
                    }
                    ExcelHelper.BuildExcel(table, "裁床记录表");
                    return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (HanderDataForXF_KZService.GetOne(T => T.GDH == GDH) != null)
                {
                    var data = HanderDataForXF_KZService.Query(T => T.GDH == GDH, O => O.Index, false);
                    DataTable table = new DataTable();
                    table.Columns.Add("姓名");
                    table.Columns.Add("归码后尺码");
                    table.Columns.Add("备注");
                    table.Columns.Add("数量");
                    table.Columns.Add("流水号");
                    table.Columns.Add("至流水号");

                    int liushui = Convert.ToInt32(fm["caidanindex"]);

                    foreach (var item in data)
                    {
                        DataRow row = table.NewRow();
                        row["姓名"] = item.Name;
                        row["归码后尺码"] = item.RtnQCode;
                        row["备注"] = item.Note;
                        row["数量"] = item.Number;
                        liushui++;
                        row["流水号"] = liushui;
                        liushui += item.Number;
                        row["至流水号"] = liushui;
                        table.Rows.Add(row);
                    }
                    ExcelHelper.BuildExcel(table, "裁床记录表");
                    return Json(new { state = 1, msg = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception("系统出错：数据库中找不到对应数据。");
                }

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    state = 0,
                    msg = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public class handleBool
        {
            public bool sizezz1 { get; set; }
            public bool sizezz2 { get; set; }
            public bool sizezz3 { get; set; }
            public bool sizezz4 { get; set; }

        }


    }
}