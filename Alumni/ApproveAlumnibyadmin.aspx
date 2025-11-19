<%-- 
==================================================================================
Created By                                                : Ayush Tyagi
On Date                                                   :28 feb 2023
Name                                                      :ApproveAlumnibyadmin 
Purpose                                                   :  
Tables used                                               :ALM_AlumniRegistration  
Stored Procedures used                                    :Alm_AlumniReqAprrovalRej
Modules                                                   :Alumni
Form                                                      :ApproveAlumnibyadmin.aspx
Last Updated Date                                         :
Last Updated By                                           :
==================================================================================--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveAlumnibyadmin.aspx.cs" Inherits="Alumni_ApproveAlumnibyadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table" width="100%">
                        <tr>
                            <td class="tableheading">List Of Pending Request
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: scroll; height: 200px;">
                                    <div class="gridiv">
                                        <Anthem:GridView ID="gvpendinreq" runat="server" AutoUpdateAfterCallBack="True"
                                            AutoGenerateColumns="False" UpdateAfterCallBack="True" Width="100%" EnableCallBack="False"
                                            OnRowCommand="gvpendinreq_RowCommand">
                                            <Columns>
                                                <%--  <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <Anthem:CheckBox runat="server" ID="chkselect" AutoUpdateAfterCallBack="true" Checked="false" />
                                                        <Anthem:Label ID="lblid" runat="Server" AutoUpdateAfterCallBack="True" Visible="false"
                                                            Text='<%# Eval("pk_alumniid") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enrollment No.">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblEnrNo" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("alumnino") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Name">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblAlumniname" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumni_name") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father Name">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblfathername" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("fathername") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="D.O.B">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblDob" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("dob") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Nature">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblNature" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumnitype") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Mentor">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblMentor" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("isMentor") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Approve/Reject">
                                                    <ItemTemplate>
                                                        <Anthem:RadioButtonList ID="rbapprove" runat="server" AutoUpdateAfterCallBack="true"
                                                            RepeatDirection="Horizontal">
                                                            <Items>
                                                                <asp:ListItem Selected="True" Value="1">Approve</asp:ListItem>
                                                                <asp:ListItem Value="0">Reject</asp:ListItem>
                                                            </Items>
                                                        </Anthem:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="VIEW">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/ALM_AlumniProfileApprovedbyadm.aspx?ID={0}",
                                                        HttpUtility.UrlEncode(Eval("pk_alumniid").ToString())) %>' Text="View Details" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="View Profile">
                                                    <ItemTemplate>
                                                        <Anthem:LinkButton runat="server" ID="lnkalumni" Text="View" CommandArgument='<%# Eval("pk_alumniid") %>'
                                                            CommandName="ViewAlmuni" AutoUpdateAfterCallBack="true" EnableCallBack="false"></Anthem:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderText="DELETE">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <Anthem:LinkButton runat="server" ID="lnkalumniPenDel" AutoUpdateAfterCallBack="true" CommandName="PendingDel"
                                                            CommandArgument='<%# Eval("pk_alumniid") %>' CausesValidation="False" OnClientClick="return confirm('Are you sure to Delete the Record.')">
                            
                            <img src="../Images/Delete.gif" />
                                                        </Anthem:LinkButton>
                                                    </ItemTemplate>

                                                </asp:TemplateField>--%>
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
                            <td class="tableheading">List Of Approved Request
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
                                                <asp:TemplateField HeaderText="Enrollment No.">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblEnrNo" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("alumnino") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Name">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblAlumniname" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumni_name") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father Name">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblfathername" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("fathername") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="D.O.B">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblDob" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("dob") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Nature">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblNature" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumnitype") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Mentor">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblMentor" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("isMentor") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <Anthem:CheckBox runat="server" ID="chkselect" AutoUpdateAfterCallBack="true" Checked="false" />
                                                        <Anthem:Label ID="lblalmid" runat="Server" AutoUpdateAfterCallBack="True" Visible="false"
                                                            Text='<%# Eval("pk_alumniid") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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

                <div class="box-body table-responsive">
                    <table class="table" width="100%">
                        <tr>
                            <td class="tableheading">List Of Rejected Request
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: scroll; height: 200px;">
                                    <div class="gridiv">
                                        <Anthem:GridView ID="gvrejected" runat="server" AutoUpdateAfterCallBack="True"
                                            AutoGenerateColumns="False" UpdateAfterCallBack="True" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enrollment No.">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblEnrNo" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("alumnino") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Name">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblAlumniname" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumni_name") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father Name">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblfathername" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("fathername") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="D.O.B">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblDob" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("dob") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alumni Nature">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblNature" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumnitype") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Mentor">
                                                    <ItemTemplate>
                                                        <Anthem:Label ID="lblMentor" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("isMentor") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <Anthem:CheckBox runat="server" ID="chkselect" AutoUpdateAfterCallBack="true" Checked="false" />
                                                        <Anthem:Label ID="lblalmid" runat="Server" AutoUpdateAfterCallBack="True" Visible="false"
                                                            Text='<%# Eval("pk_alumniid") %>'></Anthem:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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

                                <asp:Button ID="ButtonReject" runat="server" Text="ExportToExcel" CommandName="ExportToExcel" OnClick="ButtonReject_Click" 
                                    CssClass="btn btn-primary btn-sm" />

                                <Anthem:Label ID="lblmsgs" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True"></Anthem:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
