<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUploadControl.ascx.cs" Inherits="FileUploadControl" %>



<asp:Panel ID="pnlParent" runat="server" Width="350px" BorderColor="Black" BorderWidth="1px" BorderStyle="Dashed" style="border-color:#c2c2c2">

    <asp:Panel ID="pnlFiles" runat="server" Width="350px"  HorizontalAlign="Left">
        <asp:FileUpload ID="IpFile" runat="server" onchange="javascript: Add();" />
    </asp:Panel>

    <asp:Panel ID="pnlListBox" runat="server" Width="350px" BorderStyle="None">
    </asp:Panel>

    <asp:Panel ID="pnlButton" runat="server" Width="400px" HorizontalAlign="Right" style="display:none">
        <input id="btnAdd" onclick="javascript: Add();" style="width: 60px" type="button" runat="server" value="Add" />
        <input id="btnClear" onclick="javascript: Clear();" style="width: 60px" type="button" value="Clear" runat="server" />
        <asp:Button ID="btnUpload" OnClientClick="javascript:return DisableTop();" runat="server" Text="Upload" Width="60px"
            OnClick="btnUpload_Click" />
        <br />

        <asp:Label ID="lblCaption" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="Gray">
        </asp:Label>

    </asp:Panel>

</asp:Panel>
