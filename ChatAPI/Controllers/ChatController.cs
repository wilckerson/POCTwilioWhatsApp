using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace POCTwilioWhatsApp.Controllers
{
    public class ChatController : Controller{
        private ILogger<ChatController> logger;

        public ChatController(ILogger<ChatController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index(){

            logger.LogInformation("ChatApi Index =)");

            return Content($"ChatAPI v1.0 {DateTime.UtcNow}");
        }

        public IActionResult SendMsg(string msg){

            const string accountSid = "ACde513891ab73884182fd4fa0c7a6690d";
            const string authToken = "33942e96aea87cfeb9f63d0e8e36fdfb";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: msg,
                from: new Twilio.Types.PhoneNumber("whatsapp:+552120420682"),
                to: new Twilio.Types.PhoneNumber("whatsapp:+556191717234")
            );

            return Ok();
        }

       

        [HttpPost]
        public IActionResult ReceivedMsg(ReceivedMessage message)
        {
            
            var content = JsonConvert.SerializeObject(message);
            logger.LogInformation("ReceivedMsg Content:");
            logger.LogInformation(content);
            
            return Ok();
        }
    }

    public class ReceivedMessage
    {
        public string Body { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        //public string SmsMessageSid { get; set; }
        //public string NumMedia { get; set; }
        //public string SmsSid { get; set; }
        //public string SmsStatus { get; set; }
        //public int NumSegments { get; set; }
        //public string MessageSid { get; set; }
        //public string AccountSid { get; set; }
        //public string ApiVersion { get; set; }
    }
}