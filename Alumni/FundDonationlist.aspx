<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="FundDonationlist.aspx.cs" Inherits="Alumni_FundDonationlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .course-desc {
            padding: 12px;
        }
    </style>
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Fundraising</h1>
                        <%--<ul>
                            <li>
                                <a class="active" href="index.html">Home</a>
                            </li>
                            <li>Fundraising</li>
                        </ul>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <!-- Courses Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-md-12">
                    <asp:Repeater runat="server" ID="rep">
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
                                    </div>
                                    <div class="course-desc">
                                        <p>
                                            <span class="fund">INR 0</span> raised of INR 500000 goal
                                        </p>
                                        <div class="progress mb-10 mt-10">
                                            <div class="progress-bar progress-bar-striped bg-info" role="progressbar" style="width: 25%" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                        <p class="alert alert-success">
                                            <i class="fa fa-users" aria-hidden="true"></i>15 people have contributed so far.
                                        </p>
                                    </div>
                                    <div class="course-body">
                                        <asp:HyperLink runat="server" class="readon2 banner-style ext mt-10" NavigateUrl='<%# string.Format("~/Alumni/FundDonation.aspx?ID={0}",
                    HttpUtility.UrlEncode(Eval("pk_contribution_ID").ToString())) %>'
                                            Text="Contribute Now" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<div class="row evnets-item">
                        <div class="col-md-6">
                            <div class="evnets-img">
                                <img src="alumin-default-theme/images/f1.jpg" alt="Events" />
                                <a class="image-link" href="#" title="Events">
                                    <i class="fa fa-link"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="course-header">
                                <h3 class="course-title"><a href="#">Appeal for Helping Prof. Zinta and Family</a></h3>
                            </div>
                            <div class="course-body">
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
										<a class="readon2 banner-style ext mt-10" href="FundDonation.aspx">Contribute Now</a>
									</div>
									<div class="col-md-4">
										<div class="social-share">
											<label class="toggle" for="toggle">
											  <input type="checkbox" id="toggle" style="display:none;"/>
											  <div class="btnn">
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
                            </div>
                        </div>
                    </div>
                    
					<div class="row evnets-item">
                        <div class="col-md-6">
                            <div class="evnets-img">
                                <img src="alumin-default-theme/images/f2.jpg" alt="Events" />
                                <a class="image-link" href="#" title="Events">
                                    <i class="fa fa-link"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="course-header">
                                <h3 class="course-title"><a href="#">Raising of HPU Alumni Bhavan at Summer Hill Shimla 171005</a></h3>
                            </div>
                            <div class="course-body">
                                <div class="course-desc">
                                    <p>
                                        <span class="fund">INR 7,07,965</span>  raised of INR 2,00,00,000 goal
                                    </p>
									<div class="progress mb-10 mt-10">
									  <div class="progress-bar progress-bar-striped bg-info" role="progressbar" style="width: 15%" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
									</div>
									<p class="alert alert-success">
                                        <i class="fa fa-users" aria-hidden="true"></i> 38 people have contributed so far.
                                    </p>
                                </div>
                                <div class="row">
									<div class="col-md-8">
										<a class="readon2 banner-style ext mt-10" href="FundDonation.aspx">Contribute Now</a>
									</div>
									<div class="col-md-4">
										<div class="social-share">
											<label class="toggle" for="toggle1">
											  <input type="checkbox" id="toggle1"  style="display:none;"/>
											  <div class="btnn">
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
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="col-lg-4 col-md-12">
                    <div class="sidebar-area">
                        <%--<div class="search-box">
                            <h3 class="title">Search Fundraising</h3>
                            <div class="box-search">
                                <input class="form-control" placeholder="Search Here ..." name="srch-term" id="srch-term" type="text">
                                <button class="btn btn-default" type="submit"><i class="fa fa-search" aria-hidden="true"></i></button>
                            </div>
                        </div>--%>
                        <div class="cate-box">
                            <h3 class="title">Categories</h3>
                            <asp:Repeater ID="Repcountall" runat="server">
                                    <ItemTemplate>
                                        <ul>
                                            <li>
                                                <i class="fa fa-angle-right" aria-hidden="true"></i>
                                                <Anthem:LinkButton ID="lnkbtn" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkbtn_Click" Text="All" EnableCallBack="false"></Anthem:LinkButton><span style="margin-left: 252px;">(<%# Eval("all_news") %>)</span>
                                                <%--<a href="#">All Events <span><%# Eval("all_Events") %></span></a>--%>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                              <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <ul>
                                            <li>
                                                <i class="fa fa-angle-right" aria-hidden="true"></i>
                                                <Anthem:LinkButton ID="lnkbtnsecond" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkbtnsecond_Click" Text="All Events" EnableCallBack="false"></Anthem:LinkButton><span style="margin-left: 252px;"><%# Eval("all_Events") %></span>
                                                <%--<a href="#">All Events <span><%# Eval("all_Events") %></span></a>--%>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                        <%--    <ul>
                                <li>
                                    <i class="fa fa-angle-right" aria-hidden="true"></i><a href="#">All <span>(02)</span></a>
                                </li>
                                <li>
                                    <i class="fa fa-angle-right" aria-hidden="true"></i><a href="#">Academic <span>(01)</span></a>
                                </li>
                                <li>
                                    <i class="fa fa-angle-right" aria-hidden="true"></i><a href="#">Development <span>(00)</span></a>
                                </li>
                                <li>
                                    <i class="fa fa-angle-right" aria-hidden="true"></i><a href="#">Social <span>(01)</span></a>
                                </li>
                            </ul>--%>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

