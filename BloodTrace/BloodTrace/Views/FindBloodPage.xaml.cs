using Acr.UserDialogs;
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
    public partial class FindBloodPage : ContentPage
    {
        public ObservableCollection<BloodUser> _blooduser;

        public FindBloodPage()
        {
            InitializeComponent();
          
            _blooduser = new ObservableCollection<BloodUser>();

        }

        private async void RoundButton_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                FindBlood();
            }
            else
            {
                await DisplayAlert("oops!","Check your internet connection and try again","Ok");
            }

        }
        private async void FindBlood()
        {
            using (UserDialogs.Instance.Loading("Please Wait..", null, null, true, MaskType.Black))
            {
                try
                {

                    ApiServices services = new ApiServices();
                    var bloodgroup = BloodgroupPicker.Items[BloodgroupPicker.SelectedIndex].Trim().Replace("+", "%2B");
                    var country = CountryPicker.Items[CountryPicker.SelectedIndex];
                    var response = await services.FindBlood(bloodgroup, country);
                    if (response.Count == 0)
                    {
                        await DisplayAlert("Oops", "No Records found, try a diffrent search query", "Ok");
                    }
                    else
                    {

                        _blooduser = response;
                        //foreach (var item in response)
                        //{
                        //    _blooduser.Add(item);
                        //}

                        await Navigation.PushAsync(new DonorListPage(_blooduser));
                    }

                }

                catch (Exception)
                {

                    await DisplayAlert("Ooops...", "Please select a search query and try again", "Ok");
                }
            }
           
        }

    }
    }