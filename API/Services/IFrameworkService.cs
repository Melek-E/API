using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models.Domain.Extra;
namespace API.Services;

public interface IFrameworkService
{
    Task<List<Framework>> GetOrCreateFrameworksAsync(List<Framework> frameworks);
}