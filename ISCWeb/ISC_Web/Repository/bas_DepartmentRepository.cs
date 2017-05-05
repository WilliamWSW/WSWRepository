/**
* 命名空间: ISC_Web.Repository
*
* 功 能： 自定义Repository类型,实现实体特有的一些方法或功能.
* 类 名： bas_DepartmentRepository
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
using ISC_Web.DAL;
using ISC_Web.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace ISC_Web.Repository
{
    public class bas_DepartmentRepository : Repository<bas_Department>, Ibas_DepartmentRepository
    {
        private DbContext dbContext;
        protected DbSet<bas_Department> db;

        public bas_DepartmentRepository(ISCContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.db = dbContext.Set<bas_Department>();
        }

        //查询所有用户

        public IQueryable<bas_Department> SelectAll()

        {
            return db;
        }

        //通过用户名查询用户

        public bas_Department SelectByName(string Name)

        {
            return db.FirstOrDefault(u => u.deptName == Name);
        }

        //通过ID查询用户
        public bas_Department SelectById(string id)

        {
            return db.FirstOrDefault(u => u.deptID == id);
        }

        //添加用户

        public void Add_bas_Department(bas_Department sysbas_Department)

        {
            db.Add(sysbas_Department);

            // db.SaveChanges();
        }

        //删除用户

        public bool DeleteByID(string id)

        {
            var delSysbas_Department = db.FirstOrDefault(u => u.deptID == id);

            if (delSysbas_Department != null)

            {
                db.Remove(delSysbas_Department);

                // db.SaveChanges();

                return true;
            }
            else

                return false;
        }

        public bool DeleteByName(string name)
        {
            var delsysuser = db.FirstOrDefault(o => o.deptName == name);
            if (delsysuser != null)
            {
                db.Remove(delsysuser);
                //db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    dbContext.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~bas_DepartmentRepository()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}