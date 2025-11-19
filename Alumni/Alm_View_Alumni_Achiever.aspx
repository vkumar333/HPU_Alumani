<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_View_Alumni_Achiever.aspx.cs" Inherits="Alumni_Alm_View_Alumni_Achiever" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
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
                    <asp:Repeater ID="RepeventsAchievers" runat="server">
                        <ItemTemplate>
                            <div>
                                <div class="single-image">
                                    <img id="Imge" height="150" width="200" src='<%# Eval("file_Url") %>' alt="event">
                                </div>
                                <h5 style="margin: 10px 0; padding-top: 10px; text-decoration: underline; font-size: 1.25rem; color: #0a433d;"><%# Eval("alumni_name") %></h5>
                                <h5 style="margin: 10px 0; padding-top: 10px; text-decoration: underline; color: #0a433d;">Achievement </h5>
                                <p style="text-align: justify; margin-top: 0; margin-bottom: 1rem;">
                                    <%# Eval("Achievement") %>
                                </p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-lg-1 col-md-12">
                    <div class="back-btn">
                        <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- event End -->

    <!-- Main content End -->
</asp:Content>