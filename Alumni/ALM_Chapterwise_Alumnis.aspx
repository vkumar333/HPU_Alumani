<%@ Page Language="C#" MasterPageFile="AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Chapterwise_Alumnis.aspx.cs" Inherits="Alumni_ALM_Chapterwise_Alumnis" Title="Alumni Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="Stylesheet" />
    <script type="text/javascript">

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

        .checkbox-inline.custom-checkbox-inline span {
            width: 47%;
            display: inline-block;
            margin-bottom: 10px;
        }

            .checkbox-inline.custom-checkbox-inline span input {
                margin-right: 10px !important;
            }

        .chosen-container-single {
            width: 100% !important;
        }

            .chosen-container-single .chosen-single {
                height: 34px;
                line-height: 30px;
            }

                .chosen-container-single .chosen-single div {
                    top: 4px;
                }

        .mt-1 {
            margin-top: 10px;
        }
    </style>

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success" runat="server">
                <div class="panel-body pnl-body-custom">

                    <div id="rs-team-2" class="rs-team-2 team-all pt-50 pb-50">
                        <div class="container">
                            <div class="boxhead">
                                <%-- All Profiles --%>
                                <h4 style="text-align: left;">
                                    <Anthem:Label ID="lblProfileCnt" runat="server" AutoUpdateAfterCallBack="True">
                                    </Anthem:Label>
                                </h4>
                            </div>

                            <div class="row">
                                <div class="col-lg-8" style="overflow-y: scroll; height: 700px;">
                                    <div class="row">
                                        <Anthem:Repeater ID="RepProfile" runat="server" AutoUpdateAfterCallBack="true">
                                            <ItemTemplate>
                                                <div class="col-lg-4 col-md-6 col-xs-6">
                                                    <div onclick="location.href='<%# string.Format("/Alumni/Alm_Alumni_Show_Alumni_Profile_Search.aspx?ID={0}", HttpUtility.UrlEncode(Eval("pk_alumniid").ToString())) %>';" style="cursor: pointer;">
                                                        <div class="team-item">
                                                            <div class="team-img" style="height: 200px; width: 223px">
                                                                <a href="#">
                                                                    <img id="Imge" src='<%# Eval("PicUrl") %>' alt="">
                                                                </a>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;<div class="social-icon">
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
                                        </Anthem:Repeater>
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
                                                                    <asp:Button runat="server" ID="Button1" OnClick="Button1_Click" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn"></asp:Button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
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
                                                                                            Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                            Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                            Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                            Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                                Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                            Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                    Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                    Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                <Anthem:Button runat="server" ID="btndesig" OnClick="btndesig_Click" Text="SEARCH" AutoUpdateAfterCallBack="true" CssClass="btn" Style="background: #fe9700; color: white;"></Anthem:Button>
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
                                                                                    Style="background: #fe9700; color: white;"></Anthem:Button>
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

                </div>
            </div>
        </div>
    </div>

    <%--<script src="alumin-default-theme/js/jquery.min.js"></script>--%>
    <script src="alumin-default-theme/js/bootstrap.min.js"></script>
    <script src="alumin-default-theme/js/owl.carousel.min.js"></script>
    <script src="alumin-default-theme/js/slick.min.js"></script>
    <script src="alumin-default-theme/js/wow.min.js"></script>
    <script src="alumin-default-theme/js/rsmenu-main.js"></script>
    <script src="alumin-default-theme/js/jquery.magnific-popup.min.js"></script>
    <script src="alumin-default-theme/js/main.js"></script>

    <script type="text/javascript">

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        function Confirm() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }

    </script>

</asp:Content>