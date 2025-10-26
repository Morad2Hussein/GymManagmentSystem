
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManagementBll.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {

        public AttachmentService(IWebHostEnvironment hostEnvironment )
        {
            _hostEnvironment = hostEnvironment;
        }
        private readonly IWebHostEnvironment _hostEnvironment;
        #region Upload
        //Select the type of file you want to Upload
        // create an array that contains the types of files
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
        // select the max size of the file  // size = 5 MB
        private readonly long maxFileSize = 5 * 1024 * 1024;


        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                //1- check if (folder name && file) is null or not && Extension is one of element from my allowedExtension
                if (folderName is null || file is null || file.Length == 0) return null;
                // check if the size of the file matches the condition or not 
                if (file.Length > maxFileSize) return null;
                //  Get the Extension of the file
                var extension = Path.GetExtension(file.FileName).ToLower();
                // check if the extension is matches the allowedExtensions or not
                if (!allowedExtensions.Contains(extension)) return null;
                // select the path to store the files  
                // using  IWebHostEnvironment to check [folderpath -  Environment  - ..... ]
                var folderPath = Path.Combine(_hostEnvironment.WebRootPath, "images", folderName);
                // check if the folderpath is exists or not 
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // to make the name of file is unique i will use the Guid
                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(folderPath, fileName);

                // create stream to copy fileName
                using var filestream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(filestream);
                return fileName;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Upload File To Folder ={folderName} is {ex} ");
                return null;
            }

        }
        #endregion
        #region Delete 
        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(value: fileName) || string.IsNullOrEmpty(value: folderName))
                    return false;
                var FullPath = Path.Combine(_hostEnvironment.WebRootPath, "images", folderName, fileName);
                if(File.Exists(FullPath)){
                   File.Delete(FullPath);
                    return true;
                     }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Delete File With NAme {fileName} is {ex} ");
                return false;
            }
            }

        #endregion
    }
}
