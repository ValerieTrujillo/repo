using MyPersonalProject.Models.Responses;
using MyPersonalProject.Services.Interfaces;
using System.Net.Http;
using System.Web.Http;

namespace MyPersonalProject.Web.Controllers
{
    [RoutePrefix("api/fileUpload")]
    public class FileUploadController : ApiController
    {
        private IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            _fileUploadService.Delete(id);
            SuccessResponse resp = new SuccessResponse();
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, resp);
        }
    }
}