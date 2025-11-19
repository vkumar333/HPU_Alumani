<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_Alumni_Directory.aspx.cs" Inherits="Alumni_Alm_Alumni_Directory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="Stylesheet" />

    <script>
        function ShowRoomDetails(obj) {
            document.getElementById('ViewDiv').style.display = 'block';
        }
    </script>

    <script>
        function openCard(DivId) {

            var Div = ($("#" + DivId));

            Div.collapse('show')
        }

        var a = jQuery.noConflict();
        a(document).ready(BindEvents);

        function BindEvents() {
            try {
                a(".ChosenSelector").chosen();
            }
            catch (err) {
            }
        }

        $(".ChosenSelector").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true })

    </script>
    <style>
        .main-content .box .form-control {
            font-size: 12px;
        }

        .chosen-container-single {
            width: 200px !important;
        }

            .chosen-container-single .chosen-single {
                height: 34px;
                line-height: 30px;
            }

                .chosen-container-single .chosen-single div {
                    top: 4px;
                }

    </style>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg3 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Alumni Directory</h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Breadcrumbs End -->
    <!--  Start -->
    <div id="rs-team-2" class="rs-team-2 team-all pt-50 pb-50">
        <div class="container">
            <%--<div class="readbtnn mb-4">
                <h4 style="text-align:left;">All Profiles </h4>
            </div>--%>
            <Anthem:Label ID="lblProfileCnt" runat="server" AutoUpdateAfterCallBack="True" Visible="false"></Anthem:Label>
            <asp:Repeater ID="RepCount" runat="server">
                <ItemTemplate>
                    <div class="readbtnn mb-4">
                        <h4><%#Eval("pk_alumniid") %> More Alumni are Active on HPU Community</h4>
                        <p>Complete directory of the HPU Community is restricted for registered members.</p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="row">
                <div class="col-lg-8" style="overflow-y: scroll; height: 700px;">
                    <div class="row">
                        <asp:Repeater ID="RepProfile" runat="server">
                            <ItemTemplate>
                                <div class="col-lg-4 col-md-6 col-xs-6">
                                    <div class="team-item">
                                        <div class="team-img" style="height: 200px; width: 223px">
                                            <a href="#">
                                                <img id="Imge" src='<%# Eval("Filepath") %>' alt="">
                                            </a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div class="social-icon social-directory">
                                                <a href="#"><i class="fa fa-facebook"></i></a>
                                                <a href="#"><i class="fa fa-twitter"></i></a>
                                                <a href="#"><i class="fa fa-linkedin"></i></a>
                                            </div>
                                        </div>
                                        <div class="team-body">
                                            <a href="#">
                                                <Anthem:LinkButton data-toggle="modal" ID="btnname" CssClass="name" runat="server" OnClick="btnname_Click" href="#" data-target="#member" Text='<%#Eval("alumni_name") %>' CommandArgument='<%# Eval("pk_alumniid") %>'></Anthem:LinkButton>
                                                <h3 class="name"></h3>
                                            </a>
                                            <span class="designation"><%#Eval("designation") %></span>
                                            <span class="designation"><%#Eval("Current_Location") %></span>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="text-center">
                        <a class="readon2 banner-style text-center">
                            <Anthem:Button data-toggle="modal" ID="btnview" runat="server" href="#" data-target="#member" Text="View All" OnClick="btnview_Click" />
                            <%--<Anthem:Button data-toggle="modal" ID="btnview" runat="server" href="#"  OnClick="btnview_Click" Text="View All"></Anthem:Button>--%>
                            <%--    <a class="readon2 banner-style text-center" href="#">View All <i class="fa fa-angle-right" aria-hidden="true"></i></a>--%>

                        </a>
                    </div>

                </div>
                <div class="col-lg-4">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="course-syllabus">
                                <div class="sidebar-area">
                                    <div class="search-box">
                                        <h3 class="title">Search By Alumni Name</h3>
                                        <div class="box-search">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <Anthem:TextBox ID="txtsearch" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <Anthem:Button runat="server" ID="btnAlmSearch" OnClick="btnAlmSearch_Click" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn" data-toggle="modal"></Anthem:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--<div id="accordion" class="rs-accordion-style1 accordon">

                                    <div class="card" onmouseover="openCard('collapseOne')">
                                        <div class="card-header" id="headingOne">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                <span>Select a Year of Passing </span>
                                            </h3>
                                        </div>
                                        <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="box-search">
                                                            <div class="row">
                                                                <div class="col-md-8">
                                                                    <Anthem:TextBox ID="TextBox3" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <Anthem:Button runat="server" data-toggle="modal" ID="Button1" Style="background: #fe9700; color: white;" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn"></Anthem:Button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                                                       
                                    <div class="card" onmouseover="openCard('collapseThree')">
                                        <div class="card-header" id="headingThree">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="true" aria-controls="collapseThree">
                                                <span>Last Degree </span>
                                            </h3>
                                        </div>
                                        <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="box-search">
                                                            <div class="row">
                                                                <div class="col-md-8">
                                                                    <Anthem:TextBox ID="TextBox5" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <Anthem:Button runat="server" data-toggle="modal" ID="Button2" Style="background: #fe9700; color: white;" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn"></Anthem:Button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseFour')">
                                        <div class="card-header" id="headingFour">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="true" aria-controls="collapseFour">
                                                <span>Course/Degree </span>
                                            </h3>
                                        </div>
                                        <div id="collapseFour" class="collapse" aria-labelledby="headingFour" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <Anthem:TextBox ID="TextBox6" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <Anthem:Button runat="server" data-toggle="modal" ID="Button3" Style="background: #fe9700; color: white;" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn"></Anthem:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseFive')">
                                        <div class="card-header" id="headingFive">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="true" aria-controls="collapseFive">
                                                <span>Division/Department</span>
                                            </h3>
                                        </div>
                                        <div id="collapseFive" class="collapse" aria-labelledby="headingFive" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <Anthem:TextBox ID="TextBox7" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <Anthem:Button runat="server" data-toggle="modal" ID="Button4" Style="background: #fe9700; color: white;" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn"></Anthem:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseSix')">
                                        <div class="card-header" id="headingSix">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseSix" aria-expanded="true" aria-controls="collapseSix">
                                                <span>Current Location </span>
                                            </h3>
                                        </div>
                                        <div id="collapseSix" class="collapse" aria-labelledby="headingSix" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="box-search">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <Anthem:TextBox ID="TextBox8" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <Anthem:Button runat="server" data-toggle="modal" ID="Button5" Style="background: #fe9700; color: white;" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn"></Anthem:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseEight')">
                                        <div class="card-header" id="headingEight">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseEight" aria-expanded="true" aria-controls="collapseEight">
                                                <span>Company</span>
                                            </h3>
                                        </div>
                                        <div id="collapseEight" class="collapse" aria-labelledby="headingEight" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="box-search">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <Anthem:TextBox ID="TextBox2" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <Anthem:Button runat="server" data-toggle="modal" ID="Button6" Style="background: #fe9700; color: white;" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn"></Anthem:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="card" onmouseover="openCard('collapseNine')">
                                        <div class="card-header" id="headingNine">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseNine" aria-expanded="true" aria-controls="collapseNine">
                                                <span>Designation </span>
                                            </h3>
                                        </div>
                                        <div id="collapseNine" class="collapse" aria-labelledby="headingNine" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="box-search">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <Anthem:TextBox ID="TextBox4" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <Anthem:Button runat="server" data-toggle="modal" ID="Button7" Style="background: #fe9700; color: white;" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn"></Anthem:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                                                      
                                    <div class="card" onmouseover="openCard('collapseten')">
                                        <div class="card-header" id="headingtenth">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseten" aria-expanded="true" aria-controls="collapseten">
                                                <span>Skills </span>
                                            </h3>
                                        </div>
                                        <div id="collapseten" class="collapse" aria-labelledby="headingtenth" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="box-search">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <Anthem:TextBox ID="TextBox1" class="form-control" placeholder="Search Here ..." name="srch-term" runat="server"></Anthem:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <Anthem:Button runat="server" data-toggle="modal" ID="Button8" Style="background: #fe9700; color: white;" Text="SEARCH" data-target="#member" AutoUpdateAfterCallBack="true" CssClass="btn"></Anthem:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                
                                </div>--%>

                                <div id="accordion" class="rs-accordion-style1 accordon">

                                    <div class="card" onmouseover="openCard('collapseTwelve')">
                                        <div class="card-header" id="headingTwelve">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseTwelve" aria-expanded="true" aria-controls="collapseTwelve">
                                                <span>Search by Role </span>
                                            </h3>
                                        </div>
                                        <div id="collapseTwelve" class="collapse" aria-labelledby="headingTwelve" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="search-box">
                                                            <div class="box-search">
                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                        <Anthem:DropDownList ID="ddlRole"
                                                                            runat="server"
                                                                            AutoCallBack="false"
                                                                            CssClass="ChosenSelector"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            TabIndex="21">
                                                                            <asp:ListItem Text="-- Select Role --" Value=""></asp:ListItem>
                                                                            <asp:ListItem Text="Alumni" Value="ExStu"></asp:ListItem>
                                                                            <asp:ListItem Text="Faculty" Value="F"></asp:ListItem>
                                                                            <asp:ListItem Text="Student" Value="S"></asp:ListItem>
                                                                        </Anthem:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <Anthem:Button ID="btnRoleSearch"
                                                                            runat="server"
                                                                            OnClick="btnRoleSearch_Click"
                                                                            Text="SEARCH"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            CssClass="btn"
                                                                            Style="background: #fe9700; color: white; width: 100px;"
                                                                            data-target="#member"
                                                                            data-toggle="modal"></Anthem:Button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapsethirteen')">
                                        <div class="card-header" id="headingthirteen">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapsethirteen" aria-expanded="true" aria-controls="collapsethirteen">
                                                <span>Membership </span>
                                            </h3>
                                        </div>
                                        <div id="collapsethirteen" class="collapse" aria-labelledby="headingthirteen" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="search-box">
                                                            <div class="box-search">
                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                        <Anthem:DropDownList ID="ddlMembership"
                                                                            runat="server"
                                                                            AutoCallBack="false"
                                                                            CssClass="ChosenSelector"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            TabIndex="21">
                                                                            <asp:ListItem Text="-- Select Membership --" Value=""></asp:ListItem>
                                                                            <asp:ListItem Text="Life Membership" Value="LM"></asp:ListItem>
                                                                            <asp:ListItem Text="Student Membership" Value="SM"></asp:ListItem>
                                                                        </Anthem:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <Anthem:Button ID="btnMemSearch"
                                                                            runat="server"
                                                                            OnClick="btnMemSearch_Click"
                                                                            Text="SEARCH"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            CssClass="btn"
                                                                            Style="background: #fe9700; color: white; width: 100px;"
                                                                            data-target="#member"
                                                                            data-toggle="modal"></Anthem:Button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseFourteen')">
                                        <div class="card-header" id="headingFourteen">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseFourteen" aria-expanded="true" aria-controls="collapseFourteen">
                                                <span>Gender </span>
                                            </h3>
                                        </div>
                                        <div id="collapseFourteen" class="collapse" aria-labelledby="headingFourteen" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="search-box">
                                                            <div class="box-search">
                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                        <Anthem:DropDownList ID="ddlGender"
                                                                            runat="server"
                                                                            AutoCallBack="false"
                                                                            CssClass="ChosenSelector"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            TabIndex="21">
                                                                            <asp:ListItem Text="-- Select Gender --" Value=""></asp:ListItem>
                                                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                                            <asp:ListItem Text="Others" Value="O"></asp:ListItem>
                                                                        </Anthem:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <Anthem:Button ID="btnGenderSearch"
                                                                            runat="server"
                                                                            OnClick="btnGenderSearch_Click"
                                                                            Text="SEARCH"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            CssClass="btn"
                                                                            Style="background: #fe9700; color: white; width: 100px;"
                                                                            data-target="#member"
                                                                            data-toggle="modal"></Anthem:Button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseOne')">
                                        <div class="card-header" id="headingTwo">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">
                                                <span>Year of Passing</span>
                                            </h3>
                                        </div>
                                        <div id="collapseTwo" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="search-box">

                                                            <div class="box-search">

                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                        <Anthem:DropDownList ID="ddlYearOfPassing" runat="server"
                                                                            AutoCallBack="true"
                                                                            CssClass="ChosenSelector"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            TabIndex="21"
                                                                            OnSelectedIndexChanged="ddlYearOfPassing_SelectedIndexChanged">
                                                                        </Anthem:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <Anthem:Button runat="server" ID="brnsearchpassing"
                                                                            OnClick="brnsearchpassing_Click"
                                                                            Text="SEARCH"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            CssClass="btn"
                                                                            Style="background: #fe9700; color: white; width: 100px;"
                                                                            data-target="#member"
                                                                            data-toggle="modal"></Anthem:Button>
                                                                    </div>
                                                                    <div class="col-md-12 mt-1">
                                                                        <Anthem:CheckBoxList ID="chkPassingYear" runat="server"
                                                                            CssClass="checkbox-inline custom-checkbox-inline" SkinID="none"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            RepeatLayout="Flow"
                                                                            RepeatDirection="Horizontal">
                                                                        </Anthem:CheckBoxList>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseThree')">
                                        <div class="card-header" id="headingThree">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="true" aria-controls="collapseThree">
                                                <span>Course/Degree</span>
                                            </h3>
                                        </div>
                                        <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="search-box">
                                                                <div class="box-search">
                                                                    <div class="row">
                                                                        <div class="col-md-7">
                                                                            <Anthem:DropDownList ID="D_DrpDegree"
                                                                                runat="server"
                                                                                AutoCallBack="false"
                                                                                CssClass="ChosenSelector"
                                                                                AutoUpdateAfterCallBack="true"
                                                                                TabIndex="21">
                                                                            </Anthem:DropDownList>
                                                                        </div>
                                                                        <div class="col-md-5">
                                                                            <Anthem:Button ID="btndegreesearch"
                                                                                runat="server"
                                                                                OnClick="btndegreesearch_Click"
                                                                                Text="SEARCH"
                                                                                AutoUpdateAfterCallBack="true"
                                                                                CssClass="btn"
                                                                                Style="background: #fe9700; color: white; width: 100px;"
                                                                                data-target="#member"
                                                                                data-toggle="modal"></Anthem:Button>
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

                                    <div class="card" onmouseover="openCard('collapseFive')">
                                        <div class="card-header" id="headingFive">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="true" aria-controls="collapseFive">
                                                <span>Division/Department</span>
                                            </h3>
                                        </div>
                                        <div id="collapseFive" class="collapse" aria-labelledby="headingFive" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="search-box">
                                                            <div class="box-search">
                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                        <Anthem:DropDownList ID="Drp_Dept"
                                                                            runat="server"
                                                                            AutoCallBack="false"
                                                                            CssClass="ChosenSelector"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            TabIndex="21">
                                                                        </Anthem:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <Anthem:Button ID="Btndeptsearch"
                                                                            runat="server"
                                                                            OnClick="Btndeptsearch_Click"
                                                                            Text="SEARCH"
                                                                            AutoUpdateAfterCallBack="true"
                                                                            CssClass="btn"
                                                                            Style="background: #fe9700; color: white; width: 100px;"
                                                                            data-target="#member"
                                                                            data-toggle="modal"></Anthem:Button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseSix')">
                                        <div class="card-header" id="headingSix">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseSix" aria-expanded="true" aria-controls="collapseSix">
                                                <span>Current Location </span>
                                            </h3>
                                        </div>
                                        <div id="collapseSix" class="collapse" aria-labelledby="headingSix" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="search-box">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <Anthem:DropDownList ID="Drp_DAddress"
                                                                    runat="server"
                                                                    AutoCallBack="false"
                                                                    CssClass="ChosenSelector"
                                                                    AutoUpdateAfterCallBack="true"
                                                                    TabIndex="21">
                                                                </Anthem:DropDownList>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <Anthem:Button ID="btnaddress"
                                                                    runat="server"
                                                                    OnClick="btnaddress_Click"
                                                                    Text="SEARCH"
                                                                    AutoUpdateAfterCallBack="true"
                                                                    CssClass="btn"
                                                                    Style="background: #fe9700; color: white; width: 100px;"
                                                                    data-target="#member"
                                                                    data-toggle="modal"></Anthem:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseEight')">
                                        <div class="card-header" id="headingEight">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseEight" aria-expanded="true" aria-controls="collapseEight">
                                                <span>Company </span>
                                            </h3>
                                        </div>
                                        <div id="collapseEight" class="collapse" aria-labelledby="headingEight" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="search-box">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <Anthem:DropDownList ID="Drp_Comp"
                                                                    runat="server"
                                                                    AutoCallBack="false"
                                                                    CssClass="ChosenSelector"
                                                                    AutoUpdateAfterCallBack="true"
                                                                    Style="width: 100% !important;"
                                                                    TabIndex="22">
                                                                </Anthem:DropDownList>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <Anthem:Button ID="btncomp"
                                                                    runat="server"
                                                                    OnClick="btncomp_Click"
                                                                    Text="SEARCH"
                                                                    AutoUpdateAfterCallBack="true"
                                                                    CssClass="btn"
                                                                    Style="background: #fe9700; color: white; width: 100px;"
                                                                    data-target="#member"
                                                                    data-toggle="modal"></Anthem:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseNine')">
                                        <div class="card-header" id="headingNine">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseNine" aria-expanded="true" aria-controls="collapseNine">
                                                <span>Designation </span>
                                            </h3>
                                        </div>
                                        <div id="collapseNine" class="collapse" aria-labelledby="headingNine" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="search-box">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <Anthem:DropDownList ID="Drp_Desig"
                                                                    runat="server"
                                                                    AutoCallBack="false"
                                                                    CssClass="ChosenSelector"
                                                                    AutoUpdateAfterCallBack="true"
                                                                    TabIndex="21">
                                                                </Anthem:DropDownList>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <Anthem:Button runat="server" ID="btndesig"
                                                                    OnClick="btndesig_Click"
                                                                    Text="SEARCH"
                                                                    AutoUpdateAfterCallBack="true"
                                                                    CssClass="btn"
                                                                    Style="background: #fe9700; color: white; width: 100px;"
                                                                    data-target="#member"
                                                                    data-toggle="modal"></Anthem:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card" onmouseover="openCard('collapseEleven')">
                                        <div class="card-header" id="headingEleven">
                                            <h3 class="acdn-title collapsed" data-toggle="collapse" data-target="#collapseEleven" aria-expanded="true" aria-controls="collapseEleven">
                                                <span>Skills </span>
                                            </h3>
                                        </div>
                                        <div id="collapseEleven" class="collapse" aria-labelledby="headingEleven" data-parent="#accordion">
                                            <div class="card-body">
                                                <div class="search-box">
                                                    <div class="box-search">
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <Anthem:DropDownList ID="Drp_Skills"
                                                                    runat="server"
                                                                    AutoCallBack="false"
                                                                    CssClass="ChosenSelector"
                                                                    AutoUpdateAfterCallBack="true"
                                                                    TabIndex="22">
                                                                </Anthem:DropDownList>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <Anthem:Button ID="Btnskill"
                                                                    runat="server"
                                                                    OnClick="Btnskill_Click"
                                                                    EnableCallBack="false"
                                                                    Text="SEARCH"
                                                                    AutoUpdateAfterCallBack="true"
                                                                    CssClass="btn"
                                                                    Style="background: #fe9700; color: white; width: 100px;"
                                                                    data-target="#member"
                                                                    data-toggle="modal"></Anthem:Button>
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

    <div id="ViewDiv">
        <div class="modal fade" id="member" tabindex="-1" role="dialog" aria-labelledby="exampleModalScrollableTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-scrollable modal-lg dialmodel" role="document">
                <div class="modal-content" id="popup">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalScrollableTitle">Choose any one of the following to Signup/Login</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <!---<div class="row text-center">
						<div class="col-md-4 ">
							<a class="readon2 banner-style fb" href="#">Connect With Facebook <i class="fa fa-facebook color-white"></i></a>
						</div>
						<div class="col-md-4">
							<a class="readon2 banner-style gogle" href="#">Connect With Google <i class="fa fa-google color-white"></i></a>
						</div>
						<div class="col-md-4">
							<a class="readon2 banner-style linkdin" href="#">Connect With Linkedin <i class="fa fa-linkedin color-white"></i></a>
						</div>
                    </div>
					<div class="or-section mb-4" align="center">
						<span style="padding: 10px; border-radius: 50%;" class="md-color-grey">OR</span>
					</div>--->
                        <div class="contact-page-section">
                            <div class="container">
                                <div class="mb-4 mt-3">
                                    <img class="img-responsive" src="alumin-default-theme/images/login-popup.jpg" />
                                </div>
                                <div class="contact-comment-section">
                                    <form id="contact-form" method="post" action="#">
                                        <fieldset>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group mt-1">
                                                        <Anthem:TextBox ID="R_txtLogin" runat="server" class="form-control" AutoUpdateAfterCallBack="true" placeholder="Username"></Anthem:TextBox>
                                                        <%--<input name="Username" id="Username" class="form-control" type="text" placeholder="Username">--%>
                                                        <i class="fa fa-user-circle-o lognicon" aria-hidden="true"></i>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group mt-1">
                                                        <Anthem:TextBox ID="R_txtPass" TextMode="Password" runat="server" class="form-control" AutoUpdateAfterCallBack="true" placeholder="Password"></Anthem:TextBox>
                                                        <%--<input name="password" id="password" class="form-control" type="text" placeholder="Password">--%>
                                                        <i class="fa fa-key lognicon" aria-hidden="true"></i>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group" style="width: 100%; text-align: center; cursor: pointer;">
                                                        <Anthem:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Width="100%"
                                                            AutoUpdateAfterCallBack="true" OnClientClick='return ShowRoomDetails();' TextDuringCallBack="Wait..." CssClass="readon2 banner-style" />
                                                        <Anthem:Label ID="lblError" runat="server" CssClass="lblmessage" Font-Bold="True"></Anthem:Label>
                                                        <%--			<Anthem:Button class="readon2 banner-style" runat="server" style="width:100%; text-align:center; cursor:pointer;">Login</Anthem:Button>--%>

                                                        <Anthem:LinkButton runat="server" ID="lnkNeRegs" OnClick="lnkNeRegs_Click" ForeColor="Blue" AutoUpdateAfterCallBack="true" Style="padding-left: 0px;">
                                                           <i style="color: blue"> Click here for New Resgistration.</i>
                                                        </Anthem:LinkButton><br />

                                                    </div>
                                                </div>
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
    </div>
    <!-- End -->

    <script type="text/javascript">

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        function Confirm() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }

    </script>

</asp:Content>