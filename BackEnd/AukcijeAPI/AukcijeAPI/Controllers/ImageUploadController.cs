using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploadAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUploadAPI
        {
            public IFormFile files { get; set; }

        }

        [HttpPost]
        [Route("{id}")]
        public string Post([FromForm] FileUploadAPI objFile, string id)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + id/* + ".jpg"*/))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\Upload\\" + id/* + ".jpg"*/;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpGet]
        [Route("{fileName}")]
        public IActionResult Get(string fileName)
        {
            try
            {   //ekstenzija u URL-u
                var image = System.IO.File.OpenRead(_environment.WebRootPath + "\\Upload\\" + fileName);
                return File(image, "image/jpg");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}