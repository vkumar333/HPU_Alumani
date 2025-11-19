<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ALM_FundDonation.aspx.cs" Inherits="Alumni_FundDonation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../include/jquery-1.2.1.min.js"></script>
    <script src="../include/CommonJS.js"></script>
    <script lang="javascript">
        function ClickToShow() {
            dispalyPopUpShow();
        }
		
        function showPopUpDiv() {
            document.getElementById('contribute').style.display = 'block';
        }

        function hidePopUpDiv() {
            document.getElementById('contribute').style.display = 'none';
        }

        function checkValidation() {            
            if (document.getElementById('ctl00_ContentPlaceHolder1_Txt_Amount').value == "") {
                alert("Amount cannot be blank!.")
                document.getElementById('ctl00_ContentPlaceHolder1_Txt_Amount').focus();
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtname').value == "") {
                alert("Name cannot be blank!.")
                document.getElementById('ctl00_ContentPlaceHolder1_txtname').focus();
                return false;
            }
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (document.getElementById('ctl00_ContentPlaceHolder1_Textemail').value == "") {
                alert("Email can not be blank!.")
                document.getElementById('ctl00_ContentPlaceHolder1_Textemail').focus();
                return false;
            }
            else if (!emailRegex.test(document.getElementById('ctl00_ContentPlaceHolder1_Textemail').value)) {
                alert('Invalid Email-Id!');
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').value == "") {
                alert("Country Code is required!")
                document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').focus();
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtMobile').value == "") {
                alert("Mobile can not be blank!.")
                document.getElementById('ctl00_ContentPlaceHolder1_txtMobile').focus();
                return false;
            } else if (document.getElementById('ctl00_ContentPlaceHolder1_txtMobile').value.length < 10) {
                alert("Invalid Mobile No!");
                document.getElementById('ctl00_ContentPlaceHolder1_txtMobile').focus();
                return false;
            }
        }

    </script>
	
    <style>
	
        .course-desc {
            padding: 12px;
        }

        .btnConDonate {
            outline: none;
            border: none;
            border-radius: 3px;
            display: inline-block;
            text-transform: capitalize;
            font-size: 16px;
            font-weight: 500;
            color: #ffffff;
            background: #fe9700;
            position: relative;
            overflow: hidden;
        }

        .author-comment ul li.colur1, .author-comment ul li.colur2, .author-comment ul li.colur3 {
            background: #d4edda82;
        }

        .author-comment ul li.colur4, .author-comment ul li.colur5, .author-comment ul li.colur6 {
            background: #d1ecf17a;
        }

        .author-comment ul li.colur7, .author-comment ul li.colur8, .author-comment ul li.colur9 {
            background: #fff3cd4f;
        }
		
		.sidebar-area .cate-box .single-image img {
            width:100%;height:210px!important;
        }
		
    </style>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <%--<h1 class="page-title">Appeal for Helping Prof. Zinta and Family</h1>--%>
                        <%--<ul>
                            <li>
                                <a class="active" href="index.html">Home</a>
                            </li>
                            <li>Fundraising</li>
                        </ul>--%>
                        <h1 class="page-title"><%= Session["heading"].ToString() %> </h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_FundDonationlist.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <%--<div class="col-lg-12 col-md-12">
                    <div class="text-center ovrflw">
                        <h2>Contributors</h2>
                        <div class="row scroll-flww">
                            <div class="col-md-12">
                                <h3 class="title-bg">All Contributors (6)</h3>
                            </div>--%>
                            <%-- <div class="col-md-4">
                                <div class="author-comment">

                                    <ul>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-1.png" alt="Blog Details photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Anonymous</h5>
                                                        <p>22 hours ago</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            1,000
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-2.png" alt="Blog Details photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Girija Sharma</h5>
                                                        <p>Class of 1988</p>
                                                        <p>Mar 22, 2023</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            5,000
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-3.png" alt="Blog single photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Pankaj Sharma</h5>
                                                        <p>Class of 2003</p>
                                                        <p>Mar 22, 2023</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            1,100
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="author-comment">
                                    <ul>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-4.png" alt="Blog Details photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Balram Dogra</h5>
                                                        <p>Class of 1982</p>
                                                        <p>Mar 22, 2023</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            5,000
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-5.png" alt="Blog Details photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Sukhvir Singh</h5>
                                                        <p>Class of 2015</p>
                                                        <p>Mar 22, 2023</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            1,000
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-6.png" alt="Blog single photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Yoginder Verma Prof.</h5>
                                                        <p>Mar 16, 2023</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            3,100
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="author-comment">
                                    <ul>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-1.png" alt="Blog Details photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Dr. D. C. KATOCH</h5>
                                                        <p>Mar 16, 2023</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            5,100
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-1.png" alt="Blog Details photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Anonymous</h5>
                                                        <p>Mar 15, 2023</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            2,000
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="image-comments">
                                                        <img src="alumin-default-theme/images/cont-1.png" alt="Blog single photo">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="dsc-comments">
                                                        <h5 class="contri">Dr.Poonam Sharma</h5>
                                                        <p>Mar 15, 2023</p>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="dsc-comments amount">
                                                        <p>
                                                            INR
                                                            <br />
                                                            1,000
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>--%>

                            <%--<asp:Repeater ID="repTopContributor" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-4">
                                        <div class="author-comment">
                                            <ul>
                                                <li class="colur<%# Container.ItemIndex + 1 %>">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <div class="dsc-comments">
                                                                <h5 class="contri"><%# Eval("Contributor_Name") %></h5>
                                                                <p><%# Eval("PaymentDate") %></p>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="dsc-comments amount">
                                                                <p>
                                                                    INR
                                                            <br />
                                                                    <%# Eval("Donation_amount") %>
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                    </div>
                </div>--%>

                <div class="col-lg-8 col-md-12 mt-10">
                    <asp:Repeater ID="RepeventsAll" runat="server">
                        <ItemTemplate>
                            <div>
                                <div class="single-image">
                                    <img id="Imge" src='<%# Eval("FilePath") %>' alt="event" style="width: 100%;">
                                </div>
                                <h5 class="top-title"><%# Eval("Heading") %></h5>
                                <span class="date">
                                    <i class="fa fa-calendar" aria-hidden="true"></i> <%# Eval("createddate") %>
                                </span>
                                <h5 class="description">Description</h5>
                                <p style="text-align: justify;"><%# Eval("Description") %></p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <%-- <div class="col-lg-8 col-md-12 mt-10">
                    <div class="single-image">
                        <img src="alumin-default-theme/images/f11.jpg" alt="event">
                    </div>
                    <h5 class="top-title">Appeal for Helping Prof. Zinta and Family</h5>
                    <span class="date">
                        <i class="fa fa-calendar" aria-hidden="true"></i> Mar 11, 2023
                    </span>
                    <h5 class="description">Description</h5>
					<p style="text-align:justify;">Fellow Alumnus,</p><br/>
                    <p style="text-align:justify;">This is an appeal to help Prof. Zinta, our Alumnus and Prof. OF psychology, whose family recently  met with an unbearable tragedy, when joint  family wooden house was gutted in a fire accident. It not only reduced the ancestral  house to ashes but also caused loss of precious lives of close family members.  
						Your help to the family is requestd to bring solace and give them a feeling that we the Alumni are with them in this hour of huge grief. </p>
						<p style="text-align:justify;">Attached pictures are hugely disturbing but will give you the magnitude of tragedy and trauma through which family is passing.</p>
						<p style="text-align:justify;">A small help and compassion of yours can make a huge difference.</p><br/>
						<p>With regards.<br/>
						President HPUAA</p>
                    
                </div>--%>
                <div class="col-lg-4 col-md-12 mt-10">
                    <div class="sidebar-area">
                        <div class="cate-box">
                            <h3 class="title">Fundraising</h3>
                            <%-- <asp:Repeater runat="server" ID="rep">
                        <ItemTemplate>
                            <div class="row evnets-item">
                                <div class="col-md-6">
                                    <div class="evnets-img">
                                        <img id="Imge" src='<%# Eval("FilePath") %>' alt="event">
                                        <a class="image-link" href="#" title="Events">
                                            <i class="fa fa-link"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="course-header">
                                        <h3 class="course-title"><a href="#"><%# Eval("Heading") %></a></h3>
                                      <%--  <div class="course-date">
                                            <i class="fa fa-calendar-check-o"></i><%# Eval("Start_date") %>
                                        </div>--%>
                            <%-- </div>
                                    <div class="course-body">--%>
                            <%--  <div class="course-desc">
                                            <p style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                <%# Eval("Description") %>
                                            </p>
                                        </div>--%>
                            <%--                   <asp:HyperLink runat="server" class="readon2 banner-style ext mt-10" NavigateUrl='<%# string.Format("~/Alumni/FundDonation.aspx?ID={0}",
                    HttpUtility.UrlEncode(Eval("pk_contribution_ID").ToString())) %>'
                    Text="Contribute Now" />--%>

                            <%--        </div>
                                </div>
                            </div>progressbar
                        </ItemTemplate>
                    </asp:Repeater>--%>
                            <asp:Repeater ID="RepOnlydetails" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <div class="single-image">
                                            <img id="Imge" src='<%# Eval("FilePath") %>' alt="event">
                                        </div>
                                        <div class="course-desc">
                                            <p>
                                                <span class="fund">INR <%# Eval("Msg") %> raised of INR <%# Eval("Goal_Amount") %> goal</span>
                                            </p>
                                            <div class="progress mb-10 mt-10">
                                                <div class="progress-bar progress-bar-striped bg-info" role="progressbar" style='<%# "color:#6D7B8D;width:" + DataBinder.Eval(Container.DataItem, "percentage") + ";" %>' aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <p class="alert alert-success">
                                                <i class="fa fa-users" aria-hidden="true"></i> <%# Eval("Peoplecount") %>
                                            </p>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-8">
                                                <a class="readon2 banner-style ext mt-10" href="#" data-toggle="modal" href="#" data-target="#contribute" style="left: 15px;">Contribute Now</a>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="social-share">
                                                    <label class="toggle" for="toggle">
                                                        <input type="checkbox" id="toggle" style="display: none;" />
                                                        <div class="btnn" style="right: 15px;">
                                                            <i class="fa fa-share-alt"></i>
                                                            <i class="fa fa-times"></i>
                                                            <div class="social">
                                                                <a href="#"><i class="fa fa-facebook"></i></a>
                                                                <a href="#"><i class="fa fa-linkedin"></i></a>
                                                                <a href="#"><i class="fa fa-whatsapp"></i></a>
                                                            </div>
                                                        </div>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <ul class="funds">
                                            <li>
                                                <a href="#">Category: <span style="font-weight: 600;"><%# Eval("Categories") %></span></a>
                                            </li>
                                            <li>
                                                <a href="#">Start Date: <span style="font-weight: 600;"><%# Eval("createddate") %></span></a>
                                            </li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <%--  <div class="evnets-img mt-3">
                                <img src="alumin-default-theme/images/f1.jpg" alt="Events" runat="server" />
                            </div>
									<div class="course-desc">
										<p>
											<span class="fund">INR 46,800</span> raised of INR 5,00,000 goal
										</p>
										<div class="progress mb-10 mt-10">
										  <div class="progress-bar progress-bar-striped bg-info" role="progressbar" style="width: 25%" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
										</div>
										<p class="alert alert-success">
											<i class="fa fa-users" aria-hidden="true"></i> 15 people have contributed so far.
										</p>
									</div>
									<div class="row">
									<div class="col-md-8">
										<a class="readon2 banner-style ext mt-10" href="#" data-toggle="modal" href="#" data-target="#contribute" style="left: 15px;">Contribute Now</a>
									</div>
									<div class="col-md-4">
										<div class="social-share">
											<label class="toggle" for="toggle">
											  <input type="checkbox" id="toggle"  style="display:none;"/>
											  <div class="btnn" style="right:15px;">
												<i class="fa fa-share-alt"></i>
												<i class="fa fa-times"></i>
												<div class="social">
												  <a href="#"><i class="fa fa-facebook"></i></a>
												  <a href="#"><i class="fa fa-linkedin"></i></a>
												  <a href="#"><i class="fa fa-whatsapp"></i></a>
												</div>
											  </div>
											</label>
										</div>
									</div>
								</div>
								
                            <ul class="funds">
                                <li>
                                     <a href="#">Category: <span style="font-weight:600;">Social</span></a>
                                </li>
                                <li>
                                    <a href="#">Start Date:  <span style="font-weight:600;">Sat, Mar 11, 2023</span></a>
                                </li>
                            </ul>
                        </div>--%>
                        <asp:Repeater ID="coordinatorsDetails" runat="server">
                            <ItemTemplate>
                                <div class="cate-box">
                                    <h3 class="title">Project Co-ordinators</h3>
                                    <ul class="funds">
                                        <li>
                                            <a href="#"><%# Eval("loginname") %><span><i class="fa fa-user" aria-hidden="true"></i></span></a>
                                        </li>
                                        <li>
                                            <a href="#"><%# Eval("email") %><span><i class="fa fa-envelope" aria-hidden="true"></i></span></a>
                                        </li>
                                        <%--<li>
                                            <a href="#">9805076451 <span><i class="fa fa-phone" aria-hidden="true"></i></span></a>
                                        </li>--%>
                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%-- <div class="cate-box">
                            <h3 class="title">Project Co-ordinators</h3>
                            <ul class="funds">
                                <li>
                                    <a href="#">P.K Ahluwalia <span><i class="fa fa-user" aria-hidden="true"></i></span></a>
                                </li>
                                <li>
                                    <a href="#">pk_ahluwalia7@yahoo.com <span><i class="fa fa-envelope" aria-hidden="true"></i></span></a>
                                </li>
                                <li>
                                    <a href="#">9805076451 <span><i class="fa fa-phone" aria-hidden="true"></i></span></a>
                                </li>
                            </ul>
                        </div>--%>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%--<div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="gridiv">
                        <Anthem:GridView ID="gvConPayHistry" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            AutoUpdateAfterCallBack="True" Width="100%" UpdateAfterCallBack="True" OnRowCommand="gvConPayHistry_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contributor Name">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <Anthem:Label ID="lblConID" runat="server" Text='<%# Eval("Contributor") %>' AutoUpdateAfterCallBack="True"></Anthem:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Email" HeaderText="Email ID">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No.">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ContribitionAmount" HeaderText="Contribition Amount">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>

                            </Columns>
                        </Anthem:GridView>
                    </div>
                </div>

            </div>
        </div>
    </div>--%>

    <div class="modal fade" id="contribute" tabindex="-1" role="dialog" aria-labelledby="exampleModalScrollableTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <%--<h5 class="modal-title" id="exampleModalScrollableTitle">Contribution for Appeal for Helping Prof. Zinta and Family</h5>--%>
                    <span style="color: white;">
                        <Anthem:Label ID="lblHeading" class="title" runat="server" Text=""></Anthem:Label>
                    </span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="contact-page-section">
                        <div class="container">
                            <div class="contact-comment-section">
                                <h6 style="margin-bottom: 0px;">Step 1: Fill the amount you wish to contribute</h6>
                                <div id="form-messages"></div>
                                <form id="contact-form" method="post" action="#">
                                    <fieldset>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-12">
                                                <div class="form-group">
                                                    <label><strong>Donation amount </strong><span style="color: red">*</span></label>
                                                    <%--<input name="fname" id="fname" runat="server" class="form-control" type="text" placeholder="INR">--%>
                                                    <Anthem:TextBox runat="server" ID="Txt_Amount" class="form-control" placeholder="INR" onkeydown="return NumberOnly(event,this);" ondrop="return false" onpaste="return false" AutoUpdateAfterCallBack="true" MaxLength="8"></Anthem:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <h6 style="margin-bottom: 0px;">Step 2: Your Details</h6>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-12">
                                                <div class="form-group">
                                                    <label><strong>Name </strong><span style="color: red">*</span></label>
                                                    <%--   <input name="lname" id="lname" runat="server" class="form-control" type="text" placeholder="Name">--%>
                                                    <Anthem:TextBox runat="server" ID="txtname" class="form-control" placeholder="Name" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-12">
                                                <div class="form-group">
                                                    <label><strong>Email </strong><span style="color: red">*</span></label>
                                                    <Anthem:TextBox runat="server" ID="Textemail" class="form-control" placeholder="Email" AutoUpdateAfterCallBack="true" MaxLength="35"></Anthem:TextBox>
                                                    <%--<input name="email" id="email" runat="server" class="form-control"  placeholder="Email">--%>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12">
                                                <div class="form-group">
                                                    <label><strong>Country Code </strong><span style="color: red">*</span></label>
                                                    <div class="row">
                                                        <div class="col-md-6">

                                                            <%--<select class="form-control" name="PhoneCode" runat="server" id="cars">
                                                                <option value="1">US / Canada (+1)
                                                                </option>
                                                                <option value="91" selected="selected">India (+91)
                                                                </option>
                                                                <option value="93">Afghanistan (+93)
                                                                </option>
                                                                <option value="358">Aland Islands (+358)
                                                                </option>
                                                                <option value="355">Albania (+355)
                                                                </option>
                                                                <option value="213">Algeria (+213)
                                                                </option>
                                                                <option value="1684">AmericanSamoa (+1684)
                                                                </option>
                                                                <option value="376">Andorra (+376)
                                                                </option>
                                                                <option value="244">Angola (+244)
                                                                </option>
                                                                <option value="1264">Anguilla (+1264)
                                                                </option>
                                                                <option value="672">Antarctica (+672)
                                                                </option>
                                                                <option value="1268">Antigua and Barbuda (+1268)
                                                                </option>
                                                                <option value="54">Argentina (+54)
                                                                </option>
                                                                <option value="374">Armenia (+374)
                                                                </option>
                                                                <option value="297">Aruba (+297)
                                                                </option>
                                                                <option value="61">Australia (+61)
                                                                </option>
                                                                <option value="43">Austria (+43)
                                                                </option>
                                                                <option value="994">Azerbaijan (+994)
                                                                </option>
                                                                <option value="1242">Bahamas (+1242)
                                                                </option>
                                                                <option value="973">Bahrain (+973)
                                                                </option>
                                                                <option value="880">Bangladesh (+880)
                                                                </option>
                                                                <option value="1246">Barbados (+1246)
                                                                </option>
                                                                <option value="375">Belarus (+375)
                                                                </option>
                                                                <option value="32">Belgium (+32)
                                                                </option>
                                                                <option value="501">Belize (+501)
                                                                </option>
                                                                <option value="229">Benin (+229)
                                                                </option>
                                                                <option value="1441">Bermuda (+1441)
                                                                </option>
                                                                <option value="975">Bhutan (+975)
                                                                </option>
                                                                <option value="591">Bolivia, Plurinational State of (+591)
                                                                </option>
                                                                <option value="387">Bosnia and Herzegovina (+387)
                                                                </option>
                                                                <option value="267">Botswana (+267)
                                                                </option>
                                                                <option value="55">Brazil (+55)
                                                                </option>
                                                                <option value="246">British Indian Ocean Territory (+246)
                                                                </option>
                                                                <option value="673">Brunei Darussalam (+673)
                                                                </option>
                                                                <option value="359">Bulgaria (+359)
                                                                </option>
                                                                <option value="226">Burkina Faso (+226)
                                                                </option>
                                                                <option value="257">Burundi (+257)
                                                                </option>
                                                                <option value="855">Cambodia (+855)
                                                                </option>
                                                                <option value="237">Cameroon (+237)
                                                                </option>
                                                                <option value="1">Canada (+1)
                                                                </option>
                                                                <option value="238">Cape Verde (+238)
                                                                </option>
                                                                <option value=" 345">Cayman Islands (+ 345)
                                                                </option>
                                                                <option value="236">Central African Republic (+236)
                                                                </option>
                                                                <option value="235">Chad (+235)
                                                                </option>
                                                                <option value="56">Chile (+56)
                                                                </option>
                                                                <option value="86">China (+86)
                                                                </option>
                                                                <option value="61">Christmas Island (+61)
                                                                </option>
                                                                <option value="61">Cocos (Keeling) Islands (+61)
                                                                </option>
                                                                <option value="57">Colombia (+57)
                                                                </option>
                                                                <option value="269">Comoros (+269)
                                                                </option>
                                                                <option value="242">Congo (+242)
                                                                </option>
                                                                <option value="243">Congo, The Democratic Republic of the Congo (+243)
                                                                </option>
                                                                <option value="682">Cook Islands (+682)
                                                                </option>
                                                                <option value="506">Costa Rica (+506)
                                                                </option>
                                                                <option value="225">Cote d'Ivoire (+225)
                                                                </option>
                                                                <option value="385">Croatia (+385)
                                                                </option>
                                                                <option value="53">Cuba (+53)
                                                                </option>
                                                                <option value="357">Cyprus (+357)
                                                                </option>
                                                                <option value="420">Czech Republic (+420)
                                                                </option>
                                                                <option value="45">Denmark (+45)
                                                                </option>
                                                                <option value="253">Djibouti (+253)
                                                                </option>
                                                                <option value="1767">Dominica (+1767)
                                                                </option>
                                                                <option value="1849">Dominican Republic (+1849)
                                                                </option>
                                                                <option value="593">Ecuador (+593)
                                                                </option>
                                                                <option value="20">Egypt (+20)
                                                                </option>
                                                                <option value="503">El Salvador (+503)
                                                                </option>
                                                                <option value="240">Equatorial Guinea (+240)
                                                                </option>
                                                                <option value="291">Eritrea (+291)
                                                                </option>
                                                                <option value="372">Estonia (+372)
                                                                </option>
                                                                <option value="251">Ethiopia (+251)
                                                                </option>
                                                                <option value="500">Falkland Islands (Malvinas) (+500)
                                                                </option>
                                                                <option value="298">Faroe Islands (+298)
                                                                </option>
                                                                <option value="679">Fiji (+679)
                                                                </option>
                                                                <option value="358">Finland (+358)
                                                                </option>
                                                                <option value="33">France (+33)
                                                                </option>
                                                                <option value="594">French Guiana (+594)
                                                                </option>
                                                                <option value="689">French Polynesia (+689)
                                                                </option>
                                                                <option value="241">Gabon (+241)
                                                                </option>
                                                                <option value="220">Gambia (+220)
                                                                </option>
                                                                <option value="995">Georgia (+995)
                                                                </option>
                                                                <option value="49">Germany (+49)
                                                                </option>
                                                                <option value="233">Ghana (+233)
                                                                </option>
                                                                <option value="350">Gibraltar (+350)
                                                                </option>
                                                                <option value="30">Greece (+30)
                                                                </option>
                                                                <option value="299">Greenland (+299)
                                                                </option>
                                                                <option value="1473">Grenada (+1473)
                                                                </option>
                                                                <option value="590">Guadeloupe (+590)
                                                                </option>
                                                                <option value="1671">Guam (+1671)
                                                                </option>
                                                                <option value="502">Guatemala (+502)
                                                                </option>
                                                                <option value="44">Guernsey (+44)
                                                                </option>
                                                                <option value="224">Guinea (+224)
                                                                </option>
                                                                <option value="245">Guinea-Bissau (+245)
                                                                </option>
                                                                <option value="595">Guyana (+595)
                                                                </option>
                                                                <option value="509">Haiti (+509)
                                                                </option>
                                                                <option value="379">Holy See (Vatican City State) (+379)
                                                                </option>
                                                                <option value="504">Honduras (+504)
                                                                </option>
                                                                <option value="852">Hong Kong (+852)
                                                                </option>
                                                                <option value="36">Hungary (+36)
                                                                </option>
                                                                <option value="354">Iceland (+354)
                                                                </option>
                                                                <option value="62">Indonesia (+62)
                                                                </option>
                                                                <option value="98">Iran, Islamic Republic of Persian Gulf (+98)
                                                                </option>
                                                                <option value="964">Iraq (+964)
                                                                </option>
                                                                <option value="353">Ireland (+353)
                                                                </option>
                                                                <option value="44">Isle of Man (+44)
                                                                </option>
                                                                <option value="972">Israel (+972)
                                                                </option>
                                                                <option value="39">Italy (+39)
                                                                </option>
                                                                <option value="1876">Jamaica (+1876)
                                                                </option>
                                                                <option value="81">Japan (+81)
                                                                </option>
                                                                <option value="44">Jersey (+44)
                                                                </option>
                                                                <option value="962">Jordan (+962)
                                                                </option>
                                                                <option value="77">Kazakhstan (+77)
                                                                </option>
                                                                <option value="254">Kenya (+254)
                                                                </option>
                                                                <option value="686">Kiribati (+686)
                                                                </option>
                                                                <option value="850">Korea, Democratic People's Republic of Korea (+850)
                                                                </option>
                                                                <option value="82">Korea, Republic of South Korea (+82)
                                                                </option>
                                                                <option value="965">Kuwait (+965)
                                                                </option>
                                                                <option value="996">Kyrgyzstan (+996)
                                                                </option>
                                                                <option value="856">Laos (+856)
                                                                </option>
                                                                <option value="371">Latvia (+371)
                                                                </option>
                                                                <option value="961">Lebanon (+961)
                                                                </option>
                                                                <option value="266">Lesotho (+266)
                                                                </option>
                                                                <option value="231">Liberia (+231)
                                                                </option>
                                                                <option value="218">Libyan Arab Jamahiriya (+218)
                                                                </option>
                                                                <option value="423">Liechtenstein (+423)
                                                                </option>
                                                                <option value="370">Lithuania (+370)
                                                                </option>
                                                                <option value="352">Luxembourg (+352)
                                                                </option>
                                                                <option value="853">Macao (+853)
                                                                </option>
                                                                <option value="389">Macedonia (+389)
                                                                </option>
                                                                <option value="261">Madagascar (+261)
                                                                </option>
                                                                <option value="265">Malawi (+265)
                                                                </option>
                                                                <option value="60">Malaysia (+60)
                                                                </option>
                                                                <option value="960">Maldives (+960)
                                                                </option>
                                                                <option value="223">Mali (+223)
                                                                </option>
                                                                <option value="356">Malta (+356)
                                                                </option>
                                                                <option value="692">Marshall Islands (+692)
                                                                </option>
                                                                <option value="596">Martinique (+596)
                                                                </option>
                                                                <option value="222">Mauritania (+222)
                                                                </option>
                                                                <option value="230">Mauritius (+230)
                                                                </option>
                                                                <option value="262">Mayotte (+262)
                                                                </option>
                                                                <option value="52">Mexico (+52)
                                                                </option>
                                                                <option value="691">Micronesia, Federated States of Micronesia (+691)
                                                                </option>
                                                                <option value="373">Moldova (+373)
                                                                </option>
                                                                <option value="377">Monaco (+377)
                                                                </option>
                                                                <option value="976">Mongolia (+976)
                                                                </option>
                                                                <option value="382">Montenegro (+382)
                                                                </option>
                                                                <option value="1664">Montserrat (+1664)
                                                                </option>
                                                                <option value="212">Morocco (+212)
                                                                </option>
                                                                <option value="258">Mozambique (+258)
                                                                </option>
                                                                <option value="95">Myanmar (+95)
                                                                </option>
                                                                <option value="264">Namibia (+264)
                                                                </option>
                                                                <option value="674">Nauru (+674)
                                                                </option>
                                                                <option value="977">Nepal (+977)
                                                                </option>
                                                                <option value="31">Netherlands (+31)
                                                                </option>
                                                                <option value="599">Netherlands Antilles (+599)
                                                                </option>
                                                                <option value="687">New Caledonia (+687)
                                                                </option>
                                                                <option value="64">New Zealand (+64)
                                                                </option>
                                                                <option value="505">Nicaragua (+505)
                                                                </option>
                                                                <option value="227">Niger (+227)
                                                                </option>
                                                                <option value="234">Nigeria (+234)
                                                                </option>
                                                                <option value="683">Niue (+683)
                                                                </option>
                                                                <option value="672">Norfolk Island (+672)
                                                                </option>
                                                                <option value="1670">Northern Mariana Islands (+1670)
                                                                </option>
                                                                <option value="47">Norway (+47)
                                                                </option>
                                                                <option value="968">Oman (+968)
                                                                </option>
                                                                <option value="92">Pakistan (+92)
                                                                </option>
                                                                <option value="680">Palau (+680)
                                                                </option>
                                                                <option value="970">Palestinian Territory, Occupied (+970)
                                                                </option>
                                                                <option value="507">Panama (+507)
                                                                </option>
                                                                <option value="675">Papua New Guinea (+675)
                                                                </option>
                                                                <option value="595">Paraguay (+595)
                                                                </option>
                                                                <option value="51">Peru (+51)
                                                                </option>
                                                                <option value="63">Philippines (+63)
                                                                </option>
                                                                <option value="872">Pitcairn (+872)
                                                                </option>
                                                                <option value="48">Poland (+48)
                                                                </option>
                                                                <option value="351">Portugal (+351)
                                                                </option>
                                                                <option value="1939">Puerto Rico (+1939)
                                                                </option>
                                                                <option value="974">Qatar (+974)
                                                                </option>
                                                                <option value="262">Reunion (+262)
                                                                </option>
                                                                <option value="40">Romania (+40)
                                                                </option>
                                                                <option value="7">Russia (+7)
                                                                </option>
                                                                <option value="250">Rwanda (+250)
                                                                </option>
                                                                <option value="590">Saint Barthelemy (+590)
                                                                </option>
                                                                <option value="290">Saint Helena, Ascension and Tristan Da Cunha (+290)
                                                                </option>
                                                                <option value="1869">Saint Kitts and Nevis (+1869)
                                                                </option>
                                                                <option value="1758">Saint Lucia (+1758)
                                                                </option>
                                                                <option value="590">Saint Martin (+590)
                                                                </option>
                                                                <option value="508">Saint Pierre and Miquelon (+508)
                                                                </option>
                                                                <option value="1784">Saint Vincent and the Grenadines (+1784)
                                                                </option>
                                                                <option value="685">Samoa (+685)
                                                                </option>
                                                                <option value="378">San Marino (+378)
                                                                </option>
                                                                <option value="239">Sao Tome and Principe (+239)
                                                                </option>
                                                                <option value="966">Saudi Arabia (+966)
                                                                </option>
                                                                <option value="221">Senegal (+221)
                                                                </option>
                                                                <option value="381">Serbia (+381)
                                                                </option>
                                                                <option value="248">Seychelles (+248)
                                                                </option>
                                                                <option value="232">Sierra Leone (+232)
                                                                </option>
                                                                <option value="65">Singapore (+65)
                                                                </option>
                                                                <option value="421">Slovakia (+421)
                                                                </option>
                                                                <option value="386">Slovenia (+386)
                                                                </option>
                                                                <option value="677">Solomon Islands (+677)
                                                                </option>
                                                                <option value="252">Somalia (+252)
                                                                </option>
                                                                <option value="27">South Africa (+27)
                                                                </option>
                                                                <option value="500">South Georgia and the South Sandwich Islands (+500)
                                                                </option>
                                                                <option value="211">South Sudan (+211)
                                                                </option>
                                                                <option value="34">Spain (+34)
                                                                </option>
                                                                <option value="94">Sri Lanka (+94)
                                                                </option>
                                                                <option value="249">Sudan (+249)
                                                                </option>
                                                                <option value="597">Suriname (+597)
                                                                </option>
                                                                <option value="47">Svalbard and Jan Mayen (+47)
                                                                </option>
                                                                <option value="268">Swaziland (+268)
                                                                </option>
                                                                <option value="46">Sweden (+46)
                                                                </option>
                                                                <option value="41">Switzerland (+41)
                                                                </option>
                                                                <option value="963">Syrian Arab Republic (+963)
                                                                </option>
                                                                <option value="886">Taiwan (+886)
                                                                </option>
                                                                <option value="992">Tajikistan (+992)
                                                                </option>
                                                                <option value="255">Tanzania, United Republic of Tanzania (+255)
                                                                </option>
                                                                <option value="66">Thailand (+66)
                                                                </option>
                                                                <option value="670">Timor-Leste (+670)
                                                                </option>
                                                                <option value="228">Togo (+228)
                                                                </option>
                                                                <option value="690">Tokelau (+690)
                                                                </option>
                                                                <option value="676">Tonga (+676)
                                                                </option>
                                                                <option value="1868">Trinidad and Tobago (+1868)
                                                                </option>
                                                                <option value="216">Tunisia (+216)
                                                                </option>
                                                                <option value="90">Turkey (+90)
                                                                </option>
                                                                <option value="993">Turkmenistan (+993)
                                                                </option>
                                                                <option value="1649">Turks and Caicos Islands (+1649)
                                                                </option>
                                                                <option value="688">Tuvalu (+688)
                                                                </option>
                                                                <option value="256">Uganda (+256)
                                                                </option>
                                                                <option value="380">Ukraine (+380)
                                                                </option>
                                                                <option value="971">United Arab Emirates (+971)
                                                                </option>
                                                                <option value="44">United Kingdom (+44)
                                                                </option>
                                                                <option value="1">United States (+1)
                                                                </option>
                                                                <option value="598">Uruguay (+598)
                                                                </option>
                                                                <option value="998">Uzbekistan (+998)
                                                                </option>
                                                                <option value="678">Vanuatu (+678)
                                                                </option>
                                                                <option value="58">Venezuela, Bolivarian Republic of Venezuela (+58)
                                                                </option>
                                                                <option value="84">Vietnam (+84)
                                                                </option>
                                                                <option value="1284">Virgin Islands, British (+1284)
                                                                </option>
                                                                <option value="1340">Virgin Islands, U.S. (+1340)
                                                                </option>
                                                                <option value="681">Wallis and Futuna (+681)
                                                                </option>
                                                                <option value="967">Yemen (+967)
                                                                </option>
                                                                <option value="260">Zambia (+260)
                                                                </option>
                                                                <option value="263">Zimbabwe (+263)
                                                                </option>
                                                            </select>--%>

                                                            <Anthem:DropDownList ID="ddlCountry" runat="server" name="PhoneCode" AutoCallBack="True" AutoUpdateAfterCallBack="true" class="form-control"></Anthem:DropDownList>

                                                        </div>
                                                        <div class="col-md-6">
                                                            <Anthem:TextBox runat="server" ID="txtMobile" class="form-control" placeholder="Mobile Number" onkeydown="return NumberOnly(event,this);" AutoUpdateAfterCallBack="true" MaxLength="10"></Anthem:TextBox><span style="color: red">*</span>
                                                            <%--<input name="Mobile" id="mobile" class="form-control" type="text" placeholder="Mobile Number">--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group form-check mb-0">
                                            <input type="checkbox" class="form-check-input" id="exampleCheck1" runat="server">
                                            <label class="form-check-label" for="exampleCheck1"><strong>Make Anonymously </strong></label>
                                        </div>
                                        <p>*By disclosing your name, you are likely to encourage others to donate.</p>
                                        <div class="form-group mt-1">

                                            <%--<Anthem:Button ID="donate" runat="server" class="btnConDonate" OnClick="donate_Click" Text="Donate Now" AutoUpdateAfterCallBack="true"
                                                OnClientClick="this.style.display='none';document.getElementById('divLoadGif').style.display = 'block'"/>--%>

                                            <%--  <Anthem:Button ID="donate" runat="server" AutoUpdateAfterCallBack="true" OnClick="donate_Click"
                                             class="btnConDonate" EnableCallBack="true" Text="Donate Now"  OnClientClick="this.style.display='none';'"/>--%>

                                            <%--<Anthem:Button ID="donate" runat="server" AutoUpdateAfterCallBack="true" OnClick="donate_Click"
                                             class="btnConDonate" EnableCallBack="true" Text="Donate Now" />--%>

                                            <%--  <Anthem:Button ID="donate" runat="server" AutoUpdateAfterCallBack="true" OnClick="donate_Click"
                                             class="btnConDonate" EnableCallBack="true" Text="Donate Now"  OnClientClick="this.style.display='none';'"/>--%>

                                            <Anthem:Button ID="donate" runat="server" class="btnConDonate" OnClick="donate_Click" Text="Donate Now" AutoUpdateAfterCallBack="true"
                                                OnClientClick="return checkValidation();" />

                                            <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage"></Anthem:Label>
                                            <%-- <input class="readon2 banner-style ext btn-send"  type="submit" value="Donate Now">--%>
                                            <Anthem:Label ID="lblPaymentMsg" runat="server" AutoUpdateAfterCallBack="true" Font-Bold="true" ForeColor="Red" Text=""></Anthem:Label>

                                        </div>
                                    </fieldset>
                                </form>
                            </div>
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