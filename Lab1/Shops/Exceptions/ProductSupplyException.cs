namespace Shops.Exceptions;

public class ProductSupplyException : ShopsException
{
    private ProductSupplyException(string message)
        : base(message)
    {
    }

    public static ProductSupplyException MismatchInProductsKvpException()
    {
        return new ProductSupplyException("Some product provided as a key was not part of the supply item");
    }

    public static ProductSupplyException NewPriceIsNotSetException()
    {
        return new ProductSupplyException("Tried to access new price that was not set");
    }

    public static ProductSupplyException ProductAlreadyInSupply()
    {
        return new ProductSupplyException("Product is already added to supply");
    }
}