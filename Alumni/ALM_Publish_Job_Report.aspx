<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Publish_Job_Report.aspx.cs" Inherits="Alumni_ALM_Publish_Job_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="man" runat="server"></asp:ScriptManager>
    <script src="https://code.jquery.com/jquery-1.8.3.js"></script>
    <script src="https://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.4.2/chosen.jquery.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.4.2/chosen.css">

    <div>
        <table class="table">
            <tr>
                <td class="tableheading" colspan="12">Published Job Report
                </td>
            </tr>
            <tr>
                <td class="vtext" id="lblDesingation">Desingation</td>
                <td class="colon">:</td>
                <td>
                    <Anthem:TextBox ID="TxtDesignation" runat="server" AutoUpdateAfterCallBack="True"
                        MaxLength="100" SkinID="textbox"></Anthem:TextBox>
                </td>
                <td class="vtext" id="lblCompany">Company Name</td>
                <td class="colon">:</td>
                <td>
                    <Anthem:TextBox ID="TxtCompanyname" runat="server" AutoUpdateAfterCallBack="True"
                        MaxLength="100" SkinID="textbox"></Anthem:TextBox>
                </td>
            </tr>
            <tr>
                <td class="vtext" id="lblJobCategory">Job Category </td>
                <td class="colon">:</td>
                <td>
                    <Anthem:DropDownList ID="D_ddlJobCat" Width="276px" runat="server" AutoUpdateAfterCallBack="true" CssClass="ChosenSelector" AutoCallBack="true"
                        TextDuringCallBack="">
                    </Anthem:DropDownList>
                </td>
                <td class="vtext" id="lblVacancy">Vacancy Details</td>
                <td class="colon">:</td>
                <td>
                    <Anthem:TextBox ID="TxtVacancy" runat="server" AutoUpdateAfterCallBack="True"
                        MaxLength="100" SkinID="textbox"></Anthem:TextBox>
                </td>
            </tr>
            <tr>
                <td class="vtext" id="lblAlumni">Posted By</td>
                <td class="colon">:</td>
                <td>
                    <Anthem:TextBox ID="txtAlumni" runat="server" AutoUpdateAfterCallBack="True"
                        MaxLength="100" SkinID="textbox"></Anthem:TextBox>
                </td>
                <td class="vtext"></td>
                <td class="colon"></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td colspan="4">
                    <Anthem:Button ID="btnView" CssClass="logbutt" ValidationGroup="" Text="VIEW" TabIndex="22" EnableCallBack="false" runat="server" OnClick="btnView_Click" Enabled="true" />
                    <Anthem:Button ID="btnReset" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="False" OnClick="btnReset_Click" Text="RESET" class="logbutt" TextDuringCallBack="CLEARING.." TabIndex="23" />
                    <Anthem:Label ID="lblMsg1" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" ForeColor="Red" UpdateAfterCallBack="True" />
                </td>
            </tr>
        </table>
    </div>
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form">
                        <tr id="tbl_showdetail" runat="server">
                            <td colspan="8" class="tableheading">Lists of Published Jobs</td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gv_Getdata" runat="server" AllowPaging="true" OnPageIndexChanging="gv_Getdata_PageIndexChanging" PageSize="10" AutoUpdateAfterCallBack="true" Width="100%" AutoGenerateColumns="false"
                                        OnRowCommand="gv_Getdata_RowCommand">
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S. No.">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-CssClass="text-center">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblDesignation" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("Designation") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name" HeaderStyle-CssClass="text-center">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblCompanyName" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("CompanyName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Category" HeaderStyle-CssClass="text-center">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblJobCategory" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("JobCategory") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vacancy Details">
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblVacancy" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("Vacancy") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Posted By" HeaderStyle-CssClass="text-center">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblAlumniName" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("AlumniName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Details">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkViewApproved" Text="View" CausesValidation="False" CommandArgument='<%# Eval("Pk_JobPostedId") %>'
                                                        TextDuringCallBack="Loading..." OnClick="lnkViewApproved_Click" AutoUpdateAfterCallBack="True">
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" Text="Applied Job Detail" ID="gridEdit" meta:resourceKey="gv_Getdata"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" UpdateAfterCallBack="True" CausesValidation="False" EnableCallBack="false"
                                                        ID="lnkPrint" TextDuringCallBack="WAIT..." CommandArgument='<%# Eval("Pk_JobPostedId") %>'
                                                        AutoUpdateAfterCallBack="True" Text="&lt;img src=&quot;../Images/print.gif&quot; alt=&quot;&quot; border=&quot;0&quot;&gt;&lt;/img&gt;"
                                                        CommandName="PRINTREC" meta:resourcekey="lnkEditResource1"></Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </Anthem:GridView>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <Anthem:Button ID="btn_print" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btn_print_Click" Text="PRINT" EnableCallBack="false" AutoUpdateAfterCallBack="true" />

                                <Anthem:Button ID="btnexcel" runat="server" Text="Export To Excel" AutoUpdateAfterCallBack="true" Enabled="true" CommandName="ExportToExcel"
                                    OnClick="btnexcel_Click" CssClass="btn btn-primary btn-sm" EnableCallBack="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
