/**
* 命名空间: ISC_Web.ViewModels
*
* 功 能： N/A
* 类 名： GoodsList
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/19  17:44:47    吴希超   初版
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
    public class VM_GoodsList
    {
       
        public string goodsID { get; set; }
        public string extGoodsID { get; set; }
        public string goodsName { get; set; }
        public string tradeName { get; set; }
        public string factory { get; set; }
        public string spec { get; set; }
        public string unit { get; set; }
        public string spellCode { get; set; }
        public string goodsType { get; set; }
        public string extGoodsType { get; set; }
        public string goodsCode { get; set; }
        public string supervisionCode { get; set; }
        public string valid { get; set; }
        public string updateMan { get; set; }
        public System.DateTime updateTime { get; set; }

    }
}