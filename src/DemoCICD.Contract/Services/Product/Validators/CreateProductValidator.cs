﻿using FluentValidation;

namespace DemoCICD.Contract.Services.Product.Validators;
public class CreateProductValidator : AbstractValidator<Command.CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty();
    }
}
