using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MicroService.Image.WebApi.Controllers
{
    using System.IO;
    using System.Net;

    using MicroService.Core;
    using MicroService.Image.Service.Declaration;

    using Microsoft.AspNetCore.StaticFiles;

    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats;
    using SixLabors.ImageSharp.Formats.Gif;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Formats.Png;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// Controller de gestion des images.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageService _imageService;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ImageController"/>
        /// </summary>
        public ImageController()
        {
            _imageService = (IImageService)IocManager.GetSingleton<IImageService>();
        }

        [HttpPost]
        public ActionResult Create(IList<IFormFile> files)
        {
            try
            {
                if (files.Count != 1)
                {
                    return Problem("Une seule image est attendu.");
                }

                MemoryStream memory = new MemoryStream();

                files[0].CopyTo(memory);

                return Ok(_imageService.SaveFile(memory.ToArray(), files[0].FileName));
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }

        [HttpPost]
        [Route("{id}")]
        public ActionResult Update(string id, IList<IFormFile> files)
        {
            try
            {
                if (files.Count != 1)
                {
                    return Problem("Une seule image est attendu.");
                }

                MemoryStream memory = new MemoryStream();

                files[0].CopyTo(memory);

                return Ok(_imageService.UpdateFile(id, memory.ToArray(), files[0].FileName));
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _imageService.DeleteFile(id);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                return Ok(_imageService.GetFile(id));
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }

        [HttpGet]
        [Route("{id}/file")]
        public ActionResult GetFile(string id)
        {
            try
            {
                var file = _imageService.GetFile(id);
                if (file.Url.StartsWith("http"))
                {
                    return Redirect(file.Url);
                }

                string contentType;

                new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out contentType);

                return File(System.IO.File.ReadAllBytes(file.Url), contentType, file.FileName);
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }

        [HttpGet]
        public ActionResult List()
        {
            try
            {
                return Ok(_imageService.ListFile());
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }

        [HttpGet]
        [Route("{id}/resize")]
        public ActionResult Resize(string id, int x, int y)
        {
            try
            {
                var file = _imageService.GetFile(id);
                MemoryStream memoryStream;

                if (file.Url.StartsWith("http"))
                {
                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(file.Url);
                    memoryStream = new MemoryStream(imageBytes);
                }
                else
                {
                    memoryStream = new MemoryStream(System.IO.File.ReadAllBytes(file.Url));
                }

                MemoryStream output = new MemoryStream();

                IImageEncoder imageEncoder;
                if (file.FileName.Contains("gif"))
                {
                    imageEncoder = new GifEncoder();
                }
                else if (file.FileName.Contains("jpeg") || file.FileName.Contains("jpg"))
                {
                    imageEncoder = new JpegEncoder();
                }
                else if (file.FileName.Contains("png"))
                {
                    imageEncoder = new PngEncoder();
                }
                else
                {
                    imageEncoder = new PngEncoder();
                }

                using (Image image = Image.Load(memoryStream.ToArray()))
                {
                    image.Mutate(m => m.Resize(x, y));

                    image.Save(output, imageEncoder); // Automatic encoder selected based on extension.
                }

                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out contentType);

                output.Seek(0, SeekOrigin.Begin);

                return File(output, contentType, file.FileName);
            }
            catch (Exception e)
            {
                return Problem("Une erreur a eu lieu.");
            }
        }
    }
}
