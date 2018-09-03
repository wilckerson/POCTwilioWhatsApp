using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace POCTwilioWhatsApp.Controllers
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            bool mainUser = user == "poc";

            if (mainUser)
            {
                SendToWhatsApp("+556191717234", message);
            }

            await SendToClients(this.Clients, user, message, mainUser);
        }

        public async static Task SendToClients(IHubClients<IClientProxy> clients, string user, string text, bool mainUser)
        {
            await clients.All.SendAsync("ReceiveMessage", new
            {
                user = user,
                text = text,
                mainUser = mainUser,
                date = DateTime.UtcNow
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toPhone">e164 formatted phone number</param>
        /// <param name="msg"></param>
        private void SendToWhatsApp(string toPhone, string msg)
        {

            const string accountSid = "ACde513891ab73884182fd4fa0c7a6690d";
            const string authToken = "33942e96aea87cfeb9f63d0e8e36fdfb";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: msg,
                from: new Twilio.Types.PhoneNumber("whatsapp:+552120420682"),
                to: new Twilio.Types.PhoneNumber($"whatsapp:{toPhone}")
            );
        }

    }
}
