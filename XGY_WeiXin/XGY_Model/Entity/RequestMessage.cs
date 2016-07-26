using System;
using System.Data.Entity.ModelConfiguration;
using XGY_Model;

namespace XGY_Model.Entity
{

    #region RequestMessage
    /// <summary>
    /// 请求消息基类
    /// </summary>
    public class RequestMessage : BaseEntity
    {
        /// <summary>
        /// 用户发来的关键字
        /// </summary>
        public virtual string KeyWord { get; set; }
        /// <summary>
        /// 匹配模式
        /// </summary>
        public virtual MatchPattern MatchPattern { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public virtual RequestMsgType MsgType { get; set; }
        /// <summary>
        /// 响应消息的类型
        /// </summary>
        public virtual ResponseMsgType ResponseMsgType { get; set; }
        /// <summary>
        /// 响应文本消息的ID
        /// </summary>
        public virtual Guid ResponseTextMessageId { get; set; }
        /// <summary>
        /// 响应文本消息
        /// </summary>
        public virtual ResponseTextMessage ResponseTextMessage { get; set; }
        /// <summary>
        /// 响应图文消息ID
        /// </summary>
        public virtual Guid ResponseNewsMessageId { get; set; }
        /// <summary>
        /// 响应图文消息
        /// </summary>
        public virtual ResponseNewsMessage ResponseNewsMessage { get; set; }
        /// <summary>
        /// 响应方法消息ID
        /// </summary>
        public virtual Guid ResponseMethodId { get; set; }
        /// <summary>
        /// 响应方法消息
        /// </summary>
        public virtual ResponseMethodMessage ResponseMethodMessage { get; set; }
    }
    public class RequestMessageMap : EntityTypeConfiguration<RequestMessage>
    {
        public RequestMessageMap()
        {
            this.ToTable("RequestMessage");
            this.Property(x => x.KeyWord).HasMaxLength(128);
            this.HasRequired(x => x.ResponseTextMessage)
                .WithMany(x => x.RequestMessage)
                .HasForeignKey(x => x.ResponseTextMessageId);
            this.HasRequired(x => x.ResponseNewsMessage)
                .WithMany(x => x.RequestMessages)
                .HasForeignKey(x => x.ResponseNewsMessageId);
            this.HasRequired(x => x.ResponseMethodMessage)
                .WithMany(x => x.RequestMessages)
                .HasForeignKey(x => x.ResponseMethodId);
        }
    }   

    #endregion

    #region ENUM
    /// <summary>
    /// 用户的Request类型
    /// </summary>
    public enum RequestMsgType : int
    {
        /// <summary>
        /// 用户发来文字时
        /// </summary>
        Text = 0,//文本
        Voice = 1,//语音
        Image = 2,//图片
        Menu = 3,//点击菜单
        Subscription = 4,//订阅
        NoMatch = 5,//没有匹配时
    }
    /// <summary>
    /// 关键字的匹配模式,这里是对应于文本请求时使用的。
    /// </summary>
    public enum MatchPattern : int
    {
        /// <summary>
        /// 包含
        /// </summary>
        Contains = 0,
        /// <summary>
        /// 完全相等
        /// </summary>
        Equale = 1,
    }
    /// <summary>
    /// 响应消息类型
    /// </summary>
    public enum ResponseMsgType : int
    {
        Text = 0,
        News = 1,
        Music = 2,
        Image = 3,
        Method = 4
    } 
    #endregion
}
