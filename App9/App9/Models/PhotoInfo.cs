using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace App9.Models
{
    public class PhotoInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; set; }
        string title;
        ImageSource imageSource;
        public string Title
        {
            get => title;
            set
            {
                if(title != value)
                {
                    title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        public string CreatedDate { get; }
        public ImageSource ImageSource 
        {
            get => imageSource;
            set
            {
                if(imageSource != value)
                {
                    imageSource = value;
                    NotifyPropertyChanged("ImageSource");
                }
            }
        }

        public PhotoInfo(string _title = null, ImageSource _imageSource = null, DateTime createdDate = default) 
        {
            Id = Guid.NewGuid();
            Title = _title;
            ImageSource = _imageSource;
            CreatedDate = createdDate.ToShortDateString();
        }

        void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
