<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true"
    CodeFile="ALM_AlumniChangePassword.aspx.cs" Inherits="Alumni_ALM_AlumniChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function PasswordCheck() {
            var pass = document.getElementById("ctl00_ContentPlaceHolder1_R_txtPwd");
            var pass1 = document.getElementById("ctl00_ContentPlaceHolder1_R_txtRPassword");
            if (pass.value != pass1.value && pass1.value != "") {
                alert("Passwords must be same");
                pass1.value = "";
                pass1.focus();
            }
        }

        function PasswordValidate() {
            var OldPassword = document.getElementById("ctl00_ContentPlaceHolder1_R_txtOldPassword");
            var Newpwd = document.getElementById("ctl00_ContentPlaceHolder1_R_txtPwd");
            var ConfirmPwd = document.getElementById("ctl00_ContentPlaceHolder1_R_txtRPassword");

            if (OldPassword.value == "") {
                alert("Old Password  is Required");
                OldPassword.focus();
                return false;
            }
            if (Newpwd.value == "") {
                alert("New Password  is Required");
                Newpwd.focus();
                return false;
            }
            if (ConfirmPwd.value == "") {
                alert("Confirm Password  is Required");
                ConfirmPwd.focus();
                return false;
            }

            if (Newpwd.value != ConfirmPwd.value) {
                alert("Cofirm Password is incorrect");
                ConfirmPwd.value = "";
                ConfirmPwd.focus();
                return false;
            }
        }

   <%--     function CheckPassword_NewPass() {
       var R_txtPwd = document.getElementById('<%= R_txtPwd.ClientID %>');
    // Regular expression for password:
    // - Must start with a letter (uppercase or lowercase).
    // - Must contain at least one digit (\d), one special character ([!@#$%^&*]), and one alphanumeric character (\w).
    // - Must be between 8 to 15 characters long.
    var passw = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[A-Za-z])[A-Za-z\d!@#$%^&*]{8,15}$/;

    if (R_txtPwd.value.match(passw)) {
        alert('Correct, try another...');
        return true;
    }
    else {
        alert('Password must start with a letter, contain at least one digit, one special character, and be between 8 to 15 characters long.');
        return false;
    }
        }--%>

      function CheckPassword_NewPass() {
    var R_txtPwd = document.getElementById('<%= R_txtPwd.ClientID %>');
    var passw = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[A-Za-z])[A-Za-z\d!@#$%^&*]{8,15}$/;
    var validationIcon = document.getElementById('validationIcon'); // Ensure this element exists in your HTML

    if (R_txtPwd.value.match(passw)) {
        validationIcon.innerHTML = '✓';
        validationIcon.className = 'valid';
        return true;
    } else {
        validationIcon.innerHTML = '✗';
        validationIcon.className = 'invalid';
        return false;
    }
}


        function CheckPassword_ConfirmPass() {
            var R_txtRPassword = document.getElementById('<%= R_txtRPassword.ClientID %>');
    var passw = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[A-Za-z])[A-Za-z\d!@#$%^&*]{8,15}$/;
    var validationIconconfirm = document.getElementById('validationIcon'); // Ensure this element exists in your HTML

    if (R_txtRPassword.value.match(passw)) {
        validationIconconfirm.innerHTML = '✓';
        validationIconconfirm.className = 'valid';
        return true;
    } else {
        validationIconconfirm.innerHTML = '✗';
        validationIconconfirm.className = 'invalid';
        return false;
    }
        }




       <%--  function CheckPassword_ConfirmPass() {
             var R_txtRPassword = document.getElementById('<%= R_txtRPassword.ClientID %>');
    // Regular expression for password:
    // - Must start with a letter (uppercase or lowercase).
    // - Must contain at least one digit (\d), one special character ([!@#$%^&*]), and one alphanumeric character (\w).
    // - Must be between 8 to 15 characters long.
    var passw = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[A-Za-z])[A-Za-z\d!@#$%^&*]{8,15}$/;

    if (R_txtRPassword.value.match(passw)) {
        alert('Correct, try another...');
        return true;
    }
    else {
        alert('Password must start with a letter, contain at least one digit, one special character, and be between 8 to 15 characters long.');
        return false;
    }
}--%>
  </script>
     <style>
        .valid {
    color: green;
}
.invalid {
    color: red;
}
    </style>

    <div class="container-fluid mt-10">
        <div class="">

            <div class="box box-success" runat="server">
                <div class="boxhead" runat="server">
                    Alumni Change Password
                </div>
                <div class="panel-body pnl-body-custom">

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Old Password </label>
                            <div class="col-sm-6 required">
                                <Anthem:TextBox ID="R_txtOldPassword" runat="server" CssClass="textbox" TextMode="Password" AutoUpdateAfterCallBack="True" 
                                    MaxLength="15"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">New Password </label>
                            <div class="col-sm-6 required">
                                <Anthem:TextBox ID="R_txtPwd" runat="server" CssClass="textbox" TextMode="Password" AutoUpdateAfterCallBack="True" onchange="return CheckPassword_NewPass();"
                                    MaxLength="15"></Anthem:TextBox>*
                                <span id="validationIcon"></span>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Confirm New Password </label>
                            <div class="col-sm-6 required">
                                <Anthem:TextBox ID="R_txtRPassword" onblur="PasswordCheck()" runat="server" CssClass="textbox" AutoUpdateAfterCallBack="True" onchange="return CheckPassword_ConfirmPass();"
                                    TextMode="Password" MaxLength="15"></Anthem:TextBox>*
                                <span id="validationIconconfirm"></span>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-2">
                                <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click" OnClientClick="return PasswordValidate();"
                                    Text="CHANGE PASSWORD" Width="157px" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="RESET" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
