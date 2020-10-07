using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Payment.Data
{
    public class PaymentDbContext : DbContext
    {
        private IConfiguration _configuration;
        
        public PaymentDbContext(IConfiguration configuration)
        {
            _configuration = configuration;           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }

    }
}
