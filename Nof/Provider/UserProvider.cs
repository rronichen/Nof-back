using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nof.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nof.Provider
{
    public interface IUserProvider
    {
        int AddUser(User user);
        User GetUser(int Id);
    }
    public class UserProvider: IUserProvider
    {
        private readonly ApplicationDbContext _context;
        public UserProvider(ApplicationDbContext context)
        {
            _context = context;
        }
        public int AddUser(User user)
        {
            try
            {
                var existUser = _context.Users.Where(x => user.Username == x.Username).FirstOrDefault();
                if(existUser != null)
                {
                    return 0;
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                var userId = _context.Users.Where(x => user.Username == x.Username).FirstOrDefault().Id;
                return userId;

            } catch(Exception ex)
            {
                return -1;
            }
            
        }

        public User GetUser(int Id){

            try
            {
                var idParam = new SqlParameter("@userId", Id);

                var user = _context.Users.FromSqlRaw("EXECUTE GetUserById @userId", idParam).ToList();
                return user.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
