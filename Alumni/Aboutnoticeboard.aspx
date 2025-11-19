<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Aboutnoticeboard.aspx.cs" Inherits="Alumni_Aboutnoticeboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Notice Details</h1>
                        <%--<ul>
                            <li>
                                <a class="active" href="Alm_Default.aspx">Home</a>
                            </li>
                            <li>Details</li>
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
                <div class="col-lg-8 col-md-12" style="border: 1px solid #e1e1e1; padding: 15px;">
                        <asp:Repeater ID="RepeventsAll" runat="server">
                            <ItemTemplate>
                                <div>
								<div class="single-image">
                                    <img id="Imge" src='<%# Eval("Filepath") %>' alt="event">
                                </div>
                                    <h5 class="top-title"><%# Eval("Heading") %></h5>
                                  <%--  <span class="date">
                                        <i class="fa fa-calendar" aria-hidden="true"></i><%# Eval("ConvertedDate") %>
                                    </span>--%>
                                    <h5 class="description">Description</h5>
                                    <p style="text-align: justify;"><%# Eval("Description") %></p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    <div class="share-section2">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12">
                                <span>You Can Share It : </span>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-12">
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
				<div class="col-md-4">
                    <div style="float: right;">
                        <a class="readon2 banner-style" href="Alm_Default.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i>Back</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

