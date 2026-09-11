namespace WebApi.Constants;

public static class Permissions
{
    public const string UsersRead = "users.read";
    public const string UsersCreate = "users.create";
    public const string UsersUpdate = "users.update";
    public const string UsersDelete = "users.delete";

    public const string ProductsRead = "products.read";
    public const string ProductsCreate = "products.create";
    public const string ProductsUpdate = "products.update";
    public const string ProductsDelete = "products.delete";
    public static readonly string[] AllPermissions = new[]
    {
        UsersRead,
        UsersCreate,
        UsersUpdate,
        UsersDelete,
        ProductsRead,
        ProductsCreate,
        ProductsUpdate,
        ProductsDelete
    };
}