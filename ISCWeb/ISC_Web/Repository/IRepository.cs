/**
* 命名空间: ISC_Web.Repository
*
* 功 能： 实现仓储管理模式,定义接口.
* 类 名： IRepository
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
using System.Linq.Expressions;
using System.Web;

namespace ISC_Web.Repository
{
    public interface IRepository<T> where T : class
    {

        /// <summary>
        /// 仓储模式的接口，用于所有对象的CRUD方法。
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// <para>数据标记为已添加，此时并没有提交到数据库。</para>
        /// 提交到数据库请使用UnitOfWork的Commit()方法;
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// <para>数据标记为已删除，此时并没有提交到数据库。</para>
        /// 提交到数据库请使用UnitOfWork的Commit()方法;
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// <para>数据标记为已更新，此时并没有提交到数据库。</para>
        /// 提交到数据库请使用UnitOfWork的Commit()方法;
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);


        /// <summary>
        /// 根据查询条件和排序方法返回结果集
        /// </summary>
        /// <param name="order">排序方法的委托，可选项</param>
        /// <param name="filters">查询条件</param>
        List<T> SelectByWhere(Func<IQueryable<T>, IOrderedQueryable<T>> order=null, params Expression<Func<T, bool>>[] filters);

       // List<T> SelectByWhere(params Expression<Func<T, bool>>[] filters);


        /// <summary>
        /// 根据开始索引（0页）、每页大小查询条件和排序方法返回分页结果集，适用于前台分页件
        /// </summary>
        /// <param name="startIndex">页面索引，从0开始</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="order">排序方法的委托，可选项</param>
        /// <param name="filters">查询条件</param>
        List<T> SelectByWherePage(int startIndex, int pageSize, Expression<Func<T, bool>> filters,Func<IQueryable<T>, IOrderedQueryable<T>> order = null);


    }
}