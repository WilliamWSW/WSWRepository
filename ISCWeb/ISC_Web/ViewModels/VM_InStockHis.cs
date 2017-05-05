/**
* 命名空间: ISC_Web.ViewModels
*
* 功 能： N/A
* 类 名： VM_InStockHis
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/26 10:39:22    王思伟   初版
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISC_Web.ViewModels
{
    public class VM_InStockHis
    {
        [Key]
        [Column(Order = 1)]
        public string terminalID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string orderID { get; set; }
        [Key]
        [Column(Order = 3)]
        public string detailID { get; set; }
        public string goodsID { get; set; }
        public string batchNo { get; set; }
        public System.DateTime expiryDate { get; set; }
        public System.DateTime productDate { get; set; }
        public decimal orderNum { get; set; }
        public decimal actualNum { get; set; }
        public string RFIDCode { get; set; }
        public string barcode { get; set; }
        public string locatorID { get; set; }
        public string status { get; set; }
        public string updateMan { get; set; }
        public Nullable<System.DateTime> updateTime { get; set; }
        public string outOrderID { get; set; }

        public string goodsName { get; set; }
    }
}