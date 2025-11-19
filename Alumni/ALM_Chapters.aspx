<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Chapters.aspx.cs" Inherits="Alumni_ALM_Chapters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ShowRoomDetails(obj) {
            document.getElementById('ViewDiv').style.display = 'block';
        }
    </script>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Chapters </h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <div class="rs-events-list sec-spacer">
        <div class="container chapter-container">
            <div class="row">
                <asp:Repeater runat="server" ID="rpChapters">
                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="card chapter-card">
                                <div class="card-title chapter-title">
                                    <a href="#">
                                        <Anthem:LinkButton ID="lnkChapter" CssClass="name" runat="server" OnClick="lnkChapter_Click" href="#" data-target="#member" Text='<%# Eval("chapterName") %>' CommandArgument='<%# Eval("encId") %>' data-toggle="modal" Style="color: white;">
                                        </Anthem:LinkButton>
                                    </a>
                                </div>
                                <div class="underline chapter-underline"></div>
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
                        <h5 class="modal-title" id="exampleModalScrollableTitle">Choose any one of the following to Signup/Login </h5>
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
                                                        <Anthem:LinkButton runat="server" ID="lnkNewRegs" OnClick="lnkNewRegs_Click" ForeColor="Blue" AutoUpdateAfterCallBack="true" Style="padding-left: 0px;">
                                                           <i style="color: blue"> Click here for New Resgistration. </i>
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
                </div>
            </div>
        </div>
    </div>

</asp:Content>