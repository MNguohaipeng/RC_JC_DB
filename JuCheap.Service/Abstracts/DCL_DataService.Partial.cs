

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
    /// DCL_Data业务契约
    /// </summary>
    public partial class DCL_DataService : ServiceBase<DCL_DataEntity>, IDependency, IDCL_DataService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public DCL_DataService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IDCL_DataService 接口实现

		/// <summary>
		/// 添加dcl_data
		/// </summary>
		/// <param name="dto">dcl_data实体</param>
		/// <returns></returns>
		public bool Add(DCL_DataDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<DCL_DataDto, DCL_DataEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加dcl_data
        /// </summary>
        /// <param name="dtos">dcl_data集合</param>
        /// <returns></returns>
        public bool Add(List<DCL_DataDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<DCL_DataDto>, List<DCL_DataEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑dcl_data
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(DCL_DataDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<DCL_DataDto, DCL_DataEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新dcl_data
		/// </summary>
		/// <param name="dtos">dcl_data实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<DCL_DataDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<DCL_DataDto>, IEnumerable<DCL_DataEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除dcl_data
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
        /// 批量删除dcl_data
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<DCL_DataDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DCL_DataDto, DCL_DataEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 dcl_data 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public DCL_DataDto GetOne(Expression<Func<DCL_DataDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DCL_DataDto, DCL_DataEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<DCL_DataEntity, DCL_DataDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 dcl_data
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<DCL_DataDto> Query<OrderKeyType>(Expression<Func<DCL_DataDto, bool>> exp, Expression<Func<DCL_DataDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DCL_DataDto, DCL_DataEntity, bool>();
				var order = orderExp.Cast<DCL_DataDto, DCL_DataEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<DCL_DataEntity>, List<DCL_DataDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取DCL_Data
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<DCL_DataDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<DCL_DataDto, bool>> exp, Expression<Func<DCL_DataDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DCL_DataDto, DCL_DataEntity, bool>();
				var order = orderExp.Cast<DCL_DataDto, DCL_DataEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<DCL_DataDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<DCL_DataEntity>, List<DCL_DataDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取DCL_Data
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<DCL_DataDto> GetWithPages(QueryBase queryBase, Expression<Func<DCL_DataDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<DCL_DataDto, DCL_DataEntity, bool>();
				//var order = orderExp.Cast<DCL_DataDto, DCL_DataEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<DCL_DataDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<DCL_DataEntity>, List<DCL_DataDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
