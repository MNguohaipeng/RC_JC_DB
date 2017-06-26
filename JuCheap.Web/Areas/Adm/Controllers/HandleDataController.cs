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

                        dl.SizeCode = GDData.Rows[i]["GGDH"].ToString();

                        dl.GDH = GDH;

                        //  dl.Gender = GDData.Rows[i]["XB"].ToString();

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

                    #region 获取待处理数据

                    List<DCL_DataDto> GDData = DCL_DataService.Query(T => T.GDH == GDH, O => O.Id, false);

                    #endregion

                    List<DCL_DataDto> DLCData = new List<DCL_DataDto>();

                    #region 判断数据中是否有尺码编号  没有择手动补全

                    for (int i = 0; i < GDData.Count; i++)
                    {
                        DCL_DataDto dto = new DCL_DataDto();

                        dto = GDData[i];

                        if (string.IsNullOrEmpty(GDData[i].SizeCode))
                        {
                            dto.SizeCode = SizeCode;
                        }

                        DLCData.Add(dto);
                    }
                    #endregion
 
                    #region   验证此数据是否是特殊数据  和 是否可以处理

                    string Action = isSYorKZTo(DLCData);

                    if (Action == "")
                        throw new Exception("此工单号数据不统一或是没有对应的处理程序。");

                    #endregion

                    switch (Action)
                    {

                        case "XF_SY_NAN":
                            Handle_XF_SY_NAN(SizeCode, GDH);
                            break;
                        case "XF_SY_NU":
                            Handle_XF_SY_NU(SizeCode, GDH);
                            break;
                        case "XF_KZ_NAN":
                        case "XF_KZ_NU":
                            Handle_XF_KZ(SizeCode, GDH);
                            break;
                    }
                    return Json(new { state = 1, msg = "处理成功。" }, JsonRequestBehavior.AllowGet);

            

                }
                catch (CustomCatch cc)
                {
                    return Json(new { state = cc.Level, msg = cc.ExceptionString }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { state = 0, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

        }

        public bool Handle_XF_KZ(string SizeCode, string GDH)
        {

            List<XF_SY_NAN_ChiMaDto> list = null;

            #region 获取待处理数据

            List<DCL_DataDto> GDData = DCL_DataService.Query(T => T.GDH == GDH, O => O.Id, false);

            #endregion

            try
            {

                #region 开始处理数据

                List<DCL_DataDto> DLCData = new List<DCL_DataDto>();

                #region 判断数据中是否有尺码编号  没有择手动补全

                for (int i = 0; i < GDData.Count; i++)
                {
                    DCL_DataDto dto = new DCL_DataDto();

                    dto = GDData[i];

                    if (string.IsNullOrEmpty(GDData[i].SizeCode))
                    {
                        dto.SizeCode = SizeCode;
                    }

                    DLCData.Add(dto);
                }
                #endregion

                int index = 1;

                foreach (DCL_DataDto item in DLCData)
                {

  
                    decimal height_kz_nu = Convert.ToDecimal(item.ReCodeSize.ToString().Split('/')[0]);

                    string a02 = item.ReCodeSize.ToString().Split('/')[1];

                    string temp = a02.Substring(a02.Length - 1, 1);

                    string Code = a02.Substring(0, a02.Length - 1);

                    XF_KZ_CodeSizeDto dto = XF_KZ_CodeSizeService.GetOne(T => T.CP_WaistWidth.Contains(Code) && T.Code == temp && T.Size_Code == item.SizeCode&&T.Status==1);//TO DO

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
                    else {
                        throw new CustomCatch(2, "没有查询到对应的尺码表数据,请更换尺码表编号");

                    }
                }
                #endregion

                return true;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool Handle_XF_SY_NU(string SizeCode, string GDH)
        {


            List<XF_SY_NAN_ChiMaDto> list = null;

            #region 获取待处理数据

            List<DCL_DataDto> GDData = DCL_DataService.Query(T => T.GDH == GDH, O => O.Id, false);

            if (GDData.Count <= 0)
            {
                throw new Exception("没有查询到对应的待处理数据，请检查工单号。");
            }

            #endregion


            try
            {
                #region 开始处理数据

                List<DCL_DataDto> DLCData = new List<DCL_DataDto>();

                #region 判断数据中是否有尺码编号  没有择手动补全

                for (int i = 0; i < GDData.Count; i++)
                {
                    DCL_DataDto dto = new DCL_DataDto();

                    dto = GDData[i];

                    if (string.IsNullOrEmpty(GDData[i].SizeCode))
                    {
                        dto.SizeCode = SizeCode;
                    }

                    DLCData.Add(dto);
                }
#endregion

                #region   验证此数据是否可以处理

                string Action = isSYorKZTo(DLCData);

                if (isSYorKZTo(DLCData) == "")
                    throw new Exception("此工单号数据不统一或是没有对应的处理程序。");

                #endregion
 
                #region   分离普通数据和特殊数据
                List<DCL_DataDto> PTlist = new List<DCL_DataDto>();
                List<DCL_DataDto> TSlist = new List<DCL_DataDto>();

                foreach (DCL_DataDto item in DLCData)
                {
                    Regex rex = new Regex("[\u4e00-\u9fa5]{2}[-/+][0-9]{0,3}");

                    if (rex.IsMatch(item.ReCodeSize.ToString().Split('/')[2]))
                    {
                        TSlist.Add(item);
                    }
                    else
                    {
                        PTlist.Add(item);
                    }
                }

                #endregion

                DeleteForHanderData(GDH, Action);
 
                List<HanderDataForXF_SYDto> PTHandlelist = new List<HanderDataForXF_SYDto>();
 
                decimal Height;//身高

                string JingXiongWei;//净胸围

                string XiuChang;//袖长

                string TeShuShuJu;//特殊数据

                int pindex = 1;

                #region  处理普通数据

                foreach (DCL_DataDto item in PTlist)
                {

                    string DcHeight = item.ReCodeSize.ToString().Split('/')[0];

                    Height = Convert.ToDecimal(DcHeight);

                    JingXiongWei = item.ReCodeSize.ToString().Split('/')[1];//净胸围

                    XiuChang = item.ReCodeSize.ToString().Split('/')[2];//袖长

                    XF_SY_NAN_ChiMaDto dtonu = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == Height && T.NetBust == JingXiongWei && T.Size_Code == SizeCode && T.Status == 1);

                    if (dtonu != null)
                    {

                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();

                        sy.Height = dtonu.Height;

                        sy.RtnQCode = item.ReCodeSize.ToString();

                        sy.OrderCode = item.Orderid.ToString();

                        sy.Name = item.Name.ToString();

                        sy.RtnHCode = JingXiongWei;

                        sy.Number = item.Number;

                        sy.Yichang = Convert.ToDecimal(dtonu.FrontLength);

                        #region 处理袖长

                        string[] Sleecve = dtonu.Sleecve_Show.Split(' ');

                        for (int i = 0; i < Sleecve.Length; i++)
                        {

                            if (Sleecve[i].IndexOf(';') > 0)
                            {

                                if (Sleecve[i].Split(';')[0] == XiuChang)
                                {

                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);

                                }

                            }
                            else if (Sleecve[i].IndexOf(':') > 0)
                            {

                                if (Sleecve[i].Split(';')[0] == XiuChang)
                                {

                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);

                                }

                            }

                        }

                        #endregion

                        sy.Bust = dtonu.FinishedBust;

                        sy.Index = pindex;

                        HanderDataForXF_SYService.Add(sy);
                    }
                    else {
                        throw new Exception("没有查询到对应的尺码表信息");
                    }

                    pindex++;

                }

                #endregion

                #region 处理特殊数据

                foreach (DCL_DataDto item in TSlist)
                {

                    string DcHeight = item.ReCodeSize.ToString().Split('/')[0];

                    Height = Convert.ToDecimal(DcHeight);

                    JingXiongWei = item.ReCodeSize.ToString().Split('/')[1];//净胸围

                    XiuChang = item.ReCodeSize.ToString().Split('/')[2].Substring(0, 1);//袖长

                    XF_SY_NAN_ChiMaDto dtonu = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == Height && T.NetBust == JingXiongWei && T.Size_Code == SizeCode && T.Status == 1);

                    if (dtonu != null)
                    {

                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();

                        sy.Height = dtonu.Height;

                        sy.RtnQCode = item.ReCodeSize.ToString();

                        sy.OrderCode = item.Orderid.ToString();

                        sy.Name = item.Name.ToString();

                        sy.RtnHCode = JingXiongWei;

                        sy.Number = item.Number;

                        sy.Yichang = Convert.ToDecimal(dtonu.FrontLength);

                        #region 处理袖长

                        string[] Sleecve = dtonu.Sleecve_Show.Split(' ');

                        for (int i = 0; i < Sleecve.Length; i++)
                        {

                            if (Sleecve[i].IndexOf(';') > 0)
                            {

                                if (Sleecve[i].Split(';')[0] == XiuChang)
                                {

                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);

                                }

                            }
                            else if (Sleecve[i].IndexOf(':') > 0)
                            {

                                if (Sleecve[i].Split(';')[0] == XiuChang)
                                {

                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);

                                }

                            }

                        }



                        #endregion

                        sy.Bust = dtonu.FinishedBust;

                        sy.Index = pindex;

                        Regex rex = new Regex("[\u4e00-\u9fa5]{2}[-/+][0-9]{0,3}");

                        var TsArrey = rex.Matches(item.ReCodeSize.ToString().Split('/')[2]);

                        #region 處理特躰
                        foreach (Match TsItem in TsArrey)
                        {
                            Regex Tn = new Regex("[\u4e00-\u9fa5]{2}");//名称
                            Regex Tysf = new Regex("[-/+]");//运算符
                            Regex Tv = new Regex("[0-9]{1,2}");//值
                            switch (Tn.Match(TsItem.Value).Value)
                            {

                                case "袖长"://袖长
                                    decimal Sleeve = sy.Sleeve;
                                    if (Tysf.Match(TsItem.Value).Value == "+")
                                    {

                                        sy.Sleeve = Sleeve + Convert.ToInt32(Tv.Match(TsItem.Value));
                                    }
                                    else
                                    {
                                        sy.Sleeve = Sleeve + Convert.ToInt32(Tv.Match(TsItem.Value));
                                    }
                                    break;
                                case "下摆"://下摆

                                    break;
                                case "三围"://三围
                                    if (Tysf.Match(TsItem.Value).Value == "+")
                                    {
                                        sy.Yichang += Convert.ToInt32(Tv.Match(TsItem.Value));
                                        sy.Bust += Convert.ToInt32(Tv.Match(TsItem.Value));
                                    }
                                    else
                                    {
                                        string xxx = Tv.Match(TsItem.Value).Value;
                                        sy.Yichang -= Convert.ToInt32(Tv.Match(TsItem.Value).Value);
                                        sy.Bust -= Convert.ToInt32(Tv.Match(TsItem.Value).Value);
                                    }
                                    break;
                                case "肩宽"://肩宽

                                    break;
                            }
                        }
                        #endregion

                        HanderDataForXF_SYService.Add(sy);
                    }
                    else
                    {
                        throw new Exception("没有查询到对应的尺码表信息");
                    }

                    pindex++;
           
                }

                #endregion

                return true;
            }
            catch (Exception)
            {

                throw;

            }

        }

        public bool Handle_XF_SY_NAN(string SizeCode, string GDH)
        {

            List<XF_SY_NAN_ChiMaDto> list = null;

            #region 获取待处理数据

            List<DCL_DataDto> GDData = DCL_DataService.Query(T => T.GDH == GDH, O => O.Id, false);

            #endregion


            try
            {
                
                #region 开始处理数据

                List<DCL_DataDto> DLCData = new List<DCL_DataDto>();

                #region 判断数据中是否有尺码编号  没有择手动补全

                for (int i = 0; i < GDData.Count; i++)
                {
                    DCL_DataDto dto = new DCL_DataDto();

                    dto = GDData[i];

                    if (string.IsNullOrEmpty(GDData[i].SizeCode))
                    {
                        dto.SizeCode = SizeCode;
                    }

                    DLCData.Add(dto);
                }
                #endregion
 
                #region   验证此数据是否可以处理

                string Action = isSYorKZTo(DLCData);

                if (isSYorKZTo(DLCData) == "")
                    throw new Exception("此工单号数据不统一或是没有对应的处理程序。");

                #endregion
 
                DeleteForHanderData(GDH, Action);
 
                decimal Height;//身高

                string JingXiongWei;//净胸围

                string XiuChang;//袖长

                string TeShuShuJu;//特殊数据

                int index = 1;

                string NetBustNan = "";

                foreach (DCL_DataDto item in DLCData)
                {

                    NetBustNan = item.ReCodeSize.ToString().Split('/')[1];

                    string isT = "[\u4e00-\u9fa5]";

                    Regex Regex = new Regex(isT);

                    string DcHeight = item.ReCodeSize.ToString().Split('/')[0];

                    #region 处理特殊身高

                    if (Regex.IsMatch(DcHeight))
                    {
                        Height = Convert.ToDecimal(DcHeight.Substring(1, DcHeight.Length));
                    }
                    else
                    {
                        Height = Convert.ToDecimal(DcHeight);
                    }

                    #endregion

                    JingXiongWei = item.ReCodeSize.ToString().Split('/')[1];//净胸围

                    Regex rex = new Regex("[\u4e00-\u9fa5]{2}[-/+][0-9]{0,3}");

                    if (Regex.IsMatch(item.ReCodeSize.ToString().Split('/')[2]))
                    {

                        XiuChang = item.ReCodeSize.ToString().Split('/')[2].Substring(0, 1);//袖长

                    }
                    else
                    {

                        XiuChang = item.ReCodeSize.ToString().Split('/')[2];//袖长

                    }

                    //获取尺码表数据
                    XF_SY_NAN_ChiMaDto dtonan = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == Height && T.NetBust == JingXiongWei && T.Size_Code == SizeCode && T.Status == 1);

                    if (dtonan != null)
                    {
                        HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();

                        sy.Height = dtonan.Height;

                        sy.RtnQCode = item.ReCodeSize.ToString();

                        sy.OrderCode = item.Orderid.ToString();

                        sy.Name = item.Name.ToString();

                        sy.RtnHCode = JingXiongWei;

                        sy.Number = item.Number;

                        sy.GDH = item.GDH;

                        sy.Yichang = Convert.ToDecimal(dtonan.FrontLength);

                        #region 处理袖长
                        string[] Sleecve = dtonan.Sleecve_Show.Split(' ');
                        for (int i = 0; i < Sleecve.Length; i++)
                        {
                            if (Sleecve[i].IndexOf(';') > 0)
                            {
                                if (Sleecve[i].Split(';')[0] == XiuChang)
                                {
                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(';')[1]);
                                }
                            }
                            else if (Sleecve[i].IndexOf(':') > 0)
                            {
                                if (Sleecve[i].Split(':')[0] == XiuChang)
                                {
                                    sy.Sleeve = Convert.ToDecimal(Sleecve[i].Split(':')[1]);
                                }
                            }
                        }
                        #endregion

                        sy.Bust = dtonan.FinishedBust;

                        sy.Index = index;

                        var TsArrey = rex.Matches(item.ReCodeSize.ToString().Split('/')[2]);

                        #region 處理特躰
                        foreach (Match TsItem in TsArrey)
                        {
                            Regex Tn = new Regex("[\u4e00-\u9fa5]{2}");//名称
                            Regex Tysf = new Regex("[-/+]");//运算符
                            Regex Tv = new Regex("[0-9]{1,2}");//值
                            switch (Tn.Match(TsItem.Value).Value)
                            {

                                case "袖长"://袖长
                                    decimal Sleeve = sy.Sleeve;
                                    if (Tysf.Match(TsItem.Value).Value == "+")
                                    {

                                        sy.Sleeve = Sleeve + Convert.ToInt32(Tv.Match(TsItem.Value));
                                    }
                                    else
                                    {
                                        sy.Sleeve = Sleeve + Convert.ToInt32(Tv.Match(TsItem.Value));
                                    }
                                    break;
                                case "下摆"://下摆

                                    break;
                                case "三围"://三围
                                    if (Tysf.Match(TsItem.Value).Value == "+")
                                    {
                                        sy.Yichang += Convert.ToInt32(Tv.Match(TsItem.Value));
                                        sy.Bust += Convert.ToInt32(Tv.Match(TsItem.Value));
                                    }
                                    else
                                    {
                                        string xxx = Tv.Match(TsItem.Value).Value;
                                        sy.Yichang -= Convert.ToInt32(Tv.Match(TsItem.Value).Value);
                                        sy.Bust -= Convert.ToInt32(Tv.Match(TsItem.Value).Value);
                                    }
                                    break;
                                case "肩宽"://肩宽

                                    break;
                            }
                        }
                        #endregion

                        HanderDataForXF_SYService.Add(sy);
                    }

                    index++;
  
                }
                #endregion

                return true;
            }
            catch (Exception)
            {

                throw;
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

        //删除处理数据
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
                                throw new CustomCatch(2, "没有对应的尺码数据，请检查尺码表号");
                            }
                        }
                        else
                        {
                            throw new CustomCatch(2, "数据中没有尺码表号，请补充");
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
            catch (CustomCatch cc)
            {
                throw;

            }
            catch (Exception)
            {
                throw;
            }

        }

    
        //加载列表
        public JsonResult GetList(string GDH)
        {

            using (var db = new MySqlServer())
                try
                {
                    return Json(new { state = 1, msg = db.Database.GetList<DCL_DataDto>(string.Format("select * from DCL_Data where GDH='{0}'", GDH)) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { state = 1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }


        }

    
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
                        db.Database.GetInt(string.Format("update HanderDataForXF_KZ set [index]=-1 where [index]={0} and GDH='{1}' ", stopIndex, GDH));
                        db.Database.GetInt(string.Format("update HanderDataForXF_KZ set [index]={0} where [index]={1} and GDH='{2}' ", stopIndex, startIndex, GDH));
                        db.Database.GetInt(string.Format("update HanderDataForXF_KZ set [index]={0} where [index]={1} and GDH='{2}' ", startIndex, -1, GDH));
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
        public JsonResult LoadOrder(string Action, string GDH)
        {
            using (var db = new MySqlServer())
                try
                {
                    if (HanderDataForXF_SYService.GetOne(T => T.GDH == GDH) != null)
                    {
                        var orderlist = db.Database.GetList<DictionariesDto>("select * from Dictionaries where D_Key='HanderDataForXF_SYOrder'");
                        return Json(new { state = 1, msg = orderlist }, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
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

        #endregion

    }
}