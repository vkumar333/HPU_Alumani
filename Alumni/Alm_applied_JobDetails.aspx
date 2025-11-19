<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="Alm_applied_JobDetails.aspx.cs" Inherits="Alumni_Alm_applied_JobDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form">
                        <tr>
                            <td colspan="4" class="tableheading">Candidates List</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tablesubheading">List of applied Jobs By Candidates
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvDetails" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" DataKeyNames="Pk_Applied_JobId" Width="100%"
                                        UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                    <Anthem:HiddenField ID="hdncompanyid" runat="server" Value='<%# Eval("Pk_Applied_JobId") %>' />
                                                    <Anthem:Label ID="txtCompanyReqId" runat="server" Visible="false" Text='<%# Eval("Pk_Applied_JobId").ToString() %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Job_No" HeaderText="Request No.">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="alumni_name" HeaderText="Candidate Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="contactno" HeaderText="Contact No.">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="email" HeaderText="Email">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="View CV">
                                                <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <a target="_blank" id="anchorPath" href='<%#SetServiceDoc(Eval("filename").ToString()) %>'>View</a>
                                                    <%--<a runat="server" target="_blank" id="anchorPath">View File</a>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="View CV">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkViewCv" Text='<%# Eval("filename") %>'  CausesValidation="False"  CommandArgument='<%# Eval("Pk_Applied_JobId") %>' CommandName="VIEW"
                                                        TextDuringCallBack="Loading..." AutoUpdateAfterCallBack="True">
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>         --%>
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
</asp:Content>
