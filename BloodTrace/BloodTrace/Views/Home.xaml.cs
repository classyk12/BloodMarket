using Acr.UserDialogs;
using BloodTrace.Helpers;
using BloodTrace.Models;
using BloodTrace.Services;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace BloodTrace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public List<BloodUser> _bloodlatest;
        public Home()
        {
            InitializeComponent();
            this.Title = "Welcome" + " " + Settings.UserName.ToString();
            _bloodlatest = new List<BloodUser>();

        }


     
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                Settings.AccessToken = "";
                Settings.Password = "";
                Settings.UserName = "";
                Navigation.InsertPageBefore(new SignInPage(), this);
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("oops", "We could not log you out, check your internet connection and try again", "Ok");
            }
         
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushAsync(new FindBloodPage());
            }
            else
            {
               await DisplayAlert("oops..","Check your internet connection and try again","Ok");
            }
           
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushAsync(new RegisterBloodPage());
            }
            else
            {
                await DisplayAlert("oops..", "check your internet and try again", "Ok");
            }
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushAsync(new HelpPage());
            }
            else
            {
                await DisplayAlert("oops..", "Check your intermet connection and try again", "Ok");
            }
            
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                var dialog = await DisplayActionSheet("The following resource you are trying to access might have alot of data in it," +
             "Continue?", "", "", "Yes", "No");
                if (dialog == "Yes")
                {
                    FindBlood();
                }
                else
                {
                    return;
                }
            }
            else
            {
                await DisplayAlert("oops..", "Check your internet connection and try again", "Ok");
            }
           
        }
        private async void FindBlood()
        {
            using(UserDialogs.Instance.Loading("Please wait..", null, null, true, MaskType.Black))
                {

            
            try
            {
                ApiServices services = new ApiServices();
                var response = await services.LatestBloodUser();

                if (response.Count == 0)
                {
                    await DisplayAlert("Oops", "No latest Donor at the moment, check back later", "Ok");
                }
                else
                {
                        _bloodlatest = response;
                    //foreach (var item in response)
                    //{
                    //    _bloodlatest.Add(item);
                    //}
                    await Navigation.PushAsync(new LatestDonorPage(_bloodlatest));
                }

        }

            catch (Exception)
            {

                await DisplayAlert("Ooops...", "Something went wrong", "Ok");
    }
            }
        }
    }
}
