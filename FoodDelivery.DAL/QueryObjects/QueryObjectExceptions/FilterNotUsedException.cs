namespace FoodDelivery.DAL.EFCore.QueryObjects.QueryObjectExceptions;

internal class FilterNotUsedException : Exception
{
    public FilterNotUsedException() { }
    public FilterNotUsedException(string message) : base(message) { }
    public FilterNotUsedException(string message, Exception innerException) : base(message, innerException) { }
}
