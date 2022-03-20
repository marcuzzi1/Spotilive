using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
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

        public AlbumDetailsViewModel(string albumId)
        {
            AlbumTracks = new ObservableCollection<Track>();
            _ = FillTracksListAsync(albumId);
        }

        private async Task FillTracksListAsync(string albumId)
        {
            var spotify = await InitSpotify();

            var album = await spotify.Albums.Get(albumId);

            // sets the Album property if we used the constructor with the albumId
            Album = new Album(
                    album.Id,
                    album.Name,
                    album.Images[0].Url,
                    album.Artists[0].Name,
                    DateTime.Parse(album.ReleaseDate).Year.ToString(),
                    album.TotalTracks
                );

            for (int i = 0; i < album.Tracks.Items.Count; i++)
            {
                var res = album.Tracks.Items[i];

                TimeSpan time = TimeSpan.FromMilliseconds(res.DurationMs);
                var parts = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", time.Hours, time.Minutes, time.Seconds)
                                  .Split(':')
                                  .SkipWhile(s => Regex.Match(s, @"^00\w").Success)
                                  .ToArray();
                var duration = string.Join(":", parts).TrimStart('0');

                var artists = res.Artists.ToList();
                var names = new List<string>();

                foreach(var artist in artists)
                {
                    names.Add(artist.Name);
                }

                var track = new Track(
                    i + 1,
                    string.Join(", ", names),
                    duration,
                    res.Name,
                    albumId
                );
                AlbumTracks.Add(track);
            }
        }
    }
}