<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_NewsandStoriesList.aspx.cs" Inherits="Alumni_Alm_NewsandStoriesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .ovrflw .scroll-flw {
            padding: 0 22px;
            height: 422px;
        }
        .ovrflw .scroll-flw::-webkit-scrollbar-thumb {
            background: #266f68;
        }
        .btn-more a {
            display: inline-block;
            margin-left: auto;
            padding: 12px 25px;
            font-size: 13px;
            font-weight: 500;
            text-decoration: none;
            background-color: transparent;
            color: #f54828;
            border: 1px solid #f54828;
        }
            .btn-more a:hover {
                background-color: #f54828;
                color: #fff;
            }
    </style>

    <div class="rs-events-list newsandstorieslist-css sec-spacer">
        <div class="container">
            <div class="row">

                <div class="col-md-6">
                    <div class="sec-title">
                        <h2>
                            <img src="alumin-default-theme/images/news.png" style="width: 25px; height: 25px;" />&nbsp;&nbsp;News 
                        </h2>
                    </div>
                    <div class="news-list-block ovrflw">
                        <div class="scroll-flw">
                            <Anthem:Repeater ID="newsRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="row evnets-item">

                                        <div class="col-md-2">
                                            <div class="evnets-img">
                                                <img runat="server" id="Imge1" src='<%# Eval("ImageUrl")%>' alt="news" class="mx-auto" style="height: 50px;" />
                                            </div>
                                        </div>
                                        <div
                                            class="col-md-10">
                                            <div class="course-header">
                                                <h3 class="course-title"><%# Eval("Heading") %></h3>
                                                <div class="course-date">
                                                    <i class="fa fa-calendar-check-o"></i> <%# Eval("ConvertedDate") %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </Anthem:Repeater>
                        </div>
                        <div class="clearfix"></div>
                        <div class="view-more btn-more pull-right">
                            <a href="ALM_View_All_News.aspx?type=news">View All News <i class="fa fa-angle-double-right"></i></a>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="sec-title">
                        <h2>
                            <img src="alumin-default-theme/images/stories.png" style="width: 25px;" />&nbsp;&nbsp;Stories </h2>
                    </div>
                    <div class="clearfix"></div>
                    <div class="news-list-block ovrflw">
                        <div class="scroll-flw">
                            <Anthem:Repeater ID="storiesRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="row evnets-item">
                                        <div class="col-md-2">
                                            <div class="evnets-img">
                                                <img runat="server" id="Imge1" src='<%# Eval("ImageUrl")%>' alt="news" class="mx-auto" style="height: 50px;" />
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="course-header">
                                                <h3 class="course-title"><%# Eval("Heading") %></h3>
                                                <div class="course-date">
                                                    <i class="fa fa-calendar-check-o"></i> <%# Eval("ConvertedDate") %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </Anthem:Repeater>
                        </div>
                        <div class="clearfix"></div>
                        <div class="view-more btn-more pull-right">
                            <a href="ALM_View_All_Stories.aspx?type=stories">View All Stories<i class="fa fa-angle-double-right"></i></a>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>