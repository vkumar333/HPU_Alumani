<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_RegisrationCategoryWise_Fee_Mst.aspx.cs" Inherits="Alumni_ALM_RegisrationCategoryWise_Fee_Mst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../include/jquery.min.js"></script>
    <script type="text/javascript">
    </script>

    <div>
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="6" class="tableheading">Alumni Categories-Wise Fee Master </td>
                        </tr>
                        <tr>
                            <td id="lblCategory" class="vtext">Category </td>
                            <td class="colon">: </td>
                            <td class="required">
                                <Anthem:TextBox ID="txtCategory" runat="server" AutoUpdateAfterCallBack="true" MaxLength="5" onkeypress="return AllowAlphabet(event)" AutoCallBack="false" EnableCallBack="false">
                                </Anthem:TextBox><span style="color: red">*</span>
                            </td>
                            <td id="lblFeeAmount" class="vtext">Fee Amount </td>
                            <td class="colon">: </td>
                            <td class="required">
                                <Anthem:TextBox ID="txtFee" runat="server" AutoUpdateAfterCallBack="true" MaxLength="5" onkeydown="return IntegerOnly(event,this);" AutoCallBack="false" EnableCallBack="false"></Anthem:TextBox><span style="color: red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td id="lblDescrition" class="vtext">Description </td>
                            <td class="colon">: </td>
                            <td class="required">
                                <Anthem:TextBox ID="txtDescrition" runat="server" AutoUpdateAfterCallBack="true" MaxLength="50" AutoCallBack="false" EnableCallBack="false"></Anthem:TextBox><span style="color: red">*</span>
                            </td>
                            <td class="vtext" id="lblIsActive" style="width: 12%">Is Active </td>
                            <td class="colon">: </td>
                            <td>
                                <Anthem:CheckBox ID="chkIsActive" runat="server" AutoUpdateAfterCallBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td colspan="4">
                                <Anthem:Button ID="btnSave" runat="server" AutoUpdateAfterCallBack="true" Text="Save" CommandName="SAVE" OnClick="btnSave_Click" />
                                <Anthem:Button ID="btnReset" runat="server" AutoUpdateAfterCallBack="true" Text="Reset" OnClick="btnReset_Click" />
                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True" />
                            </td>
                        </tr>
                    </table>

                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="6" class="tablesubheading">List of Alumni Categories-Wise Fee Master</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" EnableCallBack="false"
                                        AutoUpdateAfterCallBack="True" Width="100%" PageSize="10" OnPageIndexChanging="gvDetails_PageIndexChanging"
                                        OnRowCommand="gvDetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alumni Category">
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblCategory" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Category") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblDescription" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Descriptions") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fee Amount">
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblFee" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Fees") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Active">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblIsActive" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("IsActive") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EDIT">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton ID="lnkbtnEdit" TextDuringCallBack="WAIT.." runat="server" CommandName="EDITREC" CommandArgument='<%# Eval("pk_feeid") %>' AutoUpdateAfterCallBack="true"> <img src="../../Images/Edit.gif" alt="" border="0"></img></Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DELETE">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Are you sure to delete this record?');" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="DELETEREC" CommandArgument='<%# Eval("pk_feeid") %>'> <img src="../../Images/Delete.gif" alt="" border="0"></img></Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </Anthem:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="gap" colspan="6"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>