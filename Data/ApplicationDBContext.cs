using dot_Net_web_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace dot_Net_web_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Registermodel> Registrations { get; set; }
        public DbSet<Registermodel> Logins { get; set; }
        public DbSet<ProfileDetailsModel> ProfileDetails { get; set; }

    }
}
