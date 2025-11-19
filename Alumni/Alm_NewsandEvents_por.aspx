<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_NewsandEvents_por.aspx.cs" Inherits="Alumni_Alm_NewsandEvents_por" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">News & Stories</h1>
                        <%--<ul>
                            <li>
                                <a class="active" href="Default.aspx">Home</a>
                            </li>
                            <li>News&Stories</li>
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
                <div class="col-lg-10 col-md-12">
                    <asp:Repeater ID="RepeventsAll" runat="server">
                        <ItemTemplate>
                            <div>
                                <div class="single-image">
                                    <img runat="server" id="Imge" src='<%# Eval("ImageUrl")%>' alt="" style="width: 100%; max-width: 50%; height: 400px;">
                                </div>
                                <h5 class="top-title"><%# Eval("Heading") %></h5>
                                <span class="date">
                                    <i class="fa fa-calendar" aria-hidden="true"></i> <%# Eval("ConvertedDate") %>
                                </span>
                                <h5 class="description">Description</h5>
                                <p style="text-align: justify;"><%# Eval("Description") %></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="share-section2">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <span>You Can Share It : </span>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <%--<ul class="share-link">
                                    <li><a href="https://www.facebook.com/HpuNewShimla" style="background: #3b5998;"><i class="fa fa-facebook" aria-hidden="true"></i>Facebook</a></li>
                                    <li><a href="https://twitter.com/hpu_shimla?t=lRks6KUPBVeBjXTZgzNs_Q&s=08" style="background: #00acee;"><i class="fa fa-twitter" aria-hidden="true"></i>Twitter</a></li>
                                    <li><a href="https://in.linkedin.com/school/himachal-pradesh-university/" style="background: #0A66C2;"><i class="fa fa-linkedin" aria-hidden="true"></i>Linkedin</a></li>
                                </ul>--%>
                                <ul class="share-link">
                                    <li><a id="facebookLink" runat="server" style="background: #3b5998;"><i class="fa fa-facebook" aria-hidden="true"></i>Facebook</a></li>
                                    <li><a id="twitterLink" runat="server" style="background: #00acee;"><i class="fa fa-twitter" aria-hidden="true"></i>Twitter</a></li>
                                    <li><a id="linkedInLink" runat="server" style="background: #0A66C2;"><i class="fa fa-linkedin" aria-hidden="true"></i>Linkedin</a></li>
                                    <li><a id="youtubeLink" runat="server" style="background: #FF0000;"><i class="fa fa-youtube" aria-hidden="true"></i>Youtube</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div style="float: right;">
                        <a class="readon2 banner-style" href="ALM_View_All_News.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i>Back</a>
                    </div>
                </div>
                <%--<div class="col-lg-4 col-md-12">
                    <div class="sidebar-area">
                        <div class="search-box">
                            <h3 class="title">Search Events</h3>
                            <div class="box-search">
                                <input class="form-control" placeholder="Search Here ..." name="srch-term" id="srch-term" type="text">
                                <button class="btn btn-default" type="submit"><i class="fa fa-search" aria-hidden="true"></i></button>
                            </div>
                        </div>
                        <div class="cate-box">
                            <h3 class="title">Categories</h3>
                            <ul>
                                <li>
                                    <i class="fa fa-angle-right" aria-hidden="true"></i> <a href="#">All Events <span>(05)</span></a>
                                </li>
                                <li>
                                    <i class="fa fa-angle-right" aria-hidden="true"></i> <a href="#">Past Events <span>(07)</span></a>
                                </li>
                                <li>
                                    <i class="fa fa-angle-right" aria-hidden="true"></i> <a href="#">Upcoming Events <span>(09)</span></a>
                                </li>
                            </ul>
                        </div>
                       
                        <div class="latest-events">
                            <h3 class="title">Latest Events</h3>
                             <asp:Repeater ID="RepLatestnews" runat="server">
                                 <ItemTemplate>
                                     <div class="post-item">
                                <div class="post-img">
                                    <a href="#"> <img  runat="server" id="Imge1" src='<%# "~/ALM_uploadimg/"+Eval("Image")%>'  alt="news"></a>
                                </div>
                                <div class="post-desc">
                                    <h4><a href="#"><%# Eval("Heading") %></a></h4>
                                    <i class="fa fa-calendar-check-o"></i> <%# Eval("ConvertedDate") %>
                                </div>
                            </div>
                                 </ItemTemplate>
                             </asp:Repeater>
                           
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    <!-- event End -->
    <!-- Main content End -->
</asp:Content>