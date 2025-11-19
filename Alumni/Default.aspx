<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Alumni_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .rs-about.style1 .desc .btn-btm .rs-view-btn a:after {
    content: "\f054";
    font-family: fontawesome;
    font-size: 10px;
    position: relative;
    right: 0px;
    top: 0px;
    display: inline-block;
    margin-left: 3px;
}
    </style>
    <!-- Slider Area Start -->
    <div id="rs-slider" class="slider-section4 slider-overlay-2">
        <div id="home-slider">
            <!-- Item 1 -->
            <div class="item active">
                <img src="alumin-default-theme/images/slider/1.jpg" alt="Slide1" />
                <div class="slide-content">
                    <div class="display-table">
                        <div class="display-table-cell">
                            <!--<div class="container">
                                <h1 class="slider-title" data-animation-in="fadeInLeft" data-animation-out="animate-out">WELCOME<br>TO OUR UNIVERSITY</h1>
                                <p data-animation-in="fadeInUp" data-animation-out="animate-out" class="slider-desc">Fusce sem dolor, interdum in efficitur at, faucibus nec lorem.Sed nec molestie justo.<br> Nunc quis sapien in arcu pharetra volutpat.Morbi nec vulputate dolor.</p>
                                <a href="#" class="sl-readmore-btn mr-30" data-animation-in="lightSpeedIn" data-animation-out="animate-out">READ MORE</a>
                                <a href="#" class="sl-get-started-btn" data-animation-in="lightSpeedIn" data-animation-out="animate-out">GET STARTED NOW</a>
                            </div>-->
                        </div>
                    </div>
                </div>
            </div>
            <!-- Item 2 -->
            <div class="item">
                <img src="alumin-default-theme/images/slider/2.jpg" alt="Slide2" />
                <div class="slide-content">
                    <div class="display-table">
                        <div class="display-table-cell">
                        </div>
                    </div>
                </div>
            </div>
            <!-- Item 3 -->
            <div class="item">
                <img src="alumin-default-theme/images/slider/3.jpg" alt="Slide3" />
                <div class="slide-content">
                    <div class="display-table">
                        <div class="display-table-cell">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Slider Area End -->
    <!-- Main content Start -->
    <div class="main-content">
        <div id="rs-about" class="rs-about style1 pt-150 pb-100 md-pb-70">
            <div class="container">
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
                                                            <a style="color:black" href="#"><%# Eval("Heading") %></a>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="btn-btm">
                                                                <div class="rs-view-btn">
                                                                     <%--<a href="Alm_NewsandEvents_por.aspx"><i class="fa fa-angle-right" aria-hidden="true"></i></a>--%>
                                                                    <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/Aboutnoticeboard.aspx?ID={0}",
                                            HttpUtility.UrlEncode(Eval("Pk_Board_ID").ToString())) %>'
                                                 />
                                                                </div>
                                                            </div>
                                                            <%--<a href="#"><i class="fa fa-angle-right" aria-hidden="true"></i>
                                                                <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/Aboutnoticeboard.aspx?ID={0}",
                                            HttpUtility.UrlEncode(Eval("Pk_Board_ID").ToString())) %>'
                                                 />
                                                            </a>--%>

                                                        </div>
                                                        </div>
                                                        
                                                    </div>
                                                    
                                              <%--  <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/Alm_NewsandEvents_por.aspx?ID={0}",
                                            HttpUtility.UrlEncode(Eval("Pk_Board_ID").ToString())) %>'
                                                Text="Read More" />--%>
                                            </li>
                                            <div></div>
                                        </ul>
                                    </ItemTemplate>
                                </Anthem:Repeater>
                            </div>
                            <a class="readon2 banner-style" data-toggle="modal" href="#" data-target="#exampleModalScrollable">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                        </div>
                    </div>
                    <div class="col-lg-8 pr-50 md-pr-15">
                        <div class="about-part">
                            <div class="sec-title">
                                <div class="sub-title primary wow fadeInUp">About Us</div>
                                <h2 class="title wow fadeInUp">University Profile</h2>
                                <div class="desc wow fadeInUp">
                                    Himachal Pradesh University was established by an Act of the Legislative Assembly of Himachal Pradesh on 22 July 1970as a response to the needs and aspirations of the Union Territory, poised for full statehood in the Union of India on 25 January 1971. It is the only multi-faculty residential and affiliating university in the State that provides higher education to urban, rural and tribal areas through formal and distant modes.
                                </div>
                                <div class="desc wow fadeInUp">
                                    The headquarters of the University is located at Summer Hill, the picturesque suburb of Shimla. The University has a total area of 241.11bighas with stately buildings set among rhododendron, silver oak, pine and deodar trees. It affords a salubrious clime and congenial atmosphere for reflection, study and research. The prime objective of the University is to disseminate knowledge, advance learning and understanding through research, training and extension programmes.
                                    It instills in its students and teachers a conscious awareness regarding the social and economic needs, cultural ethos, and future requirements of the state and the country.
                                </div>
                                <a class="readon2 banner-style" href="#">Read More <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="rs-blog" class="rs-blog main-home pb-70 pt-70 md-pt-70 md-pb-70">
            <div class="container">
                <div class="sec-title text-center mb-50 md-mb-30">
                    <div class="sub-title primary">Latest </div>
                    <h2 class="title mb-0">News&Stories</h2>
                </div>

                <div class="rs-carousel owl-carousel" data-loop="true" data-items="3" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="false" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="1" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="3" data-md-device-nav="false" data-md-device-dots="false">
                    <asp:Repeater ID="RepeaterNewsStories" runat="server">
                        <ItemTemplate>
                            <div class="blog-item">
                                <div class="image-part">
                                    <img runat="server" id="Image" src='<%# "~/ALM_uploadimg/"+Eval("Image")%>' alt="">
                                </div>
                                <div class="blog-content">
                                    <ul class="blog-meta">
                                        <li><i class="fa fa-calendar"></i><%# Eval("ConvertedDate") %></li>
                                    </ul>
                                    <h3 class="title"><a href="#"><%# Eval("Heading") %></a></h3>
                                    <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"><%# Eval("Description") %></div>
                                    <div class="btn-btm">
                                        <div class="rs-view-btn">
                                            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/Alm_NewsandEvents_por.aspx?ID={0}",
                                            HttpUtility.UrlEncode(Eval("Pk_Stories_id").ToString())) %>'
                                                Text="Read More" />
                                            <%-- <a href="Alm_NewsandEvents_por.aspx">Read More</a>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
                <div class="col-lg-12 text-center pt-45">
                    <a class="readon green-btn" href="Alm_NewsandStoriesList.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>

        <div id="rs-blog" class="rs-blog colour main-home pb-70 pt-70 md-pt-70 md-pb-70">
            <div class="container">
                <div class="sec-title text-center mb-50 md-mb-30">
                    <div class="sub-title primary">Latest </div>
                    <h2 class="title mb-0">Events</h2>
                </div>
                <div class="rs-carousel owl-carousel" data-loop="true" data-items="3" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="false" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="1" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="3" data-md-device-nav="false" data-md-device-dots="false">
                    <asp:Repeater ID="RepeaterEvents" runat="server">
                        <ItemTemplate>
                            <div class="blog-item">
                                <div class="image-part">
                                    <img runat="server" id="Imge" src='<%# Eval("Filepath") %>' alt="" style="height: 197px;">
                                </div>
                                <div class="blog-content">
                                    <ul class="blog-meta">
                                        <li><i class="fa fa-calendar"></i><%# Eval("Start_date") %></li>
                                    </ul>
                                    <h3 class="title"><a href="#"><%# Eval("Event_name") %></a></h3>
                                    <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"><%# Eval("Description") %></div>
                                    <div class="btn-btm">
                                        <div class="rs-view-btn">
                                            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/Alm_Events.aspx?ID={0}",HttpUtility.UrlEncode(Eval("PK_Events_id").ToString())) %>'
                                                Text="Read More" />
                                            <%-- <a href="Alm_Events.aspx">Read More</a>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>

                    <%--  <div class="blog-item">
                        <div class="image-part">
                            <img src="alumin-default-theme/images/e2.jpg" alt="">
                        </div>
                        <div class="blog-content">
                            <ul class="blog-meta">
                                <li><i class="fa fa-calendar"></i>Friday, Jul 22, 2022</li>
                            </ul>
                            <h3 class="title"><a href="#">Greetings on the 53rd Foundation Day of our Alma Mater HPU</a></h3>
                            <!--<div class="desc">Greetings on the of our Alma Mater HPU.</div>-->
                            <div class="btn-btm">
                                <div class="rs-view-btn">
                                    <a href="#">Read More</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="blog-item">
                        <div class="image-part">
                            <img src="alumin-default-theme/images/e4.jpg" alt="">
                        </div>
                        <div class="blog-content">
                            <ul class="blog-meta">
                                <li><i class="fa fa-calendar"></i>June 20, 2010</li>
                            </ul>
                            <h3 class="title"><a href="#">Interaction of Alumni during Induction </a></h3>
                            <div class="desc">the acquisition of knowledge, skills...</div>
                            <div class="btn-btm">
                                <div class="rs-view-btn">
                                    <a href="#">Read More</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="blog-item">
                        <div class="image-part">
                            <img src="alumin-default-theme/images/e5.jpg" alt="">
                        </div>
                        <div class="blog-content">
                            <ul class="blog-meta">
                                <li><i class="fa fa-calendar"></i>August 30, 2020</li>
                            </ul>
                            <h3 class="title"><a href="#">Our Alumni our Pride – Bond of Life</a></h3>
                            <div class="desc">HPU Alumni Association launches...</div>
                            <div class="btn-btm">
                                <div class="rs-view-btn">
                                    <a href="#">Read More</a>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="col-lg-12 text-center pt-45">
                    <a class="readon green-btn" href="Alm_Events_List.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>


        <!-- Team Section Start -->
        <div id="rs-team" class="rs-team style1 pt-94 pb-100 md-pt-64 md-pb-70">
            <div class="container">
                <div class="sec-title text-center mb-50 md-mb-30">
                    <div class="sub-title primary">Gallery </div>
                    <h2 class="title mb-0">Photos and Videos</h2>
                </div>
                <div class="rs-carousel owl-carousel nav-style2" data-loop="true" data-items="3" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="true" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="2" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="3" data-md-device-nav="true" data-md-device-dots="false">
                    <asp:Repeater ID="GalleryImages" runat="server">
                        <ItemTemplate>
                            <div class="team-item">

                                <img runat="server" id="Image2" src='<%# "~/UploadedImg/"+Eval("photofilename")%>' alt="">
                                <div class="content-part">
                                    <h4 class="name"><a href="#"><%# Eval("PhotoDesc") %></a></h4>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<div class="team-item">
                        <img src="alumin-default-theme/images/1.jpg" alt="">
                        <div class="content-part">
                            <h4 class="name"><a href="#">HPU</a></h4>
                        </div>
                    </div>--%>
                </div>
                <div class="col-lg-12 text-center pt-45">
                    <a class="readon green-btn" href="ALM_Gallery_Lists.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>
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
                                            <div class="desc" ><a style="color:black" href="#"><%# Eval("Heading") %></a></div>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </Anthem:Repeater>
                        </div>
                    </div>
                </div>
                <!--<div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                  <button type="button" class="btn btn-primary">Save changes</button>
                </div>-->
            </div>
        </div>
    </div>
</asp:Content>

