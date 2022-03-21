using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotiliveTryHard.Models;
using SpotiliveTryHard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotiliveTryHard.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecommendedPage : ContentPage
    {
        public RecommendedPage()
        {
            BindingContext = new RecommendedViewModel();
            InitializeComponent();
        }

        private async void ViewCell_Tapped(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is Playlist playlist))
            {
                return;
            }

            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new PlaylistDetailsPage(playlist));
        }
    }
}