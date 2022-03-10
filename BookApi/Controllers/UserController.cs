using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:  ControllerBase
    {
        public readonly IUserRepositories _IuserRepositories;
        public readonly IConfiguration _configuration;  

        public UserController(IUserRepositories userRepositories, IConfiguration configuration)
        {
            _IuserRepositories = userRepositories;
            _configuration = configuration;
        }

        


        //Register Route 
        [HttpPost("register")]
        public async Task<ActionResult<RegisterModel>> NewUser(UserSignUp request)
        {
            var userExist = await _IuserRepositories.GetUserbyEmail(request.email);
            if (userExist != null)
            {
                return BadRequest("User name already exist");
            }
            var user = new RegisterModel();
            user.username = request.username;
            user.firstname = request.firstname; 
            user.lastname = request.lastname;
            user.email = request.email;
            user.datecreated = request.datecreated;
            //user.role = (RegisterModel.Role)request.role;

            // hashing the password 
            Hashpassword(request.password, out byte[] passwordhash, out byte[] passwordsalt );
            user.passwordsalt = passwordsalt;
            user.hashedpassword = passwordhash;


          var new_user =  await _IuserRepositories.CreateUser(user);
          
            //return CreatedAtAction(nameof(GetById), new { id = new_user.UserId }, new_user);
            return Ok(new_user.UserId);
        }


        [HttpPost("auth/login")]
        public  async Task<ActionResult<string>> Login( LoginModel request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            var user = _IuserRepositories.GetUserbyEmail(request.Email);
            if ( user == null)
            {
                return NotFound("User does not exist ");
            }

            if (!VerifyPassword(request.Password, user.Result.hashedpassword, user.Result.passwordsalt))
            {
                return BadRequest("Incorrect Password");
            }
            string token = CreateToken(user);
            
            return  Ok(token);
        }

        

        
        [HttpGet("auth/getuser/{id}")]
        [Authorize(Roles = "Administrator")]
        [Authorize(Roles = "Superuser")]
        public async Task<ActionResult<RegisterModel>> GetById(int id)
        {

            return await _IuserRepositories.GetUser(id);
        }
        




        [HttpDelete("auth/delete/{id}")]
        [Authorize(Roles = "Superuser")]
        public async Task<ActionResult<RegisterModel>> DeleteUser(int id)
        {
            var user_to_delete = _IuserRepositories.GetUser(id);
            if (user_to_delete == null)
            {
                return NotFound("Invalid user id");
            }

            await _IuserRepositories.DeleteUser(id);
            return NotFound(); 

        }

        [HttpPut("auth/update/{id}")]
        [Authorize(Roles = "Administrator")]
        [Authorize(Roles = "Superuser")]
        public async Task<ActionResult> UpdateUser(int id, RegisterModel user )
        {
            if (user == null || user.UserId != id)
            {
                return BadRequest();
            }

            var user_to_update = await _IuserRepositories.GetUser(id);


            if (user_to_update == null)
            {
                return BadRequest($"Invalid User Id");
            }

            user_to_update.firstname = user.firstname;
            user_to_update.lastname = user.lastname;
            user_to_update.email = user.email;
            user_to_update.role = user.role;

            await _IuserRepositories.UpdateUser(user_to_update);
            return NoContent();

        }

        private void Hashpassword(string password, out byte[] hashedpassword, out byte[] passwordsalt)
        {
            using (var hmac_algo = new HMACSHA512())
            {
                hashedpassword = hmac_algo.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordsalt = hmac_algo.Key;
            }

        }

        private bool VerifyPassword( string password, byte[] hashpassword, byte[] passwordsalt)
        {
            using ( var hmac_algo = new HMACSHA512(passwordsalt))
            {
                var computeHash = hmac_algo.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(hashpassword);
            }


        }

        private string CreateToken(Task<RegisterModel> user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Result.username),
                new Claim(ClaimTypes.Email, user.Result.email),
                new Claim(ClaimTypes.Role, "Superuser"),
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim(ClaimTypes.Role, "user")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims : claims,
                expires : DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt  = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }





    }
}
