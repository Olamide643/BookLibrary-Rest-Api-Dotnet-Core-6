global using BookApi.Models;

namespace BookApi.IRepositories
{
    public interface IUserRepositories
    {
        public Task<RegisterModel> GetUser(int userid);

        public Task< RegisterModel> GetUserbyEmail(string email);

        public Task<RegisterModel> CreateUser(RegisterModel user);

        public Task UpdateUser(RegisterModel user);

        public Task DeleteUser(int  userid);



         
    }
}
