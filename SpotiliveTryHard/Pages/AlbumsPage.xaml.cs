using SpotiliveTryHard.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using SpotiliveTryHard.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotiliveTryHard.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumsPage : ContentPage
    {
        public AlbumsPage()
        {
            this.BindingContext = AlbumsViewModel.Instance;
            InitializeComponent();
        }

        private async void ViewCell_Tapped(object sender, SelectionChangedEventArgs e)
        {
            Album album = e.CurrentSelection.FirstOrDefault() as Album;
            if (album == null)
            {
                return;
            }

            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new AlbumDetails(album));
        }
    }
}