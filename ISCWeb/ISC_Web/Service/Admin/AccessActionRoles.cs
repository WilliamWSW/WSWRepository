/**
* 命名空间: ISC_Web.Service.Admin
*
* 功 能： N/A
* 类 名： AccessActionRoles
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/24 16:40:59    王思伟   初版
*
* Copyright (c) 2017 CQP Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　重庆医药(集团)股份有限公司-信息中心系统研发部　　　　　　　　　　 │
*└──────────────────────────────────┘
*/

using ISC_Web.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ISC_Web.Service.Admin
{
    public class AccessActionRoles
    {
        /// <summary>
        /// 获取当前模块允许访问的角色列表
        /// </summary>
        /// <param name="Cname">模块名称</param>
        /// <returns>返回角色列表字符串</returns>
        public static string GetControllesRoles(string Cname)
        {
            string ModuleName = "";
            //允许访问的角色列表
            string RoleNameList = "";
            //为了保持原表结构,中文名和英文名必须做转换
            switch (Cname)
            {
                case "Admin":
                    ModuleName = "系统管理";
                    break;

                case "BaseInfo":
                    ModuleName = "基础信息管理";
                    break;

                case "HardWare":
                    ModuleName = "硬件管理";
                    break;

                case "InStock":
                    ModuleName = "货柜管理";
                    break;

                case "OutStock":
                    ModuleName = "货柜管理";
                    break;

                case "StockManage":
                    ModuleName = "货柜管理";
                    break;
            }
            //获取当前模块允许访问的角色列表
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Models.sys_Module> vModule = uw.sys_Module.SelectByWhere(filters: wsw => wsw.moduleName == ModuleName);
                List<Models.sys_RoleModule> vRoleModule = uw.sys_RoleModule.SelectByWhere();
                List<Models.sys_Role> vRole = uw.sys_Role.SelectByWhere();

                var result = from mo in vModule.AsEnumerable()
                             from rm in vRoleModule.AsEnumerable()
                             from ro in vRole.AsEnumerable()
                             where mo.moduleID == rm.moduleID && rm.roleID == ro.roleID
                             select new
                             {
                                 RoleName = ro.roleName
                             };
                foreach (var item in result)
                {
                    RoleNameList += item.RoleName + ",";
                }
            }
            return RoleNameList;
        }
    }
}