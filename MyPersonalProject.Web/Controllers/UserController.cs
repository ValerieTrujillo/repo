using MyPersonalProject.Models;
using MyPersonalProject.Models.Domain;
using MyPersonalProject.Models.Request;
using MyPersonalProject.Models.Responses;
using MyPersonalProject.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPersonalProject.Web.Controllers
{
    [RoutePrefix("api/myblog")]
    public class UserController : ApiController
    {
        private IUserService _userService;
        

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("users/register"), HttpPost]
        public HttpResponseMessage RegisterUser(RegisterAddRequest model)
        {
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _userService.Register(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
        [Route("login"), HttpPost]
        public HttpResponseMessage Login(LoginAddRequest model)
        {
            try
            {
                ItemResponse<IUserAuthData> resp = new ItemResponse<IUserAuthData>();
                resp.Item = _userService.Login(model.Email, model.Password);
                if (resp.Item != null)
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Credentials");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("displayBlog"), HttpGet]
        public HttpResponseMessage DisplayAll()
        {
            ItemsResponse<ScraperModel> resp = new ItemsResponse<ScraperModel>();
            resp.Items = _userService.DisplayAll();
            return Request.CreateResponse(HttpStatusCode.OK, resp);

        }

        [Route("displayMyBlogs"), HttpGet]
        public HttpResponseMessage DisplayMyPosts()
        {
            ItemsResponse<MyBlogDomain> resp = new ItemsResponse<MyBlogDomain>();
            resp.Items = _userService.DisplayMyPosts();
            return Request.CreateResponse(HttpStatusCode.OK, resp);

        }

        [Route("myPosts/{id:int}"), HttpGet]
        public HttpResponseMessage SelectMyBlogById(int id)
        {
            ItemResponse<MyBlogDomain> resp = new ItemResponse<MyBlogDomain>();
            resp.Item = _userService.SelectMyBlogById(id);

            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        [Route("postblogs"), HttpPost]
        public HttpResponseMessage PostBlogs(WebScraperModel model)
        {
            ItemResponse<int> resp = new ItemResponse<int>();
            resp.Item = _userService.PostBlogs(model);
            return Request.CreateResponse(HttpStatusCode.OK, resp);

        }
        [Route("myblogposts"), HttpPost]
        public HttpResponseMessage MyBlogPost(MyBlogDomain model)
        {
            ItemResponse<int> resp = new ItemResponse<int>();
            resp.Item = _userService.MyBlogPost(model);
            return Request.CreateResponse(HttpStatusCode.OK, resp);

        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            _userService.Delete(id);
            SuccessResponse resp = new SuccessResponse();
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, resp);
        }
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage SelectById(int id)
        {
            ItemResponse<ScraperModel> resp = new ItemResponse<ScraperModel>();
            resp.Item = _userService.SelectById(id);

            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(int id, MyBlogUpdateRequest model)
        {
            _userService.Update(model);

            SuccessResponse resp = new SuccessResponse();
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        [Route("getListings"), HttpPost]
        public HttpResponseMessage ScrapeData(string page)
        {
            ItemsResponse<WebScraperModel> resp = new ItemsResponse<WebScraperModel>();

            resp.Items = _userService.ScrapeData(page);
            for ( int  i=0; i < resp.Items.Count; i++)
            {
                _userService.PostBlogs(resp.Items[i]);
            }

            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
    }
}
