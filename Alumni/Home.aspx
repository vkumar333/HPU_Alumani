<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    

    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/style.css" rel="stylesheet" />
    <link href="../Content/font-awesome.css" rel="stylesheet" />   
    <script src=" https://cdn-script.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <%--<script src="../Scripts_Chat/jquery-3.2.1.min.js"></script>--%>
    <script src="../Scripts_Chat/jquery.signalR-2.4.3.js"></script>
    <script src="../Scripts_Chat/date.format.js"></script>
    <script src="..\signalr/hubs"></script>
    
</head>
    <script type="text/javascript">


        var IntervalVal;
        $(function () {
 
            var chatHub = $.connection.chatHub;
            
            registerClientMethods(chatHub);
            // Start Hub
            $.connection.hub.start().done(function () {
                registerEvents(chatHub)
            });

            // Reset Message Counter on Hover
            $("#divChatWindow").mouseover(function () {
                $("#MsgCountMain").html('0');
                $("#MsgCountMain").attr("title", '0 New Messages');
            });
            // Stop Title Alert
            window.onfocus = function (event) {
                if (event.explicitOriginalTarget === window) {
                    clearInterval(IntervalVal);
                    document.title = 'SignalR Chat App';
                }
            }

        });

        // Show Title Alert
        function ShowTitleAlert(newMessageTitle, pageTitle) {
            if (document.title == pageTitle) {
                document.title = newMessageTitle;
            }
            else {
                document.title = pageTitle;
            }
        }

        function registerEvents(chatHub) {           
             var name = '<%= UserName %>';            
            if (name.length > 0) {
                chatHub.server.connect(name);
            }

        // Clear Chat
        $('#btnClearChat').click(function () {
            var msg = $("#divChatWindow").html();
            if (msg.length > 0) {
                chatHub.server.clearTimeout();
                $('#divChatWindow').html('');
            }
        });

            // Send Button Click Event
            $('#btnSendMsg').click(function () {
                var msg = $("#txtMessage").val();
                if (msg.length > 0) {
                    var userName = $('#hdUserName').val();
                    var date = GetCurrentDateTime(new Date());
                    chatHub.server.sendMessageToAll(userName, msg, date);
                    $("#txtMessage").val('');
                }
            });

            // Send Message on Enter Button
            $("#txtMessage").keypress(function (e) {
                if (e.which == 13) {
                    $('#btnSendMsg').click();
                }
            });
        }

        function registerClientMethods(chatHub) {


            // Calls when user successfully logged in
            chatHub.client.onConnected = function (id, userName, allUsers, messages, times) {

                $('#hdId').val(id);
                $('#hdUserName').val(userName);
                $('#spanUser').html(userName);

                // Add All Users
                for (i = 0; i < allUsers.length; i++) {

                    AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].UserImage, allUsers[i].LoginTime);
                }

                // Add Existing Messages
                for (i = 0; i < messages.length; i++) {
                    AddMessage(messages[i].UserName, messages[i].Message, messages[i].Time, messages[i].UserImage);

                }
            }

            // On New User Connected
            chatHub.client.onNewUserConnected = function (id, name, UserImage, loginDate) {
                AddUser(chatHub, id, name, UserImage, loginDate);
            }

            // On User Disconnected
            chatHub.client.onUserDisconnected = function (id, userName) {

                $('#Div' + id).remove();

                var ctrId = 'private_' + id;
                $('#' + ctrId).remove();


                var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');

                $(disc).hide();
                $('#divusers').prepend(disc);
                $(disc).fadeIn(200).delay(2000).fadeOut(200);

            }

            chatHub.client.messageReceived = function (userName, message, time, userimg) {

                AddMessage(userName, message, time, userimg);

                // Display Message Count and Notification
                var CurrUser1 = $('#hdUserName').val();
                if (CurrUser1 != userName) {

                    var msgcount = $('#MsgCountMain').html();
                    msgcount++;
                    $("#MsgCountMain").html(msgcount);
                    $("#MsgCountMain").attr("title", msgcount + ' New Messages');
                    var Notification = 'New Message From ' + userName;
                    IntervalVal = setInterval("ShowTitleAlert('SignalR Chat App', '" + Notification + "')", 800);

                }
            }


            chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, userimg, CurrentDateTime) {

                var ctrId = 'private_' + windowId;
                if ($('#' + ctrId).length == 0) {

                    OpenPrivateChatBox(chatHub, windowId, ctrId, fromUserName, userimg);

                }

                var CurrUser = $('#hdUserName').val();
                var Side = 'right';
                var TimeSide = 'left';

                if (CurrUser == fromUserName) {
                    Side = 'left';
                    TimeSide = 'right';

                }
                else {
                    var Notification = 'New Message From ' + fromUserName;
                    IntervalVal = setInterval("ShowTitleAlert('SignalR Chat App', '" + Notification + "')", 800);

                    var msgcount = $('#' + ctrId).find('#MsgCountP').html();
                    msgcount++;
                    $('#' + ctrId).find('#MsgCountP').html(msgcount);
                    $('#' + ctrId).find('#MsgCountP').attr("title", msgcount + ' New Messages');
                }

                var divChatP = '<div class="direct-chat-msg ' + Side + '">' +
                    '<div class="direct-chat-info clearfix">' +
                    '<span class="direct-chat-name pull-' + Side + '">' + fromUserName + '</span>' +
                    '<span class="direct-chat-timestamp pull-' + TimeSide + '"">' + CurrentDateTime + '</span>' +
                    '</div>' +

                    ' <img class="direct-chat-img" src="' + userimg + '" alt="Message User Image">' +
                    ' <div class="direct-chat-text" >' + message + '</div> </div>';

                $('#' + ctrId).find('#divMessage').append(divChatP);

                var htt = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
                $('#' + ctrId).find('#divMessage').slimScroll({
                    height: htt
                });
            }

        }

        function GetCurrentDateTime(now) {

            var localdate = dateFormat(now, "dddd, mmmm dS, yyyy, h:MM:ss TT");

            return localdate;
        }

        function AddUser(chatHub, id, name, UserImage, date) {

            var userId = $('#hdId').val();

            var code, Clist;
            if (userId == id) {

                code = $('<div class="box-comment">' +
                    '<img class="img-circle img-sm" src="' + UserImage + '" alt="User Image" />' +
                    ' <div class="comment-text">' +
                    '<span class="username">' + name + '<span class="text-muted pull-right">' + date + '</span>  </span></div></div>');


                Clist = $(
                    '<li style="background:#494949;">' +
                    '<a href="#">' +
                    '<img class="contacts-list-img" src="' + UserImage + '" alt="User Image" />' +

                    ' <div class="contacts-list-info">' +
                    ' <span class="contacts-list-name" id="' + id + '">' + name + ' <small class="contacts-list-date pull-right">' + date + '</small> </span>' +
                    ' <span class="contacts-list-msg">How have you been? I was...</span></div></a > </li >');

            }
            else {

                code = $('<div class="box-comment" id="Div' + id + '">' +
                    '<img class="img-circle img-sm" src="' + UserImage + '" alt="User Image" />' +
                    ' <div class="comment-text">' +
                    '<span class="username">' + '<a id="' + id + '" class="user" >' + name + '<a>' + '<span class="text-muted pull-right">' + date + '</span>  </span></div></div>');


                Clist = $(
                    '<li>' +
                    '<a href="#">' +
                    '<img class="contacts-list-img" src="' + UserImage + '" alt="User Image" />' +

                    ' <div class="contacts-list-info">' +
                    '<span class="contacts-list-name" id="' + id + '">' + name + ' <small class="contacts-list-date pull-right">' + date + '</small> </span>' +
                    ' <span class="contacts-list-msg">How have you been? I was...</span></div></a > </li >');


                var UserLink = $('<a id="' + id + '" class="user" >' + name + '<a>');
                $(code).click(function () {

                    var id = $(UserLink).attr('id');

                    if (userId != id) {
                        var ctrId = 'private_' + id;
                        OpenPrivateChatBox(chatHub, id, ctrId, name);

                    }

                });

                var link = $('<span class="contacts-list-name" id="' + id + '">');
                $(Clist).click(function () {

                    var id = $(link).attr('id');

                    if (userId != id) {
                        var ctrId = 'private_' + id;
                        OpenPrivateChatBox(chatHub, id, ctrId, name);

                    }

                });

            }

            $("#divusers").append(code);

            $("#ContactList").append(Clist);

        }

        function AddMessage(userName, message, time, userimg) {

            var CurrUser = $('#hdUserName').val();
            var Side = 'right';
            var TimeSide = 'left';

            if (CurrUser == userName) {
                Side = 'left';
                TimeSide = 'right';

            }

            var divChat = '<div class="direct-chat-msg ' + Side + '">' +
                '<div class="direct-chat-info clearfix">' +
                '<span class="direct-chat-name pull-' + Side + '">' + userName + '</span>' +
                '<span class="direct-chat-timestamp pull-' + TimeSide + '"">' + time + '</span>' +
                '</div>' +

                ' <img class="direct-chat-img" src="' + userimg + '" alt="Message User Image">' +
                ' <div class="direct-chat-text" >' + message + '</div> </div>';

            $('#divChatWindow').append(divChat);

            var height = $('#divChatWindow')[0].scrollHeight;
            $('#divChatWindow').scrollTop(height);

        }

        // Creation and Opening Private Chat Div
        function OpenPrivateChatBox(chatHub, userId, ctrId, userName) {
            
            var PWClass = $('#PWCount').val();

            if ($('#PWCount').val() == 'info')
                PWClass = 'info';
            //else if ($('#PWCount').val() == 'danger')
            //    PWClass = 'warning';
            //else
            //    PWClass = 'info';
            debugger;
            if ($('#' + ctrId).length) {
               // alert('Found with Length');
            }
            else
                {
                $('#PWCount').val(PWClass);
                var div1 = ' <div class="col-md-4"> <div  id="' + ctrId + '" class="box box-solid box-' + PWClass + ' direct-chat direct-chat-' + PWClass + '">' +
                    '<div class="box-header with-border">' +
                    ' <h3 class="box-title">' + userName + '</h3>' +
                    ' <div class="box-tools pull-right">' +
                    ' <span data-toggle="tooltip" id="MsgCountP" title="0 New Messages" class="badge bg-' + PWClass + '">0</span>' +
                    ' <button type="button" class="btn btn-box-tool" data-widget="collapse">' +
                    '    <i class="fa fa-minus"></i>' +
                    '  </button>' +
                    '  <button id="imgDelete" type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button></div></div>' +

                    ' <div class="box-body">' +
                    ' <div id="divMessage" class="direct-chat-messages">' +

                    ' </div>' +

                    '  </div>' +
                    '  <div class="box-footer">' +


                    '    <input type="text" id="txtPrivateMessage" name="message" placeholder="Type Message ..." class="form-control"  />' +

                    '  <div class="input-group">' +
                    '    <input type="text" name="message" placeholder="Type Message ..." class="form-control" style="visibility:hidden;" />' +
                    '   <span class="input-group-btn">' +
                    '          <input type="button" id="btnSendMessage" class="btn btn-' + PWClass + ' btn-flat" value="send" />' +
                    '   </span>' +
                    '  </div>' +

                    ' </div>' +
                    ' </div></div>';
                var $div = $(div1);
            }

            // Closing Private Chat Box
            $div.find('#imgDelete').click(function () {
                $('#' + ctrId).remove();
            });

            // Send Button event in Private Chat
            $div.find("#btnSendMessage").click(function () {
                debugger;
                $textBox = $div.find("#txtPrivateMessage");

                var msg = $textBox.val();
                if (msg.length > 0) {
                    chatHub.server.sendPrivateMessage(userId, msg);
                    $textBox.val('');
                }
            });

            // Text Box event on Enter Button
            $div.find("#txtPrivateMessage").keypress(function (e) {
                if (e.which == 13) {
                    $div.find("#btnSendMessage").click();
                }
            });

            // Clear Message Count on Mouse over           
            $div.find("#divMessage").mouseover(function () {

                $("#MsgCountP").html('0');
                $("#MsgCountP").attr("title", '0 New Messages');
            });
            // Append private chat div inside the main div
            $('#PriChatDiv').append($div);
        }
    </script>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="content-wrapper">
            <div class="row">
                <div class="col-md-8">             
                    <!--/.direct-chat -->
                </div>
                <!-- /.col -->
                <div class="col-md-4">
                    <div class="box box-solid box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Online Users <span id='UserCount'></span></h3>
                        </div>
                        <div class="box-footer box-comments" id="divusers">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row" id="PriChatDiv">
                        </div>
                        <textarea class="form-control" style="visibility: hidden;"></textarea>
                        <!--/.private-chat -->
                    </div>
                </div>
                <!-- /.col -->
                <!-- /.col -->
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>

        <span id="time"></span>
        <input id="hdId" type="hidden" />
        <input id="PWCount" type="hidden" value="info" />
        <input id="hdUserName" type="hidden" />
         
        <script src="../include/bootstrap/bootstrap.js"></script>
    </form>
</body>
</html>
