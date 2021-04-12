using System;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Lab4_app.Annotations;
using Lab4_app.Repositories;
using Xamarin.Forms;

namespace Lab4_app.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var client = RestEase.RestClient.For<IPeopleRepository>(Consts.API_URI, clientHandler);
            OnTakePhoto = new Command(() => OnTakePhotoClick());
            OnSaveData = new Command(async () => await OnSaveDataClick());
        }
        
        private Person _person = new Person();
        private string _error = "";
        private string _info = "";
        public IPeopleRepository Client;
        public ICommand OnTakePhoto { get; set; }
        public ICommand OnSaveData { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Firstname
        {
            get => _person.Firstname;
            set
            {
                if (_person.Firstname == value)
                    return;
                _person.Firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        public string Lastname
        {
            get => _person.Lastname;
            set
            {
                if (_person.Lastname == value)
                    return;
                _person.Lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public string PhoneNumber
        {
            get => _person.PhoneNumber;
            set
            {
                if (_person.PhoneNumber == value)
                    return;
                _person.PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string PictureBase64
        {
            get => _person.PictureBase64;
            set
            {
                if (_person.PictureBase64 == value)
                    return;
                _person.PictureBase64 = value;
                OnPropertyChanged(nameof(PictureBase64));
                OnPropertyChanged(nameof(PictureParsed));
            }
        }

        public string Error
        {
            get => _error;
            set
            {
                if (_error == value)
                    return;
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public string Info
        {
            get => _info;
            set
            {
                if (_info == value)
                    return;
                _info = value;
                OnPropertyChanged(nameof(Info));
            }
        }


        public ImageSource PictureParsed =>
            _person.PictureBase64 == null
                ? null
                : ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(_person.PictureBase64)));

        private void OnPropertyChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }

        private void OnTakePhotoClick()
        {
            //var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{

            //});

            //if (photo != null)
            //{
            //    imgPhoto.Source = ImageSource.FromStream(() => photo.GetStream());
            //    byte[] bytes;
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        photo.GetStream().CopyTo(memoryStream);
            //        bytes = memoryStream.ToArray();
            //    }


            //string base64 = Convert.ToBase64String(bytes);
            //}
            //person.PictureBase64 = base64;
            PictureBase64 = Images.defaultPhoto;
        }

        private async Task OnSaveDataClick()
        {
            if (!Validate())
            {
                Error = "First name, last name, phone number and picture are required.";
                return;
            }

            try
            {
                await Client.AddPersonAsync(_person);
                Info = "Data has been saved.";
                Clear();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        private void Clear()
        {
            _person = new Person();
        }

        private bool Validate()
        {
            return !(string.IsNullOrWhiteSpace(Firstname) ||
                     string.IsNullOrWhiteSpace(Lastname) ||
                     string.IsNullOrWhiteSpace(PhoneNumber) ||
                     string.IsNullOrWhiteSpace(PictureBase64)
                );
        }
    }
}