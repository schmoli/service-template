using System;
using System.Collections.Generic;

namespace Schmoli.ServiceTemplate.Models
{
    public class SecondaryItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<PrimaryItem> PrimaryItems { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
