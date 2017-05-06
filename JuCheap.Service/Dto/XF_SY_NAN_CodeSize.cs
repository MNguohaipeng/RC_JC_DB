using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Service.Dto
{
	public class XF_SY_NAN_CodeSize
	{
        private decimal id;

        public decimal ID
        {
            get { return id; }
            set { id = value; }
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

		private decimal finishedHem_SplitEnds;
		public decimal FinishedHem_SplitEnds
		{
			get { return finishedHem_SplitEnds; }
			set { finishedHem_SplitEnds = value; }
		}

		private decimal shoulderWidth;
		public decimal ShoulderWidth
		{
			get { return shoulderWidth; }
			set { shoulderWidth = value; }
		}

		private string size_Code;
		public string Size_Code
		{
			get { return size_Code; }
			set { size_Code = value; }
		}

		private string sleecve_Show;
		public string Sleecve_Show
		{
			get { return sleecve_Show; }
			set { sleecve_Show = value; }
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
