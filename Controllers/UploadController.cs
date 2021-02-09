using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using minetestupload.Models;

namespace minetestupload.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        //private IHostingEnvironment _hostingEnvironment;

        //public UploadController(IHostingEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //}
        private readonly DatabaseContext _context;
        public UploadController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                var file = Request.Form.Files[0];

                string folderName = "Upload";
                string webRootPath = startupPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var objfiles = new Files()
                    {
                        DocumentId = 0,
                        Name = fileName,
                        FileSize = file.Length.ToString(),
                        CreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }

                    _context.Files.Add(objfiles);
                    _context.SaveChanges();
                }

                return Json("Upload Successful.");
            }
            catch (System.Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }
    }
}
