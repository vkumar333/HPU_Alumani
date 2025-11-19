<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="Alm_approvePublish_Jobs.aspx.cs" Inherits="Alumni_Alm_approvePublish_Jobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form">
                        <tr>
                            <td colspan="4" class="tableheading">Manage Jobs</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tablesubheading">List of Pending Job for Approval 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvDetails" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" DataKeyNames="Pk_JobPostedId" EnableCallBack="false" Width="100%"
                                        OnRowCommand="gvDetails_RowCommand" OnPageIndexChanging="gvDetails_PageIndexChanging"
                                        UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                    <Anthem:HiddenField ID="hdncompanyid" runat="server" Value='<%# Eval("Pk_JobPostedId") %>' />
                                                    <Anthem:Label ID="txtCompanyReqId" runat="server" Visible="false" Text='<%# Eval("Pk_JobPostedId").ToString() %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="JobOpenFrom" HeaderText="Job Apply From">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="JobOpenTo" HeaderText="Last Date for Applying for a Job">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="alumni_name" HeaderText="Published By">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Details">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkView1" Text="View" CausesValidation="False" CommandArgument='<%# Eval("Pk_JobPostedId") %>'
                                                        TextDuringCallBack="Loading..." AutoUpdateAfterCallBack="True" OnClick="lnkView1_Click" CommandName="Select">
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve/Reject" ItemStyle-Width="12%">
                                                <ItemStyle HorizontalAlign="Center" />
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
                                            <%--<asp:TemplateField HeaderText="Remarks">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:TextBox ID="txtRemarks" runat="server" AutoUpdateAfterCallBack="True" MaxLength="150"
                                                        onpaste="event.returnValue=false" ondrop="event.returnValue=false"
                                                        onkeypress="return CheckMaxLength(this,149);" TextMode="MultiLine"></Anthem:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:Button runat="server" CssClass="btn btn-primary btn-sm" ID="btnSubmit" Text="DONE" CausesValidation="False" CommandArgument='<%# Eval("Pk_JobPostedId") %>'
                                                        TextDuringCallBack="Submitting..." AutoUpdateAfterCallBack="True"
                                                        CommandName="SUBMIT"></Anthem:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </Anthem:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form">
                        <tr>
                            <td class="gap" colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tablesubheading">List of Approved Jobs</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <Anthem:GridView ID="gvApprovedJobs" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoUpdateAfterCallBack="True" Width="100%"
                                    OnPageIndexChanging="gvApprovedJobs_PageIndexChanging" UpdateAfterCallBack="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <Anthem:Label ID="lblCompID" runat="server" Text='<%# Eval("CompanyName") %>' AutoUpdateAfterCallBack="True"></Anthem:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Designation" HeaderText="Designation">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JobOpenFrom" HeaderText="Job Apply From">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JobOpenTo" HeaderText="Last Date for Applying for a Job">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="alumni_name" HeaderText="Published By">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Details">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <Anthem:LinkButton runat="server" ID="lnkViewApproved" Text="View" CausesValidation="False" CommandArgument='<%# Eval("Pk_JobPostedId") %>'
                                                    TextDuringCallBack="Loading..." OnClick="lnkViewApproved_Click" AutoUpdateAfterCallBack="True">
                                                </Anthem:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </Anthem:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4">
                                <Anthem:Button ID="btnAppExportToExcel" runat="server" Text="Export To Excel" AutoUpdateAfterCallBack="true" CommandName="ExportToExcel" OnClick="btnAppExportToExcel_Click" CssClass="btn btn-primary btn-sm" EnableCallBack="false" />
                                <Anthem:Label ID="lblMsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tablesubheading">List of Rejected Job</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvRejectedJobs" runat="server" EnableCallBack="false" AllowPaging="True" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" DataKeyNames="Pk_JobPostedId" Width="100%" OnPageIndexChanging="gvRejectedJobs_PageIndexChanging"
                                        UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="JobOpenFrom" HeaderText="Job Apply From">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="JobOpenTo" HeaderText="Last Date for Applying for a Job">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="alumni_name" HeaderText="Published By">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Details">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkView2" Text="View" CausesValidation="False" CommandArgument='<%# Eval("Pk_JobPostedId") %>'
                                                        TextDuringCallBack="Loading..." OnClick="lnkView2_Click" AutoUpdateAfterCallBack="True">
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </Anthem:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4">
                                <Anthem:Button ID="btnRejExportToExcel" runat="server" Text="Export To Excel" AutoUpdateAfterCallBack="true" CommandName="ExportToExcel" OnClick="btnRejExportToExcel_Click" CssClass="btn btn-primary btn-sm" EnableCallBack="false" />
                                <Anthem:Label ID="Label1" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>