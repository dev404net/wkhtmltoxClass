using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Configuration;

namespace wkhtmltopdfClass
{
    /// <summary>
    /// Class to create a PDF file object.
    /// Currently creates a memory stream of a PDF file and
    /// a FileInfo relating to the PDF file.
    /// </summary>
    public class Pdf
    {
        public Pdf(string CurrentWkhtmlToPdfPath)
        {
            wkhtmltopdfPath = CurrentWkhtmlToPdfPath;
        }//constructor

        private string wkhtmltopdfPath;
        private string tempPath = ConfigurationManager.AppSettings["tempPath"];
        private MemoryStream pdfMemoryStream;
        public MemoryStream PdfMemoryStream
        {
            get { return pdfMemoryStream; }
            set
            {
                pdfMemoryStream = value;
            }
        }

        private FileInfo pdfFileInfo;
        public FileInfo PdfFileInfo
        {
            get { return pdfFileInfo; }
            set { pdfFileInfo = value; }
        }

        /// <summary>
        /// This method creates a PDF and stores as a temp file then reads the
        /// temp file in a memory stream and then deletes the temp file
        /// Pros: Works well.
        /// Cons: Does require write permissions on filesystem
        /// </summary>
        /// <param name="CurrentUrl">Website URL to convert to a PDF</param>
        /// <returns></returns>
        public bool makePdf(string CurrentUrl, string PdfFileName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            DateTime NowDateTime = DateTime.Now;
            string filenameImage = PdfFileName + "-" + NowDateTime.ToString("_yyyy-MM-dd_hh-mm-ss") + ".pdf";
            filenameImage = filenameImage.Replace(" ", "_");
            string pdfPath = (tempPath + filenameImage);
            startInfo.FileName = wkhtmltopdfPath;
            startInfo.Arguments = CurrentUrl + " " + pdfPath;
            try
            {
                using (Process MyProcess = Process.Start(startInfo))
                {
                    MyProcess.WaitForExit();
                    if (File.Exists(pdfPath))
                    {

                        using (FileStream pdfFileStream = File.OpenRead(pdfPath))
                        {
                            pdfMemoryStream = new MemoryStream();
                            pdfMemoryStream.SetLength(pdfFileStream.Length);
                            pdfFileStream.Read(pdfMemoryStream.GetBuffer(), 0, (int)pdfFileStream.Length);
                        }
                        pdfFileInfo = new FileInfo(pdfPath);
                        File.Delete(pdfPath);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}