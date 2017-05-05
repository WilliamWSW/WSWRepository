/**
* 命名空间: ISC_Web.Repository
*
* 功 能： 实现仓储管理模式,实现IRepository接口.
* 类 名： Repository
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/2 1:03:12    王思伟   初版
*
* Copyright (c) 2017 CQP Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：重庆医药(集团)股份有限公司-信息中心系统研发部    　　　 │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using ISC_Web.DAL;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ISC_Web.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext dbContext;
        protected DbSet<T> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// 不建议使用此方法，请使用SelectByWhere方法，按条件返回结果集。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// <para>数据标记为已添加，此时并没有提交到数据库。</para>
        /// 提交到数据库请使用UnitOfWork的Commit()方法;
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// <para>数据标记为已更新，此时并没有提交到数据库。</para>
        /// 提交到数据库请使用UnitOfWork的Commit()方法;
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// <para>数据标记为已删除，此时并没有提交到数据库。</para>
        /// 提交到数据库请使用UnitOfWork的Commit()方法;
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        /// <summary>
        /// 根据查询条件和排序方法返回结果集
        /// </summary>
        /// <param name="order">排序方法的委托，可选项</param>
        /// <param name="filters">查询条件</param>
        public List<T> SelectByWhere(Func<IQueryable<T>, IOrderedQueryable<T>> order = null, params Expression<Func<T, bool>>[] filters)
        {
            try
            {
                IQueryable<T> rs = dbSet.AsQueryable();
                if (filters != null)
                {
                    foreach (var filter in filters)
                    {
                        if (filter != null)
                        {
                            rs = rs.Where(filter);
                        }
                    }
                }
                if (order != null)
                {
                    rs = order(rs);
                }
                return rs.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 根据查询条件返回结果集
        /// </summary>
        /// <param name="filters">查询条件</param>
        //public List<T> SelectByWhere(params Expression<Func<T, bool>>[] filters)
        //{
        //    try
        //    {
        //        IQueryable<T> rs = dbSet.AsQueryable();
        //        if (filters != null)
        //        {
        //            foreach (var filter in filters)
        //            {
        //                if (filter != null)
        //                {
        //                    rs = rs.Where(filter);
        //                }
        //            }
        //        }

        //        return rs.ToList();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// 根据开始索引（0页）、每页大小查询条件和排序方法返回分页结果集，适用于前台分页件
        /// </summary>
        /// <param name="startIndex">页面索引，从0页开始</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="order">排序方法的委托,可选项</param>
        /// <param name="filters">查询条件</param>
        public List<T> SelectByWherePage(int startIndex, int pageSize, Expression<Func<T, bool>> filters, Func<IQueryable<T>, IOrderedQueryable<T>> order = null)
        {
            try
            {
                IQueryable<T> rs = order(dbSet.Where(filters));
                if (startIndex < 0 || pageSize < 1)
                {
                    return rs.ToList();
                }
                else
                {
                    return rs.Skip(startIndex * pageSize).Take(pageSize).ToList();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}