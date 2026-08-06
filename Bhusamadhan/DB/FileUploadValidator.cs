using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Bhusamadhan.DB
{
    public static class FileUploadValidator
    {
        /// <summary>
        /// Validate uploaded PDF file.
        /// Returns "OK" if valid, otherwise returns error message.
        /// maxFileSizeKB and minFileSizeKB are in KB.
        /// </summary>
        public static string IsPdf(this HttpPostedFile postedFile, uint maxFileSize, uint minFileSize)
        {
            if (maxFileSize >= 1023)
                maxFileSize = 1022;
            else
                maxFileSize = maxFileSize * 1024;

            if (minFileSize < 5)
                minFileSize = 5;
            else
                minFileSize = minFileSize * 1024;

            if (!string.Equals(postedFile.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                return "Invalid PDF File";
            }

            //-------------------------------------------
            //  Check the PDF extension
            //-------------------------------------------
            var postedFileExtension = Path.GetExtension(postedFile.FileName);
            if (!string.Equals(postedFileExtension, ".pdf", StringComparison.OrdinalIgnoreCase)
               )
            {
                //return false;
                return "Invalid PDF File Extension";
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    //return false;
                    return "File is not readable";
                }
                //------------------------------------------
                //   Check whether the file size exceeding the limit or not
                //------------------------------------------ 
                //if (postedFile.ContentLength < (minFileSize))
                //{
                //    //return false;
                //    return "File Size is too Small (Minimum " + (minFileSize / 1024) + " KB is required)";
                //}

                //if (postedFile.ContentLength > (maxFileSize))
                //{
                //    //return false;
                //    return "File Size should not be more than " + (maxFileSize / 1024) + " KB";

                //}

                byte[] buffer = new byte[minFileSize];
                postedFile.InputStream.Read(buffer, 0, Convert.ToInt32(minFileSize));
                string content = System.Text.Encoding.UTF8.GetString(buffer);

                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                 RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    // return false;
                    return "Invalid File";
                }

                if (Regex.IsMatch(content, @"%PDF-", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    // return false;
                    return "OK";
                }


            }
            catch (Exception)
            {
                //return false;
                return "Something Went Wrong.";
            }

            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return "Invalid PDF File.";
        }

        public static string IsImage1(this HttpPostedFile postedFile, int maxFileSizeMB, int minFileSizeKB)
        {
            // Convert MB to Bytes for max file size
            int maxFileSizeBytes = maxFileSizeMB * 1024 * 1024;
            int minFileSizeBytes = minFileSizeKB * 1024;

            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (!string.Equals(postedFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return "Invalid Image File";
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var postedFileExtension = Path.GetExtension(postedFile.FileName);
            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return "Invalid Image File Type";
            }

            //-------------------------------------------
            //  Attempt to read the file and check size
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                    return "File is not readable";

                if (postedFile.ContentLength < minFileSizeBytes)
                    return "File Size is too Small (Minimum {minFileSizeKB} KB is required)";

                if (postedFile.ContentLength > maxFileSizeBytes)
                    return "File Size should not be more than {maxFileSizeMB} MB";

                byte[] buffer = new byte[minFileSizeBytes];
                postedFile.InputStream.Read(buffer, 0, minFileSizeBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return "Invalid Image";
                }
            }
            catch (Exception)
            {
                return "Something Went Wrong.";
            }

            //-------------------------------------------
            //  Try to load as Bitmap
            //-------------------------------------------
            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return "Something went wrong while creating bitmap. Please ensure you have selected a valid image file";
            }
            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return "OK";
        }

        public static string IsImage(this HttpPostedFile postedFile, uint maxFileSize, uint minFileSize)
        {
            if (maxFileSize >= 1023)
                maxFileSize = 1022;
            else
                maxFileSize = maxFileSize * 1024;

            if (minFileSize < 5)
                minFileSize = 5;
            else
                minFileSize = minFileSize * 1024;

            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (!string.Equals(postedFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return "Invalid Image File";
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var postedFileExtension = Path.GetExtension(postedFile.FileName);
            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                //return false;
                return "Invalid Image File Type";
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    //return false;
                    return "File is not readable";
                }
                //------------------------------------------
                //   Check whether the image size exceeding the limit or not
                //------------------------------------------ 
                if (postedFile.ContentLength < (minFileSize))
                {
                    //return false;
                    return "File Size is too Small (Minimum " + (minFileSize / 1024) + " KB is required)";
                }

                if (postedFile.ContentLength > (maxFileSize))
                {
                    //return false;
                    return "File Size should not be more than " + (maxFileSize / 1024) + " KB";

                }

                byte[] buffer = new byte[minFileSize];
                postedFile.InputStream.Read(buffer, 0, Convert.ToInt32(minFileSize));
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    // return false;
                    return "Invalid Image";
                }
            }
            catch (Exception)
            {
                //return false;
                return "Something Went Wrong.";
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                // return false;
                return "Something went wrong while creating bitmap. Please ensure you have selected a valid image file";
            }
            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return "OK";
        }
    }
}