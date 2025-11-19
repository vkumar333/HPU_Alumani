<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="Approve_Mentor_Alumni_At_Admin.aspx.cs" Inherits="Alumni_Approve_Mentor_Alumni_At_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table" width="100%">
                        <tr>
                            <td class="tableheading">List Of Pending Alumni Mentor Request
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: scroll; height: 200px;">
                                    <div class="gridiv">
                                        <Anthem:GridView ID="gvpendinreq" runat="server" AutoUpdateAfterCallBack="True"
                                            AutoGenerateColumns="False" UpdateAfterCallBack="True" Width="100%" EnableCallBack="False"
                                            OnRowCommand="gvpendinreq_RowCommand" OnRowDataBound="gvpendinreq_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Registration No.">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblEnrNo" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("alumnino") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Name">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblAlumniname" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumni_name") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father Name">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblfathername" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("fathername") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="D.O.B">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblDob" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("dob") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Nature">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblNature" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumnitype") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Mentor" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblMentor" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("isMentor") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VIEW">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/ALM_AlumniMentorProfileShowAtAdm.aspx?ID={0}", HttpUtility.UrlEncode(Eval("encId").ToString())) %>' Text="View Details" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </Anthem:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="gap"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" class="btnarea" style="text-align: left; width: 100%">

                                <asp:Button ID="btnexcel" runat="server" Text="ExportToExcel" CommandName="ExportToExcel"
                                    OnClick="btnexcel_Click" CssClass="btn btn-primary btn-sm" />

                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True"
                                    SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True"></Anthem:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <Anthem:Label ID="lblmessage" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Text=""></Anthem:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="box-body table-responsive">
                    <table class="table" width="100%">
                        <tr>
                            <td class="tableheading">List Of Approved Alumni Mentor Request
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: scroll; height: 200px;">
                                    <div class="gridiv">
                                        <Anthem:GridView ID="gvapproved" runat="server" AutoUpdateAfterCallBack="True"
                                            AutoGenerateColumns="False" UpdateAfterCallBack="True" Width="100%"
                                            OnRowCommand="gvapproved_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Registration No.">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblEnrNo" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("alumnino") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Name">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblAlumniname" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumni_name") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father Name">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblfathername" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("fathername") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="D.O.B">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblDob" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("dob") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Nature">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblNature" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumnitype") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Mentor" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblMentor" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("isMentor") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </Anthem:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="gap"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" class="btnarea" style="text-align: left; width: 100%">

                                <asp:Button ID="ButtonApproved" runat="server" Text="ExportToExcel" CommandName="ExportToExcel" OnClick="ButtonApproved_Click"
                                    CssClass="btn btn-primary btn-sm" />

                                <Anthem:Label ID="Labelmsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True"></Anthem:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
