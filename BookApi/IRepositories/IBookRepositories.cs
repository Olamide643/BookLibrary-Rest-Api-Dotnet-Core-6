
namespace BookApi.IRepositories
{

    // Define a Interface for all tasks
    public interface IBookRepositories
    {
        // get all books 
        Task<IEnumerable<Book>> Get();

        // Get a single book 
        Task<Book> Get(int id);

        //Create a new Book Record
        Task<Book> Create(Book book);

        //Update a book
        Task Update(Book book);

        // Delete a bok
        Task Delete (int id);

    }
}
