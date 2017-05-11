

/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 05/10/2017 15:41:50
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
    /// Cs_data业务契约
    /// </summary>
    public partial class Cs_dataService : ServiceBase<Cs_dataEntity>, IDependency, ICs_dataService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public Cs_dataService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region ICs_dataService 接口实现

		/// <summary>
		/// 添加cs_data
		/// </summary>
		/// <param name="dto">cs_data实体</param>
		/// <returns></returns>
		public bool Add(Cs_dataDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<Cs_dataDto, Cs_dataEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加cs_data
        /// </summary>
        /// <param name="dtos">cs_data集合</param>
        /// <returns></returns>
        public bool Add(List<Cs_dataDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<Cs_dataDto>, List<Cs_dataEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑cs_data
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(Cs_dataDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<Cs_dataDto, Cs_dataEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新cs_data
		/// </summary>
		/// <param name="dtos">cs_data实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<Cs_dataDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<Cs_dataDto>, IEnumerable<Cs_dataEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除cs_data
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
        /// 批量删除cs_data
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<Cs_dataDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<Cs_dataDto, Cs_dataEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 cs_data 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public Cs_dataDto GetOne(Expression<Func<Cs_dataDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<Cs_dataDto, Cs_dataEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<Cs_dataEntity, Cs_dataDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 cs_data
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<Cs_dataDto> Query<OrderKeyType>(Expression<Func<Cs_dataDto, bool>> exp, Expression<Func<Cs_dataDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<Cs_dataDto, Cs_dataEntity, bool>();
				var order = orderExp.Cast<Cs_dataDto, Cs_dataEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<Cs_dataEntity>, List<Cs_dataDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取Cs_data
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<Cs_dataDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<Cs_dataDto, bool>> exp, Expression<Func<Cs_dataDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<Cs_dataDto, Cs_dataEntity, bool>();
				var order = orderExp.Cast<Cs_dataDto, Cs_dataEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<Cs_dataDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<Cs_dataEntity>, List<Cs_dataDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取Cs_data
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<Cs_dataDto> GetWithPages(QueryBase queryBase, Expression<Func<Cs_dataDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<Cs_dataDto, Cs_dataEntity, bool>();
				//var order = orderExp.Cast<Cs_dataDto, Cs_dataEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<Cs_dataDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<Cs_dataEntity>, List<Cs_dataDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
