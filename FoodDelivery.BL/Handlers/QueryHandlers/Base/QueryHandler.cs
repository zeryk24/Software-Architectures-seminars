using AutoMapper;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;

namespace FoodDelivery.BL.Handlers.QueryHandlers.Base;

public abstract class QueryHandler<TInputCommand, TResponse>
{
    protected readonly IMapper _mapper;

    public QueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public abstract Task<TResponse> Handle(TInputCommand request, CancellationToken cancellationToken);
}
