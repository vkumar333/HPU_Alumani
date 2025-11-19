<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_Alumni_Batchmates_member.aspx.cs" Inherits="Alumni_Alm_Alumni_Batchmates_member" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function ShowRoomDetails(obj) {
            document.getElementById('ViewDiv').style.display = 'block';
        }
    </script>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg3 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Select Course/Degree/Division/Department</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <div id="rs-courses-3" class="rs-courses-3 sec-spacer">
        <div class="container">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-6">
                        <div class="sec-title mb-20 md-mb-30">
                            <h2 class="title mb-20">HPU Yearbook</h2>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-9">
                                <div class="yeartext textyr">All of your friends are part of HPU yearbook</div>
                                <div class="social-share socialshre mb-3">

                                    <label class="toggle toglab" for="toggle9">
                                        <input type="checkbox" id="toggle9" style="display: none;" />
                                        <div class="btnn">
                                            <i class="fa fa-share-alt"></i>
                                            <i class="fa fa-times"></i>
                                            <div class="social">
                                                <a id="facebookLink" runat="server"><i class="fa fa-facebook"></i></a>
                                                <a id="twitterLink" runat="server"><i class="fa fa-twitter"></i></a>
                                                <a id="linkedInLink" runat="server"><i class="fa fa-linkedin"></i></a>
                                                <a id="youtubeLink" runat="server"><i class="fa fa-youtube"></i></a>
                                            </div>
                                        </div>
                                    </label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div style="float: left;">
                                    <a class="readon2 banner-style" href="Alm_alumni_Batchmates.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i>Back</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <asp:Repeater ID="GropbyYear" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-6 grid-item filter1 text-center">
                            <div class="course-item mt-30">
                                <div class="course-img">
                                    <span class="course-value" style="right: 39%;"><%# Eval("yearofPassing")%></span>
                                </div>
                                <div class="course-body  c<%# Container.ItemIndex + 1 %>">
                                    <div class="course-desc">
                                        <h4 class="course-title mt-4">
                                            <Anthem:LinkButton data-toggle="modal" runat="server" href="#" data-target="#member" OnClick="Button_Click" CommandArgument='<%# Eval("Fk_subjectid") %>'><%# Eval("degres")%></Anthem:LinkButton></h4>
                                        <p>
                                            <i class="fa fa-users"></i> <%# Eval("member") %> Member
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
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
                                                        <i class="fa fa-user-circle-o lognicon" aria-hidden="true"></i>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group mt-1">
                                                        <Anthem:TextBox ID="R_txtPass" TextMode="Password" runat="server" class="form-control" AutoUpdateAfterCallBack="true" placeholder="Password"></Anthem:TextBox>
                                                        <i class="fa fa-key lognicon" aria-hidden="true"></i>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group" style="width: 100%; text-align: center; cursor: pointer;">
                                                        <Anthem:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Width="100%"
                                                            AutoUpdateAfterCallBack="true" OnClientClick='return ShowRoomDetails();' TextDuringCallBack="Wait..." CssClass="readon2 banner-style" />
                                                        <Anthem:Label ID="lblError" runat="server" CssClass="lblmessage" Font-Bold="True"></Anthem:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>