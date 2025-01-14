using Asp.Versioning.Builder;
using DemoCICD.Contract.Extensions;
using DemoCICD.Contract.Share;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommandV1 = DemoCICD.Contract.Services.V1.Product.Command;
using CommandV2 = DemoCICD.Contract.Services.V2.Product.Command;
using QueryV1 = DemoCICD.Contract.Services.V1.Product.Query;
using QueryV2 = DemoCICD.Contract.Services.V2.Product.Query;


namespace DemoCICD.Presentation.APIs;
public static class ProductApi
{
    private const string BaseUrl = "/api/minimal/v{version:apiVersion}/products";

    public static IVersionedEndpointRouteBuilder MapProductApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost(string.Empty, CreateProducts);
        group.MapGet(string.Empty, GetProducts);
        group.MapGet("{productId}", GetProductsById);
        group.MapDelete("{productId}", DeleteProducts);
        group.MapPut("{productId}", UpdateProducts);
        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapProductApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapPost(string.Empty, CreateProductV2);
        group.MapGet(string.Empty, GetProductsV2);
        return builder;
    }

    #region ====== version 1 ======
    public static async Task<IResult> GetProducts(
    ISender Sender,
    string? serchTerm = null,
    string? sortColumn = null,
    string? sortOrder = null,
    string? sortColumnAndOrder = null,
    int pageIndex = 1,
    int pageSize = 10)
    {
        var result = await Sender.Send(new QueryV1.GetProductsQuery(
            serchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            SortOrderExtension.ConvertStringToSortOrderV2(sortColumnAndOrder),
            pageIndex,
            pageSize));
        return Results.Ok(result);
    }

    public static async Task<IResult> GetProductsById(ISender Sender, Guid productId)
    {
        var result = await Sender.Send(new QueryV1.GetProductById(productId));
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateProducts(ISender Sender, [FromBody] CommandV1.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);

        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateProducts(ISender Sender, Guid productId, [FromBody] CommandV1.CreateProductCommand updateProduct)
    {
        var updateProductCommand = new CommandV1.UpdateProductCommand(productId, updateProduct.Name, updateProduct.Price, updateProduct.Description);
        var result = await Sender.Send(updateProductCommand);
        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteProducts(ISender Sender, Guid productId)
    {
        var result = await Sender.Send(new CommandV1.DeleteProductCommand(productId));
        return Results.Ok(result);
    }
    #endregion

    #region ====== version 2 ======
    public static async Task<IResult> GetProductsV2(ISender Sender)
    {
        var result = await Sender.Send(new QueryV2.GetProductsQueryDapper());
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateProductV2(ISender Sender, [FromBody] CommandV2.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);

        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Results.Ok(result);
    }
    #endregion

    private static IResult HandlerFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                Results.BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),
            _ =>
                Results.BadRequest(
                    CreateProblemDetails(
                        "Bab Request", StatusCodes.Status400BadRequest,
                        result.Error))
        };
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null)
    {
        return new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
    }
}
