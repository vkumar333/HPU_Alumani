<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UMM/MasterPage.master" CodeFile="Alm_OfflineFundDonation.aspx.cs" Inherits="Alumni_Alm_OfflineFundDonation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../include/jquery-1.2.1.min.js"></script>
    <script src="../include/CommonJS.js"></script>--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="alumin-default-theme/style.css" rel="stylesheet" />
    <style>
        .sidebar-area .title {
            background: #0a6186;
        }

        .progress-bar {
            background-color: #337ab7 !important;
        }

        .loginfooter-lgn {
            display: none;
        }
    </style>

    <style>
        .course-desc {
            padding: 12px;
        }

        .btnConDonate {
            outline: none;
            border: none;
            padding: 12px 40px;
            border-radius: 3px;
            display: inline-block;
            text-transform: capitalize;
            font-size: 16px;
            font-family: 'Rubik', sans-serif;
            font-weight: 500;
            color: #ffffff;
            background: #fe9700;
            position: relative;
            overflow: hidden;
        }

        .bg1 {
            z-index: -1 !important;
        }

        .modal-backdrop {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 9;
            background-color: transparent;
        }

        .fade.in {
            background-color: #00000087;
            z-index: 999;
        }

        #contribute {
            z-index: 9999 !important;
        }

        .ChosenSelector {
            width: 95%;
        }
    </style>

    <!-- Breadcrumbs Start -->
    <%--<div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="text-center ovrflw">
                        <h2>Contributors</h2>
                        <div >
                            <div class="col-md-12">
                                <h3 class="title-bg">All Contributors (15)</h3>
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
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-8 col-md-12 mt-10">
                    <asp:Repeater ID="RepeventsAll" runat="server">
                        <ItemTemplate>
                            <div>
                                <div class="single-image">
                                    <img id="Imge" src='<%# Eval("FilePath") %>' alt="event">
                                </div>
                                <h5 class="top-title"><%# Eval("Heading") %></h5>
                                <span class="date">
                                    <i class="fa fa-calendar" aria-hidden="true"></i><%# Eval("createddate") %>
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
    <%--<div class="col-lg-4 col-md-12 mt-10">
                    <div class="sidebar-area">
                        <div class="cate-box">
                            <h3 class="title">Fundraising</h3>
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
                                                <i class="fa fa-users" aria-hidden="true"></i><%# Eval("Peoplecount") %>
                                            </p>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-8">
                                                <a class="readon2 banner-style ext mt-10" href="#" data-toggle="modal"  data-target="#contribute" style="left: 15px;">Contribute Now</a>
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
                                                <a href="#">Start Date:  <span style="font-weight: 600;"><%# Eval("createddate") %></span></a>
                                            </li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div> 
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
                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>--%>

    <%--  </div>
        </div>
    </div>--%>
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="text-center ovrflw">
                        <h2>Contributors</h2>
                        <div>
                            <div class="col-md-12">
                                <h3 class="title-bg">All Contributors (6)</h3>
                            </div>
                            <%--<div class="col-md-4">
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
                            </div>--%>



                            <%--<div class="col-md-4">
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
                            </div>--%>

                            <%--<div class="col-md-4">
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

                            <asp:Repeater ID="repTopContributor" runat="server">
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
                </div>

                <div class="col-lg-8 col-md-12 mt-10">
                    <asp:Repeater ID="RepeventsAll" runat="server">
                        <ItemTemplate>
                            <div>
                                <div class="single-image">
                                    <img id="Imge" src='<%# Eval("FilePath") %>' alt="event" style="width: 100%;">
                                </div>
                                <h5 class="top-title"><%# Eval("Heading") %></h5>
                                <span class="date">
                                    <i class="fa fa-calendar" aria-hidden="true"></i><%# Eval("createddate") %>
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
                                                <i class="fa fa-users" aria-hidden="true"></i><%# Eval("Peoplecount") %>
                                            </p>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-8">
                                                <a class="readon2 banner-style ext mt-10" href="#" data-toggle="modal" data-target="#contribute" style="left: 15px;">Contribute Now</a>
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
                                                <a href="#">Start Date:  <span style="font-weight: 600;"><%# Eval("createddate") %></span></a>
                                            </li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
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
                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="contribute">
        <div class="modal-dialog modal-dialog-scrollable modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <Anthem:Label ID="lblHeading" class="title" runat="server" Text="" Style="color: #fff;"></Anthem:Label>
                    <%--<h5 class="modal-title" id="exampleModalScrollableTitle"><%# Eval("Heading") %></h5>
                    <h5 class="top-title"><%# Eval("Heading") %></h5>--%>
                    <button type="button" class="close" data-dismiss="modal" style="margin: 10px;" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="contact-page-section">
                        <div class="">
                            <div class="contact-comment-section">
                                <h6 style="margin-bottom: 8px;">Step 1: Fill the amount you wish to contribute</h6>
                                <div id="form-messages"></div>
                                <form id="contact-form" method="post" action="#">
                                    <div class="form-horizontal">
                                        <div class="form-group form-group-sm">
                                            <label class="col-sm-3 control-label">Donation amount <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <%--<input name="fname" id="fname" runat="server" class="form-control" type="text" placeholder="INR">--%>
                                                <Anthem:TextBox runat="server" ID="Txt_Amount" class="form-control" placeholder="INR" onkeydown="return NumberOnly(event,this);" ondrop="return false" onpaste="return false" AutoUpdateAfterCallBack="true" MaxLength="8"></Anthem:TextBox>
                                            </div>
                                        </div>
                                        <h6 style="margin-bottom: 0px;">Step 2: Your Details</h6>
                                        <div class="form-group form-group-sm">
                                            <label class="col-sm-3 control-label">Name <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <%--   <input name="lname" id="lname" runat="server" class="form-control" type="text" placeholder="Name">--%>
                                                <Anthem:TextBox runat="server" ID="txtname" class="form-control" placeholder="Name" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                            </div>
                                            <label class="col-sm-3 control-label">Email <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <Anthem:TextBox runat="server" ID="Textemail" class="form-control" placeholder="Email" AutoUpdateAfterCallBack="true" MaxLength="35"></Anthem:TextBox>
                                                <%--<input name="email" id="email" runat="server" class="form-control"  placeholder="Email">--%>
                                            </div>
                                        </div>

                                        <div class="form-group form-group-sm">
                                            <label class="col-sm-3 control-label">Country Code <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <%--<Anthem:DropDownList ID="cars" runat="server" CssClass="form-control" name="PhoneCode">
                                                    <asp:ListItem Text="US / Canada (+1)" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="India (+91)" Value="91" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Afghanistan (+93)" Value="93"></asp:ListItem>
                                                    <asp:ListItem Text="Aland Islands (+358)" Value="358"></asp:ListItem>
                                                    <asp:ListItem Text="Albania (+355)" Value="355"></asp:ListItem>
                                                    <asp:ListItem Text="Algeria (+213)" Value="213"></asp:ListItem>
                                                    <asp:ListItem Text="American Samoa (+1684)" Value="1684"></asp:ListItem>
                                                    <asp:ListItem Text="Andorra (+376)" Value="376"></asp:ListItem>
                                                    <asp:ListItem Text="Angola (+244)" Value="244"></asp:ListItem>
                                                    <asp:ListItem Text="Anguilla (+1264)" Value="1264"></asp:ListItem>
                                                    <asp:ListItem Text="Antarctica (+672)" Value="672"></asp:ListItem>
                                                    <asp:ListItem Text="Antigua and Barbuda (+1268)" Value="1268"></asp:ListItem>
                                                    <asp:ListItem Text="Argentina (+54)" Value="54"></asp:ListItem>
                                                    <asp:ListItem Text="Armenia (+374)" Value="374"></asp:ListItem>
                                                    <asp:ListItem Text="Aruba (+297)" Value="297"></asp:ListItem>
                                                    <asp:ListItem Text="Australia (+61)" Value="61"></asp:ListItem>
                                                    <asp:ListItem Text="Austria (+43)" Value="43"></asp:ListItem>
                                                    <asp:ListItem Text="Azerbaijan (+994)" Value="994"></asp:ListItem>
                                                    <asp:ListItem Text="Bahamas (+1242)" Value="1242"></asp:ListItem>
                                                    <asp:ListItem Text="Bahrain (+973)" Value="973"></asp:ListItem>
                                                    <asp:ListItem Text="Bangladesh (+880)" Value="880"></asp:ListItem>
                                                    <asp:ListItem Text="Barbados (+1246)" Value="1246"></asp:ListItem>
                                                    <asp:ListItem Text="Belarus (+375)" Value="375"></asp:ListItem>
                                                    <asp:ListItem Text="Belgium (+32)" Value="32"></asp:ListItem>
                                                    <asp:ListItem Text="Belize (+501)" Value="501"></asp:ListItem>
                                                    <asp:ListItem Text="Benin (+229)" Value="229"></asp:ListItem>
                                                    <asp:ListItem Text="Bermuda (+1441)" Value="1441"></asp:ListItem>
                                                    <asp:ListItem Text="Bhutan (+975)" Value="975"></asp:ListItem>
                                                    <asp:ListItem Text="Bolivia, Plurinational State of (+591)" Value="591"></asp:ListItem>
                                                    <asp:ListItem Text="Bosnia and Herzegovina (+387)" Value="387"></asp:ListItem>
                                                    <asp:ListItem Text="Botswana (+267)" Value="267"></asp:ListItem>
                                                    <asp:ListItem Text="Brazil (+55)" Value="55"></asp:ListItem>
                                                    <asp:ListItem Text="British Indian Ocean Territory (+246)" Value="246"></asp:ListItem>
                                                    <asp:ListItem Text="Brunei Darussalam (+673)" Value="673"></asp:ListItem>
                                                    <asp:ListItem Text="Bulgaria (+359)" Value="359"></asp:ListItem>
                                                    <asp:ListItem Text="Burkina Faso (+226)" Value="226"></asp:ListItem>
                                                    <asp:ListItem Text="Burundi (+257)" Value="257"></asp:ListItem>
                                                    <asp:ListItem Text="Cambodia (+855)" Value="855"></asp:ListItem>
                                                    <asp:ListItem Text="Cameroon (+237)" Value="237"></asp:ListItem>
                                                    <asp:ListItem Text="Canada (+1)" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Cape Verde (+238)" Value="238"></asp:ListItem>
                                                    <asp:ListItem Text="Cayman Islands (+345)" Value="345"></asp:ListItem>
                                                    <asp:ListItem Text="Central African Republic (+236)" Value="236"></asp:ListItem>
                                                    <asp:ListItem Text="Chad (+235)" Value="235"></asp:ListItem>
                                                    <asp:ListItem Text="Chile (+56)" Value="56"></asp:ListItem>
                                                    <asp:ListItem Text="China (+86)" Value="86"></asp:ListItem>
                                                    <asp:ListItem Text="Christmas Island (+61)" Value="61"></asp:ListItem>
                                                    <asp:ListItem Text="Cocos (Keeling) Islands (+61)" Value="61"></asp:ListItem>
                                                    <asp:ListItem Text="Colombia (+57)" Value="57"></asp:ListItem>
                                                    <asp:ListItem Text="Comoros (+269)" Value="269"></asp:ListItem>
                                                    <asp:ListItem Text="Congo (+242)" Value="242"></asp:ListItem>
                                                    <asp:ListItem Text="Congo, The Democratic Republic of the Congo (+243)" Value="243"></asp:ListItem>
                                                    <asp:ListItem Text="Cook Islands (+682)" Value="682"></asp:ListItem>
                                                    <asp:ListItem Text="Costa Rica (+506)" Value="506"></asp:ListItem>
                                                    <asp:ListItem Text="Cote d'Ivoire (+225)" Value="225"></asp:ListItem>
                                                    <asp:ListItem Text="Croatia (+385)" Value="385"></asp:ListItem>
                                                    <asp:ListItem Text="Cuba (+53)" Value="53"></asp:ListItem>
                                                    <asp:ListItem Text="Cyprus (+357)" Value="357"></asp:ListItem>
                                                    <asp:ListItem Text="Czech Republic (+420)" Value="420"></asp:ListItem>
                                                    <asp:ListItem Text="Denmark (+45)" Value="45"></asp:ListItem>
                                                    <asp:ListItem Text="Djibouti (+253)" Value="253"></asp:ListItem>
                                                    <asp:ListItem Text="Dominica (+1767)" Value="1767"></asp:ListItem>
                                                    <asp:ListItem Text="Dominican Republic (+1849)" Value="1849"></asp:ListItem>
                                                    <asp:ListItem Text="Ecuador (+593)" Value="593"></asp:ListItem>
                                                    <asp:ListItem Text="Egypt (+20)" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="El Salvador (+503)" Value="503"></asp:ListItem>
                                                    <asp:ListItem Text="Equatorial Guinea (+240)" Value="240"></asp:ListItem>
                                                    <asp:ListItem Text="Eritrea (+291)" Value="291"></asp:ListItem>
                                                    <asp:ListItem Text="Estonia (+372)" Value="372"></asp:ListItem>
                                                    <asp:ListItem Text="Ethiopia (+251)" Value="251"></asp:ListItem>
                                                    <asp:ListItem Text="Falkland Islands (Malvinas) (+500)" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="Faroe Islands (+298)" Value="298"></asp:ListItem>
                                                    <asp:ListItem Text="Fiji (+679)" Value="679"></asp:ListItem>
                                                    <asp:ListItem Text="Finland (+358)" Value="358"></asp:ListItem>
                                                    <asp:ListItem Text="France (+33)" Value="33"></asp:ListItem>
                                                    <asp:ListItem Text="French Guiana (+594)" Value="594"></asp:ListItem>
                                                    <asp:ListItem Text="French Polynesia (+689)" Value="689"></asp:ListItem>
                                                    <asp:ListItem Text="Gabon (+241)" Value="241"></asp:ListItem>
                                                    <asp:ListItem Text="Gambia (+220)" Value="220"></asp:ListItem>
                                                    <asp:ListItem Text="Georgia (+995)" Value="995"></asp:ListItem>
                                                    <asp:ListItem Text="Germany (+49)" Value="49"></asp:ListItem>
                                                    <asp:ListItem Text="Ghana (+233)" Value="233"></asp:ListItem>
                                                    <asp:ListItem Text="Gibraltar (+350)" Value="350"></asp:ListItem>
                                                    <asp:ListItem Text="Greece (+30)" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="Greenland (+299)" Value="299"></asp:ListItem>
                                                    <asp:ListItem Text="Grenada (+1473)" Value="1473"></asp:ListItem>
                                                    <asp:ListItem Text="Guadeloupe (+502)" Value="502"></asp:ListItem>
                                                    <asp:ListItem Text="Guernsey (+44)" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="Guinea (+224)" Value="224"></asp:ListItem>
                                                    <asp:ListItem Text="Guinea-Bissau (+245)" Value="245"></asp:ListItem>
                                                    <asp:ListItem Text="Guyana (+595)" Value="595"></asp:ListItem>
                                                    <asp:ListItem Text="Haiti (+509)" Value="509"></asp:ListItem>
                                                    <asp:ListItem Text="Holy See (Vatican City State) (+379)" Value="379"></asp:ListItem>
                                                    <asp:ListItem Text="Honduras (+504)" Value="504"></asp:ListItem>
                                                    <asp:ListItem Text="Hong Kong (+852)" Value="852"></asp:ListItem>
                                                    <asp:ListItem Text="Hungary (+36)" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="Iceland (+354)" Value="354"></asp:ListItem>
                                                    <asp:ListItem Text="Indonesia (+62)" Value="62"></asp:ListItem>
                                                    <asp:ListItem Text="Iran, Islamic Republic of Persian Gulf (+98)" Value="98"></asp:ListItem>
                                                    <asp:ListItem Text="Iraq (+964)" Value="964"></asp:ListItem>
                                                    <asp:ListItem Text="Ireland (+353)" Value="353"></asp:ListItem>
                                                    <asp:ListItem Text="Isle of Man (+44)" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="Israel (+972)" Value="972"></asp:ListItem>
                                                    <asp:ListItem Text="Italy (+39)" Value="39"></asp:ListItem>
                                                    <asp:ListItem Text="Jamaica (+1876)" Value="1876"></asp:ListItem>
                                                    <asp:ListItem Text="Japan (+81)" Value="81"></asp:ListItem>
                                                    <asp:ListItem Text="Jersey (+44)" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="Jordan (+962)" Value="962"></asp:ListItem>
                                                    <asp:ListItem Text="Kazakhstan (+77)" Value="77"></asp:ListItem>
                                                    <asp:ListItem Text="Kenya (+254)" Value="254"></asp:ListItem>
                                                    <asp:ListItem Text="Kiribati (+686)" Value="686"></asp:ListItem>
                                                    <asp:ListItem Text="Korea, Democratic People's Republic of Korea (+850)" Value="850"></asp:ListItem>
                                                    <asp:ListItem Text="Korea, Republic of South Korea (+82)" Value="82"></asp:ListItem>
                                                    <asp:ListItem Text="Kuwait (+965)" Value="965"></asp:ListItem>
                                                    <asp:ListItem Text="Kyrgyzstan (+996)" Value="996"></asp:ListItem>
                                                    <asp:ListItem Text="Laos (+856)" Value="856"></asp:ListItem>
                                                    <asp:ListItem Text="Latvia (+371)" Value="371"></asp:ListItem>
                                                    <asp:ListItem Text="Lebanon (+961)" Value="961"></asp:ListItem>
                                                    <asp:ListItem Text="Lesotho (+266)" Value="266"></asp:ListItem>
                                                    <asp:ListItem Text="Liberia (+590)" Value="590"></asp:ListItem>
                                                    <asp:ListItem Text="Libyan Arab Jamahiriya (+218)" Value="218"></asp:ListItem>
                                                    <asp:ListItem Text="Liechtenstein (+423)" Value="423"></asp:ListItem>
                                                    <asp:ListItem Text="Lithuania (+370)" Value="370"></asp:ListItem>
                                                    <asp:ListItem Text="Luxembourg (+352)" Value="352"></asp:ListItem>
                                                    <asp:ListItem Text="Macao (+853)" Value="853"></asp:ListItem>
                                                    <asp:ListItem Text="Macedonia (+389)" Value="389"></asp:ListItem>
                                                    <asp:ListItem Text="Madagascar (+261)" Value="261"></asp:ListItem>
                                                    <asp:ListItem Text="Malawi (+265)" Value="265"></asp:ListItem>
                                                    <asp:ListItem Text="Malaysia (+60)" Value="60"></asp:ListItem>
                                                    <asp:ListItem Text="Maldives (+960)" Value="960"></asp:ListItem>
                                                    <asp:ListItem Text="Mali (+223)" Value="223"></asp:ListItem>
                                                    <asp:ListItem Text="Malta (+356)" Value="356"></asp:ListItem>
                                                    <asp:ListItem Text="Marshall Islands (+692)" Value="692"></asp:ListItem>
                                                    <asp:ListItem Text="Martinique (+596)" Value="596"></asp:ListItem>
                                                    <asp:ListItem Text="Mauritania (+222)" Value="222"></asp:ListItem>
                                                    <asp:ListItem Text="Mauritius (+230)" Value="230"></asp:ListItem>
                                                    <asp:ListItem Text="Mayotte (+262)" Value="262"></asp:ListItem>
                                                    <asp:ListItem Text="Mexico (+52)" Value="52"></asp:ListItem>
                                                    <asp:ListItem Text="Micronesia, Federated States of Micronesia (+691)" Value="691"></asp:ListItem>
                                                    <asp:ListItem Text="Moldova (+373)" Value="373"></asp:ListItem>
                                                    <asp:ListItem Text="Mongolia (+976)" Value="976"></asp:ListItem>
                                                    <asp:ListItem Text="Montenegro (+382)" Value="382"></asp:ListItem>
                                                    <asp:ListItem Text="Montserrat (+1664)" Value="1664"></asp:ListItem>
                                                    <asp:ListItem Text="Morocco (+212)" Value="212"></asp:ListItem>
                                                    <asp:ListItem Text="Mozambique (+258)" Value="258"></asp:ListItem>
                                                    <asp:ListItem Text="Myanmar (+95)" Value="95"></asp:ListItem>
                                                    <asp:ListItem Text="Namibia (+264)" Value="264"></asp:ListItem>
                                                    <asp:ListItem Text="Nauru (+674)" Value="674"></asp:ListItem>
                                                    <asp:ListItem Text="Nepal (+977)" Value="977"></asp:ListItem>
                                                    <asp:ListItem Text="Netherlands (+31)" Value="31"></asp:ListItem>
                                                    <asp:ListItem Text="Netherlands Antilles (+599)" Value="599"></asp:ListItem>
                                                    <asp:ListItem Text="New Caledonia (+687)" Value="687"></asp:ListItem>
                                                    <asp:ListItem Text="New Zealand (+64)" Value="64"></asp:ListItem>
                                                    <asp:ListItem Text="Nicaragua (+505)" Value="505"></asp:ListItem>
                                                    <asp:ListItem Text="Niger (+227)" Value="227"></asp:ListItem>
                                                    <asp:ListItem Text="Nigeria (+234)" Value="234"></asp:ListItem>
                                                    <asp:ListItem Text="Niue (+683)" Value="683"></asp:ListItem>
                                                    <asp:ListItem Text="Norfolk Island (+672)" Value="672"></asp:ListItem>
                                                    <asp:ListItem Text="Northern Mariana Islands (+1670)" Value="1670"></asp:ListItem>
                                                    <asp:ListItem Text="Norway (+47)" Value="47"></asp:ListItem>
                                                    <asp:ListItem Text="Oman (+590)" Value="590"></asp:ListItem>
                                                    <asp:ListItem Text="Pakistan (+92)" Value="92"></asp:ListItem>
                                                    <asp:ListItem Text="Palau (+680)" Value="680"></asp:ListItem>
                                                    <asp:ListItem Text="Palestinian Territory, Occupied (+970)" Value="970"></asp:ListItem>
                                                    <asp:ListItem Text="Panama (+507)" Value="507"></asp:ListItem>
                                                    <asp:ListItem Text="Papua New Guinea (+675)" Value="675"></asp:ListItem>
                                                    <asp:ListItem Text="Paraguay (+595)" Value="595"></asp:ListItem>
                                                    <asp:ListItem Text="Peru (+51)" Value="51"></asp:ListItem>
                                                    <asp:ListItem Text="Philippines (+63)" Value="63"></asp:ListItem>
                                                    <asp:ListItem Text="Pitcairn (+872)" Value="872"></asp:ListItem>
                                                    <asp:ListItem Text="Poland (+48)" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="Portugal (+351)" Value="351"></asp:ListItem>
                                                    <asp:ListItem Text="Puerto Rico (+1939)" Value="1939"></asp:ListItem>
                                                    <asp:ListItem Text="Qatar (+974)" Value="974"></asp:ListItem>
                                                    <asp:ListItem Text="Reunion (+262)" Value="262"></asp:ListItem>
                                                    <asp:ListItem Text="Romania (+40)" Value="40"></asp:ListItem>
                                                    <asp:ListItem Text="Russia (+7)" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Rwanda (+250)" Value="250"></asp:ListItem>
                                                    <asp:ListItem Text="Saint Barthelemy (+290)" Value="290"></asp:ListItem>
                                                    <asp:ListItem Text="Saint Helena, Ascension and Tristan Da Cunha (+290)" Value="290"></asp:ListItem>
                                                    <asp:ListItem Text="Saint Kitts and Nevis (+1869)" Value="1869"></asp:ListItem>
                                                    <asp:ListItem Text="Saint Lucia (+1758)" Value="1758"></asp:ListItem>
                                                    <asp:ListItem Text="Saint Martin (+590)" Value="590"></asp:ListItem>
                                                    <asp:ListItem Text="Saint Pierre and Miquelon (+508)" Value="508"></asp:ListItem>
                                                    <asp:ListItem Text="Saint Vincent and the Grenadines (+1784)" Value="1784"></asp:ListItem>
                                                    <asp:ListItem Text="Samoa (+685)" Value="685"></asp:ListItem>
                                                    <asp:ListItem Text="San Marino (+378)" Value="378"></asp:ListItem>
                                                    <asp:ListItem Text="Sao Tome and Principe (+239)" Value="239"></asp:ListItem>
                                                    <asp:ListItem Text="Saudi Arabia (+966)" Value="966"></asp:ListItem>
                                                    <asp:ListItem Text="Senegal (+221)" Value="221"></asp:ListItem>
                                                    <asp:ListItem Text="Serbia (+381)" Value="381"></asp:ListItem>
                                                    <asp:ListItem Text="Seychelles (+248)" Value="248"></asp:ListItem>
                                                    <asp:ListItem Text="Sierra Leone (+232)" Value="232"></asp:ListItem>
                                                    <asp:ListItem Text="Singapore (+65)" Value="65"></asp:ListItem>
                                                    <asp:ListItem Text="Slovakia (+421)" Value="421"></asp:ListItem>
                                                    <asp:ListItem Text="Slovenia (+386)" Value="386"></asp:ListItem>
                                                    <asp:ListItem Text="Solomon Islands (+677)" Value="677"></asp:ListItem>
                                                    <asp:ListItem Text="Somalia (+252)" Value="252"></asp:ListItem>
                                                    <asp:ListItem Text="South Africa (+27)" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="South Georgia and the South Sandwich Islands (+500)" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="South Sudan (+211)" Value="211"></asp:ListItem>
                                                    <asp:ListItem Text="Spain (+34)" Value="34"></asp:ListItem>
                                                    <asp:ListItem Text="Sri Lanka (+94)" Value="94"></asp:ListItem>
                                                    <asp:ListItem Text="Sudan (+249)" Value="249"></asp:ListItem>
                                                    <asp:ListItem Text="Suriname (+597)" Value="597"></asp:ListItem>
                                                    <asp:ListItem Text="Svalbard and Jan Mayen (+47)" Value="47"></asp:ListItem>
                                                    <asp:ListItem Text="Swaziland (+268)" Value="268"></asp:ListItem>
                                                    <asp:ListItem Text="Sweden (+46)" Value="46"></asp:ListItem>
                                                    <asp:ListItem Text="Switzerland (+41)" Value="41"></asp:ListItem>
                                                    <asp:ListItem Text="Syrian Arab Republic (+963)" Value="963"></asp:ListItem>
                                                    <asp:ListItem Text="Taiwan (+886)" Value="886"></asp:ListItem>
                                                    <asp:ListItem Text="Tajikistan (+992)" Value="992"></asp:ListItem>
                                                    <asp:ListItem Text="Tanzania, United Republic of Tanzania  (+255)" Value="255"></asp:ListItem>
                                                    <asp:ListItem Text="Thailand (+66)" Value="66"></asp:ListItem>
                                                    <asp:ListItem Text="Timor-Leste (+670)" Value="670"></asp:ListItem>
                                                    <asp:ListItem Text="Togo (+228)" Value="228"></asp:ListItem>
                                                    <asp:ListItem Text="Tokelau (+690)" Value="690"></asp:ListItem>
                                                    <asp:ListItem Text="Tonga (+676)" Value="676"></asp:ListItem>
                                                    <asp:ListItem Text="Trinidad and Tobago (+1868)" Value="1868"></asp:ListItem>
                                                    <asp:ListItem Text="Tunisia (+216)" Value="216"></asp:ListItem>
                                                    <asp:ListItem Text="Turkey (+90)" Value="90"></asp:ListItem>
                                                    <asp:ListItem Text="Turkmenistan (+993)" Value="993"></asp:ListItem>
                                                    <asp:ListItem Text="Turks and Caicos Islands (+1649)" Value="1649"></asp:ListItem>
                                                    <asp:ListItem Text="Tuvalu (+688)" Value="688"></asp:ListItem>
                                                    <asp:ListItem Text="Uganda (+256)" Value="256"></asp:ListItem>
                                                    <asp:ListItem Text="Ukraine (+380)" Value="380"></asp:ListItem>
                                                    <asp:ListItem Text="United Arab Emirates (+971)" Value="971"></asp:ListItem>
                                                    <asp:ListItem Text="United Kingdom (+44)" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="United States (+1)" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Uruguay (+598)" Value="598"></asp:ListItem>
                                                    <asp:ListItem Text="Uzbekistan (+998)" Value="998"></asp:ListItem>
                                                    <asp:ListItem Text="Vanuatu (+678)" Value="678"></asp:ListItem>
                                                    <asp:ListItem Text="Venezuela, Bolivarian Republic of Venezuela (+58)" Value="58"></asp:ListItem>
                                                    <asp:ListItem Text="Vietnam (+84)" Value="84"></asp:ListItem>
                                                    <asp:ListItem Text="Virgin Islands, British (+1284)" Value="1284"></asp:ListItem>
                                                    <asp:ListItem Text="Virgin Islands, U.S. (+1340)" Value="1340"></asp:ListItem>
                                                    <asp:ListItem Text="Wallis and Futuna (+681)" Value="681"></asp:ListItem>
                                                    <asp:ListItem Text="Yemen (+967)" Value="967"></asp:ListItem>
                                                    <asp:ListItem Text="Zambia (+260)" Value="260"></asp:ListItem>
                                                    <asp:ListItem Text="Zimbabwe (+263)" Value="263"></asp:ListItem>
                                                </Anthem:DropDownList>--%>
                                                <Anthem:DropDownList ID="ddlCountry" runat="server" name="PhoneCode" AutoCallBack="True" AutoUpdateAfterCallBack="true" CssClass="ChosenSelector"></Anthem:DropDownList>

                                            </div>
                                            <label class="col-sm-3 control-label">Mobile No. <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <Anthem:TextBox runat="server" ID="mobile" class="form-control" placeholder="Mobile Number" onkeydown="return NumberOnly(event,this);" AutoUpdateAfterCallBack="true" MaxLength="10"></Anthem:TextBox>
                                                <%--<input name="Mobile" id="mobile" class="form-control" type="text" placeholder="Mobile Number">--%>
                                            </div>
                                        </div>

                                        <div class="form-group form-group-sm">
                                            <label class="col-sm-3 control-label">Challan/Reference No. <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <Anthem:TextBox runat="server" ID="TxtDraft" class="form-control" placeholder="Challan/Reference No" AutoUpdateAfterCallBack="true" MaxLength="35"></Anthem:TextBox>
                                            </div>
                                            <label class="col-sm-3 control-label">Payment Date <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <Anthem:TextBox ID="Txt_paymentDate" runat="server" CssClass="form-control" Width="120"
                                                    SkinID="none" placeholder="DD/MM/YYYY"
                                                    AutoUpdateAfterCallBack="True" MaxLength="10" onkeypress="return false"
                                                    onpaste="return false;" ondrop="return false;" AutoCallBack="false" />
                                                <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00$ContentPlaceHolder1$Txt_paymentDate);return false;">
                                                    <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                                                        width="34" /></a>
                                                <%--<Anthem:TextBox runat="server" ID="Txt_paymentDate" AutoUpdateAfterCallBack="True" MaxLength="10"
                                                    onMouseDown="DisableRightClick(event);" onkeydown="return NoEntryrDateTextBox();" CssClass="textboxdates"></Anthem:TextBox>
                                                      <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_paymentDate);return false;">
                                                    <img align="absMiddle" alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                                                        width="34" />
                                                </a>--%>
                                            </div>
                                        </div>

                                        <div class="form-group form-group-sm">
                                            <label class="col-sm-3 control-label">Payment Type <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <Anthem:DropDownList ID="ddl_PaymentType" runat="server" AutoCallBack="True" AutoUpdateAfterCallBack="true" CssClass="ChosenSelector">
                                                    <asp:ListItem Value="0" Selected="True">--Select Payment Type--</asp:ListItem>
                                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                                    <asp:ListItem Value="2">Online </asp:ListItem>
                                                    <asp:ListItem Value="3">Offline </asp:ListItem>
                                                </Anthem:DropDownList>
                                            </div>
                                            <label class="col-sm-3 control-label">Reciever Bank A/c No: <span style="color: red">*</span></label>
                                            <div class="col-sm-3">
                                                <Anthem:TextBox runat="server" ID="TxtReciverBankAcc" class="form-control" placeholder="RecieverbankAccount" AutoUpdateAfterCallBack="true" MaxLength="20"></Anthem:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group form-check mb-0">
                                            <div class="col-sm-9 col-sm-offset-3">
                                                <input type="checkbox" class="form-check-input" runat="server" id="exampleCheck1">
                                                <label class="form-check-label" for="exampleCheck1">Make Anonymously</label>
                                                <p>* By disclosing your name, you are likely to encourage others to donate.</p>
                                            </div>
                                        </div>

                                        <div class="form-group mt-1">
                                            <div class="col-sm-9 col-sm-offset-3">
                                                <%-- <Anthem:Button ID="donate" runat="server" class="btnConDonate" OnClick="donate_Click" Text="Donate Now" AutoUpdateAfterCallBack="true"
                                                OnClientClick="this.style.display='none';document.getElementById('divLoadGif').style.display = 'block'" EnableCallBack="true" />--%>

                                                <Anthem:Button ID="donate" runat="server" AutoUpdateAfterCallBack="true" OnClick="donate_Click" CausesValidation="true"
                                                    class="btnConDonate" Text="Donate Now" />
                                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage"></Anthem:Label>
                                                <%-- <input class="readon2 banner-style ext btn-send"  type="submit" value="Donate Now">--%>
                                                <Anthem:Label ID="lblPaymentMsg" runat="server" AutoUpdateAfterCallBack="true" Font-Bold="true" ForeColor="Red" Text=""></Anthem:Label>
                                            </div>

                                        </div>

                                    </div>


                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js" class="findcal"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 9999; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>