/**
* 命名空间: ISC_Web.Service
*
* 功 能： 验证页面访问权限,避免用户恶意跳转未授权页面.
* 类 名： CustomAuthorizeAttribute
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/24 10:47:53    王思伟   初版
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
using System.Web.Mvc;
using ISC_Web.DAL;
using ISC_Web.Models;
using System.Data.SqlClient;
using System.Web.Security;

namespace ISC_Web.Service
{
   
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        ISCContext db = new ISCContext();
        private string[] AuthRoles { get; set; }

       
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            //获取设定的action允许的角色
            string roles = Service.Admin.AccessActionRoles.GetControllesRoles(controllerName);
            if (!string.IsNullOrWhiteSpace(roles))
            {
                this.AuthRoles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                this.AuthRoles = new string[] { };
            }
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                FormsAuthentication.SignOut();
                //return RedirectToAction("Login", "Main");
                filterContext.Result = new RedirectResult("/Main/AccessError/");
            }
        }

       
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (AuthRoles == null || AuthRoles.Length == 0)
            {
                return true;
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.StatusCode = 403;
                return false;
            }

            //确定当前用户角色是否属于指定的角色
            #region 确定当前用户角色是否属于指定的角色
            //1. 获取用户所在角色 
            string query = @"  SELECT roleName from sys_Role
                                WHERE roleID IN
                                ( SELECT SysRoleID FROM [Sys_UserRole] 
                                WHERE [SysUserID] =(
                                SELECT a.userID  FROM [dbo].bas_User a
                                WHERE a.loginName=@userName) )";
            string currentUser = httpContext.User.Identity.Name;

            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@userName",currentUser)
            };
            var userRoles = db.Database.SqlQuery<string>(query, paras).ToList();

            //2. 验证是否属于 AuthRoles
            for (int i = 0; i < AuthRoles.Length; i++)
            {
                if (userRoles.Contains(AuthRoles[i]))
                {
                    return true;
                }
            }
            #endregion
            httpContext.Response.StatusCode = 403;
            return false;
        }
    }
}