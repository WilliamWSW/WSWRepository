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

    public partial class biz_RFID_IF_History
    {
        [Key]
        public int Id { get; set; }
        public string Event { get; set; }
        public string LogText { get; set; }
        public string CreateTime { get; set; }
    }
}