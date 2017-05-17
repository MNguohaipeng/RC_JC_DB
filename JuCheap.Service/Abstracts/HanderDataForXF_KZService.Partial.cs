

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
    /// HanderDataForXF_KZ业务契约
    /// </summary>
    public partial class HanderDataForXF_KZService : ServiceBase<HanderDataForXF_KZEntity>, IDependency, IHanderDataForXF_KZService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public HanderDataForXF_KZService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IHanderDataForXF_KZService 接口实现

		/// <summary>
		/// 添加handerdataforxf_kz
		/// </summary>
		/// <param name="dto">handerdataforxf_kz实体</param>
		/// <returns></returns>
		public bool Add(HanderDataForXF_KZDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<HanderDataForXF_KZDto, HanderDataForXF_KZEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加handerdataforxf_kz
        /// </summary>
        /// <param name="dtos">handerdataforxf_kz集合</param>
        /// <returns></returns>
        public bool Add(List<HanderDataForXF_KZDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<HanderDataForXF_KZDto>, List<HanderDataForXF_KZEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑handerdataforxf_kz
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(HanderDataForXF_KZDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<HanderDataForXF_KZDto, HanderDataForXF_KZEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新handerdataforxf_kz
		/// </summary>
		/// <param name="dtos">handerdataforxf_kz实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<HanderDataForXF_KZDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<HanderDataForXF_KZDto>, IEnumerable<HanderDataForXF_KZEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除handerdataforxf_kz
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
        /// 批量删除handerdataforxf_kz
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<HanderDataForXF_KZDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_KZDto, HanderDataForXF_KZEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 handerdataforxf_kz 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public HanderDataForXF_KZDto GetOne(Expression<Func<HanderDataForXF_KZDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_KZDto, HanderDataForXF_KZEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<HanderDataForXF_KZEntity, HanderDataForXF_KZDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 handerdataforxf_kz
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<HanderDataForXF_KZDto> Query<OrderKeyType>(Expression<Func<HanderDataForXF_KZDto, bool>> exp, Expression<Func<HanderDataForXF_KZDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_KZDto, HanderDataForXF_KZEntity, bool>();
				var order = orderExp.Cast<HanderDataForXF_KZDto, HanderDataForXF_KZEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<HanderDataForXF_KZEntity>, List<HanderDataForXF_KZDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取HanderDataForXF_KZ
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<HanderDataForXF_KZDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<HanderDataForXF_KZDto, bool>> exp, Expression<Func<HanderDataForXF_KZDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_KZDto, HanderDataForXF_KZEntity, bool>();
				var order = orderExp.Cast<HanderDataForXF_KZDto, HanderDataForXF_KZEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<HanderDataForXF_KZDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<HanderDataForXF_KZEntity>, List<HanderDataForXF_KZDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取HanderDataForXF_KZ
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<HanderDataForXF_KZDto> GetWithPages(QueryBase queryBase, Expression<Func<HanderDataForXF_KZDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HanderDataForXF_KZDto, HanderDataForXF_KZEntity, bool>();
				//var order = orderExp.Cast<HanderDataForXF_KZDto, HanderDataForXF_KZEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<HanderDataForXF_KZDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<HanderDataForXF_KZEntity>, List<HanderDataForXF_KZDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
