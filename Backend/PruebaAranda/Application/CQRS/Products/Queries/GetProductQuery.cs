using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public class GetProductQuery : IRequest<ResponseDto>
    {
        public int ProductId { get; set; }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ResponseDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var productFind = await _repository.GetByIdAsync(request.ProductId);

            if (productFind == null)
                throw new NotFoundException(nameof(Product), request.ProductId);

            var product = _mapper.Map<ProductDto>(productFind);

            return new ResponseDto(true, product);
        }
    }
}
