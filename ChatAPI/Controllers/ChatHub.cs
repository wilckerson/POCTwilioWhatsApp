using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ChatController> logger;

        public ChatHub(ILogger<ChatController> logger)
        {
            this.logger = logger;
        }

        public async Task Join(string toClient)
        {
            //Agrupando os clientes em grupos
            await Groups.AddToGroupAsync(this.Context.ConnectionId, toClient);
        }

        public async Task SendMessage(string user, string message, string toClient)
        {
            if (string.IsNullOrEmpty(user)) { return; }
            if (string.IsNullOrEmpty(message)) { return; }
            if (string.IsNullOrEmpty(toClient)) { return; }

            bool mainUser = user == "poc";

            logger.LogInformation($"ChatHubMessage user:{user} message:{message} toClient:{toClient}");

            if (mainUser)
            {
                SendToWhatsApp(toClient, message);
            }

           

            await SendToClients(this.Clients, user, message, mainUser,toClient);
        }

        public async static Task SendToClients(IHubClients<IClientProxy> clients, string user, string text, bool mainUser, string groupName)
        {
            //await clients.All.SendAsync("ReceiveMessage", new

            //Enviando notificação apenas para os usuários conectados no grupo deste cliente
            var group = clients.Group(groupName);
            await group.SendAsync("ReceiveMessage", new
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
