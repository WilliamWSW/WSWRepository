using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISC_Web.ViewModels
{
    public class VM_InventoryDt
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
        public string goodsName { get; set; }
        public string batchNo { get; set; }
        public Nullable<System.DateTime> expiryDate { get; set; }
        public Nullable<System.DateTime> productDate { get; set; }
        public decimal orderNum { get; set; }
        public Nullable<decimal> actualNum { get; set; }
        public string RFIDCode { get; set; }
        public string barcode { get; set; }
        public string locatorID { get; set; }
        public string status { get; set; }
        public string updateMan { get; set; }
        public System.DateTime updateTime { get; set; }
    }
}