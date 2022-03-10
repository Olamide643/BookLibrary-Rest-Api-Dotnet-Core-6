 global using BookApi.IRepositories;
global using BookApi.Model;

namespace BookApi.Repositories
{

    // extend the Book Repositories class to include the Interface 
    public class BookRepositories : IBookRepositories
    {

        // Declare an instance of the BookContext
        public readonly BookContext _context;

        //Construct of the class [ Also assign value to _context]
        public BookRepositories(BookContext context)
        {
        _context = context;
        }

        // Implemnetation of IbookRepositories Create method
        async Task<Book> IBookRepositories.Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book; 
        }

        // Implemenetation of IbookRepositories Delete method
        async Task IBookRepositories.Delete(int id)
        {
            var book_to_delete = await _context.Books.FindAsync(id);
            if (book_to_delete != null)
                _context.Remove(book_to_delete);
            await _context.SaveChangesAsync();
        }

        // Implemenetation of IbookRepositories Get all method
        async Task<IEnumerable<Book>> IBookRepositories.Get()
        {
            return await _context.Books.ToListAsync();

        }

        // Implemenetation of IbookRepositories Get   method
        async Task<Book> IBookRepositories.Get(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        // Implemenetation of IbookRepositories Update method
        Task IBookRepositories.Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
