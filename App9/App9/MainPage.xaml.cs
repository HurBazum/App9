using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using App9.Pages;

namespace App9
{
    public partial class MainPage : ContentPage
    {
        public string Pin { get; set; } //9959

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (Preferences.ContainsKey("pin"))
            {
                Pin = Preferences.Get("pin", "");
            }
            else
            {
                Preferences.Set("pin", "");
            }

            base.OnAppearing();
        }


        async void TryToAccess(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(errorMessage.Text))
            {
                errorMessage.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(Pin))
            {
                Pin = pinEntry.Text;
                Preferences.Set("pin", pinEntry.Text);
                pinEntry.Text = string.Empty;
                await Navigation.PushAsync(new PhotoListPage());
            }
            else if (pinEntry.Text == Pin)
            {
                pinEntry.Text = string.Empty;
                await Navigation.PushAsync(new PhotoListPage());
            }
            else
            {
                errorMessage.Text = $"Неверный пин-код: {Pin}";
            }
        }

        void PinEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pinEntry.Text.Length < 4)
            {
                accessButton.IsEnabled = false;
            }
            else
            {
                accessButton.IsEnabled = true;
            }
        }
    }
}
