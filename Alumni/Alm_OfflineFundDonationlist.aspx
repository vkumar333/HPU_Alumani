<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UMM/MasterPage.master" CodeFile="Alm_OfflineFundDonationlist.aspx.cs" Inherits="Alumni_Alm_OfflineFundDonationlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="alumin-default-theme/style.css" rel="stylesheet" />
    <style>
        .sidebar-area .title {
            background: #0a6186;
        }

        .progress-bar {
            background-color: #337ab7 !important;
        }
    </style>
    <table class="table mobile_form">
        <tr>
          <td colspan="12" class="tableheading">Offline Contribution</td>
        </tr>
    </table>
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-md-12">
                    <asp:Repeater runat="server" ID="rep">
                        <ItemTemplate>
                            <div class="row evnets-item">
                                <div class="col-md-6">
                                    <div class="evnets-img">
                                        <img style="height: 235px!important" width="100%" id="Imge" src='<%# Eval("FilePath") %>' alt="event">
                                        <a class="image-link" href="#" title="Events">
                                            <i class="fa fa-link"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="course-header">
                                        <h3 class="course-title"><a href="#"><%# Eval("Heading") %></h3>
                                    </div>
                                    <div class="course-desc">
                                        <p>
                                            <span class="fund"><%# Eval("MsgNew") %> </span>
                                        </p>
                                        <div class="progress mb-10 mt-10">
                                            <div class="progress-bar progress-bar-striped bg-info" role="progressbar" style='<%# "color:#6D7B8D;width:" + DataBinder.Eval(Container.DataItem, "percentage") + ";" %>' aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            <%--<div class="progress-bar progress-bar-striped bg-info" role="progressbar" style="width: 25%" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>--%>
                                        </div>
                                        <p class="alert alert-success">
                                            <i class="fa fa-users" aria-hidden="true"></i> <%# Eval("Peoplecount") %>
                                        </p>
                                    </div>
                                    <div class="course-body">
                                        <asp:HyperLink runat="server" class="readon2 banner-style ext mt-10" NavigateUrl='<%# string.Format("~/Alumni/Alm_OfflineFundDonation.aspx?ID={0}",
                                             HttpUtility.UrlEncode(Eval("encId").ToString())) %>'
                                            Text="Contribute Now" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-lg-4 col-md-12">
                    <div class="sidebar-area">
                        <div class="cate-box">
                            <h3 class="title">Categories</h3>
                            <asp:Repeater ID="Repcountall" runat="server">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <i class="fa fa-angle-right" aria-hidden="true"></i>
                                            <Anthem:LinkButton ID="lnkbtn" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkbtn_Click" Text="All" EnableCallBack="false"></Anthem:LinkButton><span class="pull-right">(<%# Eval("allcounts") %>)</span>
                                            <%--<a href="#">All Events <span><%# Eval("all_Events") %></span></a>--%>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <i class="fa fa-angle-right" aria-hidden="true"></i>
                                            <Anthem:LinkButton ID="lnkbtnAcad" runat="server" AutoUpdateAfterCallBack="true" CommandName="View" CommandArgument='1' Text="Academic" EnableCallBack="false"></Anthem:LinkButton><span class="pull-right">(<%# Eval("all_Acadmics") %>)</span>
                                            <asp:HiddenField ID="LblIds" runat="server" Value='1' />
                                            <%--<a href="#">All Events <span><%# Eval("all_Events") %></span></a>--%>
                                        </li>

                                    </ul>

                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <i class="fa fa-angle-right" aria-hidden="true"></i>
                                            <Anthem:LinkButton ID="lnkbtndevelopmennt" runat="server" CommandName="View" CommandArgument='2' AutoUpdateAfterCallBack="true" Text="Development" EnableCallBack="false"></Anthem:LinkButton><span class="pull-right">(<%# Eval("all_Development") %>)</span>
                                            <asp:HiddenField ID="LblIds1" runat="server" Value='2' />
                                            <%--<a href="#">All Events <span><%# Eval("all_Events") %></span></a>--%>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="Repeater3" runat="server" OnItemCommand="Repeater3_ItemCommand">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <i class="fa fa-angle-right" aria-hidden="true"></i>
                                            <Anthem:LinkButton ID="lnkbtnsocial" runat="server" AutoUpdateAfterCallBack="true" CommandName="View" CommandArgument='3' Text="Social" EnableCallBack="false"></Anthem:LinkButton><span class="pull-right">(<%# Eval("all_Social") %>)</span>
                                            <asp:HiddenField ID="LblIds2" runat="server" Value='3' />
                                            <%--<a href="#">All Events <span><%# Eval("all_Events") %></span></a>--%>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>