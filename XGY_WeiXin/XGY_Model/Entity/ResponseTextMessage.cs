using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Model.Entity
{
    #region 响应文本消息+ResponseTextMessage
    /// <summary>
    /// 响应的文本消息
    /// </summary>
    public class ResponseTextMessage : BaseEntity
    {
        public ResponseTextMessage()
        {
           // this.RequestMessage = new Collection<RequestMessage>();
        }
        /// <summary>
        /// 文本内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 响应消息分类
        /// </summary>
        public virtual ResponseMsgType ResponseMsgType { get; set; }
        /// <summary>
        /// 一对多属性配置----相当于导航属性
        /// </summary>
        public virtual ICollection<RequestMessage> RequestMessage { get; set; }
    }

    public class ResponseTextMessageMap : EntityTypeConfiguration<ResponseTextMessage>
    {
        public ResponseTextMessageMap()
        {
            this.ToTable("ResponseTextMessage");
            this.HasKey(x => x.Id);
            this.Property(x => x.Content).IsMaxLength();
            this.Property(x => x.ResponseMsgType).IsRequired();
            this.HasMany(x => x.RequestMessage);
        }
    }
    #endregion
}
