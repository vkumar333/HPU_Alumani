<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="mysettings.aspx.cs" Inherits="UMM_mysettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading1">
        <a onclick="$('span#absdot1').toggle(); $(this).toggleClass('abstract'); $(this).toggleClass('abstract-close'); $('div#abs1').slideToggle(400); return false;"
            class="abstract" href="javascript:;"><strong>click</strong></a> My Smtp Settings</div>
    <div id="abs1" class="box" style="display: block;">
        <table border="0" class="table" width="100%">
            <tr>
                <td id="lblSmtp" class="vtext" style="width: 20%">
                    Smtp
                </td>
                <td class="required">
                    <Anthem:DropDownList ID="D_ddlSmtp" runat="server" AutoUpdateAfterCallBack="True"
                        SkinID="dropdown">
                    </Anthem:DropDownList>
                </td>
            </tr>
            <tr>
                <td id="lblEmail" class="vtext">
                    Email Address
                </td>
                <td class="required">
                    <Anthem:TextBox ID="E_txtEmail" runat="server" AutoUpdateAfterCallBack="True"
                        MaxLength="100" SkinID="textbox"></Anthem:TextBox>
                </td>
            </tr>
            <tr>
                <td id="lblPassword" class="vtext">
                    Password
                </td>
                <td class="required">
                    <Anthem:TextBox ID="R_txtPassword" runat="server" AutoUpdateAfterCallBack="True"
                        MaxLength="30" TextMode="Password" SkinID="textbox"></Anthem:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" class="btnarea">
                    <Anthem:Button AutoUpdateAfterCallBack="True" ID="btnSave" runat="server" CommandName="UPDATE"
                        OnClick="btnSave_Click" Text="UPDATE" TextDuringCallBack="UPDATING..." PreCallBackFunction="btnSave_PreCallBack" />
                    <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

