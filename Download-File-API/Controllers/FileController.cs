using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Download_File_API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {

        FileExtensionContentTypeProvider fileExtensionContentTypeProvider;
        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProviders)
        {
            fileExtensionContentTypeProvider = fileExtensionContentTypeProviders;
        }


        [HttpGet("{fileid}")]
        public IActionResult GetFile(string fileid)
        {
            string pathToFile = "Files/"+fileid;
            if(!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            var bytes=System.IO.File.ReadAllBytes(pathToFile);

            if(!fileExtensionContentTypeProvider.TryGetContentType(pathToFile,out var contentType))
            {
                contentType = "application/actet-stream";
            }

            return File(bytes,contentType,Path.GetFileName(pathToFile));
        }

    }
}
