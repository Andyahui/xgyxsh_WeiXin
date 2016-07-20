using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Model.Entity
{
    /// <summary>
    /// 请求消息类
    /// </summary>
    public class RequestMessage:BaseEntity
    {
        /// <summary>
        /// 用户发送来的关键字
        /// </summary>
        public virtual string KeyWord { get; set; }
        /// <summary>
        /// 匹配模式
        /// </summary>
        public virtual MatchPattern MatchPattern { get; set; }       
        /// <summary>
        /// 请求消息类型
        /// </summary>
        public virtual RequestMsgType MsgType { get; set; }   
        /// <summary>
        /// 响应消息ID
        /// </summary>
        public virtual Guid ResponseMessgeId { get; set; }
        /// <summary>
        /// 响应消息
        /// </summary>
        public virtual ResponseBaseMessage ResponseMessage { get; set; }
        /// <summary>
        /// 响应消息类型
        /// </summary>
        public virtual ResponseMsgType ResponseMsgType { get; set; }
    }

    public class RequestMessageMap:EntityTypeConfiguration<RequestMessage>
    {
        public RequestMessageMap()
        {
            this.ToTable("RequestMessage");
            this.Property(x => x.KeyWord).HasMaxLength(128);
            this.HasOptional(x => x.ResponseMessage)
                .WithMany(x => x.RequestMessages)
                .HasForeignKey(x => x.ResponseMessgeId);
        }
    }

    /// <summary>
    /// 请求的类型
    /// </summary>
    public enum RequestMsgType:int
    {
        Text=1,  //文本类型
        Voice=1,  //语言类型
        Image=2,  //图片类型
        Menu=3,  //点击菜单
        Subscription=4,  //订阅
        NoMatch=5   //没有匹配
    }
    /// <summary>
    /// 关键字的匹配模式
    /// </summary>
    public enum MatchPattern:int
    {
        Contains=0,
        Equale=1
    }
}
