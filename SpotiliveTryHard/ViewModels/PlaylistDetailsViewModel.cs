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
    public class PlaylistDetailsViewModel : BaseViewModel
    {
        public Playlist Playlist
        {
            get { return GetValue<Playlist>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<Track> PlaylistTracks
        {
            get { return GetValue<ObservableCollection<Track>>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Créé un PlaylistDetailsViewModel avec un Playlist en entrée
        /// </summary>
        /// <param name="playlist"></param>
        public PlaylistDetailsViewModel(Playlist playlist)
        {
            Playlist = playlist;
            PlaylistTracks = new ObservableCollection<Track>();
            _ = FillTracksListAsync(Playlist.Id);
        }

        /// <summary>
        /// Remplis la liste de musiques de la playlist
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        private async Task FillTracksListAsync(string playlistId)
        {
            var spotify = await InitSpotify();

            var playlist = await spotify.Playlists.Get(playlistId);

            // remplis la liste de musiques
            for (int i = 0; i < playlist.Tracks.Items.Count; i++)
            {
                var res = playlist.Tracks.Items[i].Track;

                // Vérification que le résultat est bien une musique et non un épisode de Podcast
                if (res is FullTrack fullTrack)
                {
                    // Mets en forme la durée des musiques
                    TimeSpan time = TimeSpan.FromMilliseconds(fullTrack.DurationMs);
                    var parts = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", time.Hours, time.Minutes, time.Seconds)
                                      .Split(':')
                                      .SkipWhile(s => Regex.Match(s, @"^00\w").Success)
                                      .ToArray();
                    var duration = string.Join(":", parts).TrimStart('0');

                    // Mets en forme la liste des artistes pour chaque musique
                    var artists = fullTrack.Artists.ToList();
                    var names = new List<string>();
                    foreach (var artist in artists)
                    {
                        names.Add(artist.Name);
                    }

                    var track = new Track(
                        i + 1,
                        string.Join(", ", names),
                        duration,
                        fullTrack.Name,
                        fullTrack.Album.Id
                    );
                    PlaylistTracks.Add(track);
                }
            }
        }
    }
}