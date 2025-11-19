<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Publish_JobCategory.aspx.cs" Inherits="Alumni_ALM_Publish_JobCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box box-success">
                    <!--Main Table-->
                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%" align="left" class="table">
                        <tr>
                            <td align="left" valign="top">
                                <!--Sub Main Table-->
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="3" class="tableheading">Job Category
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="tdgap"></td>
                                    </tr>
                                    <tr>
                                        <td id="lblJobName" style="width: 15%" class="vtext">Category </td>
                                        <td class="colon">:
                                        </td>
                                        <td class="required">
                                            <Anthem:TextBox ID="R_txtJobName" runat="server" SkinID="textbox" MaxLength="50"
                                                AutoUpdateAfterCallBack="True" ondrag="return false" ondrop="return false" onpaste="return false"
                                                oncut="return false" />
                                            *
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="lblisactive" style="width: 15%" class="vtext">Is Active
                                        </td>
                                        <td class="colon">:
                                        </td>
                                        <td class="required">
                                            <Anthem:CheckBox ID="Chk_IsActive" ForeColor="Black" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="tdgap"></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <Anthem:Button ID="btnSave" runat="server" Text="SAVE" CommandName="SAVE" OnClick="btnSave_Click"
                                                            AutoUpdateAfterCallBack="true" PreCallBackFunction="btnSave_PreCallBack" TextDuringCallBack="WAIT..." CssClass="btn btn-primary btn-sm" />
                                                    </td>
                                                    <td class="btngap"></td>
                                                    <td>
                                                        <Anthem:Button ID="btnReset" runat="server" Text="RESET" CausesValidation="False"
                                                            OnClick="btnReset_Click" AutoUpdateAfterCallBack="true" TextDuringCallBack="WAIT..." CssClass="btn btn-default btn-sm" />
                                                    </td>
                                                    <td class="btngap"></td>
                                                    <td>
                                                        <Anthem:Label ID="lblMsg" runat="server" SkinID="lblmessage" AutoUpdateAfterCallBack="True" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="tdgap"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="tablesubheading">Lists of Job Category
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="tdgap"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div class="gridiv">
                                                <Anthem:GridView ID="gvDetails" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                                    AutoUpdateAfterCallBack="True" OnRowCommand="gvDetails_RowCommand"
                                                    UpdateAfterCallBack="True" OnPageIndexChanging="gvDetails_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category">
                                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LBl_isactive" runat="server" Text='<%# Bind("IsActive") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EDIT">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkEdit" CausesValidation="false" CommandArgument='<%# Eval("Pk_JobCId") %>'
                                                                    CommandName="SELECT" TextDuringCallBack="WAIT...">
                                                                    <img src="../Images/Edit.gif" alt="" border="0">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DELETE">
                                                            <ItemTemplate>
                                                                <Anthem:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" TextDuringCallBack="WAIT..." CommandArgument='<%# Eval("Pk_JobCId") %>' CommandName="DELETEREC" PreCallBackFunction="btnDelete_PreCallBack">
                                                                     <img src="../Images/Delete.gif" alt="" border="0" >
                                                                </Anthem:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </Anthem:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <!--Sub Main Table End-->
                            </td>
                        </tr>
                    </table>
                    <!--Main Table End-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>