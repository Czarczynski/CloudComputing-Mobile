using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lab5_app.DTO;
using Lab5_app.DTO.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab5_app.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private HubConnection _hubConnection;
        private readonly string _userName;
        private ObservableCollection<UserChatMessage> Messages { get; } = new ObservableCollection<UserChatMessage>();

        public ChatPage(string userName)
        {
            _userName = userName;
            InitializeComponent();
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            btnSend.Clicked += BtnSend_Clicked;
            lvMessage.ItemsSource = Messages;
            _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5000/chathub").Build();
            _hubConnection.On<UserChatMessage>(Consts.RECEIVE_MESSAGE, ReceiveMessage_Event);
            _hubConnection.StartAsync();
        }

        private void ReceiveMessage_Event(UserChatMessage obj)
        {
            Messages.Insert(0, obj);
        }

        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            var message = txtMessage.Text;
            if (string.IsNullOrEmpty(message))
            {
                await DisplayAlert("Error", "Message is empty", "OK");
                return;
            }

            await _hubConnection.SendAsync(Consts.SEND_MESSAGE, new UserChatMessage{Message = message, Username = _userName});
        }
    }
}