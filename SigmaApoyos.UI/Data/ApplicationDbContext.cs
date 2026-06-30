using Microsoft.EntityFrameworkCore;

namespace SigmaApoyos.UI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : SigmaApoyos.Infrastructure.Persistence.ApplicationDbContext(options)
{
}
