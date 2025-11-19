<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_View_All_Stories.aspx.cs" Inherits="Alumni_ALM_View_All_Stories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../include/jquery-1.2.1.min.js"></script>
    <style>
        .popupbox .vtext {
            width: 50%;
        }

        .popupboxouter {
            width: 540px;
        }

        .popupbox {
            width: 515px;
        }

        .modal-header {
            background: #016773;
        }

            .modal-header .close {
                margin-top: -25px;
                color: #fff;
                font-size: 25px;
            }

        .modal-title {
            color: #fff;
            font-size: 15px;
            font-weight: 500;
        }

        .popbutton {
            background-color: #f0ad4e;
            border: 1px solid #eea236;
            color: #FFFFFF;
            cursor: pointer;
            font-weight: 600;
            padding: 5px 6px;
            border-radius: 5px 5px 5px 5px;
        }

            .popbutton:hover {
                color: #fff;
            }

        .rs-blog {
            background: #ffffff;
        }

            .rs-blog.main-home .blog-item .blog-content {
                /*min-height: 335px;*/
                margin-bottom: 30px;
            }

        .custom-image-fit {
            width: fit-content !important;
            height: 100%;
            display: block;
        }

        .blog-item.custm-blog-item .alumni-slider-thumb {
            width: 98%;
        }

        .blog-item.custm-blog-item {
            position: relative;
            display: block;
        }

        .rs-blog.main-home .blog-item.custm-blog-item:hover .alumni-slider-thumb {
            width: 98%;
        }

        @media (max-width:732px) and (min-width:320px) {
            .popupbox .vtext {
                width: 100%;
            }
        }
    </style>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Stories </h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_NewsandStoriesList.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <!-- Stories Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container-fluid">

            <div class="box box-success" runat="server">
                <div class="panel-body pnl-body-custom">
                    <div class="">
                        <div id="rs-blog" class="rs-blog main-home md-pt-70 md-pb-70">
                            <div class="row">
                                <asp:Repeater ID="storiesRepeater" runat="server">
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
                                                            <a name="anchStories" href="ALM_StoriesDetails.aspx?id=<%# Eval("encId").ToString() %>">View More </a>
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
    <!-- Stories End -->
</asp:Content>