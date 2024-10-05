using AutoMapper;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;

public class ChangeOrderPaymentTypeCommandHandler : CommandHandler<ChangeOrderPaymentTypeCommand, OrderDetailModel>, IRequestHandler<ChangeOrderPaymentTypeCommand, OrderDetailModel>
{
    private readonly UserManager<UserEntity> _userManager;

    public ChangeOrderPaymentTypeCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<OrderDetailModel> Handle(ChangeOrderPaymentTypeCommand request, CancellationToken cancellationToken)
    { 
        var user = await _userManager.GetUserAsync(request.User);
        
        using var unitOfWork = _unitOfWorkProvider.Create();
        var orderEntity = await unitOfWork.OrderRepository.GetByIdAsync(request.OrderId);
        if (orderEntity == null)
        {
            return new OrderDetailModel
            {
                Id = -400,
            };
        }
        
        if (orderEntity.UserId != user.Id)
        {
            return new OrderDetailModel
            {
                Id = -403,
            };
        }
        
        orderEntity.PaymentType = request.PaymentType;
        unitOfWork.OrderRepository.Update(orderEntity);
        await unitOfWork.Commit();

        return _mapper.Map<OrderDetailModel>(orderEntity);
    }
}