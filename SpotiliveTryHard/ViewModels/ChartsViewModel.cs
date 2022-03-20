using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotiliveTryHard.Models;

namespace SpotiliveTryHard.ViewModels
{
    public class ChartsViewModel : BaseViewModel
    {
        public ObservableCollection<Track> WorldTopMusic
        {
            get { return GetValue<ObservableCollection<Track>>(); }
            set { SetValue(value); }
        }

        public ChartsViewModel()
        {
            WorldTopMusic = new ObservableCollection<Track>();
            _ = FillTopMusicsAsync();
        }

        private async Task FillTopMusicsAsync()
        {
            Debug.WriteLine("Affichage des tops");
            var spotify = await InitSpotify();
            Debug.WriteLine("Spotify client initialisé");
            var personalizationRequest = new PersonalizationTopRequest();
            Debug.WriteLine("Requete perso");
            personalizationRequest.Limit = 20;
            Debug.WriteLine("Limite de la requete");
            var topMusics = await spotify.Personalization.GetTopTracks(personalizationRequest);
            Debug.WriteLine("Recup des tops");
            WorldTopMusic.Clear();
            Debug.WriteLine("Vidage de la liste");
            Debug.WriteLine(topMusics.Items.Count);
            for (int i = 0; i < topMusics.Items.Count; i++)
            {
                var res = topMusics.Items[i];
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
                    res.Album.Id
                );
                WorldTopMusic.Add(track);
            }
        }
    }
}