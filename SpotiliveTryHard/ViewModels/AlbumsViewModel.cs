using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotiliveTryHard.Models;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

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
            _ = FillListAsync("TEST");
        }

        public async Task FillListAsync(string search)
        {
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest("04a12d4c788943579aa277274d179ac8", "f4cec9611d5245d39efa7c64992b0484");
            var response = await new OAuthClient(config).RequestToken(request);

            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            var searchResponse = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Album, search));

            ListOfAlbums.Clear();

            for (int i = 0; i < searchResponse.Albums.Items.Count; i++)
            {
                Debug.WriteLine(searchResponse.Albums.Items[i].Name);
                Debug.WriteLine(searchResponse.Albums.Items[i].Artists[0].Name);

                var album = new Album(searchResponse.Albums.Items[i].Name);

                ListOfAlbums.Add(album);
            }
        }

        private ICommand _searchCommand;
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