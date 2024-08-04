using Login_Service_with_JWT_Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Login_Service_with_JWT_Auth.Services
{
    public class EFDBContext : DbContext
    {
        public EFDBContext(DbContextOptions options) : base(options) { }

        public DbSet<UserEntities> User { get; set; }
    }
}
