namespace ads.feira.api.Helpers
{   
    public static class FilesExtensions
    {
        public static async Task<string> UploadImage(IFormFile image, string folderPath = "wwwroot/images")
        {
            if (image != null && image.Length > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderPath, fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                return $"/{folderPath}/{fileName}".Replace("wwwroot/", "");
            }
            return null;
        }
    }
}
