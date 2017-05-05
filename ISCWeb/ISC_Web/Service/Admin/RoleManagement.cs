/**
* 命名空间: ISC_Web.Service.Admin
*
* 功 能： N/A
* 类 名： RoleManagement
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/14 10:57:07    王思伟   初版
*
* Copyright (c) 2017 CQP Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　重庆医药(集团)股份有限公司-信息中心系统研发部　　　　　　　　　　 │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISC_Web.Models;
using ISC_Web.DAL;
using ISC_Web.ViewModels;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ISC_Web.Service.Admin
{
    public class RoleManagement
    {
        public static List<sys_Role> GetRoleListByName(string name)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var result = uw.sys_Role.SelectByWhere(filters: wsw => wsw.roleName.Contains((string.IsNullOrEmpty(name) ? wsw.roleName : name)));
                return result;
            }

        }

        public static List<sys_Role>GetAllRole()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var result = uw.sys_Role.GetAll().ToList<sys_Role>();
             
                return result;
            }
        }
        /// <summary>
        /// 给角色和用户界面的listbox绑定数据源
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        
        public static List<SelectListItem> BindRoleUser(string id)
        {
            ISCContext db = new ISCContext();
           
            string query = @"SELECT distinct u.userID,u.loginName,u.userName,Selected=(
                                        case when ur.SysUserID in 
                                        (SELECT SysUserID FROM [dbo].[sys_UserRole] where SysRoleID=@id ) 
                                        then 'true'
                                        else 'false'
                                        end)
                                        FROM  [dbo].[bas_User] u LEFT JOIN dbo.[sys_UserRole] ur
                                        ON u.userID=ur.SysUserID";
            SqlParameter[] paras = {
                            new SqlParameter("@id",id)
                        };
            List<VM_TempUser> users = db.Database.SqlQuery<VM_TempUser>(query, paras).ToList();
            var test = (from u in users
                        select new SelectListItem
                        {
                            Text = u.loginName+"  "+u.userName,
                            Value = u.userID,
                            Selected = Convert.ToBoolean(u.Selected)
                        });
            List<SelectListItem> listRoleUsers = new List<SelectListItem>();
            listRoleUsers.AddRange(test);
            return listRoleUsers;

        }
        /// <summary>
        /// 给角色和权限界面绑定数据源
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        //
        public static List<SelectListItem> BindRoleModule(string id)
        {
            ISCContext db = new ISCContext();
            
            string query = @"SELECT distinct u.moduleID,u.moduleName,Selected=(
                                        case when ur.moduleID in 
                                        (SELECT moduleID FROM [dbo].[sys_RoleModule] where roleID=@id ) 
                                        then 'true'
                                        else 'false'
                                        end)
                                        FROM  [dbo].sys_Module u LEFT JOIN dbo.[sys_RoleModule] ur
                                        ON u.moduleID=ur.moduleID";
            SqlParameter[] paras = {
                            new SqlParameter("@id",id)
                        };
            List<VM_TempModule> users = db.Database.SqlQuery<VM_TempModule>(query, paras).ToList();
            var test = (from u in users
                        select new SelectListItem
                        {
                            Text = u.moduleName,
                            Value = u.moduleID,
                            Selected = Convert.ToBoolean(u.Selected)
                        });
            List<SelectListItem> listRoleModule = new List<SelectListItem>();
            listRoleModule.AddRange(test);
            return listRoleModule;

        }
        /// <summary>
        /// 角色和用户界面的数据更新到数据库
        /// </summary>
        /// <param name="uw">EF database context</param>
        /// <param name="namelist">当前角色下的所有用户ID列表</param>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        //
        public static bool UpdateRoleUser(UnitOfWork uw, string[] namelist, string id)
        {

           
            
            //删除角色关联的所有用户,然后再插入最新的用户列表到角色
            if (id != null)
            {
                int vroleID = Convert.ToInt32(id);
                //删除角色下面所有用户
                var temp = uw.sys_UserRole.SelectByWhere(filters: wsw => wsw.SysRoleID == vroleID).ToList();
                if (temp.Count > 0)
                {
                    foreach (var item in temp)
                    {
                        uw.sys_UserRole.Delete(item);

                    }
                }

                //插入新值
                for (int i = 0; i < namelist.Length; i++)
                {
                    sys_UserRole NewUserRole = new sys_UserRole();
                    NewUserRole.SysRoleID = Convert.ToInt32(id);
                    NewUserRole.SysUserID = Convert.ToInt32(namelist[i].ToString().Trim());
                    NewUserRole.updateTime = DateTime.Now;
                    uw.sys_UserRole.Insert(NewUserRole);

                }
            }
            return true;
        }
        /// <summary>
        /// 角色和权限界面的数据更新到数据库
        /// </summary>
        /// <param name="uw">EF database context</param>
        /// <param name="Modulelist">当前角色下所有权限ID</param>
        /// <param name="id">角色ID</param>
        /// <param name="updateman">更新人员名称</param>
        /// <returns></returns>
        public static bool UpdateRoleModule(UnitOfWork uw, string[] Modulelist, string id,string updateman)
        {



            //删除角色关联的所有用户,然后再插入最新的用户列表到角色
            if (id != null)
            {
               // int vroleID = Convert.ToInt32(id);
                //删除角色下面所有权限
                var temp = uw.sys_RoleModule.SelectByWhere(filters: wsw => wsw.roleID == id).ToList();
                if (temp.Count > 0)
                {
                    foreach (var item in temp)
                    {
                        uw.sys_RoleModule.Delete(item);

                    }
                }

                //插入新值
                for (int i = 0; i < Modulelist.Length; i++)
                {
                    sys_RoleModule NewRoleModule = new sys_RoleModule();
                    NewRoleModule.roleID = id;
                    NewRoleModule.moduleID = Modulelist[i].ToString().Trim();
                    NewRoleModule.valid = "1";
                    NewRoleModule.updateMan = updateman;
                    NewRoleModule.updateTime = DateTime.Now;
                    uw.sys_RoleModule.Insert(NewRoleModule);

                }
            }
            return true;
        }
    }
}