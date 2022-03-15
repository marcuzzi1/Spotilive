<<<<<<< HEAD
﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SpotifyAPI.Web;
=======
﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotiliveTryHard.Models;
>>>>>>> eda22badb2108927e1a31b0bf2c9f300a0a0b736
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpotiliveTryHard.ViewModels
{
    public class AlbumsViewModel : BaseViewModel
    {
        private static AlbumsViewModel _instance = new AlbumsViewModel();

<<<<<<< HEAD
            //set => SetValue(value);
=======
        public static AlbumsViewModel Instance { get { return _instance; } }

        public ObservableCollection<Album> ListOfAlbums
        {
            get { return GetValue<ObservableCollection<Album>>(); }
            set { SetValue(value); }
>>>>>>> eda22badb2108927e1a31b0bf2c9f300a0a0b736
        }

        public AlbumsViewModel()
        {
            ListOfAlbums = new ObservableCollection<Album>();
            _ = FillListAsync("TEST");
        }

        public async Task FillListAsync(string search)
        {
            //ListOfAlbums.Clear();
            Debug.WriteLine("TU VAS MARCHER ENCULE");
            var config = SpotifyClientConfig.CreateDefault();

<<<<<<< HEAD
            var request = new ClientCredentialsRequest("079c96c8d58e4cda9e5d1c9e9653c9e8", "07d4306a42d94b27aaddc2887e134c04");
=======
            var request = new ClientCredentialsRequest("04a12d4c788943579aa277274d179ac8", "f4cec9611d5245d39efa7c64992b0484");
>>>>>>> eda22badb2108927e1a31b0bf2c9f300a0a0b736
            var response = await new OAuthClient(config).RequestToken(request);

            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

<<<<<<< HEAD
            var searchResponse = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Album, "enfer"));
            Debug.WriteLine("TU VAS MARCHER BATARD");
            Debug.WriteLine(searchResponse.Albums.Items.Count);
            for (int i = 0; i < searchResponse.Albums.Items.Count; i++)
            {
                var album = await spotify.Albums.Get(searchResponse.Albums.Items[i].Id);
                this.ListOfAlbums.Add(album);
            }
            Debug.WriteLine("C'est tout bon");
=======
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
>>>>>>> eda22badb2108927e1a31b0bf2c9f300a0a0b736
        }
    }
}
