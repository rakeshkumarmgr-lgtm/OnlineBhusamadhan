using System;
using System.IO;
using System.Web.UI.WebControls;

namespace Bhusamadhan.DB
{
    /// <summary>
    /// Provides helper methods for uploading PDF and Image files
    /// through the UploadImageService web service.
    /// </summary>
    public class FileSaveServer
    {
        public FileSaveServer()
        {
            // Default Constructor
        }

        /// <summary>
        /// Upload PDF document to server.
        /// </summary>
        /// <param name="_path">Destination Path</param>
        /// <param name="_Photo">Base64 Encoded File</param>
        /// <param name="_filename">File Name</param>
        /// <param name="_fileExt">File Extension</param>
        /// <returns>Upload Status</returns>
        public static string InsertPDFNew(string _path, string _Photo, string _filename, string _fileExt)
        {
            string msg = "";

            try
            {
                UploadImageService.ImageWebService service1 = new UploadImageService.ImageWebService();

                string a = service1.InsertPDF(_path, _Photo, _filename, _fileExt);

                msg = a;
            }
            catch (Exception ee)
            {
                msg = ee.Message;
            }

            return msg;
        }

        /// <summary>
        /// Upload Image to server.
        /// </summary>
        /// <param name="_path">Destination Path</param>
        /// <param name="_Photo">Base64 Encoded Image</param>
        /// <param name="_filename">File Name</param>
        /// <returns>Upload Status</returns>
        public static string InsertPicNew(string _path, string _Photo, string _filename)
        {
            string msg = "";

            try
            {
                UploadImageService.ImageWebService service1 = new UploadImageService.ImageWebService();

                string a = service1.InsertImage(_path, _Photo, _filename);

                msg = a;
            }
            catch (Exception ee)
            {
                msg = ee.Message;
            }

            return msg;
        }

        /// <summary>
        /// Converts uploaded file into Base64 string.
        /// </summary>
        /// <param name="FileUpload1">ASP.NET FileUpload Control</param>
        /// <returns>Base64 String</returns>
        //public static string getBase64(FileUpload FileUpload1)
        //{
        //    BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream);

        //    byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);

        //    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

        //    return base64String;
        //}

        public static string getBase64(FileUpload FileUpload1)
        {
            if (FileUpload1 == null)
                return string.Empty;

            if (!FileUpload1.HasFile)
                return string.Empty;

            if (FileUpload1.PostedFile == null)
                return string.Empty;

            if (FileUpload1.PostedFile.InputStream == null)
                return string.Empty;

            using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
            {
                byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);

                if (bytes == null || bytes.Length == 0)
                    return string.Empty;

                return Convert.ToBase64String(bytes);
            }
        }
    }
}