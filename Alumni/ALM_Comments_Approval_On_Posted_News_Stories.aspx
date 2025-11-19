<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Comments_Approval_On_Posted_News_Stories.aspx.cs" Inherits="Alumni_ALM_Comments_Approval_On_Posted_News_Stories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function CheckAll(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("ctl00_ContentPlaceHolder1_dgv");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

        function UnCheckOrCheck(childCheckBox) {
            var countCheck = 0;
            var GridVwHeaderChckbox = document.getElementById("ctl00_ContentPlaceHolder1_dgv");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                if (GridVwHeaderChckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked == false) {
                    GridVwHeaderChckbox.rows[0].cells[5].getElementsByTagName("INPUT")[0].checked = false;
                    return;
                }
                else {
                    if (GridVwHeaderChckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked == true) {
                        countCheck++;
                    }
                }
            }
            var length = GridVwHeaderChckbox.rows.length;
            if ((length - 1) == (countCheck)) {
                GridVwHeaderChckbox.rows[0].cells[5].getElementsByTagName("INPUT")[0].checked = true;
            }
        }

    </script>

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table" border="0" style="width: 100%">

                        <tr>
                            <td colspan="6" class="tableheading">List of Comments Posted On News Stories Approval Pending </td>
                        </tr>

                        <tr>
                            <td align="center" colspan="6">
                                <div style="overflow: scroll; height: 200px;">
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

                                                <asp:TemplateField HeaderText="News & Stories" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommentedby" runat="server" Text='<%# Eval("Heading") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Commented By" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEventname" runat="server" Text='<%# Eval("commentedby") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Comments" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComment" runat="server" Text='<%# Eval("comment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Commented Date" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommenteddate" runat="server" Text='<%# Eval("commentdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Select All <Anthem:CheckBox ID="chkSelectAll" runat="server" onclick="CheckAll(this)" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <Anthem:CheckBox runat="server" ID="chkselect" AutoUpdateAfterCallBack="true" Checked="false" onclick="UnCheckOrCheck(this)" />
                                                        <Anthem:Label ID="lblid" runat="Server" AutoUpdateAfterCallBack="True" Visible="false" Text='<%# Eval("pk_CommentID") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </Anthem:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <Anthem:Button ID="btnprocess" runat="server" AutoUpdateAfterCallBack="true" Text="PROCESSED"
                                    TextDuringCallBack="Wait..." EnabledDuringCallBack="false" OnClick="btnprocess_Click" />&nbsp;

                <Anthem:Button ID="btnReset" runat="server" AutoUpdateAfterCallBack="true" Text="RESET"
                    TextDuringCallBack="Wait..." EnabledDuringCallBack="false" OnClick="btnReset_Click" />

                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblMsg"
                                    Text=""></Anthem:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box-body table-responsive">
                    <table class="table">
                        <tr>
                            <td colspan="6" class="tableheading">List of Approved Comments Posted On News Stories </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <div style="overflow: scroll; height: 200px;">
                                    <div class="gridiv">
                                        <Anthem:GridView ID="gdapproval" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageIndex="0" PageSize="10"
                                            Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="News & Stories" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommentedby" runat="server" Text='<%# Eval("Heading") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Commented By" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEventname" runat="server" Text='<%# Eval("commentedby") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Comments" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComment" runat="server" Text='<%# Eval("comment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Commented Date" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommenteddate" runat="server" Text='<%# Eval("commentdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </Anthem:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>