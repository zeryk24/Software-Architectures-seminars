using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.RestaurantQueries;
using FoodDelivery.BL.Queries.UserQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.RestaurantModels;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.QueryHandlers.UserQueryHandlers;

public class GetAllUsersQueryHandler : QueryHandler<GetAllUsersQuery, List<UserListModel>>, IRequestHandler<GetAllUsersQuery, List<UserListModel>>
{
    private readonly IQueryObject<UserEntity> _userQueryObject;
    private readonly UserManager<UserEntity> _userManager;

    public GetAllUsersQueryHandler(IMapper mapper, IQueryObject<UserEntity> userQueryObject,
        UserManager<UserEntity> userManager) : base(mapper)
    {
        _userQueryObject = userQueryObject;
        _userManager = userManager;
    }

    public override async Task<List<UserListModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        if (request.Page > 0 && request.PageSize > 0)
            _userQueryObject.Page(request.Page, request.PageSize);
        
        var users = await _userQueryObject.ExecuteAsync();
        var result = new List<UserListModel>();
        foreach (var user in users)
        {
            var userListModel = _mapper.Map<UserListModel>(user);
            userListModel.Role = (await _userManager.GetRolesAsync(user)).Single();
            result.Add(userListModel);
        }

        return result;
    }
}

