using System;
using System.Collections.Generic;

namespace disaster_management.Models
{
    public partial class LivestockStatistic
    {
        public int StatisticId { get; set; }
        public int? FarmId { get; set; }
        public DateTime? StatisticDate { get; set; }
        public string? AnimalType { get; set; }
        public int? AnimalCount { get; set; }

        public virtual LivestockFarm? Farm { get; set; }
    }
}
