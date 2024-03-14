using CloudStorageTestApplication.UseCase.User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StorageController : ControllerBase
{
    [HttpPost]
    public IActionResult UploadImage(IFormFile file)
    {
        var useCase = new UploadProfilePhotoUseCase();
            useCase.Execute(file);
        return Created("",file);
    }
}
