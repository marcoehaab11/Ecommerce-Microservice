using AutoMapper;
using Discount.Core.Entities;
using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Mapper
{
    public class DiscoutProfile :Profile
    {
        public DiscoutProfile()
        {
            CreateMap<Coupon,CouponModel>().ReverseMap();
        }
    }
}
