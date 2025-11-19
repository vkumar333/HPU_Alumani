<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ALM_Payment_Success_backup.aspx.cs" Inherits="Onlinepayment_ALM_Payment_Success" %>


<html>
<head runat="server">    
    <link href="include/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    
    <link rel="stylesheet" type="text/css" href="include/bootstrap/css/font-awesome.min.css" />
    <link rel="stylesheet" href="include/bootstrap/css/mini-event-calendar.min.css" />
    <link rel="stylesheet" type="text/css" href="include/bootstrap/css/style.css" />
    <link rel="stylesheet" href="include/bootstrap/css/scroll.css" />
    <link rel="stylesheet" href="include/bootstrap/css/weather.css" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style>
        input#RblUGType_0 {
            margin-right: 4px;
        }

        input#RblUGType_1 {
            margin: 4px;
        }

        input#RblPGType_0 {
            margin-right: 4px;
        }

        input#RblPGType_1 {
            margin: 4px;
        }

        table#RblProgramType {
            margin-top: 7px;
            margin-bottom: -8px;
        }

        input#RblProgramType_0 {
            margin-left: 5px;
            margin-right: 4px;
        }

        input#RblProgramType_1 {
            margin-left: 5px;
            margin-right: 4px;
        }

        .logbutt {
            background: #016773 none repeat scroll 0 0;
            border: medium none;
            border-radius: 5px;
            color: #fff;
            cursor: pointer;
            font-size: 14px;
            font-weight: bold;
            padding: 5px 10px 6px;
        }

        .auto-style1 {
            border-collapse: collapse;
            max-width: 100%;
            margin-right: 33;
            margin-bottom: 20px;
        }
    </style>



    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-97553048-1', 'auto');
        ga('send', 'pageview');

    </script>

    <script type="text/javascript" language="javascript">
        function IsDigit(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var result = (keyCode >= 48 && keyCode <= 57);
            return result;
        }

        function Check_Before_Delete() {
            var x = confirm("Are you sure to delete this record?");
            if (x == true)
                return true;
            else
                return false;
        }

    </script>
    <script type="text/javascript">
        function AllownumberOnly(event, source) {
            var ev = (event) ? (event) : window.event;
            var code = ev.which ? ev.which : ev.keyCode;
            if (code >= 48 && code <= 57 || code == 8 || code == 9 || code >= 96 && code <= 105 || code == 37 || code == 13 ||
                code == 38 || code == 39 || code == 40 || code == 46 || code == 123 || code == 116)
                return true;
            else if (ev.altKey) {
                alert("ALT Key is not allowed");
                return false;
            }
            else
                return false;
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function CheckDob() {
            var Mnth = 0;
            var Dte = 0;
            var dobtxt = document.getElementById('ctl00_ContentPlaceHolder1_txtDob').value;
            if (dobtxt != "") {
                var GetDate = dobtxt.split('/')[0];
                var GetMonth = (dobtxt.split('/')[1]) - 1;// as month start from 0 and end to 11
                var GetYear = dobtxt.split('/')[2];
                var current = new Date();


                var d1 = new Date(GetYear, GetMonth, GetDate);  // get the selected dob
               <%-- var degid = <%= Session["ProgrammeId"].ToString()%>;
               
                if(degid==37 || degid==35 || degid==34 || degid==111 || degid==112)
                {
                    Mnth=11; // put here 31 st December of current year as month start from 0 to 11 so 11 is the month of December
                    Dte=31;  // When the Degree is B.Sc Nurshing
                }
                else
                {--%>
                Mnth = 6; //// put here 1 st july of current year as month start from 0 to 11 so 6 is the month of july
                Dte = 1;
                // }
                var d2 = new Date(current.getFullYear(), Mnth, Dte);

                if (d1 > d2) {
                    alert("Please Choose Valid Date of Birth!");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtDob').value = '';
                    return false;
                }

                var d1Y = d1.getFullYear();

                var d2Y = d2.getFullYear();

                var d1M = d1.getMonth();

                var d2M = d2.getMonth();
                var Month = (d2M + 12 * d2Y) - (d1M + 12 * d1Y);

                var totalYear = Month / 12;

                var totalMonth = Month - (12 * Math.floor(totalYear));

                document.getElementById('ctl00_ContentPlaceHolder1_stYear').innerText = "0";
                document.getElementById('ctl00_ContentPlaceHolder1_stYear').innerText = Math.floor(totalYear);
                document.getElementById('ctl00_ContentPlaceHolder1_stMonths').innerText = "0";
                document.getElementById('ctl00_ContentPlaceHolder1_stMonths').innerText = totalMonth;

                if (totalYear < 15) {

                    alert("Age should be greater than atleast 15 Years!");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtDob').value = '';

                    return false;
                }

                document.getElementById('ctl00_ContentPlaceHolder1_hfvforyear').value = Math.floor(totalYear);
                document.getElementById('ctl00_ContentPlaceHolder1_hfvformonth').value = totalMonth;

            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_stYear').innerText = "0";
                document.getElementById('ctl00_ContentPlaceHolder1_stMonths').innerText = "0";
            }


        }

        function isNumberKey(evt, cntrl) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function OnDOB(e) {

            if (e.shiftKey || e.ctrlKey || e.altKey) {
                e.preventDefault();
            } else {
                var key = e.keyCode;
                if (!((key == 9))) {
                    e.preventDefault();
                }
            }
        }

        <%-- function SetCategory() {

            var degid =   <%= Session["ProgrammeId"].ToString()%>;
            if(degid!=58 && degid!=80 && degid!=81 && degid!=52)
            {                
                document.getElementById("ctl00_ContentPlaceHolder1_ddlCategory").selectedIndex = 0;
                document.getElementById("ctl00_ContentPlaceHolder1_stroRs").innerHTML = "";
            }
        }--%>



        //function to show marks table when degree is BSC Nursing



    </script>

    <script type="text/javascript">
        function NumberOnlyForMarks(obj, vl) {
            var navigatorVersion = navigator.appVersion;
            var navigatorAgent = navigator.userAgent;
            var browserName = navigator.appName;
            var fullVersionName = '' + parseFloat(navigator.appVersion);
            var majorVersionName = parseInt(navigator.appVersion, 10);
            var nameOffset, verOffset, ix;
            // For FireFox Browser
            if ((verOffset = navigatorAgent.indexOf("Firefox")) != -1) {
                browserName = "Firefox";
                var intKey = (window.Event) ? obj.which : obj.keyCode;
                if (obj.shiftKey && intKey != 9)  //Block the special charecter which pressed with shift key
                {
                    alert("Shift key not allowed ");
                    return false;
                }
                if (obj.altKey)// || intKey == 9)//Block special symbol which pressed with alt key
                {
                    alert("Alt key not allowed");
                    return false;
                }
                if (intKey >= 48 && intKey <= 57)//for 0-9 front keys
                {
                    return true;
                }
                else if (intKey >= 96 && intKey <= 105)//for 0-9 front keys
                {
                    return true;
                }
                else if (intKey == 46 || intKey == 110 || intKey == 190)//for decimals
                {
                    var val = vl.value;
                    for (i = 0; i < val.length; i++) {
                        var cchar = val.charAt(i);
                        if (cchar == '.') {
                            return false;
                        }
                    }
                }
                else if (intKey == 36 || intKey == 37 || intKey == 39 || intKey == 8 || intKey == 46 || intKey == 35 || intKey == 9) {
                    return true;
                }
                else {
                    return false;
                }

            } //For Other Browsers
            else {
                if (window.event.shiftKey && event.keyCode != 9)  //Block the special charecter which pressed with shift key
                {
                    alert("Shift key not allowed ");
                    event.returnValue = false;
                    return false
                }
                if (window.event.altKey)//Block special symzbol which pressed with alt key
                {
                    alert("Alt key not allowed");
                    event.returnValue = false;
                    return false;
                }
                if (event.keyCode >= 48 && event.keyCode <= 57)//for 0-9 front keys
                {
                    event.returnValue = true;
                    return true;
                }
                else if (event.keyCode >= 96 && event.keyCode <= 105)//for 0-9 right keys
                {
                    event.returnValue = true;
                    return true;
                }
                else if (event.keyCode == 46 || event.keyCode == 110 || event.keyCode == 190)//for decimals
                {
                    var val = vl.value;
                    for (i = 0; i < val.length; i++) {
                        var cchar = vl.value.charAt(i);
                        if (cchar == '.') {
                            event.returnValue = false;
                            return false;
                        }
                    }
                }
                else if (event.keyCode == 36 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 35 || event.keyCode == 9) {
                    event.returnValue = true;
                    return true;
                }
                else {
                    event.returnValue = false;
                    return false;
                }
            }
        }
        function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</head>
<body>
    <div class="container-fluid">
       <div class="row">
                <nav class="navbar navbar-default">
                    <div class="container">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar"><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                            <a class="navbar-brand" href="#">
                                <img src="include/bootstrap/images/hpu-logo_img.png" class="img-responsive" />
                            </a>
                        </div>
                        <div class="collapse navbar-collapse" id="myNavbar">
                            <ul class="nav navbar-nav pull-right">

                                <li><a href="https://rme.hpushimla.in" data-toggle="modal" data-backdrop="static" data-keyboard="false"><i class="fa fa-fw fa-user"></i>Home</a></li>
                                <%--<li><a href="CommonPGandUG/RME_Status_Page.aspx" data-toggle="modal" data-backdrop="static" data-keyboard="false"><i class="fa fa-fw fa-user"></i>Home</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </nav>
            </div>
        <div class="clearfix"></div>
        <div class="body-content-inside">
            <form id="form1" runat="server">
                <div class="">
                    <fieldset class="fieldset-formating">
                        <legend><i class="fa fa-fw fa-user"></i>RME Fee Management</legend>

                        <div class="mainbody">
                            <Anthem:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" AutoUpdateAfterCallBack="true" OnClick="btnBack_Click" />
                            <div class="bodywrap" style="min-height: 340px;">
                                <div class="payment-pg-outer " style="width: 75%; margin-top: 5%;">
                                    <div class="payment-pg payment-common" runat="server" id="divSuccessPayment" visible="false">
                                        <Anthem:Label ID="lblSuccessMsg" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                        <br />
                                        <br />
                                        <Anthem:Button ID="bntSuccess" runat="server" CssClass="btn btn-warning" AutoUpdateAfterCallBack="true" Text="Click here to Print your Migration Certificate" OnClick="bntSuccess_Click" />
                                    </div>
                                </div>
                                <div class="payment-pg-outer" style="width: 75%; margin-top: 5%;">
                                    <div class="payment-pg-Pending payment-common" runat="server" id="divPendingPayment" visible="false">
                                        <Anthem:Label ID="lblPendingMsg" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                        <br />
                                        <br />
                                        <Anthem:Button ID="btnPending" runat="server" CssClass="btn btn-warning" Text="Make Another Transaction" AutoUpdateAfterCallBack="true" OnClick="btnPending_Click" />
                                    </div>
                                </div>
                                <div class="payment-pg-outer" style="width: 75%; margin-top: 5%;">
                                    <div class="payment-pg-fail payment-common" runat="server" id="divFailedPayment" visible="false">
                                        <Anthem:Label ID="lblFailedMsg" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                        <br />
                                        <br />
                                        <Anthem:Button ID="btnFailed" runat="server" CssClass="btn btn-warning" Text="Make Another Transaction" AutoUpdateAfterCallBack="true" OnClick="btnFailed_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </form>
        </div>
        <div class="footertext1">                        
            <a href="mailto:iumshpu@gmail.com" target="_blank">
                <img src="App_Themes/Darkgreen/images/manage-iti.png" />
            </a>
        </div>
    </div>
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js" class="findcal"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</body>
</html>