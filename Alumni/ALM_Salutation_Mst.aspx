<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Salutation_Mst.aspx.cs" Inherits="Alumni_ALM_Salutation_Mst" %>

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
                                        <td colspan="3" class="tableheading">Salutation Master
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="tdgap"></td>
                                    </tr>
                                    <tr>
                                        <td id="lblSalutationName" style="width: 15%" class="vtext">Salutation Name</td>
                                        <td class="colon">:
                                        </td>
                                        <td class="required">
                                            <Anthem:TextBox ID="R_txtSalutationName" runat="server" SkinID="textbox" MaxLength="5"
                                                AutoUpdateAfterCallBack="True" ondrag="return false" ondrop="return false" onpaste="return false"
                                                oncut="return false" />
                                            *
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="lblRemarks" style="width: 15%" class="vtext">Remarks
                                        </td>
                                        <td class="colon">:
                                        </td>
                                        <td class="required">
                                            <Anthem:TextBox ID="txtRemarks" runat="server" SkinID="textbox" MaxLength="249" ondrag="return false"
                                                ondrop="return false" onpaste="return false" oncut="return false" onkeypress="return CheckMaxLength(this,254);"
                                                AutoUpdateAfterCallBack="True" TextMode="MultiLine" />
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
                                        <td colspan="3" class="tablesubheading">List of Salutations
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
                                                        <asp:TemplateField HeaderText="Salutation Name">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="feddtl" runat="server" Text='<%# Bind("Salutation_Name") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="feddtlr" runat="server" Text='<%# Bind("Remarks") %>'></asp:Literal>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EDIT">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkEdit" CausesValidation="false" CommandArgument='<%# Eval("PK_Salutation_ID") %>'
                                                                    CommandName="SELECT" TextDuringCallBack="WAIT...">
                                                                    <img src="../Images/Edit.gif" alt="" border="0">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DELETE">
                                                            <ItemTemplate>
                                                                <Anthem:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" TextDuringCallBack="WAIT..." CommandArgument='<%# Eval("PK_Salutation_ID") %>'
                                                                    CommandName="DELETEREC" PreCallBackFunction="btnDelete_PreCallBack">
                                                                     <img src="../Images/Delete.gif" alt="" border="0" >
                                                                </Anthem:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
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