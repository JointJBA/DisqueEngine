<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Samples.aspx.cs" Inherits="WebRenderToy.Samples" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="XmlDataSource1">
        <HeaderTemplate>
            <br />
        </HeaderTemplate>
        <ItemTemplate>
            <br />
            <img src="<%# XPath("Img") %>" />
            <textarea readonly="readonly" style="width: 795px; height: 350px">
    <%# XPath("Code") %>
    </textarea>
            <br />
        </ItemTemplate>
        <FooterTemplate>
            <br />
        </FooterTemplate>
    </asp:Repeater>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Scripts/SamplesData.xml">
    </asp:XmlDataSource>
</asp:Content>
