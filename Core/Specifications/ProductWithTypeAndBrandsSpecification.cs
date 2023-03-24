using Core.Entities;

namespace Core.Specifications;

public class ProductWithTypeAndBrandsSpecification: BaseSpecification<Product>
{
    public ProductWithTypeAndBrandsSpecification(ProductSpecParams productSpecParams): base(x => 
        (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
        (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
    ) {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddOrderBy(x => x.Name);
        ApplyPaging
        (
            productSpecParams.PageSize * (productSpecParams.PageIndex -1),
            productSpecParams.PageSize
        );

        if (string.IsNullOrEmpty(productSpecParams.Sort)) return;
        {
            switch (productSpecParams.Sort)
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