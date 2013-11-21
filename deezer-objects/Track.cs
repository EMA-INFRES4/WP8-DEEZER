using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace deezer_objects
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int Duration { get; set; }
        public string Preview { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public string Type { get; set; }
    }
}
