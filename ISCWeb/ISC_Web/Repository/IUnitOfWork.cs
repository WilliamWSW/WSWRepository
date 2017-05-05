using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ISC_Web.Repository
{
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// 数据存储接口，数据最终在这里进行提交保存。
        /// </summary>
        DbContext Context { get; }


        /// <summary>
        /// 把用户的CRUD操作提交到数据库。
        /// </summary>
        void Commit();
        bool IsDisposed { get; }
    }
}