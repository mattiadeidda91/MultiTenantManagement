﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MultiTenantManagement.Abstractions.Models.Entities.Common
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
