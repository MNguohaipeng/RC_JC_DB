using JuCheap.Service.Abstracts;
using JuCheap.Service.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JuCheap.Web
{
    public class GenerateExcel
    {

        public IXF_KZ_CodeSizeService XF_KZ_CodeSizeService { get; set; }

        //生成西服上衣男的excel
        public DataRow GenerateExcelForXF_SY_NAN(DCL_DataDto GDData,DataTable table,string Size_Code)
        {

            decimal height_kz_nu = Convert.ToDecimal(GDData.ReCodeSize.ToString().Split('/')[0]);

            string a02 = GDData.ReCodeSize.ToString().Split('/')[1];

            string temp = a02.Substring(a02.Length - 1, 1);

            string Code = a02.Substring(0, a02.Length - 1);

            XF_KZ_CodeSizeDto dto = XF_KZ_CodeSizeService.GetOne(T => T.CP_WaistWidth.Contains(Code) && T.Code == temp && T.Size_Code == Size_Code);

            if (dto != null)
            {

                DataRow row = table.NewRow();

                row["身高"] = height_kz_nu;

                row["归码后尺码"] = GDData.ReCodeSize;

                row["姓名"] = GDData.Name;

                row["数量"] = GDData.Number;

                row["双褶臀围"] = dto.SZ_HipLength_CP;

                row["单褶臀围"] = dto.DZ_HipLength_CP;

                row["腰"] = Code;


                return row;
            }
            else {
                return null;

            }


        }

    }
}