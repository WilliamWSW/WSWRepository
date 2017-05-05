/**
* 命名空间: ISC_Web.Controllers
*
* 功 能： 主界面菜单控制
* 类 名： MainController
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
using System.Web.Mvc;
using ISC_Web.Service;
using System;
using System.Web.Security;
using ISC_Web.DAL;
using System.Linq;

namespace ISC_Web.Controllers
{
    
    public class MainController : Controller
    {
        //Tree结构变量
        private string s;

        //各模块权限标记
        //货柜管理权限
        private bool StockManage = false;

        //硬件管理权限
        private bool HardWare = false;

        //基础信息管理权限
        private bool BaseInfo = false;

        //系统管理权限
        private bool SystemManage = false;
        [Authorize]
        [HandleError(View = "CustomError")]
        // GET: Main
        public ActionResult Main()
        {
            //首页权限
            ViewBag.Home = true;
            //---------------你懂的--------------------------------------
            ViewBag.Menu = JSMenu(User.Identity.Name.ToString());
            if (StockManage)
            {  //货柜管理权限
                ViewBag.Stock = true;
            }
            else
            {
                ViewBag.Stock = false;
            }
            //---------------你懂的--------------------------------------
            if (HardWare)
            { //硬件管理权限
                ViewBag.HardWare = true;
            }
            else
            {
                ViewBag.HardWare = false;
            }
            //---------------你懂的--------------------------------------
            if (BaseInfo)
            { //基础信息管理权限
                ViewBag.BaseInfo = true;
            }
            else
            {
                ViewBag.BaseInfo = false;
            }
            //---------------你懂的--------------------------------------
            if (SystemManage)
            {  //系统管理权限
                ViewBag.System = true;
            }
            else
            {
                ViewBag.System = false;
            }
            UnitOfWork uw = new UnitOfWork();
            ViewBag.User = uw.bas_User.SelectByWhere(filters: wsw => wsw.loginName == HttpContext.User.Identity.Name).FirstOrDefault().userName.ToString();
            uw.Dispose();
            return View();
        }

        [Authorize]
        [HandleError(View = "CustomError")]
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="uid">用户身份</param>
        /// <returns></returns>
        public string JSMenu(string UserName)
        {
            //调用Service业务逻辑层
            //获取用户权限
            var usercheck = Service.AuthorizationCheck.UserCheck(UserName);
            StockManage = usercheck.StockManage;
            HardWare = usercheck.HardWare;
            BaseInfo = usercheck.BaseInfo;
            SystemManage = usercheck.SystemManage;

            //测试专用，正式上线时请注释掉。
            //StockManage = true;
            //HardWare = true;
            //BaseInfo = true;
            //SystemManage = true;

            #region JSTree字符串拼装

            s = "[{";
            s += "        id:'menu',";
            s += "        homePage : 'System',";
            s += "        menu:[{";
            s += "            text: '<p class=\"font-tree-parent\">系统帮助</p>',";
            s += "            items:[";
            s += "              { id: 'System', text: '系统介绍', href: '/Main/Help', closeable: false },";
            s += "              { id: 'Support', text: '技术支持', href: '/Main/Support' },";
            s += "           ]";
            s += "        }]";
            s += "    }";
            //BMK 1.StockManage
            //如果有货柜管理权限，添加Tree相应节点。
            if (StockManage)
            {
                s += "                ,{";
                s += "                    id: 'StockManage',";
                s += "                    menu:";
                s += "                    [";
                s += "                        {";
                s += "                            text: '<p class=\"font-tree-parent\">入柜管理</p>',";
                s += "                            items:";
                s += "                            [";
                s += "                               { id: 'InstockHis', text: '入柜记录查询', href: '/InStock/InstockHis/'+hh },";
                s += "                            ]";
                s += "                        },";
                s += "                        {";
                s += "                            text: '<p class=\"font-tree-parent\">出柜管理</p>',";
                s += "                            items:";
                s += "                            [";
                s += "                                { id: 'OutstockHis', text: '出柜记录查询', href: '/OutStock/OutstockHis/'+hh },";
                s += "                            ]";
                s += "                        },";
                s += "                       {";
                s += "                           text: '<p class=\"font-tree-parent\">库存管理</p>',";
                s += "                           items:";
                s += "                           [";
                s += "                                { id: 'StockQuery', text: '库存查询', href: '/StockManage/StockQuery/'+hh },";
                s += "                                { id: 'Inventory', text: '盘点查询', href: '/StockManage/Inventory/'+hh },";
                s += "                                { id: 'GoodsLogQuery', text: '业务日志查询', href: '/StockManage/GoodsLogQuery/' },";
                s += "                           ]";
                s += "                       }";
                s += "                    ]";
                s += "                }";
            }

            //BMK 2.HardWare
            //如果有硬件管理权限，添加Tree相应节点。
            if (HardWare)
            {
                s += "                 ,{";
                s += "                     id: 'HardWare',";
                s += "                     menu:";
                s += "                     [";
                s += "                        {";
                s += "                            text: '<p class=\"font-tree-parent\">硬件管理</p>',";
                s += "                            items:";
                s += "                            [";
                s += "                              { id: 'LockAddress', text: '板锁地址管理', href: '/HardWare/LocatorSet/'+hh },";
                s += "                            ]";
                s += "                        }";
                //s += "                          {";
                //s += "                              text: '<p class=\"font-tree-parent\">RFID管理</p>',";
                //s += "                              items:";
                //s += "                              [";
                //s += "                                { id: 'RFIDSetting', text: 'RFID参数设置', href: '/Main/TempPage' }";
                //s += "                              ]";
                //s += "                          }";
                s += "                     ]";
                s += "                 }";
            }

            //BMK 3.BaseInfo
            //如果有基础信息管理权限，添加Tree相应节点。
            if (BaseInfo)
            {
                s += "                 ,{";
                s += "                     id: 'BaseInfo',";
                s += "                     menu:";
                s += "                     [";
                s += "                         {";
                s += "                             text: '<p class=\"font-tree-parent\">信息维护</p>',";
                s += "                             items:";
                s += "                             [";
                s += "                               { id: 'Goods', text: '商品目录', href: '/BaseInfo/GoodsList/'+hh },";
                s += "                               { id: 'Terminal', text: '终端设置', href: '/BaseInfo/TerminalList/'+hh },";
                s += "                               { id: 'Locator', text: '货位设置', href: '/BaseInfo/LocatorList/'+hh },";
                s += "                               { id: 'LocatorGoods', text: '商品货位关系设置', href: '/BaseInfo/LocatorGoods/'+hh },";
                s += "                               { id: 'UserFinger', text: '用户指纹管理', href: '/BaseInfo/UserFinger/'+hh }";
                s += "                             ]";
                s += "                         }";
                s += "                     ]";
                s += "                 }";
            }

            //BMK 4.SystemManage
            //如果有系统管理权限，添加Tree相应节点。
            if (SystemManage)
            {
                s += "                ,{";
                s += "                    id: 'SystemManage',";
                s += "                    menu:";
                s += "                    [";
                s += "                        {";
                s += "                            text: '<p class=\"font-tree-parent\">权限管理</p>',";
                s += "                            items:";
                s += "                            [";
                s += "                              { id: 'User', text: '用户管理', href: '/Admin/UserList/'+hh },";
                s += "                              { id: 'Role', text: '角色管理', href: '/Admin/Role/'+hh }";
                s += "                            ]";
                s += "                        },";
                s += "                        {";
                s += "                            text: '<p class=\"font-tree-parent\">部门信息管理</p>',";
                s += "                            items:";
                s += "                            [";
                s += "                              { id: 'Department', text: '科室信息维护', href: '/Admin/Department/'+hh }";
                s += "                            ]";
                s += "                        }";
                s += "                    ]";
                s += "                }";
            }
            //这一行必须得有。
            s += "              ];";
            return s;

            #endregion JSTree字符串拼装
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult AccessError()
        {
            return View();
        }
        public ActionResult TempPage()
        {
            return View();
        }

        public ActionResult Support()
        {
            return View();
        }

        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            TempData["ReturnUrl"] = Convert.ToString(Request["ReturnUrl"]);
            return View();
        }
        [HttpPost]
        [HandleError(View = "CustomError")]
        public ActionResult Login(FormCollection fc)
        {
            //获取表单提交的数据
            string user = fc["UserName"].ToString().Trim();
            string password = fc["Password"].ToString().Trim();
            bool rememberMe = fc["ckbRememberMe"] == null ? false : true;
            string returnUrl = Convert.ToString(TempData["ReturnUrl"]);
            //用户信息验证
            var result = AuthorizationCheck.LoginUserCheck(user,password);
            if (ModelState.IsValid)
            {
                if (result.Count > 0)
                {
                    FormsAuthentication.SetAuthCookie(user, rememberMe);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                    return Redirect("~/Main/Main");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                   
                   
                   
                }
                else
                {
                    ViewBag.LoginError = "用户名或密码不正确.";
                    ViewBag.CurrentTime = DateTime.Now;
                }
            }
           
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Main");
        }
    }
}