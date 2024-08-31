using Microsoft.EntityFrameworkCore;
using NewWebApi.DB.Models;

namespace NewWebApi.DB
{
    public class NewDB:DbContext
    {
        public NewDB(DbContextOptions<NewDB> options):base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
