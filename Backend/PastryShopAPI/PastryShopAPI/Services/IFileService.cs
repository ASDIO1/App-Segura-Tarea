using Microsoft.AspNetCore.Http;

namespace PastryShopAPI.Services
{
    public interface IFileService
    {
        string UploadFile(IFormFile file);
        bool IsNewFile(IFormFile actualfile, IFormFile newFile);
    }
}
