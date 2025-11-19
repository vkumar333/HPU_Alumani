<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Mentors_ViewAll.aspx.cs" Inherits="Alumni_ALM_Mentors_ViewAll" %>

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
            document.getElementById('exampleModalScrollableMentor').style.display = 'block';
        }

        function hidePopUp() {
            debugger;
            document.getElementById('exampleModalScrollableMentor').style.display = 'none';
        }

        function showMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMentor').style.display = 'block';
        }

        function hideMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMentor').style.display = 'none';
        }

        function showMenteeMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMentee').style.display = 'block';
        }

        function hideMenteeMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMentee').style.display = 'none';
        }

        function showMentorMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMentorMsgss').style.display = 'block';
        }

        function hideMentorMsgPopUp() {
            debugger;
            document.getElementById('modalPopUpMentorMsgss').style.display = 'none';
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

            <%--// Check for disallowed special characters (example: only allow letters, numbers, basic punctuation)
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
                        <Anthem:Repeater ID="RepMentorProfile" runat="server" AutoUpdateAfterCallBack="true">
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
                                            <p class="help-heading">Offering Help In</p>
                                            <p class="help-text"><%# Eval("topicTextWithCommaSeparated") %></p>
                                            <Anthem:Button ID="btnViewProfile" runat="server" AutoUpdateAfterCallBack="true" Text="View Profile" CommandName="MENTOR" TextDuringCallBack="SUBMITING..." OnClick="btnViewProfile_Click" CommandArgument='<%# Eval("pk_mdtlid") %>' data-toggle="modal" data-target="#exampleModalScrollableMentor" />

                                            <span style="position: relative">
                                                <Anthem:Button ID="btnMsgNow" runat="server" AutoUpdateAfterCallBack="true" Text="Message Now" CommandName="MSGNOW" TextDuringCallBack="SUBMITING..." OnClick="btnMsgNow_Click" CommandArgument='<%# Eval("receiverID") %>' Visible='<%# (bool)Eval("isBtnEnabledForMessageToEachOther") && Eval("pk_alumniid").ToString() != Session["AlumniID"].ToString() %>' data-toggle="modal" data-target="#modalPopUpMentorMsgss" Enabled='<%# Eval("pk_alumniid").ToString() != Session["AlumniID"].ToString() %>' />

                                                <%-- <div class="notification-mentor" visible='<%# (bool)Eval("isBtnEnabledForMessageToEachOther") && Eval("pk_alumniid").ToString() != Session["AlumniID"].ToString() %>' runat="server">
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
                    <%--<div class="" oncontextmenu="return false;">
                        <Anthem:Panel ID="msgPnl" runat="server" AutoUpdateAfterCallBack="true" Visible="false">

                            <Anthem:HiddenField ID="hdnSelectedAlumniID" runat="server" AutoUpdateAfterCallBack="true" />
                            <Anthem:HiddenField ID="hdnpk_MReqID" runat="server" AutoUpdateAfterCallBack="true" />

                            <asp:Literal ID="litMessages" runat="server" Mode="PassThrough"></asp:Literal>

                            <Anthem:TextBox ID="txtMessagess" runat="server" placeholder="Type your message..." AutoUpdateAfterCallBack="true" TextMode="MultiLine" CssClass="form-control" />
                            <Anthem:Button ID="btnSendMsg" runat="server" AutoUpdateAfterCallBack="true" Text="SEND" TextDuringCallBack="SENDING..." EnableCallBack="false" OnClick="btnSendMsg_Click" CommandName="SEND" CssClass="btn btn-warning" />
                            <asp:Label ID="lblStatus" runat="server" ForeColor="Green" />
                            <Anthem:Label ID="lblMsgss" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="true" />

                        </Anthem:Panel>
                    </div>--%>
					<br />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="exampleModalScrollableMentor">
        <div class="modal-dialog modal-dialog-scrollable modal-sm">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h1 class="modal-title">&nbsp;</h1>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="alumniCard" runat="server">
                        <div class="avatar avatar text-center">
                            <Anthem:Image ID="imgProfile" ImageUrl='<%# Eval("fileUrlPic") %>' class="mentor-img" alt="Alumni Image" runat="server" AutoUpdateAfterCallBack="true" />
                        </div>
                        <div class="details text-center">
                            <strong>
                                <Anthem:Label ID="lblAlumniName" Text='<%# Eval("alumni_name") %>' runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                <Anthem:HiddenField ID="hdnMAlumniID" Value='<%# Eval("pk_alumniid") %>' runat="server" AutoUpdateAfterCallBack="true" />
                                <Anthem:HiddenField ID="hdnMReqID" Value='<%# Eval("pk_mdtlid") %>' runat="server" AutoUpdateAfterCallBack="true" />
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
                        <%-- <div class="p-20 mt-15" style="border: solid 1px #d7d7d7; border-radius: 10px;">--%>
                        <div class="ovrflw1" oncontextmenu="return false;">
                            <div class="message-box scroll-flw1" oncontextmenu="return false;">
                                <p>
                                    Offering Help In:
                                    <Anthem:Label ID="lblOfferHelpIn" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                </p>
                                <hr />
                                <p>
                                    Offering Help To:
                                    <Anthem:Label ID="lblOfferingHelpTo" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                </p>
                                <hr />
                                <p>
                                    Domains:
                                    <Anthem:Label ID="lblDomains" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                </p>
                                <hr />
                                <p>
                                    Skills:
                                    <Anthem:Label ID="lblSkills" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                </p>
                                <hr />
                                <p>
                                    Message:
                                    <Anthem:Label ID="lblMentorMsg" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                </p>
                                <hr />
                                <p>
                                    Request Status:
                                    <Anthem:Label ID="lblRequestStatus" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                </p>
                            </div>
                        </div>
                        <%--</div>--%>
                    </div>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <Anthem:Button ID="btnGoToMentorProfile" runat="server" AutoUpdateAfterCallBack="true" Text="Go to Profile" TextDuringCallBack="Viewing..." CommandName="MENTORPROFILE" OnClick="btnGoToMentorProfile_Click" data-toggle="modal" data-target="#exampleModalScrollableMentor" CssClass="btn btn-default mb-10" />
                    <Anthem:Button ID="btnSendMentorMsg" runat="server" AutoUpdateAfterCallBack="true" Text="Send Request to Mentor" CommandName="MENTOR" TextDuringCallBack="Viewing..." OnClick="btnSendMentorMsg_Click" data-toggle="modal" data-target="#modalPopUpMentor" CssClass="btn btn-warning" />
                    <Anthem:Button ID="btnSendMenteeMSG" runat="server" AutoUpdateAfterCallBack="true" Text="Send Request to Mentor" TextDuringCallBack="Viewing..." CommandName="MENTORPROFILE" OnClick="btnSendMenteeMSG_Click" data-toggle="modal" data-target="#exampleModalMenteeMSG" CssClass="btn btn-warning" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalPopUpMentor">
        <div class="modal-dialog modal-dialog-scrollable modal-sm">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <h5 class="modal-title" id="modalheaderTitle">Send Mentorship Request </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div1" runat="server">
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-12 control-label">Once your request is accepted, you will receive a message from them. </label>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-12 control-label">My goal for this session is <span style="color: red;">*</span> </label>
                                <div class="col-sm-12">
                                    <Anthem:DropDownList ID="ddl_goal" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></Anthem:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-12 control-label" style="color: #0054af;">Add a personalised message </label>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-12 control-label">Mentioning the agenda clearly will increase your chances of getting accepted. </label>
                            <div class="col-sm-12 mb-20">
                                <Anthem:CheckBox ID="chk_personal" runat="server" Text="Yes" CssClass="form-control" OnCheckedChanged="chk_personal_CheckedChanged" AutoCallBack="true" Style="color: black;"></Anthem:CheckBox>
                            </div>
                            <div class="col-sm-12">
                                <Anthem:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Visible="false" placeholder="Message" AutoUpdateAfterCallBack="true" />
                                <br />
                            </div>
                        </div>
                        <%--<div class="form-group form-group-sm">
                            <label class="col-sm-12 control-label">Message for : </label>
                            <div class="col-sm-12">
                                <span class="spanLoginReg">
                                    <Anthem:RadioButtonList ID="rdblist" runat="server" RepeatDirection="Horizontal" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="rdblist_SelectedIndexChanged" Height="22px">
                                    </Anthem:RadioButtonList>
                                </span>
                            </div>
                        </div>
                        <Anthem:Panel ID="pnlMentor" runat="server" Style="display: none;" AutoUpdateAfterCallBack="true">
                            <div class="form-group form-group-sm">
                                <label class="col-sm-12 labelname"></label>
                                <div class="col-sm-12">
                                    <div id="DivMentor" class="DivStyleWithScroll" style="width: 100%; height: 120px;">
                                        <Anthem:CheckBoxList ID="chkboxListsMentors" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true" />
                                    </div>
                                </div>
                            </div>
                        </Anthem:Panel>
                        <Anthem:Panel ID="pnlMentee" runat="server" Style="display: none;" AutoUpdateAfterCallBack="true">
                            <div class="form-group form-group-sm">
                                <label class="col-sm-12 labelname"></label>
                                <div class="col-sm-12">
                                    <div id="DivMentee" class="DivStyleWithScroll" style="width: 100%; height: 120px;">
                                        <Anthem:CheckBoxList ID="chkboxListsMentees" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" />
                                    </div>
                                </div>
                            </div>
                        </Anthem:Panel>--%>
                    </div>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <Anthem:Button ID="btnCancel" runat="server" Text="CANCEL" TextDuringCallBack="CANCELING.." AutoUpdateAfterCallBack="True" OnClick="btnCancel_Click" EnableCallBack="false" CommandName="CANCEL" CssClass="btn btn-default" />
                    <Anthem:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="SUBMIT" AutoUpdateAfterCallBack="true" TextDuringCallBack="SUBMITING..." EnableCallBack="false" CommandName="SUBMIT" CssClass="btn btn-warning" />
                    <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="true" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="exampleModalMenteeMSG">
        <div class="modal-dialog modal-dialog-scrollable modal-sm">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <h5 class="modal-title" id="modalheaderTitleMentee">Send Mentee Request for Mentor </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div2" runat="server">
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-12 control-label">Once your request is accepted, you will receive a message from them. </label>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-12 control-label">Seeking Help For : <span style="color: red;">*</span> </label>
                                <div class="col-sm-12">
                                    <Anthem:DropDownList ID="ddlHelpIn" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="true"></Anthem:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-12 control-label" style="color: #0054af;">Add a personalised message </label>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-12 control-label">Mentioning the agenda clearly will increase your chances of getting accepted. </label>
                            <div class="col-sm-12 mb-20">
                                <Anthem:CheckBox ID="chkIsPersonal" runat="server" Text="Yes" CssClass="form-control" OnCheckedChanged="chkIsPersonal_CheckedChanged" AutoCallBack="true" Style="color: black;"></Anthem:CheckBox>
                            </div>
                            <div class="col-sm-12">
                                <Anthem:TextBox ID="txtMRMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Visible="false" placeholder="Message" AutoUpdateAfterCallBack="true" />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <Anthem:Button ID="btnMRCancel" runat="server" Text="CANCEL" TextDuringCallBack="CANCELING.." AutoUpdateAfterCallBack="True" OnClick="btnMRCancel_Click" EnableCallBack="false" CommandName="CANCEL" CssClass="btn btn-default" />
                    <Anthem:Button ID="btnMRSubmit" runat="server" OnClick="btnMRSubmit_Click" Text="SUBMIT" AutoUpdateAfterCallBack="true" TextDuringCallBack="SUBMITING..." EnableCallBack="false" CommandName="SUBMIT" CssClass="btn btn-warning" />
                    <Anthem:Label ID="lblMRMsg" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="true" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="modalPopUpMentorMsgss" oncontextmenu="return false;">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header" style="background: #1b5c56; justify-content: left;">
                    <h5 class="modal-title" id="modalheaderTitleSMW">Send Messages Now </h5>
                    <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideMentorMsgPopUp();">
                        <span aria-hidden="true">&times;</span>
                    </button>--%>
                    <Anthem:Button ID="btnCloseModal" Style="background-color: transparent; border: none;" runat="server" Text="Close" OnClick="btnCloseModal_Click" CssClass="close-1" aria-label="Close" AutoUpdateAfterCallBack="true" data-dismiss="modal"></Anthem:Button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card" id="Div3" runat="server">
                        <!-- Message History -->
                        <Anthem:Repeater ID="rptMentorRequests" runat="server" AutoUpdateAfterCallBack="true" OnItemCommand="rptMentorRequests_ItemCommand">
                            <ItemTemplate>
                                <div class="message-box" onclick="setSelectedIDs('<%# Eval("senderID") %>', '<%# Eval("pk_MReqID") %>')">
                                    <div class="message-header">Mentorship For : </div>
                                    <div class="message-body"><%# Eval("goalDescription") %> </div>
                                    <div class="message-time">🕒 <%# Eval("sentAt") %> </div>
                                    <div class="message-header"><%# Eval("status") %> </div>

                                    <div class="message-actions">
                                        <Anthem:Button ID="lnkAccept" runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Accepting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn warning"></Anthem:Button>

                                        &nbsp;|&nbsp;
                                        
                                        <Anthem:Button ID="lnkReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("pk_MReqID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Rejecting.." Enabled='<%# Eval("isVisible") %>'
                                            CssClass="btn btn-warning"></Anthem:Button>

                                        &nbsp;|&nbsp;

                                        <span style="position: relative">
                                            <Anthem:Button ID="btnSendMessageNow" runat="server" Text="Message Now" CommandName="SENDMSGNOW" CommandArgument='<%# Eval("senderID") + "|" + Eval("pk_MReqID") + "|" + Eval("receiverID") %>' EnableCallBack="true" AutoUpdateAfterCallBack="true" TextDuringCallBack="Sending.." Enabled='<%# Eval("isSendMsgNow") %>' CssClass="btn warning"></Anthem:Button>

                                            <%--<div class="notification-mentor">
                                                25
                                            </div>--%>
                                        </span>
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
                            <Anthem:HiddenField ID="hdnpk_MReqID" runat="server" AutoUpdateAfterCallBack="true" />

                            <asp:Literal ID="litMessages" runat="server" Mode="PassThrough"></asp:Literal>

                            <Anthem:TextBox ID="txtMessagess" runat="server" placeholder="Type your message..." AutoUpdateAfterCallBack="true" TextMode="MultiLine" CssClass="form-control" MaxLength="1000"  />

                            <%--onkeypress="return CheckMaxLength(this, 1000);" onkeyup="return CheckMaxLength(this, 1000);" onpaste="return CheckMaxLength(this, 1000);" ondrop="return CheckMaxLength(this, 1000);" oninput="sanitizeInput(this)"--%>

                            <Anthem:Button ID="btnSendMsgss" runat="server" AutoUpdateAfterCallBack="true" Text="SEND" TextDuringCallBack="SENDING..." EnableCallBack="false" OnClick="btnSendMsgss_Click" CommandName="SEND" CssClass="btn btn-warning mt-10" OnClientClick="return validateMessageBeforeSend();" />

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