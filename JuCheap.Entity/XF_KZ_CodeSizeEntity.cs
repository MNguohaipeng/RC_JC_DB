using JuCheap.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JuCheap.Entity //�޸����ֿռ�
{
    public class XF_KZ_CodeSizeEntity:BaseEntity
    {

        public string Code { get; set; }//����

        public decimal DZ_HipLength_CP { get; set; }//������Χ

        public decimal SZ_HipLength_CP { get; set; }//˫����Χ

        public decimal Crosspiece { get; set; }//�ᵵ

        public decimal LegWidth_UnderTheWaves { get; set; }//�ȷ�    ����10CM

        public decimal FrontRise_EvenWaist { get; set; }//ǰ������

        public decimal AfterTheWaves_EvenWaist { get; set; }//��������

        public string NetHip { get; set; }//����Χ

        public string CP_WaistWidth { get; set; }//��Ʒ��Χ

        public string Height { get; set; }//���

        public string LongPants { get; set; }//�㳤

        public string NetWaist { get; set; }//����Χ

        public string Size_Code { get; set; }//������

        public int Status { get; set; }//״̬

        public string RowData { get; set; }//ƴ�ӵ�������

    }
}