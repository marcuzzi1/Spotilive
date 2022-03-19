using SpotiliveTryHard.Models;
using SpotiliveTryHard.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotiliveTryHard.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayListPage : ContentPage
    {
        public PlayListPage()
        {
            BindingContext = PlaylistsViewModel.Instance;
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