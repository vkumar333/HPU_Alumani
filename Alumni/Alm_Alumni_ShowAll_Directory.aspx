<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_Alumni_ShowAll_Directory.aspx.cs" Inherits="Alumni_Alm_Alumni_ShowAll_Directory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="rs-breadcrumbs bg3 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Alumni Directory </h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 96)
                return false;
        }
    </script>
	
    <!-- Breadcrumbs End -->
    <!--  Start -->
    <div id="rs-team-2" class="rs-team-2 team-all pt-50 pb-50">
        <div class="container">
			<asp:Repeater ID="RepCount" runat="server">
                <ItemTemplate>
                    <div class="readbtnn mb-4">
                        <h4 style="text-align:left;"><%#Eval("pk_alumniid") %> Profile Records </h4>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <%--<div class="sec-title mb-20 md-mb-30">
                <h2 class="title mb-20">All Profiles </h2>
            </div>--%>
            <div class="row">
                <div class="col-lg-8" style="overflow-y: scroll; height: 700px;">
                    <div class="row">
                        <asp:Repeater ID="RepProfile" runat="server">
                            <ItemTemplate>
                                <div class="col-lg-4 col-md-6 col-xs-6">
                                    <div onclick="location.href='<%# string.Format("/Alumni/Alm_Alumni_Show_Alumni_Profile.aspx?ID={0}",HttpUtility.UrlEncode(Eval("pk_alumniid").ToString())) %>';" style="cursor: pointer;">
                                        <div class="team-item">
                                            <div class="team-img" style="height: 200px; width: 223px">
                                                <a href="#">
                                                    <%-- <img src="alumin-default-theme/images/a1.jpg" alt="" />--%>
                                                    <img id="Imge" src='<%# Eval("Filepath") %>' alt="">
                                                </a>
                                                <div class="social-icon">
                                                    <a href="#"><i class="fa fa-facebook"></i></a>
                                                    <a href="#"><i class="fa fa-twitter"></i></a>
                                                    <a href="#"><i class="fa fa-linkedin"></i></a>
                                                </div>
                                            </div>
                                            <div class="team-body">
                                                <a href="#">
                                                    <h3 class="name"><%#Eval("alumni_name") %></h3>
                                                </a>
                                                <span class="designation"><%#Eval("designation") %></span>
                                                <span class="designation">Shimla, Himachal Pradesh</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <%-- <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a2.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Abha Chauhan Khimta</h3>
                                    </a>
                                    <span class="designation">Class of 2010, Ph.D in Political Science</span>
                                    <span class="designation">Shimla, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a3.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Abhiraj Rajendra Mishra</h3>
                                    </a>
                                    <span class="designation">Faculty, Sanskrit</span>
                                    <span class="designation">Shimla, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a4.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Abhishek Mahajan</h3>
                                    </a>
                                    <span class="designation">Class of 2021, Education</span>
                                    <span class="designation">Chamba, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a5.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Aditi Bhadan</h3>
                                    </a>
                                    <span class="designation">Class of 2017, Computer Science</span>
                                    <span class="designation">Solan, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a6.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Aditi Sharma</h3>
                                    </a>
                                    <span class="designation">Class of 2003, MBA</span>
                                    <span class="designation">Dharmshala, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a7.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Ajay Sood</h3>
                                    </a>
                                    <span class="designation">Class of 2001, Economics</span>
                                    <span class="designation">Shimla, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a8.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Ajay Kumar Attri</h3>
                                    </a>
                                    <span class="designation">Class of 2005, M.Ed, Education</span>
                                    <span class="designation">Mandi, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a9.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">AJEET KUMAR</h3>
                                    </a>
                                    <span class="designation">Class of 2019, M.Phil in Physics, Physics</span>
                                    <span class="designation">Patiala, Punjab</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a10.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Ajit Kumar</h3>
                                    </a>
                                    <span class="designation">Class of 2007, M.Sc in Mathematics, Mathematics</span>
                                    <span class="designation">Kangra, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a11.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Amarjeet Singh</h3>
                                    </a>
                                    <span class="designation">Class of 2014, Ph.D in Physics, Physics</span>
                                    <span class="designation">Shimla, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-xs-6">
                            <div class="team-item">
                                <div class="team-img">
                                    <a href="#">
                                        <img src="alumin-default-theme/images/a12.jpg" alt="" /></a>
                                    <div class="social-icon">
                                        <a href="#"><i class="fa fa-facebook"></i></a>
                                        <a href="#"><i class="fa fa-twitter"></i></a>
                                        <a href="#"><i class="fa fa-linkedin"></i></a>
                                    </div>
                                </div>
                                <div class="team-body">
                                    <a href="#">
                                        <h3 class="name">Amarjeet K. Sharma</h3>
                                    </a>
                                    <span class="designation">Ph.D in Physics, Physics</span>
                                    <span class="designation">Shimla, Himachal Pradesh</span>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="course-syllabus">
                                <div class="sidebar-area">
                                    <div class="search-box">
                                        <h3 class="title">Search Alumni</h3>
                                        <div class="box-search">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <Anthem:TextBox ID="txtsearch" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button runat="server" ID="btnsearch" OnClick="btnsearch_Click" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="accordion" class="rs-accordion-style1 accordon">
                                    <%-- <div class="card">
                                        <div class="card-header" id="headingOne">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                <span>Search by Role </span>
                                            </h3>
                                        </div>
                                        <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <select class="form-control" name="Select_Role" id="Select Role">
                                                                <option value="1">Select Role
                                                                </option>
                                                                <option value="2">Admin
                                                                </option>
                                                                <option value="3">Alumni
                                                                </option>
                                                                <option value="4">Faculty
                                                                </option>
                                                                <option value="5">Student
                                                                </option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="card">
                                        <div class="card-header" id="headingTwo">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                <span>Year of Passing</span>
                                            </h3>
                                        </div>
                                        <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="search-box">

                                                            <div class="box-search">
                                                                <div class="row">
                                                                    <div class="col-md-8">
                                                                        <Anthem:TextBox ID="Txtyearofpassing" class="form-control" onkeypress="return isNumberKey(event)" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <Anthem:Button runat="server" ID="brnsearchpassing" OnClick="brnsearchpassing_Click" EnableCallBack="false" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn" Style="background: #fe9700; color: white;"></Anthem:Button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--  <div class="col-md-12">
                                                            <Anthem:DropDownList ID="D_ddlYeofPass" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="D_ddlYeofPass_SelectedIndexChanged"  class="form-control"></Anthem:DropDownList>--%>
                                                        <%-- <select class="form-control" name="Select_a Year_of_Joining" id="Select a Year of Joining">
                                                                <option value="1">Select a Year of Joining
                                                                </option>
                                                                <option value="2">2023
                                                                </option>
                                                                <option value="3">2022
                                                                </option>
                                                                <option value="4">2021
                                                                </option>
                                                                <option value="5">2020
                                                                </option>
                                                                <option value="6">2019
                                                                </option>
                                                                <option value="7">2018
                                                                </option>
                                                                <option value="8">2017
                                                                </option>
                                                                <option value="9">2016
                                                                </option>
                                                                <option value="10">2015
                                                                </option>
                                                            </select>--%>
                                                        <%--</div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <div class="card-header" id="headingThree">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                                <span>Course/Degree</span>
                                            </h3>
                                        </div>
                                        <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <%--<select class="form-control" name="Select_a_Last_Degree" id="Select a Last Degree">
                                                                <option value="1">Select a Last Degree
                                                                </option>
                                                                <option value="2">2023
                                                                </option>
                                                                <option value="3">2022
                                                                </option>
                                                                <option value="4">2021
                                                                </option>
                                                                <option value="5">2020
                                                                </option>
                                                                <option value="6">2019
                                                                </option>
                                                                <option value="7">2018
                                                                </option>
                                                                <option value="8">2017
                                                                </option>
                                                                <option value="9">2016
                                                                </option>
                                                                <option value="10">2015
                                                                </option>
                                                            </select>--%>
                                                            <div class="search-box">
                                                                <div class="box-search">
                                                                    <div class="row">
                                                                        <div class="col-md-8">
                                                                            <Anthem:TextBox ID="Txtdegree" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <Anthem:Button runat="server" ID="btndegreesearch" OnClick="btndegreesearch_Click" EnableCallBack="false" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn" Style="background: #fe9700; color: white;"></Anthem:Button>
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

                                    <%--  <div class="card">
                                        <div class="card-header" id="headingFour">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                                <span>Course/Degree </span>
                                            </h3>
                                        </div>
                                        <div id="collapseFour" class="collapse" aria-labelledby="headingFour" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <select class="form-control" name="Select_Course/Degree" id="Select Course/Degree">
                                                            <option value="1">Select Course/Degree
                                                            </option>
                                                            <option value="2">Bachelor of Hotel Management
                                                            </option>
                                                            <option value="3">BCA
                                                            </option>
                                                            <option value="4">MBA
                                                            </option>
                                                            <option value="5">MTTM
                                                            </option>
                                                            <option value="6">MCA
                                                            </option>
                                                            <option value="7">M.COM
                                                            </option>
                                                            <option value="8">M.Phil in Commerce
                                                            </option>
                                                            <option value="9">Ph.D in Commerce
                                                            </option>
                                                            <option value="10">BBA
                                                            </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="card">
                                        <div class="card-header" id="headingFive">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                                                <span>Division/Department</span>
                                            </h3>
                                        </div>
                                        <div id="collapseFive" class="collapse" aria-labelledby="headingFive" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <%-- <select class="form-control" name="Select_Division/Department" id="Select Division/Department">
                                                            <option value="1">Select Division/Department
                                                            </option>
                                                            <option value="2">Arts
                                                            </option>
                                                            <option value="3">Bio Sciences
                                                            </option>
                                                            <option value="4">Biotechnology
                                                            </option>
                                                            <option value="5">Business Administration
                                                            </option>
                                                            <option value="6">Chemistry
                                                            </option>
                                                            <option value="7">Commerce
                                                            </option>
                                                            <option value="8">Computer Science
                                                            </option>
                                                            <option value="9">Economics
                                                            </option>
                                                            <option value="10">Education
                                                            </option>
                                                        </select>--%>
                                                        <div class="search-box">
                                                            <div class="box-search">
                                                                <div class="row">
                                                                    <div class="col-md-8">
                                                                        <Anthem:TextBox ID="Txtdept" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <Anthem:Button runat="server" ID="Btndeptsearch" OnClick="Btndeptsearch_Click" EnableCallBack="false" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn" Style="background: #fe9700; color: white;"></Anthem:Button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <div class="card-header" id="headingSix">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseSix" aria-expanded="false" aria-controls="collapseSix">
                                                <span>Current Address </span>
                                            </h3>
                                        </div>
                                        <div id="collapseSix" class="collapse" aria-labelledby="headingSix" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="search-box">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <Anthem:TextBox ID="Txtaddress" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <Anthem:Button runat="server" ID="btnaddress" OnClick="btnaddress_Click" EnableCallBack="false" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn" Style="background: #fe9700; color: white;"></Anthem:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <div class="card-header" id="headingEight">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseEight" aria-expanded="false" aria-controls="collapseEight">
                                                <span>Company</span>
                                            </h3>
                                        </div>
                                        <div id="collapseEight" class="collapse" aria-labelledby="headingEight" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="search-box">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <Anthem:TextBox ID="Txtcomp" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <Anthem:Button runat="server" ID="btncomp" OnClick="btncomp_Click" EnableCallBack="false" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn" Style="background: #fe9700; color: white;"></Anthem:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <div class="card-header" id="headingNine">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseNine" aria-expanded="false" aria-controls="collapseNine">
                                                <span>Designation </span>
                                            </h3>
                                        </div>
                                        <div id="collapseNine" class="collapse" aria-labelledby="headingNine" data-parent="#accordion">
                                            <div class="card-body">
                                               <div class="search-box">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <Anthem:TextBox ID="Txtdesig" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <Anthem:Button runat="server" ID="btndesig" OnClick="btndesig_Click" EnableCallBack="false" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn" Style="background: #fe9700; color: white;"></Anthem:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>                                 
                                    <div class="card">
                                        <div class="card-header" id="headingEleven">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseEleven" aria-expanded="false" aria-controls="collapseEleven">
                                                <span>Skills </span>
                                            </h3>
                                        </div>
                                        <div id="collapseEleven" class="collapse" aria-labelledby="headingEleven" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="search-box">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <Anthem:TextBox ID="Txtskill" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <Anthem:Button runat="server" ID="Btnskill" OnClick="Btnskill_Click" EnableCallBack="false" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn" Style="background: #fe9700; color: white;"></Anthem:Button>
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
                </div>

            </div>
        </div>
    </div>
</asp:Content>

