using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Taqm.Service.Abstracts;

namespace Taqm.Service.Services
{
    public class FileService : IFileService
    {
        #region Fields
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructors
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Methods
        public async Task<string> UploadImageAsync(string Location, IFormFile file)
        {
            if (file == null || file.Length <= 0) return "NoImage";
            var path = _webHostEnvironment.WebRootPath + "/" + Location + "/";
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;
            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                using (FileStream fileStream = File.Create(path + fileName))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return $"/{Location}/{fileName}";
                }
            }
            catch (Exception)
            {
                return "FailedToUploadImage";
            }
        }
        #endregion
    }
}
