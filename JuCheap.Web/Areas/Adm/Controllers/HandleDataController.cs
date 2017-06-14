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
using Newtonsoft.Json;

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
        public JsonResult ImportHandleData(string GDH)
        {
            using (MyRepository db = new MyRepository())
                try
                {
                    DataTable GDData = db.Database.GetDataTable(string.Format("select * from pytbulkgh where GDH='{0}'", GDH));

                    List<DCL_DataDto> DCLList = new List<DCL_DataDto>();


                    #region 删除相同工单号的数据
                   var deleteDate = DCL_DataService.Query(T => T.GDH == GDH, O => O.Index, false);
                   foreach (var item in deleteDate)
                   {
                       DCL_DataService.Delete(item.Id);
                   }
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

                        dl.GDH = GDH;

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
            SizeCode = GDH;

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

                    if (isSYorKZTo(GDData) == "")
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
                                    #region XF_SY_NAN

                                    List<HanderDataForXF_SYDto> SYnanlist = new List<HanderDataForXF_SYDto>();

                                    string NetBustNan = item.ReCodeSize.ToString().Split('/')[1];

                                    SizeCode = GDH;//TO DO

                                    string sthi = item.ReCodeSize.ToString().Split('/')[0];

                                    Height = Convert.ToDecimal(sthi.Substring(1, sthi.Length - 1));

                                    A02 = item.ReCodeSize.ToString().Split('/')[1];

                                    A02R = A02.Substring(A02.Length - 1, 1);

                                    A02S = A02.Substring(0, A02.Length - 1);

                                    A03 = item.ReCodeSize.ToString().Split('/')[2];

                                    XF_SY_NAN_ChiMaDto dtonan = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == Height && T.NetBust == A02 && T.Size_Code == SizeCode);

                                    SleeveDto sledto = SleeveService.GetOne(T => T.FK_CoatSize_ID == dtonan.Id && T.Code == dtonan.NetBust.Substring(dtonan.NetBust.Length - 1, 1));

                                    Match XF_SY_NAN_Match = Regex.Match(A03);

                                    if (dtonan != null)
                                    {

                                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();

                                        sy.Height = dtonan.Height;

                                        sy.RtnQCode = item.ReCodeSize.ToString();

                                        sy.OrderCode = item.Orderid.ToString();

                                        sy.Name = item.Name.ToString();

                                        sy.RtnHCode = A02;

                                        sy.Number = item.Number;

                                        sy.GDH = item.GDH;

                                        sy.Yichang = Convert.ToDecimal(dtonan.FrontLength);

                                        #region 处理袖长
                                        string[] Sleecve = dtonan.Sleecve_Show.Split(' ');
                                        for (int i = 0; i < Sleecve.Length; i++)
                                        {
                                            if (Sleecve[i].IndexOf(';') > 0)
                                            {
                                                if (Sleecve[i].Split(';')[0] == A03.Substring(0, 1))
                                                {
                                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);
                                                }
                                            }
                                            else if (Sleecve[i].IndexOf(':') > 0)
                                            {
                                                if (Sleecve[i].Split(';')[0] == A03.Substring(0, 1))
                                                {
                                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);
                                                }
                                            }
                                        }
                                        #endregion

                                        sy.Bust = dtonan.FinishedBust;

                                        sy.Index = index;

                                        Regex tregsy = new Regex("[+_-]");
                                        Match macsy = tregsy.Match(A03);

                                        Regex Reg_T_Name = new Regex("[\u4e00-\u9fa5]{2,4}");
                                        Match Mat_T_Name = Reg_T_Name.Match(A03);

                                        Regex Reg_T_Number = new Regex(@"\d");
                                        Match Mat_T_Number = Reg_T_Number.Match(A03);


                                        switch (Mat_T_Name.Value)
                                        {
                                            case "袖长"://袖长
                                                decimal Sleeve = sy.Sleeve;
                                                if (macsy.Value == "+")
                                                {

                                                    sy.Sleeve = Sleeve + Convert.ToInt32(Mat_T_Number.Value);
                                                }
                                                else
                                                {
                                                    sy.Sleeve = Sleeve - Convert.ToInt32(Mat_T_Number.Value);
                                                }
                                                break;
                                            case "下摆"://下摆

                                                break;
                                            case "三围"://三围
                                                if (macsy.Value == "+")
                                                {
                                                    sy.Yichang += Convert.ToInt32(Mat_T_Number.Value);
                                                    sy.Bust += Convert.ToInt32(Mat_T_Number.Value);
                                                }
                                                else
                                                {
                                                    sy.Yichang -= Convert.ToInt32(Mat_T_Number.Value);
                                                    sy.Bust -= Convert.ToInt32(Mat_T_Number.Value);
                                                }
                                                break;
                                            case "肩宽"://肩宽

                                                break;
                                        }

                                        HanderDataForXF_SYService.Add(sy);

                                    }

                                    #endregion
                                    break;
                                case "XF_SY_NU":
                                    #region　XF_SY_NU
                                    List<HanderDataForXF_SYDto> SYlist = new List<HanderDataForXF_SYDto>();

                                    string NetBustNu = item.ReCodeSize.ToString().Split('/')[1];

                                    SizeCode = GDH;//TO DO

                                    Height = Convert.ToDecimal(item.ReCodeSize.ToString().Split('/')[0]);

                                    A02 = item.ReCodeSize.ToString().Split('/')[1];

                                    A02R = A02.Substring(A02.Length - 1, 1);

                                    A02S = A02.Substring(0, A02.Length - 1);

                                    A03 = item.ReCodeSize.ToString().Split('/')[2];

                                    XF_SY_NAN_ChiMaDto dtonu = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == Height && T.NetBust == A02 && T.Size_Code == SizeCode);

                                    Match XF_SY_NU_Match = Regex.Match(A03);

                                    if (dtonu != null)
                                    {

                                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();

                                        sy.Height = dtonu.Height;

                                        sy.RtnQCode = item.ReCodeSize.ToString();

                                        sy.OrderCode = item.Orderid.ToString();

                                        sy.Name = item.Name.ToString();

                                        sy.RtnHCode = A02;

                                        sy.Number = item.Number;

                                        sy.Yichang = Convert.ToDecimal(dtonu.FrontLength);

                                        #region 处理袖长
                                        string[] Sleecve = dtonu.Sleecve_Show.Split(' ');
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

                                        sy.Bust = dtonu.FinishedBust;

                                        sy.Index = index;

                                        Regex tregsy = new Regex(@"\+\d+");
                                        Match macsy = tregsy.Match(A03);
                                        switch (DictionariesService.GetOne(T => T.D_Key == "T_XF_SY_" + XF_SY_NU_Match.Success).D_Value)
                                        {
                                            case "Sleecve_Show"://袖长
                                                string sssxxx = macsy.Success.ToString().Substring(1, 2);
                                                if (macsy.Success.ToString().Substring(1, 2) == "+")
                                                {

                                                }
                                                break;
                                            case "FinishedHem_NoFork"://下摆
                                                break;
                                            case "NetBust,InWaist"://三围
                                                break;
                                            case "ShoulderWidth"://肩宽
                                                break;
                                        }

                                        HanderDataForXF_SYService.Add(sy);

                                    }
                                    #endregion
                                    break;
                                case "XF_KZ_NAN":
                                case "XF_KZ_NU":
                                    #region XF_KZ
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
                                    #endregion
                                    break;
                            }
                        }
                        else
                        {

                            switch (Action)
                            {

                                case "XF_SY_NAN":
                                    #region XF_SY_NAN
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

                                        sy.GDH = item.GDH;

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
                                    #endregion

                                    break;
                                case "XF_SY_NU":
                                    #region XF_SY_NU
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
                                    #endregion

                                    break;
                                case "XF_KZ_NAN":
                                case "XF_KZ_NU":
                                    #region XF_KZ
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
                                    #endregion

                                    break;
                                default:
                                    throw new Exception("没有对应的处理程序。");

                            }

                        }
                        index++;
                    }
                    #endregion

                    return Json(new { state = 1, msg = "处理成功。" }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { state = 0, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

        }

        //获取初排数据
        public JsonResult GetListForHanderData(string GDH, string Order, string type)
        {
            using (var db = new MySqlServer())
                try
                {
                    Order = Order.TrimEnd(',');

                    if (HanderDataForXF_SYService.GetOne(T => T.GDH == GDH) != null)
                    {
                        string xxxxdsadsax = string.Format("select ROW_NUMBER()over(order by {0}) rownum,* from (select * from  HanderDataForXF_SY ) as list order by {0}", Order);

                        //list.Height,list.Sleeve,list.Bust
                        var SYList = db.Database.GetDataTable(string.Format("select ROW_NUMBER()over(order by {0}) rownum,* from (select * from  HanderDataForXF_SY ) as list order by {0}", Order));
                        if (type != "NotOrder")
                        {
                            for (int i = 0; i < SYList.Rows.Count; i++)
                            {
                                var xxxxx = db.Database.GetInt(string.Format("update HanderDataForXF_SY set [index]={0} where id={1}", SYList.Rows[i]["rownum"].ToString(), SYList.Rows[i]["Id"].ToString()));
                            }
                        }
                        return Json(new { state = 1, action = "HanderDataForXF_SY", msg = HanderDataForXF_SYService.Query(T => T.GDH == GDH, O => O.Index, false) }, JsonRequestBehavior.AllowGet);

                    }
                    else if (HanderDataForXF_KZService.GetOne(T => T.GDH == GDH) != null)
                    {

                        return Json(new { state = 1, action = "HanderDataForXF_KZ", msg = HanderDataForXF_KZService.Query(T => T.GDH == GDH, O => O.Index, false) }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetList(string GDH)
        {

            using (var db = new MySqlServer())
                try
                {
                    return Json(new { state = 1, msg = db.Database.GetList<DCL_DataDto>(string.Format("select * from DCL_Data where GDH='{0}'",GDH)) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { state = 1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }


        }

        #endregion
        //调整顺序
        public JsonResult UpdateIndex(int startIndex, int stopIndex, string GDH)
        {
            using (var db = new MySqlServer())
                try
                {

                    if (HanderDataForXF_SYService.GetOne(T => T.GDH == GDH) != null)
                    {
                        db.Database.GetInt(string.Format("update HanderDataForXF_SY set [index]=-1 where [index]={0} and GDH='{1}' ", stopIndex, GDH));
                        db.Database.GetInt(string.Format("update HanderDataForXF_SY set [index]={0} where [index]={1} and GDH='{2}' ", stopIndex, startIndex, GDH));
                        db.Database.GetInt(string.Format("update HanderDataForXF_SY set [index]={0} where [index]={1} and GDH='{2}' ", startIndex, -1, GDH));
                        return Json(new { state = 1, msg = "排序成功。" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (HanderDataForXF_KZService.GetOne(T => T.GDH == GDH) != null)
                    {
                        db.Database.GetInt(string.Format("update HanderDataForXF_KZ set index=-1 where index={1} and GDH={2} ;@@identity", stopIndex, GDH));
                        db.Database.GetInt(string.Format("update HanderDataForXF_KZ set index={0} where index={1} and GDH={2} ;@@identity", stopIndex, startIndex, GDH));
                        db.Database.GetInt(string.Format("update HanderDataForXF_KZ set index={0} where index={1} and GDH={2} ;@@identity", startIndex, -1, GDH));
                        return Json(new { state = 1, msg = "排序成功。" }, JsonRequestBehavior.AllowGet);

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

        //导出裁单
        public JsonResult LoadOrder(string Action,string GDH)
        {
            using (var db = new MySqlServer())
                try
                {
                    if (HanderDataForXF_SYService.GetOne(T => T.GDH == GDH) != null)
                    {
                        var orderlist = db.Database.GetList<DictionariesDto>("select * from Dictionaries where D_Key='HanderDataForXF_SYOrder'");
                        return Json(new { state = 1, msg = orderlist }, JsonRequestBehavior.DenyGet);
                    }
                    else {
                        var orderlist = db.Database.GetList<DictionariesDto>("select * from Dictionaries where D_Key='HanderDataForXF_KZOrder'");
                        return Json(new { state = 1, msg = orderlist }, JsonRequestBehavior.DenyGet);
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