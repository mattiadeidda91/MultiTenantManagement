﻿namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public double Price { get; set; }
    }
}
