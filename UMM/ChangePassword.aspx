<%@ Page Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="UMM_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script src="../include/sha1.js" type="text/javascript"></script>
<script src="../include/utf8.js" type="text/javascript"></script>
    <script language="javascript">
        function PasswordCheck() {
            var pass = document.getElementById("ctl00_ContentPlaceHolder1_R_txtPwd");
            var pass1 = document.getElementById("ctl00_ContentPlaceHolder1_R_txtRPassword");
            if (pass.value != pass1.value && pass1.value != "") {
                alert("Password & Confirm Password must be same");
                pass1.value = "";
                pass1.focus();
            }
        }

        function setval() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_R_txtOldPassword').value == "") {
                alert("Old Password is required!")
                document.getElementById('ctl00_ContentPlaceHolder1_R_txtOldPassword').focus();
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_R_txtPwd').value == "") {
                document.getElementById('ctl00_ContentPlaceHolder1_hash').value = null;
                document.getElementById('ctl00_ContentPlaceHolder1_R_txtPwd').focus();
                alert("New Password is required!")
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_R_txtRPassword').value == "") {
                alert("Confirm New Password is required!")
                document.getElementById('ctl00_ContentPlaceHolder1_R_txtRPassword').focus();
                return false;
            }

            document.getElementById('ctl00_ContentPlaceHolder1_hash').value = Sha1.hash(document.getElementById('ctl00_ContentPlaceHolder1_R_txtPwd').value);
            //document.getElementById('ctl00_ContentPlaceHolder1_R_txtPwd').value = '';
            //document.getElementById('ctl00_ContentPlaceHolder1_R_txtOldPassword').value = '';
            //document.getElementById('ctl00_ContentPlaceHolder1_R_txtRPassword').value = '';
        }
        function checkpolicy() {

            var pass = document.getElementById("ctl00_ContentPlaceHolder1_R_txtPwd");
            var Str = document.getElementById('ctl00_ContentPlaceHolder1_R_txtPwd').value;
            var len = document.getElementById('ctl00_ContentPlaceHolder1_MinLength').value;
            if (document.getElementById('ctl00_ContentPlaceHolder1_Allow_Pass_equal_userid').value == "True") {
                if (document.getElementById('ctl00_ContentPlaceHolder1_username').value == Str) {
                    alert("Login Name & Password can not be same");
                    pass.value = "";
                    pass.focus();
                    return false;
                }

            }
            if (Str.length < len) {
                alert("New password should have minimum " + len + " Characters");
                pass.value = "";
                pass.focus();
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_MinNumericChar').value == "True") {
                if (!/\d/.test(Str)) //For one Numeric Case
                {
                    alert("New password should Contain a Numeric Value");
                    pass.value = "";
                    pass.focus();
                    return false;
                }
            }
        }

    </script>
    <table border="0" class="secContent" style="width: 100%;">
        <tr>
            <td align="center" class="tableheading" colspan="3">
                Change Password
            </td>
        </tr>
        <tr>
            <td class="tdgap" colspan="3">
            </td>
        </tr>
        <tr>
            <td>
                <input runat="server" type="hidden" name="MinLength" id="MinLength" readonly autocomplete="off" />
                <input runat="server" type="hidden" name="MinUpperCaseChar" id="MinUpperCaseChar"
                    readonly autocomplete="off" />
                <input runat="server" type="hidden" name="MinNumericChar" id="MinNumericChar" readonly
                    autocomplete="off" />
                <input runat="server" type="hidden" name="MinSpecialChar" id="MinSpecialChar" readonly
                    autocomplete="off" />
                <input runat="server" type="hidden" name="Allow_Pass_equal_userid" id="Allow_Pass_equal_userid"
                    readonly autocomplete="off" />
                <input runat="server" type="hidden" name="username" id="username" readonly autocomplete="off" />
                <input runat="server" type="hidden" name="hash" id="hash" readonly autocomplete="off" />
            </td>
        </tr>
        <tr>
            <td id="lblOldPassword" align="left" class="vtext" style="width: 162px">
                Old Password
            </td>
            <td class="colon" align="left">
                :
            </td>
            <td class="required" align="left">
                <Anthem:TextBox ID="R_txtOldPassword" AutoUpdateAfterCallBack="true" runat="server"
                    CssClass="textbox" TextMode="Password" ></Anthem:TextBox>*
            </td>
        </tr>
        <tr>
            <td id="lblPwd" align="left" class="vtext" style="width: 162px">
                New Password
            </td>
            <td align="left" class="colon">
                :
            </td>
            <td align="left" class="required">
                <Anthem:TextBox ID="R_txtPwd" runat="server" AutoUpdateAfterCallBack="true" CssClass="textbox"
                    TextMode="Password" onblur="return checkpolicy();"></Anthem:TextBox>*
            </td>
        </tr>
        <tr>
            <td id="lblRPassword" align="left" class="vtext" style="width: 162px">
                Confirm New Password
            </td>
            <td class="colon" align="left">
                :
            </td>
            <td class="required" align="left">
                <Anthem:TextBox ID="R_txtRPassword" AutoUpdateAfterCallBack="true" runat="server"
                    CssClass="textbox" TextMode="Password" onblur="PasswordCheck();"></Anthem:TextBox>
                *
            </td>
        </tr>
        <tr>
            <td style="width: 162px; height: 21px;">
            </td>
            <td style="width: 100px; height: 21px;">
                &nbsp;
            </td>
            <td style="width: 100px; height: 21px;">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <Anthem:Button ID="btnSave" runat="server" OnClientClick="return setval();" CssClass="btn btn-primary "
                    Text="Change Password" Width="157px" AutoUpdateAfterCallBack="true" TextDuringCallBack="WAIT..."
                    OnClick="btnSave_Click"/>
                <Anthem:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Reset" OnClick="btnCancel_Click"
                    AutoUpdateAfterCallBack="true" />
            </td>
        </tr>
    </table>
</asp:Content>
