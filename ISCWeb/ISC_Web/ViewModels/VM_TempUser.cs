/**
* 命名空间: ISC_Web.ViewModels
*
* 功 能： N/A
* 类 名： TempUser
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/18 16:16:25    王思伟   初版
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

namespace ISC_Web.ViewModels
{
    public class VM_TempUser
    {
        public string userID { get; set; }
        public string loginName { get; set; }
        public string userName { get; set; }
        public string Selected { get; set; }
    }
}