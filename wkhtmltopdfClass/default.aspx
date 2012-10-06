<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="wkhtmltopdfClass._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
        URL: <asp:TextBox ID="tbUrl" runat="server">http://codetux.com</asp:TextBox>
        <br />
        <h1>wkhtmltopdf example</h1>
        <p>
        Convert the webpage found at the URL entered above into a PDF file.
        <br />
        <asp:Button ID="btConvertToImage" runat="server" Text="Convert webpage to PDF" 
            onclick="btConvertToPdf_Click" />
            </p>
    </div>
    </form>
</body>
</html>
