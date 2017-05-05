/**
* 命名空间: ISC_Web.Controllers
*
* 功 能： 系统管理模块
* 类 名： ISCController
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
using ISC_Web.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISC_Web.Controllers
{

    [CustomAuthorize]
    [HandleError(View = "CustomError")]
    /// <summary>
    /// 系统管理模板
    /// </summary>
    public class AdminController : Controller
    {
        private UnitOfWork uw = new UnitOfWork();


        #region 部门信息管理
        //部门信息查询
        public ActionResult Department(string id, string sortOrder, string Hisorder, string search, string filter, int? page, int? goback)
        {
            //获取用户屏幕分辨率，返回PageList单页行数变量。
            int PageNum = SystemSetting.GetScreen(id);
            string HisOrder = "";
            // var gobackflag = goback == 1 ? false : true;
            if (search != null && goback == null)
            {
                page = 1;
            }
            else if (filter != null)
            {
                search = filter;
            }
            if (Hisorder != null && goback != 2)
            {
                HisOrder = Hisorder;
                ViewBag.HisOrder = Hisorder;
                ViewBag.NameSortParm = Hisorder;
            }
            ViewBag.CurrentFilter = search;
            //第一次初始化排序默认为ASC
            if (sortOrder == null && HisOrder == "" && goback == null)
            {
                ViewBag.NameSortParm = "ASC";
                HisOrder = "ASC";
                ViewBag.HisOrder = "ASC";
            }
            else if (sortOrder != null && sortOrder.Equals("ASC") && goback == 1)
            {
                ViewBag.NameSortParm = "DESC";
                HisOrder = "DESC";
                ViewBag.HisOrder = "DESC";
            }
            else if (sortOrder != null && sortOrder.Equals("DESC") && goback == 1)
            {
                ViewBag.NameSortParm = "ASC";
                HisOrder = "ASC";
                ViewBag.HisOrder = "ASC";
            }
            else
            {
                ViewBag.NameSortParm = sortOrder;
                ViewBag.HisOrder = sortOrder;
            }
            //标题栏排序要按相反取值
            if (goback == 2)
            {
                HisOrder = Hisorder.Equals("ASC") ? "DESC" : "ASC";
            }
            //按关键字和排序筛选
            List<bas_Department> users = SortDisplay.SortBybas_Department_2(HisOrder, search);
            //ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
            //保存参数变量,用于页面返回时保证数据原始性.
            ViewBag.Screen = id;
            ViewBag.Order = HisOrder;
            ViewBag.HisOrder = HisOrder;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.PageNum = page;
           
            //页面显示行数设置
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        //部门详细信息
        public ActionResult DepartmentDetails(string id, string screen, string order, string search, string filter, string pagenum)
        {
            var result = uw.bas_Department.SelectByWhere(filters: wsw => wsw.deptID == id).FirstOrDefault();
            ViewBag.Screen = screen;
            ViewBag.Order = order;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.PageNum = pagenum;

            return View(result);
        }

        //编辑部门信息
        public ActionResult DepartmentEdit(string id, string screen, string order, string search, string filter, string pagenum)
        {
            var result = uw.bas_Department.SelectByWhere(filters: wsw => wsw.deptID == id).FirstOrDefault();
            ViewBag.Screen = screen;
            ViewBag.Order = order;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.PageNum = pagenum;
            //绑定valid值
            List<SelectListItem> valid = new List<SelectListItem>
            {
                  new SelectListItem {Text="已启用",Value="1" },
                  new SelectListItem {Text="未启用",Value="0" }
            };
            ViewBag.Valid = new SelectList(valid, "Value", "Text", result.valid);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentEdit(bas_Department dp, string screen, string Hisorder, string search, string filter, string pagenum, string id)
        {
            try
            {
                using (UnitOfWork uw = new UnitOfWork())
                {
                    uw.bas_Department.Update(dp);
                    uw.Commit();
                }
            }
            catch (Exception e1)
            {
                throw new Exception(e1.Message);
            }
            return RedirectToAction("Department", new { id = screen, Hisorder = Hisorder, search = search, filter = filter, page = pagenum, goback = 1 });
        }

        public ActionResult DepartmentDel(string id, string screen)
        {
            var de = uw.bas_Department.SelectByWhere(filters: wsw => wsw.deptID == id).First();
            uw.bas_Department.Delete(de);
            uw.Commit();
            uw.Dispose();
            return RedirectToAction("Department", new { id = screen });
        }

        public ActionResult Create(string screen, string order, string search, string filter, string pagenum)
        {
            ViewBag.Screen = screen;
            ViewBag.Order = order;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.PageNum = pagenum;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Create(bas_Department de, string screen, string Hisorder, string search, string filter, string pagenum)
        {
            uw.bas_Department.Insert(de);
            uw.Commit();
            uw.Dispose();
            return RedirectToAction("Department", new { id = screen, Hisorder = Hisorder, search = search, filter = filter, page = pagenum, goback = 1 });
        }

        #endregion


        #region 角色管理


        public ActionResult Role(string id, string txtrmsearch, int? page, int? goback)
        {
            //获取用户屏幕分辨率，返回PageList单页行数变量。
            int PageNum = SystemSetting.GetScreen(id);
            var result = Service.Admin.RoleManagement.GetRoleListByName(txtrmsearch);
            //保存参数变量,用于页面返回时保证数据原始性.
            ViewBag.Screen = id;
            ViewBag.Search = txtrmsearch;
            ViewBag.PageNum = page;

            //页面显示行数设置
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult RoleEdit(string id, string screen, string search, string pagenum)
        {
            var result = uw.sys_Role.SelectByWhere(filters: wsw => wsw.roleID == id).FirstOrDefault();
            if (result == null)
            {
                return HttpNotFound();
            }
            //传递用户查询条件属性,保证返回数据的原始性
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;

            //绑定当前角色下所有用户
            List<SelectListItem> listRoleUsers = new List<SelectListItem>();
            listRoleUsers = Service.Admin.RoleManagement.BindRoleUser(id);
            ViewBag.nameList = listRoleUsers;
            //获取当前角色下所有权限
            List<SelectListItem> listRoleModule = new List<SelectListItem>();
            listRoleModule = Service.Admin.RoleManagement.BindRoleModule(id);
            ViewBag.moduleList = listRoleModule;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleEdit(sys_Role vRole, string screen, string search, string pagenum,FormCollection fc)
        {
            try
            {
                using (UnitOfWork uw = new UnitOfWork())
                {
                    uw.sys_Role.Update(vRole);

                    //获取当前角色下的所有用户
                    string NameList = fc["nameList"];
                    string[] userid = { };
                    //如果为空即当前角色下没有任何用户,因此只删除数据,不插入数据.
                    if (NameList != null)
                    {
                       userid = fc["nameList"].ToString().Split(',');
                    }
                    //更新用户和角色关系,成功返回true
                     var b_RoleUser = Service.Admin.RoleManagement.UpdateRoleUser(uw, userid, vRole.roleID);


                    //获取当前角色下的所有权限
                    string ModuleList = fc["moduleList"];
                    string[] moduleid = { };
                    //如果为空即当前角色下没有任何权限,因此只删除数据,不插入数据.
                    if (ModuleList != null)
                    {
                        moduleid = fc["moduleList"].ToString().Split(',');
                    }
                    //更新用户和角色关系,成功返回true
                    var b_RoleModule = Service.Admin.RoleManagement.UpdateRoleModule(uw, moduleid, vRole.roleID,User.Identity.Name.ToString());
                    //所有对数据库的操作进行事务提交.
                    uw.Commit();
                }
            }
            catch (Exception e1)
            {
                throw new Exception(e1.Message);
            }
            return RedirectToAction("Role", new { id = screen, txtrmsearch = search, page = pagenum, goback = 1 });
        }

        public ActionResult CreateRole(string id, string screen, string search, string pagenum)
        {
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(sys_Role vRole, string screen, string search, string pagenum)
        {
            uw.sys_Role.Insert(vRole);
            uw.Commit();
            uw.Dispose();
            return RedirectToAction("Role", new { id = screen, search = search, page = pagenum, goback = 1 });
        }

        public ActionResult RoleDel(string id, string screen)
        {
            var vRole = uw.sys_Role.SelectByWhere(filters: wsw => wsw.roleID == id).First();
            uw.sys_Role.Delete(vRole);
            uw.Commit();
            uw.Dispose();
            return RedirectToAction("Role", new { id = screen });
        }
        #endregion


        #region 用户管理
        public ActionResult UserList(string id, string txtrmsearch, int? page, int? goback)
        {
            //获取用户屏幕分辨率，返回PageList单页行数变量。
            int PageNum = SystemSetting.GetScreen(id);
            var result = Service.Admin.UserManagement.GetUserListByLoginName(txtrmsearch);
            //保存参数变量,用于页面返回时保证数据原始性.
            ViewBag.Screen = id;
            ViewBag.Search = txtrmsearch;
            ViewBag.PageNum = page;

            //页面显示行数设置
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UserEdit(string id, string screen, string search, string pagenum)
        {

            var result = uw.bas_User.SelectByWhere(filters: wsw => wsw.userID == id).FirstOrDefault();
            //获取角色信息
            int vid = Convert.ToInt32(id);
           
            var list_roles = from a in uw.sys_UserRole.SelectByWhere().AsEnumerable()
                             from b in uw.sys_Role.SelectByWhere().AsEnumerable()
                             where a.SysUserID == vid && a.SysRoleID.ToString() == b.roleID
                             select new
                             {
                                 RoleName = b.roleName
                             };
            string rolename = "";
            foreach (var item in list_roles)
            {

                rolename += item.RoleName + ",";
            }
            ViewBag.Roles =rolename;
            //传递用户查询条件属性,保证返回数据的原始性
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            //部门下拉框数据
            var departmentlist = from d in new ISCContext().bas_Department
                                 orderby d.deptName
                                 select new { id = d.deptID, name = d.deptName };

            ViewBag.Depart = new SelectList(departmentlist, "id", "name", result.deptID);
            ViewBag.Entity = result;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(bas_User vUser, string screen, string search, string pagenum, string delist)
        {
            try
            {

                using (UnitOfWork uw = new UnitOfWork())
                {
                    var entity_user = uw.bas_User.SelectByWhere(filters: wsw => wsw.userID == vUser.userID).FirstOrDefault();
                    entity_user.loginName = vUser.loginName;
                    entity_user.userName = vUser.userName;
                    entity_user.userPwd = vUser.userPwd;
                    entity_user.userCard = vUser.userCard;
                    entity_user.deptID = delist;
                    entity_user.updateMan = vUser.updateMan;
                    entity_user.updateTime = vUser.updateTime;
                    entity_user.telephone = vUser.telephone;
                    uw.bas_User.Update(entity_user);
                    uw.Commit();
                }
                return RedirectToAction("UserList", new { id = screen, txtrmsearch = search, page = pagenum, goback = 1 });

            }
            catch (Exception e1)
            {
                throw new Exception(e1.Message);
            }
        }

        public ActionResult CreateUser(string id, string screen, string search, string pagenum)
        {
            //部门下拉框数据
            var departmentlist = from d in new ISCContext().bas_Department
                                 orderby d.deptName
                                 select new { id = d.deptID, name = d.deptName };

            ViewBag.Depart = new SelectList(departmentlist, "id", "name");
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            int MaxUserID = Service.Admin.UserManagement.GetMaxUserID();
            ViewBag.MaxUserID = MaxUserID + 1;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(bas_User vUser, string screen, string search, string pagenum, string delist)
        {
            vUser.spellCode = vUser.loginName;
            vUser.extUserID = vUser.userID;
            vUser.deptID = delist;
            uw.bas_User.Insert(vUser);
            uw.Commit();
            uw.Dispose();
            return RedirectToAction("UserList", new { id = screen, search = search, page = pagenum, goback = 1 });
        }

        public ActionResult UserDel(string id, string screen)
        {
            var vUser = uw.bas_User.SelectByWhere(filters: wsw => wsw.userID == id).First();
            int uid = Convert.ToInt32(id);
            //删除用户
            uw.bas_User.Delete(vUser);
            //查询用户权限
            var ur = uw.sys_UserRole.SelectByWhere(filters: wsw => wsw.SysUserID == uid).ToList();
            foreach (var item in ur)
            {
                uw.sys_UserRole.Delete(item as sys_UserRole);
            }
           
            uw.Commit();
            uw.Dispose();
            return RedirectToAction("UserList", new { id = screen });
        }
        #endregion

    }
}