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

    public partial class bas_UserRoots
    {
        public string terminalID { get; set; }
        public string userID { get; set; }
        public string valid { get; set; }
        public string isadmin { get; set; }
        public System.DateTime updateTime { get; set; }

        [Key]
        public int ID { get; set; }
    }
}
