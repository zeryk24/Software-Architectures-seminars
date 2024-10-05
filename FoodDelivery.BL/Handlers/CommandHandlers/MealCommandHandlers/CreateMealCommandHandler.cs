using AutoMapper;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;

public class CreateMealCommandHandler : CommandHandler<CreateMealCommand, MealDetailModel>, IRequestHandler<CreateMealCommand, MealDetailModel>
{
    public CreateMealCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) {}

    public override async Task<MealDetailModel> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var mealEntity = _mapper.Map<MealEntity>(request.MealCreateModel);
        using var unitOfWork = _unitOfWorkProvider.Create();
        unitOfWork.MealRepository.Insert(mealEntity);
        await unitOfWork.Commit();

        return _mapper.Map<MealDetailModel>(mealEntity);
    }
}
