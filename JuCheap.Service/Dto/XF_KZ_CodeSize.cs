using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JuCheap.Service.Dto //修改名字空间
{
    public class XF_KZ_CodeSize
    {
        [Key]
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private decimal dZ_HipLength_CP;
        public decimal DZ_HipLength_CP
        {
            get { return dZ_HipLength_CP; }
            set { dZ_HipLength_CP = value; }
        }

        private decimal sZ_HipLength_CP;
        public decimal SZ_HipLength_CP
        {
            get { return sZ_HipLength_CP; }
            set { sZ_HipLength_CP = value; }
        }

        private decimal crosspiece;
        public decimal Crosspiece
        {
            get { return crosspiece; }
            set { crosspiece = value; }
        }

        private decimal legWidth_UnderTheWaves;
        public decimal LegWidth_UnderTheWaves
        {
            get { return legWidth_UnderTheWaves; }
            set { legWidth_UnderTheWaves = value; }
        }

        private decimal frontRise_EvenWaist;
        public decimal FrontRise_EvenWaist
        {
            get { return frontRise_EvenWaist; }
            set { frontRise_EvenWaist = value; }
        }

        private decimal afterTheWaves_EvenWaist;
        public decimal AfterTheWaves_EvenWaist
        {
            get { return afterTheWaves_EvenWaist; }
            set { afterTheWaves_EvenWaist = value; }
        }

        private string netHip;
        public string NetHip
        {
            get { return netHip; }
            set { netHip = value; }
        }

        private string cP_WaistWidth;
        public string CP_WaistWidth
        {
            get { return cP_WaistWidth; }
            set { cP_WaistWidth = value; }
        }

        private string height;
        public string Height
        {
            get { return height; }
            set { height = value; }
        }

        private string longPants;
        public string LongPants
        {
            get { return longPants; }
            set { longPants = value; }
        }

        private string netWaist;
        public string NetWaist
        {
            get { return netWaist; }
            set { netWaist = value; }
        }

        private string size_Code;
        public string Size_Code
        {
            get { return size_Code; }
            set { size_Code = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        private int status;
        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}