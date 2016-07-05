using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace XGY_WeiXin.Areas.Admin.Models
{
    #region User管理
    public class UserListView
    {
        public ICollection<UserView> Items { get; set; }
    }

    public class UserView
    {
        public Guid Id { get; set; }

        [DisplayName("用户名"), Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [DisplayName("登录名"), Required(ErrorMessage = "登录名不能为空")]
        public string LoginName { get; set; }
        [DisplayName("登录密码"), Required(ErrorMessage = "登录密码不能为空")]
        public string LoginPwd { get; set; }
        [DisplayName("用户头像"), Required(ErrorMessage = "头像不能为空")]
        public string HeadPhoto { get; set; }

    }

    public class CreateUserView
    {
        [DisplayName("用户名"), Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [DisplayName("登录名"), Required(ErrorMessage = "登录名不能为空")]
        public string LoginName { get; set; }
        [DisplayName("登录密码"), Required(ErrorMessage = "登录密码不能为空")]
        public string LoginPwd { get; set; }
        [DisplayName("用户头像"), Required(ErrorMessage = "头像不能为空")]
        public string HeadPhoto { get; set; }
    }

    public class UpdateUserView
    {
        public Guid Id { get; set; }
        [DisplayName("用户名"), Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [DisplayName("登录名"), Required(ErrorMessage = "登录名不能为空")]
        public string LoginName { get; set; }
        [DisplayName("登录密码"), Required(ErrorMessage = "登录密码不能为空")]
        public string LoginPwd { get; set; }
        [DisplayName("用户头像"), Required(ErrorMessage = "头像不能为空")]
        public string HeadPhoto { get; set; }
    }
    #endregion

    #region Account登录
    public class UserAccountView
    {        
        [DisplayName("登录名"), Required(ErrorMessage = "登录名不能为空")]
        public string LoginName { get; set; }
        [DisplayName("登录密码"), Required(ErrorMessage = "登录密码不能为空")]
        public string LoginPwd { get; set; }                
    }

    public class UpdateUserAccountView
    {
        public Guid Id { get; set; }
        [DisplayName("用户名"), Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [DisplayName("登录名"), Required(ErrorMessage = "登录名不能为空")]
        public string LoginName { get; set; }
        [DisplayName("登录密码"), Required(ErrorMessage = "登录密码不能为空")]
        public string LoginPwd { get; set; }
        [DisplayName("用户头像"), Required(ErrorMessage = "头像不能为空")]
        public string HeadPhoto { get; set; }
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }
    }

    #endregion
}