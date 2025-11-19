<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ALM_Alumni_Home.aspx.cs" MasterPageFile="~/Alumni/AlumniMasterPage.master" Inherits="Alumni_ALM_Alumni_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="../include/jquery-1.2.1.min.js"></script>
    <script runat="server">
        crypto crp = new crypto();
    </script>	
    <script>	
        // After successful login, show the chat div
        function ClickToShow() {
            //$("#plus").click(function () {
            //    alert('button click');
            //});
            dispalyShow();

            alert('button click');
        }
		
        function showChatDiv() {
            document.getElementById('chatDiv').style.display = 'block';
        }
		
        // Example usage after login form submission
        //document.getElementById('loginForm').addEventListener('submit', function (event) {
        //    event.preventDefault(); // Prevent form submission
        //    var username = document.getElementById('username').value;
        //    var password = document.getElementById('password').value;

        //    if (login(username, password)) {
        //        // Call showChatDiv function after successful login
        //        showChatDiv();
        //    } else {
        //        // Display error message or handle invalid login
        //    }
        //});
		
    </script>
	<script type="text/javascript">

        function setSelectedIDs(alumniId, mReqId) {
            document.getElementById('<%= hdnSelectedAlumniID.ClientID %>').value = alumniId;
            document.getElementById('<%= hdnpk_MReqID.ClientID %>').value = mReqId;
        }
		
		document.onkeydown = function (e) {
            // Disable F12, Ctrl+Shift+I, Ctrl+Shift+C, Ctrl+U
            if (
                e.keyCode === 123 || // F12
                (e.ctrlKey && e.shiftKey && e.keyCode === 73) || // Ctrl+Shift+I
                (e.ctrlKey && e.shiftKey && e.keyCode === 67) || // Ctrl+Shift+C
                (e.ctrlKey && e.keyCode === 85) // Ctrl+U
            ) {
                return false;
            }
        };

        document.addEventListener('contextmenu', function (e) {
            e.preventDefault();
        }, false);

    </script>
	
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">

    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
        <%--<ol class="carousel-indicators">
            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
        </ol>--%>
        <div class="carousel-inner">
            <asp:Repeater ID="sliderRepeater" runat="server">
                <ItemTemplate>
                    <div class="item <%#GetActiveClass(Container.ItemIndex) %>">
                        <div class="alumni-slider-main">
                            <img class="img-fluid d-block mx-auto" runat="server" id="Imge" src='<%# Eval("Filepath") %>' alt="" />
                        </div>
                        <div class="alumni-slider-main-bg" style='<%# "background:url(" + Eval("Filepath") + ") scroll no-repeat center center;" %>'>></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left"></span>
        </a>
        <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right"></span>
        </a>
    </div>
	
	<%--<div class="box box-success" style="visibility:hidden;">
        <div class="boxhead">
            <h3 class="box-title">
                <span>
                    <img src="../img/msg-bell-icon.png" alt="Notification" style="width: 35px;" />
                    Notification Alerts Messages 
                <asp:Label ID="lblNotificationCount" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                </span>
            </h3>
        </div>
        <div class="box-body">
            <div id="inner-content-div6" class="">
                <span class="username">
                    <marquee direction="right" behavior="scroll" scrollamount="5">
                        
                        <Anthem:LinkButton ID="lnkMentorshipRequest" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkMentorshipRequest_Click" data-toggle="modal" data-target="#modalPopUpMentorRequestMsgss" Style="color: red; font-weight:500;">
                                       <i class="fa-solid fa-messages"></i> Please click here to View Mentorship Request Details.
                        </Anthem:LinkButton>
                        
                        <br />

                        <Anthem:LinkButton ID="lnkMenteeRequest" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkMenteeRequest_Click" data-toggle="modal" data-target="#modalPopUpMenteeRequestMsgss" Style="color: red; font-weight:500;">
                                       <i class="fa-solid fa-messages"></i> Please click here to View Mentee Request Details.
                        </Anthem:LinkButton>

                        <br />

                        <Anthem:LinkButton ID="lnkBtnMentorMsg" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkBtnMentorMsg_Click" data-toggle="modal" data-target="#modalPopUpMentorMsgss" Style="color: red; font-weight:500;">
                                       <i class="fa-solid fa-messages"></i> Please click here to View Mentor Messages.
                        </Anthem:LinkButton>  
                    <br />
                        <Anthem:LinkButton ID="lnkBtnMenteeMsg" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkBtnMenteeMsg_Click" data-toggle="modal" data-target="#exampleModalScrollableMentee" Style="color: red;">
                                       <i class="fa-solid fa-messages"></i> Please click here to View Mentee Messages.
                        </Anthem:LinkButton>

                        <br />
                </marquee>
                </span>
            </div>
        </div>
    </div>--%>
	
	<div class="container-fluid">
        <div class="box box-success mb-0 mt-15">
            <div class="boxhead  mb-0">
                <h3 class="box-title">
                    <span>
                        <img src="../img/msg-bell-icon.png" alt="Notification" style="width: 25px;" />

                        <Anthem:LinkButton ID="lnkMenteeRequest" style="font-size:14px;" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkMenteeRequest_Click" data-toggle="modal" data-target="#modalPopUpMenteeRequestMsgss" class="alert-link text-danger">
                        </Anthem:LinkButton>
                        <%--<div class="notification-static" >
                              <Anthem:Label ID="lblNewReceivedMsgCount" runat="server" ForeColor="White" Font-Bold="true" SkinID="none" AutoUpdateAfterCallBack="true"></Anthem:Label>
                         </div>--%> 
                        <asp:PlaceHolder 
                            ID="phBadge" 
                            runat="server"
                            Visible='<%# !string.IsNullOrWhiteSpace(lblNewReceivedMsgCount.Text) && Convert.ToInt32(lblNewReceivedMsgCount.Text.Trim()) > 0 %>'
                            >
                        <%--<div class="notification-static">
                                <Anthem:Label ID="lblNewReceivedMsgCount" runat="server" ForeColor="White" Font-Bold="true" SkinID="none" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </div>--%>
							
							<div class="notification-static">
                                <%--<Anthem:Label ID="lblNewReceivedMsgCount" runat="server" ForeColor="White" Font-Bold="true" SkinID="none" AutoUpdateAfterCallBack="true"></Anthem:Label>--%>

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <Anthem:Label ID="lblNewReceivedMsgCount" runat="server" ForeColor="White" Font-Bold="true" SkinID="none" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick" />
                            </div>
                        </asp:PlaceHolder>
                    </span>
                </h3>
            </div>
        </div>
    </div>
	
    <div id="rs-about" class="rs-about style1 pt-20 pb-15 md-pb-70">
        <div class="container-fluid">
            <div class="row">
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
                            <a class="readon2 banner-style" href="ALM_AboutUs_Home.aspx">Read More <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                        </div>
                    </div>
                </div>

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
                                                        <a style="color: black; font-size: 16PX;"><%# Eval("Heading") %></a>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="btn-btm">
                                                            <div class="rs-view-btn">
                                                                <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/ALM_AboutNoticeBoard.aspx?ID={0}",
                                            HttpUtility.UrlEncode(Eval("encId").ToString())) %>'
                                                                    Style="color: black; font-size: 16PX;" />
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
                        <a class="readon2 banner-style" data-toggle="modal" style="margin-top: 15px;" href="#" data-target="#exampleModalScrollable">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="alumni_home_pg">
        <div class="container-fluid">
            <div class="">
                <div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">Notable Alumni </h3>
                    </div>
                    <div class="box-body">
                        <div id="rs-blogNotableAlumni" class="rs-blog main-home md-pt-70 md-pb-70" style="background:none;">
                            <div class="rs-carousel owl-carousel" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="false" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="1" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="false" data-md-device-dots="false">
                                <asp:Repeater ID="rptNotableAlumni" runat="server">
                                    <ItemTemplate>
                                        <div class="blog-item">
                                            <div class="image-part">
                                                <img runat="server" id="Imge" src='<%# Eval("PicUrl") %>' alt="" class="mx-auto" />
                                            </div>
                                            <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("PicUrl") + ") scroll no-repeat center center;" %>'>></div>
                                            <div class="blog-content">
                                                <h3 class="title"><a style="color: black"><%# Eval("Name") %></a></h3>
                                                <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <%# Eval("Subheading") %>
                                                </div>
                                                <div class="btn-btm">
                                                    <div class="rs-view-btn">
                                                        <a name="anchNews" href="ALM_Notable_Details.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="col-lg-12 text-center pt-20">
                                <a class="readon green-btn" href="ALM_Notable_Lists.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">Achievers </h3>
                    </div>
                    <div class="box-body">
                        <div id="rs-blogAchievers" class="rs-blog main-home" style="background:none;">
                            <div class="rs-carousel owl-carousel nav-style2" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="6000" data-smart-speed="1000" data-dots="false" data-nav="true" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="2" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="true" data-md-device-dots="false">
                                <asp:Repeater ID="achieversRepeater" runat="server">
                                    <ItemTemplate>
                                        <div class="blog-item custom-blog-item text-center">
                                            <div class="custom-blog-image">
                                                <img class="img-circle" src="<%# Eval("file_Url")%>" alt="" />
                                            </div>
                                            <div class="blog-content">
                                                <h3 class="title cust-title">
                                                    <a id="alumniid" name="AnchAcheiv" href="#"><%# Eval("alumni_name")%> </a>
                                                </h3>
                                                <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <%# Eval("Achievement")%>
                                                </div>
                                                <div class="btn-btm btm-cut">
                                                    <div class="rs-view-btn">
                                                        <a id="alumni" href="Alm_View_Alumni_Achiever.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="col-lg-12 text-center pt-20">
                                <a class="readon green-btn" href="ALM_Alumni_Achievers_Lists.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">News & Stories </h3>
                    </div>
                    <div class="box-body">
                        <div id="rs-blogNewsStories" class="rs-blog main-home md-pt-70 md-pb-70" style="background:none;">
                            <div class="rs-carousel owl-carousel" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="false" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="1" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="false" data-md-device-dots="false">
                                <asp:Repeater ID="NewsStoriesRepeater" runat="server">
                                    <ItemTemplate>
                                        <div class="blog-item">
                                            <div class="image-part">
                                                <img runat="server" id="Imge" src='<%# Eval("ImageUrl") %>' alt="" class="mx-auto">
                                            </div>
                                            <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("ImageUrl") + ") scroll no-repeat center center;" %>'></div>
                                            <div class="blog-content">
                                                <ul class="blog-meta">
                                                    <li><i class="fa fa-calendar"></i><%# Eval("ConvertedDate") %></li>
                                                </ul>
                                                <h3 class="title"><a style="color: black"><%# Eval("Heading") %></a></h3>
                                                <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <%# Eval("Description") %>
                                                </div>
                                                <div class="btn-btm">
                                                    <div class="rs-view-btn">
                                                        <a name="anchNews" href="ALM_NewsStories_Details.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-lg-12 text-center pt-20">
                            <a class="readon green-btn" href="ALM_Alumni_NewsStories_Lists.aspx">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                        </div>
                    </div>
                </div>

                <div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">Events</h3>
                    </div>
                    <div class="box-body">
                        <div id="rs-blogEvents" class="rs-blog main-home md-pt-70 md-pb-70" style="background:none;">
                            <div class="rs-carousel owl-carousel" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="6000" data-smart-speed="1000" data-dots="false" data-nav="false" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="1" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="false" data-md-device-dots="false">
                                <asp:Repeater ID="repeaterEventss" runat="server">
                                    <ItemTemplate>
                                        <div class="blog-item">
                                            <div class="image-part">
                                                <img runat="server" id="Imge" src='<%# Eval("File_Url") %>' alt="" class="mx-auto">
                                            </div>
                                            <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("File_Url") + ") scroll no-repeat center center;" %>'></div>
                                            <div class="blog-content">
                                                <ul class="blog-meta">
                                                    <li><i class="fa fa-calendar"></i><%# Eval("Start_date") %></li>
                                                </ul>
                                                <h3 class="title"><a style="color: black"><%# Eval("Event_name") %></a></h3>
                                                <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <%# Eval("Description") %>
                                                </div>
                                                <div class="btn-btm">
                                                    <div class="rs-view-btn">
                                                        <a name="anchEvents" href="Alm_View_Events_student.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-lg-12 text-center pt-20">
                            <a class="readon green-btn" href="Alumni_NewsEvents_View.aspx">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                        </div>
                    </div>
                </div>
				
                <div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">Gallery</h3>
                    </div>
                    <div class="box-body">
                        <div id="rs-teamG" class="rs-team style1 md-pt-64 md-pb-70">
                            <div class="">
                                <div class="rs-carousel owl-carousel nav-style2" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="true" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="2" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="true" data-md-device-dots="false">
                                    <asp:Repeater ID="GalleryImages" runat="server">
                                        <ItemTemplate>
                                            <div class="team-item">
                                                <div class="image-part">
                                                    <img runat="server" id="Image2" src='<%# Eval("ImageUrl") %>' alt="" class="mx-auto" />
                                                </div>
                                                <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("ImageUrl") + ") scroll no-repeat center center;" %>'></div>
                                                <div class="content-part">
                                                    <h4 class="name"><a href="#"><%# Eval("PhotoDesc") %></a></h4>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-lg-12 text-center pt-20">
                                    <a class="readon green-btn" href="Alumni_EventsGallery_View.aspx">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--<div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">Notice Boards </h3>
                    </div>
                    <div class="box-body">
                        <div id="rs-blogNoticeBoards" class="rs-blog main-home">
                            <div class="rs-carousel owl-carousel nav-style2" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="7000" data-smart-speed="1100" data-dots="false" data-nav="true" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="2" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="true" data-md-device-dots="false">
                                <asp:Repeater ID="rptrNoticeBoards" runat="server">
                                    <ItemTemplate>
                                        <div class="blog-item">
                                            <div class="image-part">
                                                <img runat="server" id="Imge" src='<%# Eval("File_Url") %>' alt="" class="mx-auto" />
                                            </div>
                                            <div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("File_Url") + ") scroll no-repeat center center;" %>'></div>
                                            <div class="blog-content">
                                                <ul class="blog-meta">
                                                    <li><i class="fa fa-calendar"></i><%# Eval("NoticeDate") %></li>
                                                </ul>
                                                <h3 class="title" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <a style="color: black"><%# Eval("Heading") %></a>
                                                </h3>
                                                <div class="btn-btm">
                                                    <div class="rs-view-btn">
                                                        <a name="anchNotice" href="../Alumni/ALM_AboutNoticeBoard.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-lg-12 text-center pt-20">
                            <a class="readon green-btn" data-toggle="modal" href="#" data-target="#exampleModalScrollable">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                        </div>
                    </div>
                </div>--%>
				
                <div class="box box-success">
                    <div class="">
                    </div>
                    <div class="box-body">
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
                            <a class="readon green-btn" href="ALM_GalleriesVideos.aspx">View All  <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                        </div>
                    </div>
                </div>			
                

                <div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">Vacancies </h3>
                    </div>
                    <div class="box-body">
                        <div id="rs-blogVacancies" class="rs-blog main-home" style="background:none;">
                            <div class="rs-carousel owl-carousel nav-style2" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="5000" data-smart-speed="800" data-dots="false" data-nav="true" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="2" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="true" data-md-device-dots="false">
                                <asp:Repeater ID="vaccanciesRepeater" runat="server">
                                    <ItemTemplate>
                                        <div class="blog-item">
                                            <div class="blog-content cust-cnt">
                                                <h3 class="title">
                                                    <%# Eval("Designation")%>
                                                </h3>
                                                <div class="desc">
                                                    <ul>
                                                        <li>Skills Required:- <span><%# Eval("SkillsReq")%></span></li>
                                                        <li>Selection Process:- <span><%# Eval("SelectionProcess")%> </span></li>
                                                        <li>Job Vacancy URL:- <span><%# Eval("JobVacncyUrl")%></span></li>
                                                    </ul>
                                                </div>
                                                <div class="btn-btm btm-cut">
                                                    <div class="rs-view-btn">
                                                        <a name="AnchVacncy" href="Alm_ViewPublishedJobs.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-lg-12 text-center pt-20">
                            <a class="readon green-btn" href="ALM_Alumni_VacanciesLists.aspx">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>
                        </div>
                    </div>
                </div>

                <div class="box box-success">
                    <div class="boxhead">
                        <h3 class="box-title">Upcoming Birthday's Reminders </h3>
                    </div>
                    <div class="box-body">
                        <div id="rs-blogBirthday" class="rs-blog main-home" style="background:none;">
                            <div class="rs-carousel owl-carousel nav-style2" data-loop="true" data-items="4" data-margin="30" data-autoplay="true" data-hoverpause="true" data-autoplay-timeout="7000" data-smart-speed="1200" data-dots="false" data-nav="true" data-nav-speed="false" data-center-mode="false" data-mobile-device="1" data-mobile-device-nav="false" data-mobile-device-dots="false" data-ipad-device="2" data-ipad-device-nav="false" data-ipad-device-dots="false" data-ipad-device2="2" data-ipad-device-nav2="false" data-ipad-device-dots2="false" data-md-device="4" data-md-device-nav="true" data-md-device-dots="false">
                                <asp:Repeater ID="rptBirthday" runat="server">
                                    <ItemTemplate>
                                        <div class="blog-item custom-blog-item text-center">
                                            <div class="custom-blog-image">
                                                <img class="img-circle" src="<%# Eval("ImageUrl")%>" alt="" />
                                            </div>
                                            <div class="blog-content">
                                                <h5 class="title cust-title">
                                                    <%# Eval("alumni_name")%>                                                     
                                                </h5>
                                                <div class="desc">
                                                    <p>
                                                        <span><i class="fa fa-bell"></i>&nbsp;<%# Eval("Messagess")%></span>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>

                 <!-- Contact Info -->
                <%--<div id="rs-contactus" class="rs-about style1 pt-80 md-pb-70">
                    <div class="about-part">                        
                        <div style="flex: 1; min-width: 400px; line-height: 23px;" class="text-center">
                            <h3 style="font-size: 21px; font-weight: 600;">Contact Info</h3>
                            <p style="font-size: 19px; font-weight: 600; margin-top: 10px;">Himachal Pradesh University, Summer Hill, Shimla-171005</p>
                            <p style="font-size: 14px; font-weight: 500;"><a href="https://www.hpuniv.ac.in" target="_blank"><i class="fa fa-link"></i>&nbsp; www.hpuniv.ac.in</a></p>
                            <p style="font-size: 14px; font-weight: 500;"><a href="mailto:hpualumniassociation@gmail.com"><i class="fa fa-envelope-o"></i>&nbsp;hpualumniassociation@gmail.com</a></p>
                            <p>
                                <a href="https://www.facebook.com/hpuaa">
                                    <img src="alumin-default-theme/images/facebook.png" alt="Facebook" width="24" />
                                </a>
                                <a href="https://x.com/i/flow/login?redirect_after_login=%2Fhpu_shimla">
                                    <img src="alumin-default-theme/images/twitter.png" alt="Twitter/X" width="24" />
                                </a>
                                <a href="https://www.youtube.com/channel/UCbULrU3kflW3xgwYD3WQlDQ/videos">
                                    <img src="alumin-default-theme/images/youtube.png" alt="YouTube" width="24" />
                                </a>
                            </p>
                        </div>
                    </div>
                </div>--%>

            </div>
        </div>
    </div>


    <%-- Chat Process Start --%>
    <%--<div class="livechat-close-main">
        <a class="livechat-close-btn" href="#"><i class="fa fa-close" aria-hidden="true" onclick="displayClose()" id="closeicon"></i></a>
        <div class="livechat-close" id="assistanceboat" onclick="dispalyShow()" style="display: none;">
        </div>
    </div>--%>

    <%--<div class="livechat" style="display:none;">
        <div class="chat_live_main">
            <div class="need-assistance-head" id="iconchat" style="display: block;">
                <a class="chat-robot" href="javascript:void(0);">
                    <img class="botchat" src="../img/chatbot-ai.png" id="botchat" onclick="dispalyShow();">
                </a> 
            </div>
            <div class="livechat_inside" style="display: none;" id="lvchat">
                <div class="chatheader">

                    <div class="headerchat">
                        <div class="position">
                            <span style="font-size: 16px;"> 
                                Hello!</span>
                            <p><%= Server.HtmlEncode(CurrentSender.Text).ToUpper() %> </p>
                        </div>
                    </div>

                    <div class="chatbtn" style="display: none;">
                        <input type="button" id="minus" value="-" class="fa fa-minus" onclick="dispalyHide();" style="display: block;">
                        <input type="button" id="plus" value="+" class="fa fa-minus" onclick="dispalyShow();" style="display: none;">
                    </div>
                    <div class="chatbtn-close">
                        <a href="javascript:void(0);">
                            <img src="../img/chat-close-btn.png" onclick="displayClose()">
                        </a>
                        &nbsp;
                    </div>

                    <div class="chat-circle-icon">
                        <img class="img-circle custom-chat-image" src='<%# "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + Eval("ImageUrl") %>' id="active-user" alt="" />
                    </div>
                    <div class="waveWrapper waveAnimation">
                        <div class="waveWrapperInner bgTop">
                            <div class="wave waveTop" style="background-image: url('../img/wave-top.png')"></div>
                        </div>
                        <div class="waveWrapperInner bgMiddle">
                            <div class="wave waveMiddle" style="background-image: url('../img/wave-mid.png')"></div>
                        </div>
                        <div class="waveWrapperInner bgBottom">
                            <div class="wave waveBottom" style="background-image: url('../img/wave-bot.png')"></div>
                        </div>
                    </div>
                </div>

                <asp:DataList ID="DataList1" runat="server" DataSourceID="sqlDataSource1" RepeatColumns="3" Width="100%">
                    <ItemTemplate>
                        <div class="active-user-conatiner">
                            <img class="img-circle custom-chat-image" src="<%# "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + Eval("FileNam") %>" alt="" />
                            <asp:LinkButton ID="lblUserName" Text='<%# Eval("alumni_name") %>' CommandArgument='<%# Eval("pk_alumniid") %>' runat="server" OnClick="lblUserName_Click" CssClass="online-user"></asp:LinkButton>
                            <div class="activeuser"></div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>

                <div class="chat-body">
                    

                    <div class="ng-scope">
                        <audio id="audioId" src="Sound/tone.wav" style="display: none;" controls="controls" preload="auto"></audio>
                        <asp:Label ID="CurrentSender" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="CurrentSenderId" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="CurrentReceiver" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="CurrentReceiverId" runat="server" Visible="false"></asp:Label>
                        
                        <asp:SqlDataSource ID="sqlDataSource1" runat="server" ConnectionString='<% $ConnectionStrings:IUMSNXG %>' SelectCommand="SELECT [pk_alumniid], [alumni_name], (SELECT [Files_Unique_Name] FROM ALM_AlumniRegistration_File_dtl WHERE [Fk_alumniid] = pk_alumniid AND [IsProfilePicOrDoc] = 1) FileNam FROM ALM_AlumniRegistration WHERE ([OnlineStatus] = 1)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="CurrentSenderId" PropertyName="Text" Name="AlumniId" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                        

                        <asp:UpdatePanel ID="updPnl" runat="server">
                            <ContentTemplate>
                                <div style="height: 250px; width: 100%; display: none;">
                                    <h4 style="text-align: center;" class="alert alert-info">
                                        <%= GetWelcomeBanner(CurrentReceiver.Text) %>
                                        <span style="float: right;"></span>
                                    </h4>
                                </div>
                                <asp:Panel ID="chatPnl" runat="server">
                                    <div style="vertical-align: middle; min-height: 250px;" class="pre-scrollable">
                                        <div>
                                            <asp:DataList ID="DataList2" runat="server" Font-Bold="false" Font-Italic="false" Font-Overline="false" Font-Strikeout="false" Font-Underline="false" HorizontalAlign="Center" RepeatLayout="Table">
                                                <ItemTemplate>
                                                    <div class='<%# GetStyleForMsgList(Eval("MsgSender").ToString()) %> MainChatListClass'>
                                                        <asp:Label ID="label1" runat="server" Text='<%# GetPerfactName(Eval("MsgSender").ToString()) %>'></asp:Label>
                                                        <asp:Label ID="label2" runat="server" Text='<%# Eval("ChatMsg") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="padding: 5px 15px 5px 5px">
                            <asp:Panel ID="MsgPanel" runat="server" DefaultButton="sendBTN">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <script>
                                            function loadingIconVisible() {
                                                document.GetElementById('<%=LoadingImage.ClientID%>').style.opacity = 1;
                                            }
                                        </script>
                                        <div>
                                            <table style="width: 100%" class="table">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="MSGTextBox" runat="server" placeholder="Enter Message" CssClass="form-control" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="sendBTN" runat="server" Text="Send" OnClick="sendBTN_Click" CssClass="btn btn-default" Width="70%" OnClientClick="loadingIconVisible()" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Image ID="LoadingImage" runat="server" ImageUrl="~/ICON/loadingIcon.gif" ImageAlign="Right" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </div>

                    </div>
                </div>
            </div>
        </div>--%>


    <script>
        function playSound() {
            document.getElementById("audioId").play();
        }
    </script>

    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
    <%--   <script src="../Scripts_Chat/bootstrap.bundle.min.js"></script>
    <script src="../Scripts_Chat/event-handler.js"></script>
    <%--  Chat Process End --%>

    <script type="text/javascript">

        //to bind all the Records on Home page

        $(document).ready(function () {

            GetAcheiversList();
            //GetNoticAlert();
            BindCurrentEvent();
            BindPreviousEvents();
            GetCurrentVaCancy();
            GetNoticeBoardLists();
            ////apply for event which is paid
            $(document).on("click", "[name='btnEventApply']", function () {
                //alert('Event apply paid');
                Get_ClickedElement_Dtls(($(this).attr("id")), "EAP");
                return false;
            });

            $(document).on("click", "#Chk_PaidEvent", function () {
                if ($('input[name=Chk_Paid]').is(':checked')) {
                    // alert('checked');
                    $("[name='btnEvent_ApplyPaid']").removeAttr('disabled');
                }
                else {
                    // alert('un checked');
                    $("[name='btnEvent_ApplyPaid']").attr("disabled", "disabled");
                    // Your Statment
                }
                //return false;
            });

            $(document).on("click", "[name='btnEvent_ApplyPaid']", function () {
                $("[name='btnEvent_ApplyPaid']").attr("disabled", "disabled");
                $("#Chk_PaidEvent").attr("disabled", "disabled");
                Save_Paid_EventApplyDtls($(this).attr("id"));
                return false;
            });

            //apply for event without paying

            $(document).on("click", "[name='btnEventAp']", function () {
                Get_ClickedElement_Dtls(($(this).attr("id")), "EA");
                return false;
            });

            $(document).on("click", "#Chk_UnPaidEvent", function () {
                if ($('input[name=Chk_UnPaid]').is(':checked')) {
                    // alert('checked');
                    $("[name='btnEvent_Apply']").removeAttr('disabled');
                }
                else {
                    // alert('un checked');
                    $("[name='btnEvent_Apply']").attr("disabled", "disabled");
                    // Your Statment
                }
                //return false;
            });

            $(document).on("click", "[name='btnEvent_Apply']", function () {
                Save_UnPaid_EventApplyDtls($(this).attr("id"));
                return false;
            });

            $("[name='AnchVacncy']").click(function () {
                Get_ClickedElement_Dtls(($(this).attr("id")), "V");
                return false;
            });

            $("[name='AnchPEvent']").click(function () {
                Get_ClickedElement_Dtls(($(this).attr("id")), "P");
                return false;
            });

            $(document).on("click", "[name='AnchCEvent']", function () {
                Get_ClickedElement_Dtls(($(this).attr("id")), "E");
                return false;
            })

            $("[name='AnchNews']").click(function () {
                Get_ClickedElement_Dtls(($(this).attr("id")), "N");
                return false;
            });

            $(document).on('click', '.C_AnchAcheiv', function () {
                Get_ClickedElement_Dtls(($(this).attr("id")), "A");
                return false;
            })

            $(document).on('click', '.C_AnchAcheivrMore', function () {
                Get_ClickedElement_Dtls(($(this).attr("id")), "A");
                return false;
            })
        });

        function BindPreviousEvents() {

            $.ajax({
                url: 'ALM_Alumni_Home.aspx/GetPreviousEventDtls',
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                // data: JSON.stringify({}),
                async: false,
                //  processData: false,
                cache: false,
                success: function (data) {

                    $("#inner-content-div2").html('');

                    for (var i = 0; i < data.d.length; i++) {
                        $("#inner-content-div2").append('<div class="comment-text global_bullet"> <span class="username"><a name="AnchPEvent" id=' + data.d[i].PkId + ' href="#">' + data.d[i].EventName + '</a></span></div>');
                    }
                },
                error: function (xhr) {

                }
            });
        }

        ////function BindCurrentEvent() {

        ////    $.ajax({
        ////        url: 'ALM_Alumni_Home.aspx/GetCurrentEventDtls',
        ////        dataType: "json",
        ////        type: "POST",
        ////        contentType: 'application/json; charset=utf-8',
        ////        // data: JSON.stringify({}),
        ////        async: false,
        ////        //  processData: false,
        ////        cache: false,
        ////        success: function (data) {

        ////            $("#inner-content-div4").html('');

        ////            for (var i = 0; i < data.d.length; i++) {

        ////                if (data.d[i].IsEventPaid == "visible") {

        ////                    if (data.d[i].ISAlReadyApplied == "No") {
        ////                        //check if already applied this event then apply button will not be show to this user for paid event
        ////                        $("#inner-content-div4").append('<div class="comment-text global_bullet"> <span class="username"><a name="AnchCEvent" id=' + data.d[i].PkId + ' href="#">' + data.d[i].EventName + '</a> <input name="btnEventApply" id=' + data.d[i].PkId + '  style="padding: 2px 10px; visibility:' + data.d[i].IsEventPaid + ';"  value="APPLY & PAY" class="btn btn-primary btn-sm" type="submit" /></span></div>')
        ////                    }
        ////                    else {
        ////                        $("#inner-content-div4").append('<div class="comment-text global_bullet"> <span class="username"><a name="AnchCEvent" id=' + data.d[i].PkId + ' href="#">' + data.d[i].EventName + '</a> </span></div>')
        ////                    }
        ////                }
        ////                else {
        ////                    if (data.d[i].ISAlReadyApplied == "No") {
        ////                        //check if already applied this event then apply button will not be show to this user for unpaid event
        ////                        $("#inner-content-div4").append('<div class="comment-text global_bullet"> <span class="username"><a name="AnchCEvent" id=' + data.d[i].PkId + ' href="#">' + data.d[i].EventName + '</a><input name="btnEventAp" id=' + data.d[i].PkId + '  style="padding: 2px 10px;"  value="APPLY" class="btn btn-primary btn-sm" type="submit" /> </span></div>')
        ////                    }
        ////                    else {
        ////                        //$("#inner-content-div4").append('<div class="comment-text global_bullet"> <span class="username"><a name="AnchCEvent" id=' + data.d[i].PkId + " href='Alm_View_Events_student.aspx?ID=" + data.d[i].PkId + "'>" + data.d[i].EventName + '</a> </span></div>')

        ////                        $("#inner-content-div4").append("<div id='rs-blog' class='rs-blog main-home md-pt-70 md-pb-70'><div class='col-md-3'><div class='blog-item'><div class='image-part'><img class='img-responsive' height='200px' width='295px' src='" + data.d[i].ImgSrc + "'/></div><div class='blog-content'><ul class='blog-meta'><li><i class='fa fa-calendar'></i> " + data.d[i].Startdate + " </li></ul><h3 class='title'><a name='AnchCEvent' id=" + data.d[i].PkId + " href='Alm_View_Events_Student_Details.aspx?ID=" + data.d[i].PkId + "'>" + data.d[i].EventName + "</a></h3><div class='desc'></div></div></div></div></div>")

        ////                        //$("#inner-content-div4").append("<div id='rs-blog' class='rs-blog main-home pb-70 pt-70 md-pt-70 md-pb-70'><div class='container'><div class='rs-carousel owl-carousel' data-loop='true' data-items='3' data-margin='30' data-autoplay='true' data-hoverpause='true' data-autoplay-timeout='5000' data-smart-speed='800' data-dots='false' data-nav='false' data-nav-speed='false' data-center-mode='false' data-mobile-device='1' data-mobile-device-nav='false' data-mobile-device-dots='false' data-ipad-device='2' data-ipad-device-nav='false' data-ipad-device-dots='false' data-ipad-device2='1' data-ipad-device-nav2='false' data-ipad-device-dots2='false' data-md-device='3' data-md-device-nav='false' data-md-device-dots='false'><asp:Repeater ID='RepeaterEvents' runat='server'><ItemTemplate><div class='col-md-3'><div class='blog-item'><div class='image-part'><img class='img-responsive' height='200px' width='295px' src='" + data.d[i].ImgSrc + "'/></div><div class='blog-content'><ul class='blog-meta'><li> <i class='fa fa-calendar'></i> " + data.d[i].Startdate + " </li></ul><h3 class='title'><a name='AnchCEvent' id=" + data.d[i].PkId + " href='Alm_View_Events_student.aspx?ID=" + data.d[i].PkId + "'>" + data.d[i].EventName + "</a></h3><div class='desc'></div></div></div></div></ItemTemplate></asp:Repeater></div><div class='col-lg-12 text-center pt-45'><a class='readon green-btn' href='Alm_Events_List.aspx'>View All <i class='fa fa-angle-right' aria-hidden='true'></i></a></div></div></div>")
        ////                    }
        ////                }
        ////            }
        ////        },
        ////        error: function (xhr) {

        ////        }
        ////    });
        ////}

        function GetCurrentVacancy() {

            $.ajax({
                url: 'ALM_Alumni_Home.aspx/GetCurrentVaCancy',
                type: "POST",
                contentType: 'application/json; charset-utf-8',
                async: false,
                success: function (data) {

                    $("#inner-content-div5").html('');
                    for (var i = 0; i < data.d.length; i++) {
                        //$("#inner-content-div5").append("<div class='comment-text global_bullet'><span class='username'><a name='AnchVacncy' id=" + data.d[i].Pk_JobPostedId + " href='Alm_ViewPublishedJobs.aspx?id=" + data.d[i].Pk_JobPostedId + "'>" + data.d[i].CompanyName + " &nbsp;For&nbsp;  " + data.d[i].Designation + " &nbsp;Opening From  " + data.d[i].OpenFrom + " To  " + data.d[i].OpenTo + "</a> </span></div>");

                        $("#inner-content-div5").append("<div id='rs-blog' class='rs-blog main-home'><div class='col-md-3'><div class='blog-item'><div class='blog-content cust-cnt'> <h3 class='title'> <a id=' + data.d[i].Pk_JobPostedId + ' name='AnchVacncy' href='Alm_ViewPublishedJobs.aspx?id=" + data.d[i].Pk_JobPostedId + "'> " + data.d[i].Designation + " </a> </h3> <div class='desc'> <ul> <li>Skills Required:- <span> " + data.d[i].SkillsReq + "</span></li> <li>Selection Process:- <span> " + data.d[i].SelectionProcess + " </span></li> <li>Job Vacancy URL:- <span> " + data.d[i].JobVacncyUrl + "</span></li></ul></div><div class='btn-btm btm-cut'> <div class='rs-view-btn'> <a id=' + data.d[i].Pk_JobPostedId + ' name='AnchVacncy' href='Alm_ViewPublishedJobs.aspx?id=" + data.d[i].Pk_JobPostedId + "'>View More</a> </div></div></div></div></div></div>");
                    }
                },
                error: function (xhr) {

                }
            });
        }

        function GetAcheiversList() {

            $.ajax({
                url: 'ALM_Alumni_Home.aspx/GetAcheivers',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (data) {

                    $("#inner-content-div1").html('');

                    for (var i = 0; i < data.d.length; i++) {

                        //$("#inner-content-div1").append("<div class='box-comment'><img class='img-circle img-sm' src='" + data.d[i].ImgSrc + "'/> <div class='comment-text'> <span class='username'><a class = 'C_AnchAcheiv' name='AnchAcheiv' id=" + data.d[i].alumniid + " href='#'> " + data.d[i].alumni_name + "</a></span> " + data.d[i].Achievement + " <span class='text-muted pull-right'><a class='C_AnchAcheivrMore'  id=" + data.d[i].alumniid + "   href='#'>More..</a> </span></div> </div>");

                        $("#inner-content-div1").append(
                           "<div id='rs-blog' class='rs-blog main-home'><div class='col-md-4'><div class='blog-item custom-blog-item text-center'> <div class='custom-blog-image'> <img class='img-circle' src='" + data.d[i].file_Url + "'/> </div> <div class='blog-content'> <h3 class='title cust-title'> <a id=" + data.d[i].alumniid + " name='AnchAcheiv' href='#'> " + data.d[i].alumni_name + " </a> </h3> <div class='desc'> " + data.d[i].Achievement + " </div> <div class='btn-btm btm-cut'> <div class='rs-view-btn'> <a id=" + data.d[i].alumniid + " href='Alm_View_Alumni_Achiever.aspx?id=" + data.d[i].alumniid + "'>View More</a> </div></div></div></div></div></div>"
                           );
                    }
                },
                error: function (xhr) {
                    alert('Error ' + xhr.response);
                }
            });
        }

        function Save_UnPaid_EventApplyDtls(Pk_id) {

            $.ajax({
                url: "ALM_Alumni_Home.aspx/SaveUnpaid_EventApply_Dtls",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                async: false,
                data: JSON.stringify({ Pk_Eid: Pk_id }),
                success: function (data) {

                    // alert(jsonData);

                    if (data.d == 1) {
                        $('#lblUnPd_EventApplyMsg').html('');
                        $('#lblUnPd_EventApplyMsg').html('Applied Successfully!').css("color", "red");
                        $("[name='btnEvent_Apply']").attr("disabled", "disabled");
                        $("#Chk_UnPaidEvent").attr("disabled", "disabled");
                        BindCurrentEvent();
                        return false;
                        // setTimeout(hideDive, 2000);
                        //$('#selector').delay(5000)
                        // hideDive();
                    }
                    else {
                        alert('try Again');
                    }
                },
                error: function (xhr) {
                    debugger;
                    alert('error');
                    alert(xhr.response);
                }
            });
        }

        function Save_Paid_EventApplyDtls(Pk_id) {

            $.ajax({
                url: "ALM_Alumni_Home.aspx/Save_paid_EventApply_Dtls",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                async: false,
                data: JSON.stringify({ Pk_Eid: Pk_id }),
                success: function (data) {

                    // alert(jsonData);

                    if (data.d == 1) {

                        window.location.href = "ALM_Alumni_ApplyForEvent.aspx";
                    }
                    else {
                        alert('try Again');
                    }
                },
                error: function (xhr) {
                    debugger;
                    alert('error');
                    alert(xhr.response);
                }
            });
        }

        function hideDive() {
            //  alert(564564564564);
            $("#DivVancyPopUp").css({ "display": "block" });
        }

        function GetNoticeBoardLists() {

            $.ajax({
                url: 'ALM_Alumni_Home.aspx/GetNoticeBoardLists',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (data) {

                    $("#inner-content-div6").html('');

                    for (var i = 0; i < data.d.length; i++) {

                        $("#inner-content-div6").append("<div class='comment-text global_bullet'><span class='username'><a id=" + data.d[i].pk_boardid + " href='AboutNoticeBoard_OnHome.aspx?id=" + data.d[i].pk_boardid + "'>" + data.d[i].heading + "</a></span></div>");
                    }
                },
                error: function (xhr) {
                    alert('Error ' + xhr.response);
                }
            });
        }

    </script>

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

    <div id="DivVancyPopUp" class="white_content-new-1" style="display: none;">
        <div class="popupboxouter">
            <div class="popupbox" style="height: auto; overflow-y: scroll">
                <div onclick="document.getElementById('DivVancyPopUp').style.display='none';document.getElementById('fade').style.display='block';" class="close-1">
                    X  
                </div>

                <table id="tbl" class="table mobile_form">
                </table>

            </div>
        </div>
    </div>

    <%--============================ Pop Up ============================================================ --%>
    <div class="modal fade white_content-new-1" id="ViewDiv" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: max-content;">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Mentor Assigned</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="box-body table-responsive">
                        <div class="gridiv">
                            <Anthem:GridView ID="gvAlumni" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                AutoUpdateAfterCallBack="True" DataKeyNames="fk_MentiId" Width="100%" PageSize="10"
                                OnRowCommand="gvAlumni_RowCommand"
                                OnPageIndexChanging="gvAlumni_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo.">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Mentor Name" DataField="alumni_name">
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Contact No" DataField="contactno">
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Email" DataField="email">
                                        <ItemStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </Anthem:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade white_content-new-1 alumni_home" id="exampleModalScrollable" tabindex="-1" role="dialog" aria-labelledby="exampleModalScrollableTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalScrollableTitle">Notice Board</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="notice-bord style1 custom-pop ovrflw">
                        <div class="scroll-flw">
                            <Anthem:Repeater ID="rptNoticeBords" runat="server">
                                <ItemTemplate>
                                    <ul>
                                        <li>
                                            <div class="date"><span><%# Eval("DayDate") %></span><%# Eval("Month_Name") %></div>
                                            <div class="desc"><a style="color: black" href="#"><%# Eval("Heading") %></a></div>
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
	
	    <%--<div class="modal fade white_content-new-1" id="exampleModalScrollableMentor" tabindex="-1" role="dialog" aria-labelledby="exampleModalScrollableTitleMentor" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalScrollableTitleMentor">Notify Mentor Messages
                        <Anthem:Button ID="btnMentor" runat="server" AutoUpdateAfterCallBack="true" CssClass="pull-right" Text="Back" TextDuringCallBack="Closing..." EnableCallBack="false" OnClick="btnMentor_Click" />
                    </h5>
                </div>
                <div class="modal-body">
                    <div class="notice-bord style1 custom-pop ovrflw">
                        <div class="scroll-flw">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="box box-success">
                                        <div class="panel-body pnl-body-custom">
                                            <div id="inner-content-div15" class="">

                                                <Anthem:Repeater ID="rptMentorRequests" runat="server" AutoUpdateAfterCallBack="true" OnItemCommand="rptMentorRequests_ItemCommand">
                                                    <ItemTemplate>
                                                        <div class="message-box">
                                                            <div class="message-header"><%# Eval("userName") %> </div>
                                                            <div class="message-body"><%# Eval("messageText") %> </div>
                                                            <div class="message-time">🕒 <%# Eval("sentAt") %> </div>
                                                            <div class="message-header"><%# Eval("status") %> </div>

                                                            <div class="message-actions">
                                                                <Anthem:LinkButton ID="lnkAccept" runat="server" Text="Accept" CommandName="Accept"
                                                                    CommandArgument='<%# Eval("MRequestID") %>' EnableCallBack="false"
                                                                    AutoUpdateAfterCallBack="true" TextDuringCallBack="Accepting.." Enabled='<%# Eval("isVisible") %>'>
                                                                </Anthem:LinkButton>

                                                                &nbsp;|&nbsp;

                                                                <Anthem:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="Reject"
                                                                    CommandArgument='<%# Eval("MRequestID") %>' EnableCallBack="false"
                                                                    AutoUpdateAfterCallBack="true" TextDuringCallBack="Rejecting.." Enabled='<%# Eval("isVisible") %>'>
                                                                </Anthem:LinkButton>
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
                </div>
            </div>
        </div>
    </div>--%>
	
    <div class="modal fade white_content-new-1" id="exampleModalScrollableMentee" tabindex="-1" role="dialog" aria-labelledby="exampleModalScrollableTitleMentee" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalScrollableTitleMentee">Notify Mentee Messages
                        <Anthem:Button ID="btnMentee" runat="server" AutoUpdateAfterCallBack="true" CssClass="pull-right" Text="Back" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnMentee_Click" />
                    </h5>
                </div>
                <div class="modal-body">
                    <div class="notice-bord style1 custom-pop ovrflw">
                        <div class="scroll-flw">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="box box-success">
                                        <div class="panel-body pnl-body-custom">
                                            <div id="inner-content-div16" class="">
                                                <Anthem:Repeater ID="rptMenteeMessages" runat="server" AutoUpdateAfterCallBack="true">
                                                    <ItemTemplate>
                                                        <div class="message-box">
                                                            <div class="message-header"><%# Eval("userName") %> : </div>
                                                            <div class="message-body"><%# Eval("messageText") %></div>
                                                            <div class="message-time">🕒 <%# Eval("sentAt") %></div>
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
                </div>
            </div>
        </div>
    </div>
	
	<div class="modal" id="modalPopUpMentorMsgss">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <h5 class="modal-title" id="modalheaderTitle">Notify Mentor Messages </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div1" runat="server">
                        <!-- Message History -->
                        <Anthem:Repeater ID="rptMentorRequests" runat="server" AutoUpdateAfterCallBack="true" OnItemCommand="rptMentorRequests_ItemCommand">
                            <ItemTemplate>
                                <div class="message-box" onclick="setSelectedIDs('<%# Eval("userID") %>', '<%# Eval("pk_MReqID") %>')">
                                    <div class="message-header"><%# Eval("userName") %> : </div>
                                    <div class="message-body"><%# Eval("messageText") %> </div>
                                    <div class="message-time">🕒 <%# Eval("sentAt") %> </div>
                                    <div class="message-header"><%# Eval("status") %> </div>

                                    <%--<div class="message-actions">
                                        <Anthem:LinkButton ID="lnkAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Accepting.." Enabled='<%# Eval("isVisible") %>'>
                                        </Anthem:LinkButton>

                                        &nbsp;|&nbsp;
                                        
                                        <Anthem:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Rejecting.." Enabled='<%# Eval("isVisible") %>'>
                                        </Anthem:LinkButton>
                                    </div>--%>

                                    <div class="message-actions">
                                        <Anthem:Button ID="lnkAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Accepting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn warning"></Anthem:Button>

                                        &nbsp;|&nbsp;
                                        
                                        <Anthem:Button ID="lnkReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Rejecting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn btn-warning"></Anthem:Button>

                                        &nbsp;|&nbsp;

                                        <Anthem:Button ID="btnSendMessageNow" runat="server" Text="Message Now" CommandName="SENDMSGNOW" CommandArgument='<%# Eval("userID") + "|" + Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Sending.." Enabled='<%# Eval("isSendMsgNow") %>' CssClass="btn warning"></Anthem:Button>

                                    </div>
                                </div>
                            </ItemTemplate>
                        </Anthem:Repeater>
                    </div>

                    <div class="" oncontextmenu="return false;">
                        <Anthem:Panel ID="msgPnl" runat="server" AutoUpdateAfterCallBack="true" Visible="false">

                            <Anthem:HiddenField ID="hdnSelectedAlumniID" runat="server" AutoUpdateAfterCallBack="true" />
                            <Anthem:HiddenField ID="hdnpk_MReqID" runat="server" AutoUpdateAfterCallBack="true" />

                            <asp:Literal ID="litMessages" runat="server" Mode="PassThrough"></asp:Literal>

                            <Anthem:TextBox ID="txtMessagess" runat="server" placeholder="Type your message..." AutoUpdateAfterCallBack="true" TextMode="MultiLine" CssClass="form-control" />
                            <Anthem:Button ID="btnSendMsgss" runat="server" AutoUpdateAfterCallBack="true" Text="SEND" TextDuringCallBack="SENDING..." EnableCallBack="false" OnClick="btnSendMsgss_Click" CommandName="SEND" CssClass="btn btn-warning" />
                            <asp:Label ID="lblStatus" runat="server" ForeColor="Green" />
                            <Anthem:Label ID="lblMsgss" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="true" />

                        </Anthem:Panel>
                    </div>

                </div>
                <!-- Modal footer -->
                <%--<div class="modal-footer">
                    <Anthem:Button ID="btnCancel" runat="server" Text="CANCEL" TextDuringCallBack="CANCELING.." AutoUpdateAfterCallBack="True" OnClick="btnCancel_Click" EnableCallBack="false" CommandName="CANCEL" CssClass="btn btn-default" />                    
                    <Anthem:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="SUBMIT" AutoUpdateAfterCallBack="true" TextDuringCallBack="SUBMITING..." EnableCallBack="false" CommandName="SUBMIT" CssClass="btn btn-warning" />
                    <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="true" />
                </div>--%>
            </div>
        </div>
    </div>
	
	<div class="modal" id="modalPopUpMentorRequestMsgss">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <h5 class="modal-title" id="modalheaderTitleMR">Mentorship Request Messages </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="mRequest" runat="server">
                        <!-- Message History -->
                        <Anthem:Repeater ID="rptMR" runat="server" AutoUpdateAfterCallBack="true" OnItemCommand="rptMR_ItemCommand">
                            <ItemTemplate>
                                <div class="message-box" onclick="setSelectedIDs('<%# Eval("senderID") %>', '<%# Eval("pk_MReqID") %>')">
                                    <div class="message-header">Mentorship For : </div>   
                                    <div class="message-body"><%# Eval("goalDescription") %> </div>
                                    <div class="message-header"><%# Eval("senderName") %> : </div>                                    
                                    <div class="message-body"><%# Eval("messageText") %> </div>
                                    <div class="message-time">🕒 <%# Eval("sentAt") %> </div>
                                    <div class="message-header"><%# Eval("status") %> </div>

                                    <div class="message-actions">
                                        <Anthem:Button ID="lnkAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Accepting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn warning"></Anthem:Button>

                                        &nbsp;|&nbsp;
                                        
                                        <Anthem:Button ID="lnkReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Rejecting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn btn-warning"></Anthem:Button>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </Anthem:Repeater>
                    </div>

                </div>
            </div>
        </div>
    </div>
	
	<div class="modal" id="modalPopUpMenteeRequestMsgss" oncontextmenu="return false;">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>--%>
                    <Anthem:Button ID="btnCloseModal" Style="background-color: transparent; border: none;" runat="server" Text="Close" OnClick="btnCloseModal_Click" CssClass="close-1" aria-label="Close" AutoUpdateAfterCallBack="true" data-dismiss="modal"></Anthem:Button>
                    <h5 class="modal-title" id="modalheaderTitleMentee">Received Mentee Requests for Mentorship </h5>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div2" runat="server">
                        <!-- Message History -->
                        <Anthem:Repeater ID="rptMRM" runat="server" AutoUpdateAfterCallBack="true" OnItemCommand="rptMRM_ItemCommand">
                            <ItemTemplate>
                                <div class="message-box" onclick="setSelectedIDs('<%# Eval("senderID") %>', '<%# Eval("pk_MRID") %>')">
                                    <div class="message-header">Mentorship For : </div>
                                    <div class="message-body"><%# Eval("seekingHelpFor") %> </div>
                                    <div class="message-header"><%# Eval("senderName") %> : </div>
                                    <div class="message-body"><%# Eval("messageText") %> </div>
                                    <div class="message-time">🕒 <%# Eval("sentAt") %> </div>
                                    <div class="message-header"><%# Eval("requestStatus") %> </div>

                                    <div class="message-actions">
                                        <Anthem:Button ID="lnkMRMAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("pk_MRID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Accepting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn btn-warning"></Anthem:Button>

                                        &nbsp;|&nbsp;
                                        
                                        <Anthem:Button ID="lnkMRMReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("pk_MRID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Rejecting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn btn-warning"></Anthem:Button>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </Anthem:Repeater>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <%--<div class="modal" id="modalPopUpMenteeRequestMsgss">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <h5 class="modal-title" id="modalheaderTitleMRMsg">Mentee Request Details for Mentorship </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div2" runat="server">
                        <!-- Message History -->
                        <Anthem:Repeater ID="rptMRM" runat="server" AutoUpdateAfterCallBack="true" OnItemCommand="rptMRM_ItemCommand">
                            <ItemTemplate>
                                <div class="message-box" onclick="setSelectedIDs('<%# Eval("senderID") %>', '<%# Eval("pk_MRID") %>')">
                                    <div class="message-header">Mentorship For : </div>   
                                    <div class="message-body"><%# Eval("seekingHelpFor") %> </div>
                                    <div class="message-header"><%# Eval("senderName") %> : </div>                                    
                                    <div class="message-body"><%# Eval("messageText") %> </div>
                                    <div class="message-time">🕒 <%# Eval("sentAt") %> </div>
                                    <div class="message-header"><%# Eval("requestStatus") %> </div>

                                    <div class="message-actions">
                                        <Anthem:Button ID="lnkMRMAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("pk_MRID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Accepting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn warning"></Anthem:Button>

                                        &nbsp;|&nbsp;
                                        
                                        <Anthem:Button ID="lnkMRMReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("pk_MRID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Rejecting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn btn-warning"></Anthem:Button>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </Anthem:Repeater>
                    </div>

                </div>
            </div>
        </div>
    </div>--%>
	
</asp:Content>