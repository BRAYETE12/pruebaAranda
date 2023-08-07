using Microsoft.AspNetCore.Http;

namespace Application.Common.Services
{
    public class FileService
    {

        public async Task<string> Save(IFormFile file, string folder) 
        {
            var fileName = $"{Guid.NewGuid()}.{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folder, fileName);

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
