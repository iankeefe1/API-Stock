using API_Portfolio.Data;
using API_Portfolio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt;
using BCrypt.Net;
using API_Portfolio.Model;

namespace API_Portfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)  // ✅ inject
        {
            _context = context;
        }

        #region GET

        [HttpGet("GetUserByUsersUsernameAndPassword")]
        public string GetUserByUsersUsernameAndPassword(string username, string password)
        {
            if (_context == null)
                return "_context is NULL";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return "Username or Password cannot be empty";
            else
            {
                var user = _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

                if (user == null)
                {
                    return "Login Failed";
                }
                else
                {
                    bool valid = BCrypt.Net.BCrypt.Verify(password, user.Passwords);
                    if (!valid)
                    {
                        return "Login Failed";
                    }
                    else
                    {
                        return "Login Success";
                    }

                }
            }
        }
        #endregion

        #region POST

        [HttpPost("CreateUser")]
        public string CreateUser(User usercreate)
        {

            string hash = BCrypt.Net.BCrypt.HashPassword(usercreate.Passwords);

            var user = _context.Users
            .Where(u => u.Username == usercreate.Username && u.Passwords == hash)
            .FirstOrDefault();

            //if (user != null)
            //{
            //    return "Login Success";
            //}
            if (user == null)
            {
                Users users = new Users();
                users.Username = usercreate.Username;
                users.Passwords = hash;

                if (usercreate.Email != null)
                {
                    users.Email = usercreate.Email;
                }
                if (usercreate.FirstName != null)
                {
                    users.FirstName = usercreate.FirstName;
                }
                if (usercreate.Lastname != null)
                {
                    users.Lastname = usercreate.Lastname;
                }
                if (usercreate.Telphone != null)
                {
                    users.Telphone = usercreate.Telphone;
                }
                _context.Add(users);
                _context.SaveChanges();

                return "Create Users Complete";
            }
            else
            {
                return "Users Already Exited";
            }
        }
        #endregion

        #region PUT
        [HttpPut("EditUserIdentity")]
        public string EditUserIdentity(User usercreate)
        {
            if (string.IsNullOrEmpty(usercreate.Username) || string.IsNullOrEmpty(usercreate.Passwords))
                return "Username or Password cannot be empty";

            string hash = BCrypt.Net.BCrypt.HashPassword(usercreate.Passwords);

            var user = _context.Users
            .Where(u => u.Username == usercreate.Username)
            .FirstOrDefault();

            bool valid = BCrypt.Net.BCrypt.Verify(usercreate.Passwords, user.Passwords);

            if (user == null || !valid)
            {
                return "No User Found";
            }
            else
            {
                if (usercreate.Email != null)
                {
                    user.Email = usercreate.Email;
                }
                if (usercreate.FirstName != null)
                {
                    user.FirstName = usercreate.FirstName;
                }
                if (usercreate.Lastname != null)
                {
                    user.Lastname = usercreate.Lastname;
                }
                if (usercreate.Telphone != null)
                {
                    user.Telphone = usercreate.Telphone;
                }
                _context.SaveChanges();

                return "Edit Users Completed";
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("DeleteUserIdentity")]
        public string DeleteUserIdentity(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return "Username or Password cannot be empty";

            string hash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = _context.Users
            .Where(u => u.Username == username)
            .FirstOrDefault();

            bool valid = BCrypt.Net.BCrypt.Verify(password, user.Passwords);

            if (user == null || !valid)
            {
                return "No User Found";
            }
            else
            {
                _context.Remove(user);
                _context.SaveChanges();
                return "Delete Users Completed";
            }
        }
        #endregion


    }
}
