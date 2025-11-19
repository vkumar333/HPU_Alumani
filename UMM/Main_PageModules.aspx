<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="Main_PageModules.aspx.cs" Inherits="UMM_Main_PageModules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">

        <div class="row">
            <div class="box box-success">

                <div class="box-body table-responsive">

                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td class="" colspan="3"></td>
                        </tr>
                        <tr>
                            <td align="right" colspan="3">
                                <%--<asp:Label ID="lblcurrentfinyear" runat="server" Font-Bold="true" CssClass="lblmessagesmall"></asp:Label>--%>
                            </td>

                        </tr>
                        <tr>
                            <td class="vtext" align="left" width="20%" id="lblIncomeHead">Module Name</td>
                            <td class="colon">:
                            </td>
                            <td class="required" align="left">
                                <Anthem:DropDownList ID="ddlmodule" runat="server" AutoUpdateAfterCallBack="true" CssClass="textboxmedium"></Anthem:DropDownList>
                                *
                            </td>
                            <td class="vtext" align="left" width="20%" id="lblIncomeHead1">Module Type</td>
                            <td class="colon">:
                            </td>
                            <td class="required" align="left">
                                <Anthem:DropDownList ID="ddlmtype" runat="server" AutoUpdateAfterCallBack="true" CssClass="textboxmedium">
                                    <asp:ListItem Text="Select Module Type" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Staff" Value="2"></asp:ListItem>
                                </Anthem:DropDownList>
                                *
                            </td>

                        </tr>
                        <tr>
                            <td class="vtext" align="left" id="lblDescription">Module Path</td>
                            <td class="colon">:
                            </td>
                            <td align="left" class="required">
                                <Anthem:TextBox ID="txtpath" runat="server" AutoUpdateAfterCallBack="True" CssClass="textboxmultiline" MaxLength="255"></Anthem:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="vtext">Module Image</td>
                            <td class="colon">:</td>
                            <td align="left" class="required">
                                <Anthem:FileUpload ID="flUpload" runat="server" AutoUpdateAfterCallBack="True" />
                                <Anthem:Label ID="lblPath" runat="server" AutoUpdateAfterCallBack="true" />
                                <Anthem:LinkButton ID="lnkImage" runat="server" CausesValidation="False"
                                    EnableCallBack="false" />
                                <Anthem:HiddenField ID="hflnkupload" runat="server" AutoUpdateAfterCallBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td class="colon"></td>
                            <td align="left" class="required">
                                <Anthem:Button ID="btnSave" runat="server" CommandName="SAVE" CssClass="btn btn-primary btn-sm" Text="SAVE"
                                    PreCallBackFunction='btnSave_PreCallBack' AutoUpdateAfterCallBack="True" EnableCallBack="false"
                                    UpdateAfterCallBack="True" OnClick="btnSave_Click" />
                                <Anthem:Button ID="btnReset" runat="server"
                                    CssClass="btn btn-default btn-sm" Text="RESET" CommandName="RESET" OnClick="btnReset_Click" EnableCallBack="false" />
                                <Anthem:Label ID="lblMsg" runat="server" CssClass="lblmessage" AutoUpdateAfterCallBack="True"
                                    UpdateAfterCallBack="True" meta:resourcekey="lblMsgResource1"></Anthem:Label>
                            </td>
                        </tr>


                    </table>
                </div>

                <div class="box-body" style="padding-bottom: 0;">
                    <table class="table mobile_form" width="100%" style="margin: 0;">
                        <tr>

                            <%--<td class="tablesubheading" align="left">List of Income Head</td>--%>
                        </tr>


                    </table>
                </div>

                <div class="box-body table-responsive">
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td>
                                <div class="gridiv">

                                    <Anthem:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                        Width="100%" meta:resourcekey="gvResource1" PageSize="5" OnPageIndexChanging="gv_PageIndexChanging" OnRowCommand="gv_RowCommand">
                                        <RowStyle CssClass="gridrowstyle" />
                                        <EditRowStyle CssClass="grideditrowstyle" />
                                        <SelectedRowStyle CssClass="gridselectedrowstyle" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="gridheaderstyle" />
                                        <AlternatingRowStyle CssClass="gridalternativerowstyle" />
                                        <FooterStyle CssClass="gridfooterstyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="gridpagerstyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." meta:resourcekey="TemplateFieldResource1" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />

                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Module_name" HeaderText="Module Name">
                                                <HeaderStyle Width="20%" />
                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="module_path" HeaderText="Module Path">
                                                <HeaderStyle Width="30%" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="moduletype" HeaderText="Module Type">
                                                <HeaderStyle Width="20%" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:ImageField DataImageUrlField="moduleimage" HeaderText="Module Image">
                                                <HeaderStyle Width="20%" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:ImageField>


                                            <asp:TemplateField HeaderText="EDIT" meta:resourcekey="TemplateFieldResource2">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" CommandName="EDITREC" CommandArgument='<%# Eval("pk_id") %>'
                                                        ID="lnkEdit">
                            <img src="../../images/edit.gif" alt="" border="0" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DELETE" meta:resourcekey="TemplateFieldResource3">
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" PreCallBackFunction="btnDelete_PreCallBack" TextDuringCallBack="Wait.."
                                                        AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" CommandName="DELETEREC"
                                                        CommandArgument='<%# Eval("pk_id") %>' CausesValidation="False"
                                                        ID="lnkDelete">
                                                        <img  src ="../../Images/Delete.gif" alt ="" border ="0" /></Anthem:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </Anthem:GridView>


                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

