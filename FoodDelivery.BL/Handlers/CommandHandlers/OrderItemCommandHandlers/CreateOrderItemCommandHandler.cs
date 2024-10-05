using AutoMapper;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.OrderItemsModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class CreateOrderItemCommandHandler : CommandHandler<CreateOrderItemCommand, OrderItemDetailModel>, IRequestHandler<CreateOrderItemCommand, OrderItemDetailModel>
{
    private readonly UserManager<UserEntity> _userManager;

    public CreateOrderItemCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<OrderItemDetailModel> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var orderItemEntity = _mapper.Map<OrderItemEntity>(request.OrderItemCreateModel);
        
        using var unitOfWork = _unitOfWorkProvider.Create();
        var meal = await unitOfWork.MealRepository.GetByIdAsync(request.OrderItemCreateModel.MealId);
        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.OrderItemCreateModel.OrderId);
        
        if ( order == null || meal == null)
        {
            return new OrderItemDetailModel
            {
                Id = -400,
            };
        }
        
        var user = await _userManager.GetUserAsync(request.User);
        if (order.UserId != user.Id)
        {
            return new OrderItemDetailModel
            {
                Id = -403,
            };
        }

        orderItemEntity.UnitPrice = meal.Price;
        unitOfWork.OrderItemRepository.Insert(orderItemEntity);
        await unitOfWork.Commit();

        return _mapper.Map<OrderItemDetailModel>(orderItemEntity);
    }
}
