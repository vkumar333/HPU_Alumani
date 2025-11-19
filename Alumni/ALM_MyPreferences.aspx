<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_MyPreferences.aspx.cs" Inherits="Alumni_ALM_MyPreferences" %>

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
   
    <div class="container-custom">
        <div class="boxhead mt-15 mb-0">
            My Preferences             
                <a class="btn btn-warning btn-sm pull-right" href="../Alumni/ALM_Mentorship.aspx">Back </a>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <div class="dashboard custom-mentor-dashboard mt-15" oncontextmenu="return false;">
                    <div class="card bg-gradient-danger custom-bg-gradient-danger card-img-holder text-white m-0" style="min-height: 174px;">
                        <div class="card-body">
                            <img src="../img/circle.png" class="card-img-absolute" alt="circle-image">
                            <i class="fa fa-sliders card-img-absolute" aria-hidden="true"></i>
                            <h2 class="font-weight-normal mb-3 text-white">Edit as Mentor </h2>
                            <h2 class="mb-5 text-white">
                                <Anthem:Label ID="lblPreferencesMentor" runat="server" AutoUpdateAfterCallBack="true" Text=""></Anthem:Label>
                                &nbsp;
                            </h2>
                            <h6 class="card-text">
                                <Anthem:LinkButton ID="lnkViewAllMentors" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkViewAllMentors_Click" Text="Click here" EnableCallBack="false"></Anthem:LinkButton>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="dashboard custom-mentor-dashboard mt-15" oncontextmenu="return false;">
                    <div class="card bg-gradient-info custom-bg-gradient-info card-img-holder text-white m-0" style="min-height: 174px;">
                        <div class="card-body">
                            <img src="../img/circle.png" class="card-img-absolute" alt="circle-image">
                            <i class="fa fa-sliders card-img-absolute" aria-hidden="true"></i>
                            <h2 class="font-weight-normal mb-3 text-white">Edit as Mentee </h2>
                            <h2 class="mb-5 text-white">
                                <Anthem:Label ID="lblPreferencesMentee" runat="server" AutoUpdateAfterCallBack="true" Text=""></Anthem:Label>
                                &nbsp;
                            </h2>
                            <h6 class="card-text">
                                <Anthem:LinkButton ID="lnkViewAllMentees" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkViewAllMentees_Click" Text="Click here"></Anthem:LinkButton>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="dashboard custom-mentor-dashboard mt-15" oncontextmenu="return false;">
                    <div class="card bg-gradient-danger-2 custom-bg-gradient-danger card-img-holder text-white m-0" style="min-height: 174px;">
                        <div class="card-body">
                            <img src="../img/circle.png" class="card-img-absolute" alt="circle-image">
                            <i class="fa fa-user card-img-absolute" aria-hidden="true"></i>
                            <h2 class="font-weight-normal mb-3 text-white">Remove as Mentor </h2>
                            <h2 class="mb-5 text-white">
                                <Anthem:Label ID="lblRemoveAsMentor" runat="server" AutoUpdateAfterCallBack="true" Text=""></Anthem:Label>
                                &nbsp;
                            </h2>
                            <h6 class="card-text">
                                <Anthem:LinkButton ID="lnkRemoveMentor" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkRemoveMentor_Click" Text="Click here"></Anthem:LinkButton>
                            </h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="dashboard custom-mentor-dashboard mt-15">
                    <div class="card bg-gradient-success custom-bg-gradient-success card-img-holder text-white m-0" style="min-height: 174px;">
                        <div class="card-body">
                            <img src="../img/circle.png" class="card-img-absolute" alt="circle-image">
                            <i class="fa fa-user card-img-absolute" aria-hidden="true"></i>
                            <h2 class="font-weight-normal mb-3 text-white">Remove as Mentee </h2>
                            <h2 class="mb-5 text-white">
                                <Anthem:Label ID="lblRemoveAsMentee" runat="server" AutoUpdateAfterCallBack="true" Text=""></Anthem:Label>
                                &nbsp;
                            </h2>
                            <h6 class="card-text">
                                <Anthem:LinkButton ID="lnkRemoveMentee" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkRemoveMentee_Click" Text="Click here"></Anthem:LinkButton>
                            </h6>
                        </div>
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