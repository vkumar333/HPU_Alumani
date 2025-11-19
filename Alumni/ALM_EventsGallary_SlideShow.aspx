<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_EventsGallary_SlideShow.aspx.cs" Inherits="Alumni_ALM_EventsGallary_SlideShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid mt-10">
        <div class="">
            <div class="box box-success">
				<div class="boxhead mt-10 mb-10">
                    &nbsp;Gallery
					<a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/Alumni_EventsGallery_View.aspx">Back </a>
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="rs-gallery pb-60 md-pt-70 md-pb-70">
                        <div class="row">

                            <asp:Repeater ID="galleryCoverRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3">
                                        <div class="gallery-img" style="margin-bottom:20px;">
                                            <a class="image-popup" href='<%# Eval("ImageUrl")%>' style="height: 220px;">
                                                <img class="img-responsive" src='<%# Eval("ImageUrl")%>' alt="" style="height: 220px;width:100%;">
                                            </a>

                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                    </div>
                </div>
				<%--<div class="col-lg-1 col-md-12" style="margin-top: 15px;">
                    <div class="back-btn">
                        <a href="Alumni_EventsGallery_View.aspx">Back</a>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>

    <script src="Plc_default_Theme/js/lightbox-plus-jquery.min.js"></script>
    <script src="alumin-default-theme/js/jquery.magnific-popup.min.js"></script>

</asp:Content>