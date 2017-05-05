using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ISC_Web.ViewModels
{
    public partial class VM_Stock
    {
        [Key]
        [Column(Order = 1)]
        public string terminalID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string goodsID { get; set; }
        [Key]
        [Column(Order = 3)]
        public string batchNo { get; set; }
        public System.DateTime expiryDate { get; set; }
        public System.DateTime productDate { get; set; }
        [Key]
        [Column(Order = 4)]
        public string locatorID { get; set; }
        [Key]
        [Column(Order = 5)]
        public string RFID { get; set; }
        public string barCode { get; set; }
        public decimal bizNum { get; set; }
        public decimal actualNum { get; set; }
        public string updateMan { get; set; }
        public System.DateTime updateTime { get; set; }
        public virtual string goodsName { get; set; }
    }
}