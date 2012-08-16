using System.Configuration;
using System.IO;
using System.Diagnostics;
using System;
using System.Web;
namespace websiteurltopdf
{
    public class PdfCreator
    {
        public PdfCreator(string CurrentUrl, string CurrentTempPath, string CurrentWkhtmlToPdfPath)
        {
            tempPath = CurrentTempPath;
            wkhtmltopdfPath = CurrentWkhtmlToPdfPath;
            Url = CurrentUrl;
        } //constructor

        private string Url;
        private string tempPath;
        private string wkhtmltopdfPath;
        private MemoryStream pdfMemoryStream;
        public MemoryStream PdfMemoryStream
        {
            get { return pdfMemoryStream; }
        }

        private FileInfo pdfFileInfo;
        public FileInfo PdfFileInfo
        {
            get { return pdfFileInfo; }
        }

        /// <summary>
        /// Create PDF method!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <returns></returns>
        public string CreatePdf()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            DateTime NowDateTime = DateTime.Now;
            string filenamePDF = "wkhtmltopdf" + NowDateTime.ToString("_yyyy-MM-dd_hh-mm-ss-ffffff") + ".pdf";
            string pdfPath = (tempPath + filenamePDF);
            startInfo.FileName = wkhtmltopdfPath;
            startInfo.Arguments = Url + " " + pdfPath;

            try
            {
                Process MyProcess = Process.Start(startInfo);
                MyProcess.WaitForExit();
                if (File.Exists(pdfPath))
                {
                    pdfMemoryStream = new MemoryStream();
                    using (FileStream PdfFileStream = File.OpenRead(pdfPath))
                    {
                        pdfMemoryStream.SetLength(PdfFileStream.Length);
                        PdfFileStream.Read(pdfMemoryStream.GetBuffer(), 0, (int)PdfFileStream.Length);
                    }
                    pdfFileInfo = new FileInfo(pdfPath);
                    File.Delete(pdfPath);
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