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
        public async Task<ResponseDto> Get([FromQuery] GetProductsQuery query)
                => await _mediator.Send(query);

        [HttpGet("{ProductId}")]
        public async Task<ResponseDto> Get([FromRoute] GetProductQuery query)
                => await _mediator.Send(query);

        [HttpPost]
        public async Task<ResponseDto> Post([FromForm] CreateProductCommand command)
                => await _mediator.Send(command);

        [HttpPut]
        public async Task<ResponseDto> Put([FromForm] UpdateProductCommand command)
                => await _mediator.Send(command);

        [HttpDelete("{Id}")]
        public async Task<ResponseDto> Delete([FromRoute] DeleteProductCommand query)
                => await _mediator.Send(query);
    }
}
