
using Infrastructure.Interfaces;

namespace Infrastructure.Helpers;

public class UniqueIdGenerator : IUniqueIdGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
