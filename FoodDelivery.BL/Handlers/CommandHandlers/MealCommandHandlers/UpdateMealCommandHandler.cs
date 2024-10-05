using AutoMapper;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;

public class UpdateMealCommandHandler : CommandHandler<UpdateMealCommand, MealDetailModel>, IRequestHandler<UpdateMealCommand, MealDetailModel>
{
    public UpdateMealCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<MealDetailModel> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        var mealEntity = (await unitOfWork.MealRepository.GetByIdAsync(request.MealUpdateModel.Id));
        
        mealEntity.Name = request.MealUpdateModel.Name;
        mealEntity.Description = request.MealUpdateModel.Description;
        mealEntity.Price = request.MealUpdateModel.Price;
        mealEntity.MealType = request.MealUpdateModel.MealType;
        
        unitOfWork.MealRepository.Update(mealEntity);
        await unitOfWork.Commit();

        return _mapper.Map<MealDetailModel>(mealEntity);
    }
}
