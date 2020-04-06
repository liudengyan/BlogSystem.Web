using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class UserManager
    {
        public async Task<bool> LoginAsync(string email, string pwd)
        {
            using (DAL.UserService userSvc = new DAL.UserService())
            {
                return await userSvc.Login(email, pwd);
            }
        }
        //业务逻辑层，一个按钮一个方法。
        public async Task Regist(string email, string pwd)
        {
            using (DAL.UserService userSvc = new DAL.UserService())
            {
                await userSvc.CreatAsync(new Models.User { Email = email, Password = pwd, LastLoginTime = DateTime.Now });
            }
        }

        public async Task<Dto.UserDto> GetUserByEmailAsync(string email)
        {
            using (DAL.UserService userService = new DAL.UserService())
            {
                var result = await userService.GetAll().FirstAsync(m=>m.Email==email);
                return new Dto.UserDto { Email= result.Email,Password= result.Password,LastLoginTime= result.LastLoginTime,UserId = result.Id};
            }
        }

        public async Task<List<Dto.UserDto>> GetAllUser()
        {
            using (DAL.UserService userService = new DAL.UserService())
            {
                return await userService.GetAll().Select(m=>new Dto.UserDto {Email = m.Email,Password=m.Password,LastLoginTime=m.LastLoginTime,UserId=m.Id}).ToListAsync();
            }
        }
    }
}
