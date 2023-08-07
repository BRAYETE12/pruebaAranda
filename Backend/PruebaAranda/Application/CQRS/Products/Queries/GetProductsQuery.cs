using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public class GetProductsQuery : IRequest<ResponseDto>
    {
        public string? SortDir { get; set; }
        public string? SortProperty { get; set; }
        public int? CurrentPage { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ResponseDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.NoTrackin()
                    .OrderBy($"{request.SortProperty} {request.SortDir}")
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .GetPagedResultAsync(request.PageSize.Value, request.CurrentPage.Value);

            return new ResponseDto(true, products);

        }

    }
}
