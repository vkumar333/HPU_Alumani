<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_NotableAlumni_Details.aspx.cs" Inherits="Alumni_ALM_NotableAlumni_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        a.anchor-style {
            color: #0099ff;
            text-decoration: none;
            font-weight: bold;
            font-size: 12px;
        }

            a.anchor-style:hover {
                color: #0056b3;
                text-decoration: underline;
            }
    </style>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Notable Alumni </h1>
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
                                    <img runat="server" id="Imge" src='<%# Eval("PicUrl")%>' alt="" style="width: 100%; max-width: 50%; height: 400px;">
                                </div>
                                <h5 class="top-title"><%# Eval("SubHeading") %></h5>
                                <h5 class="description">Description</h5>
                                <p style="text-align: justify;">
                                    <%# Eval("Comments") %>
                                </p>
                                <%--<Anthem:HyperLink ID="lnkWebsite" runat="server" Target="_blank" AutoUpdateAfterCallBack="true" NavigateUrl='<%# Eval("WebsiteLinks") %>' style="color: #0099ff; text-decoration: none; font-weight: bold; font-size: 16px;"></Anthem:HyperLink>--%>
                                <a id="anchorLink" name="anchor" runat="server" href='<%# Eval("WebsiteLinks") %>' class="anchor-style" target="_blank">Visit Website Link</a>
                                <br /><br />
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
                        <a class="readon2 banner-style" href="ALM_NotableAlumni_Lists.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i>Back</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- event End -->
    <!-- Main content End -->
</asp:Content>