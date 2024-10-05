using AutoMapper;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.RestaurantModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class CreateRestaurantCommandHandler : CommandHandler<CreateRestaurantCommand, RestaurantDetailModel>, IRequestHandler<CreateRestaurantCommand, RestaurantDetailModel>
{
    public CreateRestaurantCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) {}

    public override async Task<RestaurantDetailModel> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantEntity = _mapper.Map<RestaurantEntity>(request.RestaurantCreateModel);
        restaurantEntity.Disabled = false;

        using var unitOfWork = _unitOfWorkProvider.Create();
        unitOfWork.RestaurantRepository.Insert(restaurantEntity);
        await unitOfWork.Commit();

        return _mapper.Map<RestaurantDetailModel>(restaurantEntity);
    }
}