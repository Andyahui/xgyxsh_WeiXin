using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.Context;

namespace XGY_WeiXin.WeiXinHelper
{
    public class CustomMessageHandle : MessageHandler<CustomMessageContext>
    {
        //PostModel：表示的都是从微信服务器里面得到的值，时间戳，字符串等。(WeiXinController中使用过)
        //构造函数的inputStream用于接收来自微信服务器的请求流（如果需要在外部处理，这里也可以传入XDocument）。
        public CustomMessageHandle(Stream inputSrream,PostModel postModel):base(inputSrream,postModel)
        {            
        }
        /// <summary>
        /// 必须实现抽象的类------作用：用于放回一条信息，当没有对应类型的微信消息没有被代码处理，那么默认会执行返回这里的结果。
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
    public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
    {
        //CreateResponseMessage<T>  这里是创建一个放回的对象，代表不同的类型，
        var responseMessage = base.CreateResponseMessage<ResponseMessageText>();//ResponseMessageText可以更换为别的类型
        responseMessage.Content = "这条消息来自DefaultResponseMessage。";
        return responseMessage;
    }
        /// <summary>
        ///1： 处理用户发送过来的文字消息。重写OnTextRequest方法。
    /// --------（总结：）方法里面可以自由发挥，读取DB，判断关键字，甚至返回不同的ResponseMessageXX类型（只要最终的类型都是在IResponseMessageBase接口下的即可）。
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            //CreateResponseMessage<类型>根据当前的RequestMessage创建指定类型的ResponseMessage；创建相应消息.
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您的OpenID是：" + requestMessage.FromUserName + "。\r\t您发送了文字信息：" +
                                      requestMessage.Content;
            return responseMessage;
        }
        /// <summary>
        /// OnExecuting会在所有消息处理方法（如OnTextRequest，OnVoiceRequest等）执行之前执行
        /// 1：第一个执行这个方法。之后才执行我们的处理消息。
        /// </summary>
        public override void OnExecuting()
        {
            if (RequestMessage.FromUserName=="")
            {
                CancelExcute = true;  //终止此用户的对话。
            }
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "不好意思，你被拉黑了。";
            ResponseMessage = responseMessage;  //设置返回对象
        }
    }
}