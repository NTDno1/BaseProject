﻿using AutoMapper;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Entities.Identity;

namespace DemoCICD.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Product, Response.ProductResponse>().ReverseMap();
    }
}
