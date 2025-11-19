<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_Events.aspx.cs" Inherits="Alumni_Alm_Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Events</h1>
                        <%-- <ul>
                            <li>
                                <a class="active" href="Default.aspx">Home</a>
                            </li>
                            <li>Events</li>
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
                <div class="col-lg-10 col-md-12">
                    <asp:Repeater ID="RepeventsAll" runat="server" OnItemDataBound="RepeventsAll_ItemDataBound">
                        <ItemTemplate>
                            <div>
                                <div class="single-image">
                                    <img id="Imge" src='<%# Eval("Filepath") %>' alt="event" class="mx-auto">
                                </div>
                                <h5 class="top-title"><%# Eval("Event_name") %></h5>
                                <span class="date">
                                    <i class="fa fa-calendar" aria-hidden="true"></i> <%# Eval("Start_date") %>
                                </span>
                                <h5 class="description">Description</h5>
                                <p style="text-align: justify;"><%# Eval("Description") %></p>
                                <br />

                                <Anthem:Label ID="lblEventLink" AutoUpdateAfterCallBack="true" runat="server" Visible="false" ForeColor="Black"></Anthem:Label>
                                <Anthem:Label ID="lblColon" AutoUpdateAfterCallBack="true" runat="server" Visible="false" ForeColor="Black"></Anthem:Label>
                                <a id="anchorEventLink" name="anchor" runat="server" style="color: #0B6790;"></a>
                                <br />
                                <br />

                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="share-section2">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <span>You Can Share It : </span>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <%--<ul class="share-link">
                                    <li><a href="https://www.facebook.com/HpuNewShimla" style="background: #3b5998;"><i class="fa fa-facebook" aria-hidden="true"></i>Facebook</a></li>
                                    <li><a href="https://twitter.com/hpu_shimla?t=lRks6KUPBVeBjXTZgzNs_Q&s=08" style="background: #00acee;"><i class="fa fa-twitter" aria-hidden="true"></i>Twitter</a></li>
                                    <li><a href="https://in.linkedin.com/school/himachal-pradesh-university/" style="background: #0A66C2;"><i class="fa fa-linkedin" aria-hidden="true"></i>Linkedin</a></li>
                                </ul>--%>
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
                        <a class="readon2 banner-style" href="Alm_Events_List.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i>Back</a>
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
    <!-- event End -->


    <!-- Main content End -->
</asp:Content>