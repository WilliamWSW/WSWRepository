/**
* 命名空间: ISC_Web.Controllers
*
* 功 能： 基础信息管理模块
* 类 名： BaseInfoController
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
    /// 基础信息管理模块
    /// </summary>
    public class BaseInfoController : Controller
    {
        private UnitOfWork uw = new UnitOfWork();

        public ActionResult GoodsList(string id, string txtrmsearch, int? page, int? goback)
        {
            //获取用户屏幕分辨率，返回PageList单页行数变量。
            int PageNum = SystemSetting.GetScreen(id);
            var result = Service.BaseInfo.BaseInfoManagement.GetGoodsListByLoginName(txtrmsearch);
            //保存参数变量,用于页面返回时保证数据原始性.
            ViewBag.Screen = id;
            ViewBag.Search = txtrmsearch;
            ViewBag.PageNum = page;

            //页面显示行数设置
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        //部门详细信息
        public ActionResult GoodsDetails(string id, string screen, string order, string search, string filter, string pagenum)
        {
            var result = uw.bas_Goods.SelectByWhere(filters: wsw => wsw.goodsID == id).FirstOrDefault();
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
            ViewBag.Entity = result;

            return View(result);
        }

        public ActionResult GoodsEdit(string id, string screen, string search, string pagenum)
        {

            var result = uw.bas_Goods.SelectByWhere(filters: wsw => wsw.goodsID == id).FirstOrDefault();
            //传递用户查询条件属性,保证返回数据的原始性
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            //绑定valid值
            List<SelectListItem> valid = new List<SelectListItem>
            {
                  new SelectListItem {Text="已启用",Value="1" },
                  new SelectListItem {Text="未启用",Value="0" }
            };
            ViewBag.Valid = new SelectList(valid, "Value", "Text", result.valid);
            ViewBag.Entity = result;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GoodsEdit(bas_Goods vGoods, string screen, string search, string pagenum, string delist)
        {
            try
            {

                using (UnitOfWork uw = new UnitOfWork())
                {
                    var entity_goods = uw.bas_Goods.SelectByWhere(filters: wsw => wsw.goodsID == vGoods.goodsID).FirstOrDefault();
                    entity_goods.goodsID = vGoods.goodsID;
                    entity_goods.extGoodsID = vGoods.extGoodsID;
                    entity_goods.goodsName = vGoods.goodsName;
                    entity_goods.tradeName = vGoods.tradeName;
                    entity_goods.factory = vGoods.factory;
                    entity_goods.spec = vGoods.spec;
                    entity_goods.unit = vGoods.unit;
                    entity_goods.spellCode = vGoods.spellCode;
                    entity_goods.goodsType = vGoods.goodsType;
                    entity_goods.extGoodsType = vGoods.extGoodsType;
                    entity_goods.goodsCode = vGoods.goodsCode;
                    entity_goods.supervisionCode = vGoods.supervisionCode;
                    entity_goods.valid = vGoods.valid;
                    entity_goods.updateMan = vGoods.updateMan;
                    entity_goods.updateTime = vGoods.updateTime;

                    uw.bas_Goods.Update(entity_goods);
                    uw.Commit();
                }
                return RedirectToAction("GoodsList", new { id = screen, txtrmsearch = search, page = pagenum, goback = 1 });

            }
            catch (Exception e1)
            {
                throw new Exception(e1.Message);
            }
        }

        public ActionResult CreateGoods(string screen, string order, string search, string filter, string pagenum)
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
        public ActionResult CreateGoods(bas_Goods vGoods, string screen, string search, string pagenum, string delist)
        {
            vGoods.goodsID = vGoods.goodsID;
            uw.bas_Goods.Insert(vGoods);
            uw.Commit();
            return RedirectToAction("GoodsList", new { id = screen, search = search, page = pagenum, goback = 1 });
        }

        public ActionResult GoodsDel(string id, string screen)
        {
            var vGoods = uw.bas_Goods.SelectByWhere(filters: wsw => wsw.goodsID == id).First();
            uw.bas_Goods.Delete(vGoods);
            uw.Commit();
            return RedirectToAction("GoodsList", new { id = screen });
        }



        public ActionResult LocatorList(string id, string search, int? page, int? goback)
        {
            //获取用户屏幕分辨率，返回PageList单页行数变量。
            int PageNum = SystemSetting.GetScreen(id);
            var result = Service.BaseInfo.BaseInfoManagement.GetLocatorListByLoginName(search);
            //保存参数变量,用于页面返回时保证数据原始性.
            ViewBag.Screen = id;
            ViewBag.Search = search;
            ViewBag.PageNum = page;

            //页面显示行数设置
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LocatorEdit(string id1, string id2, string screen, string search, string pagenum)
        {

            var result = uw.bas_Locator.SelectByWhere(filters: wsw => wsw.terminalID == id1 && wsw.locatorID == id2).FirstOrDefault();
            //传递用户查询条件属性,保证返回数据的原始性
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            //绑定valid值
            List<SelectListItem> valid = new List<SelectListItem>
            {
                  new SelectListItem {Text="已启用",Value="1" },
                  new SelectListItem {Text="未启用",Value="0" }
            };
            ViewBag.Valid = new SelectList(valid, "Value", "Text", result.valid);
            //绑定lockType值
            List<SelectListItem> lockType = new List<SelectListItem>
            {
                  new SelectListItem {Text="不限制",Value="0" },
                  new SelectListItem {Text="单人认证",Value="1" },
                  new SelectListItem {Text="双人认证",Value="2" }
            };
            ViewBag.lockType = new SelectList(lockType, "Value", "Text", result.lockType);
            //绑定lockType值
            List<SelectListItem> locatorType = new List<SelectListItem>
            {
                  new SelectListItem {Text="",Value=" " },
                  new SelectListItem {Text="大",Value="0" },
                  new SelectListItem {Text="中",Value="1" },
                  new SelectListItem {Text="小",Value="2" }
            };
            ViewBag.locatorType = new SelectList(locatorType, "Value", "Text", result.locatorType);
            ViewBag.Entity = result;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocatorEdit(bas_Locator vLocator, string screen, string search, string pagenum, string delist)
        {
            try
            {

                using (UnitOfWork uw = new UnitOfWork())
                {
                    var entity_locator = uw.bas_Locator.SelectByWhere(filters: wsw => wsw.terminalID==vLocator.terminalID && wsw.locatorID == vLocator.locatorID).FirstOrDefault();
                    entity_locator.terminalID = vLocator.terminalID;
                    entity_locator.locatorID = vLocator.locatorID;
                    entity_locator.locatorName = vLocator.locatorName;
                    entity_locator.lockType = vLocator.lockType;
                    entity_locator.isMulti = vLocator.isMulti;
                    entity_locator.isMixBatch = vLocator.isMixBatch;
                    entity_locator.locatorType = vLocator.locatorType;
                    entity_locator.cubage = vLocator.cubage;
                    entity_locator.valid = vLocator.valid;
                    entity_locator.updateMan = vLocator.updateMan;
                    entity_locator.updateTime = vLocator.updateTime;

                    uw.bas_Locator.Update(entity_locator);
                    uw.Commit();
                }
                return RedirectToAction("LocatorList", new { id = screen, txtrmsearch = search, page = pagenum, goback = 1 });

            }
            catch (Exception e1)
            {
                throw new Exception(e1.Message);
            }
        }

        public ActionResult CreateLocator(string id1, string id2, string screen, string search, string pagenum)
        {
            //传递用户查询条件属性,保证返回数据的原始性
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            //绑定valid值
            List<SelectListItem> valid = new List<SelectListItem>
            {
                  new SelectListItem {Text="已启用",Value="1" },
                  new SelectListItem {Text="未启用",Value="0" }
            };
            ViewBag.Valid = new SelectList(valid, "Value", "Text", "0");
            //绑定lockType值
            List<SelectListItem> lockType = new List<SelectListItem>
            {
                  new SelectListItem {Text="不限制",Value="0" },
                  new SelectListItem {Text="单人认证",Value="1" },
                  new SelectListItem {Text="双人认证",Value="2" }
            };
            ViewBag.lockType = new SelectList(lockType, "Value", "Text", "0");
            //绑定lockType值
            List<SelectListItem> locatorType = new List<SelectListItem>
            {
                  new SelectListItem {Text="",Value=" " },
                  new SelectListItem {Text="大",Value="0" },
                  new SelectListItem {Text="中",Value="1" },
                  new SelectListItem {Text="小",Value="2" }
            };
            ViewBag.locatorType = new SelectList(locatorType, "Value", "Text", "");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLocator(bas_Locator vLocator, string screen, string search, string pagenum, string delist)
        {
            uw.bas_Locator.Insert(vLocator);

            var vLocarotSet = new sys_LocatorSet();
            vLocarotSet.terminalID = vLocator.terminalID;
            vLocarotSet.locatorID = vLocator.locatorID;
            vLocarotSet.plateAddress = 0;
            vLocarotSet.lockAddress = 0;
            vLocarotSet.uniqueCode = "0";
            vLocarotSet.valid = "0";
            vLocarotSet.updateMan = vLocator.updateMan;
            vLocarotSet.updateTime = vLocator.updateTime;

            uw.sys_LocatorSet.Insert(vLocarotSet);

            uw.Commit();
            return RedirectToAction("LocatorList", new { id = screen, search = search, page = pagenum, goback = 1 });
        }

        //public ActionResult GoodsDel(string id, string screen)
        //{
        //    var vGoods = uw.bas_Goods.SelectByWhere(filters: wsw => wsw.goodsID == id).First();
        //    uw.bas_Goods.Delete(vGoods);
        //    uw.Commit();
        //    return RedirectToAction("GoodsList", new { id = screen });
        //}

        public ActionResult UserFinger(string id, string txtufsearch, int? page, int? goback)
        {
            //获取用户屏幕分辨率，返回PageList单页行数变量。
            int PageNum = SystemSetting.GetScreen(id);
            var result = Service.BaseInfo.BaseInfoManagement.GetUserFingerListByUserName(txtufsearch);
            //保存参数变量,用于页面返回时保证数据原始性.
            ViewBag.Screen = id;
            ViewBag.Search = txtufsearch;
            ViewBag.PageNum = page;

            //页面显示行数设置
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UserFingerDel(string id, string screen)
        {
            int vid = Convert.ToInt32(id);
            var de = uw.bas_UserFinger.SelectByWhere(filters: wsw => wsw.ID == vid).FirstOrDefault();
            uw.bas_UserFinger.Delete(de);
            uw.Commit();
            uw.Dispose();
            return RedirectToAction("UserFinger", new { id = screen });
        }
        public ActionResult LocatorDel(string id, string locatorID, string screen)
        {
            var vLocator = uw.bas_Locator.SelectByWhere(filters: wsw => wsw.terminalID == id && wsw.locatorID == locatorID).First();
            uw.bas_Locator.Delete(vLocator);
            var vLocatorSet = uw.sys_LocatorSet.SelectByWhere(filters: wsw => wsw.terminalID == id && wsw.locatorID == locatorID);
            if (vLocatorSet.Count != 0)
            {
                var vLocatorSet_update = vLocatorSet.First();
                uw.sys_LocatorSet.Delete(vLocatorSet_update);
            }
            uw.Commit();
            return RedirectToAction("LocatorList", new { id = screen });
        }
        public ActionResult LocatorGoods(string id, string txtglsterminalid_input, string txtglsGoodsCode, string txtglsLocatorID,int? page, int? goback)
        {
            //获取用户屏幕分辨率，返回PageList单页行数变量。
            int PageNum = SystemSetting.GetScreen(id);
            var result = Service.BaseInfo.BaseInfoManagement.GetLocatorGoodsByLoginName(txtglsterminalid_input,txtglsGoodsCode, txtglsLocatorID);
            //保存参数变量,用于页面返回时保证数据原始性.
            ViewBag.Screen = id;
            ViewBag.TerminalID = txtglsterminalid_input;
            ViewBag.GoodsID = txtglsGoodsCode;
            ViewBag.LocatorID = txtglsLocatorID;
            ViewBag.PageNum = page;

            //页面显示行数设置
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult LocatorGoodsReflush(string id)
        {
            //格口下拉框数据
            var locatorIDlist = from d in new ISCContext().bas_Locator
                                where d.valid=="1" & d.terminalID == id 
                                orderby d.locatorID
                                select new { id = d.locatorID, name = d.locatorName };
            ViewBag.locatorID = new SelectList(locatorIDlist, "id", "name");

            return Json(locatorIDlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateLocatorGoods(string id, string screen, string search, string pagenum)
        {
            //终端下拉框数据
            var terminallist = from d in new ISCContext().sys_Terminal
                               orderby d.terminalID
                               select new { id = d.terminalID, name = d.terminalName };
            //药品下拉框数据
            var goodslist = from d in new ISCContext().bas_Goods
                            orderby d.goodsName
                            select new { id = d.goodsID, name = d.goodsName + ", " + d.spec };

            string result = (from d in new ISCContext().sys_Terminal
                          orderby d.terminalID
                          select d.terminalID).FirstOrDefault();

            //格口下拉框数据
            var locatorIDlist = from d in new ISCContext().bas_Locator 
                                where d.terminalID== result & d.valid == "1"
                                orderby d.locatorID
                            select new { id = d.locatorID, name = d.locatorName};

            ViewBag.Terminal = new SelectList(terminallist, "id", "name");
            ViewBag.locatorID = new SelectList(locatorIDlist, "id", "name");
            ViewBag.Goods = new SelectList(goodslist, "id", "name");
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            //int MaxUserID = Service.Admin.UserManagement.GetMaxUserID();
            //ViewBag.MaxUserID = MaxUserID + 1;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLocatorGoods(bas_LocatorGoods vGoods, string screen, string search, string pagenum,

        string terminallist, string goodslist)
        {
            vGoods.goodsID = goodslist;
            vGoods.terminalID = terminallist;
            uw.bas_LocatorGoods.Insert(vGoods);
            uw.Commit();
            return RedirectToAction("LocatorGoods", new { id = screen, search = search, page = pagenum, goback = 1 });
        }
        public ActionResult LocatorGoodsEdit(string id,string terminalID,string locatorID, string screen, string search, string pagenum,string lotno)
        {

            var result = uw.bas_LocatorGoods.SelectByWhere(filters: wsw => wsw.goodsID == id && wsw.terminalID == terminalID && wsw.locatorID == locatorID).FirstOrDefault();
            //传递用户查询条件属性,保证返回数据的原始性
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            //部门下拉框数据
            //终端下拉框数据
            var terminallist = from d in new ISCContext().sys_Terminal
                               orderby d.terminalID
                               select new { id = d.terminalID, name = d.terminalName };
            //药品下拉框数据
            var goodslist = from d in new ISCContext().bas_Goods
                            orderby d.goodsName
                            select new { id = d.goodsID, name = d.goodsName + ", " + d.spec };
            ViewBag.Terminal = new SelectList(terminallist, "id", "name", result.terminalID);
            ViewBag.Goods = new SelectList(goodslist, "id", "name", result.goodsID);
            ViewBag.goodsID_old = id;
            ViewBag.batchNo_old = lotno;
            ViewBag.Entity = result;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocatorGoodsEdit(bas_LocatorGoods vGoods, string screen, string search, string pagenum, string

        terminallist, string goodslist,string goodsID_old, string batchNo_old)
        {
            vGoods.goodsID = goodslist;
            vGoods.terminalID = terminallist;
            try
            {
                using (UnitOfWork uw = new UnitOfWork())
                {
                    var entity_goods = uw.bas_LocatorGoods.SelectByWhere(filters: wsw => wsw.goodsID == goodsID_old & wsw.terminalID == vGoods.terminalID & wsw.locatorID == vGoods.locatorID & wsw.batchNo == batchNo_old).FirstOrDefault();
                    if (entity_goods != null)
                    {
                        entity_goods.terminalID = vGoods.terminalID;
                        entity_goods.locatorID = vGoods.locatorID;
                        entity_goods.extLocatorID = vGoods.extLocatorID;
                        entity_goods.goodsID = vGoods.goodsID;
                        entity_goods.batchNo = vGoods.batchNo;
                        entity_goods.maxNum = vGoods.maxNum;
                        entity_goods.minNum = vGoods.minNum;
                        entity_goods.updateMan = vGoods.updateMan;
                        entity_goods.updateTime = vGoods.updateTime;
                        uw.bas_LocatorGoods.Update(entity_goods);
                        uw.Commit();
                    }
                }
                return RedirectToAction("LocatorGoods", new { id = screen, txtrmsearch = search, page = pagenum, goback = 1 });
            }
            catch (Exception e1)
            {
                throw new Exception(e1.Message);
            }

        }


        public ActionResult LocatorGoodsDel(string id, string locatorid, string goodsid, string batchno)
        {
            var vGoods = uw.bas_LocatorGoods.SelectByWhere(filters: wsw => wsw.terminalID == id & wsw.batchNo == batchno &wsw.locatorID == locatorid & wsw.goodsID == goodsid).First();
            uw.bas_LocatorGoods.Delete(vGoods);
            uw.Commit();
            return RedirectToAction("LocatorGoods");
        }
        

        public ActionResult TerminalList(string id, string txtrmsearch, int? page, int? goback)
        {
            //获取用户屏幕分辨率，返回PageList单页行数变量。
            int PageNum = SystemSetting.GetScreen(id);
            var result = Service.BaseInfo.BaseInfoManagement.GeTerminalListByLoginName();
            //保存参数变量,用于页面返回时保证数据原始性.
            ViewBag.Screen = id;
            ViewBag.Search = txtrmsearch;
            ViewBag.PageNum = page;

            //页面显示行数设置
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        //部门详细信息
        public ActionResult TerminalDetails(string id, string screen, string order, string search, string filter, string pagenum)
        {
            var result = uw.sys_Terminal.SelectByWhere().FirstOrDefault();
            ViewBag.Screen = screen;
            ViewBag.Order = order;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.PageNum = pagenum;

            return View(result);
        }

        public ActionResult TerminalEdit(string id, string screen, string search, string pagenum)
        {
            var result = uw.sys_Terminal.SelectByWhere(filters: wsw => wsw.terminalID == id).FirstOrDefault();
            //传递用户查询条件属性,保证返回数据的原始性
            ViewBag.Screen = screen;
            ViewBag.Search = search;
            ViewBag.PageNum = pagenum;
            //绑定terminalType值
            List<SelectListItem> terminalType = new List<SelectListItem>
            {
                  new SelectListItem {Text="麻精",Value="1" },
                  new SelectListItem {Text="针剂",Value="2" },
                  new SelectListItem {Text="综合病区",Value="3" },
                  new SelectListItem {Text="高值耗材",Value="4" }
            };
            ViewBag.terminalType = new SelectList(terminalType, "Value", "Text", result.terminalType);
            List<SelectListItem> openMode = new List<SelectListItem>
            {
                  new SelectListItem {Text="依次扫码打开",Value="1" },
                  new SelectListItem {Text="依次自动打开",Value="2" },
                  new SelectListItem {Text="一次性打开",Value="3" }
            };
            ViewBag.openMode = new SelectList(openMode, "Value", "Text", result.openMode);

            //药品下拉框数据
            var departmentlist = from d in new ISCContext().bas_Department
                                 orderby d.deptID
                                 select new { id = d.deptID, name = d.deptName };
            ViewBag.Department = new SelectList(departmentlist, "id", "name",result.departmentID);

            ViewBag.Entity = result;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TerminalEdit(sys_Terminal vTerminal, string screen, string search, string pagenum, string departmentlist)
        {
            try
            {
                vTerminal.departmentID = departmentlist;

                using (UnitOfWork uw = new UnitOfWork())
                {
                    var entity_terminal = uw.sys_Terminal.SelectByWhere(filters: wsw => wsw.terminalID == vTerminal.terminalID).FirstOrDefault();
                    entity_terminal.terminalID = vTerminal.terminalID;
                    entity_terminal.terminalName = vTerminal.terminalName;
                    entity_terminal.terminalMAC = vTerminal.terminalMAC;
                    entity_terminal.terminalIP = vTerminal.terminalIP;
                    entity_terminal.terminalType = vTerminal.terminalType;
                    entity_terminal.openMode = vTerminal.openMode;
                    entity_terminal.departmentID = vTerminal.departmentID;
                    entity_terminal.updateMan = vTerminal.updateMan;
                    entity_terminal.updateTime = vTerminal.updateTime;

                    uw.sys_Terminal.Update(entity_terminal);
                    uw.Commit();
                }
                return RedirectToAction("TerminalList", new { id = screen, txtrmsearch = search, page = pagenum, goback = 1 });

            }
            catch (Exception e1)
            {
                throw new Exception(e1.Message);
            }
        }
        
        public ActionResult CreateTerminal(string screen, string order, string search, string filter, string pagenum)
        {
            var result = uw.sys_Terminal.SelectByWhere().FirstOrDefault();

            ViewBag.Screen = screen;
            ViewBag.Order = order;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.PageNum = pagenum;
            //绑定terminalType值
            List<SelectListItem> terminalType = new List<SelectListItem>
            {
                  new SelectListItem {Text="麻精",Value="1" },
                  new SelectListItem {Text="针剂",Value="2" },
                  new SelectListItem {Text="综合病区",Value="3" },
                  new SelectListItem {Text="高值耗材",Value="4" }
            };
            ViewBag.terminalType = new SelectList(terminalType, "Value", "Text", "0");
            List<SelectListItem> openMode = new List<SelectListItem>
            {
                  new SelectListItem {Text="依次扫码打开",Value="1" },
                  new SelectListItem {Text="依次自动打开",Value="2" },
                  new SelectListItem {Text="一次性打开",Value="3" }
            };
            ViewBag.openMode = new SelectList(openMode, "Value", "Text", "0");
            
            //药品下拉框数据
            var departmentlist = from d in new ISCContext().bas_Department
                            orderby d.deptID
                            select new { id = d.deptID,name=d.deptName };
            ViewBag.Department = new SelectList(departmentlist,"id", "name");
            ViewBag.Entity = result;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTerminal(sys_Terminal vTerminal, string screen, string search, string pagenum, string departmentlist)
        {
            vTerminal.departmentID = departmentlist;
            uw.sys_Terminal.Insert(vTerminal);
            uw.Commit();
            return RedirectToAction("TerminalList", new { id = screen, search = search, page = pagenum, goback = 1 });
        }


        public ActionResult TerminalDel(string id)
        {
            var vTerminal = uw.sys_Terminal.SelectByWhere(filters: wsw => wsw.terminalID == id ).First();
            uw.sys_Terminal.Delete(vTerminal);
            uw.Commit();
            return RedirectToAction("TerminalList");
        }

    }
}