using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public class DeleteShoppingCartCommand:IRequest<Unit>
    {
        public string UserName { get; set; }
        public DeleteShoppingCartCommand(string userName)
        {
            UserName=userName;
        }
    }
}
