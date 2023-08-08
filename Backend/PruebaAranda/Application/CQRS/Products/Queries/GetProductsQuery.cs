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
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public string? SortDirName { get; set; }
        public string? SortDirCategory { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
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
            var products = _repository.NoTrackin().AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
                products = products.Where(p => p.Name.ToUpper().Contains(request.Name.ToUpper()));

            if (!string.IsNullOrEmpty(request.Description))
                products = products.Where(p => p.Description.ToUpper().Contains(request.Description.ToUpper()));

            if (request.CategoryId > 0)
                products = products.Where(p => p.CategoryId == request.CategoryId);

            if (!string.IsNullOrEmpty(request.SortDirName))
                products = products.OrderBy($"Name {request.SortDirName}");

            if (!string.IsNullOrEmpty(request.SortDirCategory))
                products = products.OrderBy($"CategoryId {request.SortDirCategory}");

            var response = products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .GetPagedResultAsync(request.PageSize, request.CurrentPage);

            return await Task.FromResult(new ResponseDto(true, response.Result));

        }

    }
}
