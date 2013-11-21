using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deezer_objects
{
    public class TracksResponse
    {
        public DataList<Track> Tracks { get; set; }
        public DataList<Album> Albums { get; set; }
        public DataList<Artist> Artists { get; set; }
    }
}
