/**
* 命名空间: ISC_Web.ViewModels
*
* 功 能： N/A
* 类 名： UserList
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/17 14:44:47    王思伟   初版
*
* Copyright (c) 2017 CQP Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　重庆医药(集团)股份有限公司-信息中心系统研发部　　　　　　　　　　 │
*└──────────────────────────────────┘
*/

using System.ComponentModel.DataAnnotations;

namespace ISC_Web.ViewModels
{
    public class VM_LocatorList
    {
       
        public string terminalID { get; set; }

        public string locatorID { get; set; }
        public string locatorName { get; set; }
        public string lockType { get; set; }
        public string isMulti { get; set; }
        public string isMixBatch { get; set; }
        public string locatorType { get; set; }
        public decimal? cubage { get; set; }
        public string valid { get; set; }
        public string updateMan { get; set; }
        public System.DateTime updateTime { get; set; }
    }
}