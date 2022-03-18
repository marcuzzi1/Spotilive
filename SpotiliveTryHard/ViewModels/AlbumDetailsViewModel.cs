using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using SpotifyAPI.Web;
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

        public ObservableCollection<Track> AlbumTracks
        {
            get { return GetValue<ObservableCollection<Track>>(); }
            set { SetValue(value); }
        }

        public AlbumDetailsViewModel(Album album)
        {
            Album = album;
            AlbumTracks = new ObservableCollection<Track>();
            _ = FillTracksListAsync(album.Id);
        }

        private async Task FillTracksListAsync(string albumId)
        {
            var album = await spotify.Albums.Get(albumId);
            // AlbumTracks.Clear();
            Debug.WriteLine("Et ça passe par là");
            for (int i = 0; i < album.Tracks.Items.Count; i++)
            {
                Debug.WriteLine("Saucisse numero : " +  i);
                var res = album.Tracks.Items[i];
                var track = new Track(
                    res.Artists[0].Name,
                    res.DurationMs,
                    res.Name
                );
                Debug.WriteLine(track.Name);
                AlbumTracks.Add(track);
                
            }
        }
    }
}