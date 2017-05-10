using JuCheap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlSugar;
using System.Data.SqlClient;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class HandleDataController : Controller
    {
        // GET: Adm/HandleData
        public ActionResult Handle()
        {
            List<Service.RecodeHdata> list = new List<Service.RecodeHdata>();
            using (var db = SugarDao.GetInstance())
                try
                {
                    var table = db.GetList<Service.Cs_data>("select * from cs_data");

                    foreach (var item in table)
                    {
                        Service.RecodeHdata rh = new Service.RecodeHdata();

                        SqlDataReader read = db.GetReader("select *, (select Length from Sleeve where FK_CoatSize_ID=list.id and Code='S') as Sleecve_Show from (select * from XF_SY_NU_CodeSize where Height = '" + item.ReCodeSize.Split('/')[0] + "' and FrontLength = '" + item.ReCodeSize.Split('/')[1] + "' and Size_Code = 'CS-2015-5-4') as list");


                        if (read.Read())
                        {
                            rh.Yichang = Convert.ToDecimal(read["FrontLength"]);
                            list.Add(rh);
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }


            return View(list);
        }

        [HttpPost]
        public ActionResult Handle(string Size_Code)
        {

            return View();
        }

    }
}