using AutoMapper;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.Repositories.Interfaces.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces.Base;
using FoodDelivery.Shared.Models.FeedbacksModels;

namespace FoodDelivery.BL.Handlers.CommandHandlers.Base;

public abstract class CommandHandler<TInputCommand, TReturnModel>
{
    protected readonly IUnitOfWorkProvider<IEFCoreUnitOfWork> _unitOfWorkProvider;
    protected readonly IMapper _mapper;

    public CommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _mapper = mapper;
    }

    public abstract Task<TReturnModel> Handle(TInputCommand request, CancellationToken cancellationToken);
}
