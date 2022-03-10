using BookApi.Models;
using Microsoft.EntityFrameworkCore;
namespace BookApi.Data
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<RegisterModel> Users { set; get; }
    }

}

    
