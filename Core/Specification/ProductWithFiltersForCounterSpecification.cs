﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductWithFiltersForCounterSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCounterSpecification(ProductSpecParams productParams) 
            : base(x =>
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            
        }
    }
}
