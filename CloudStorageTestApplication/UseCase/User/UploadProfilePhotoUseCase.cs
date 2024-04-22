using FileTypeChecker;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorageTestApplication.UseCase.User
{
    public class UploadProfilePhotoUseCase
    {
        public void Execute(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var isRecognizableType = fileStream.Is<JointPhotographicExpertsGroup>();

        }
    }
}
