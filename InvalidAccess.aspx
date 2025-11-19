<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvalidAccess.aspx.cs" Inherits="InvalidAccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Invalid Page</title>
    <script src="include/sha1.js" type="text/javascript"></script>
    <script src="include/utf8.js" type="text/javascript"></script>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="include/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="include/bootstrap/font-awesome.min.css" rel="stylesheet" />
    <link href="include/bootstrap/style_outside-page.css" rel="stylesheet" />
    <script src="include/bootstrap/bootstrap.js"></script>
    <%--<style type="text/css">
        html, body {
            height: 100%;
            overflow: hidden;
        }
    </style>--%>
</head>
<body style="background: #fff;">

    <form id="form1" runat="server">
        <div class="error_page">
            <div id="wrap" class="animsition backlayer" style="opacity: 1;">
                <header class="layerLeft dark-header">
                <div class="logo text-center"><img src="<%=Page.ResolveUrl("~/App_Themes/skyblue/images/logo.png")%>"></div>
            </header>
                <div class="layerRight col-md-12 col-xs-12">
                    <div class="page-login text-center">
                        <div class="login-page">
                            <div class="form-validation mt-20">
                                <span class="" style="margin-bottom: 20px; display: inline-block;">
                                    <img src="img/invalid-access.png" width="140" /></span>
                                <p class="error_404" style="text-align: center !important">INVALID ACCESS</p>
                                <p class="error_p" style="text-align: center !important">You may want to return to </p>
                                <div class="form-group text-center ">
                                    <a href='<%=Page.ResolveUrl("~/main.aspx")%>' class="btn btn-greensea b-0 br-2" style="margin: 0;">Click here for Home Page !!</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <footer class="loginfooter-lgn"><a class="" href="#"><img src="img/manage-iti.png"> </a></footer>
            </div>
        </div>
    </form>
    <%-- <form id="form1" runat="server">
        <div class="logout-wp">
            <span>
                <h1>INVALID ACCESS</h1>
                <a href="../mlsupreadm/main.aspx" class="wel-ic">Back to Home Page</a></span>
            <div class="log-ic">
                <div style="text-align: center; width: 200px;">
                    <img src="Images/warninvalid.gif" /><br />
                    Copy / Paste URL in new tab /<br />
                    window is not allowed !
                </div>
            </div>
        </div>
    </form>--%>
</body>
</html>
