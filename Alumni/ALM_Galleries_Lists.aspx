<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Galleries_Lists.aspx.cs" Inherits="Alumni_ALM_Galleries_Lists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <style>
        .bg1 {
            z-index: -1 !important;
        }

        .back-btn {
            position: absolute;
            top: -10px;
            background: #d1595e;
        }
    </style>

    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Gallery</h1>
                        <%--   <ul>
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
                                        <a href='ALM_Gallery.aspx?Groupid=<%# DataBinder.Eval(Container.DataItem, "Pk_Groupid")%>'>
                                            <img style="width: 100%; height: 220px;" runat="server" id="Image" src='<%# "~/UploadedImg/"+Eval("photofilename")%>' alt="">
                                        </a>
                                    </div>
                                    <div class="title">
                                        <a href='ALM_Gallery.aspx?Groupid=<%# DataBinder.Eval(Container.DataItem, "Pk_Groupid")%>'><%# Eval("GroupName") %></a>
                                    </div>
                                </div>

                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-lg-1 col-md-12">
                <div class="back-btn">
                    <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
                </div>
            </div>
        </div>
        <!-- Events Section End -->
    </div>
</asp:Content>