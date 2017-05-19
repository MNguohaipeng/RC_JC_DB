

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
    /// XF_KZ_CodeSize业务契约
    /// </summary>
    public partial class XF_KZ_CodeSizeService : ServiceBase<XF_KZ_CodeSizeEntity>, IDependency, IXF_KZ_CodeSizeService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public XF_KZ_CodeSizeService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IXF_KZ_CodeSizeService 接口实现

		/// <summary>
		/// 添加xf_kz_codesize
		/// </summary>
		/// <param name="dto">xf_kz_codesize实体</param>
		/// <returns></returns>
		public bool Add(XF_KZ_CodeSizeDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加xf_kz_codesize
        /// </summary>
        /// <param name="dtos">xf_kz_codesize集合</param>
        /// <returns></returns>
        public bool Add(List<XF_KZ_CodeSizeDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<XF_KZ_CodeSizeDto>, List<XF_KZ_CodeSizeEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑xf_kz_codesize
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(XF_KZ_CodeSizeDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新xf_kz_codesize
		/// </summary>
		/// <param name="dtos">xf_kz_codesize实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<XF_KZ_CodeSizeDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<XF_KZ_CodeSizeDto>, IEnumerable<XF_KZ_CodeSizeEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除xf_kz_codesize
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
        /// 批量删除xf_kz_codesize
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<XF_KZ_CodeSizeDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 xf_kz_codesize 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public XF_KZ_CodeSizeDto GetOne(Expression<Func<XF_KZ_CodeSizeDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<XF_KZ_CodeSizeEntity, XF_KZ_CodeSizeDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 xf_kz_codesize
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<XF_KZ_CodeSizeDto> Query<OrderKeyType>(Expression<Func<XF_KZ_CodeSizeDto, bool>> exp, Expression<Func<XF_KZ_CodeSizeDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity, bool>();
				var order = orderExp.Cast<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<XF_KZ_CodeSizeEntity>, List<XF_KZ_CodeSizeDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取XF_KZ_CodeSize
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<XF_KZ_CodeSizeDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<XF_KZ_CodeSizeDto, bool>> exp, Expression<Func<XF_KZ_CodeSizeDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity, bool>();
				var order = orderExp.Cast<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<XF_KZ_CodeSizeDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<XF_KZ_CodeSizeEntity>, List<XF_KZ_CodeSizeDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取XF_KZ_CodeSize
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<XF_KZ_CodeSizeDto> GetWithPages(QueryBase queryBase, Expression<Func<XF_KZ_CodeSizeDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity, bool>();
				//var order = orderExp.Cast<XF_KZ_CodeSizeDto, XF_KZ_CodeSizeEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<XF_KZ_CodeSizeDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<XF_KZ_CodeSizeEntity>, List<XF_KZ_CodeSizeDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
