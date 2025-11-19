<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_alumni_Batchmetes_ShortProfile.aspx.cs" Inherits="Alumni_Alm_alumni_Batchmetes_ShortProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .course-seats {
            margin: 0 !important;
        }

        .rs-courses-3 .course-footer {
            display: block;
        }
    </style>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg3 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Members</h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_alumni_Batchmates.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <div id="rs-courses-3" class="rs-courses-3 sec-spacer">
        <div class="container">
            <asp:Repeater ID="headingrep" runat="server">
                <ItemTemplate>
                    <div class="sec-title mb-20 md-mb-30">
                        <h2 class="title mb-20"><%#Eval("member") %> Member From <%#Eval("degres") %>, Year <%#Eval("yearofpassing") %></h2>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="row grid">
                <asp:Repeater ID="profileRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-6 grid-item filter1">
                            <div onclick="location.href='<%# string.Format("/Alumni/Alm_alumni_Batchmates_fullProfile.aspx?ID={0}",HttpUtility.UrlEncode(Eval("encId").ToString())) %>';" style="cursor: pointer;">
                                <div class="course-item mt-30">
                                    <div class="course-img">
                                        <img id="Imge" src='<%# Eval("Filepath") %>' alt="profile">
                                        <span class="course-value corvalue"><%#Eval("yearofpassing") %></span>
                                    </div>
                                    <div class="course-body c1">
                                        <div class="course-desc">
                                            <h4 class="course-title"><%#Eval("alumni_name") %></a></h4>
                                            <p>
                                                <%#Eval("Current_Location") %>, Himachal Pradesh
                                            </p>
                                        </div>
                                    </div>
                                    <div class="course-footer text-center">
                                        <div class="course-seats">
                                            <i class="fa fa-user" aria-hidden="true"></i>
                                            <a>
                                                <asp:HyperLink runat="server" Style="margin-top: 10px;" cursor="pointer" NavigateUrl='<%# string.Format("~/Alumni/Alm_alumni_Batchmates_fullProfile.aspx?ID={0}",HttpUtility.UrlEncode(Eval("encId").ToString())) %>'
                                                    Text="View More" /></a>
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
</asp:Content>