using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces.Base;

public interface IUnitOfWork : IDisposable
{
    Task Commit();
}
