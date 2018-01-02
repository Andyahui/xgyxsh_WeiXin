using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Model.Entity
{
    #region 响应功能消息--(调用返回方法)+ResponseMethodMessage
    /// <summary>
    /// 返回调用一个方法之后返回的消息
    /// </summary>
    public class ResponseMethodMessage : BaseEntity
    {
        public ResponseMethodMessage()
        {
            this.RequestMessages=new Collection<RequestMessage>();
        }
        /// <summary>
        /// 方法的名称：便于程序处理的一个名字
        /// </summary>
        public virtual  string MethodName { get; set; }
        /// <summary>
        /// 方法的显示名称：便于管理员操作的一个名称
        /// </summary>
        public virtual string DisplayName { get; set; }

        public virtual ResponseMsgType ResponseMsgType { get; set; }
        public virtual ICollection<RequestMessage> RequestMessages { get; set; }

    }
    public class ResponseMethodMessageMap : EntityTypeConfiguration<ResponseMethodMessage>
    {
        public ResponseMethodMessageMap()
        {
            this.ToTable("ResponseMethodMessage");
            this.HasKey(x => x.Id);
            this.Property(x => x.MethodName).HasMaxLength(128);
            this.Property(x => x.DisplayName).HasMaxLength(128);
            this.Property(x => x.ResponseMsgType).IsRequired();
            this.HasMany(x => x.RequestMessages);
        }
    }
    #endregion
}
