﻿using MultiTenantManagement.Abstractions.Models.Entities.Common;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class CustomerActivity : TenantEntity
    {
        public Guid CustomerId { get; set; }
        public Guid ActivityId { get; set; }
        public Guid? SiteId { get; set; }

        public virtual Activity Activity { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        //public virtual Site Site { get; set; } = null!; TDB
    }
}
