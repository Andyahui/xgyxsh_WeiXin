using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XGY_WeiXin.Areas.Admin.Models
{
    public class PageResponseTextList
    {
        public ICollection<ResponseTextMessageView> Items { get; set; }
    }

    public class ResponseTextMessageView
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Content { get; set; }
    }

    public class CreateResponseTextMessageView
    {

        [Display(Name="响应文本内容"),Required(ErrorMessage ="内容不能为空")]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class UpdateResponseTextMessageView
    {
        public Guid Id { get; set; }
        [Display(Name="响应文本内容"),Required(ErrorMessage = "文本内容不能为空")]
        public string Content { get; set; }
        //public DateTime CreateTime { get; set; }
    }
}