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
        public ObservableCollection<FullAlbum> ListOfAlbums
        {
            get => GetValue<ObservableCollection<FullAlbum>>();

            set => SetValue(value);
        }

        public AlbumsViewModel()
        {
            _ = InitListAsync();
        }

        public async Task InitListAsync()
        {
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest("04a12d4c788943579aa277274d179ac8", "8d914dd3ad334fc4a09709ad2175284e");
            var response = await new OAuthClient(config).RequestToken(request);

            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            var searchResponse = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Album, "Carvery"));

            for (int i = 0; i < searchResponse.Albums.Total; i++)
            {
                var album = await spotify.Albums.Get(searchResponse.Albums.Items[i].Id);
                ListOfAlbums.Add(album);

                Debug.WriteLine(album.Name);
                Debug.WriteLine(album.Artists[0].Name);
            }
            
        }
    }
}
