using AutoMapper;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.OrderItemsModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class UpdateOrderItemCommandHandler : CommandHandler<UpdateOrderItemCommand, OrderItemDetailModel>, IRequestHandler<UpdateOrderItemCommand, OrderItemDetailModel>
{
    private readonly UserManager<UserEntity> _userManager;

    public UpdateOrderItemCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<OrderItemDetailModel> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(request.User);
        
        using var unitOfWork = _unitOfWorkProvider.Create();
        
        var orderItemEntity = await unitOfWork.OrderItemRepository.GetByIdAsync(request.OrderItemUpdateModel.Id);

        if (orderItemEntity == null)
        {
            return new OrderItemDetailModel
            {
                Id = -400,
            };
        }
        
        var orderEntity = await unitOfWork.OrderRepository.GetByIdAsync(orderItemEntity.OrderId);
        
        if (orderEntity.UserId != user.Id)
        {
            return new OrderItemDetailModel
            {
                Id = -403,
            };
        }
        
        orderItemEntity.Amount = request.OrderItemUpdateModel.Amount;
        unitOfWork.OrderItemRepository.Update(orderItemEntity);
        await unitOfWork.Commit();

        return _mapper.Map<OrderItemDetailModel>(orderItemEntity);
    }
}
