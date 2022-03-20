using SpotifyAPI.Web;
using System;

namespace SpotiliveTryHard.Models
{
    public class Album
    {
        // ID Spotify de l'album
        public string Id { get; set; }
        // Nom de l'album
        public string Name { get; set; }
        // Pochette de l'album
        public string Thumbnail { get; set; }
        //Nom de l'artiste
        public string Artist { get; set; }
        // Date de sortie de l'album
        public string Date { get; set; }
        // Nombre total de musiques présentes dans l'album
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
