using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRChat_Practice.App_Code
{

    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class ChatHub : Hub
    {
        public void Send(string name, string message,string icon)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message,icon);
            //Notify(name + "：" + message);

            var chatMessage = new ChatMessage
            {
                Sender = name,
                Message = message,
                Icon = icon,
                Timestamp = DateTime.UtcNow
            };

            ChatHistory.AddMessage(chatMessage);

            //Clients.All.SendAsync("ReceiveMessage", chatMessage);
            
        }

        public void Notify(string message)
        {
            Clients.All.notify(message);
        }

        public static class ChatHistory
        {
            private static readonly List<ChatMessage> Messages = new List<ChatMessage>();

            public static void AddMessage(ChatMessage message)
            {
                Messages.Add(message);
            }

            public static List<ChatMessage> GetAllMessages()
            {
                return Messages;
            }
        }

        public void GetChatHistory()
        {
            var history = ChatHistory.GetAllMessages();
            Clients.Caller.SendAsync("ChatHistory", history);
            Clients.All.ChatHistory(history);
        }

    }
}