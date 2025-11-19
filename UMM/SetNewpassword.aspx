<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="SetNewpassword.aspx.cs" Inherits="UMM_SetNewpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" class="secContent" style="width: 100%;">
        <tr>
            <td align="center" class="tableheading" colspan="3">
                Set New Password
            </td>
        </tr>
        <tr>
            <td class="tdgap" colspan="3">
            </td>
        </tr>
        <tr>
            <td id="lblOldPassword" align="left" class="vtext">
                User Name
            </td>
            <td class="colon" align="left">
                :</td>
            <td class="required" align="left">
                <Anthem:TextBox ID="R_txtusername" AutoUpdateAfterCallBack="true" runat="server"
                    CssClass="textbox"></Anthem:TextBox>*
            </td>
        </tr>
        <tr>
            <td id="lblPwd" align="left" class="vtext">
                New Password
            </td>
            <td align="left" class="colon">
                :</td>
            <td align="left" class="required">
                <Anthem:TextBox ID="R_txtPwd" runat="server" AutoUpdateAfterCallBack="true" CssClass="textbox"
                    TextMode="Password"></Anthem:TextBox>*
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <Anthem:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                    Text="Change Password" Width="157px" AutoUpdateAfterCallBack="true" TextDuringCallBack="WAIT..." PreCallBackFunction="btnSave_PreCallBack" />
                <Anthem:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click"
                    AutoUpdateAfterCallBack="true" />
            </td>
        </tr>
    </table>
</asp:Content>


