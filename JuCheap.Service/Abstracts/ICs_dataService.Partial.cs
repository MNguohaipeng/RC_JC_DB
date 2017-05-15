

/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 05/15/2017 17:02:40
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
    /// Cs_data业务契约
    /// </summary>
    public partial interface ICs_dataService
    {
		/// <summary>
		/// 添加cs_data
		/// </summary>
		/// <param name="cs_data">cs_data实体</param>
		/// <returns></returns>
		bool Add(Cs_dataDto cs_data);

		/// <summary>
        /// 批量添加cs_data
        /// </summary>
        /// <param name="models">cs_data集合</param>
        /// <returns></returns>
        bool Add(List<Cs_dataDto> models);

		/// <summary>
		/// 编辑cs_data
		/// </summary>
		/// <param name="cs_data">实体</param>
		/// <returns></returns>
		bool Update(Cs_dataDto cs_data);

		/// <summary>
		/// 批量更新cs_data
		/// </summary>
		/// <param name="cs_datas">cs_data实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<Cs_dataDto> cs_datas);

		/// <summary>
		/// 删除cs_data
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除cs_data
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<Cs_dataDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 cs_data 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        Cs_dataDto GetOne(Expression<Func<Cs_dataDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 cs_data
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<Cs_dataDto> Query<OrderKeyType>(Expression<Func<Cs_dataDto, bool>> exp, Expression<Func<Cs_dataDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取cs_data
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<Cs_dataDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<Cs_dataDto, bool>> exp, Expression<Func<Cs_dataDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取cs_data
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<Cs_dataDto> GetWithPages(QueryBase queryBase, Expression<Func<Cs_dataDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
