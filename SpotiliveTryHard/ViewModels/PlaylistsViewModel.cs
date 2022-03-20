using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotiliveTryHard.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.Diagnostics;

namespace SpotiliveTryHard.ViewModels
{
    public class PlaylistsViewModel : BaseViewModel
    {
        private static PlaylistsViewModel _instance = new PlaylistsViewModel();

        public static PlaylistsViewModel Instance { get { return _instance; } }

        public ObservableCollection<Playlist> ListOfPlaylists
        {
            get { return GetValue<ObservableCollection<Playlist>>(); }
            set { SetValue(value); }
        }

        public PlaylistsViewModel()
        {
            ListOfPlaylists = new ObservableCollection<Playlist>();
            /*
             * Par manque de temps, nous n'avons pas pu faire une base de données locale qui aurait garder les résulats de la dernière recherche. 
             * C'est ici que le remplissage via la base aurait pu se faire.
            */
        }

        /// <summary>
        /// Remplis la liste de playlists en fonction du filtre de recherche en paramètre.
        /// </summary>
        /// <param name="search">filre de recherche</param>
        /// <returns></returns>
        private async Task FillListAsync(string search)
        {
            var spotify = await InitSpotify();
            var searchResponse = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Playlist, search));

            ListOfPlaylists.Clear();

            // remplis la liste de playlists
            for (int i = 0; i < searchResponse.Playlists.Items.Count; i++)
            {
                var res = searchResponse.Playlists.Items[i];
                var playlist = new Playlist(
                    res.Id,
                    res.Name,
                    res.Images[0].Url,
                    res.Owner.DisplayName,
                    res.Description,
                    res.Tracks.Total.Value
                );
                ListOfPlaylists.Add(playlist);
            }
        }

        private ICommand _searchCommand;
        /// <summary>
        /// Appelle la fonction FillListAsync avec la recherche entrée par l'utilisateur via la barre de recherche.
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>((text) =>
                {
                    _ = FillListAsync(text);
                }));
            }
        }
    }
}