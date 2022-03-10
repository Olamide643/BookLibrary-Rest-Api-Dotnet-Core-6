using BookApi.IRepositories;
using System.Security.Cryptography;

namespace BookApi.Repositories
{
    public class UserRepositories : IUserRepositories
    {

        public readonly UserContext _usercontext;

        public UserRepositories(UserContext usercontext)
        {
            _usercontext = usercontext;
        }

        public async Task<RegisterModel> CreateUser(RegisterModel user)
        {
            _ = _usercontext.AddAsync(user);
            await _usercontext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int userid)
        {
            var user_to_delete = _usercontext.Users.FindAsync(userid);
            if (user_to_delete != null)
               _usercontext.Remove(user_to_delete);
            await _usercontext.SaveChangesAsync();
          
        }

        public async Task<RegisterModel> GetUser(int userid)
        {
            return await _usercontext.Users.FindAsync(userid);
        }

        public  Task<RegisterModel> GetUserbyEmail(string email)
        {

            return  _usercontext.Users.FirstOrDefaultAsync(e => e.email == email);
        }

        public  Task UpdateUser(RegisterModel user)
        {
            _usercontext.Entry(user).State = EntityState.Modified;
            return  _usercontext.SaveChangesAsync();
        }
    }

  

  
}

