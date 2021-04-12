using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lab3_app.Repositories;

namespace Lab3_app
{
    public partial class App : Application
    {
        private const string API_URI = "https://192.168.1.36:5001/api";

        public App()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var client = RestEase.RestClient.For<IPeopleRepository>(API_URI, clientHandler);
            InitializeComponent();

            MainPage = new MainPage(client);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}