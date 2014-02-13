<%@ Page Title="" Language="C#" MasterPageFile="~/RayMaster.Master" AutoEventWireup="true" CodeBehind="OBJConv.aspx.cs" Inherits="WebRenderToy.OBJConv" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder2" runat="server">
    <asp:TextBox ID="input" runat="server" Height="400px" Width="100%" 
        TextMode="MultiLine"></asp:TextBox>
    <asp:Button ID="conv" runat="server" Text="Convert" style="float: right;" 
        onclick="conv_Click"/>
    <asp:TextBox ID="output" runat="server" Height="400px" Width="100%" 
        ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
</asp:Content>
