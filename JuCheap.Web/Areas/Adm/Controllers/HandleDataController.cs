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
        public ICs_dataService Cs_dataService { get; set; }

        public ISleeveService SleeveService { get; set; }

        public IXF_SY_NAN_ChiMaService XF_SY_NAN_ChiMaService { get; set; }

        public IXF_KZ_CodeSizeService XF_KZ_CodeSizeService { get; set; }

        // GET: Adm/HandleData
        public ActionResult Handle(int moudleId, int menuId, int btnId)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Handle(FormCollection fm)
        {

            using (MyRepository db = new MyRepository())
                try
                {
                    var data = Cs_dataService.Query(q => 1 == 1, s => s.Id, false);

                    List<XF_SY_NAN_ChiMaDto> list = null;

                    //where GDH='{0}'

                    DataTable GDData = db.Database.GetDataTable(string.Format("select * from pytbulkgh where GDH='T511-16041407'", fm["GDH"]));

                    string Action = "";

                    for (int i = 0; i < GDData.Rows.Count; i++)
                    {

                        bool[] sizezz = isSYorKZ(GDData.Rows[i]["SIZECODE"].ToString());

                        int trueCount = 0;

                        for (int a = 0; a < sizezz.Length; a++)
                        {
                            switch (a)
                            {
                                case 0://特殊上衣
                                    if (sizezz[a])
                                    {
                                        Action = "T_XF_SY_" + gender(GDData.Rows[i]["XB"].ToString());
                                        trueCount++;
                                    }
                                    break;
                                case 1://特殊裤子
                                    if (sizezz[a])
                                    {
                                        Action = "T_XF_KZ_" + gender(GDData.Rows[i]["XB"].ToString());
                                        trueCount++;
                                    }
                                    break;
                                case 2://普通上衣
                                    if (sizezz[a])
                                    {
                                        Action = "XF_SY_" + gender(GDData.Rows[i]["XB"].ToString());
                                        trueCount++;
                                    }
                                    break;
                                case 3://普通裤子
                                    if (sizezz[a])
                                    {
                                        Action = "XF_KZ_" + gender(GDData.Rows[i]["XB"].ToString());
                                        trueCount++;
                                    }
                                    break;

                                default:
                                    throw new Exception("没有对应的处理程序，请检查数据，程序代码：" + Action);
                            }

                        }

                        if (trueCount > 1)
                        {

                            throw new Exception("数据出错，出现多个匹配代码。");

                        }

                    }

                    switch (Action)
                    {

                        case "XF_SY_NAN":
                            List < HanderDataForXF_SYDto > SYlist= new List<HanderDataForXF_SYDto>();
                            foreach (var item in data)
                            {
                                decimal a01 = Convert.ToDecimal(item.ReCodeSize.Split('/')[0]);

                                string a02 = item.ReCodeSize.Split('/')[1];

                                XF_SY_NAN_ChiMaDto dto = XF_SY_NAN_ChiMaService.GetOne(T => T.Height == a01 && T.NetBust == a02 && T.Size_Code == fm["Size_Code"]);
                                SleeveDto sledto = SleeveService.GetOne(T => T.FK_CoatSize_ID == dto.Id && T.Code == dto.NetBust.Substring(dto.NetBust.Length - 1, 1));

                                if (dto != null)
                                {
                                    HanderDataForXF_SYDto sy = new HanderDataForXF_SYDto();
                                    sy.Height = dto.Height;
                                    sy.RtnQCode = item.ReCodeSize;
                                    sy.OrderCode = item.Orderid;
                                    sy.Name = item.Name;
                                    sy.RtnHCode = a02;
                                    sy.Number = item.Number;
                                    sy.Yichang = Convert.ToDecimal(dto.FrontLength);
                                    sy.Bust = dto.FinishedBust;
                                    sy.Sleeve = sledto.Length;
                                    SYlist.Add(sy);
                                }
                            }

                            break;
                        case "XF_SY_NU":
                            break;
                        case "XF_KZ_NAN":
                            break;
                        case "XF_KZ_NU":


                            List<XF_KZ_CodeSizeDto> XF_KZ_NU_List = new List<XF_KZ_CodeSizeDto>();
                            foreach (var item in data)
                            {
                                decimal a01 = Convert.ToDecimal(item.ReCodeSize.Split('/')[0]);

                                string a02 = item.ReCodeSize.Split('/')[1];
                                string temp = a02.Substring(a02.Length - 1, 1);
                                string Code = a02.Substring(0, a02.Length - 1);
                                string Size_Code = fm["Size_Code"];

                                XF_KZ_CodeSizeDto dto = XF_KZ_CodeSizeService.GetOne(T => T.CP_WaistWidth.Contains(temp) && T.Code == a02 && T.Size_Code == Size_Code);


                                if (dto != null)
                                {
                                    XF_KZ_NU_List.Add(dto);
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    if (list.Count > 0)
                    {
                        return Json(new { state = 1, msg = list }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("解析失败，请检查尺码表编号是否对应");
                    }
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
        public bool[] isSYorKZ(string size)
        {

            string TSY = "[0-9]{2,3}/[0-9A-z]{2,4}/[0-9]{2,3}[\u4e00-\u9fa5]{2,3}";//特殊上衣

            string TKZ = "[0-9]{2,3}/[0-9A-z]{2,4}[\u4e00-\u9fa5]{2,3}";//特殊裤子

            string PTSY = "[0-9]{2,3}/[0-9A-z]{2,4}/[0-9]{2,3}";//普通上衣

            string PTKZ = "[0-9]{2,3}/[0-9A-z]{2,4}";//普通裤子

            bool[] zzArrey = new bool[4];

            Regex RegexTSY = new Regex(TSY);
            zzArrey[0] = RegexTSY.IsMatch(size);

            Regex RegexTKZ = new Regex(TKZ);
            zzArrey[1] = RegexTKZ.IsMatch(size);

            Regex RegexPTSY = new Regex(PTSY);
            zzArrey[2] = RegexPTSY.IsMatch(size);

            Regex RegexPTKZ = new Regex(PTKZ);
            zzArrey[3] = RegexPTKZ.IsMatch(size);

            return zzArrey;
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
                SearchKey = Request["keywords"]
            };
            string xxxx = Request["orderBy"];

            Expression<Func<Cs_dataDto, bool>> exp = item => !item.IsDeleted;
            if (!queryBase.SearchKey.IsBlank())
                exp = exp.And(item => item.Name.Contains(queryBase.SearchKey));

            var dto = Cs_dataService.GetWithPages(queryBase, exp, Request["orderBy"], Request["orderDir"]);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}