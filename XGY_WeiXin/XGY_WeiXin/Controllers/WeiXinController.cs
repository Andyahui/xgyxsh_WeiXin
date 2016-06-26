using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using XGY_WeiXin.WeiXinHelper;

namespace XGY_WeiXin.Controllers
{
    /// <summary>
    /// 微信服务器发送的reque请求，从这里在到别的地方去处理相应的逻辑。
    /// </summary>
    public class WeiXinController : Controller
    {
        public static readonly string Token = "xgyweixin"; //与微信公众账号后台的Token设置保持一致，区分大小写。        
        public static readonly string AppId = "";               //自己的Appid，用于获取;

        #region GET验证请求

        /// <summary>
        /// 微信后台验证地址（使用Get）
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过，，官网的有说明。
            }
            else
            {
                return
                    Content("failed:" + postModel.Signature + "," +
                            CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                            "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

#endregion

        #region POST处理请求
        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// PS：此方法为简化方法，效果与OldPost一致。
        /// v0.8之后的版本可以结合Senparc.Weixin.MP.MvcExtension扩展包，使用WeixinResult，见MiniPost方法。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            postModel.Token = Token;
            // postModel.EncodingAESKey = "";          //根据自己后台的设置保持一致
            postModel.AppId = AppId;                       //根据自己后台的设置保持一致  

            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                //??? 这里有问题，要是不注释的话，就会在这里出错，也就是数字签名有问题。
                //return Content("参数错误！");
            }

            //  1：自定义MessageHandler，对微信请求的详细判断操作都在这里面。  实例化了一个类
            var messageHandler = new CustomMessageHandle(Request.InputStream, postModel);   //接收消息

            //  2：执行微信处理过程----执行完这里之后ResponseMessage才会有值。
            messageHandler.Execute();

            ////   2.5：启动消息去重功能
            // messageHandler.OmitRepeatedMessage = true;     //启动消息去重功能。

            //  3：return new FixWeixinBugWeixinResult(messageHandler); 这个有换行的问题。           
            //return new FixWeixinBugWeixinResult(messageHandler.ToString());

            //  3：注意第三个----为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
            return new WeixinResult(messageHandler);                 //v0.8+
        } 
        #endregion
    }
}
