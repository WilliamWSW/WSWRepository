using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISC_Web.ViewModels
{
    public class VM_GoodsLog
    {
        public int logID { get; set; }
        public string terminalID { get; set; }
        public string operationtype { get; set; }
        public string goodsID { get; set; }
        public string goodsName { get; set; }
        public string batchNo { get; set; }
        public System.DateTime expiryDate { get; set; }
        public System.DateTime productDate { get; set; }
        public string locatorID { get; set; }
        public string RFID { get; set; }
        public string barCode { get; set; }
        public decimal actualNum { get; set; }
        public string operator1 { get; set; }
        public string operator2 { get; set; }
        public string operator3 { get; set; }
        public Nullable<System.DateTime> startTime { get; set; }
        public Nullable<System.DateTime> endTime { get; set; }
        public string orderID { get; set; }
        public string detailID { get; set; }
    }
}