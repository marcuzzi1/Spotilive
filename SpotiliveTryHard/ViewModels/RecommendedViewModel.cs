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
    public class RecommendedViewModel : BaseViewModel
    {
        public string Message
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<Playlist> ListOfRecommendations
        {
            get { return GetValue<ObservableCollection<Playlist>>(); }
            set { SetValue(value); }
        }

        public RecommendedViewModel()
        {
            ListOfRecommendations = new ObservableCollection<Playlist>();
            _ = FillRecommendationsAsync();
        }

        private async Task FillRecommendationsAsync()
        {
            var spotify = await InitSpotify();

            var request = new FeaturedPlaylistsRequest
            {
                Country = "FR",
                Locale = "fr_FR",
            };
            var response = await spotify.Browse.GetFeaturedPlaylists(request);

            Message = response.Message;

            for (int i = 0; i < response.Playlists.Items.Count; i++)
            {
                var res = response.Playlists.Items[i];
                var playlist = new Playlist(
                    res.Id,
                    res.Name,
                    res.Images[0].Url,
                    res.Owner.DisplayName,
                    res.Description,
                    res.Tracks.Total.Value
                );
                ListOfRecommendations.Add(playlist);
            }
        }
    }
}