using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace Oucheng
{


    [LuisModel("1996d0e0-197a-40e4-84c0-e05a9ae34102", "781479ab342a42f88ca645b30d22b411")]


    [Serializable]
    public class SimpleLUISDialog : LuisDialog<object>
    {

        public const string HELLO_ANSWERE = "想领养我？来点击链接吧 http://doraemonbotframework.azurewebsites.net/";
        public const string ASK_FOR_CHANNEL = " 您现在还未绑定会员卡哦，快来绑定我吧 http://wx.sephora.cn/index.php?g=CustomWap&m=Bindcard&a=index&token=ulstbc1414854920 ";
        public const string ASK_FOR_CHANNEL2 = "您现在有589积分哦";
        public const string PHONE_CHANNEL = "已经帮您找到 请点击链接 http://m.sephora.cn/webapp/wcs/stores/servlet/SearchResult?searchTerm=%E9%9D%A2%E8%86%9C";
        public const string INTERNET_CHANNEL = "已经帮您找到 点击链接 http://wx.sephora.cn/index.php?g=CustomWap&m=OnlineBooking&a=index&token=ulstbc1414854920";

        int flag = 1;
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(HELLO_ANSWERE);
            context.Wait(MessageReceived);
        }
        //[LuisIntent("None")]
        //public async Task None2(IDialogContext context, LuisResult result)
        //{
        //    await context.PostAsync("您说："+ result.Query+"是什么意思？");
        //    context.Wait(MessageReceived);
        //}
        [LuisIntent("领养")]
        public async Task SetTemp(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(HELLO_ANSWERE);
            context.Wait(MessageReceived);
        }
        [LuisIntent("积分查询")]
        public async Task Temp(IDialogContext context, LuisResult result)
        {
            if (flag == 1) { flag = 2; await context.PostAsync(ASK_FOR_CHANNEL); }
            else { flag = 1; await context.PostAsync(ASK_FOR_CHANNEL2); }

            context.Wait(MessageReceived);
        }
        [LuisIntent("面膜推荐")]
        public async Task Temp2(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(PHONE_CHANNEL);
            context.Wait(MessageReceived);
        }

        [LuisIntent("修眉推荐")]
        public async Task Temp3(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(INTERNET_CHANNEL);
            context.Wait(MessageReceived);
        }
      
        public SimpleLUISDialog()
        {

        }
        public SimpleLUISDialog(ILuisService service)
            : base(service)
        {
        }
    }
}