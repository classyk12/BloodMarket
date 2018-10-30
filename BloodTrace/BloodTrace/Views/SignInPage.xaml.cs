using Acr.UserDialogs;
using BloodTrace.Services;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodTrace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            EntryEmail.Completed += (s, e) => EntryPassword.Focus();
            EntryPassword.Completed += (s, e) => Button_Clicked(s, e);
           
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                using (UserDialogs.Instance.Loading("Please Wait..", null, null, true, MaskType.Black))
                {


                    try
                    {


                        ApiServices services = new ApiServices();
                        bool response = await services.LoginUser(EntryEmail.Text, EntryPassword.Text);
                        if (response)
                        {
                            Navigation.InsertPageBefore(new Home(), this);
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error", "Email or Password incorrect", "Ok");
                        }
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Ooops!", "Something went wrong", "Ok");
                    }
                }
            }
            else
            {
               await  DisplayAlert("oops..", "Check your internet connection and try again","Ok");
            }
        }
       


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                Navigation.PushAsync(new SignUpPage());
            }
            else
            {
                DisplayAlert("oops..", "Check your internet connection and try again", "Ok");
            }


        }
        
    }
}
