<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Alumni_Achivers_Notification.aspx.cs" Inherits="Alumni_ALM_Alumni_Achivers_Notification" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../include/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        //function checkal()
        //{
        //    $("#ctl00_ContentPlaceHolder1_gvAchievers_ctl01_chkSelectAll").on("click", function () {

        //        if ($(this).is(":checked")) {
        //            $("#ctl00_ContentPlaceHolder1_gvAchievers tr").not(":first").each(function () {
        //                $(this).children(':eq(4)').each(function () {

        //                    $(this).children().find('input:checkbox:first').prop('checked', true);
        //                });
        //            });
        //        }
        //        if ($(this).is(":checked") == false) {

        //            $("#ctl00_ContentPlaceHolder1_gvAchievers tr").not(":first").each(function () {
        //                $(this).children(':eq(4)').each(function () {

        //                    $(this).children().find('input:checkbox:first').prop('checked', false);
        //                });
        //            });
        //        }
        //    });
        //}

        //$(document).ready(BindEvents);
        //function BindEvents() {
        //    checkal();
        //}
        //function Anthem_PreCallBack() { BindEvents(); }
        //function Anthem_PostCallBack() { BindEvents(); }



        function CheckAll(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("ctl00_ContentPlaceHolder1_gvAchievers");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[4].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

        function UnCheckOrCheck(childCheckBox) {
            var countCheck = 0;
            var GridVwHeaderChckbox = document.getElementById("ctl00_ContentPlaceHolder1_gvAchievers");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                if (GridVwHeaderChckbox.rows[i].cells[4].getElementsByTagName("INPUT")[0].checked == false) {
                    GridVwHeaderChckbox.rows[0].cells[4].getElementsByTagName("INPUT")[0].checked = false;
                    return;
                }
                else {
                    if (GridVwHeaderChckbox.rows[i].cells[4].getElementsByTagName("INPUT")[0].checked == true) {
                        countCheck++;
                    }
                }
            }
            var length = GridVwHeaderChckbox.rows.length;
            if ((length - 1) == (countCheck)) {
                GridVwHeaderChckbox.rows[0].cells[4].getElementsByTagName("INPUT")[0].checked = true;
            }

        }


    </script>
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
    <table class="table" width="100%">
         <tr>
            <td class="tableheading">Alumni Achievers Notification
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
        </tr>
       

        <tr>
            <td class="tableheading">List Of Alumni Achievers
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: scroll; height: 200px;">
                     <div class="gridiv">
                    <Anthem:GridView ID="gvAchievers" runat="server" AutoUpdateAfterCallBack="True"
                        AutoGenerateColumns="False" UpdateAfterCallBack="True" Width="100%" EnableCallBack="False">
                        <Columns>

                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="Alumni Code" DataField="alumnicode" />--%>

                            <asp:BoundField HeaderText="Alumni Name" DataField="alumni_name" />
                            <asp:BoundField HeaderText="Registration No." DataField="alumnino" />
                            <asp:BoundField HeaderText="Acheivements" DataField="Achievement" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Select All<Anthem:CheckBox ID="chkSelectAll" runat="server"
                                        onclick="CheckAll(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <Anthem:CheckBox runat="server" ID="chkselect" AutoUpdateAfterCallBack="true" Checked="false" onclick="UnCheckOrCheck(this)" />
                                    <Anthem:Label ID="lblid" runat="Server" AutoUpdateAfterCallBack="True" Visible="false"
                                        Text='<%# Eval("pk_alumniid") %>'></Anthem:Label>
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
            <td class="tableheading">List Of Processed Acheivers
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: scroll; height: 200px;">
                     <div class="gridiv">
                    <Anthem:GridView ID="gvProcAchievers" runat="server" AutoUpdateAfterCallBack="True"
                        AutoGenerateColumns="False" UpdateAfterCallBack="True" Width="100%" OnRowCommand="gvProcAchievers_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="Alumni Code" DataField="alumnicode" />--%>
                            <asp:BoundField HeaderText="Alumni Name" DataField="alumni_name" />
                            <asp:BoundField HeaderText="Registration No." DataField="alumnino" />
                            <asp:BoundField HeaderText="Acheivements" DataField="Achievement" />
                            <asp:TemplateField HeaderText="DELETE">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <Anthem:LinkButton runat="server" ID="lnkAchDelete" AutoUpdateAfterCallBack="true" CommandName="DELETEREC"
                                        OnClientClick="return confirm('Are you sure to delete this Record')"
                                        CommandArgument='<%# Eval("pk_alumniid") %>' CausesValidation="False">
                            
                            <img src="../Images/Delete.gif" />
                                    </Anthem:LinkButton>
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

