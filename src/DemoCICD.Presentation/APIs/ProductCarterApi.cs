using Carter;
using DemoCICD.Contract.Extensions;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using CommandV1 = DemoCICD.Contract.Services.V1.Product.Command;
using CommandV2 = DemoCICD.Contract.Services.V2.Product.Command;
using QueryV1 = DemoCICD.Contract.Services.V1.Product.Query;
using QueryV2 = DemoCICD.Contract.Services.V2.Product.Query;

namespace DemoCICD.Presentation.APIs;
public class ProductCarterApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/carter/v{version:apiVersion}/products";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group1 = app.NewVersionedApi("products-cater-name-show-on-swagger").MapGroup(BaseUrl).HasApiVersion(1);
        group1.MapPost(string.Empty, CreateProducts);
        group1.MapGet(string.Empty, GetProducts);
        group1.MapGet("{productId}", GetProductsById);
        group1.MapDelete("{productId}", DeleteProducts);
        group1.MapPut("{productId}", UpdateProducts);

        var group2 = app.NewVersionedApi("products-cater-name-show-on-swagger").MapGroup(BaseUrl).HasApiVersion(2);
        group2.MapPost(string.Empty, CreateProductV2);
        group2.MapGet(string.Empty, GetProductsV2);
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
}
