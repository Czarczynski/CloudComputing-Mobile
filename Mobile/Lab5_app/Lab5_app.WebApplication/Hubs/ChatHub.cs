using System;
using System.Threading.Tasks;
using Lab5_app.DTO;
using Lab5_app.DTO.Models;
using Microsoft.AspNetCore.SignalR;

namespace Lab5_app.WebApplication.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(UserChatMessage message)
        {
            message.TimeStamp = DateTime.Now;
            await Clients.All.SendAsync(Consts.RECEIVE_MESSAGE, message);
        }
    }
}