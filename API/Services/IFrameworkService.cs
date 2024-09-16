using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models.Domain.Extra;
namespace API.Services;

public interface IFrameworkService
{
    Task<ICollection<Framework>> GetOrCreateFrameworksAsync(ICollection<Framework> frameworks);
}

    