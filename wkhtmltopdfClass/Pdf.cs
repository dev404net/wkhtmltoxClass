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
    public class Pdf
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CurrentWkhtmlToPdfPath">wkhtmltopdf exe file path</param>
        public Pdf(string CurrentWkhtmlToPdfPath)
        {
            wkhtmltopdfPath = CurrentWkhtmlToPdfPath;
        }

        private string wkhtmltopdfPath;
        private string tempPath = ConfigurationManager.AppSettings["tempPath"];

        /// <summary>
        /// Used to copy the stream outputed from the process
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
        /// This method creates a PDF and stores as a temp file then reads the
        /// temp file in a memory stream and then deletes the temp file
        /// Pros: Works well.
        /// Cons: Does require write permissions on filesystem
        /// </summary>
        /// <param name="CurrentUrl">Website URL to convert to a PDF</param>
        /// <returns></returns>
        public MemoryStream makePdf(string CurrentUrl)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            DateTime NowDateTime = DateTime.Now;
            string filenameImage = "wkhtmltopdf" + NowDateTime.ToString("_yyyy-MM-dd_hh-mm-ss") + ".pdf";
            string pdfPath = (tempPath + filenameImage);
            startInfo.FileName = wkhtmltopdfPath;
            startInfo.Arguments = CurrentUrl + " " + pdfPath;
            MemoryStream pdfStream = new MemoryStream();
            try
            {
                Process MyProcess = Process.Start(startInfo);
                MyProcess.WaitForExit();
                if (File.Exists(pdfPath))
                {
                   
                    using (FileStream pdfFileStream = File.OpenRead(pdfPath))
                    {
                        pdfStream.SetLength(pdfFileStream.Length);
                        pdfFileStream.Read(pdfStream.GetBuffer(), 0, (int)pdfFileStream.Length);
                    }
                    FileInfo pdfFileInfo = new FileInfo(pdfPath);
                    File.Delete(pdfPath);
                    return pdfStream;
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
                pdfStream.Dispose();
            }
        }
    }
}