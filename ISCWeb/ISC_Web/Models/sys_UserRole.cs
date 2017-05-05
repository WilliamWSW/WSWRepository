/**
* 命名空间: ISC_Web.Models
*
* 功 能： N/A
* 类 名： sys_UserRole
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/19 9:30:54    王思伟   初版
*
* Copyright (c) 2017 CQP Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　重庆医药(集团)股份有限公司-信息中心系统研发部　　　　　　　　　　 │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISC_Web.Models
{
    public class sys_UserRole
    {
        [Key]
        [Column(Order = 1)]
        public int SysUserID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int SysRoleID { get; set; }
       
        public System.DateTime updateTime { get; set; }
    }
}