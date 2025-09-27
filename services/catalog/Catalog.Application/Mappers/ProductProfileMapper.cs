using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Mappers
{
    public class ProductProfileMapper:Profile
    {
        public ProductProfileMapper()
        {
                CreateMap<Product,ProductResponsDto>().ReverseMap();
                CreateMap<Pagination<Product>,Pagination<ProductResponsDto>>().ReverseMap();

                CreateMap<ProductBrand,BrandResponsDto>().ReverseMap();
                CreateMap<ProductType, TypeResponsDto>().ReverseMap();

                 CreateMap<CreateProductCommand, Product>();
        }
    }
}
