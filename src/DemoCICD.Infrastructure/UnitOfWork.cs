﻿using DemoCICD.Domain.Abstractions.Dappers;
using DemoCICD.Domain.Abstractions.Dappers.Repositories.Product;

namespace DemoCICD.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IProductRepository productRepository)
    {
        Products = productRepository;
    }

    public IProductRepository Products { get; }
}
