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
	public partial class DonorListPage : ContentPage
	{
       
		public DonorListPage (ObservableCollection<BloodUser> blooduser)
		{
			InitializeComponent ();
            donorList.ItemsSource = blooduser;
            
		}

        private async void donorList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                if (donorList.SelectedItem == null)
                {
                    return;
                }
                var selected = e.SelectedItem as BloodUser;
                donorList.SelectedItem = null;
               await Navigation.PushAsync(new ProfilePage(selected));
            }
            else
            {
              await   DisplayAlert("oops..", "Check your internet and try again", "Ok");
            }
        }

     
       
    }
}