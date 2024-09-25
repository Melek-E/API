using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models.Domain.Extra;
using API.Data;
namespace API.Services;
public class FrameworkService : IFrameworkService
{
    private readonly QuizzDbContext _context;

    public FrameworkService(QuizzDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Framework>> GetOrCreateFrameworksAsync(ICollection<Framework> frameworks)
    {
        var result = new List<Framework>();

        foreach (var framework in frameworks)
        {
            var existingFramework = await _context.Frameworks
                .FirstOrDefaultAsync(f => f.Name == framework.Name);

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

        await _context.SaveChangesAsync();

        return result;
    }


    public async Task RemoveFrameworksForUserAsync(string userId)
    {
        // Retrieve the user including the Frameworks associated with them
        var user = await _context.Users
            .Include(u => u.Frameworks)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        // Clear the user's frameworks
        user.Frameworks.Clear();

        // Save the changes
        await _context.SaveChangesAsync();
    }
}

