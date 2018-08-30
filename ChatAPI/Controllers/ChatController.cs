using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace POCTwilioWhatsApp.Controllers
{
    public class ChatController : Controller{

        public IActionResult Index(){
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
        public IActionResult ReceivedMsg(){


            StreamReader reader = new StreamReader(Request.Body);
            string content = reader.ReadToEnd();

            return Content(content);
        }
    }
}