using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CerealAPI.Contexts
{
    public class UserContext(DbContextOptions<UserContext> options)
        : IdentityDbContext<IdentityUser>(options)
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;" +
                    "Initial Catalog=CerealDatabase;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30;" +
                    "Encrypt=False;" +
                    "Trust Server Certificate=False;" +
                    "Application Intent=ReadWrite;" +
                    "Multi Subnet Failover=False");
            }
        }
    }
}
