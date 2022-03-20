using System.Collections.Generic;
using SpotifyAPI.Web;

namespace SpotiliveTryHard.Models
{
    public class Track
    {
        // Position de la musique dans l'album / la playlist
        public int Index { get; set; }
        // Nom de(s) la/les artiste(s) de la musique
        public string Artist { get; set; }
        // Durée de la musique
        public string Duration { get; set; }
        // Nom de la musique
        public string Name { get; set; }
        // ID Spotify de l'album de la musique
        public string AlbumId { get; set; }

        public Track(int index, string artist, string duration, string name, string albumId)
        {
            Index = index;
            Artist = artist;
            Duration = duration;
            Name = name;
            AlbumId = albumId;
        }
    }
}