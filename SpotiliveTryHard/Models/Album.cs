using SpotifyAPI.Web;
using System;

namespace SpotiliveTryHard.Models
{
    public class Album
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }

        public Album(string name, string thumbnailUrl)
        {
            Name = name;
            Thumbnail = thumbnailUrl;
        }
    }
}
