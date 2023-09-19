using App9.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App9.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoListPage : ContentPage
    {
        public string PhotoCount { get; set; } = "Все фотографии";
        public ObservableCollection<PhotoInfo> Photos { get; set; } = new ObservableCollection<PhotoInfo>();

        public PhotoInfo SelectedItem;
        public PhotoListPage()
        {
            InitializeComponent();
            FillPhotos();
            PhotoCount += $"({Photos.Count})";
        }

        protected override void OnAppearing()
        {
            BindingContext = this;

            base.OnAppearing();
        }

        void FillPhotos()
        {
            var photosFromGallery = Directory.GetFiles("/storage/emulated/0/DCIM/Camera", "*.jpg");

            foreach(var photo in photosFromGallery)
            {
                var file = new FileInfo(photo);

                Photos.Add(new PhotoInfo(file.Name, photo, file.CreationTime));
            }
        }

        void SelectPhoto(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedItem = (PhotoInfo)e.SelectedItem;
        }

        async void Logout(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void ZoomPhoto(object sender, EventArgs e)
        {
            if(SelectedItem != null)
            {
                await Navigation.PushAsync(new PhotoPage(SelectedItem));
            }
        }

        async void UpdatePhoto(object sender, EventArgs e)
        {
            if(SelectedItem != null)
            {
                await Navigation.PushAsync(new RenamePhotoPage(SelectedItem));
            }
        }

        async void RemovePhoto(object sender, EventArgs e)
        {
            if(SelectedItem != null)
            {
                    var item = SelectedItem;

                    string path = new ImageSourceConverter().ConvertToInvariantString(item.ImageSource);

                    var result = await DisplayAlert("Удаление", "Уверены, что хотите удалить это фото?", "Yes", "No");

                    if (!result)
                    {
                        return;
                    }
                    else
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        Photos.Remove(item);

                        SelectedItem = null;
                    }
            }
        }
    }
}