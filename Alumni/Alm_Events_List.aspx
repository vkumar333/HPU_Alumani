<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_Events_List.aspx.cs" Inherits="Alumni_Alm_Events_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Events</h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <!-- Courses Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-md-12">
                    <asp:Repeater runat="server" ID="rep">
                        <ItemTemplate>
                            <div class="row evnets-item">
                                <div class="col-md-6">
                                    <div class="evnets-img evnets-img-page">
                                        <img id="Imge" src='<%# Eval("Filepath") %>' alt="event" class="mx-auto">
                                        <a class="image-link" href="#" title="Events">
                                            <i class="fa fa-link"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="course-header">
                                        <h3 class="course-title"><%# Eval("Event_name") %></h3>
                                        <div class="course-date">
                                            <i class="fa fa-calendar-check-o"></i> <%# Eval("Start_date") %>
                                        </div>
                                    </div>
                                    <div class="course-body">
                                        <div class="course-desc">
                                            <p style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                <%# Eval("Description") %>
                                            </p>
                                        </div>
                                        <asp:HyperLink runat="server" class="readon2 banner-style ext mt-10" NavigateUrl='<%# string.Format("~/Alumni/Alm_Events.aspx?ID={0}",
                    HttpUtility.UrlEncode(Eval("encId").ToString())) %>'
                                            Text="View More" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-lg-4 col-md-12">
                    <div class="sidebar-area">
                        <div class="search-box">
                            <h3 class="title">Search Events</h3>
                            <div class="box-search">
                                <div class="row">
                                    <div class="col-md-8">
                                        <Anthem:TextBox ID="txtsearch" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button runat="server" ID="btnsearch" OnClick="btnsearch_Click" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn button-page"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="cate-box">
                            <h3 class="title">Categories</h3>
                            <asp:Repeater ID="Repcountevents" runat="server">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <i class="fa fa-angle-right" aria-hidden="true"></i>
                                            <Anthem:LinkButton ID="lnkbtn" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkbtn_Click" Text="All Events" EnableCallBack="false"></Anthem:LinkButton><span class="pull-right"><%# Eval("all_Events") %></span>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <i class="fa fa-angle-right" aria-hidden="true"></i>
                                            <Anthem:LinkButton ID="lnkbtnpast" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkbtnpast_Click" Text="Past Events" EnableCallBack="false"></Anthem:LinkButton><span class="pull-right"><%# Eval("all_Events") %></span>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="Repeater2" runat="server">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <i class="fa fa-angle-right" aria-hidden="true"></i>
                                            <Anthem:LinkButton ID="lnkUpcoming" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkUpcoming_Click" Text="Upcoming Events" EnableCallBack="false"></Anthem:LinkButton><span class="pull-right"><%# Eval("all_Events") %></span>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="latest-events">
                            <h3 class="title">Latest Events</h3>
                            <asp:Repeater ID="RepLatestEvents" runat="server">
                                <ItemTemplate>
                                    <div class="post-item">
                                        <div class="post-img mt-2 mb-2 mr-2 ml-2">
                                            <img id="Imge1" src='<%# Eval("Filepath") %>' alt="events">
                                        </div>
                                        <div class="post-desc">
                                            <h4><a href="Alm_Events.aspx?ID=<%#Eval("encId").ToString() %>"><%# Eval("Event_name") %></a></h4>
                                            <i class="fa fa-calendar-check-o"></i> <%# Eval("Start_date") %>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <Anthem:Label runat="server" ID="lblmsg"></Anthem:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- events End -->
</asp:Content>