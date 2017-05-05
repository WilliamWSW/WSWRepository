/**
* 命名空间: ISC_Web.Controllers
*
* 功 能： 硬件管理模块
* 类 名： HardWareController
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
using System.Web;
using System.Web.Mvc;
using ISC_Web.Repository;
using ISC_Web.DAL;
using ISC_Web.Models;
using ISC_Web.Service;
using PagedList;

namespace ISC_Web.Controllers
{
    [CustomAuthorize]
    [HandleError(View = "CustomError")]
    /// <summary>
    /// 硬件管理模块
    /// </summary>
    public class HardWareController : Controller
    {
        private UnitOfWork uw = new UnitOfWork();

        // GET: HardWare
        public ActionResult LocatorSet(string id, string sortOrder, string Hisorder, string search, string filter, int? page, int? goback)
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
            List<sys_LocatorSet> users = SortDisplay.SortBysys_LocatorSet_1(HisOrder, search);
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
        public ActionResult LocatorSetDetails(string id1, string id2, string screen, string order, string search, string filter, string pagenum)
        {
            var result = uw.sys_LocatorSet.SelectByWhere(filters: wsw => wsw.terminalID == id1 && wsw.locatorID == id2).FirstOrDefault();
            ViewBag.Screen = screen;
            ViewBag.Order = order;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.PageNum = pagenum;

            return View(result);
        }
        //编辑部门信息
        public ActionResult LocatorSetEdit(string id1, string id2, string screen, string order, string search, string filter, string pagenum)
        {
            var result = uw.sys_LocatorSet.SelectByWhere(filters: wsw => wsw.terminalID == id1 && wsw.locatorID == id2).FirstOrDefault();
            ViewBag.Screen = screen;
            ViewBag.Order = order;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            //绑定valid值
            List<SelectListItem> valid = new List<SelectListItem>
            {
                  new SelectListItem {Text="已启用",Value="1" },
                  new SelectListItem {Text="未启用",Value="0" }
            };
            ViewBag.Valid = new SelectList(valid, "Value", "Text", result.valid);
            ViewBag.PageNum = pagenum;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocatorSetEdit(sys_LocatorSet dp, string screen, string Hisorder, string search, string filter, string pagenum, string id)
        {

            try
            {
                using (UnitOfWork uw = new UnitOfWork())
                {

                    uw.sys_LocatorSet.Update(dp);
                    uw.Commit();
                }
            }
            catch (Exception e1)
            {

                throw new Exception(e1.Message);
            }
            return RedirectToAction("LocatorSet", new { id = screen, Hisorder = Hisorder, search = search, filter = filter, page = pagenum, goback = 1 });
        }



        public ActionResult Index()
        {
            return View();
        }
    }
}