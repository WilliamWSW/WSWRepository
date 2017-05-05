/**
* 命名空间: ISC_Web.Service.Admin
*
* 功 能： N/A
* 类 名： UserManagement
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/17 12:06:27    王思伟   初版
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

namespace ISC_Web.Service.Admin
{
    public class UserManagement
    {
        public static List<VM_UserList> GetUserListByLoginName(string name)
        {

            DAL.ISCContext db = new ISCContext();
               // var result = uw.bas_User.SelectByWhere(filters: wsw => wsw.loginName.Contains((string.IsNullOrEmpty(name) ? wsw.loginName : name)));
                var result = (from user in db.bas_User join depart in db.bas_Department
                              on user.deptID equals depart.deptID
                              into temp
                              where user.userName.Contains((string.IsNullOrEmpty(name) ? user.userName : name))
                              
                              from t in temp.DefaultIfEmpty()
                              select new { userID = user.userID, loginName = user.loginName, userName = user.userName
                              , userCard = user.userCard, deptID = user.deptID, deptName = t.deptName, updateMan = user.updateMan
                              , updateTime = user.updateTime,roleID=user.roleID,telephone=user.telephone }
                              );

            //ViewModel类,存放结果.
            List<VM_UserList> ResultList = new List<VM_UserList>();
            foreach (var item in result)
            {
                VM_UserList TempObject = new VM_UserList();
                TempObject.userID = item.userID;
                TempObject.loginName = item.loginName;
                TempObject.userName = item.userName;
                TempObject.userCard = item.userCard;
                TempObject.deptID = item.deptID;
                TempObject.deptName = item.deptName;
                TempObject.updateMan = item.updateMan;
                TempObject.updateTime = item.updateTime;
                TempObject.roleID = item.roleID;
                TempObject.telephone = item.telephone;
                ResultList.Add(TempObject);
            }
                return ResultList;
            

        }


        public static int GetMaxUserID()
        {
            using (UnitOfWork uw= new UnitOfWork())
            {
                var result = uw.bas_User.SelectByWhere().Max(wsw=>wsw.userID);
                return Convert.ToInt32(result);
            }
           
        }
    }
}