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
        }

        private async Task FillListAsync(string search)
        {
            var searchResponse = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Album, search));

            ListOfAlbums.Clear();

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