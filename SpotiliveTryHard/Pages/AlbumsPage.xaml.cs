using SpotiliveTryHard.ViewModels;
using System;

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

        private async void ViewCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}