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
    public class VM_TerminalList
    {
       
        public string terminalID { get; set; }

        public string terminalName { get; set; }
        public string terminalMAC { get; set; }
        public string terminalIP { get; set; }
        public string terminalType { get; set; }
        public string openMode { get; set; }
        public string departmentID { get; set; }
        public string updateMan { get; set; }
        public System.DateTime updateTime { get; set; }
    }
}