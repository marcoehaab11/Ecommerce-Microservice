using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, IList<BrandResponsDto>>
    {
        private readonly Mapper _mapper;
        private readonly IProductBrand _brandRepository;
        public GetAllBrandQueryHandler
            (IProductBrand brandRepository,
            IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = (Mapper)mapper;
        }
        public async Task<IList<BrandResponsDto>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetProductBrands();
            //var mappedBrands = _mapper.Map<IList<BrandResponsDto>>(brands);
            var mappedBrandsList = _mapper.Map<IList<ProductBrand>, IList<BrandResponsDto>>(brands.ToList());
            return mappedBrandsList;
        }
    }
}
