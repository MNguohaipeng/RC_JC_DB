

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
    /// DCL_Data业务契约
    /// </summary>
    public partial interface IDCL_DataService
    {
		/// <summary>
		/// 添加dcl_data
		/// </summary>
		/// <param name="dcl_data">dcl_data实体</param>
		/// <returns></returns>
		bool Add(DCL_DataDto dcl_data);

		/// <summary>
        /// 批量添加dcl_data
        /// </summary>
        /// <param name="models">dcl_data集合</param>
        /// <returns></returns>
        bool Add(List<DCL_DataDto> models);

		/// <summary>
		/// 编辑dcl_data
		/// </summary>
		/// <param name="dcl_data">实体</param>
		/// <returns></returns>
		bool Update(DCL_DataDto dcl_data);

		/// <summary>
		/// 批量更新dcl_data
		/// </summary>
		/// <param name="dcl_datas">dcl_data实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<DCL_DataDto> dcl_datas);

		/// <summary>
		/// 删除dcl_data
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除dcl_data
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<DCL_DataDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 dcl_data 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        DCL_DataDto GetOne(Expression<Func<DCL_DataDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 dcl_data
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<DCL_DataDto> Query<OrderKeyType>(Expression<Func<DCL_DataDto, bool>> exp, Expression<Func<DCL_DataDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取dcl_data
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<DCL_DataDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<DCL_DataDto, bool>> exp, Expression<Func<DCL_DataDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取dcl_data
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<DCL_DataDto> GetWithPages(QueryBase queryBase, Expression<Func<DCL_DataDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
