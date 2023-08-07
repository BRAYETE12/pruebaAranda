using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Models.TablasReferencia;
using System;

namespace Application.Common.Models
{
    public class ProfileMap : Profile
    {
        public ProfileMap()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Category, ReferenceTableDto>();

        }
    }
}
