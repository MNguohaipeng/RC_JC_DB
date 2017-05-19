

/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 05/18/2017 17:20:53
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
    /// XF_KZ_CodeSize业务契约
    /// </summary>
    public partial interface IXF_KZ_CodeSizeService
    {
		/// <summary>
		/// 添加xf_kz_codesize
		/// </summary>
		/// <param name="xf_kz_codesize">xf_kz_codesize实体</param>
		/// <returns></returns>
		bool Add(XF_KZ_CodeSizeDto xf_kz_codesize);

		/// <summary>
        /// 批量添加xf_kz_codesize
        /// </summary>
        /// <param name="models">xf_kz_codesize集合</param>
        /// <returns></returns>
        bool Add(List<XF_KZ_CodeSizeDto> models);

		/// <summary>
		/// 编辑xf_kz_codesize
		/// </summary>
		/// <param name="xf_kz_codesize">实体</param>
		/// <returns></returns>
		bool Update(XF_KZ_CodeSizeDto xf_kz_codesize);

		/// <summary>
		/// 批量更新xf_kz_codesize
		/// </summary>
		/// <param name="xf_kz_codesizes">xf_kz_codesize实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<XF_KZ_CodeSizeDto> xf_kz_codesizes);

		/// <summary>
		/// 删除xf_kz_codesize
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除xf_kz_codesize
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<XF_KZ_CodeSizeDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 xf_kz_codesize 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        XF_KZ_CodeSizeDto GetOne(Expression<Func<XF_KZ_CodeSizeDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 xf_kz_codesize
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<XF_KZ_CodeSizeDto> Query<OrderKeyType>(Expression<Func<XF_KZ_CodeSizeDto, bool>> exp, Expression<Func<XF_KZ_CodeSizeDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取xf_kz_codesize
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<XF_KZ_CodeSizeDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<XF_KZ_CodeSizeDto, bool>> exp, Expression<Func<XF_KZ_CodeSizeDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取xf_kz_codesize
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<XF_KZ_CodeSizeDto> GetWithPages(QueryBase queryBase, Expression<Func<XF_KZ_CodeSizeDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
