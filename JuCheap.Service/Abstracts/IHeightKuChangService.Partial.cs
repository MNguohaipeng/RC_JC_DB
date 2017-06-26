

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
    /// HeightKuChang业务契约
    /// </summary>
    public partial interface IHeightKuChangService
    {
		/// <summary>
		/// 添加heightkuchang
		/// </summary>
		/// <param name="heightkuchang">heightkuchang实体</param>
		/// <returns></returns>
		bool Add(HeightKuChangDto heightkuchang);

		/// <summary>
        /// 批量添加heightkuchang
        /// </summary>
        /// <param name="models">heightkuchang集合</param>
        /// <returns></returns>
        bool Add(List<HeightKuChangDto> models);

		/// <summary>
		/// 编辑heightkuchang
		/// </summary>
		/// <param name="heightkuchang">实体</param>
		/// <returns></returns>
		bool Update(HeightKuChangDto heightkuchang);

		/// <summary>
		/// 批量更新heightkuchang
		/// </summary>
		/// <param name="heightkuchangs">heightkuchang实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<HeightKuChangDto> heightkuchangs);

		/// <summary>
		/// 删除heightkuchang
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除heightkuchang
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<HeightKuChangDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 heightkuchang 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        HeightKuChangDto GetOne(Expression<Func<HeightKuChangDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 heightkuchang
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<HeightKuChangDto> Query<OrderKeyType>(Expression<Func<HeightKuChangDto, bool>> exp, Expression<Func<HeightKuChangDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取heightkuchang
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<HeightKuChangDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<HeightKuChangDto, bool>> exp, Expression<Func<HeightKuChangDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取heightkuchang
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<HeightKuChangDto> GetWithPages(QueryBase queryBase, Expression<Func<HeightKuChangDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
