using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities
{
    public class HousingBillPaymentContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public HousingBillPaymentContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source = DESKTOP-T7UAG83\\SQLEXPRESS; Database =HousingBillPaymentDB ; integrated security = True;");
        }
        public DbSet<Apartment> Apartment { get; set; }
        public DbSet<Debt> Debt { get; set; }
        public DbSet<DebtType> DebtType { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<User> User { get; set; }

    }
}
