

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
    /// Sleeve业务契约
    /// </summary>
    public partial class SleeveService : ServiceBase<SleeveEntity>, IDependency, ISleeveService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public SleeveService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region ISleeveService 接口实现

		/// <summary>
		/// 添加sleeve
		/// </summary>
		/// <param name="dto">sleeve实体</param>
		/// <returns></returns>
		public bool Add(SleeveDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<SleeveDto, SleeveEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加sleeve
        /// </summary>
        /// <param name="dtos">sleeve集合</param>
        /// <returns></returns>
        public bool Add(List<SleeveDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<SleeveDto>, List<SleeveEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑sleeve
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(SleeveDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<SleeveDto, SleeveEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新sleeve
		/// </summary>
		/// <param name="dtos">sleeve实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<SleeveDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<SleeveDto>, IEnumerable<SleeveEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除sleeve
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
        /// 批量删除sleeve
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<SleeveDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<SleeveDto, SleeveEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 sleeve 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public SleeveDto GetOne(Expression<Func<SleeveDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<SleeveDto, SleeveEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<SleeveEntity, SleeveDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 sleeve
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<SleeveDto> Query<OrderKeyType>(Expression<Func<SleeveDto, bool>> exp, Expression<Func<SleeveDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<SleeveDto, SleeveEntity, bool>();
				var order = orderExp.Cast<SleeveDto, SleeveEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<SleeveEntity>, List<SleeveDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取Sleeve
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<SleeveDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<SleeveDto, bool>> exp, Expression<Func<SleeveDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<SleeveDto, SleeveEntity, bool>();
				var order = orderExp.Cast<SleeveDto, SleeveEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<SleeveDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<SleeveEntity>, List<SleeveDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取Sleeve
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<SleeveDto> GetWithPages(QueryBase queryBase, Expression<Func<SleeveDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<SleeveDto, SleeveEntity, bool>();
				//var order = orderExp.Cast<SleeveDto, SleeveEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<SleeveDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<SleeveEntity>, List<SleeveDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
