using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotiliveTryHard.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace SpotiliveTryHard.ViewModels
{
    public class AlbumsViewModel : BaseViewModel
    {
        private static AlbumsViewModel _instance = new AlbumsViewModel();

        public static AlbumsViewModel Instance { get { return _instance; } }

        public ObservableCollection<Album> ListOfAlbums
        {
            get { return GetValue<ObservableCollection<Album>>(); }
            set { SetValue(value); }
        }

        public AlbumsViewModel()
        {
            ListOfAlbums = new ObservableCollection<Album>();
            /*
             * Par manque de temps, nous n'avons pas pu faire une base de donn�es locale qui aurait garder les r�sulats de la derni�re recherche. 
             * C'est ici que le remplissage via la base aurait pu se faire.
            */
        }

        /// <summary>
        /// Remplis la liste d'albums en fonction du filtre de recherche en param�tre.
        /// </summary>
        /// <param name="search">filre de recherche</param>
        /// <returns></returns>
        private async Task FillListAsync(string search)
        {
            var spotify = await InitSpotify();

            var searchResponse = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Album, search));

            // vide la liste si une pr�c�dente recherche a d�j� �t� faite
            ListOfAlbums.Clear();

            // remplis la liste des albums
            for (int i = 0; i < searchResponse.Albums.Items.Count; i++)
            {
                var res = searchResponse.Albums.Items[i];
                var album = new Album(
                    res.Id,
                    res.Name,
                    res.Images[0].Url,
                    res.Artists[0].Name,
                    DateTime.Parse(res.ReleaseDate).Year.ToString(),
                    res.TotalTracks
                );
                ListOfAlbums.Add(album);
            }
        }

        private ICommand _searchCommand;
        /// <summary>
        /// Appelle la fonction FillListAsync avec la recherche entr�e par l'utilisateur via la barre de recherche.
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