<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_GalleryVideos.aspx.cs" Inherits="Alumni_ALM_GalleryVideos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        div#Anthem_ctl00_ContentPlaceHolder1_RepeaterVideoGallery__ {
            width: 100%;
            display: contents;
        }

        .video-wrapper iframe {
            width: 100% !important;
        }
    </style>

    <script src="https://www.youtube.com/iframe_api"></script>

    <script type="text/javascript">
        var players = [];

        // YouTube API Ready
        function onYouTubeIframeAPIReady() {
            var divs = document.querySelectorAll("[id^='player_']");
            divs.forEach(function (div, index) {
                var videoId = div.getAttribute("data-videoid");
                players[index] = new YT.Player(div.id, {
                    height: '250',
                    width: '290',
                    videoId: videoId,
                    events: {
                        'onStateChange': onPlayerStateChange
                    }
                });
            });
        }

        function onPlayerStateChange(event) {
            if (event.data == YT.PlayerState.PLAYING) {
                // Pause all other videos
                players.forEach(function (p) {
                    if (p !== event.target) {
                        p.pauseVideo();
                    }
                });
            }
        }
    </script>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Video Gallery </h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <%--    <div class="rs-history mt-50">
        <div class="container-fluid">
            <div class="sec-title mt-30">
                <h2>Video Overview</h2>
            </div>
            <div class="gallery-list-grid custm-gallery-list-grid">
                <div class="row">
                    <Anthem:Repeater ID="RepeaterVideoGallery" runat="server">
                        <ItemTemplate>
                            <div class="col-md-3">
                                <div class="video-wrapper">
                                    <div id='player_<%# Container.ItemIndex %>' data-videoid='<%# Eval("VideoId") %>' width="100%" height="250"></div>
                                    <div class="card-desc">
                                        <p class="description-youtube">
                                            <Anthem:Label ID="LblVideoDesc" runat="server" Text='<%# Eval("Title") %>' AutoUpdateAfterCallBack="true"></Anthem:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </Anthem:Repeater>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="rs-events-list sec-spacer">
        <div class="container-fluid">
            <div class="box box-success" runat="server">
                <div class="panel-body pnl-body-custom">
                    <div class="">
                        <div id="rs-blog" class="rs-blog main-home pb-70 md-pt-70 md-pb-70">
                            <div class="row">
                                <Anthem:Repeater ID="RepeaterVideoGallery" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-3">
                                            <div class="video-wrapper">
                                                <div id='player_<%# Container.ItemIndex %>' data-videoid='<%# Eval("VideoId") %>' width="100%" height="250"></div>
                                                <div class="card-desc">
                                                    <p class="description-youtube">
                                                        <Anthem:Label ID="LblVideoDesc" runat="server" Text='<%# Eval("Title") %>' AutoUpdateAfterCallBack="true"></Anthem:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </Anthem:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>