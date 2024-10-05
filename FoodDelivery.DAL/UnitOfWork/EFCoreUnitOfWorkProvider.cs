using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.DAL.EFCore.Database;

namespace FoodDelivery.DAL.EFCore.UnitOfWork;

public class EFCoreUnitOfWorkProvider : IUnitOfWorkProvider<IEFCoreUnitOfWork>
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public EFCoreUnitOfWorkProvider(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IEFCoreUnitOfWork Create()
    {
        return new EFCoreUnitOfWork(_contextFactory());
    }
}
