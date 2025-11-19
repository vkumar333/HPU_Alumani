<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Notable_Details.aspx.cs" Inherits="Alumni_ALM_Notable_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--<div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <!-- Breadcrumbs End -->

    <!-- Courses Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <%--<asp:Repeater ID="RepeventsAll" runat="server">
                        <ItemTemplate>--%>
                    <div style="border: 1px solid #e1e1e1; padding: 15px;">
                        <div class="single-image">
                            <img id="Imge" style="width: 100%; max-width: 50%; height: 400px;" src='<%# Eval("Filepath") %>' alt="event" runat="server">
                            <br />
                            <br />
                        </div>

                        <div class="mb-30">
                            <h5 style="margin: 10px 0; padding-top: 10px; text-decoration: underline; font-size: 1.25rem; color: #0a433d;">
                                <Anthem:Label ID="lblName" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                            </h5>
                            <span class="date">
                                <i class="" aria-hidden="true"></i>
                                <h4>
                                    <Anthem:Label ID="lblSubHeading" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label></h4>
                            </span>
                            <h5 style="margin: 10px 0; padding-top: 10px; text-decoration: underline; color: #0a433d;">Description: </h5>
                            <p style="text-align: justify; margin-top: 0; margin-bottom: 1rem;">
                                <Anthem:Label ID="lblDescription" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                <br />
                                <br />
                                <%--<Anthem:HyperLink ID="lnkWebsite" runat="server" Target="_blank" Visible="false" AutoUpdateAfterCallBack="true" CssClass="anchor-style"></Anthem:HyperLink>--%>
                                <%--<a id="anchorLink" name="anchor" runat="server" class="link-style"></a>--%>
                                <a id="anchorLink" name="anchor" runat="server" href='<%# Eval("WebsiteLinks") %>' class="anchor-style" target="_blank">Visit Website Link</a>
                                <br />
                                <br />
                            </p>
                            <div style="border: 1px solid #ddd; padding: 15px 15px 15px 15px;">
                                <div class="row">
                                    <div class="col-lg-12 col-md-6 col-sm-12">
                                        <span>You Can Share It : </span>
                                    </div>
                                    <div class="col-lg-12 col-md-6 col-sm-12">
                                        <ul style="float: right;">
                                            <li style="float: left; list-style: none; margin: 20px 0;"><a id="facebookLink" runat="server" class="mb-3" style="background: #3b5998; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-facebook" aria-hidden="true"></i>Facebook</a></li>
                                            <li style="float: left; list-style: none; margin: 20px 0;"><a id="twitterLink" runat="server" class="mb-3" style="background: #00acee; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-twitter" aria-hidden="true"></i>Twitter</a></li>
                                            <li style="float: left; list-style: none; margin: 20px 0;"><a id="linkedInLink" runat="server" class="mb-3" style="background: #0A66C2; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-linkedin" aria-hidden="true"></i>Linkedin</a></li>
											<li style="float: left; list-style: none; margin: 20px 0;"><a id="youtubeLink" runat="server" class="mb-3" style="background: #FF0000; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-youtube" aria-hidden="true"></i>Youtube</a></li>
                                        </ul>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-1 col-md-12" style="margin-top: 15px;">
                    <div class="back-btn">
                        <a href="ALM_Notable_Lists.aspx">Back</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- event End -->

    <!-- Main content End -->
</asp:Content>