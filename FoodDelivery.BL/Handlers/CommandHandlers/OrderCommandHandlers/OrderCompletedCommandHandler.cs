using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;

public class OrderCompletedCommandHandler : CommandHandler<OrderCompletedCommand, OrderDetailModel>, IRequestHandler<OrderCompletedCommand, OrderDetailModel>
{
    private readonly UserManager<UserEntity> _userManager;

    public OrderCompletedCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
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

        return _mapper.Map<OrderDetailModel>(order);
    }
}