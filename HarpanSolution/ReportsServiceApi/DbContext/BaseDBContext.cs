using Microsoft.EntityFrameworkCore;
using ReportsServiceApi.Domain.Entities;

namespace ReportsServiceApi.DbContext
{
    public class BaseDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BaseDBContext(DbContextOptions<BaseDBContext> options) : base(options)
        {
        }

        public DbSet<Reports> Reports { get; set; }
    }
}
