using e_commerce_pro.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_pro.Data
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
        {

        }
        public DbSet<UserSingup> Usersingup { get; set; }

        public DbSet<Otp> OTPinfo { get; set; }

        public DbSet<CategorieS> categories { get; set; }

        public DbSet<Products> products { get; set; }

    }
}
