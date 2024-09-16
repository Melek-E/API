using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models.Domain.Extra;
using API.Data;
namespace API.Services;
public class FrameworkService
{
    private readonly QuizzDbContext _context;

    public FrameworkService(QuizzDbContext context)
    {
        _context = context;
    }

    public async Task<List<Framework>> GetOrCreateFrameworksAsync(List<Framework> frameworks)
    {
        var result = new List<Framework>();

        foreach (var framework in frameworks)
        {
            var existingFramework = await _context.Frameworks
                .FirstOrDefaultAsync(f => f.Name.ToUpper() == framework.Name.ToUpper());

            if (existingFramework == null)
            {
                _context.Frameworks.Add(framework);
                result.Add(framework);
            }
            else
            {
                result.Add(existingFramework);
            }
        }


        return result;
    }
}
