

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
    /// Dictionaries业务契约
    /// </summary>
    public partial class DictionariesService : ServiceBase<DictionariesEntity>, IDependency, IDictionariesService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public DictionariesService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IDictionariesService 接口实现

		/// <summary>
		/// 添加dictionaries
		/// </summary>
		/// <param name="dto">dictionaries实体</param>
		/// <returns></returns>
		public bool Add(DictionariesDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<DictionariesDto, DictionariesEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加dictionaries
        /// </summary>
        /// <param name="dtos">dictionaries集合</param>
        /// <returns></returns>
        public bool Add(List<DictionariesDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<DictionariesDto>, List<DictionariesEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑dictionaries
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(DictionariesDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<DictionariesDto, DictionariesEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新dictionaries
		/// </summary>
		/// <param name="dtos">dictionaries实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<DictionariesDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<DictionariesDto>, IEnumerable<DictionariesEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除dictionaries
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
        /// 批量删除dictionaries
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<DictionariesDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DictionariesDto, DictionariesEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 dictionaries 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public DictionariesDto GetOne(Expression<Func<DictionariesDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DictionariesDto, DictionariesEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<DictionariesEntity, DictionariesDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 dictionaries
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<DictionariesDto> Query<OrderKeyType>(Expression<Func<DictionariesDto, bool>> exp, Expression<Func<DictionariesDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DictionariesDto, DictionariesEntity, bool>();
				var order = orderExp.Cast<DictionariesDto, DictionariesEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<DictionariesEntity>, List<DictionariesDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取Dictionaries
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<DictionariesDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<DictionariesDto, bool>> exp, Expression<Func<DictionariesDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DictionariesDto, DictionariesEntity, bool>();
				var order = orderExp.Cast<DictionariesDto, DictionariesEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<DictionariesDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<DictionariesEntity>, List<DictionariesDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取Dictionaries
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<DictionariesDto> GetWithPages(QueryBase queryBase, Expression<Func<DictionariesDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DictionariesDto, DictionariesEntity, bool>();
				//var order = orderExp.Cast<DictionariesDto, DictionariesEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<DictionariesDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<DictionariesEntity>, List<DictionariesDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
