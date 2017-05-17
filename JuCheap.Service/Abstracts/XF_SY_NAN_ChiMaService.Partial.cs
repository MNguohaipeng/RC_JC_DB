

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
    /// XF_SY_NAN_ChiMa业务契约
    /// </summary>
    public partial class XF_SY_NAN_ChiMaService : ServiceBase<XF_SY_NAN_ChiMaEntity>, IDependency, IXF_SY_NAN_ChiMaService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public XF_SY_NAN_ChiMaService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IXF_SY_NAN_ChiMaService 接口实现

		/// <summary>
		/// 添加xf_sy_nan_chima
		/// </summary>
		/// <param name="dto">xf_sy_nan_chima实体</param>
		/// <returns></returns>
		public bool Add(XF_SY_NAN_ChiMaDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加xf_sy_nan_chima
        /// </summary>
        /// <param name="dtos">xf_sy_nan_chima集合</param>
        /// <returns></returns>
        public bool Add(List<XF_SY_NAN_ChiMaDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<XF_SY_NAN_ChiMaDto>, List<XF_SY_NAN_ChiMaEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑xf_sy_nan_chima
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(XF_SY_NAN_ChiMaDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新xf_sy_nan_chima
		/// </summary>
		/// <param name="dtos">xf_sy_nan_chima实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<XF_SY_NAN_ChiMaDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<XF_SY_NAN_ChiMaDto>, IEnumerable<XF_SY_NAN_ChiMaEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除xf_sy_nan_chima
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
        /// 批量删除xf_sy_nan_chima
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 xf_sy_nan_chima 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public XF_SY_NAN_ChiMaDto GetOne(Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<XF_SY_NAN_ChiMaEntity, XF_SY_NAN_ChiMaDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 xf_sy_nan_chima
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<XF_SY_NAN_ChiMaDto> Query<OrderKeyType>(Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp, Expression<Func<XF_SY_NAN_ChiMaDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity, bool>();
				var order = orderExp.Cast<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<XF_SY_NAN_ChiMaEntity>, List<XF_SY_NAN_ChiMaDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取XF_SY_NAN_ChiMa
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<XF_SY_NAN_ChiMaDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp, Expression<Func<XF_SY_NAN_ChiMaDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity, bool>();
				var order = orderExp.Cast<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<XF_SY_NAN_ChiMaDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<XF_SY_NAN_ChiMaEntity>, List<XF_SY_NAN_ChiMaDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取XF_SY_NAN_ChiMa
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<XF_SY_NAN_ChiMaDto> GetWithPages(QueryBase queryBase, Expression<Func<XF_SY_NAN_ChiMaDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity, bool>();
				//var order = orderExp.Cast<XF_SY_NAN_ChiMaDto, XF_SY_NAN_ChiMaEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<XF_SY_NAN_ChiMaDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<XF_SY_NAN_ChiMaEntity>, List<XF_SY_NAN_ChiMaDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
