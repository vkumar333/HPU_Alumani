<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Mentorship.aspx.cs" Inherits="Alumni_ALM_Mentorship" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

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
    <style>
        .card {
            box-shadow: 0px 0px 10px #00000040;
            border-radius: 5px;
        }

        .readon2 {
            padding: 12px 40px !important;
        }

        h5.fw-bold {
            font-size: 18px;
            margin-bottom: 10px;
        }

        .alert-success {
            color: #3c763d !important;
            background-color: #dff0d8 !important;
            border-color: #d6e9c6 !important;
        }

        .alert-info {
            color: #31708E !important;
            background-color: #d9edf7 !important;
            border-color: #bce8f1 !important;
        }

        .message-box {
            border: 1px solid #ddd;
            border-radius: 10px;
            background-color: #f9f9f9;
            padding: 12px;
            margin-bottom: 10px;
            font-family: 'Segoe UI', sans-serif;
        }

        .message-header {
            font-weight: bold;
            color: #007B83;
        }

        .message-body {
            margin-top: 5px;
            margin-bottom: 5px;
        }

        .message-time {
            font-size: 0.9em;
            color: #888;
        }
    </style>

    <div class="container-custom">
        <div class="boxhead mt-15 mb-0">
            Flash Mentorship             
                <a class="btn btn-warning btn-sm pull-right" href="../Alumni/ALM_Alumni_Home.aspx">Back </a>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <div class="dashboard mt-15" oncontextmenu="return false;">
                    <div class="card bg-gradient-danger card-img-holder text-white m-0">
                        <div class="card-body">
                            <img src="../img/circle.png" class="card-img-absolute" alt="circle-image">
                            <i class="fa fa-users card-img-absolute" aria-hidden="true"></i>
                            <h2 class="font-weight-normal mb-3 text-white text-shadow">Total Mentors </h2>
                            <h2 class="mb-5 text-white text-shadow">
                                <Anthem:Label ID="lblCountMentors" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </h2>
                            <h6 class="card-text">
                                <a id="lnkViewAllMentors" class="text-shadow" href="ALM_Mentors_ViewAll.aspx?type=mentor" runat="server">View All</a>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="dashboard mt-15" oncontextmenu="return false;">
                    <div class="card bg-gradient-info card-img-holder text-white m-0">
                        <div class="card-body">
                            <img src="../img/circle.png" class="card-img-absolute" alt="circle-image">
                            <i class="fa fa-users card-img-absolute" aria-hidden="true"></i>
                            <h2 class="font-weight-normal mb-3 text-white text-shadow">Total Mentees </h2>
                            <h2 class="mb-5 text-white text-shadow">
                                <Anthem:Label ID="lblCountMentees" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </h2>
                            <h6 class="card-text">
                                <a id="lnkViewAllMentees" class="text-shadow" href="ALM_Mentees_ViewAll.aspx?type=mentee" runat="server">View All</a>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="dashboard mt-15" oncontextmenu="return false;">
                    <div class="card bg-gradient-danger-2 card-img-holder text-white m-0" style="min-height: 174px;">
                        <div class="card-body">
                            <img src="../img/circle.png" class="card-img-absolute" alt="circle-image">
                            <i class="fa fa-home card-img-absolute" aria-hidden="true"></i>
                            <h2 class="font-weight-normal mb-3 text-white text-shadow">Mentorship Homepage </h2>
                            <h2 class="mb-5 text-white text-shadow">
                                <Anthem:Label ID="lblCountMentorshipHomepage" runat="server" AutoUpdateAfterCallBack="true" Text=""></Anthem:Label>
                                &nbsp;
                            </h2>
                            <h6 class="card-text">
                                <a id="lnkMentorshipHomepage" class="text-shadow" href="ALM_MentorshipHomepage.aspx" runat="server">Click here </a>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="dashboard mt-15">
                    <div class="card bg-gradient-success card-img-holder text-white m-0" style="min-height: 174px;">
                        <div class="card-body">
                            <img src="../img/circle.png" class="card-img-absolute" alt="circle-image">
                            <i class="fa fa-sliders card-img-absolute" aria-hidden="true"></i>
                            <h2 class="font-weight-normal mb-3 text-white text-shadow">My Preferences </h2>
                            <h2 class="mb-5 text-white text-shadow">
                                <Anthem:Label ID="lblCountMyPreferences" runat="server" AutoUpdateAfterCallBack="true" ToolTip=""></Anthem:Label>
                                &nbsp;
                            </h2>
                            <h6 class="card-text">
                                <a id="lnkMyPreferences" class="text-shadow" href="ALM_MyPreferences.aspx" runat="server">Click here </a>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="alert alert-success mt-15" style="font-size: 12px; visibility: hidden;" id="notificationDiv" runat="server">
            <strong><i class="fa fa-bell-o"></i>Notification Alert :- </strong>
            <Anthem:LinkButton ID="lnkMenteeRequested" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkMenteeRequested_Click" data-toggle="modal" data-target="#modalPopUpMenteeRequestMsgss" class="alert-link text-dark">
                    <i class="fa-solid fa-messages"></i> Click here to view mentee request details.
            </Anthem:LinkButton>
        </div>

    </div>

    <div class="modal" id="modalPopUpMenteeRequestMsgss" oncontextmenu="return false;">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h5 class="modal-title" id="modalheaderTitleMentee">Mentee Requested Messages </h5>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div1" runat="server">
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

</asp:Content>