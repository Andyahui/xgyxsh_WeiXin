using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.BaiduMap;
using Senparc.Weixin.MP.Entities.GoogleMap;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.Context;

namespace XGY_WeiXin.WeiXinHelper
{
    /// <summary>
    /// 微信后台处理的核心逻辑类，处理微信响应的任务，实现了处理单个用户上下文的功能。
    /// </summary>
    public class CustomMessageHandle : MessageHandler<CustomMessageContext>
    {
        //PostModel：表示的都是从微信服务器里面得到的值，时间戳，字符串等。(WeiXinController中使用过)
        //构造函数的inputStream用于接收来自微信服务器的请求流（如果需要在外部处理，这里也可以传入XDocument）。

        public CustomMessageHandle(Stream inputSrream,PostModel postModel):base(inputSrream,postModel)
        {            
        }

        #region ***：网站服务器无处理时默认回复
        /// <summary>
        /// 没有任何处理的默认返回值。
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
        #endregion


       //（总结：）方法里面可以自由发挥，读取DB，判断关键字，甚至返回不同的ResponseMessageXX类型（只要最终的类型都是在IResponseMessageBase接口下的即可）。

        #region 一： 常规消息请求，(就是用户的请求。)

        #region 1：文本请求
        /// <summary>
        ///1： 处理用户发送过来的文字消息。重写OnTextRequest方法。    
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            //CreateResponseMessage<类型>根据当前的RequestMessage创建指定类型的ResponseMessage；创建相应消息.
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            if (requestMessage.Content=="客服")
            {
                //多客服功能。
                return this.CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
            }
            else
            {                
                //普通文本功能。
                responseMessage.Content = "您的OpenID是：" + requestMessage.FromUserName + "。\r\t您发送了文字信息：" +
                                      requestMessage.Content + "注意了，我是张辉";
                return responseMessage;                
            }                        
        } 
        #endregion

        #region 2：处理图片请求

        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            var responseImage = base.CreateResponseMessage<ResponseMessageText>();
            responseImage.Content = "来自图片，ahui";
            return responseImage;
        }
        #endregion

        #region 3：处理语音请求

        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            var responseVoice = base.CreateResponseMessage<ResponseMessageVoice>();
            responseVoice.Voice=new Voice()
            {                
                MediaId = requestMessage.MediaId
            };           
            return responseVoice;
        }

        #endregion

        #region 4：处理链接请求

        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            var responseLink = base.CreateResponseMessage<ResponseMessageNews>();
            responseLink.Articles.Add(new Article()
            {
                Title="处理链接来的请求。",
                Description =requestMessage.Description,
                PicUrl = "http://pic.cnblogs.com/avatar/679140/20141128195544.png",
                Url=requestMessage.Url
            });
            return responseLink;
        }

        #endregion

        #region 5：处理视频请求 -----有点问题???

        public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            var responseVideo = base.CreateResponseMessage<ResponseMessageVideo>();
            responseVideo.Video = new Video()
            {
                Title = "来自视频的请求",
                Description = "很好看的视频直播。",
                MediaId = requestMessage.MediaId
            };
            return responseVideo;
        }

        #endregion

        #region 6：处理位置请求

        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            //返回的是图文消息,是关于地址的图文消息。
            var responseLocation = base.CreateResponseMessage<ResponseMessageNews>();

            var markersList = new List<BaiduMarkers>();
            markersList.Add(new BaiduMarkers()
            {
                Size=BaiduMarkerSize.m,
                Color ="red",
                Label="A",
                Latitude =requestMessage.Location_X,
                Longitude=requestMessage.Location_Y,
            });
            var mapUrl = BaiduMapHelper.GetBaiduStaticMap(requestMessage.Location_Y,requestMessage.Location_X,1,13,markersList);
            responseLocation.Articles.Add(new Article()
            {
                Description = string.Format("您刚才发送了地理位置信息。Location_X:{0},Location_Y:{1},Scale:{2},标签：{3}",requestMessage.Location_X,requestMessage.Location_Y,requestMessage.Scale,requestMessage.Label),
                PicUrl = "http://pic.cnblogs.com/avatar/679140/20141128195544.png",
                Title="张辉的地图",
                Url = mapUrl
            });
            return responseLocation;
        }

        #endregion

        #region 执行各种请求之前先执行这里的方法。(测试之后不是这样的）
        /// <summary>
        /// OnExecuting会在所有消息处理方法（如OnTextRequest，OnVoiceRequest等）执行之前执行
        /// 1：第一个执行这个方法。之后才执行我们的文本处理。
        /// </summary>
        public override void OnExecuting()
        {   //01:终止用户的对话
            //if (RequestMessage.FromUserName == "wx82e5f59acba2d931")
            //{
            //    CancelExcute = true;            //终止此用户的对话。
            //}
            //var responseMessage = CreateResponseMessage<ResponseMessageText>();
            //responseMessage.Content = "不好意思，你被拉黑了。";
            //ResponseMessage = responseMessage;  //设置返回对象

            //02：请求去重
            if (OmitRepeatedMessage&&CurrentMessageContext.RequestMessages.Count>1)
            {
                var lastMessage = CurrentMessageContext.RequestMessages[CurrentMessageContext.RequestMessages.Count-2];
                if (lastMessage.MsgId!=0&&lastMessage.MsgId==RequestMessage.MsgId)
                {
                    CancelExcute = true;   //重复消息，取消执行。
                }
            }
        }  
        #endregion
        

    #endregion


        #region 二：事件Event处理

        #region 1：点击事件
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            var respondeEvert = base.CreateResponseMessage<ResponseMessageText>();
            respondeEvert.Content = "点击事件，";
            return respondeEvert;
        } 
        #endregion

        #region 2：订阅事件
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseSubscribe = base.CreateResponseMessage<ResponseMessageText>();
            responseSubscribe.Content = "欢迎订阅，张辉欢迎您。";
            return responseSubscribe;
        }

        #endregion

        #region 3：取消订阅事件

        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseUnScript = base.CreateResponseMessage<ResponseMessageText>();
            responseUnScript.Content = "走了，不送。";
            return responseUnScript;
        }

        #endregion

        #region 上报地理位置事件

        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            return base.OnEvent_LocationRequest(requestMessage);
        }

        #endregion

        public override IResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        {
            var responseView = base.CreateResponseMessage<ResponseMessageText>();
            responseView.Content = "点击带有view的菜单栏";
            return responseView;
        }

        #region 
        
        #endregion


        #endregion
    }
}