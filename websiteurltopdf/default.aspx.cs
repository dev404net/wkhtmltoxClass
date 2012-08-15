using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace websiteurltopdf
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlDownloadArea.Visible = true;
            string TempPath = Server.MapPath(ConfigurationManager.AppSettings["tempPath"]);
            string WkhtmltopdfPath = Server.MapPath(ConfigurationManager.AppSettings["wkhtmltopdfpath"]);
            displayResponse.Text = TempPath + "<br />" + WkhtmltopdfPath;
            displayResponse.Visible = true;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            createPdf();
        }

        private void createPdf()
        {
            string TempPath = Server.MapPath(ConfigurationManager.AppSettings["tempPath"]);
            string WkhtmltopdfPath = Server.MapPath(ConfigurationManager.AppSettings["wkhtmltopdfpath"]);
            PdfCreator MyPdfCreater = new PdfCreator(inputUrl.Text, TempPath, WkhtmltopdfPath);
            string PdfCreatorResponse = MyPdfCreater.CreatePdf();


            if (PdfCreatorResponse == "true")
            {

                string PdfFileName;
                try
                {
                    PdfFileName = MyPdfCreater.PdfFileInfo.Name;
                }
                catch
                {
                    PdfFileName = "error";
                }
                displayResponse.Text = PdfCreatorResponse;
                System.IO.MemoryStream mstream = MyPdfCreater.PdfMemoryStream;
                byte[] byteArray = mstream.ToArray();
                mstream.Flush();
                mstream.Close();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + PdfFileName);
                Response.AddHeader("Content-Length", byteArray.Length.ToString());
                Response.ContentType = "application/pdf-stream";
                Response.BinaryWrite(byteArray);
                displayResponse.Visible = true;
                pnlDownloadArea.Visible = true;
            }
            else
            {
                displayResponse.Text = PdfCreatorResponse + "<br />" + TempPath + "<br />" + WkhtmltopdfPath;
                displayResponse.Visible = true;
                pnlDownloadArea.Visible = true;
            }
        }

        protected void btnCreateImage_Click(object sender, EventArgs e)
        {
            createImage();
        }

        private void createImage()
        {
            string TempPath = Server.MapPath(ConfigurationManager.AppSettings["tempPath"]);
            string WkhtmltoimagePath = Server.MapPath(ConfigurationManager.AppSettings["wkhtmltoimagepath"]);
            ImageCreator MyImageCreater = new ImageCreator(inputUrl.Text, TempPath, WkhtmltoimagePath);
            string ImageCreaterResponse = MyImageCreater.CreateImage();

            if (ImageCreaterResponse == "true")
            {
                string ImageFileName;
                try
                {

                    ImageFileName = MyImageCreater.ImageFileInfo.Name;
                }
                catch
                {
                    ImageFileName = "error";
                }

                displayResponse.Text = ImageCreaterResponse;
                pnlDownloadArea.Visible = true;
                System.IO.MemoryStream mstream = MyImageCreater.ImageMemoryStream;
                byte[] byteArray = mstream.ToArray();
                mstream.Flush();
                mstream.Close();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ImageFileName);
                Response.AddHeader("Content-Length", byteArray.Length.ToString());
                Response.ContentType = "application/jpg-stream";
                Response.BinaryWrite(byteArray);
                displayResponse.Visible = true;
                pnlDownloadArea.Visible = true;
            }
            else
            {
                displayResponse.Text = ImageCreaterResponse + "<br />" + TempPath + "<br />" + WkhtmltoimagePath;
                displayResponse.Visible = true;
                pnlDownloadArea.Visible = true;
            }
        }
    }
}