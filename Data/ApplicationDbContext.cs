using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lab5_start.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options) {
    public DbSet<Models.Gift> Gifts { get; set; }
    public DbSet<Models.GiftRequest> GiftRequests { get; set; }
}
