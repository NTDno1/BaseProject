namespace DemoCICD.Contract.Extensions;
public static class ProductExtension
{
    public static string GetSortProductProperty(string sortColumn)
    {
        return sortColumn.ToLower() switch
        {
            "name" => "Name",
            "price" => "Price",
            "description" => "Description",
            _ => "Id"
        };
    }
}

