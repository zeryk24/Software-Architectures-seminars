using Inventory.Domain.Common;
using Inventory.Domain.Common.Enums;

namespace Inventory.Domain.Order.ValueObjects;

public class OrderAddress : ValueObject
{
    public State State { get; private set; }
    public string City { get; private set; }
    public string Code { get; private set; }
    public string StreetAndNumber { get; private set; }
    
    private OrderAddress(State state, string city, string code, string streetAndNumber)
    {
        State = state;
        City = city;
        Code = code;
        StreetAndNumber = streetAndNumber;
    }

    public static OrderAddress Create(State state, string city, string code, string streetAndNumber) =>
        new OrderAddress(state, city, code, streetAndNumber);
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return State;
        yield return City;
        yield return Code;
        yield return StreetAndNumber;
    }
}