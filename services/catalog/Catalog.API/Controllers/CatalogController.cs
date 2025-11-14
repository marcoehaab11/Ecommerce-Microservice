using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IMediator mediator, ILogger<CatalogController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet]
        [Route("[action]/{id}",Name ="GetProuctById")]
        [ProducesResponseType(typeof(ProductResponsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponsDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            _logger.LogInformation("Product with id: {ProductId} featched", id);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{typeName}", Name = "GetProuctsByTypeName")]
        [ProducesResponseType(typeof(IList<ProductResponsDto>), (int)HttpStatusCode.OK)]
       
        public async Task<ActionResult<ProductResponsDto>> GetProuctsByTypeName(string typeName)
        {
            var query = new GetAllProductByTypeNameQuery(typeName);
            var result = await _mediator.Send(query);
            _logger.LogInformation($"Product with Type: {typeName} featched");

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponsDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponsDto>> GetAllProducts([FromQuery] CatalogSpecParams spec)
        {
            var query = new GetAllProductQuery(spec);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponsDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BrandResponsDto>> GetAllBrands()
        {
            var query = new GetAllBrandQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetAllTypess")]
        [ProducesResponseType(typeof(IList<TypeResponsDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TypeResponsDto>> GetAllTypess()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponsDto),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponsDto>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send<ProductResponsDto>(productCommand);
            return Ok();
        }


        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand productCommand)
        {
            var result = await _mediator.Send<bool>(productCommand);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}",Name="DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var command= new DeleteProductCommand(id);

            var result = await _mediator.Send<bool>(command);
            return Ok();
        }



    }
}
