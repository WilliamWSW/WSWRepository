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
    using System.ComponentModel;

    public partial class bas_User
    {
        [Key]
        public string userID { get; set; }

       
        [StringLength(15,MinimumLength =1,ErrorMessage ="用户名长度8-15位")]
        [DisplayName("登录用户名")]
        public string loginName { get; set; }
        public string userName { get; set; }
        public string userPwd { get; set; }
        public string spellCode { get; set; }
        public string roleID { get; set; }
        public string userCard { get; set; }
        public string telephone { get; set; }
        public string deptID { get; set; }
        public string extUserID { get; set; }
        public string valid { get; set; }
        public string updateMan { get; set; }
        public System.DateTime updateTime { get; set; }
    }
}
