using Microsoft.AspNetCore.Http;

namespace Taqm.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImageAsync(string location, IFormFile file);
    }
}
