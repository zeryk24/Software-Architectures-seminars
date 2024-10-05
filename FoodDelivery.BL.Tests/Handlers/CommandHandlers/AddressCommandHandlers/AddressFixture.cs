using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.AddressModels;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.AddressCommandHandlers;

public class AddressFixture
{
    public AddressEntity AddressEntity { get; set; }
    public AddressCreateModel AddressCreateModel { get; set; }
    public AddressDetailModel AddressDetailModel { get; set; }
    public AddressUpdateModel AddressUpdateModel { get; set; }
    
    public AddressFixture()
    {
        AddressEntity = new AddressEntity
        { 
            Id = 1,
            Street = "Elk Avenue",
            Number = "3302",
            City = "Webberville",
            PostalCode = "48892"
        };

        AddressCreateModel = new AddressCreateModel
        {
            Street = "Elk Avenue",
            Number = "3302",
            City = "Webberville",
            PostalCode = "48892"
        };
        
        AddressDetailModel = new AddressDetailModel
        {
            Id = 1,
            Street = "Elk Avenue",
            Number = "3302",
            City = "Webberville",
            PostalCode = "48892"
        };

        AddressUpdateModel = new AddressUpdateModel
        {
            Street = "Elk Avenue",
            Number = "3302",
            City = "Webberville",
            PostalCode = "48892"
        };
    }
}