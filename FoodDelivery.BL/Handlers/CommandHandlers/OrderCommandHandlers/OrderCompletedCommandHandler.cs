using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.Contracts;
using FoodDelivery.Contracts.Utils;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Wolverine;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;

public class OrderCompletedCommandHandler : CommandHandler<OrderCompletedCommand, OrderDetailModel>, IRequestHandler<OrderCompletedCommand, OrderDetailModel>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMessageBus _sender;
    private readonly IGetOrderItemsByOrderIdQueryObject<OrderItemEntity> _orderItemsByOrderIdQueryObject;

    public OrderCompletedCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager, IMessageBus sender, IGetOrderItemsByOrderIdQueryObject<OrderItemEntity> orderItemsByOrderIdQueryObject) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
        _sender = sender;
        _orderItemsByOrderIdQueryObject = orderItemsByOrderIdQueryObject;
    }

    public override async Task<OrderDetailModel> Handle(OrderCompletedCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(request.User) ?? throw new ValidationException("User not found");
        
        using var unitOfWork = _unitOfWorkProvider.Create();
        
        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.OrderId) ?? throw new ValidationException("Order not found");;

        if (order.UserId != user.Id)
            throw new ValidationException("This user is not the owner of that order");

        order.Completed = true;

        unitOfWork.OrderRepository.Update(order);
        await unitOfWork.Commit();

        var orderItems = await _orderItemsByOrderIdQueryObject.UseFilter(request.OrderId).ExecuteAsync();
            
        var address = await unitOfWork.AddressRepository.GetByIdAsync(order.AddressId);
        
        var completedOrder = new OrderCompletedMessage.Order(
            IdConverter.IntToGuid(order.Id),
            orderItems.Select(oi => new OrderCompletedMessage.OrderItem(
                IdConverter.IntToGuid((int)oi.MealId),
                oi.Amount,
                oi.UnitPrice)
            ), new OrderCompletedMessage.Address(address.City, address.PostalCode, address.Street + address.Number)
        );
        
        var message = new OrderCompletedMessage(completedOrder);
        await _sender.PublishAsync(message);
        
        return _mapper.Map<OrderDetailModel>(order);
    }
}