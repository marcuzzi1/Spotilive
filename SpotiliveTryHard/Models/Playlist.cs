using System;
using System.Collections.Generic;
using System.Text;

namespace SpotiliveTryHard.Models
{
    public class Playlist
    {
        // ID Spotify de la playlist
        public string Id { get; set; }
        // Nom de la playlist
        public string Name { get; set; }
        // Pochette de la playlist
        public string Thumbnail { get; set; }
        // Nom du créateur de la playlist
        public string Author { get; set; }
        // Description de la playlist
        public string Description { get; set; }
        // Nombre total de musiques dans la playlist
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
