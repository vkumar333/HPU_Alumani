<%--created by:-ayush tyagi--%>

<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Gallery.aspx.cs" Inherits="Alumni_ALM_Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Gallery</h1>
                        <%-- <ul>
                            <li>
                                <a class="active" href="Alm_Default.aspx">Home</a>
                            </li>
                            <li>Gallery</li>
                        </ul>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->
    <div class="main-content">
        <!-- Events Section Start -->
        <div class="rs-gallery pt-50 pb-60 md-pt-70 md-pb-70">
            <div class="container">
                <div>
                    <div style="float: right;">
                        <a class="readon2 banner-style" href="ALM_Gallery_Lists.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i>Back</a>
                    </div>
                </div>
                <div class="sec-title text-center mb-20 md-mb-30">
                    <h2 class="title mb-20"> 
                        <Anthem:Label ID="lblGalleryAlbum" runat="server" AutoUpdateAfterCallBack="true" Font-Bold="true" ForeColor="Black" Text=""></Anthem:Label>
                    </h2> <%--HPU --%>
                </div>
                <div class="row">
                    <asp:Repeater ID="GalleryImages" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 mb-30 col-md-6">
                                <div class="gallery-img">
                                    <a class="image-popup" href="<%# Eval("ImageUrl")%>">
                                        <img style="width: 100%; height: 220px;" runat="server" id="Img1" src='<%# Eval("ImageUrl")%>' alt=""></a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <!-- Events Section End -->
    </div>
</asp:Content>