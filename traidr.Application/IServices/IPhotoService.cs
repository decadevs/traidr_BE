using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace traidr.Application.IServices
{
  
        public interface IPhotoService
        {
            Task<ImageUploadResult> AddPhotoAsync(IFormFile imageFile);
            Task<DeletionResult> DeletePhotoAsync(string publicId);
        }
 
}
