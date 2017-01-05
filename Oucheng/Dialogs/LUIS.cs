using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace BotFramework
{


    [LuisModel("c251feb8-a23f-4f95-a584-94f34cd684b2", "781479ab342a42f88ca645b30d22b411")]


    [Serializable]
    public class SimpleLUISDialog : LuisDialog<object>
    {


        public const string ENTITY_PLATE = "车牌号";

        public const string HELLO_ANSWERE = "您好，我是金吉列留学咨询小助手小豆,请问有什么可以帮您 ?";
        public const string ASK_FOR_CHANNEL = "我们提供多种渠道提供留学咨询，电话，网络，上门。您希望我们如何联系您?";
        public const string PHONE_CHANNEL = "我们的全国咨询热线：400-010-8000 马上给您接通吗 ? ";
        public const string INTERNET_CHANNEL = "马上接通乐语咨询系统";
        public const string ONSITE_CHANNEL = "金吉列出国留学有限公司目前有48家分公司根据您目前的定位离您最近的为北京，是否前往?";
        public const string Q_COUNTRY = "请问您咨询哪个国家?";

        public const string R_CONFIRM = "不客气，有问题随时联系智能助手小豆，金吉列竭诚为您服务！";

        private string plate = "";

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(HELLO_ANSWERE);
            context.Wait(MessageReceived);
        }

        [LuisIntent("问好")]
        public async Task SetTemp(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(HELLO_ANSWERE);
            context.Wait(MessageReceived);

        }

        [LuisIntent("问联系方式")]
        public async Task WeatherQuery(IDialogContext context, LuisResult result)
        {

       
                var reply = context.MakeMessage();
                reply.Text = string.Format("");

                reply.Attachments = new List<Attachment>();

                var actions = new List<CardAction>();
                for (int i = 0; i < 3; i++)
                {
                    actions.Add(new CardAction
                    {
                        Title = $"Button:{i}",
                        Value = $"Action:{i}",
                        Type = ActionTypes.ImBack
                    });
                }
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
              
                reply.Attachments.Add(
                    new HeroCard
                    {
                        Title = $"联系方式选择",
                        Images = new List<CardImage>
                    {
                        new CardImage
                        {
                            Url = $"https://sparcpoint.blob.core.windows.net/blog-images/11f83525-db1e-4bcb-85d2-0d8de797ac44"
                        }
                    },
                        Buttons = actions
                    }.ToAttachment()
                );
              
                await context.PostAsync(reply);
                context.Wait(MessageReceived);
 

        }
        enum ANS { 好的, 稍后 };


        [LuisIntent("电话")]
        public async Task ProcessStoreHours(IDialogContext context, LuisResult result)
        {
            var ans = (IEnumerable<ANS>)Enum.GetValues(typeof(ANS));

            PromptDialog.Choice(context, phoneCallResult, ans, PHONE_CHANNEL);
        }

        private async Task phoneCallResult(IDialogContext context, IAwaitable<ANS> ans)
        {
            var replay = string.Empty;
            switch (await ans)
            {
                case ANS.好的:
                    await context.PostAsync("正在为您转接，请稍后。。。");
                    break;
                case ANS.稍后:
                    await context.PostAsync("电话没有接通，但我已经将联系信息给您发短信了，您可以在方便的时候进行咨询，金吉列竭诚为您服务。");
                    break;
                default:
                    await context.PostAsync(R_CONFIRM);
                    break;
            }

            //await context.PostAsync(R_CONFIRM);

            //context.Wait(MessageReceived);
        }



        [LuisIntent("网络")]
        public async Task verifyVehicle(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(" 马上接通乐语咨询系统");
            context.Wait(MessageReceived);
        }
     
        [LuisIntent("上门")]
        public async Task shangmen(IDialogContext context, LuisResult result)
        {
            var ans = (IEnumerable<ANS>)Enum.GetValues(typeof(ANS));
            PromptDialog.Choice(context, shangmenResult, ans, ONSITE_CHANNEL);
        }

        private async Task shangmenResult(IDialogContext context, IAwaitable<ANS> ans)
        {
            var answer = (IEnumerable<ANS>)Enum.GetValues(typeof(ANS));

            var replay = string.Empty;
            switch (await ans)
            {
                case ANS.好的:
                    await context.PostAsync("这是具体的位置 http://j.map.baidu.com/Gi3qF 是否为您推荐顾问 ?");
                    break;
                case ANS.稍后:
                    await context.PostAsync("暂时不前往，但我已经将地址信息给您发短信了，您可以在方便的时候进行咨询，金吉列竭诚为您服务。");
                    break;
                default:
                    await context.PostAsync("好的，有问题随时告诉我。谢谢！");
                    break;
            }


            context.Wait(MessageReceived);
        }

        [LuisIntent("确认")]
        public async Task Thanks_return(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(Q_COUNTRY);
            context.Wait(MessageReceived);

        }

        [LuisIntent("感谢")]
        public async Task Thanks2_return(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(R_CONFIRM);
            context.Wait(MessageReceived);

        }

        [LuisIntent("国家")]
        public async Task onuntry_return(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("已经为您安排了" + result.Query + "顾问，电话和姓名已经发到您的手机。");
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