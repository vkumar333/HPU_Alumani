<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_Mentees_Assigned.aspx.cs"
    Inherits="Alumni_Alm_Mentees_Assigned" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .back-btnn {
            background: #016773;
            padding: 10px;
            border-radius: 9px;
            text-align: center;
            margin-top: 10px;
            float: right;
            width: 90px;
            margin-bottom: 10px;
            color: #fff;
            font-weight: 500;
        }

        .back-bt {
            color: #fff;
            font-size: 12px;
            text-decoration: none;
        }

        .a-txtColor {
            color: #fff;
            font-size: 12px;
            text-decoration: none;
        }

        .boxhead h3.box-title {
            font-size: 14px !important;
        }
    </style>
    <%--<table class="table">
        <tr>
            <td class="tablesubheading" colspan="4">List of Mentees Assigned</td>
        </tr>
        <tr>
            <td colspan="4">
                <div class="gridiv">
                    <Anthem:GridView ID="gvAlumni" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        AutoUpdateAfterCallBack="True" DataKeyNames="Pk_Mentee_Reqid" Width="100%" PageSize="10"
                        OnRowCommand="gvAlumni_RowCommand"
                        OnPageIndexChanging="gvAlumni_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo.">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Registration No" DataField="alumnino">
                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Alumni Name" DataField="alumni_name">
                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Contact No" DataField="contactno">
                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="Assigned Date" DataField="Action_date">
                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                            </asp:BoundField>--%>
    <%-- <asp:BoundField Visible="false" HeaderText="College Name" DataField="collegenamer">
                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Degree" DataField="DegreeName">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Year of Passing" DataField="yearofpassing">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                        </asp:BoundField>--%>
    <%-- <asp:TemplateField HeaderText="View">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <Anthem:LinkButton runat="server" UpdateAfterCallBack="True" ID="lnkEdit" CausesValidation="False"
                                    CommandArgument='<%# Eval("pk_alumniid") %>' TextDuringCallBack="Loading..." AutoUpdateAfterCallBack="True" CommandName="SELECT">
                               <img src="../Images/search1.gif" alt="View" border="0" height="20" width="20"></img>
                                </Anthem:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
    <%--</Columns>
                    </Anthem:GridView>
                </div>
            </td>
        </tr>
    </table>--%>

    <div class="alumni_home_pg">
        <div class="col-md-12">
            <div class="row">
                <div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">List of Mentees Assigned </h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="panel-body pnl-body-custom">
                        <div id="inner-content-div1" class="">
                            <div class="gridiv">
                                <Anthem:GridView ID="gvAlumni" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    AutoUpdateAfterCallBack="True" DataKeyNames="Pk_Mentee_Reqid" Width="100%" PageSize="10"
                                    OnRowCommand="gvAlumni_RowCommand" CssClass="gr"
                                    OnPageIndexChanging="gvAlumni_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo.">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Registration No" DataField="alumnino">
                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Alumni Name" DataField="alumni_name">
                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Contact No" DataField="contactno">
                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Assigned Date" DataField="Action_date">
                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </Anthem:GridView>
                            </div>
                        </div>
                        <div class="back-btnn">
                            <a href="../Alumni/ALM_Alumni_Home.aspx" class="a-txtColor">Back</a>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
