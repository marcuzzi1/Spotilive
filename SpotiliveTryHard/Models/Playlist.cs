using System;
using System.Collections.Generic;
using System.Text;

namespace SpotiliveTryHard.Models
{
    public class Playlist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int TotalTracks { get; set; }

        public Playlist(string id, string name, string thumbnailUrl, string author, string description, int totalTracks)
        {
            Id = id;
            Name = name;
            Thumbnail = thumbnailUrl;
            Author = author;
            Description = description;
            TotalTracks = totalTracks;
        }
    }
}
