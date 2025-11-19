<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Gallery_Lists.aspx.cs" Inherits="Alumni_ALM_Gallery_Lists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Gallery</h1>
                        <%--   <ul>
                            <li>
                                <a class="active" href="Alm_Default.aspx">Home</a>
                            </li>
                            <li>Gallery</li>
                        </ul>--%>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->
    <!-- Main content Start -->
    <div class="main-content">
        <div class="rs-gallery pt-50 md-pt-70 md-pb-70">
            <div class="container">
                <div class="sec-title text-center mb-20 md-mb-30">
                    <h2 class="title mb-20">Gallery</h2>
                </div>
                <div class="row">
                    <asp:Repeater ID="GalleryImages" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 mb-30 col-md-6">
                                <div class="gallery-item">
                                    <div class="gallery-img">
                                        <a href='ALM_Gallery.aspx?Groupid=<%# DataBinder.Eval(Container.DataItem, "encId")%>'>
                                            <img style="width: 100%; height: 220px;" runat="server" id="Image" src='<%# Eval("ImageUrl")%>' alt="">
                                        </a>
                                    </div>
                                    <div class="title">
                                        <a href='ALM_Gallery.aspx?Groupid=<%# DataBinder.Eval(Container.DataItem, "encId")%>'><%# Eval("GroupName") %></a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<div class="col-lg-4 mb-30 col-md-6">
                            <div class="gallery-item">
                                <div class="gallery-img">
                                    <a href="ALM_Gallery.aspx">
                                        <img src="alumin-default-theme/images/2.jpg" alt=""></a>
                                </div>
                                <div class="title">
                                    <a href="ALM_Gallery.aspx">HPU</a>
                                </div>
                            </div>
                        </div>--%>
                </div>
            </div>
        </div>
        <!-- Events Section End -->
    </div>
</asp:Content>