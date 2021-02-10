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

        [HttpGet, DisableRequestSizeLimit]
        public ActionResult UploadView()
        {
            try
            {
                var products = (from p in _context.Files
                select new {p.DocumentId, p.Name, p.FileSize, p.CreatedOn}).ToList();

                return Json(products);
            }
            catch (System.Exception ex)
            {
                return Json("View Failed: " + ex.Message);
            }
        }
    }
}
