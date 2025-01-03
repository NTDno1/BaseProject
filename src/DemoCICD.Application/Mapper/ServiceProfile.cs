﻿using AutoMapper;
using DemoCICD.Contract.Abstractions.Share;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Entities.Identity;

namespace DemoCICD.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // V1
        CreateMap<Product, Response.ProductResponse>().ReverseMap();
        CreateMap<PagedResult<Product>, PagedResult<Response.ProductResponse>>().ReverseMap();

        // V2
        //CreateMap<Product, Contract.Services.V2.Product.Response.ProductResponse>().ReverseMap();
    }
}
