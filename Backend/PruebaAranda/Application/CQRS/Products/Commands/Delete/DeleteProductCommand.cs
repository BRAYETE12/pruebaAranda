using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseDto>
    {
        private readonly IRepository<Product> _repository;

        public DeleteProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }


        async Task<ResponseDto> IRequestHandler<DeleteProductCommand, ResponseDto>
            .Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _repository.GetByIdAsync(request.Id);

            if (product == null)
                throw new NotFoundException(nameof(Product), request.Id);

            await _repository.DeleteAsync(product);
            await _repository.SaveChangesAsync();

            return new ResponseDto(true, product.Id);
        }

    }
}
