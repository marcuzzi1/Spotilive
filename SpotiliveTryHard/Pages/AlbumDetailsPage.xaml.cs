using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class AlbumDetailsPage : ContentPage
    {
        public AlbumDetailsPage(Album album)
        {
            BindingContext = new AlbumDetailsViewModel(album);
            InitializeComponent();
        }
    }
}