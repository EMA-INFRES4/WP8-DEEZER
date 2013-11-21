﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deezer_objects
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public Artist Artist { get; set; }
        public string Type { get; set; }
    }
}
