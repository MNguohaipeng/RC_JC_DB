

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
    /// HeightKuChang业务契约
    /// </summary>
    public partial class HeightKuChangService : ServiceBase<HeightKuChangEntity>, IDependency, IHeightKuChangService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public HeightKuChangService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IHeightKuChangService 接口实现

		/// <summary>
		/// 添加heightkuchang
		/// </summary>
		/// <param name="dto">heightkuchang实体</param>
		/// <returns></returns>
		public bool Add(HeightKuChangDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<HeightKuChangDto, HeightKuChangEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加heightkuchang
        /// </summary>
        /// <param name="dtos">heightkuchang集合</param>
        /// <returns></returns>
        public bool Add(List<HeightKuChangDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<HeightKuChangDto>, List<HeightKuChangEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑heightkuchang
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(HeightKuChangDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<HeightKuChangDto, HeightKuChangEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新heightkuchang
		/// </summary>
		/// <param name="dtos">heightkuchang实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<HeightKuChangDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<HeightKuChangDto>, IEnumerable<HeightKuChangEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除heightkuchang
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
        /// 批量删除heightkuchang
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<HeightKuChangDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HeightKuChangDto, HeightKuChangEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 heightkuchang 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public HeightKuChangDto GetOne(Expression<Func<HeightKuChangDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HeightKuChangDto, HeightKuChangEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<HeightKuChangEntity, HeightKuChangDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 heightkuchang
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<HeightKuChangDto> Query<OrderKeyType>(Expression<Func<HeightKuChangDto, bool>> exp, Expression<Func<HeightKuChangDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HeightKuChangDto, HeightKuChangEntity, bool>();
				var order = orderExp.Cast<HeightKuChangDto, HeightKuChangEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<HeightKuChangEntity>, List<HeightKuChangDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取HeightKuChang
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<HeightKuChangDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<HeightKuChangDto, bool>> exp, Expression<Func<HeightKuChangDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HeightKuChangDto, HeightKuChangEntity, bool>();
				var order = orderExp.Cast<HeightKuChangDto, HeightKuChangEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<HeightKuChangDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<HeightKuChangEntity>, List<HeightKuChangDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取HeightKuChang
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<HeightKuChangDto> GetWithPages(QueryBase queryBase, Expression<Func<HeightKuChangDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<HeightKuChangDto, HeightKuChangEntity, bool>();
				//var order = orderExp.Cast<HeightKuChangDto, HeightKuChangEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<HeightKuChangDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<HeightKuChangEntity>, List<HeightKuChangDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
