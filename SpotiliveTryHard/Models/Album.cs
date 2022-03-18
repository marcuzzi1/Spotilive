using SpotifyAPI.Web;
using System;

namespace SpotiliveTryHard.Models
{
    public class Album
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Artist { get; set; }
        public string Date { get; set; }

        public Album(string Id, string name, string thumbnailUrl, string artist, string date)
        {
            Id = Id;
            Name = name;
            Thumbnail = thumbnailUrl;
            Artist = artist;
            Date = date;
        }
    }
}
