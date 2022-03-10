
namespace BookApi.Data
{

    // Entends  the BookContext Class with DbContext from Entity Framework class
    public class BookContext : DbContext 
    {
        // Configuration of the BookContext from the dependencies injection 
        public BookContext(DbContextOptions<BookContext> options) : base(options){ }

        // Dbset ==> Table  Entity ==> Records/Rows in the table
        public DbSet<Book> Books { get; set; }  

    }
}
