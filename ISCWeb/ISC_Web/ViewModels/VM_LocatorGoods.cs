using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISC_Web.ViewModels
{
    public class VM_LocatorGoods
    {
        public string terminalID { get; set; }
        public string locatorID { get; set; }
        public string extLocatorID { get; set; }
        public string goodsID { get; set; }
        public string goodsName { get; set; }
        public string batchNo { get; set; }
        public decimal maxNum { get; set; }
        public decimal minNum { get; set; }
        public string updateMan { get; set; }
        public System.DateTime updateTime { get; set; }
    }
}