﻿using System.Collections.Generic;
using SpotifyAPI.Web;

namespace SpotiliveTryHard.Models
{
    public class Track
    {
        public int Index { get; set; }
        public string Artist { get; set; }
        
        public string Duration { get; set; }
        
        public string Name { get; set; }

        public Track(int index, string artist, string duration, string name)
        {
            Index = index;
            Artist = artist;
            Duration = duration;
            Name = name;
        }
    }
}