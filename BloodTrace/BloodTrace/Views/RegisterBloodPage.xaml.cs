using Acr.UserDialogs;
using BloodTrace.Helpers;
using BloodTrace.Services;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
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
    public partial class RegisterBloodPage : ContentPage
    {
        public static MediaFile file;
        public RegisterBloodPage()
        {
            InitializeComponent();
            nametextbox.Completed += (s, e) => emailtextbox.Focus();
            emailtextbox.Completed += (s, e) => phonetextbox.Focus();
            phonetextbox.Completed += (s, e) => BloodPicker.Focus();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {


                await CrossMedia.Current.Initialize();
                var dialog = await DisplayActionSheet("Additional Prompt..", "", "", "Take Photo", "Choose from Gallery");
                if (dialog == "Take Photo")
                {

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("No Camera", ":( No camera available.", "OK");
                        return;
                    }

                    file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        //PhotoSize = PhotoSize.Medium,
                        //CompressionQuality = 80,
                        Directory = "Sample",
                        Name = "test.jpg"
                    });
                }

                else
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("No Storage", ":( No storage available.", "OK");
                        return;
                    }

                    file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        CompressionQuality = 80

                    });


                }
                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");


                ImgDonar.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });


            }
            catch (Exception)
            {
                await DisplayAlert("No Permission Granted", "Go to Settings>Apps>BloodTrace>Permissions to fix this", "Ok");
                CrossPermissions.Current.OpenAppSettings();
            }
        }

        private async void RoundButton_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {


                using (UserDialogs.Instance.Loading("Please give us a minute", null, null, true, MaskType.Black))
                {

                    try
                    {

                        var imageArray = Helpers.filehelper.Readfully(file.GetStream());
                        // file.Dispose(); //is used to destroy this file so as to prevent memnfluxory i
                        var country = CountryPicker.Items[CountryPicker.SelectedIndex];
                        var bloodgroup = BloodPicker.Items[BloodPicker.SelectedIndex];


                        var register = new Models.BloodUser
                        {
                            Username = nametextbox.Text,
                            Email = Settings.UserName.ToString(),
                            Phone = phonetextbox.Text,
                            Country = country,
                            BloodGroup = bloodgroup,
                            ImageArray = imageArray,

                        };
                        ApiServices services = new ApiServices();

                        var response = await services.RegisterDonor(register);

                        if (!response)
                        {
                            await DisplayAlert("Ooops!!", "Something went wrong", "Ok");
                        }
                        else
                        {

                            await DisplayAlert("Hello...", "Record added Successfully", "Ok");
                            Navigation.InsertPageBefore(new Home(), this);
                            await Navigation.PopAsync();
                        }
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Ooops...", "Image or Text Fields cannot be empty,check and retry", "Ok");

                    }
                }

            }
            else
            {
               await DisplayAlert("oops..", "Check your internet connection and try again", "Ok");
            }
        }
    }
}