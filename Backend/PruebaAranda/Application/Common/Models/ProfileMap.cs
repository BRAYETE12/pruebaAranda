using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Models.TablasReferencia;
namespace Application.Common.Models
{
    public class ProfileMap : Profile
    {
        public ProfileMap()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category, ReferenceTableDto>();
        }
    }
}
