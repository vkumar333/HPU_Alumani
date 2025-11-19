<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_Events_Lists.aspx.cs" Inherits="Alumni_Alm_Events_Lists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .btn {
            position: absolute;
            right: 3px;
            background: transparent;
            border: none;
            box-shadow: none;
            top: 50%;
            outline: none;
            transform: translateY(-50%);
        }
    </style>
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Events</h1>
                        <%--<ul>
                            <li>
                                <a class="active" href="Alm_Default.aspx">Home</a>
                            </li>
                            <li>Events</li>
                        </ul>--%>
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
                    <asp:Repeater ID="rep" runat="server" >
                        <ItemTemplate>
                            <div class="row evnets-item">
                                <div class="col-md-6">
                                    <div class="evnets-img">
                                        <img id="Imge" src='<%# Eval("Filepath") %>' alt="event">
                                        <a class="image-link" href="#" title="Events">
                                            <i class="fa fa-link"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="course-header">
                                        <h3 class="course-title"><a href="#"><%# Eval("Event_name") %></a></h3>
                                        <div class="course-date">
                                            <i class="fa fa-calendar-check-o"></i><%# Eval("Start_date") %>
                                        </div>
                                    </div>
                                    <div class="course-body">
                                        <div class="course-desc">
                                            <p style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                <%# Eval("Description") %>
                                            </p>
                                        </div>
                                        <asp:HyperLink runat="server" class="readon2 banner-style ext mt-10" NavigateUrl='<%# string.Format("~/Alumni/Alm_Events.aspx?ID={0}", HttpUtility.UrlEncode(Eval("PK_Events_id").ToString())) %>' Text="View More" />
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
                                    <asp:Button runat="server"  ID="btnsearch" OnClick="btnsearch_Click" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn"></asp:Button>
                                </div>
                                 </div>
                            </div>
                        </div>
                            <div class="cate-box">
                                <h3 class="title" style="margin-top: 3px; margin-bottom:auto;">Categories</h3>
                                <asp:Repeater ID="Repcountevents" runat="server">
                                    <ItemTemplate>
                                        <ul>
                                            <li>
                                                <i class="fa fa-angle-right" aria-hidden="true"></i>
                                                <Anthem:LinkButton ID="lnkbtn" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkbtn_Click" Text="All Events" EnableCallBack="false"></Anthem:LinkButton><span style="margin-left: 252px;"><%# Eval("all_Events") %></span>
                                                <%--<a href="#">All Events <span><%# Eval("all_Events") %></span></a>--%>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <ul>
                                            <li>
                                                <i class="fa fa-angle-right" aria-hidden="true"></i>
                                                <Anthem:LinkButton ID="lnkbtnpast" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkbtnpast_Click" Text="Past Events" EnableCallBack="false" ></Anthem:LinkButton><span style="margin-left: 240px;"><%# Eval("all_Events") %></span>
                                                <%--<a href="#">Past Events <span><%# Eval("all_Events") %></span></a>--%>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <ul>
                                            <li>
                                                <i class="fa fa-angle-right" aria-hidden="true"></i>
                                                <Anthem:LinkButton ID="lnkUpcoming" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkUpcoming_Click" Text="Upcoming Events" EnableCallBack="false" ></Anthem:LinkButton><span style="margin-left: 195px;"><%# Eval("all_Events") %></span>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="latest-events">
                                <h3 class="title">Latest Events</h3>
                                <asp:Repeater ID="RepLatestEvents" runat="server">
                                    <ItemTemplate>
                                        <div class="post-item">
                                            <div class="post-img">
                                                <a href="#">
                                                    <img id="Imge1" src='<%# Eval("Filepath") %>' alt="events"></a>
                                            </div>
                                            <div class="post-desc">
                                                <h4><a href="#"><%# Eval("Event_name") %></a></h4>
                                                <i class="fa fa-calendar-check-o"></i><%# Eval("Start_date") %>
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

