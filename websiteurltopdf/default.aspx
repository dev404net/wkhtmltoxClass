<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="websiteurltopdf._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p><asp:Label ID="displayIntro" runat="server" Text="Label">Enter a full URL e.g. http://www.google.com</asp:Label></p>
        <asp:TextBox ID="inputUrl" runat="server" Text="http://www.google.com"></asp:TextBox><br />
        <asp:Button ID="btnCreate"
            runat="server" Text="Create PDF" onclick="btnCreate_Click" /><br />
        <asp:Button ID="btnCreateImage" runat="server" Text="Create Image" 
            onclick="btnCreateImage_Click" />
            <asp:Panel ID="pnlDownloadArea" runat="server">
                <asp:Label ID="displayResponse" runat="server" Text="Label"></asp:Label><br />
                <asp:Image ID="imgResponse" runat="server" />
            </asp:Panel>
    </div>
    </form>
</body>
</html>
