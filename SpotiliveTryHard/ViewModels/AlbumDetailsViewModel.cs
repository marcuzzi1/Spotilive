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

        /// <summary>
        /// Créé un AlbumDetailsViewModel avec un objet Album en entrée
        /// </summary>
        /// <param name="album"></param>
        public AlbumDetailsViewModel(Album album)
        {
            Album = album;
            AlbumTracks = new ObservableCollection<Track>();
            _ = FillTracksListAsync(album.Id);
        }

        /// <summary>
        /// Créé un AlbumDetailsViewModel avec un id d'album en entrée
        /// </summary>
        /// <param name="album"></param>
        public AlbumDetailsViewModel(string albumId)
        {
            AlbumTracks = new ObservableCollection<Track>();
            _ = FillTracksListAsync(albumId);
        }

        /// <summary>
        /// Remplis la liste de musiques de l'album
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        private async Task FillTracksListAsync(string albumId)
        {
            var spotify = await InitSpotify();

            var album = await spotify.Albums.Get(albumId);

            // Initialise la propriété Album si la création du ViewModel s'est fait avec l'ID
            Album = new Album(
                    album.Id,
                    album.Name,
                    album.Images[0].Url,
                    album.Artists[0].Name,
                    DateTime.Parse(album.ReleaseDate).Year.ToString(),
                    album.TotalTracks
                );

            // remplis la liste de musiques
            for (int i = 0; i < album.Tracks.Items.Count; i++)
            {
                var res = album.Tracks.Items[i];

                // Mets en forme la durée des musiques
                TimeSpan time = TimeSpan.FromMilliseconds(res.DurationMs);
                var parts = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", time.Hours, time.Minutes, time.Seconds)
                                  .Split(':')
                                  .SkipWhile(s => Regex.Match(s, @"^00\w").Success)
                                  .ToArray();
                var duration = string.Join(":", parts).TrimStart('0');

                // Mets en forme la liste des artistes pour chaque musique
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