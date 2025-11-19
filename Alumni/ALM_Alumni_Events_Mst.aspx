<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Alumni_Events_Mst.aspx.cs" Inherits="Alumni_ALM_Alumni_Events_Mst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="7" class="tableheading">Upload Company Profile & Description</td>
                        </tr>
                        <tr>
                            <td id="lblContactPerson" class="vtext" style="width: 15%">Company Name/Description :</td>
                            <td class="required">

                                <Anthem:TextBox ID="R_txtCompanyDesc" runat="server" AutoUpdateAfterCallBack="True"
                                    MaxLength="100"
                                    SkinID="textbox" TextMode="SingleLine"></Anthem:TextBox>*
                            </td>

                            <td id="lblContactPersons" class="vtext" style="width: 15%">Upload Company Logo :</td>
                            <td class="required">
                                <Anthem:FileUpload ID="flUploadLogo" AutoUpdateAfterCallBack="true" runat="server" />
                                &nbsp;
                             <span style="font-weight: normal">File size shouldn’t be greater than 5 Mb
                          <br />
                             format type. as in (PNG, JPEG, JPG)</span>
                            <Anthem:LinkButton runat="server" ID="lnkviewBrc" CausesValidation="False"
                                    AutoUpdateAfterCallBack="True" OnClick="lnkviewBrc_Click"> </Anthem:LinkButton>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: center">
                                <%--<Anthem:Button AutoUpdateAfterCallBack="True" ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" CommandName="SAVE" Text="SAVE" TextDuringCallBack="SAVING..."
                                    PreCallBackFunction="btnSave_PreCallBack" OnClick="btnSave_Click" />--%>
                                <Anthem:Button ID="btnSave1" runat="server" AutoUpdateAfterCallBack="true" CssClass="btn btn-primary btn-sm"
                                    CommandName="SAVE" TextDuringCallBack="SAVING..." OnClick="btnSave1_Click" Text="SAVE" PreCallBackFunction="btnSave1_PreCallBack" />
                                <Anthem:Button ID="Reset" runat="server" CausesValidation="False" CssClass="btn btn-default btn-sm" Text="RESET" TextDuringCallBack="RESETING.." AutoUpdateAfterCallBack="True"
                                    OnClick="Reset_Click" />
                                <Anthem:Label ID="Label1" runat="server" AutoUpdateAfterCallBack="True"
                                    SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True" />
                            </td>
                        </tr>
                    </table>
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="7" class="tablesubheading">List of Uploded Details </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvUploadList" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" Width="100%"
                                        OnRowCommand="gvUploadList_RowCommand"
                                        UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Guest Category Id" Visible="false">
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblPk_companyProfileID" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("Pk_companyProfileID") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Description">
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblcompdesc" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Company_Desc") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Logo">
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblProfileCLogo" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Company_Logo") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EDIT">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton ID="lnkbtnEdit" TextDuringCallBack="WAIT.." runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="EDITREC" CommandArgument='<%# Eval("Pk_companyProfileID") %>'> <img src="../../Images/Edit.gif" alt="" border="0"></img></Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DELETE">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Are you sure to delete this record?');" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="DELETEREC" CommandArgument='<%# Eval("Pk_companyProfileID") %>'> <img src="../../Images/Delete.gif" alt="" border="0"></img></Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </Anthem:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td class="gap"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

