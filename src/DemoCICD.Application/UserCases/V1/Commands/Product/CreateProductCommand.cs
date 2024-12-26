using DemoCICD.Contract.Share;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public record CreateProductCommand(string Name) : ICommand;
