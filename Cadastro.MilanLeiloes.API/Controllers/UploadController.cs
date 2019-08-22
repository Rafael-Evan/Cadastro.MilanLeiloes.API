using Cadastro.MilanLeiloes.Domain.Model;
using Cadastro.MilanLeiloes.Domain.Models;
using Cadastro.MilanLeiloes.Repository;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public UploadController(ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost("Upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile(string name, IFormFile files, string type, string email)
        {
            var pasta = DateTime.Now.Year;
            if (files == null || files.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "Fotos/CadastroMilanLeiloes/" + pasta,
                        files.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                var documentos = new Documentos();
                var user = await _userManager.FindByEmailAsync(email);
                documentos.UserId = user.Id;
                documentos.Name = name;
                documentos.Pasta = pasta;
                documentos.Data = DateTime.UtcNow;

                _context.Add(documentos);

                var result = await _context.SaveChangesAsync();

                await files.CopyToAsync(stream);

            }

            return Ok(files);
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
