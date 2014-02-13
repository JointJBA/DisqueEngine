<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRenderToy.Default" Async="true" ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>C# Raytracer</title>
    <link rel="stylesheet" type="text/css" href="Styles/Site.css"/>
</head>
<body>
<div id="mainBody">
    <form id="form1" runat="server">
    <asp:TextBox ID="textEditor" runat="server" Height="350px" TextMode="MultiLine" Width="795px"></asp:TextBox>
    <br />
    <asp:Button ID="render" runat="server" Text="Render" onclick="render_Click" 
        Height="40px" Width="800px" />
    <asp:Image ID="renderedImage" runat="server" />
    <br />
    <asp:Repeater runat="server" DataSourceID="ImageSource">
    <HeaderTemplate>
    <table border="2" style="width: 800px">
    <thead>
    <tr>
    <td>
    Image
    </td>
    <td>
    Samples
    </td>
    <td>
    Total Render Time
    </td>
    <td>
    Date Rendered
    </td>
    </tr>
    </thead>
    </HeaderTemplate>
    <ItemTemplate>
    <tr>
    <td>
    <a href="ImageHandler.ashx?id=<%# Eval("ID")%>"><img src="ImageHandler.ashx?id=<%# Eval("ID")%>" style="height: 180px"/></a>
    </td>
    <td>
    <%# Eval("Samples") %>
    </td>
    <td>
    <%# Eval("RenderTime") %>
    </td>
    <td>
    <%# Eval("RenderDate") %>
    </td>
    </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="ImageSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ImageBase %>"         
        SelectCommand="SELECT [ID], [Samples], [RenderTime], [RenderDate] FROM [Image] ORDER BY [ID] DESC">
    </asp:SqlDataSource>
    <br />
    </form>
    </div>
</body>
</html>
