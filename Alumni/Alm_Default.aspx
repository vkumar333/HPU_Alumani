<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_Default.aspx.cs" Inherits="Alumni_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .ovrflw .scroll-flw {
            height: 417px;
        }
    </style>

    <!-- Slider Area Start -->
    <%--<div id="rs-slider" class="slider-section4 slider-overlay-2">
        <div id="home-slider">
            <!-- Item 1 -->
            <asp:Repeater ID="sliderRepeater" runat="server">
                <ItemTemplate>
                    <div class="item active">
                         <img class="img-responsive" runat="server" id="Imge" src='<%# Eval("Filepath") %>' alt="" style="width:1423px; height:608px;" >
                        <div class="slide-content">
                            <div class="display-table">
                                <div class="display-table-cell">
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>--%>

    <div id="carouselExampleIndicators" class="carousel slide carousel-fade" data-ride="carousel" data-interval="5000">
        <%-- <ol class="carousel-indicators">
            <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
        </ol> --%>
        <div class="carousel-inner">
            <asp:Repeater ID="sliderRepeater" runat="server">
                <ItemTemplate>
                    <div class="carousel-item <%#GetActiveClass(Container.ItemIndex) %>">
                        <div class="alumni-slider-main">
                            <img class="img-fluid d-block mx-auto" runat="server" id="Imge" src='<%# Eval("Filepath") %>' alt="" />
                        </div>
                        <div class="alumni-slider-main-bg" style='<%# "background:url(" + Eval("Filepath") + ") scroll no-repeat center center;" %>'>></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>
    <!-- Slider Area End -->
	
    <!-- Main content Start -->
    <div class="main-content">
        <div id="rs-about" class="rs-about style1 pt-20 pb-15 md-pb-70">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-4 order-last">
                        <div class="notice-bord style1 ovrflw">
                            <h4 class="title">Notice Board</h4>
                            <div class="scroll-flw">
                                <Anthem:Repeater ID="RepterDetails" runat="server">
                                    <ItemTemplate>
                                        <ul>
                                            <li class="wow fadeInUp">
                                                <div class="date"><span><%# Eval("DayDate") %></span><%# Eval("Month_Name") %></div>
                                                <div class="desc">
                                                    <div class="row">
                                                        <div class="col-md-10">
                                                            <a style="color: black"><%# Eval("Heading") %></a>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="btn-btm">
                                                                <div class="rs-view-btn">
                                                                    <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/Aboutnoticeboard.aspx?ID={0}",
                                            HttpUtility.UrlEncode(Eval("encId").ToString())) %>' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <div></div>
                                        </ul>
                                    </ItemTemplate>
                                </Anthem:Repeater>
                            </div>
                            <a class="readon2 banner-style mt-2" data-toggle="modal" href="#" data-target="#exampleModalScrollable">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                        </div>
                    </div>
                    <div class="col-lg-8 pr-50 md-pr-15">
                        <div class="about-part">
                            <div class="sec-title">
                                <div class="sub-title primary wow fadeInUp">About Us</div>
                                <h2 class="title wow fadeInUp">University Profile</h2>
                                <div class="desc wow fadeInUp">
                                    Himachal Pradesh University was established by an Act of the Legislative Assembly of Himachal Pradesh on 22 July 1970 as a response to the needs and aspirations of the Union Territory, poised for full statehood in the Union of India on 25 January 1971. It is the only multi-faculty residential and affiliating university in the State that provides higher education to urban, rural and tribal areas through formal and distant modes.
                                </div>
                                <div class="desc wow fadeInUp">
                                    The headquarters of the University is located at Summer Hill, the picturesque suburb of Shimla. The University has a total area of 241.11 bighas with stately buildings set among rhododendron, silver oak, pine and deodar trees. It affords a salubrious clime and congenial atmosphere for reflection, study and research. The prime objective of the University is to disseminate knowledge, advance learning and understanding through research, training and extension programmes.
                                    It instills in its students and teachers a conscious awareness regarding the social and economic needs, cultural ethos, and future requirements of the state and the country.
                                </div>
                                <a class="readon2 banner-style" href="Alm_About.aspx">Read More <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="rs-blog1" class="rs-blog main-home pb-15 pt-45 md-pt-70 md-pb-70" style="background: #fff;">
            <div class="container-fluid">
                <div class="sec-title text-center mb-50 md-mb-30">
                    <div class="sub-title primary">Latest </div>
                    <h2 class="title mb-0">Notable Alumni </h2>
                </div>
                <div class="rs-carousel owl-carousel" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="false" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="1" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="false" data-md-device-dots="false">
                    <asp:Repeater ID="rptNotableAlumni" runat="server">
                        <ItemTemplate>
                            <div class="blog-item">
                                <div class="image-part">
                                    <img runat="server" id="Image" src='<%# Eval("PicUrl") %>' alt="" class="mx-auto">
                                </div>
                                <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("PicUrl") + ") scroll no-repeat center center;" %>'>></div>
                                <div class="blog-content">
                                    <h3 class="title"><a style="color: black"><%# Eval("Name") %></a></h3>
                                    <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"><%# Eval("Subheading") %></div>
                                    <div class="btn-btm">
                                        <div class="rs-view-btn">
                                            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/ALM_NotableAlumni_Details.aspx?ID={0}",
                                            HttpUtility.UrlEncode(Eval("encId").ToString())) %>'
                                                Text="Read More" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-lg-12 text-center pt-20">
                    <a class="readon green-btn" href="ALM_NotableAlumni_Lists.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>
        <div id="rs-blog" class="rs-blog main-home pb-15 pt-45 md-pt-70 md-pb-70">
            <div class="container-fluid">
                <div class="sec-title text-center mb-50 md-mb-30">
                    <div class="sub-title primary">Latest </div>
                    <h2 class="title mb-0">News & Stories</h2>
                </div>

                <div class="rs-carousel owl-carousel" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="false" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="1" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="false" data-md-device-dots="false">
                    <asp:Repeater ID="RepeaterNewsStories" runat="server">
                        <ItemTemplate>
                            <div class="blog-item">
                                <div class="image-part">
                                    <img runat="server" id="Image" src='<%# Eval("ImageUrl") %>' alt="" class="mx-auto">
                                </div>
                                <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("ImageUrl") + ") scroll no-repeat center center;" %>'>></div>
                                <div class="blog-content">
                                    <ul class="blog-meta">
                                        <li><i class="fa fa-calendar"></i><%# Eval("ConvertedDate") %></li>
                                    </ul>
                                    <h3 class="title"><a style="color: black"><%# Eval("Heading") %></a></h3>
                                    <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"><%# Eval("Description") %></div>
                                    <div class="btn-btm">
                                        <div class="rs-view-btn">
                                            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/Alm_NewsandEvents_por.aspx?ID={0}",
                                            HttpUtility.UrlEncode(Eval("encId").ToString())) %>'
                                                Text="Read More" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-lg-12 text-center pt-20">
                    <a class="readon green-btn" href="Alm_NewsandStoriesList.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>
        <div id="rs-blog" class="rs-blog colour main-home pb-45 pt-45 md-pt-70 md-pb-70">
            <div class="container-fluid">
                <div class="sec-title text-center mb-50 md-mb-30">
                    <div class="sub-title primary">Latest </div>
                    <h2 class="title mb-0">Events</h2>
                </div>
                <div class="rs-carousel owl-carousel" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="false" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="1" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="false" data-md-device-dots="false">
                    <asp:Repeater ID="RepeaterEvents" runat="server">
                        <ItemTemplate>
                            <div class="blog-item">
                                <div class="image-part">
                                    <img runat="server" id="Imge" src='<%# Eval("Filepath") %>' alt="" class="mx-auto">
                                </div>
                                <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("Filepath") + ") scroll no-repeat center center;" %>'>></div>
                                <div class="blog-content">
                                    <ul class="blog-meta">
                                        <li><i class="fa fa-calendar"></i><%# Eval("Start_date") %></li>
                                    </ul>
                                    <h3 class="title"><a href="javascript:void(0);"><%# Eval("Event_name") %></a></h3>
                                    <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"><%# Eval("Description") %></div>
                                    <div class="btn-btm">
                                        <div class="rs-view-btn">
                                            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/Alm_Events.aspx?ID={0}",HttpUtility.UrlEncode(Eval("encId").ToString())) %>' Text="Read More" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-lg-12 text-center pt-20">
                    <a class="readon green-btn" href="Alm_Events_List.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>
        <!-- Team Section Start -->
        <div id="rs-team" class="rs-team style1 pt-45 pb-45 md-pt-64 md-pb-70">
            <div class="container-fluid">
                <div class="sec-title text-center mb-50 md-mb-30">
                    <div class="sub-title primary">Gallery </div>
                    <h2 class="title mb-0">Albums and Photos</h2>
                </div>
                <div class="rs-carousel owl-carousel nav-style2" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="true" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="2" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="true" data-md-device-dots="false">
                    <asp:Repeater ID="GalleryImages" runat="server">
                        <ItemTemplate>
                            <div class="team-item">
                                <div class="image-part">
                                    <img runat="server" id="Image2" src='<%# Eval("ImageUrl")%>' class="mx-auto" />
                                </div>
                                <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("ImageUrl") + ") scroll no-repeat center center;" %>'>></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-lg-12 text-center pt-20">
                    <a class="readon green-btn" href="ALM_Gallery_Lists.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>

        <div class="style1 pt-45 pb-45 md-pt-64 md-pb-70">
            <div class="container-fluid">
                <div class="row">
                    <asp:Repeater ID="rptGalleryVideos" runat="server">
                        <ItemTemplate>
                            <div class="col-sm-3">
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
                    </asp:Repeater>
                    <div class="col-lg-12 text-center pt-20">
                        <a class="readon green-btn" href="ALM_GalleryVideos.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                    </div>
                </div>
            </div>
        </div>

        <!-- Contact Info -->
        <!--<div id="rs-contactus" class="rs-about style1 pt-20 md-pb-70">
            <div class="about-part">
                <div style="flex: 1; min-width: 300px;" class="text-center">
                    <h3>Contact Info</h3>
                    <p>Himachal Pradesh University, Summer Hill, Shimla-171005</p>
                    <p><a href="https://www.hpuniv.ac.in" target="_blank"><i class="fa fa-link"></i>&nbsp; www.hpuniv.ac.in</a></p>
                    <p><a href="mailto:hpualumniassociation@gmail.com"><i class="fa fa-envelope-o"></i>&nbsp;hpualumniassociation@gmail.com</a></p>
                    <p>
                        <a id="facebookLink" runat="server">
                            <img src="alumin-default-theme/images/facebook.png" alt="Facebook" width="24" />
                        </a>
                        <a id="twitterLink" runat="server">
                            <img src="alumin-default-theme/images/twitter.png" alt="Twitter/X" width="24" />
                        </a>
                        <a id="linkedInLink" runat="server" >
                            <img src="alumin-default-theme/images/linkedin.png" alt="LinkedIn" width="24" />
                        </a>
                        <a id="youtubeLink" runat="server" >
                            <img src="alumin-default-theme/images/youtube.png" alt="YouTube" width="24" />
                        </a>
                    </p>
                </div>

            </div>
        </div>-->

        <!-- Team Section End -->
    </div>
	
    <!-- Main content End -->
    <div class="modal fade" id="exampleModalScrollable" tabindex="-1" role="dialog" aria-labelledby="exampleModalScrollableTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalScrollableTitle">Notice Board</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="notice-bord style1 ovrflw">
                        <div class="scroll-flw">
                            <Anthem:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <div class="date"><span><%# Eval("DayDate") %></span><%# Eval("Month_Name") %></div>
                                            <div class="desc"><%--<a style="color: black" href="#">--%><%# Eval("Heading") %><%--</a>--%></div>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </Anthem:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

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

</asp:Content>