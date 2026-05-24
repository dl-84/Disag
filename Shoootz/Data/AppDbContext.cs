using Microsoft.EntityFrameworkCore;

namespace Shoootz.Data;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) { }
