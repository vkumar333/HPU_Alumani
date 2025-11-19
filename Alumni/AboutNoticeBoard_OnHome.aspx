<%--created by:-ayush tyagi
date:-31-01-2023--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="AboutNoticeBoard_OnHome.aspx.cs" Inherits="Alumni_AboutNoticeBoard_OnHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .bg1 {
            z-index: -1;
        }
    </style>
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Details</h1>
                        <ul>
                            <li>
                                <a class="active" href="Alm_Default.aspx">Home</a>
                            </li>
                            <li>Details</li>
                        </ul>
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
                            <div>
                                <h5 class="top-title"><%# Eval("Heading") %></h5>
                                <%--  <span class="date">
                                        <i class="fa fa-calendar" aria-hidden="true"></i><%# Eval("ConvertedDate") %>
                                    </span>--%>
                                <h5 class="description">Description</h5>
                                <p style="text-align: justify;"><%# Eval("Description") %></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<div class="share-section2">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <span>You Can Share It : </span>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <ul class="share-link">
                                    <li><a href="#" style="background: #3b5998;"><i class="fa fa-facebook" aria-hidden="true"></i>Facebook</a></li>
                                    <li><a href="#" style="background: #00acee;"><i class="fa fa-twitter" aria-hidden="true"></i>Twitter</a></li>
                                    <li><a href="#" style="background: #0A66C2;"><i class="fa fa-linkedin" aria-hidden="true"></i>Linkedin</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-1 col-md-12">
        <div class="back-btn">
            <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
        </div>
    </div>
</asp:Content>