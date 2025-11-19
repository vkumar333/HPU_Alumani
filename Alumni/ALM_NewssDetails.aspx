<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_NewssDetails.aspx.cs" Inherits="Alumni_ALM_NewssDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">News Details </h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

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
                        <a class="readon2 banner-style" href="ALM_View_All_Newss.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i>Back</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>