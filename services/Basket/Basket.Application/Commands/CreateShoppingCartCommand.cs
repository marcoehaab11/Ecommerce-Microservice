using Basket.Application.Responses;
using Basket.Core.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public class CreateShoppingCartCommand :IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }


        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public CreateShoppingCartCommand(string userName)
        {
            UserName=userName;
        }
    }
}
