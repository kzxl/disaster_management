using System;
using System.Collections.Generic;

namespace disaster_management.Models
{
    public partial class VetMedicineAgency
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
