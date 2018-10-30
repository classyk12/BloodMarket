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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        { 
            InitializeComponent();
            EntryEmail.Completed += (s, e) => EntryPassword.Focus();
            EntryPassword.Completed += (s, e) => EntryPassword2.Focus();
            EntryPassword2.Completed += (s, e) => BtnSignUp_Clicked(s, e);
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

           
            using(UserDialogs.Instance.Loading("please wait....Signing you up",null,null,true,MaskType.Black))
                {
       
            try
            {

            
          var  Services = new ApiServices();
          bool response =   await Services.RegisterUser(EntryEmail.Text,EntryPassword.Text,EntryPassword2.Text);
            if(response)
            {
              await   DisplayAlert("Successful", "Your Account has been created, you can now Sign In", "Ok");
               await  Navigation.PopToRootAsync();
            }
            else
            {
               await  DisplayAlert("Oops", "Be sure to use at least an Uppercase Letter(A-Z) and a special character(! @ - _) and try again", "Ok");
            }
            }
            catch(Exception)
            {
                await DisplayAlert("Ooops!!", "Something went wrong", "Ok");
            }
            }
        }
            else
            {
               await  DisplayAlert("oops..", "Check your internet connection and try again","Ok");
            }
        }
    }
}
