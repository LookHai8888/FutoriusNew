using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewFYP2.Models;

namespace NewFYP2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Event> NewEventTable { get; set; }

        public DbSet<ApplicationUser> NewUserTable { get; set; }
    }
}
