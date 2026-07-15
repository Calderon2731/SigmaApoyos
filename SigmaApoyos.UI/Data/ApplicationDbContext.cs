using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace SigmaApoyos.UI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
    : SigmaApoyos.Infrastructure.Persistence.ApplicationDbContext(options, httpContextAccessor)
{
}
