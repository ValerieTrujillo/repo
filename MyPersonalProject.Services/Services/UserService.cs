using HtmlAgilityPack;
using MyPersonalProject.Models;
using MyPersonalProject.Models.Domain;
using MyPersonalProject.Models.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyPersonalProject.Services
{
    public class UserService : IUserService
    {
        private IDataProvider _dataProvider;

        public UserService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }


        public int Register(RegisterAddRequest model)
        {
            int userId = 0;
            string password = model.Password;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            this._dataProvider.ExecuteNonQuery(
                "Registration_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter parm = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    paramCol.Add(parm);
                    paramCol.AddWithValue("@FirstName", model.FirstName);
                    paramCol.AddWithValue("@MiddleInitial", model.MiddleInitial);
                    paramCol.AddWithValue("@LastName", model.LastName); 
                    paramCol.AddWithValue("@Email", model.Email);
                    paramCol.AddWithValue("@Salt", salt);
                    paramCol.AddWithValue("@Password", hashedPassword);
                    paramCol.AddWithValue("@ModifiedBy", model.ModifiedBy);
                },
                returnParameters: delegate (SqlParameterCollection paramCol)
                {
                    userId = (int)paramCol["@Id"].Value;
                }
           );
            return userId;
        }

        public int PostBlogs(WebScraperModel model)
        {
            int Id = 0;
            _dataProvider.ExecuteNonQuery(
                "WebScraper_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = "@Id";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Direction = System.Data.ParameterDirection.Output;
                    paramCol.Add(parm);

                    paramCol.AddWithValue("@Header", model.Header);
                    paramCol.AddWithValue("@Description", model.Description);
                    paramCol.AddWithValue("@Image", model.Image);
                },
                returnParameters: delegate (SqlParameterCollection paramCol)
                {
                    Id = (int)paramCol["@Id"].Value;
                }
                );
            return Id;
         }

        public int MyBlogPost(MyBlogDomain model)
        {
            int Id = 0;
            _dataProvider.ExecuteNonQuery(
                "BlogPost_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = "@Id";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Direction = System.Data.ParameterDirection.Output;
                    paramCol.Add(parm);

                    paramCol.AddWithValue("@Header", model.Header);
                    paramCol.AddWithValue("@Description", model.Description);
                    paramCol.AddWithValue("@BlogPost", model.BlogPost);
                    paramCol.AddWithValue("@Image", model.Image);
                },
                returnParameters: delegate (SqlParameterCollection paramCol)
                {
                    Id = (int)paramCol["@Id"].Value;
                }
                );
            return Id;
        }
        public List<WebScraperModel> ScrapeData(string page)
        {
            List<WebScraperModel> _webScraperModel = new List<WebScraperModel>();
            
                    var web = new HtmlWeb();
                    var doc = web.Load(page);

                    var Articles = doc.DocumentNode.SelectNodes("//article");

                    foreach (var article in Articles)
                    {
                        var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//a[@class = 'entry-title-link']").InnerText);
                        var description = HttpUtility.HtmlDecode(article.SelectSingleNode(".//p").InnerText);
                        var image = HttpUtility.HtmlDecode(article.SelectSingleNode(".//img[@class = 'alignleft post-image entry-image']").GetAttributeValue("src", ""));
                        


                        
                        WebScraperModel art = new WebScraperModel()
                        {
                            Header = header,
                            Description = description,
                            Image = image
                        };
                        _webScraperModel.Add(art);
                    }

            return _webScraperModel;
        }

        public List<ScraperModel> DisplayAll()
        {
            List<ScraperModel> result = new List<ScraperModel>();
            _dataProvider.ExecuteCmd(
                "WebScraper_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    ScraperModel model = DisplayAllMapper(reader);
                    result.Add(model);
                }
                );
            return result;
        }

        public List<MyBlogDomain> DisplayMyPosts()
        {
            List<MyBlogDomain> result = new List<MyBlogDomain>();
            _dataProvider.ExecuteCmd(
                "BlogPost_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    MyBlogDomain model = DisplayMyPostsMapper(reader);
                    result.Add(model);
                }
                );
            return result;
        }

        public MyBlogDomain SelectMyBlogById(int id)
        {
            MyBlogDomain model = null;
            _dataProvider.ExecuteCmd(
                "BlogPost_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    model = DisplayMyPostsMapper(reader);
                }
                );
            return model;
        }

        public ScraperModel SelectById(int id)
        {
            ScraperModel model = null;
            _dataProvider.ExecuteCmd(
                "WebScraper_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    model = DisplayAllMapper(reader);
                }
                );
            return model;
        }

        public void Update(MyBlogUpdateRequest model)
        {
            _dataProvider.ExecuteNonQuery(
                "BlogPost_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", model.Id);
                    paramCol.AddWithValue("@Header", model.Header);
                    paramCol.AddWithValue("@Description", model.Description);
                    paramCol.AddWithValue("@BlogPost", model.BlogPost);
                    paramCol.AddWithValue("@Image", model.Image);
                }
                );
        }

        public void Delete(int id)
        {
            _dataProvider.ExecuteNonQuery(
                "BlogPost_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }
                );
        }
        public LoginDomain UserLogin(string email, string passwordHash)
        {
            LoginDomain response = new LoginDomain();

            _dataProvider.ExecuteCmd(
                "Login_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("Email", email);
                    paramList.AddWithValue("Password", passwordHash);

                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    response = LoginMapper(reader);
                });
            return response;

        }
        public static LoginDomain LoginMapper(IDataReader reader)
        {
            LoginDomain model = new LoginDomain();
            int index = 0;
            model.Id = reader.GetInt32(index++);
            model.Email = reader.GetString(index++);
            model.Salt = reader.GetString(index++);
            model.Password = reader.GetString(index++);
            model.ModifiedBy = reader.GetString(index++);

            return model;
        }

        public static ScraperModel DisplayAllMapper(IDataReader reader)
        {
            ScraperModel model = new ScraperModel();
            int index = 0;
            model.Id = reader.GetInt32(index++);
            model.Header = reader.GetString(index++);
            model.Description = reader.GetString(index++);
            model.Image = reader.GetString(index++);

            return model;
        }

        public static MyBlogDomain DisplayMyPostsMapper(IDataReader reader)
        {
            MyBlogDomain model = new MyBlogDomain();
            int index = 0;
            model.Id = reader.GetInt32(index++);
            model.Header = reader.GetString(index++);
            model.Description = reader.GetString(index++);
            model.BlogPost = reader.GetString(index++);
            model.Image = reader.GetString(index++);

            return model;
        }

        public IUserAuthData Login(string email, string password)
        {
            throw new System.NotImplementedException();
        }
    }
    
}