using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models.TablasReferencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ReferenceTable.Queries
{
    public class GetCategoryQuery : IRequest<ResponseDto>{}

    public class GetCategorysQueryHandler : IRequestHandler<GetCategoryQuery, ResponseDto>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategorysQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.NoTrackin()
                    .ProjectTo<ReferenceTableDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            return new ResponseDto(true, list);

        }

    }

}
