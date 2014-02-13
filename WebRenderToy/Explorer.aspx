<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Explorer.aspx.cs" Inherits="WebRenderToy.Explorer"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tbody>
            <tr>
                <td>
                    <div style="height: 650px; width: 200px; overflow: auto; float: left">
                        <asp:TreeView ID="tree" runat="server">
                            <Nodes>
                            </Nodes>
                        </asp:TreeView>
                    </div>
                </td>
                <td>
                    <div style="height: 650px; width: 1000px; overflow: auto">
                        <textarea runat="server" id="file" readonly="readonly" style="width: 100%; height: 100%" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
