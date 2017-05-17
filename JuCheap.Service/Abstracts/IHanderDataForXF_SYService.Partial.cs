

/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 05/17/2017 14:38:35
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
    /// HanderDataForXF_SY业务契约
    /// </summary>
    public partial interface IHanderDataForXF_SYService
    {
		/// <summary>
		/// 添加handerdataforxf_sy
		/// </summary>
		/// <param name="handerdataforxf_sy">handerdataforxf_sy实体</param>
		/// <returns></returns>
		bool Add(HanderDataForXF_SYDto handerdataforxf_sy);

		/// <summary>
        /// 批量添加handerdataforxf_sy
        /// </summary>
        /// <param name="models">handerdataforxf_sy集合</param>
        /// <returns></returns>
        bool Add(List<HanderDataForXF_SYDto> models);

		/// <summary>
		/// 编辑handerdataforxf_sy
		/// </summary>
		/// <param name="handerdataforxf_sy">实体</param>
		/// <returns></returns>
		bool Update(HanderDataForXF_SYDto handerdataforxf_sy);

		/// <summary>
		/// 批量更新handerdataforxf_sy
		/// </summary>
		/// <param name="handerdataforxf_sys">handerdataforxf_sy实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<HanderDataForXF_SYDto> handerdataforxf_sys);

		/// <summary>
		/// 删除handerdataforxf_sy
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除handerdataforxf_sy
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<HanderDataForXF_SYDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 handerdataforxf_sy 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        HanderDataForXF_SYDto GetOne(Expression<Func<HanderDataForXF_SYDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 handerdataforxf_sy
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<HanderDataForXF_SYDto> Query<OrderKeyType>(Expression<Func<HanderDataForXF_SYDto, bool>> exp, Expression<Func<HanderDataForXF_SYDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取handerdataforxf_sy
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<HanderDataForXF_SYDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<HanderDataForXF_SYDto, bool>> exp, Expression<Func<HanderDataForXF_SYDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取handerdataforxf_sy
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<HanderDataForXF_SYDto> GetWithPages(QueryBase queryBase, Expression<Func<HanderDataForXF_SYDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
