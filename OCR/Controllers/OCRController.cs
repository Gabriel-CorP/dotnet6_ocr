using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Tesseract;
using OCR.BL;
namespace OCR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCRController : ControllerBase
    {
        private Validaciones valid;
        public OCRController()
        {
            valid = new Validaciones();
        }
        [HttpPost("getText"), DisableRequestSizeLimit]
        public IActionResult getText(IFormFile documento)
        {
            try
            {
                var file = documento;
                var folderName = Path.Combine("tessdata", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = valid.Fecha() + fileName;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    try
                    {
                        var ocrengine = new TesseractEngine(Path.Combine(Directory.GetCurrentDirectory(), @"tessdata"), "spa", EngineMode.Default);
                        var img = Pix.LoadFromFile(fullPath);
                        var res = ocrengine.Process(img);
                        var delete = valid.DeleteImage(fullPath);// eliminando la umagen para evitar aculumacion de datos
                        return Ok(res.GetText());
                    }
                    catch (Exception ex)
                    {

                        return NotFound(ex.Message);
                    }


                }
                else
                {
                    return NotFound("menor a cero");
                }






            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
        
    }
}
