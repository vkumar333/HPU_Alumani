<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Mentees_ViewAll.aspx.cs" Inherits="Alumni_ALM_Mentees_ViewAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        body {
            padding-right: 0 !important;
        }
        /*.modal-dialog-scrollable .modal-body {
            overflow-y: auto;
        }*/

        .modal-body {
            position: relative;
            -ms-flex: 1 1 auto;
            flex: 1 1 auto;
            padding: 1rem;
        }

        .modal-dialog-scrollable {
            max-height: calc(100% - 3.5rem);
        }

        .modal-dialog-scrollable {
            display: -ms-flexbox;
            display: flex;
            max-height: calc(100% - 1rem);
        }

            .modal-dialog-scrollable .modal-content {
                max-height: calc(100vh - 3.5rem);
                overflow: hidden;
            }

        .modal.show .modal-dialog {
            -webkit-transform: none;
            transform: none;
        }

        .modal-open .modal {
            overflow-x: hidden;
            overflow-y: auto;
        }

        .modal-content {
            position: relative;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 100%;
            pointer-events: auto;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid rgba(0, 0, 0, .2);
            border-radius: .3rem;
            outline: 0;
        }

        .modal-header .close {
            padding: 1rem 1rem;
            margin: -1rem -1rem -1rem auto;
        }

        .modal-header {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-align: start;
            align-items: flex-start;
            -ms-flex-pack: justify;
            justify-content: space-between;
            padding: 1rem 1rem;
            border-bottom: 1px solid #dee2e6;
            border-top-left-radius: calc(.3rem - 1px);
            border-top-right-radius: calc(.3rem - 1px);
        }

        .modal-dialog-scrollable .modal-footer, .modal-dialog-scrollable .modal-header {
            -ms-flex-negative: 0;
            flex-shrink: 0;
        }

        .modal-header {
            background: #ffffff;
        }

        button.close {
            color: #9f9f9f;
            opacity: 1;
            font-size: 38px;
            font-weight: 100;
            position: absolute;
            z-index: 9;
            right: 5px;
            top: -2px;
        }

        .modal-backdrop {
            background-color: #0000005e;
        }

        .highlight-card {
            border: 2px solid #007bff;
            background-color: #f0f8ff;
        }

        .self-tag {
            position: absolute;
            top: 10px;
            right: 10px;
            background: #007bff;
            color: white;
            padding: 5px 10px;
            border-radius: 5px;
            font-size: 0.8em;
        }
    </style>
    <style>
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
    <style>
        .chat-container {
            display: flex;
            flex-direction: column;
        }

        .message-box {
            max-width: 100%;
            padding: 10px;
            margin: 5px;
            border-radius: 10px;
            position: relative;
            word-wrap: break-word;
        }

        .message-sender {
            background-color: #dcf8c6;
            align-self: flex-start;
            text-align: left;
        }

        .message-receiver {
            background-color: #ffffff;
            align-self: flex-end;
            text-align: right;
        }

        .message-time {
            font-size: 0.75em;
            color: #888;
            margin-top: 4px;
        }

        .ovrflw .scroll-flw {
            height: 180px;
            overflow: auto;
        }

            .ovrflw .scroll-flw::-webkit-scrollbar {
                width: 5px;
                height: 8px;
                background-color: #edf3f5;
            }

            .ovrflw .scroll-flw::-webkit-scrollbar-thumb {
                background: #666;
            }

        .ovrflw1 .scroll-flw1 {
            height: 180px;
            overflow: auto;
        }

            .ovrflw1 .scroll-flw1::-webkit-scrollbar {
                width: 5px;
                height: 8px;
                background-color: #edf3f5;
            }

            .ovrflw1 .scroll-flw1::-webkit-scrollbar-thumb {
                background: #666;
            }

        .notification-mentor {
            background: #f50000;
            color: #fff;
            padding: 5px;
            display: inline-block;
            border-radius: 20px;
            font-size: 10px;
            height: 19px;
            line-height: 8px;
            position: absolute;
            top: -20px;
            right: -13px;
        }
    </style>

    <script type="text/javascript">

        function showPopUp(obj) {
            debugger;
            document.getElementById('exampleModalScrollableMentee').style.display = 'block';
        }

        function hidePopUp() {
            debugger;
            document.getElementById('exampleModalScrollableMentee').style.display = 'none';
        }

        function showMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMentee').style.display = 'block';
        }

        function hideMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMentee').style.display = 'none';
        }

        function showMenteeMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMenteeMsgss').style.display = 'block';
        }

        function hideMenteeMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMenteeMsgss').style.display = 'none';
        }

        function updateNotification(count) {
            const badge = document.getElementById("messageBadge");
            badge.textContent = count;

            if (count > 0) {
                badge.classList.add("active");
            } else {
                badge.classList.remove("active");
            }
        }

    </script>

    <%--<script type="text/javascript">

        function sanitizeInput(field) {
            // Allow only letters, numbers, spaces, and basic punctuation
            const regex = /[^a-zA-Z0-9 .,?!'"()\-]/g;
            field.value = field.value.replace(regex, '');
        }

    </script>--%>
    <script type="text/javascript">
        function validateMessageBeforeSend() {
            var messageBox = document.getElementById('<%= txtMessagess.ClientID %>');
            var message = messageBox.value.trim();

            // Check if message is empty
            if (message === "") {
                alert("Message cannot be empty.");
                return false;
            }

            // Check length
            if (message.length > 1000) {
                alert("Message exceeds the maximum length of 1000 characters.");
                return false;
            }

           <%-- // Check for disallowed special characters (example: only allow letters, numbers, basic punctuation)
            var regex = /^[a-zA-Z0-9\s.,!?'"@#$/()%&*-_+=:;\\[\]{}<>|~`^]*$/;
            if (!regex.test(message)) {
                alert("Message contains invalid characters.");
                return false;
            }--%>

            // Passed all checks
            return true;
        }
    </script>

    <div class="container-custom">
        <div class="box box-success" runat="server">
            <div class="panel-body pnl-body-custom">
                <div class="mt-15">
                    <div class="boxhead">
                        <Anthem:Label ID="lblProfileCnt" runat="server" AutoUpdateAfterCallBack="True"></Anthem:Label>
                        <a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_Mentorship.aspx">Back </a>
                    </div>
                    <div class="row">
                        <Anthem:Repeater ID="RepMenteeProfile" runat="server" AutoUpdateAfterCallBack="true" OnItemDataBound="RepMenteeProfile_ItemDataBound">
                            <ItemTemplate>
                                <div class="col-sm-3">
                                    <div class="mentor-container">
                                        <div class="mentor-card <%# GetCardClass(Eval("pk_alumniid").ToString()) %>">
                                            <%-- Show 'Self' tag if current user --%>
                                            <%# 
                                              (Eval("pk_alumniid").ToString() == Session["AlumniID"].ToString()) 
                                                  ? "<div class='self-tag'>Self</div>" 
                                                  : "" 
                                            %>
                                            <img src='<%# Eval("fileUrlPic") %>' class="mentor-img" alt="Profile Pic" />
                                            <h3><%# Eval("alumni_name") %></h3>
                                            <p class="designation"><%# Eval("Designation") %></p>
                                            <p class="help-heading">Seeking Help For : </p>
                                            <p class="help-text"><%# Eval("topicTextWithCommaSeparated") %></p>
                                            <Anthem:Button ID="btnViewMenteeProfile" runat="server" AutoUpdateAfterCallBack="true" Text="View Profile" CommandName="MENTEE" TextDuringCallBack="Viewing..." OnClick="btnViewMenteeProfile_Click" CommandArgument='<%# Eval("Pk_Mentee_Reqid") %>' data-toggle="modal" data-target="#exampleModalScrollableMentee" />

                                            <span style="position: relative">
                                                <Anthem:Button ID="btnMsgNow" runat="server" AutoUpdateAfterCallBack="true" Text="Message Now" CommandName="MSGNOW" TextDuringCallBack="SUBMITING..." OnClick="btnMsgNow_Click" CommandArgument='<%# Eval("pk_alumniid") %>' Visible='<%# (bool)Eval("isBtnEnabledForMessageToEachOther") && Eval("pk_alumniid").ToString() != Session["AlumniID"].ToString() %>' data-toggle="modal" data-target="#modalPopUpMenteeMsgss" Enabled='<%# Eval("pk_alumniid").ToString() != Session["AlumniID"].ToString() %>' />

                                                <%--<div class="notification-mentor" visible='<%# (bool)Eval("isBtnEnabledForMessageToEachOther") && Eval("pk_alumniid").ToString() != Session["AlumniID"].ToString() %>' runat="server">
                                                    <%# Eval("isReceivedNewMessagesCnt").ToString()%>
                                                </div>--%>

                                                <asp:PlaceHolder
                                                    ID="phBadge"
                                                    runat="server"
                                                    Visible='<%# (bool)Eval("isBtnEnabledForMessageToEachOther") 
                                                                  && Eval("pk_alumniid").ToString() != Session["AlumniID"].ToString() 
                                                                  && Convert.ToInt32(Eval("isReceivedNewMessagesCnt")) > 0 %>'>

                                                    <div class="notification-mentor">
                                                        <%# Eval("isReceivedNewMessagesCnt").ToString() %>
                                                    </div>

                                                </asp:PlaceHolder>

                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </Anthem:Repeater>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="exampleModalScrollableMentee">
        <div class="modal-dialog modal-dialog-scrollable modal-sm">
            <div class="modal-content">
                <!-- Modal Header -->
                <button type="button" class="close" data-dismiss="modal">×</button>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="alumniCard" runat="server">
                        <div class="avatar text-center">
                            <Anthem:Image ID="imgProfile" ImageUrl='<%# Eval("fileUrlPic") %>' class="mentor-img" alt="Alumni Image" runat="server" AutoUpdateAfterCallBack="true" />
                        </div>
                        <div class="details text-center">
                            <strong>
                                <Anthem:Label ID="lblAlumniName" Text='<%# Eval("alumni_name") %>' runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                <Anthem:HiddenField ID="hdnMAlumniID" Value='<%# Eval("pk_alumniid") %>' runat="server" AutoUpdateAfterCallBack="true" />
                                <Anthem:HiddenField ID="hdnMReqID" Value='<%# Eval("Pk_Mentee_Reqid") %>' runat="server" AutoUpdateAfterCallBack="true" />
                            </strong>
                            <p class="alumni-class">
                                Class of
                                <Anthem:Label ID="lblPassingYear" AutoUpdateAfterCallBack="true" runat="server" Text='<%# Eval("yearofpassing") %>'></Anthem:Label>
                            </p>
                            <p>
                                <Anthem:Label ID="lblDegree" AutoUpdateAfterCallBack="true" runat="server" Text='<%# Eval("degreename") %>'></Anthem:Label>,
                                <Anthem:Label ID="lblDept" AutoUpdateAfterCallBack="true" runat="server" Text='<%# Eval("DeptName") %>'></Anthem:Label>
                            </p>
                        </div>
                        <%--<div class="p-20 mt-15" style="border: solid 1px #d7d7d7; border-radius: 10px;">--%>
                        <div class="ovrflw1" oncontextmenu="return false;">
                            <div class="message-box scroll-flw1" oncontextmenu="return false;">
                                <p>
                                    Seeking Help For :
                                        <br />
                                    <Anthem:Label ID="lblOfferHelpIn" AutoUpdateAfterCallBack="true" runat="server" Text='<%# Eval("topicTextWithCommaSeparated") %>'></Anthem:Label>
                                </p>
                                <hr />
                                <p>
                                    Professional Background :
                                        <br />
                                    <Anthem:Label ID="lblOfferingHelpTo" AutoUpdateAfterCallBack="true" runat="server" Text='<%# Eval("Professional_Background") %>'></Anthem:Label>
                                </p>
                                <hr />
                                <p>
                                    Seeking Help in Domain :
                                        <br />
                                    <Anthem:Label ID="lblDomains" AutoUpdateAfterCallBack="true" runat="server" Text='<%# Eval("domains") %>'></Anthem:Label>
                                </p>
                            </div>
                        </div>
                        <%--</div>--%>
                    </div>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-default">Send Message </button>
                    <button type="button" class="btn btn-warning">Go to Profile </button>--%>
                        
                    <Anthem:Button ID="btnGoToMenteeProfile" runat="server" AutoUpdateAfterCallBack="true" Text="Go to Profile" TextDuringCallBack="Viewing..." CommandName="MENTEEPROFILE" OnClick="btnGoToMenteeProfile_Click" data-toggle="modal" data-target="#exampleModalScrollableMentee" CssClass="btn btn-default mb-10" />

                    <Anthem:Button ID="btnSendMenteeMsg" runat="server" AutoUpdateAfterCallBack="true" Text="Send Message" CommandName="MENTEE" TextDuringCallBack="Viewing..." OnClick="btnSendMenteeMsg_Click" data-toggle="modal" data-target="#modalPopUpMentee" SkinID="none" CssClass="btn btn-warning" />

                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalPopUpMentee">
        <div class="modal-dialog modal-dialog-scrollable modal-sm">
            <div class="modal-content">
                <!-- Modal Header -->
                <%--<div class="modal-header">
                    <h2>Send Message </h2>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>--%>
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <h5 class="modal-title" id="modalheaderTitle">Send Message </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div1" runat="server">
                        <p>
                            <strong>To : </strong>
                            <span id="menteeNameLabel">
                                <Anthem:Label ID="lblMenteeName" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="font-weight: bold" ForeColor="Black" UpdateAfterCallBack="true" />
                            </span>
                        </p>
                        <hr />

                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-12 control-label">Message : </label>
                                <br />
                                <div class="col-sm-12">
                                    <Anthem:TextBox ID="txtMsg" runat="server" ForeColor="Black" TextMode="MultiLine" CssClass="form-control" Font-Size="Medium" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-default">Send Message </button>
                    <button type="button" class="btn btn-warning">Go to Profile </button>--%>

                    <Anthem:Button ID="btnCancel" runat="server" AutoUpdateAfterCallBack="true" Text="CANCEL" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnCancel_Click" CommandName="CANCEL" CssClass="btn btn-default" />

                    <Anthem:Button ID="btnSendMsg" runat="server" AutoUpdateAfterCallBack="true" Text="SEND" TextDuringCallBack="SENDING..." EnableCallBack="false" OnClick="btnSendMsg_Click" CommandName="SEND" CssClass="btn btn-warning" />

                    <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="true" />

                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalPopUpMenteeMsgss">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <h5 class="modal-title" id="modalheaderTitleMRMsg">Mentee Messages Now </h5>
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideMenteeMsgPopUp();">
                        <span aria-hidden="true">&times;</span>
                    </button>--%>

                    <Anthem:Button ID="btnCloseModal" Style="background-color: transparent; border: none;" runat="server" Text="Close" OnClick="btnCloseModal_Click" CssClass="close-1" aria-label="Close" AutoUpdateAfterCallBack="true" data-dismiss="modal"></Anthem:Button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div2" runat="server" oncontextmenu="return false;">
                        <!-- Message History -->
                        <Anthem:Repeater ID="rptMRM" runat="server" AutoUpdateAfterCallBack="true" OnItemCommand="rptMRM_ItemCommand" OnItemDataBound="rptMRM_ItemDataBound">
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
                                            CssClass="btn warning"></Anthem:Button>

                                        &nbsp;|&nbsp;
                                        
                                        <Anthem:Button ID="lnkMRMReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("pk_MRID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Rejecting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn btn-warning"></Anthem:Button>

                                        &nbsp;|&nbsp;

                                        <Anthem:Button ID="btnSendMessageNow" runat="server" Text="Message Now" CommandName="SENDMSGNOW" CommandArgument='<%# Eval("senderID") + "|" + Eval("pk_MRID") + "|" + Eval("receiverID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Sending.." Enabled='<%# Eval("isSendMsgNow") %>' CssClass="btn warning"></Anthem:Button>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </Anthem:Repeater>
                    </div>
                    <div class="ovrflw" oncontextmenu="return false;">
                        <Anthem:Panel ID="pnlMsg" runat="server" AutoUpdateAfterCallBack="true" Visible="false">
                            <div class="message-box scroll-flw" oncontextmenu="return false;">
                                <Anthem:Repeater ID="rptReceivedMsgs" runat="server" AutoUpdateAfterCallBack="true">
                                    <ItemTemplate>
                                        <%# (Eval("senderID").ToString() == Session["AlumniID"].ToString()) ? "<div class='message-box message-sender'>" : "<div class='message-box message-receiver'>" %>
                                        <div class="message-header"><%# Eval("senderName") %></div>
                                        <div class="message-body"><%# Eval("messageText") %></div>
                                        <div class="message-time">🕒 <%# Eval("sentAt") %></div>
                                        </div>
                                    </ItemTemplate>
                                </Anthem:Repeater>
                            </div>
                        </Anthem:Panel>
                    </div>
                    <div class="" oncontextmenu="return false;">
                        <Anthem:Panel ID="msgPnl" runat="server" AutoUpdateAfterCallBack="true" Visible="false">

                            <Anthem:HiddenField ID="hdnSelectedAlumniID" runat="server" AutoUpdateAfterCallBack="true" />
                            <Anthem:HiddenField ID="hdnpk_MRID" runat="server" AutoUpdateAfterCallBack="true" />

                            <asp:Literal ID="litMessages" runat="server" Mode="PassThrough"></asp:Literal>

                            <Anthem:TextBox ID="txtMessagess" runat="server" placeholder="Type your message..." AutoUpdateAfterCallBack="true" TextMode="MultiLine" CssClass="form-control" MaxLength="1000"  />

                            <%--onkeypress="return CheckMaxLength(this, 1000);" onkeyup="return CheckMaxLength(this, 1000);" onpaste="return CheckMaxLength(this, 1000);" ondrop="return CheckMaxLength(this, 1000);" oninput="sanitizeInput(this)"--%>

                            <Anthem:Button ID="btnSendMsgss" runat="server" AutoUpdateAfterCallBack="true" Text="SEND" TextDuringCallBack="SENDING..." EnableCallBack="false" OnClick="btnSendMsgss_Click" CommandName="SEND" CssClass="btn btn-warning mt-10" />

                            <Anthem:Button ID="btnCloseMsgPopUp" runat="server" AutoUpdateAfterCallBack="true" Text="CLOSE" TextDuringCallBack="SENDING..." EnableCallBack="false" OnClick="btnCloseMsgPopUp_Click" CommandName="Close" CssClass="btn btn-default mt-10" />

                            <asp:Label ID="lblStatus" runat="server" ForeColor="Green" />
                            <Anthem:Label ID="lblMsgss" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="true" />

                        </Anthem:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>