using System.Runtime.CompilerServices;
using SpotiliveTryHard.Models;

namespace SpotiliveTryHard.ViewModels
{
    public class AlbumDetailsViewModel : BaseViewModel
    {
        public Album Album
        {
            get { return GetValue<Album>(); }
            set { SetValue(value); }
        }

        public AlbumDetailsViewModel(Album album)
        {
            Album = album;
        }
    }
}