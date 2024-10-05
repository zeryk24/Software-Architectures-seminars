using FoodDelivery.DAL.Entities.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DAL.Entities.Base;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
}
