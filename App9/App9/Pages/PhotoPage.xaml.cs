using App9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App9.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoPage : ContentPage
    {
        public ImageSource ImageSrc { get; set; }
        public string ImageTitle { get; set; }
        public string ImageCreatedDate { get; set; }
        public PhotoPage(PhotoInfo photoInfo)
        {
            InitializeComponent();

            ImageSrc = photoInfo.ImageSource;
            ImageTitle = photoInfo.Title;
            ImageCreatedDate = photoInfo.CreatedDate;
        }

        protected override void OnAppearing()
        {
            BindingContext = this;

            base.OnAppearing();
        }

        async void Return(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}