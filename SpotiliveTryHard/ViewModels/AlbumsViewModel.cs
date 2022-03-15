using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SpotifyAPI.Web;
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

            //set => SetValue(value);
        }

        public AlbumsViewModel()
        {
            _ = InitListAsync();
        }

        public async Task InitListAsync()
        {
            //ListOfAlbums.Clear();
            Debug.WriteLine("TU VAS MARCHER ENCULE");
            var config = SpotifyClientConfig.CreateDefault();

            var request = new ClientCredentialsRequest("079c96c8d58e4cda9e5d1c9e9653c9e8", "07d4306a42d94b27aaddc2887e134c04");
            var response = await new OAuthClient(config).RequestToken(request);

            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            var searchResponse = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Album, "enfer"));
            Debug.WriteLine("TU VAS MARCHER BATARD");
            Debug.WriteLine(searchResponse.Albums.Items.Count);
            for (int i = 0; i < searchResponse.Albums.Items.Count; i++)
            {
                var album = await spotify.Albums.Get(searchResponse.Albums.Items[i].Id);
                this.ListOfAlbums.Add(album);
            }
            Debug.WriteLine("C'est tout bon");
        }
    }
}
