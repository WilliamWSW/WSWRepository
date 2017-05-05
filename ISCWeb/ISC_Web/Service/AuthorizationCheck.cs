using ISC_Web.DAL;
using ISC_Web.Models;
using ISC_Web.ViewModels;

/**
* 命名空间: ISC_Web.Service
*
* 功 能： 系统权限验证
* 类 名： AuthorizationCheck
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

using System.Collections.Generic;
using System.Linq;

namespace ISC_Web.Service
{
    public class AuthorizationCheck
    {
        /// <summary>
        /// 获取用户各个模块的访问权限，用来初始化界面。
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static VM_SystemModule UserCheck(string UserName)
        {
            VM_SystemModule vSystemModule = new VM_SystemModule();
            //默认所有权限为false
            vSystemModule.StockManage = false;
            vSystemModule.HardWare = false;
            vSystemModule.SystemManage = false;
            vSystemModule.BaseInfo = false;
            ISCContext db = new ISCContext();

            //获取用户所有角色ID
            var allroles = (from u in db.bas_User
                            join ur in db.sys_UserRole on u.userID equals ur.SysUserID.ToString()
                            where u.loginName == UserName
                            select new { RoleID = ur.SysRoleID }).ToList();
            foreach (var item in allroles)
            {
                //获取每个角色下面所有module权限.
                var b_RoleModule = (from rm in db.sys_RoleModule
                                    join m in db.sys_Module on rm.moduleID equals m.moduleID
                                    where rm.roleID==item.RoleID.ToString()
                                    select new { ModuleName = m.moduleName }
                                  ).ToList();
                foreach (var mu in b_RoleModule)
                {
                    if (mu.ModuleName.ToString().Equals("货柜管理"))
                    {
                        vSystemModule.StockManage = true;
                    }
                    if (mu.ModuleName.ToString().Equals("硬件管理"))
                    {
                        vSystemModule.HardWare = true;
                    }
                    if (mu.ModuleName.ToString().Equals("系统管理"))
                    {
                        vSystemModule.SystemManage = true;
                    }
                    if (mu.ModuleName.ToString().Equals("基础信息管理"))
                    {
                        vSystemModule.BaseInfo = true;
                    }
                }
            }

            return vSystemModule;
        }

        /// <summary>
        /// 系统登录认证
        /// </summary>
        /// <param name="name">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        public static List<bas_User> LoginUserCheck(string name, string password)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var userresult = uw.bas_User.SelectByWhere(filters: wsw => wsw.loginName == name & wsw.userPwd == password);
                return userresult;
            }
        }
    }
}