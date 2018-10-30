using BloodTrace.Models;
using Plugin.Messaging;
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
    public partial class ProfilePage : ContentPage
    {
        private string _email;
        private string _phone;

        public ProfilePage(BloodUser blooduser)
        {
       
            InitializeComponent();
            Pic.Source = blooduser.FullLogoPath;
            DonarImg.Source = blooduser.FullLogoPath;
            LblBloodGroup.Text = blooduser.BloodGroup;
            LblCountry.Text = blooduser.Country;
            NameLbl.Text = blooduser.Username;
            _email = blooduser.Email;
            _phone = blooduser.Phone;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //tap event for call
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(_phone);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //tap event for email
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                emailMessenger.SendEmail(_email, "Write a Subject", "Write Email Body here");

            }
            }
        }
}