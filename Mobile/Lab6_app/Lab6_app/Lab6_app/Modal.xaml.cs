using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab6_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Modal : ContentPage
    {
        public Modal()
        {
            InitializeComponent();
            
            btnGoBack.Clicked += (s,e) => Navigation.PopModalAsync();
        }
    }
}