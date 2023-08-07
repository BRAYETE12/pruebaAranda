using Application.Common.Interfaces;
using Application.Common.Services;
using Application.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<ResponseDto>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int CategoryId { get; set; }
        public IFormFile? Img { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly FileService _fileService;

        public CreateProductCommandHandler(IRepository<Product> repository, 
            FileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }


        async Task<ResponseDto> IRequestHandler<CreateProductCommand, ResponseDto>
            .Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
            };

            if (request.Img != null && request.Img.Length > 0)
                product.img = await _fileService.Save(request.Img, "Imagenes");

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return new ResponseDto(true, product);
        }

    }
}
