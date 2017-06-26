

/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 06/15/2017 13:50:31
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JuCheap.Service.Dto;

namespace JuCheap.Service.Abstracts
{ 
	/// <summary>
    /// HanderDataForXF_KZ业务契约
    /// </summary>
    public partial interface IHanderDataForXF_KZService
    {
		/// <summary>
		/// 添加handerdataforxf_kz
		/// </summary>
		/// <param name="handerdataforxf_kz">handerdataforxf_kz实体</param>
		/// <returns></returns>
		bool Add(HanderDataForXF_KZDto handerdataforxf_kz);

		/// <summary>
        /// 批量添加handerdataforxf_kz
        /// </summary>
        /// <param name="models">handerdataforxf_kz集合</param>
        /// <returns></returns>
        bool Add(List<HanderDataForXF_KZDto> models);

		/// <summary>
		/// 编辑handerdataforxf_kz
		/// </summary>
		/// <param name="handerdataforxf_kz">实体</param>
		/// <returns></returns>
		bool Update(HanderDataForXF_KZDto handerdataforxf_kz);

		/// <summary>
		/// 批量更新handerdataforxf_kz
		/// </summary>
		/// <param name="handerdataforxf_kzs">handerdataforxf_kz实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<HanderDataForXF_KZDto> handerdataforxf_kzs);

		/// <summary>
		/// 删除handerdataforxf_kz
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除handerdataforxf_kz
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<HanderDataForXF_KZDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 handerdataforxf_kz 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        HanderDataForXF_KZDto GetOne(Expression<Func<HanderDataForXF_KZDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 handerdataforxf_kz
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<HanderDataForXF_KZDto> Query<OrderKeyType>(Expression<Func<HanderDataForXF_KZDto, bool>> exp, Expression<Func<HanderDataForXF_KZDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取handerdataforxf_kz
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<HanderDataForXF_KZDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<HanderDataForXF_KZDto, bool>> exp, Expression<Func<HanderDataForXF_KZDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取handerdataforxf_kz
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<HanderDataForXF_KZDto> GetWithPages(QueryBase queryBase, Expression<Func<HanderDataForXF_KZDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
