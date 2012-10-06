<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="wkhtmltoimageClass._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>wkhtmltoimage example</h1>
                URL: <asp:TextBox ID="tbUrl" runat="server">http://codetux.com</asp:TextBox>
        <br />
            <p>
                        Convert the webpage found at the URL entered above into a JPG file.
        <br />
                <asp:Button ID="btConvertToImage" runat="server" 
                    Text="Convert to image without using temp file" 
                    onclick="btConvertToImage_Click" />
            </p>
    </div>
    </form>
</body>
</html>
