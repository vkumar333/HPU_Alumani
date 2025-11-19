<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Fund_DonationLists.aspx.cs" Inherits="Alumni_ALM_Fund_DonationLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .alert-success {
            color: #155724 !important;
            background-color: #d4edda !important;
            border-color: #c3e6cb !important;
        }
    </style>
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Fundraising</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <div class="rs-events-list sec-spacer">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12 col-md-12 text-center">
                    <span class="spanLoginReg">&nbsp;<p class="p-text">Categories</p>
                        <asp:RadioButtonList ID="rdbCategories" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbCategories_SelectedIndexChanged" AutoPostBack="true">
                        </asp:RadioButtonList></span>
                </div>

                <div class="col-lg-12 col-md-12">
                    <div class="row evnets-item evnets-item-page">

                        <asp:Repeater runat="server" ID="rep">
                            <ItemTemplate>
                                <div class="col-md-4">
                                    <div class="evnets-item-shadow">
                                        <div class="course-header">
                                            <h3 class="course-title"><%# Eval("Heading") %></h3>
                                        </div>
                                        <div class="course-desc">
                                            <p>
                                                <span class="fund"><%# Eval("MsgNew") %> </span>
                                            </p>
                                            <div class="progress mb-10 mt-10">
                                                <div class="progress-bar progress-bar-striped bg-info" role="progressbar" style='<%# "color:#6D7B8D;width:" + DataBinder.Eval(Container.DataItem, "percentage") + ";" %>' aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <p class="alert alert-success">
                                                <i class="fa fa-users" aria-hidden="true"></i> <%# Eval("Peoplecount") %>
                                            </p>
                                            <div style="padding: 15px 0;">
                                                <img src='<%# Eval("FilePath") %>' width="100%" style="height: 170px;" />
                                            </div>
                                        </div>
                                        <div class="course-body">
                                            <asp:HyperLink runat="server" class="readon2 banner-style ext ext-page mt-10" NavigateUrl='<%# string.Format("~/Alumni/ALM_Fund_Donation.aspx?ID={0}", HttpUtility.UrlEncode(Eval("encId").ToString())) %>'
                                                Text="View / Contribute Now" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="col-lg-1 col-md-12">
                    <div class="back-btn">
                        <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
                    </div>
                </div>
                <div class="col-lg-4 col-md-12" style="display: none;">
                    <div class="sidebar-area">
                        <div class="cate-box">
                            <h3 class="title">Categories</h3>
                            <asp:Repeater ID="Repcountall" runat="server">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <i class="fa fa-angle-right" aria-hidden="true"></i>
                                            <Anthem:LinkButton ID="lnkbtn" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkbtn_Click" Text="All" EnableCallBack="false"></Anthem:LinkButton><span class="pull-right">(<%# Eval("allcounts") %>)</span>
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