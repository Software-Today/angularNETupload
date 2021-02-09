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
    public class UploadViewController : Controller
    {
        private readonly DatabaseContext _context;
        public UploadViewController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var objfiles = new Files()
                    {
                        DocumentId = 0,
                        Name = newFileName,
                        FileSize = files.Length.ToString(),
                        CreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }

                    _context.Files.Add(objfiles);
                    _context.SaveChanges();

                }
            }
            return View();
        }
    }
}
