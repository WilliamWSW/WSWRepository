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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class sys_Camera
    {
        [Key]
        [Column(Order = 1)]
        public string terminalID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string cameraID { get; set; }
        public string cameraIP { get; set; }
        public string valid { get; set; }
        public string updateMan { get; set; }
        public System.DateTime updateTime { get; set; }
    }
}
