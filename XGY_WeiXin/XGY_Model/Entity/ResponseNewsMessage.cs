using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Model.Entity
{
    #region 响应图文消息+ResponseNewsMessage
    /// <summary>
    /// 响应的News类型消息
    /// </summary>
    public class ResponseNewsMessage:BaseEntity
    {
        public ResponseNewsMessage()
        {
            this.Items = new Collection<ResponseNewsItem>();
            this.RequestMessages = new Collection<RequestMessage>();
        }
        public virtual ICollection<ResponseNewsItem> Items { get; set; }
        public virtual ICollection<RequestMessage> RequestMessages { get; set; }
        public virtual ResponseMsgType ResponseMsgType { get; set; }
    }
    public class ResponseNewsMessageMap : EntityTypeConfiguration<ResponseNewsMessage>
    {
        public ResponseNewsMessageMap()
        {
            this.ToTable("ResponseNewsMessage");
            this.HasKey(x=>x.Id);
            this.HasMany(x => x.Items);
            this.HasMany(x => x.RequestMessages);            
            this.Property(x => x.ResponseMsgType).IsRequired();
        }
    }

    #region 响应图文消息中的列表+ResponseNewsItem
    /// <summary>
    /// 图文消息集合
    /// </summary>
    public class ResponseNewsItem : BaseEntity
    {
        public virtual string PicUrl { get; set; }
        /// <summary>
        /// 返回的URL
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// 返回的Title
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 返回的详细描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Order { get; set; }
    }

    public class ResponseNewsItemMap : EntityTypeConfiguration<ResponseNewsItem>
    {
        public ResponseNewsItemMap()
        {
            this.ToTable("ResponseNewsItem");
            this.Property(x => x.PicUrl).HasMaxLength(512);
            this.Property(x => x.Url).HasMaxLength(512);
            this.Property(x => x.Title).HasMaxLength(128);
            this.Property(x => x.Description).HasMaxLength(1024);
        }
    }

    #endregion

    #endregion
}
