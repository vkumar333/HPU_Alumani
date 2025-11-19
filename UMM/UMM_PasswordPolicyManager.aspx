<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="UMM_PasswordPolicyManager.aspx.cs" Inherits="UMM_UMM_PasswordPolicyManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" class="table" width="100%">
<tr>
<td colspan="2" class="tableheading">
    Configure Password Policies</td>
</tr>
<tr>
<td style="width:45%;" id="Length">
    Min. Password Length</td>
<td class="required">
    <Anthem:TextBox ID="R_txtLength" runat="server" AutoUpdateAfterCallBack="True" 
        MaxLength="1" Width="18px"></Anthem:TextBox>
</td>
</tr>
<tr>
<td>
     Password should have Upper Case Characters?</td>
<td class="required">
    <Anthem:RadioButton ID="rdoUpperYes" runat="server" GroupName="Upper" 
        Text="Yes" AutoUpdateAfterCallBack="True" Checked="True" />
    <Anthem:RadioButton ID="rdoUpperNo" runat="server" GroupName="Upper" Text="No" 
        AutoUpdateAfterCallBack="True" />
</td>
</tr>
<tr>
<td>
   Password should have Numeric Values?</td>
<td class="required">
   <Anthem:RadioButton ID="rdoNumericYes" runat="server" GroupName="Numeric" 
        Text="Yes" AutoUpdateAfterCallBack="True" Checked="True" />
    <Anthem:RadioButton ID="rdoNumericNo" runat="server" GroupName="Numeric" Text="No" 
        AutoUpdateAfterCallBack="True" />
</td>
</tr>
<tr>
<td>
    Password should have Special Characters?</td>
<td class="required">
     <Anthem:RadioButton ID="rdoSpecialYes" runat="server" GroupName="special" 
        Text="Yes" AutoUpdateAfterCallBack="True" Checked="True" />
    <Anthem:RadioButton ID="rdoSpecialNo" runat="server" GroupName="special" Text="No" 
        AutoUpdateAfterCallBack="True" />
</td>
</tr>
<tr>
<td>
    Password can be equal to UserId?</td>
<td class="required">
    <Anthem:RadioButton ID="rdo_equalYes" runat="server" GroupName="equal" 
        Text="Yes" AutoUpdateAfterCallBack="True" Checked="True" />
    <Anthem:RadioButton ID="rdo_equalNo" runat="server" GroupName="equal" Text="No" 
        AutoUpdateAfterCallBack="True" />
</td>
</tr>
<tr>
<td colspan="2">
    <Anthem:Button ID="btnSave" runat="server" TextDuringCallBack="SAVING.." Text="SAVE"  PreCallBackFunction="btnSave_PreCallBack"
        AutoUpdateAfterCallBack="True" onclick="btnSave_Click" />
</td>
</tr>
<tr>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
</table>
</asp:Content>

