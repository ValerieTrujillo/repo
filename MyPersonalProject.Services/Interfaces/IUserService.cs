using MyPersonalProject.Models;
using MyPersonalProject.Models.Domain;
using MyPersonalProject.Models.Request;
using System.Collections.Generic;

namespace MyPersonalProject.Services
{
    public interface IUserService
    {
        int Register(RegisterAddRequest model);
        //int Login(LoginAddRequest model);
        IUserAuthData Login(string email, string password);
        List<WebScraperModel> ScrapeData(string page);
        List<ScraperModel> DisplayAll();
        ScraperModel SelectById(int id);
        int PostBlogs(WebScraperModel model);
        int MyBlogPost(MyBlogDomain model);
        void Update(MyBlogUpdateRequest model);
        void Delete(int id);
        List<MyBlogDomain> DisplayMyPosts();
        MyBlogDomain SelectMyBlogById(int id);
    }
}
