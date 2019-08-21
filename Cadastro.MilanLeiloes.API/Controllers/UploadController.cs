using Cadastro.MilanLeiloes.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.MilanLeiloes.API.Controllers
{
    public class UploadController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public UploadController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpPost("Upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile(string name, IFormFile files, string type)
        {
            var pasta = DateTime.Now.Year;
            if (files == null || files.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "Fotos/CadastroMilanLeiloes/" + pasta,
                        files.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await files.CopyToAsync(stream);

            }

            return RedirectToAction("Files");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, contentType:(path), Path.GetFileName(path));
        }
    }
}
