using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int CategoryId { get; set; }
        public IFormFile? Img { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly FileService _fileService;

        public UpdateProductCommandHandler(IRepository<Product> repository,
            FileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }


        async Task<ResponseDto> IRequestHandler<UpdateProductCommand, ResponseDto>
            .Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _repository.GetByIdAsync(request.Id);

            if (product == null)
                throw new NotFoundException(nameof(Product), request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId;

            if (request.Img != null && request.Img.Length > 0)
                product.img = await _fileService.Save(request.Img, "Archivos/Imagenes");

            await _repository.SaveChangesAsync();

            return new ResponseDto(true, product);
        }

    }
}
