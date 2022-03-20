using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ChartsViewModel(string country)
        {
            WorldTopMusic = new ObservableCollection<Track>();
        }

        private async Task FillCountryTopMusicsAsync(string country)
        {
            var spotify = await InitSpotify();
            var personalizationRequest = new PersonalizationTopRequest();
            personalizationRequest.Limit = 20;
            var topMusics = await spotify.Personalization.GetTopTracks(personalizationRequest);
            WorldTopMusic.Clear();
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