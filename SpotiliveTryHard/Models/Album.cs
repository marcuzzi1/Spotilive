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
        public int TotalTracks { get; set; }

        public Album(string id, string name, string thumbnailUrl, string artist, string date, int totalTracks)
        {
            Id = id;
            Name = name;
            Thumbnail = thumbnailUrl;
            Artist = artist;
            Date = date;
            TotalTracks = totalTracks;
        }
    }
}
