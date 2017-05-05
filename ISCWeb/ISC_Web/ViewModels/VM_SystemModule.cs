/**
* 命名空间: ISC_Web.ViewModels
*
* 功 能： 存放主界面菜单权限
* 类 名： SystemModule
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

namespace ISC_Web.ViewModels
{
    //系统模块访问权限存储
    public class VM_SystemModule
    {
        /// <summary>
        /// 货柜管理权限
        /// </summary>
        public bool StockManage { get; set; }


        /// <summary>
        /// 硬件管理权限
        /// </summary>
        public bool HardWare { get; set; }


        /// <summary>
        /// 基础信息权限
        /// </summary>
        public bool BaseInfo { get; set; }


        /// <summary>
        /// 系统管理权限
        /// </summary>
        public bool SystemManage { get; set; }

    }
}