using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Mapper
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
                CreateMap<ShoppingCart,ShoppingCartResponse>().ReverseMap();
                CreateMap<ShoppingCartItem, ShoppingCartItem>().ReverseMap();
        }
    }
}
