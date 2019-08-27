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
        public async Task<IActionResult> UploadFile(string name, ICollection<IFormFile> files, string type, string email)
        {
            var nomedoArquivo = Guid.NewGuid().ToString() + ".png";
            var pasta = DateTime.Now.Year;
            if (files == null)
                return Content("file not selected");

            var verificarPasta = Path.Combine(
                        Directory.GetCurrentDirectory(), "Fotos/CadastroMilanLeiloes/" + pasta);

            if (!Directory.Exists(verificarPasta))
            {
                //Criamos um com o nome folder
                Directory.CreateDirectory(verificarPasta);

            }
            //var path = Path.Combine(
            //           Directory.GetCurrentDirectory(), "Fotos/CadastroMilanLeiloes/" + pasta,
            //           files.FileName);

            var path = Path.Combine(
                       Directory.GetCurrentDirectory(), "Fotos/CadastroMilanLeiloes/" + pasta,
                       nomedoArquivo);

            //if (System.IO.File.Exists(path))
            //{
            //    //string file_name = "the file name";
            //    var uploads = verificarPasta;
            //    foreach (var file in files)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var fileStream = new FileStream(Path.Combine(uploads, Guid.NewGuid().ToString()) + ".png", FileMode.Create))
            //            {
            //                var documentos = new Documentos();
            //                var user = await _userManager.FindByEmailAsync(email);
            //                documentos.UserId = user.Id;
            //                documentos.Name = name;
            //                documentos.Pasta = pasta;
            //                documentos.Data = DateTime.UtcNow;

            //                _context.Add(documentos);

            //                var result = await _context.SaveChangesAsync();

            //                await file.CopyToAsync(fileStream);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            using (var stream = new FileStream(path, FileMode.Create))
            {
                var documentos = new Documentos();
                var user = await _userManager.FindByEmailAsync(email);
                documentos.UserId = user.Id;
                documentos.Name = nomedoArquivo;
                documentos.Pasta = pasta;
                documentos.Data = DateTime.UtcNow;

                _context.Add(documentos);

                var result = await _context.SaveChangesAsync();

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        await file.CopyToAsync(stream);
                    }
                };
            }
            //}
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
            return File(memory, contentType: (path), Path.GetFileName(path));
        }
    }
}
