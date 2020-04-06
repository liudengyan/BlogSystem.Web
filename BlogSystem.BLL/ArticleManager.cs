using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Webdiyer.WebControls.Mvc;

namespace BlogSystem.BLL
{
    public class ArticleManager
    {
        public async Task CreateAsync(string title,string content,string email)
        {
            UserManager userMan = new UserManager();
            
            var user = await userMan.GetUserByEmailAsync(email);
            using (DAL.ArticleService asv = new DAL.ArticleService())
            {
                await asv.CreatAsync(new Models.Article { UserId = user.UserId, Title = title, Content = content ,CreatTime = DateTime.Now});
            }
        }

        public async Task<List<Dto.ArticleDto>> GetAllArticleAsync(int pageIndex = 0,int pagesize=10)
        {
            using (DAL.ArticleService asv = new DAL.ArticleService())
            {
                return await asv.GetAllWhere(m => true, false, pageIndex, pagesize)
                    .Include(m => m.User)
                    .Select(m => new Dto.ArticleDto
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        UserEmail = m.User.Email,
                        UserId = m.UserId,
                        CreatTime = m.CreatTime
                    }).ToListAsync();
            }
        }

        public PagedList<Dto.ArticleDto> GetAllArticle(int id=0,int pagesize=10)
        {
            using (DAL.ArticleService asv = new DAL.ArticleService())
            {
                return asv.GetAllWhere(m => true, false)
                    .Include(m => m.User)
                    .Select(m => new Dto.ArticleDto
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        UserEmail = m.User.Email,
                        UserId = m.UserId,
                        CreatTime = m.CreatTime
                    }).ToPagedList(id,pagesize);
            }
        }
    }
}
