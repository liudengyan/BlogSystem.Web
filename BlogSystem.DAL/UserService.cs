using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class UserService:BaseService<User>
    {
        public async Task<bool> Login(string email , string pwd)
        {
            var result = await Exists(m => m.Email == email && m.Password==pwd);
            if (result==true)
            {
                var user = await GetAll().FirstAsync(m=>m.Email==email);
                user.LastLoginTime = DateTime.Now;
                await db.SaveChangesAsync();
            }
            return result;
        }

        
    }
}
