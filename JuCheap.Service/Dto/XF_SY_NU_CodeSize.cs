using System;
using System.Collections.Generic;
using System.Text;

namespace JuCheap.Service.Dto //修改名字空间
{
    public class XF_SY_NU_CodeSize 
    {

        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private decimal height;
        public decimal Height
        {
            get { return height; }
            set { height = value; }
        }

        private string frontLength;
        public string FrontLength
        {
            get { return frontLength; }
            set { frontLength = value; }
        }

        private string netBust;
        public string NetBust
        {
            get { return netBust; }
            set { netBust = value; }
        }

        private decimal finishedBust;
        public decimal FinishedBust
        {
            get { return finishedBust; }
            set { finishedBust = value; }
        }

        private decimal inWaist;
        public decimal InWaist
        {
            get { return inWaist; }
            set { inWaist = value; }
        }

        private decimal finishedHem_NoFork;
        public decimal FinishedHem_NoFork
        {
            get { return finishedHem_NoFork; }
            set { finishedHem_NoFork = value; }
        }

        private decimal sleeveWidth;
        public decimal SleeveWidth
        {
            get { return sleeveWidth; }
            set { sleeveWidth = value; }
        }

        private decimal shoulderWidth;
        public decimal ShoulderWidth
        {
            get { return shoulderWidth; }
            set { shoulderWidth = value; }
        }



        private string sleecve_Show;
        public string Sleecve_Show
        {
            get { return sleecve_Show; }
            set { sleecve_Show = value; }
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