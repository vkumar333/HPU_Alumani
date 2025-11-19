<%@ Page Title="" Language="C#" MasterPageFile="../Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_AboutNoticeBoard.aspx.cs" Inherits="Alumni_ALM_AboutNoticeBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Notice Board Details</h1>
                        <%--<ul>
                            <li>
                                <a class="active" href="Alm_Default.aspx">Home</a>
                            </li>
                            <li>Notice Board Details</li>
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
                <div class="col-lg-12 col-md-12">
                       <%-- <asp:Repeater ID="RepeventsAll" runat="server">
                            <ItemTemplate>
                                <div class="single-image">
                            <img id="Imge" height="300" width="500" src='<%# Eval("Filepath") %>' alt="event" runat="server">
                            <br />
                            <br />
                        </div>
                                <div>
                                    <h5 class="top-title"><%# Eval("Heading") %></h5>
                                    <h5 class="description">Description: </h5>
                                    <p style="text-align: justify;"><%# Eval("Description") %></p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>--%>
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

                    <div style="border: 1px solid #e1e1e1; padding: 15px;">
                        <div class="single-image">
                            <img id="Imge" height="300" width="500" src='<%# Eval("Filepath") %>' alt="event" runat="server">
                            <br />
                            <br />
                        </div>

                        <div class="mb-30">
                            <h5 style="margin: 10px 0; padding-top: 10px; text-decoration: underline; font-size: 1.25rem; color: #0a433d;">
                                <Anthem:Label ID="lblHeading" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                            </h5>
                            <span class="date">
                                <i class="fa fa-calendar" aria-hidden="true"></i>
                                <Anthem:Label ID="lblStartDate" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                            </span>
                            <h5 style="margin: 10px 0; padding-top: 10px; text-decoration: underline; color: #0a433d;">Description: </h5>
                            <p style="text-align: justify; margin-top: 0; margin-bottom: 1rem;">
                                <Anthem:Label ID="lblDescription" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                            </p>
                            <div style="border: 1px solid #ddd; padding: 15px 15px 15px 15px;">
                                <div class="row">
                                    <div class="col-lg-12 col-md-6 col-sm-12">
                                        <span>You Can Share It : </span>
                                    </div>
                                    <div class="col-lg-12 col-md-6 col-sm-12">
                                        <ul style="float: right;">
                                            <li style="float: left; list-style: none; margin: 20px 0;"><a class="mb-3" href="https://www.facebook.com/HpuNewShimla" style="background: #3b5998; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-facebook" aria-hidden="true"></i>Facebook</a></li>
                                            <li style="float: left; list-style: none; margin: 20px 0;"><a class="mb-3" href="https://twitter.com/hpu_shimla?t=lRks6KUPBVeBjXTZgzNs_Q&s=08" style="background: #00acee; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-twitter" aria-hidden="true"></i>Twitter</a></li>
                                            <li style="float: left; list-style: none; margin: 20px 0;"><a class="mb-3" href="https://in.linkedin.com/school/himachal-pradesh-university/" style="background: #0A66C2; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-linkedin" aria-hidden="true"></i>Linkedin</a></li>
                                        </ul>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>            
            <div class="col-lg-1 col-md-12" style="margin-top: 15px;">
                <div class="back-btn">
                    <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>