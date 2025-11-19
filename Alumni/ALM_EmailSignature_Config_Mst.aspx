<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_EmailSignature_Config_Mst.aspx.cs" Inherits="Alumni_ALM_EmailSignature_Config_Mst" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />

    <script type="text/javascript">

        function clearCKEditor() {
            debugger;
            var sub = document.getElementById("ctl00_ContentPlaceHolder1_txtSenderEmail");
            var desc = document.getElementById("ctl00_ContentPlaceHolder1_txtSigns");
            CKEDITOR.instances[desc].setData('');
            document.getElementById("ctl00_ContentPlaceHolder1_txtSenderEmail").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_txtSigns").value = "";
        }

        function fillCKEditor() {
            debugger;
            //alert('HI');
            var desc = document.getElementById("ctl00_ContentPlaceHolder1_txtSigns").value;
            const dataToFill = desc;
            CKEDITOR.instances['<%= txtSigns.ClientID %>'].setData(dataToFill);
        }

    </script>
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" class="table" width="100%">
        <tr>
            <td colspan="6" class="tableheading">E-Mail Signature Configuration </td>
        </tr>
        <tr>
            <td colspan="6" class="tdgap"></td>
        </tr>
        <tr>
            <td class="vtext">Sender Email </td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:TextBox ID="txtSenderEmail" runat="server" SkinID="textboxlong" AutoUpdateAfterCallBack="true" TabIndex="1"></Anthem:TextBox>*
            </td>
            <td class="vtext">Receive Replies On </td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:TextBox ID="txtReceiveReplyEmail" runat="server" SkinID="textboxlong" AutoUpdateAfterCallBack="true" TabIndex="2"></Anthem:TextBox>*
            </td>
        </tr>
        <tr>
            <td class="vtext">Email Signature </td>
            <td class="colon">: </td>
            <td class="required" style="width: 100%; vertical-align: top;" colspan="4">
                <Anthem:Panel ID="pnl" runat="server" AutoUpdateAfterCallBack="true">
                    <CKEditor:CKEditorControl ID="txtSigns" runat="server" onpaste="return false;" />* 
                </Anthem:Panel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td colspan="6">
                <Anthem:Button ID="btnSaveSign" runat="server" AutoUpdateAfterCallBack="true" Text="SAVE" OnClick="btnSaveSign_Click" EnableCallBack="false" CommandName="SAVE" />
                <Anthem:Button ID="btnReset" runat="server" Text="RESET" OnClick="btnReset_Click" OnClientClick="clearCKEditor();" AutoUpdateAfterCallBack="true" EnableCallBack="false" />
                <Anthem:Label ID="lblMsg" runat="server" ForeColor="Red" autocallback="true" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6"></td>
        </tr>
        <tr>
            <td id="Td2" colspan="6" class="tablesubheading" runat="server">Lists of E-mail Signs </td>
        </tr>
        <tr>
            <td colspan="6">
                <Anthem:GridView ID="gvEmailSigns" runat="server" AutoGenerateColumns="False" Width="100%" AutoUpdateAfterCallBack="true"
                    PageSize="10" AllowPaging="true" OnRowCommand="gvEmailSigns_RowCommand" OnPageIndexChanging="gvEmailSigns_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sender Email">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblSenderEmail" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("SenderEmail") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receive Replies On">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblReceiveReplyEmail" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("ReceiveReplyEmail") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Signature">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblMailSignature" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("EmailSigns") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="EDIT">
                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                            <ItemTemplate>
                                <Anthem:LinkButton ID="lnkbtnEdit" TextDuringCallBack="WAIT.." runat="server" EnableCallBack="false" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="EDITREC" CommandArgument='<%# Eval("pk_AEmailSignId") %>'> 
                                    <img src="../../Images/Edit.gif" alt="" border="0"></img>
                                </Anthem:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DELETE">
                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                            <ItemTemplate>
                                <Anthem:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Are you sure to delete this record?');" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="DELETEREC" CommandArgument='<%# Eval("pk_AEmailSignId") %>'> <img src="../../Images/Delete.gif" alt="" border="0"></img></Anthem:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </Anthem:GridView>
            </td>
        </tr>
    </table>

</asp:Content>