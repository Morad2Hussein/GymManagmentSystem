using Microsoft.AspNetCore.Http;


namespace GymManagementBll.Services.AttachmentService
{
    public interface IAttachmentService
    {
        #region Upload
        /// <summary > for  Upload service
        // I want to take two parameters:
        // folderName: the location where I want to store the files after uploading.
        // IFormFile : used in ASP.NET Core to handle file uploads from HTTP requests, typically from HTML forms.
        // IFormFile file  Represents the uploaded file from the client(e.g., image, PDF, etc.).
        //string folderName In case the file name is not specified
        /// </summary>
        string? Upload(string folderName, IFormFile file);
        #endregion

        #region Delete
        /// <summary>
        /// Deletes a file from the specified folder.
        /// </summary>
        /// <param name="fileName">The name of the file to delete.</param>
        /// <param name="folderName">The folder where the file is stored.</param>
        /// <returns>True if the file was successfully deleted; otherwise, false.</returns>


        bool Delete(string fileName, string folderName); 
        #endregion


    }
}
