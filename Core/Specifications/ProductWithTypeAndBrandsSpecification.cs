﻿using Core.Entities;

namespace Core.Specifications;

public class ProductWithTypeAndBrandsSpecification: BaseSpecification<Product>
{
    public ProductWithTypeAndBrandsSpecification(string sort, int? brandId, int? typeId): base(x => 
        (!brandId.HasValue || x.ProductBrandId == brandId) &&
        (!typeId.HasValue || x.ProductTypeId == typeId)
    ) {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddOrderBy(x => x.Name);

        if (string.IsNullOrEmpty(sort)) return;
        {
            switch (sort)
            {
                case "PriceAsc": AddOrderBy(p => p.Price);
                    break;
                case "PriceDesc": AddOrderByDescending(p => p.Price);
                    break;
                default: AddOrderBy(x => x.Name);
                    break;
            }
        }
    }

    public ProductWithTypeAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}