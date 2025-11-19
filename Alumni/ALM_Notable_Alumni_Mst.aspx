<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Notable_Alumni_Mst.aspx.cs" Inherits="Alumni_ALM_Notable_Alumni_Mst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form" style="width: 100%;">
                        <tr>
                            <td colspan="7" class="tableheading">Manage Notable Alumni </td>
                        </tr>
                        <tr>
                            <td class="vtext">Name </td>
                            <td class="colon" style="width: 2%">: </td>
                            <td class="required" style="width: 10%; margin-left: 640px;">
                                <Anthem:TextBox ID="txtName" runat="server" MaxLength="100" SkinID="textboxlong" AutoUpdateAfterCallBack="true" TabIndex="1" onkeypress="return CheckMaxLength(this, 100);" onkeyup="return CheckMaxLength(this, 100);" onpaste="return CheckMaxLength(this, 100);" ondrop="return CheckMaxLength(this, 100);"></Anthem:TextBox>*
                            </td>
                            <td class="vtext" style="width: 15%">Subheading </td>
                            <td class="colon" valign="top">: </td>
                            <td class="required" style="width: 15%; margin-left: 40px;">
                                <Anthem:TextBox ID="txtSubheading" runat="server" MaxLength="200" SkinID="textboxlong" TextMode="SingleLine" AutoUpdateAfterCallBack="true" TabIndex="2" onkeypress="return CheckMaxLength(this, 200);" onkeyup="return CheckMaxLength(this, 200);" onpaste="return CheckMaxLength(this, 200);" ondrop="return CheckMaxLength(this, 200);"></Anthem:TextBox>*
                            </td>
                        </tr>
                        <tr>
                            <td class="vtext" style="width: 15%">Upload Photo </td>
                            <td class="colon" valign="top">:</td>
                            <td class="required" style="width: 10%">
                                <Anthem:FileUpload ID="flUploadPhoto" AutoUpdateAfterCallBack="true" runat="server" TabIndex="8" />
                                &nbsp;
                             <span style="font-weight: normal">File size shouldn’t be greater than 2 MB
                                 <br />
                                 format type. as in (PNG, JPEG, JPG)
                             </span>
                                <Anthem:LinkButton runat="server" ID="lnkViewPhoto" CausesValidation="False" AutoUpdateAfterCallBack="True" OnClick="lnkViewPhoto_Click"> </Anthem:LinkButton>
                            </td>
                            <td class="vtext" style="width: 15%">Comments </td>
                            <td class="colon" valign="top">: </td>
                            <td class="required" style="width: 15%; margin-left: 40px;">
                                <Anthem:TextBox ID="txtComments" runat="server" MaxLength="50" SkinID="textboxmultiline" TextMode="MultiLine" AutoUpdateAfterCallBack="true" Height="90px" Width="250px" TabIndex="2" onkeypress="return CheckMaxLength(this, 50);" onkeyup="return CheckMaxLength(this, 50);" onpaste="return CheckMaxLength(this, 50);" ondrop="return CheckMaxLength(this, 50);"></Anthem:TextBox>*
                            </td>
                        </tr>
                        <tr>
                            <td class="vtext">IsActive </td>
                            <td class="colon" valign="top">: </td>
                            <td>
                                <Anthem:CheckBox ID="chkActive" runat="server" AutoUpdateAfterCallBack="True" AutoCallBack="true" TabIndex="9" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: center">
                                <Anthem:Button ID="btnSave" runat="server" AutoUpdateAfterCallBack="true" CssClass="btn btn-primary btn-sm" EnableCallBack="false"
                                    CommandName="SAVE" TextDuringCallBack="SAVING..." OnClick="btnSave_Click" Text="SAVE" />
                                <Anthem:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="btn btn-default btn-sm" Text="RESET" TextDuringCallBack="RESETING.." AutoUpdateAfterCallBack="True" OnClick="btnReset_Click" EnableCallBack="false" />
                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True" />
                            </td>
                        </tr>
                    </table>
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="7" class="tablesubheading">Lists of Notable Alumni </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvNotableAlumni" runat="server" AllowPaging="false" AutoGenerateColumns="False" DataKeyNames="PK_NAID"
                                        AutoUpdateAfterCallBack="True" Width="100%" OnRowCommand="gvNotableAlumni_RowCommand" UpdateAfterCallBack="True">
                                        <Columns>
                                            <%--<asp:BoundField DataField="Name" HeaderText="Name" />                                           
                                            <asp:BoundField DataField="Subheading" HeaderText="Subheading" />
                                            <asp:HyperLinkField DataTextField="Link" HeaderText="Link" DataNavigateUrlFields="Link" />--%>

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblName" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Name") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub-heading">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblSubheading" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Description") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comments">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblComments" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Comments") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Photo">
                                                <ItemTemplate>
                                                    <img src='<%# Eval("PhotoPath") %>' alt="Alumni Photo" width="50" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="EDIT">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton ID="lnkEdit" TextDuringCallBack="WAIT.." runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="EDITREC" CommandArgument='<%# Eval("PK_NAID") %>'> <img src="../Images/Edit.gif" alt="" border="0"></img></Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DELETE">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton ID="lnkDelete" OnClientClick="return confirm('Are you sure to delete this record?');" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="DELETEREC" CommandArgument='<%# Eval("PK_NAID") %>'> <img src="../Images/Delete.gif" alt="" border="0"></img></Anthem:LinkButton>
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