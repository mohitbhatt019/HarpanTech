using DataContracts.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataContracts.DbContext
{
    public class BaseDBContext : IdentityDbContext<AppUser>
    {
        public BaseDBContext(DbContextOptions<BaseDBContext> options) : base(options)
        {

        }

        public DbSet<Reports> reports { get; set; }
        public DbSet<ReportsWorker> reportsWorkers { get; set; }
        public DbSet<Tracking> trackings { get; set; }

    }
}
