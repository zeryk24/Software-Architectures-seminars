using AutoMapper;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.BL.Queries.RestaurantQueries;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class RemoveRestaurantCommandHandler : CommandHandler<RemoveRestaurantCommand, bool>, IRequestHandler<RemoveRestaurantCommand, bool>
{
    public RemoveRestaurantCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) {}

    public override async Task<bool> Handle(RemoveRestaurantCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        var restaurantEntity = await unitOfWork.RestaurantRepository.GetByIdAsync(request.Id);
        if (restaurantEntity == null)
        {
            return false;
        }

        restaurantEntity.Disabled = true;
        unitOfWork.RestaurantRepository.Update(restaurantEntity);
        await unitOfWork.Commit();
        return true;
    }
}
