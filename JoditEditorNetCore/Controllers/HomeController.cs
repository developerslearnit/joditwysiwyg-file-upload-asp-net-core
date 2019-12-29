using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JoditEditorNetCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.IO;

namespace JoditEditorNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("file/upload")]
        [Produces("application/json")]
        public async Task<IActionResult> Post(IFormFile file)
        {

            var filess = Request.Form.Files;

            // Get the file from the POST request
            var theFile = filess.FirstOrDefault();

            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;

            var fileRoute = Path.Combine(webRootPath, "uploads");

            
            string fullPath = Path.Combine(fileRoute, theFile.FileName);

            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(fullPath);
            if (!dir.Exists)
            {
                dir.Directory.Create();
            }
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await theFile.CopyToAsync(stream);
            }

            string AppBaseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";


            return Ok(new
            {
                success = true,
                data = new
                {
                    files = new[] { theFile.FileName },
                    baseurl = $"{AppBaseUrl}/uploads/",
                    message ="",
                    error="",
                    path = $"{AppBaseUrl}/uploads/{theFile.FileName}"
                }
            });


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
