<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_View_All_News.aspx.cs" Inherits="Alumni_ALM_View_All_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../include/jquery-1.2.1.min.js"></script>
    

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">News </h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_NewsandStoriesList.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <!-- News Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container-fluid">

            <div class="box box-success" runat="server">
                <div class="panel-body pnl-body-custom">
                    <div class="custom-view-all-news">
                        <div id="rs-blog" class="rs-blog main-home md-pt-70 md-pb-70" style="background:#fff;">
                            <div class="row">
                                <asp:Repeater ID="newsRepeater" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-3">
                                            <div class="blog-item custm-blog-item">
                                                <div class="image-part">
                                                    <asp:Image ID="imgCover" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' class="mx-auto custom-image-fit" />
                                                </div>
                                                <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("ImageUrl") + ") scroll no-repeat center center;" %>'></div>
                                                <div class="blog-content">
                                                    <div class="course-date">
                                                        <i class="fa fa-calendar-check-o"></i> <%# Eval("ConvertedDate") %>
                                                    </div>
                                                    <h3 class="title" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                        <asp:Label ID="lblStories" runat="server" Text='<%# Eval("Heading") %>'></asp:Label>
                                                    </h3>
                                                    <div class="btn-btm">
                                                        <div class="rs-view-btn">
                                                            <a name="anchStories" href="ALM_NewDetails.aspx?id=<%# Eval("encId").ToString() %>">View More </a>
                                                        </div>
                                                    </div>
                                                </div>
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
    <!-- News End -->
</asp:Content>