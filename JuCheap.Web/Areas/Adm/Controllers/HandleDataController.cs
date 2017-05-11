using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using JuCheap.Core.Extentions;
using JuCheap.Service.Abstracts;
using JuCheap.Service.Dto;
using JuCheap.Service.Enum;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class HandleDataController:AdmBaseController
    {
        public ICs_dataService Cs_dataService { get; set; }

        public IXF_SY_NAN_CodeSizeService IXF_SY_NAN_CodeSizeService { get; set; }

        // GET: Adm/HandleData
        public ActionResult Handle(int moudleId, int menuId, int btnId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Handle(string Size_Code)
        {

            var data = Cs_dataService.Query(q => 1 == 1, s=>s.Id, false);
            foreach (var item in data)
            {
                decimal a01 = Convert.ToDecimal(item.ReCodeSize.Split('/')[0]);
                string a02 = item.ReCodeSize.Split('/')[1];
               XF_SY_NAN_CodeSizeDto dto= IXF_SY_NAN_CodeSizeService.GetOne(T => T.Height == a01 );
            }

            return View();
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