<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_NewsandStoriesLists.aspx.cs" Inherits="Alumni_Alm_NewsandStoriesLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">News & Stories</h1>
                        <br />
                        <br />
                        <%-- <ul>
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
                    <asp:Repeater ID="RepeventsAll" runat="server">
                        <ItemTemplate>
                            <div class="row evnets-item">

                                <div class="col-md-6">
                                    <div class="evnets-img">
                                        <img runat="server" id="Imge1" src='<%# "~/ALM_uploadimg/"+Eval("Image")%>' alt="news" />
                                        <a class="image-link" href="Alm_NewsandEvents_por.aspx" title="Events">
                                            <i class="fa fa-link"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="course-header">
                                        <h3 class="course-title"><a href="Alm_NewsandEvents_por.aspx"><%# Eval("Heading") %></a></h3>
                                        <div class="course-date">
                                            <i class="fa fa-calendar-check-o"></i><%# Eval("ConvertedDate") %>
                                        </div>
                                    </div>
                                    <div class="course-body">
                                        <div class="course-desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                            <p>
                                                <%# Eval("Description") %>
                                            </p>
                                        </div>
                                        <asp:HyperLink runat="server" class="readon2 banner-style ext mt-10" NavigateUrl='<%# string.Format("~/Alumni/Alm_NewsandEvents_por.aspx?ID={0}", HttpUtility.UrlEncode(Eval("Pk_Stories_id").ToString())) %>' Text="View More" />
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
                                        <asp:Button runat="server" ID="btnsearch" OnClick="btnsearch_Click" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="cate-box">
                            <h3 class="title">Categories</h3>
                            <ul>
                                <li>
                                    <Anthem:LinkButton ID="lnj" runat="server" class="fa fa-angle-right" aria-hidden="true" CommandArgument='1' EnableCallBack="false" OnClick="lnj_Click">Alumni Stories</Anthem:LinkButton>
                                    <asp:HiddenField ID="LblIds" runat="server" Value='1' />
                                </li>
                                <li>
                                    <Anthem:LinkButton ID="lnkbtn" class="fa fa-angle-right" aria-hidden="true" runat="server" AutoUpdateAfterCallBack="true" EnableCallBack="false" CommandArgument='2' OnClick="lnkbtn_Click">Institute Updates</Anthem:LinkButton>
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Fk_Ctaegory_Id") %>' />
                                </li>

                            </ul>
                        </div>
                        <div class="latest-events">
                            <h3 class="title">Latest Events</h3>
                            <div class="latest-events-body">
                                <asp:Repeater ID="RepLatestnews" runat="server">
                                    <ItemTemplate>
                                        <div class="post-item">
                                            <div class="post-img">
                                                <a href="#">
                                                    <img runat="server" id="Imge1" src='<%# "~/ALM_uploadimg/"+Eval("Image")%>' alt="news"></a>
                                            </div>
                                            <div class="post-desc">
                                                <h4><a href="#"><%# Eval("Heading") %></a></h4>
                                                <i class="fa fa-calendar-check-o"></i><%# Eval("ConvertedDate") %>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- events End -->
</asp:Content>
