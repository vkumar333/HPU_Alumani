<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_GalleriesVideos.aspx.cs" Inherits="Alumni_ALM_GalleriesVideos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

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

    <%-- <div class="rs-history mt-50">
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
	
    <div class="container-fluid mt-10">
        <div class="">
            <div class="box box-success" runat="server">
                <div class="boxhead">
                    Video Gallery
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="">
                        <div id="rs-blog" class="rs-blog main-home pb-70 md-pt-70 md-pb-70">
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
                <div class="col-lg-1 col-md-12">
                    <div class="back-btn">
                        <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>