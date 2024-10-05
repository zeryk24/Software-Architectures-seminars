using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;

public interface IUnitOfWorkProvider<TUnitOfWork> where TUnitOfWork : IUnitOfWork
{
	public TUnitOfWork Create();

}
