using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SftpCache
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = null!;
        public DateTime LastWriteTime { get; set; }
    }
}
