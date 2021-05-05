using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Lab6_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            //V1
            
            var mainPage = new MainPage();
            var navPage = new NavigationPage(mainPage);
            MainPage = navPage;
            
            
            //V2
            
            // var tabbedPage = new TabbedPage();
            // tabbedPage.Children.Add(new TabPage1());
            // tabbedPage.Children.Add(new TabPage2());
            //
            // MainPage = tabbedPage;


        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}