using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Configuration;

namespace wkhtmltoimageClass
{
    public class Image
    {
        public Image(string CurrentWkhtmlToImagePath)
        {
            wkhtmlToImagePath = CurrentWkhtmlToImagePath;
        }//constructor

        private string wkhtmlToImagePath;
        private string tempPath = ConfigurationManager.AppSettings["tempPath"];



        /// <summary>
        /// Used to copy a stream outputed from the process
        /// and paste into the initialsed empty stream.
        /// </summary>
        /// <param name="input">Stream outputed from process</param>
        /// <param name="output">Stream to copy into.</param>
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }


        /// <summary>
        /// Creats an image of a webpage using a URL then stores the result in a temp file.
        /// The temp file is then read into a memory stream and returned with the temp file
        /// being deleted.
        /// Pros: Works correctly.
        /// Cons: Requires write permissions on file system
        /// </summary>
        /// <param name="CurrentUrl"></param>
        /// <returns></returns>
        public MemoryStream makeImage(string CurrentUrl)
        {
            using (Process MyProcess = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                DateTime NowDateTime = DateTime.Now;
                string filenameImage = "wkhtmltoimage" + NowDateTime.ToString("_yyyy-MM-dd_hh-mm-ss") + ".jpg";
                string imagePath = (tempPath + filenameImage);
                startInfo.FileName = wkhtmlToImagePath;
                startInfo.Arguments = CurrentUrl + " " + imagePath;
                MemoryStream imageStream = new MemoryStream();
                try
                {
                    MyProcess.StartInfo = startInfo;
                    MyProcess.Start();
                    MyProcess.WaitForExit();
                    if (File.Exists(imagePath))
                    {
                        using (FileStream imageFileStream = File.OpenRead(imagePath))
                        {
                            imageStream.SetLength(imageFileStream.Length);
                            imageFileStream.Read(imageStream.GetBuffer(), 0, (int)imageFileStream.Length);
                        }
                        FileInfo imageFileInfo = new FileInfo(imagePath);
                        File.Delete(imagePath);
                        return imageStream;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
                finally
                {
                    imageStream.Dispose();
                }
            }
        }
    }
}