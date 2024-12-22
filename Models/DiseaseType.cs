using System;
using System.Collections.Generic;

namespace disaster_management.Models
{
    public partial class DiseaseType
    {
        public DiseaseType()
        {
            Outbreaks = new HashSet<Outbreak>();
        }

        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Severity { get; set; } // Nghiem trong
        public string? DiseaseGroup { get; set; }

        public virtual ICollection<Outbreak> Outbreaks { get; set; }
    }
}
