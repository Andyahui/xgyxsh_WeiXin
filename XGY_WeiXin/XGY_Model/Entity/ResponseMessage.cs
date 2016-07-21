using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Model.Entity
{

    #region ResponseBaseMessage
    public class ResponseBaseMessage : BaseEntity
    {
        public ResponseBaseMessage()
        {
            this.RequestMessages = new Collection<RequestMessage>();
        }

        public virtual ResponseMsgType ResponseMsgType { get; set; }
        public virtual ICollection<RequestMessage> RequestMessages { get; set; }
    }

    public class ResponseBaseMessageMap : EntityTypeConfiguration<ResponseBaseMessage>
    {
        public ResponseBaseMessageMap()
        {
            this.ToTable("ResponseBaseMessage");
            this.HasKey(x => x.Id);
            this.HasMany(x => x.RequestMessages);
        }
    } 
    #endregion

    #region 响应Text消息
    public class ResponseTextMessage : ResponseBaseMessage
    {
        public virtual string Content { get; set; }
    }

    public class ResponseTextMessageMap : EntityTypeConfiguration<ResponseTextMessage>
    {
        public ResponseTextMessageMap()
        {
            this.ToTable("ResponseBaseMessage");
            this.Property(x => x.Content).IsMaxLength();
            this.Map(x => x.Requires(t => t.ResponseMsgType.HasFlag(ResponseMsgType.Text)));
        }
    } 
    #endregion

    #region 响应News类型消息

    #region ResponseNewsMessage
    public class ResponseNewsMessage : ResponseBaseMessage
    {
        public ResponseNewsMessage()
        {
            this.Items = new Collection<ResponseNewItem>();
        }
        public virtual ICollection<ResponseNewItem> Items { get; set; }

    }

    public class ResponseNewsMessageType : EntityTypeConfiguration<ResponseNewsMessage>
    {
        public ResponseNewsMessageType()
        {
            this.ToTable("ResponseBaseMessage");
            this.HasMany(x => x.Items);       //配置单一的一对多关系
            this.Map(x => x.Requires(t => t.ResponseMsgType.HasFlag(ResponseMsgType.News)));
        }
    } 
    #endregion

    #region ResponseNewItem
    public class ResponseNewItem : BaseEntity
    {
        public virtual string PicUrl { get; set; }
        public virtual string Url { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int Order { get; set; }
    }

    public class ResponseNewItemMap : EntityTypeConfiguration<ResponseNewItem>
    {
        public ResponseNewItemMap()
        {
            this.ToTable("ResponseNewItem");
            this.Property(x => x.PicUrl).HasMaxLength(512);
            this.Property(x => x.Url).HasMaxLength(512);
            this.Property(x => x.Title).HasMaxLength(128);
            this.Property(x => x.Description).IsMaxLength();
        }
    } 
    #endregion

    #endregion
    /// <summary>
    /// 响应消息类型
    /// </summary>
    public enum ResponseMsgType:int
    {
        Text=0,
        News=1,
        Music=2,
        Image=3,
        Method=4
    }
}
