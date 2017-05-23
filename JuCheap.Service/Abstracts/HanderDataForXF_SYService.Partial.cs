

/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 05/22/2017 09:24:30
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using AutoMapper;
using JuCheap.Core;
using JuCheap.Core.Extentions;
using JuCheap.Entity;
using JuCheap.Service.Dto;
using Mehdime.Entity;

namespace JuCheap.Service.Abstracts
{ 
	/// <summary>
    /// HanderDataForXF_SY业务契约
    /// </summary>
    public partial class HanderDataForXF_SYService : ServiceBase<HanderDataForXF_SYEntity>, IDependency, IHanderDataForXF_SYService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public HanderDataForXF_SYService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IHanderDataForXF_SYService 接口实现

		/// <summary>
		/// 添加handerdataforxf_sy
		/// </summary>
		/// <param name="dto">handerdataforxf_sy实体</param>
		/// <returns></returns>
		public bool Add(HanderDataForXF_SYDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<HanderDataForXF_SYDto, HanderDataForXF_SYEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加handerdataforxf_sy
        /// </summary>
        /// <param name="dtos">handerdataforxf_sy集合</param>
        /// <returns></returns>
        public bool Add(List<HanderDataForXF_SYDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<HanderDataForXF_SYDto>, List<HanderDataForXF_SYEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑handerdataforxf_sy
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(HanderDataForXF_SYDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<HanderDataForXF_SYDto, HanderDataForXF_SYEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新handerdataforxf_sy
		/// </summary>
		/// <param name="dtos">handerdataforxf_sy实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<HanderDataForXF_SYDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<HanderDataForXF_SYDto>, IEnumerable<HanderDataForXF_SYEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除handerdataforxf_sy
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		public bool Delete(int id)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);

                var model = dbSet.FirstOrDefault(item => item.Id == id);
                dbSet.Remove(model);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        /// 批量删除handerdataforxf_sy
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<HanderDataForXF_SYDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_SYDto, HanderDataForXF_SYEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 handerdataforxf_sy 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public HanderDataForXF_SYDto GetOne(Expression<Func<HanderDataForXF_SYDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_SYDto, HanderDataForXF_SYEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<HanderDataForXF_SYEntity, HanderDataForXF_SYDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 handerdataforxf_sy
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<HanderDataForXF_SYDto> Query<OrderKeyType>(Expression<Func<HanderDataForXF_SYDto, bool>> exp, Expression<Func<HanderDataForXF_SYDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_SYDto, HanderDataForXF_SYEntity, bool>();
				var order = orderExp.Cast<HanderDataForXF_SYDto, HanderDataForXF_SYEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<HanderDataForXF_SYEntity>, List<HanderDataForXF_SYDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取HanderDataForXF_SY
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<HanderDataForXF_SYDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<HanderDataForXF_SYDto, bool>> exp, Expression<Func<HanderDataForXF_SYDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_SYDto, HanderDataForXF_SYEntity, bool>();
				var order = orderExp.Cast<HanderDataForXF_SYDto, HanderDataForXF_SYEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<HanderDataForXF_SYDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<HanderDataForXF_SYEntity>, List<HanderDataForXF_SYDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取HanderDataForXF_SY
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<HanderDataForXF_SYDto> GetWithPages(QueryBase queryBase, Expression<Func<HanderDataForXF_SYDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_SYDto, HanderDataForXF_SYEntity, bool>();
				//var order = orderExp.Cast<HanderDataForXF_SYDto, HanderDataForXF_SYEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<HanderDataForXF_SYDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<HanderDataForXF_SYEntity>, List<HanderDataForXF_SYDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
