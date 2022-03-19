using SpotiliveTryHard.Models;
using SpotiliveTryHard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotiliveTryHard.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistDetailsPage : ContentPage
    {
        public PlaylistDetailsPage(Playlist playlist)
        {
            BindingContext = new PlaylistDetailsViewModel(playlist);
            InitializeComponent();
        }
    }
}