using Microsoft.AspNetCore.Http;

namespace ads.feira.application.Helpers
{
    public static class IFormFileExtensions
    {
        public static async Task<string> ConvertToBase64String(this IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
    }
}
