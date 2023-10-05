using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class DistributedLocks
    {
        public int IdDistributedLocks { get; set; }
        public string? Resource { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
