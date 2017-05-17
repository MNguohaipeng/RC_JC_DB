

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
    /// Sleeve业务契约
    /// </summary>
    public partial interface ISleeveService
    {
		/// <summary>
		/// 添加sleeve
		/// </summary>
		/// <param name="sleeve">sleeve实体</param>
		/// <returns></returns>
		bool Add(SleeveDto sleeve);

		/// <summary>
        /// 批量添加sleeve
        /// </summary>
        /// <param name="models">sleeve集合</param>
        /// <returns></returns>
        bool Add(List<SleeveDto> models);

		/// <summary>
		/// 编辑sleeve
		/// </summary>
		/// <param name="sleeve">实体</param>
		/// <returns></returns>
		bool Update(SleeveDto sleeve);

		/// <summary>
		/// 批量更新sleeve
		/// </summary>
		/// <param name="sleeves">sleeve实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<SleeveDto> sleeves);

		/// <summary>
		/// 删除sleeve
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除sleeve
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<SleeveDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 sleeve 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        SleeveDto GetOne(Expression<Func<SleeveDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 sleeve
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<SleeveDto> Query<OrderKeyType>(Expression<Func<SleeveDto, bool>> exp, Expression<Func<SleeveDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取sleeve
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<SleeveDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<SleeveDto, bool>> exp, Expression<Func<SleeveDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取sleeve
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<SleeveDto> GetWithPages(QueryBase queryBase, Expression<Func<SleeveDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
