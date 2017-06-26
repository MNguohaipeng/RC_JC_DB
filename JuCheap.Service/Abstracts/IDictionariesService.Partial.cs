

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
    /// Dictionaries业务契约
    /// </summary>
    public partial interface IDictionariesService
    {
		/// <summary>
		/// 添加dictionaries
		/// </summary>
		/// <param name="dictionaries">dictionaries实体</param>
		/// <returns></returns>
		bool Add(DictionariesDto dictionaries);

		/// <summary>
        /// 批量添加dictionaries
        /// </summary>
        /// <param name="models">dictionaries集合</param>
        /// <returns></returns>
        bool Add(List<DictionariesDto> models);

		/// <summary>
		/// 编辑dictionaries
		/// </summary>
		/// <param name="dictionaries">实体</param>
		/// <returns></returns>
		bool Update(DictionariesDto dictionaries);

		/// <summary>
		/// 批量更新dictionaries
		/// </summary>
		/// <param name="dictionariess">dictionaries实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<DictionariesDto> dictionariess);

		/// <summary>
		/// 删除dictionaries
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除dictionaries
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<DictionariesDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 dictionaries 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        DictionariesDto GetOne(Expression<Func<DictionariesDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 dictionaries
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<DictionariesDto> Query<OrderKeyType>(Expression<Func<DictionariesDto, bool>> exp, Expression<Func<DictionariesDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取dictionaries
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<DictionariesDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<DictionariesDto, bool>> exp, Expression<Func<DictionariesDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取dictionaries
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<DictionariesDto> GetWithPages(QueryBase queryBase, Expression<Func<DictionariesDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
