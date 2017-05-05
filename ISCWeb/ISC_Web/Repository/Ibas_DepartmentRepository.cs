/**
* 命名空间: ISC_Web.Repository
*
* 功 能： 自定义Repository类型,实现实体特有的一些方法或功能.
* 类 名： Ibas_DepartmentRepository
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
using ISC_Web.Models;
using System;
using System.Linq;

namespace ISC_Web.Repository
{
    public interface Ibas_DepartmentRepository : IRepository<bas_Department>, IDisposable
    {
        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        IQueryable<bas_Department> SelectAll();

        /// <summary>
        /// 通过用户名查找
        /// </summary>
        /// <param name="bas_DepartmentName"></param>
        /// <returns></returns>
        bas_Department SelectByName(string bas_DepartmentName);

        /// <summary>
        /// 通过ID查找
        /// </summary>
        /// <returns></returns>
        bas_Department SelectById(string id);

        /// <summary>
        /// 增加用户，参数名是bas_Department实体对象。
        /// </summary>
        /// <param name="sysbas_Department"></param>
        void Add_bas_Department(bas_Department sysbas_Department);

        /// <summary>
        /// 删除用户，根据Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteByID(string id);

        /// <summary>
        /// 删除用户，根据用户名name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool DeleteByName(string name);
    }
}