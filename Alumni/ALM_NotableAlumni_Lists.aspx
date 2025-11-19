<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_NotableAlumni_Lists.aspx.cs" Inherits="Alumni_ALM_NotableAlumni_Lists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Notable Alumni </h1>
                        <br />
                        <br />
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back </a>
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
                    <asp:Repeater ID="RepeventsAll" runat="server">
                        <ItemTemplate>
                            <div class="row evnets-item">

                                <div class="col-md-6">
                                    <div class="evnets-img">
                                        <img runat="server" id="Imge1" src='<%# Eval("PicUrl")%>' alt="news" style="height: 190px; width: 100%;" />
                                        <a class="image-link" href="#" title="NotableAlumni">
                                            <i class="fa fa-link"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="course-header">
                                        <h3 class="course-title"><%# Eval("SubHeading") %></h3>
                                        <%--<div class="course-date">
                                            <i class="fa fa-calendar-check-o"></i> <%# Eval("ConvertedDate") %>
                                        </div>--%>
                                    </div>
                                    <div class="course-body">
                                        <div class="course-desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                            <p>
                                                <%# Eval("Comments") %>
                                            </p>
                                        </div>
                                        <asp:HyperLink runat="server" class="readon2 banner-style ext mt-10" NavigateUrl='<%# string.Format("~/Alumni/ALM_NotableAlumni_Details.aspx?ID={0}", HttpUtility.UrlEncode(Eval("encId").ToString())) %>' Text="View More" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <%--<div class="col-lg-4 col-md-12">
                    <div class="sidebar-area">
                        <div class="search-box">
                            <h3 class="title">Search News & Stories </h3>
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
                            <h3 class="title">Categories </h3>
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
                            <h3 class="title">Latest News & Stories </h3>
                            <div class="latest-events-body">
                                <asp:Repeater ID="RepLatestnews" runat="server">
                                    <ItemTemplate>
                                        <div class="post-item">
                                            <div class="post-img">
                                                <img runat="server" id="Imge1" src='<%# Eval("PicUrl")%>' alt="news" style="height: 80px; width: 100%;">
                                            </div>
                                            <div class="post-desc">
                                                <h4><a href="Alm_NewsandEvents_por.aspx?ID=<%#Eval("encId").ToString() %>"><%# Eval("Heading") %></a></h4>
                                                <i class="fa fa-calendar-check-o"></i> <%# Eval("CreatedDate") %>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    <!-- events End -->
</asp:Content>