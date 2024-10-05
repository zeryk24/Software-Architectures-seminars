using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.UserQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.Shared.Models.OrderModels;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.QueryHandlers.UserQueryHandlers;

public class GetAllUserOrdersQueryHandler : QueryHandler<GetAllUserOrdersQuery, List<OrderListModel>>, IRequestHandler<GetAllUserOrdersQuery, List<OrderListModel>>
{
    private readonly IGetAllUserOrdersQueryObject<OrderEntity> _getAllUserOrdersQueryObject;
    private readonly UserManager<UserEntity> _userManager;

    public GetAllUserOrdersQueryHandler(IMapper mapper, IGetAllUserOrdersQueryObject<OrderEntity> getAllUserOrdersQueryObject,
        UserManager<UserEntity> userManager) : base(mapper)
    {
        _getAllUserOrdersQueryObject = getAllUserOrdersQueryObject;
        _userManager = userManager;
    }

    public override async Task<List<OrderListModel>> Handle(GetAllUserOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(request.User);
        
        var orders = await _getAllUserOrdersQueryObject.UseFilter(user.Id).ExecuteAsync();
        return _mapper.Map<ICollection<OrderListModel>>(orders).ToList();
    }
}
