<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_AlumniSuggestion_Report.aspx.cs" Inherits="Alumni_ALM_AlumniSuggestion_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" style="width: 100%">
        <tr>
            <td colspan="6" class="tableheading">Alumni Suggestion Report </td>
        </tr>
        <tr>
            <td class="vtext" id="lbl_ins_date">From Date</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:TextBox ID="From_Date" runat="server" CssClass="textbox"
                    SkinID="textboxdate" placeholder="DD/MM/YYYY"
                    AutoUpdateAfterCallBack="True" MaxLength="10" onkeypress="return false"
                    onpaste="return false;" ondrop="return false;" AutoCallBack="false" />
                <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00$ContentPlaceHolder1$From_Date);return false;">
                    <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                        width="34" /></a>
            </td>
            <td class="vtext" id="lbl_ins_due_date">To Date</td>
            <td class="colon">:</td>
            <td class="required">
                <Anthem:TextBox ID="To_Date" runat="server" CssClass="textbox"
                    SkinID="textboxdate" placeholder="DD/MM/YYYY"
                    AutoUpdateAfterCallBack="True" MaxLength="10" onkeypress="return false"
                    onpaste="return false;" ondrop="return false;" AutoCallBack="false" />
                <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00$ContentPlaceHolder1$To_Date);return false;">
                    <img align="absMiddle" alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                        width="34" /></a><span style="color: red"></span>*
            </td>
        </tr>

        <tr>
            <td></td>
            <td></td>
            <td colspan="4">
                <Anthem:Button ID="btn_Suggestion" AutoUpdateAfterCallBack="true" UpdateAfterCallBack="true" runat="server" Text="VIEW"
                    OnClick="btn_Suggestion_Click" TextDuringCallBack="Viewing..." CssClass="btn btn-primary btn-sm" />
                <Anthem:Button ID="btnReset" AutoUpdateAfterCallBack="true" runat="server" Text="RESET" OnClick="btnReset_Click"
                    TextDuringCallBack="Wait..." CssClass="btn btn-primary btn-sm" />
                <Anthem:Label ID="Label1" AutoUpdateAfterCallBack="true" UpdateAfterCallBack="true" runat="server" SkinID="lblmessage"></Anthem:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" class="tdgap"></td>
        </tr>
        <tr>
            <td colspan="6" class="tableheading">
                <Anthem:Label ID="lblSuggestion" AutoUpdateAfterCallBack="true" UpdateAfterCallBack="true" runat="server"></Anthem:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" class="tdgap"></td>
        </tr>
        <tr>
            <td colspan="6">
                <div class="gridiv">
                    <Anthem:GridView ID="dgv" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageIndex="0" PageSize="10"
                        Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Alumni Name">
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAlumniname" runat="server" Text='<%# Eval("Alumni_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Suggestion">
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSuggestion" runat="server" Text='<%# Eval("Suggestion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Suggestion Date">
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSuggestiondate" runat="server" Text='<%# Eval("Suggestiondate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </Anthem:GridView>
                </div>

            </td>
        </tr>
        <tr>
            <td align="left" colspan="6">
                <Anthem:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" AutoUpdateAfterCallBack="true" Enabled="false" CommandName="ExportToExcel" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary btn-sm" EnableCallBack="false" />
                <Anthem:Label ID="lblMsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </td>
        </tr>
    </table>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js" class="findcal"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>