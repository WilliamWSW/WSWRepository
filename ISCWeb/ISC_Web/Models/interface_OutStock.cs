//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISC_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class interface_OutStock
    {
        [Key]
        public string PICKING_CODE { get; set; }
        public string STORE_ID { get; set; }
        public string LOCATOR_ID { get; set; }
        public string DRUG_ID { get; set; }
        public string DRUG_NAME { get; set; }
        public string SPEC { get; set; }
        public string UNIT { get; set; }
        public Nullable<int> DETAIL_NUM { get; set; }
        public string BATCH_NUM { get; set; }
        public Nullable<int> NUM { get; set; }
        public Nullable<System.DateTime> Insertdate { get; set; }
        public int DataUP { get; set; }
        public string SCANCODE { get; set; }
        public string patientID { get; set; }
        public string patientName { get; set; }
        public string patientAge { get; set; }
        public string patientSex { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public string creator { get; set; }
        public Nullable<System.DateTime> expiryDate { get; set; }
        public Nullable<System.DateTime> productDate { get; set; }
        public string patientDept { get; set; }
    }
}
