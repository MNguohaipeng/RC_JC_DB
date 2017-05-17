

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
    /// XF_SY_NU_CodeSize业务契约
    /// </summary>
    public partial interface IXF_SY_NU_CodeSizeService
    {
		/// <summary>
		/// 添加xf_sy_nu_codesize
		/// </summary>
		/// <param name="xf_sy_nu_codesize">xf_sy_nu_codesize实体</param>
		/// <returns></returns>
		bool Add(XF_SY_NU_CodeSizeDto xf_sy_nu_codesize);

		/// <summary>
        /// 批量添加xf_sy_nu_codesize
        /// </summary>
        /// <param name="models">xf_sy_nu_codesize集合</param>
        /// <returns></returns>
        bool Add(List<XF_SY_NU_CodeSizeDto> models);

		/// <summary>
		/// 编辑xf_sy_nu_codesize
		/// </summary>
		/// <param name="xf_sy_nu_codesize">实体</param>
		/// <returns></returns>
		bool Update(XF_SY_NU_CodeSizeDto xf_sy_nu_codesize);

		/// <summary>
		/// 批量更新xf_sy_nu_codesize
		/// </summary>
		/// <param name="xf_sy_nu_codesizes">xf_sy_nu_codesize实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<XF_SY_NU_CodeSizeDto> xf_sy_nu_codesizes);

		/// <summary>
		/// 删除xf_sy_nu_codesize
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除xf_sy_nu_codesize
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<XF_SY_NU_CodeSizeDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 xf_sy_nu_codesize 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        XF_SY_NU_CodeSizeDto GetOne(Expression<Func<XF_SY_NU_CodeSizeDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 xf_sy_nu_codesize
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<XF_SY_NU_CodeSizeDto> Query<OrderKeyType>(Expression<Func<XF_SY_NU_CodeSizeDto, bool>> exp, Expression<Func<XF_SY_NU_CodeSizeDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取xf_sy_nu_codesize
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<XF_SY_NU_CodeSizeDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<XF_SY_NU_CodeSizeDto, bool>> exp, Expression<Func<XF_SY_NU_CodeSizeDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取xf_sy_nu_codesize
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<XF_SY_NU_CodeSizeDto> GetWithPages(QueryBase queryBase, Expression<Func<XF_SY_NU_CodeSizeDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
