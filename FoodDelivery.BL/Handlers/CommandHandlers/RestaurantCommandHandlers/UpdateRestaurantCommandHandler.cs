using AutoMapper;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.RestaurantModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class UpdateRestaurantCommandHandler : CommandHandler<UpdateRestaurantCommand, RestaurantDetailModel>, IRequestHandler<UpdateRestaurantCommand, RestaurantDetailModel>
{
    public UpdateRestaurantCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) {}

    public override async Task<RestaurantDetailModel> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        var restaurantEntity = await unitOfWork.RestaurantRepository.GetByIdAsync(request.RestaurantUpdateModel.Id);
        if (restaurantEntity == null)
        {
            return new RestaurantDetailModel
            {
                Id = -400,
            };
        }
        restaurantEntity.Name = request.RestaurantUpdateModel.Name;
        unitOfWork.RestaurantRepository.Update(restaurantEntity);
        await unitOfWork.Commit();

        return _mapper.Map<RestaurantDetailModel>(restaurantEntity);
    }
}
