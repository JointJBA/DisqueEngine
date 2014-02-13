<%@ Page Title="" Language="C#" MasterPageFile="~/RayMaster.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="WebRenderToy.Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder2" runat="server">
    <h5>How to use the raytracer?</h5>
    <p>The raytracer is divided into two libraries: the raytracer and the parser. In the 
        raytracer library everything happens, but it cannot read directly from xml 
        without the parser library. In order for the raytracer to work there are some 
        elements that must be present in the xml data. If any error happens please 
        report it in the <a href="Report.aspx">Report</a> section.</p>
    <p>Format:</p>
    <textarea id="code">&lt;World Tracer=&quot;[Tracer]&quot;&gt;
  &lt;World.ViewPlane Width=&quot;[Width]&quot; Height=&quot;[Height]&quot; Samples=&quot;[Samples]&quot; PixelSize=&quot;[PixelSize]&quot; MaxDepth=&quot;[Max Depth]&quot; Sampler=&quot;[Sampler]&quot;/&gt;
  &lt;World.AmbientLight&gt;
    &lt;[AmbientLight]/&gt;
  &lt;/World.AmbientLight&gt;
  &lt;World.Camera&gt;
    &lt;[Camera]/&gt;
  &lt;/World.Camera&gt;
  &lt;Lights&gt;
    &lt;[Light]/&gt;
  &lt;/Lights&gt;
  &lt;!-- Optional --&gt;
  &lt;Textures&gt;
    &lt;[Texture]/&gt;
  &lt;/Textures&gt;
  &lt;!-- Optional ends here --&gt;
  &lt;Objects&gt;
    &lt;[Object]&gt;
      &lt;[Object].Material&gt;
        &lt;[Material/&gt;
      &lt;/[Object].Material&gt;
    &lt;/[Object]&gt;
  &lt;/Objects&gt;
&lt;/World&gt;</textarea>
    <script type="text/javascript">
        editor = CodeMirror.fromTextArea(document.getElementById("code"), {
            mode: { name: "xml", alignCDATA: true },
            lineNumbers: true, readOnly: true
        });
        editor.setSize(800, 300);
    </script>
    <asp:Panel ID="tablePanel" runat="server">
    </asp:Panel>
    </asp:Content>
