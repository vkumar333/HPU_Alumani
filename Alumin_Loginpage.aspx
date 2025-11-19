<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alumin_Loginpage.aspx.cs" Inherits="Alumni_Alumin_Loginpage" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>ALUMNI :: PORTAL</title>

    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalsable=no" name="viewport" />
    <script src="../include/bootstrap.min.js"></script>
    <link href="../include/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../include/Bootstrap/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../include/Bootstrap/css/style.css" rel="stylesheet" />
    <script src="include/sha1.js" type="text/javascript"></script>
    <script src="include/utf8.js" type="text/javascript"></script>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="include/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="include/bootstrap/font-awesome.min.css" rel="stylesheet" />
    <link href="include/bootstrap/main-page.css" rel="stylesheet" />
    <script src="include/bootstrap/bootstrap.js"></script>
    <link href="include/bootstrap/style_bootstrap.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("#R_txtPass").attr("type", type);
            });
        });

        function Check() {
            f.username.focus();
            var index = document.URL.lastIndexOf('?');
            //alert(parseInt(index));
            if (parseInt(index) > -1) {
                alert('Either you are not Authorized or Your Username/Password is invalid !!!');
                window.location.href = "Loginpage.aspx";
            }
        }

        function burstCache() {
            createCaptcha();
            if (!navigator.onLine) {
                document.body.innerHTML = 'Offline Viewing not Allowed...pls be online.';
                window.location = 'AppError.aspx';
            }

        }

        function onsubmitclick() {
            var f = document.getElementById("f");
            var username = "";

            if (document.getElementById('R_txtLogin').value == "") {
                alert("User Name can not be blank !!!")
                return false;
            }
            else if (document.getElementById('R_txtPass').value == "") {
                alert("Password can not be blank !!!")
                return false;
            }

            else if (document.getElementById('R_txtCaptcha').value == "") {
                alert("Captcha can not be blank !!!");
                return false;
            }
            var comapre = validatecap();

            if (!comapre) {
                alert("Invalid captcha");
                createCaptcha();
            }

            else {

                var hashpass = Sha1.hash(f.upassword.value);
                //var salt = "" + Math.floor(Math.random() * 1111111111118 + 10000);
                var salt = "" + Math.floor(Math.random() * 899999999999 + 100000000000);
                var salt1, salt2;
                salt1 = salt.substring(0, 6);
                salt2 = salt.substring(6, 12);
                var Saltedhash = Sha1.hash(salt + hashpass);
                var Saltedhash = salt1 + Saltedhash + salt2;
                f.saltval.value = ''; //  Math.random().toString(36).substr(2, 16);
                f.hash.value = Saltedhash; //  Sha1.hash(f.salt.value + hashpass); //Saltedhash
                f.upassword.value = "";
                f.uname.value = f.username.value;
            }
        }

        var code;
        function createCaptcha() {
            //clear the contents of captcha div first 
            document.getElementById('captcha').innerHTML = "";
            var charsArray =
            "0123456789";
            var lengthOtp = 6;
            var captcha = [];
            for (var i = 0; i < lengthOtp; i++) {
                //below code will not allow Repetition of Characters
                var index = Math.floor(Math.random() * charsArray.length + 1); //get the next character from the array
                if (captcha.indexOf(charsArray[index]) == -1)
                    captcha.push(charsArray[index]);
                else i--;
            }
            var canv = document.createElement("canvas");
            canv.id = "captcha";
            canv.width = 100;
            canv.height = 50;
            var ctx = canv.getContext("2d");
            ctx.font = "25px Georgia";
            ctx.strokeText(captcha.join(""), 0, 30);
            //storing captcha so that can validate you can save it somewhere else according to your specific requirements
            code = captcha.join("");
            document.getElementById("captcha").appendChild(canv); // adds the canvas to the body element
        }

        function validatecap() {
            //event.preventDefault();
            //debugger
            if (document.getElementById("cpatchaTextBox").value == code) {

                return true;
            } else {

                f.username.value = "";
                f.upassword.value = "";
                alert("Invalid Captcha. try Again");
                return false;
                //createCaptcha();
            }
        }

        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            if (reg.test(emailField.value) == false) {
                alert('Invalid Email Address');
                emailField.value = "";
                return false;
            }
            return true;
        }

    </script>

    <style>
        input[type=radio] {
            accent-color: red;
        }
    </style>

</head>

<body onload="burstCache();">
    <form id="form1" runat="server" defaultbutton="btnLogin">
        <div class="headermain">
            <div class="empheader">
                <div class="col-md-6">
                    <a style="float: left; margin: 7px 0 0 0;" href='#'>
                        <img class="img-responsive" border="0" src="../App_Themes/CCSBLUE/images/hpu-logo.png" />
                    </a>
                </div>
                <div class="col-md-6">
                    <ul class="header_pnl_link">
                        <li class="home"><a href="Alumni/Alm_Default.aspx">Home</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="container custom-conatiner">
                <div class="form-inn">
                    <div class="row">
                        <div class="col-sm-7">
                            <h1 style="font-size: 24px; font-weight: 700;">For New Alumni </h1>
                            <span class="spanLoginReg" style="font-size: 20px; font-weight: 500;">&nbsp; Alumni Type                              
                                 <Anthem:RadioButtonList ID="rdalumnitype" runat="server" RepeatDirection="Horizontal" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="rdalumnitype_SelectedIndexChanged">
                                     <asp:ListItem Value="F"> <b class="spanLoginReg">&nbsp;Faculty</b></asp:ListItem>
                                     <asp:ListItem Value="S"> <b class="spanLoginReg">&nbsp; Current Student</b></asp:ListItem>
                                     <asp:ListItem Value="ExStu"> <b class="spanLoginReg">&nbsp;Ex-Student</b></asp:ListItem>
                                     <asp:ListItem Value="StuCh"> <b class="spanLoginReg">&nbsp;Student Chapter</b></asp:ListItem>
                                 </Anthem:RadioButtonList>
                            </span>

                            <div class="form-group">
                                <span class="spanLoginReg">
                                    <Anthem:LinkButton runat="server" ID="alm_registration" CssClass="flash" Text="Forgot Your Password ?" OnClick="alm_registration_Click" title="Click Here To Navigate On Resgistration Page" Style="font-weight: 700; font-size: 19px;">
                                        <i class="fa fa-fw fa-user-plus flash fontawsomemargin" style="color: yellow"></i>New Registration
                                    </Anthem:LinkButton>
                                </span>
                            </div>

                            <div class="form-group">
                                <p style="color: #ffffff; font-family: Arial,Helvetica,sans-serif; font-size: 15px; font-weight: 600;">
                                    <i class="fa fa-envelope fontawsomemargin"></i>
                                    Mail us at : hpualumniassociation@gmail.com
                                </p>
                            </div>

                            <%--<div class="form-group">
                                <ul style="color: #FFFFFF; font-family: Arial,Helvetica,sans-serif; font-size: 15px; font-weight: 600; margin-bottom: 0;">
                                    <li>Life-time Membership Fee : ₹ 2000 </li>
                                    <li>Life-time Student Membership Fee : ₹ 1000 </li>
                                    <li>Student Chapter Membership Fee : ₹ 100 </li>
                                </ul>
                                <div class="form-group form-group-sm">
                                    <img id="imgQR" autoupdateaftercallback="true" cssclass="img-responsive" runat="server" style="float: right; height: 70px; width: 70px;" />
                                    <span id="qrPlaceholder" style="display: none; float: right; color: gray; font-size: 12px;">QR Code not available</span>
                                </div>
                            </div>

                             <div class="form-group">
                                <span id="noteMessage" class="note-line">Note: No fees will be applicable for Specially Abled (Divyangjan) persons.
                                </span>
                            </div>--%>

                            <div class="form-group fees-qr-wrapper">
                                <!-- Fee list -->
                                <ul>
                                    <li>Life-time Membership Fee : ₹ 2000</li>
                                    <li>Life-time Student Membership Fee : ₹ 1000</li>
                                    <li>Student Chapter Membership Fee : ₹ 100</li>
                                </ul>

                                <!-- QR Box -->
                                <%--<div class="qr-box" id="qrScan" runat="server" visible="false" AutoUpdateAfterCallBack="true">--%>
                                <%--<img id="imgQR" runat="server" class="img-responsive" alt="QR Code" />--%>
                                <%--<Anthem:Image ID="imgQR" runat="server" class="img-responsive" alt="QR Code" AutoUpdateAfterCallBack="true" />
                                    <span id="qrPlaceholder" style="display: none;" runat="server">QR Code not available</span>
                                    <p style="color: #ffffff; font-size: 15px; margin-left: -10px; text-decoration: underline">Scan to Register</p>
                                </div>--%>

                                <Anthem:Panel ID="pnlQR" runat="server" Visible="false" AutoUpdateAfterCallBack="true">
                                    <div class="qr-box">
                                        <Anthem:Image ID="imgQR" runat="server"
                                            class="img-responsive"
                                            alt="QR Code"
                                            AutoUpdateAfterCallBack="true" />
                                        <span id="qrPlaceholder" style="display: none;" runat="server">QR Code not available</span>
                                        <p style="color: #ffffff; font-size: 15px; margin-left: -10px; text-decoration: underline">Scan to Register</p>
                                    </div>
                                </Anthem:Panel>

                            </div>

                            <div class="form-group">
                                <span id="noteMessage" class="note-line">Note: No fees will be applicable for Specially Abled (Divyangjan) persons.
                                </span>
                            </div>

                        </div>

                        <div class="col-sm-5">
                            <h1 style="font-size: 24px; font-weight: 700;">Alumni Login </h1>
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <i class="fa fa-fw fa-user"></i>
                                    </span>
                                    <Anthem:TextBox ID="R_txtLogin" runat="server" MaxLength="50" AutoUpdateAfterCallBack="true" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-key"></i>
                                    </div>
                                    <Anthem:TextBox ID="R_txtPass" TextMode="Password" runat="server" MaxLength="50" CssClass="form-control" />
                                    <span id="toggle_pwd" class="fa fa-fw fa-eye field_icon" style="cursor: pointer;"></span>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <Anthem:ImageButton ID="imgReferesh" runat="server" Style="border-width: 0px; padding: 0; width: 15px; height: 15px;" OnClick="imgReferesh_Click" src="Images/refreshing.png" />
                                    </div>
                                    <Anthem:TextBox ID="R_txtCaptcha" CssClass="form-control" runat="server" MaxLength="5" placeholder="Enter Captcha" AutoUpdateAfterCallBack="true" AutoComplete="off" />

                                    <Anthem:Image ID="imgCaptcha" runat="server" Style="position: absolute; right: 0; z-index: 111; height: 34px;" AutoUpdateAfterCallBack="true" />

                                </div>
                            </div>

                            <%-- <div class="viewrow">
                                <Anthem:Label ID="lblCaptch" runat="server" Font-Bold="true" ForeColor="Red"> </Anthem:Label>
                                <label style="color: white; font-size: 12px; font-weight: bold;">Captcha :</label>                                
                            </div>--%>

                            <div class="captcha-case-txt">
                                <Anthem:Label ID="lblStar" runat="server" Text="* (Captcha letters are Case Sensitive)" ForeColor="white" />
                            </div>

                            <Anthem:Button ID="btnLogin" runat="server" Text="Login" OnClick="imgLogin_Click" AutoUpdateAfterCallBack="true" OnClientClick='return onsubmitclick();' TextDuringCallBack="Wait..." CssClass="btn btn-primary btn-lg btn-block login-btn" />

                            <input type="hidden" runat="server" name="hash" id="hash" readonly autocomplete="off" />
                            <input type="hidden" runat="server" name="uname" id="uname" readonly autocomplete="off" />
                            <input type="hidden" runat="server" name="saltval" id="saltval" autocomplete="off" />

                            <Anthem:LinkButton runat="server" ID="lnkForgotpass" Text="Forgot Your Password ?" OnClientClick="ShowSearch(this);"
                                ForeColor="#FFFFFF" AutoUpdateAfterCallBack="true" Style="padding-left: 0px;"></Anthem:LinkButton><br />
                            <Anthem:Label ID="lblError" runat="server" CssClass="lblmessage" Font-Bold="True"
                                Font-Underline="False" AutoUpdateAfterCallBack="true" />

                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="row">
                                        <Anthem:Label ID="lblPaymentMsg" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--<div class="copyrightss">
            <img src="../App_Themes/CCSBLUE/images/manage-iti.png" />
        </div>--%>

        <div id="SearchDiv" class="white_content-new-1" style="display: none;">
            <div class="popupboxouter" style="width: 440px;">
                <div class="popupbox" style="width: 440px;">
                    <div onclick="document.getElementById('SearchDiv').style.display='none';" class="close-1">
                        X
                    </div>
                    <table class="table" width="100%">
                        <tr>
                            <td class="tableheading" colspan="3">Forgot Password </td>
                        </tr>
                        <tr>
                            <td class="vtext" id="lblUsername">USER NAME </td>
                            <td class="colon">: </td>
                            <td class="required">
                                <Anthem:TextBox runat="server" ID="R_txtUsername" AutoUpdateAfterCallBack="true"></Anthem:TextBox>*
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="vtext" id="lblEmail">EMAIL </td>
                            <td class="colon">: </td>
                            <td class="required">
                                <Anthem:TextBox runat="server" ID="E_txtEmail" Visible="false" AutoUpdateAfterCallBack="true" onblur="return validateEmail(this)"></Anthem:TextBox>*
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td>
                                <Anthem:Button runat="server" ID="btnSend" TextDuringCallBack="SENDING..." CausesValidation="true" AutoUpdateAfterCallBack="true"
                                    Text="SEND" OnClick="btnSend_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <Anthem:Label runat="server" ID="lblMsg" AutoUpdateAfterCallBack="true" ForeColor="#ff0000"></Anthem:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <script src="include/jquery.min.js"></script>
        <script src="../include/jquery.backstretch.min.js"></script>
        <script>
            $.backstretch("include/bootstrap/images/alumni-login.jpg", { speed: 500 });
        </script>

        <script type="text/javascript">
            function ShowSearch(obj) {
                document.getElementById('SearchDiv').style.display = 'block';
            }

            function validateAlumniType() {

                var radioButtons = document.getElementsByName('<%= rdalumnitype.ClientID %>');
                var isSelected = false;

                for (var i = 0; i < radioButtons.length; i++) {
                    if (radioButtons[i].checked) {
                        isSelected = true;
                        break;
                    }
                }
                if (!isSelected) {
                    alert("Please select an alumni type before proceeding with the registration.");
                    return false;
                }
                return true;
            }
        </script>

        <script type="text/javascript">

            //Disable mouse right click
            $("body").on("contextmenu", function (e) {
                return false;
            });

            $(document).on("contextmenu", function (e) {
                e.preventDefault();
            });

        </script>
    </form>
</body>
</html>