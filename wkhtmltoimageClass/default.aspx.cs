using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wkhtmltoimageClass
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btConvertToImage_Click(object sender, EventArgs e)
        {
            createImage();
        }

        private void createImage()
        {
            string WkhtmltoimagePath = ConfigurationManager.AppSettings["wkhtmltoimagePath"];
            Image MyImage = new Image(WkhtmltoimagePath);
            MemoryStream mstream = MyImage.makeImage(tbUrl.Text);

            if (mstream != null)
            {
                byte[] byteArray = mstream.ToArray();
                mstream.Flush();
                mstream.Close();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "createdImage.jpg");
                Response.AddHeader("Content-Length", byteArray.Length.ToString());
                Response.ContentType = "application/jpg";
                Response.BinaryWrite(byteArray);
                mstream.Dispose();
            }
            else
            {
                mstream.Dispose();
            }
        }
    }
}