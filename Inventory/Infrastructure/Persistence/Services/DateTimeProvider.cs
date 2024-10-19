using Inventory.Features.Services;
using RegistR.Attributes.Base;

namespace Inventory.Infrastructure.Persistence.Services;

[Register<IDateTimeProvider>]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;

    public DateTime UtcNow => DateTime.UtcNow;
}