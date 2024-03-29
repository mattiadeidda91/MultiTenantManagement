﻿using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Rates : TenantEntity
    {
        public double? Daily { get; set; }
        public double? Weekly { get; set; }
        public double? Monthly { get; set; }
        public double? Quarterly { get; set; }
        public double? HalfYearly { get; set; }
        public double? Annual { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivityId { get; set; }

        public virtual Activity Activity { get; set; } = null!;
    }
}
