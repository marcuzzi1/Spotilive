using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using spotilive.Models;
using System.Diagnostics;

namespace spotilive.ViewModels
{
    public class AlbumsViewModel : BaseViewModel
    {
        public static AlbumsViewModel _instance = new AlbumsViewModel();
        public static AlbumsViewModel Instance
        {
            get { return _instance; }
        }
        public ObservableCollection<Album> ListOfAlbums
        {
            get => GetValue<ObservableCollection<Album>>();

            set => SetValue(value);
        }

        public void AlbumsViewModelAsync()
        {
            _ = InitListAsync();
        }

        public async Task InitListAsync()
        {
            Debug.WriteLine("SALUT");
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest("04a12d4c788943579aa277274d179ac8", "8d914dd3ad334fc4a09709ad2175284e");
            var response = await new OAuthClient(config).RequestToken(request);

            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            var randsearch = spotify.Search.Item(new SearchRequest(SearchRequest.Types.Album, "Civilisation"));

            var albums = randsearch.Result.Albums.Items;

            for (int i = 0; i < randsearch.Result.Albums.Total; i++)
            {
                ListOfAlbums.Add(new Album(albums[i].Name));
                Debug.WriteLine(albums[i].Name);
            }
        }
    }
}
