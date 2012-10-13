using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wkhtmltopdfClass
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btConvertToPdf_Click(object sender, EventArgs e)
        {
            createPDF();
        }

        private void createPDF()
        {
            string WkhtmltopdfPath = ConfigurationManager.AppSettings["wkhtmltopdfPath"];
            Pdf MyPdf = new Pdf(WkhtmltopdfPath);
            string PdfFileName = "codeTux";
            
            if (MyPdf.makePdf(tbUrl.Text, PdfFileName))
            {
                using (MyPdf.PdfMemoryStream)
                {
                    byte[] byteArray = MyPdf.PdfMemoryStream.ToArray();
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + MyPdf.PdfFileInfo.Name);
                    Response.AddHeader("Content-Length", byteArray.Length.ToString());
                    Response.ContentType = "application/pdf-stream";
                    Response.BinaryWrite(byteArray);
                }
            }
        }
    }
}