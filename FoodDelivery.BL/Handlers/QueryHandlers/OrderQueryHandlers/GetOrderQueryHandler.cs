using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.OrderQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.QueryHandlers.OrderQueryHandlers;

public class GetOrderQueryHandler : QueryHandler<GetOrderQuery, OrderDetailModel>, IRequestHandler<GetOrderQuery, OrderDetailModel>
{
    protected readonly IUnitOfWorkProvider<IEFCoreUnitOfWork> _unitOfWorkProvider;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IGetOrderItemsByOrderIdQueryObject<OrderItemEntity> _orderItemsByOrderIdQueryObject;
    protected readonly IMapper _mapper;

    public GetOrderQueryHandler(IMapper mapper, IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider,
        UserManager<UserEntity> userManager, IGetOrderItemsByOrderIdQueryObject<OrderItemEntity> orderItemsByOrderIdQueryObject) : base(mapper)
    {
        _mapper = mapper;
        _unitOfWorkProvider = unitOfWorkProvider;
        _userManager = userManager;
        _orderItemsByOrderIdQueryObject = orderItemsByOrderIdQueryObject;
    }

    public override async Task<OrderDetailModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
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
        
        orderEntity.Address = await unitOfWork.AddressRepository.GetByIdAsync(orderEntity.AddressId);
        var orderItems = await _orderItemsByOrderIdQueryObject.UseFilter(request.OrderId).ExecuteAsync();
        orderEntity.OrderItems = orderItems.ToList();
        

        var result = _mapper.Map<OrderDetailModel>(orderEntity);
        result.RestaurantName = (await unitOfWork.RestaurantRepository.GetByIdAsync(result.RestaurantId)).Name;

        if (await _userManager.IsInRoleAsync(user, Constants.Roles.Customer) && 
            orderEntity.UserId != user.Id)
        {
            return new OrderDetailModel
            {
                Id = -403,
            };
        }

        return result;
    }
}