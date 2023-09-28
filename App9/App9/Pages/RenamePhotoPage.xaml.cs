using App9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App9.Pages
{
    /// <summary>
    /// Создаёт копию фотографии с новым именем
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RenamePhotoPage : ContentPage
    {
        public PhotoInfo PhotoInfo { get; set; }
        public string ChangeOldName { get; set; } = "Изменить старое имя - ";
        public RenamePhotoPage(PhotoInfo photoInfo)
        {
            InitializeComponent();

            PhotoInfo = photoInfo;
            ChangeOldName += PhotoInfo.Title;
        }

        protected override void OnAppearing()
        {
            BindingContext = this;

            base.OnAppearing();
        }

        void RenamePhoto(object sender, EventArgs e)
        {
            if (!entryName.Text.Contains(".jpg"))
            {
                entryName.Text += ".jpg";
            }

            var path = new ImageSourceConverter().ConvertToInvariantString(PhotoInfo.ImageSource);
            var newPath = path.Replace(PhotoInfo.Title, entryName.Text);
            File.Move(path, newPath);

            PhotoInfo.Title = entryName.Text;
            PhotoInfo.ImageSource = newPath;
        }

        void EntryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(entryName.Text.Length == 0 || entryName.Text == PhotoInfo.Title)
            {
                renameButton.IsEnabled = false;
            }
            else
            {
                renameButton.IsEnabled = true;
            }
        }

        async void Return(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}