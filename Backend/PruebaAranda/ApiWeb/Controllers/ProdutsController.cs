using Application.CQRS.Products.Commands.Create;
using Application.CQRS.Products.Commands.Delete;
using Application.CQRS.Products.Commands.Update;
using Application.CQRS.Products.Queries;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<ResponseDto> Get([FromQuery] GetProductsQuery query)
                => _mediator.Send(query);

        [HttpGet("{Id}")]
        public Task<ResponseDto> Get([FromRoute] GetProductQuery query)
                => _mediator.Send(query);

        [HttpPost]
        public Task<ResponseDto> Post([FromBody] CreateProductCommand command)
                => _mediator.Send(command);

        [HttpPut]
        public Task<ResponseDto> Put([FromBody] UpdateProductCommand command)
                => _mediator.Send(command);

        [HttpDelete("{Id}")]
        public Task<ResponseDto> Delete([FromRoute] DeleteProductCommand query)
                => _mediator.Send(query);
    }
}
