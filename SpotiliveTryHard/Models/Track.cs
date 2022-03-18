using System.Collections.Generic;
using SpotifyAPI.Web;

namespace SpotiliveTryHard.Models
{
    public class Track
    {
        public string Artist { get; set; }
        
        public int Duration { get; set; }
        
        public string Name { get; set; }

        public Track(string artist, int duration, string name)
        {
            Artist = artist;
            Duration = duration;
            Name = name;
        }
    }
}