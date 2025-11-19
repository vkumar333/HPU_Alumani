<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_alumni_Batchmates.aspx.cs" Inherits="Alumni_Alm_alumni_Batchmates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .social-share {
            margin-top: 0px !important;
        }

        .toglab {
            margin-top: -2rem;
        }
    </style>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg3 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Members</h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->
    <div id="rs-courses-3" class="rs-courses-3 sec-spacer">
        <div class="container">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-6">
                        <div class="sec-title mb-20 md-mb-30">
                            <h2 class="title mb-20">Select Year</h2>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="yeartext">Make sure all of your friends are part of HPU yearbook</div>
                        <div class="social-share mb-3">

                            <label class="toggle toglab" for="toggle9">
                                <input type="checkbox" id="toggle9" style="display: none;" />
                                <div class="btnn">
                                    <i class="fa fa-share-alt"></i>
                                    <i class="fa fa-times"></i>
                                    <div class="social">
                                        <a id="facebookLink" runat="server"><i class="fa fa-facebook"></i></a>
                                        <a id="twitterLink" runat="server"><i class="fa fa-twitter"></i></a>
                                        <a id="linkedInLink" runat="server"><i class="fa fa-linkedin"></i></a>
                                        <a id="youtubeLink" runat="server"><i class="fa fa-youtube"></i></a>
                                    </div>
                                </div>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row grid">
                <asp:Repeater ID="GropbyYear" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-6 grid-item filter1 text-center">
                            <div class="course-item mt-30">
                                <div onclick="location.href='<%# string.Format("/Alumni/Alm_Alumni_Batchmates_member.aspx?ID={0}",HttpUtility.UrlEncode(Eval("encId").ToString())) %>';" style="cursor: pointer;">
                                    <div class="course-img">
                                        <span class="course-value" style="right: 39%;"><%# Eval("yearofPassing") %></span>
                                    </div>
                                    <div class="course-body c<%# Container.ItemIndex + 1 %>">
                                        <div class="course-desc">
                                            <h4 class="course-title mt-4">Class Of <%# Eval("yearofPassing") %></a></h4>
                                            <p>
                                                <i class="fa fa-users"></i> <%# Eval("members") %> Member
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="course-footer d-block">
                                    <asp:HyperLink runat="server" Style="margin-top: 10px;" cursor="pointer" NavigateUrl='<%# string.Format("~/Alumni/Alm_Alumni_Batchmates_member.aspx?ID={0}",HttpUtility.UrlEncode(Eval("encId").ToString())) %>'
                                        Text="View More" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>