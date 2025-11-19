<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="Alm_AssignedMentor.aspx.cs" Inherits="Alumni_Alm_AssignedMentor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../include/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var gv = $('[id$=gvApprovedComp]');
            $("[id*='chkall']").on('change', function () {
                gv.find("[id*='chkstud']").prop('checked', $(this).is(":checked"));
            });
            $("[id*='chkstud']").on('change', function () {
                gv.find("[id*='chkall']:checkbox").prop('checked', gv.find("[id*='chkstud']:checkbox").length == gv.find("[id*='chkstud']:checkbox:checked").length);
            });

            var gvRejected_Company = $('[id$=gvRejected_Company]');
            $("[id*='R_student_ALL']").on('change', function () {
                gvRejected_Company.find("[id*='chk_R_stud']").prop('checked', $(this).is(":checked"));
            });
            $("[id*='chk_R_stud']").on('change', function () {
                gvRejected_Company.find("[id*='R_student_ALL']:checkbox").prop('checked', gvRejected_Company.find("[id*='chk_R_stud']:checkbox").length == gvRejected_Company.find("[id*='chk_R_stud']:checkbox:checked").length);
            });
        });

    </script>
    <script type="text/javascript">
        //function SelectheaderCheckboxes(headerchk) {
        //    var gvcheck = document.getElementById('ctl00_ContentPlaceHolder1_gvRejected_Company');
        //    var i;
        //    if (headerchk.checked) {
        //        for (i = 0; i < gvcheck.rows.length; i++) {
        //            var inputs = gvcheck.rows[i].getElementsByTagName('input');
        //            inputs[0].checked = true;
        //        }
        //    }
        //    else {
        //        for (i = 0; i < gvcheck.rows.length; i++) {
        //            var inputs = gvcheck.rows[i].getElementsByTagName('input');
        //            inputs[0].checked = false;
        //        }
        //    }
        //}

        //function Selectchildcheckboxes(header) {
        //    var ck = header;
        //    var count = 0;
        //    var gvcheck = document.getElementById('gvdetails');
        //    var headerchk = document.getElementById(header);
        //    var rowcount = gvcheck.rows.length;

        //    for (i = 1; i < gvcheck.rows.length; i++) {
        //        var inputs = gvcheck.rows[i].getElementsByTagName('input');
        //        if (inputs[0].checked) {
        //            count++;
        //        }
        //    }

        //    if (count == rowcount - 1) {
        //        headerchk.checked = true;
        //    }
        //    else {
        //        headerchk.checked = false;
        //    }
        //}
    </script>

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table" width="100%">
                        <tr>
                            <td colspan="4" class="tablesubheading">List of Pending Mentees Request</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvDetails" runat="server" EnableCallBack="false" AllowPaging="true" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" DataKeyNames="Pk_Mentee_Reqid" Width="100%" OnRowDataBound="gvDetails_RowDataBound"
                                        OnRowCommand="gvDetails_RowCommand"
                                        OnPageIndexChanging="gvDetails_PageIndexChanging" PageSize="10" UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Alumni Registration No.">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_loginname" runat="server" Text='<%# Eval("loginname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mentee Name">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_alumni_name" runat="server" Text='<%# Eval("alumni_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Requested Date">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Regno" runat="server" Text='<%# Eval("CreationDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mentors Name" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:DropDownList ID="ddlMentors" runat="server" AutoUpdateAfterCallBack="True">
                                                    </Anthem:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="VIEW" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="VIEW" Text="View Request Form" EnableCallBack="false" CausesValidation="False" CommandArgument='<%# Eval("Pk_Mentee_Reqid") %>'
                                                        TextDuringCallBack="Wait..." AutoUpdateAfterCallBack="True" OnClick="VIEW_Click">
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Approve/Reject">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:RadioButtonList ID="Rd_Approve" runat="server"
                                                        RepeatDirection="Horizontal">
                                                        <Items>
                                                            <asp:ListItem Value="True">Approve</asp:ListItem>
                                                            <asp:ListItem Value="False">Reject</asp:ListItem>
                                                        </Items>
                                                    </Anthem:RadioButtonList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <ItemTemplate>
                                                    <Anthem:TextBox ID="txtremarks" TextMode="MultiLine" runat="server"></Anthem:TextBox>
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
                        <tr>
                            <td align="left" colspan="2" class="btnarea" style="text-align: left; width: 100%">
                                <Anthem:Button AutoUpdateAfterCallBack="True" ID="btnSave" runat="server" CommandName="SAVE"
                                    OnClick="btnSave_Click" Text="PROCESS" CssClass="btn btn-primary btn-sm" TextDuringCallBack="SAVING..."
                                    PreCallBackFunction="btnSave_PreCallBack" />
                                <%-- <Anthem:Button ID="btnReset" runat="server" CssClass="btn btn-default btn-sm" CausesValidation="False" OnClick="btnReset_Click"
                                    Text="RESET" TextDuringCallBack="RESETING.." AutoUpdateAfterCallBack="True" />--%>
                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True"
                                    SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True" />
                            </td>
                        </tr>
                    </table>
                    <table class="table" width="100%">
                        <tr>
                            <td colspan="4" class="tablesubheading">List of Assigned Mentors</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="Gvapproved_Details" runat="server" EnableCallBack="false" AllowPaging="true" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" DataKeyNames="Pk_Mentee_Reqid" Width="100%"
                                        OnPageIndexChanging="Gvapproved_Details_PageIndexChanging" PageSize="10" UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Alumni Registration No.">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_loginname" runat="server" Text='<%# Eval("loginname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mentee Name">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_alumni_name" runat="server" Text='<%# Eval("alumni_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Approved Date">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("Action_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("isApproved") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mentors Name" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:Label runat="server" ID="lblMentors" Text='<%# Eval("assign_Mentor") %>' AutoCallBack="true" AutoUpdateAfterCallBack="True">
                                                    </Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblremarks" Text='<%# Eval("Remarks") %>' AutoCallBack="true" AutoUpdateAfterCallBack="True"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="VIEW" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="VIEWs" Text="View Request Form" EnableCallBack="false" CausesValidation="False" CommandArgument='<%# Eval("Pk_Mentee_Reqid") %>'
                                                        TextDuringCallBack="Wait..." AutoUpdateAfterCallBack="True" OnClick="VIEWs_Click">
                                                    </Anthem:LinkButton>
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

                    <table class="table" width="100%">
                        <tr>
                            <td colspan="4" class="tablesubheading">List of Rejected Request</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="GVRejectDetails" runat="server" EnableCallBack="false" AllowPaging="true" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" DataKeyNames="Pk_Mentee_Reqid" Width="100%"
                                        OnPageIndexChanging="GVRejectDetails_PageIndexChanging" PageSize="10" UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Alumni Registration No.">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_loginname" runat="server" Text='<%# Eval("loginname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mentee Name">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_alumni_name" runat="server" Text='<%# Eval("alumni_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Approved Date">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("Action_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("isApproved") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblremarks" Text='<%# Eval("Remarks") %>' AutoCallBack="true" AutoUpdateAfterCallBack="True"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="VIEW" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkrejectview" Text="View Request Form" EnableCallBack="false" CausesValidation="False" CommandArgument='<%# Eval("Pk_Mentee_Reqid") %>'
                                                        TextDuringCallBack="Wait..." AutoUpdateAfterCallBack="True" OnClick="lnkrejectview_Click">
                                                    </Anthem:LinkButton>
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