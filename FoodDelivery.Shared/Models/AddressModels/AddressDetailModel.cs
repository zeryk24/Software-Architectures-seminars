using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.Shared.Models.AddressModels;

public class AddressDetailModel
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
}
