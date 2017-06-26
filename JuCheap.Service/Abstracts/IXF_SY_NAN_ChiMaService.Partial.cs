

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
    /// XF_SY_NAN_ChiMa业务契约
    /// </summary>
    public partial interface IXF_SY_NAN_ChiMaService
    {
		/// <summary>
		/// 添加xf_sy_nan_chima
		/// </summary>
		/// <param name="xf_sy_nan_chima">xf_sy_nan_chima实体</param>
		/// <returns></returns>
		bool Add(XF_SY_NAN_ChiMaDto xf_sy_nan_chima);

		/// <summary>
        /// 批量添加xf_sy_nan_chima
        /// </summary>
        /// <param name="models">xf_sy_nan_chima集合</param>
        /// <returns></returns>
        bool Add(List<XF_SY_NAN_ChiMaDto> models);

		/// <summary>
		/// 编辑xf_sy_nan_chima
		/// </summary>
		/// <param name="xf_sy_nan_chima">实体</param>
		/// <returns></returns>
		bool Update(XF_SY_NAN_ChiMaDto xf_sy_nan_chima);

		/// <summary>
		/// 批量更新xf_sy_nan_chima
		/// </summary>
		/// <param name="xf_sy_nan_chimas">xf_sy_nan_chima实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<XF_SY_NAN_ChiMaDto> xf_sy_nan_chimas);

		/// <summary>
		/// 删除xf_sy_nan_chima
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除xf_sy_nan_chima
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 xf_sy_nan_chima 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        XF_SY_NAN_ChiMaDto GetOne(Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 xf_sy_nan_chima
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<XF_SY_NAN_ChiMaDto> Query<OrderKeyType>(Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp, Expression<Func<XF_SY_NAN_ChiMaDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取xf_sy_nan_chima
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<XF_SY_NAN_ChiMaDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp, Expression<Func<XF_SY_NAN_ChiMaDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取xf_sy_nan_chima
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<XF_SY_NAN_ChiMaDto> GetWithPages(QueryBase queryBase, Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
