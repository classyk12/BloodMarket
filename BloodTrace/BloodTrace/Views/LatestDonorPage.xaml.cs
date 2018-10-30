using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTrace.Models;
using BloodTrace.Services;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodTrace.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LatestDonorPage : ContentPage
	{
        
		public LatestDonorPage (List<BloodUser> _blooduser)
		{
			InitializeComponent ();
            donorList.ItemsSource = _blooduser;
		}
      
        
        private void donorList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                if (donorList.SelectedItem == null)
                {
                    return;
                }
                var selected = e.SelectedItem as BloodUser;
                donorList.SelectedItem = null;
                Navigation.PushAsync(new ProfilePage(selected));
            }
            else
            {
                DisplayAlert("oops..", "Check your internet and try again", "Ok");
            }
        }
    }
}