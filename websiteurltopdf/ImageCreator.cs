using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace websiteurltopdf
{
    public class ImageCreator
    {
        public ImageCreator(string CurrentUrl, string CurrentTempPath, string CurrentWkhtmlToPdfPath)
        {
            tempPath = CurrentTempPath;
            wkhtmltopdfPath = CurrentWkhtmlToPdfPath;
            Url = CurrentUrl;
        }

        private string Url;
        private string tempPath;
        private string wkhtmltopdfPath;
        private MemoryStream imageMemoryStream;
        public MemoryStream ImageMemoryStream
        {
            get { return imageMemoryStream; }
        }

        private FileInfo imageFileInfo;
        public FileInfo ImageFileInfo
        {
            get { return imageFileInfo; }
        }

        public string CreateImage()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            DateTime NowDateTime = DateTime.Now;
            string filenameImage = "wkhtmltoimage" + NowDateTime.ToString("_yyyy-MM-dd_hh-mm-ss") + ".jpg";
            string imagePath = (tempPath + filenameImage);
            startInfo.FileName = wkhtmltopdfPath;
            startInfo.Arguments = Url + " " + imagePath;

            try
            {
                Process MyProcess = Process.Start(startInfo);
                MyProcess.WaitForExit();
                if (File.Exists(imagePath))
                {
                    imageMemoryStream = new MemoryStream();
                    using (FileStream imageFileStream = File.OpenRead(imagePath))
                    {
                        imageMemoryStream.SetLength(imageFileStream.Length);
                        imageFileStream.Read(imageMemoryStream.GetBuffer(), 0, (int)imageFileStream.Length);
                    }
                    imageFileInfo = new FileInfo(imagePath);
                    File.Delete(imagePath);
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}