<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_AlumniProfileApprovedbyadm.aspx.cs" Inherits="Alumni_ALM_AlumniProfileApprovedbyadm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%=Request.ApplicationPath%>/include/CommonJS.js' type="text/javascript"></script>
    <script src='<%=Request.ApplicationPath%>/include/jquery.min.js' type="text/javascript"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
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

    <script language="javascript" type="text/javascript">
        function ButtonClick(ctr) {
            var ctrl = "ctl00_ContentPlaceHolder1_" + ctr;
            document.getElementById(ctrl).click();
            return false;
        }
        function removefile() {
            $('#ctl00_ContentPlaceHolder1_imgProfileimg').attr("src", "../Alumni/StuImage/No_image.png");
            return false;
        }
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                return true;
            else {
                alert('Please Enter Only Character.');
                return false;
            }
        }
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
        function previewFile() {
            var preview = document.querySelector('#<%=imgProfileimg.ClientID %>');
            var file = document.querySelector('#<%=flUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

        function CheckDob() {
            alert("fsffsf");
            var Mnth = 0;
            var Dte = 0;
            var dobtxt = document.getElementById('ctl00_ContentPlaceHolder1_R_txtPostedDate').value;
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
                    document.getElementById('ctl00_ContentPlaceHolder1_R_txtPostedDate').value = '';
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
                    document.getElementById('ctl00_ContentPlaceHolder1_R_txtPostedDat').value = '';

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
        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (reg.test(ctl00_ContentPlaceHolder1_E_txtEmail.value) == false) {
                alert('Invalid Email Address');
                ctl00_ContentPlaceHolder1_E_txtEmail.value = "";
                return false;
            }
            return true;
        }

        document.addEventListener('contextmenu', function (e) {
            e.preventDefault();
        });

    </script>



    <fieldset class="fieldset-border">
        <legend>Alumni Profile</legend>
        <div class="panel-body pnl-body-custom">

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Alumni Type </label>
                    <div class="col-sm-4 required">
                        <Anthem:RadioButtonList ID="rdalumnitype" runat="server" RepeatDirection="Horizontal" AutoCallBack="true" AutoUpdateAfterCallBack="true" Height="22px" Enabled="false" oncontextmenu="return false;">
                            <asp:ListItem Value="F"> <b class="spanLoginReg">&nbsp;Faculty</b></asp:ListItem>
                            <asp:ListItem Value="S"> <b class="spanLoginReg">&nbsp; Current Student</b></asp:ListItem>
                            <asp:ListItem Value="ExStu"> <b class="spanLoginReg">&nbsp;Ex-Student</b></asp:ListItem>
                        </Anthem:RadioButtonList>
                    </div>
                    <label class="col-sm-2 control-label">Membership Type</label>
                    <label class="col-md-4 control-label custom-text-left">
                        <Anthem:RadioButtonList ID="rbdAlumniMemtype" runat="server" CssClass="form-control" RepeatDirection="Horizontal" AutoCallBack="true" OnSelectedIndexChanged="rbdAlumniMemtype_SelectedIndexChanged" AutoUpdateAfterCallBack="true" Enabled="false" oncontextmenu="return false;">
                            <asp:ListItem Value="LM"><b class="spanLoginReg">&nbsp;Life Membership(2000)</b> </asp:ListItem>
                            <asp:ListItem Value="SM"><b class="spanLoginReg">&nbsp;Student Membership(1000)</b> </asp:ListItem>
                        </Anthem:RadioButtonList>
                    </label>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Alumni Registration No.</label>
                    <div class="col-md-4">
                        <Anthem:TextBox ID="R_txtAlumnino" runat="server" AutoUpdateAfterCallBack="True"
                            MaxLength="100" CssClass="form-control" Font-Bold="true" Enabled="false" oncontextmenu="return false;"></Anthem:TextBox>
                        <span style="color: red">*</span>
                    </div>
                    <label class="col-sm-2 control-label">Alumni Name: </label>
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-4">
                                <Anthem:DropDownList ID="ddlAlmPrefix" CssClass="form-control" AutoCallBack="true" runat="server" AutoUpdateAfterCallBack="true" Enabled="true" />
                            </div>
                            <div class="col-sm-8">
                                <Anthem:TextBox ID="txtAlumniName" Enabled="true" onkeypress="return AllowAlphabet(event)" runat="server"
                                    MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Father's Name: </label>
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-4">
                                <Anthem:DropDownList ID="ddlFPrefix" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" Enabled="true" />
                            </div>
                            <div class="col-sm-8">
                                <Anthem:TextBox ID="R_txtFatherName" Enabled="true" onkeypress="return AllowAlphabet(event)" runat="server" AutoUpdateAfterCallBack="True"
                                    MaxLength="100" CssClass="form-control" ondrop="return false" onpaste="return false" TabIndex="3"></Anthem:TextBox>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                    </div>
                    <label class="col-sm-2 control-label">Mother's Name: </label>
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-4">
                                <Anthem:DropDownList ID="ddlMPrefix" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="true" Enabled="true" />
                            </div>
                            <div class="col-sm-8">
                                <Anthem:TextBox ID="R_txtMotherName" Enabled="true" onkeypress="return AllowAlphabet(event)" runat="server" AutoUpdateAfterCallBack="True"
                                    MaxLength="100" CssClass="form-control" TabIndex="2"></Anthem:TextBox>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Date of Birth: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="R_txtPostedDate" Enabled="true" Style="width: 120px !important" runat="server" AutoUpdateAfterCallBack="True" MaxLength="19"
                            onMouseDown="DisableRightClick(event);" onpaste="event.returnValue=false" ondrop="event.returnValue=false"
                            CssClass="form-control" onkeydown="alert('Please Select Date from Calendar');" onkeypress="return false;" TabIndex="16"></Anthem:TextBox>
                        <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_R_txtPostedDate);return false;">
                            <img align="absMiddle" alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                                width="34" />
                        </a>*
                    </div>
                    <label class="col-sm-2 control-label">Email ID: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="E_txtEmail" Enabled="true" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="True" MaxLength="50" TextDuringCallBack="" AutoCallBack="true"
                            ondrop="return false" onpaste="return false" OnTextChanged="E_txtEmail_TextChanged"></Anthem:TextBox>
                        <Anthem:Label ID="lblEmailMsg" ForeColor="Red" runat="server" AutoUpdateAfterCallBack="true"
                            AssociatedControlID="E_txtEmail"></Anthem:Label>*
                    </div>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Mobile No.: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="R_txtContactno" Enabled="true" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" MaxLength="10" TextDuringCallBack="" AutoCallBack="true"
                            ondrop="return false" onpaste="return false" ondrag="return false" OnTextChanged="R_txtContactno_TextChanged"
                            onkeydown="return IntegerOnly(event,this);" TabIndex="11"></Anthem:TextBox>

                        <Anthem:Label ID="lblMobleNoMsg" ForeColor="Red" runat="server" AutoUpdateAfterCallBack="true"
                            AssociatedControlID="R_txtContactno"></Anthem:Label>*
                    </div>
                    <label class="col-sm-2 control-label">Remarks: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="txtremarks" Enabled="true" runat="server" CssClass="form-control" MaxLength="500"
                            TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" onpaste="event.returnValue=false"
                            ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="22"></Anthem:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Presently Working In (Organization/Institution): </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="txtCurrentOccupation" Enabled="true" onkeypress="return AllowAlphabet(event)" ondrop="return false" onpaste="return false" runat="server" AutoUpdateAfterCallBack="True"
                            MaxLength="100" CssClass="form-control" TabIndex="2"></Anthem:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">Designation: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="txtDesignation" Enabled="true" onkeypress="return AllowAlphabet(event)" runat="server" AutoUpdateAfterCallBack="True"
                            MaxLength="100" CssClass="form-control" ondrop="return false" onpaste="return false" TabIndex="3"></Anthem:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Permanent Address: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="txtperadd" Enabled="true" runat="server" AutoUpdateAfterCallBack="True" TabIndex="14"
                            MaxLength="250" CssClass="form-control" TextMode="MultiLine" ondrop="return false"></Anthem:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">Present Address: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="txtCurrentAddress" Enabled="true" runat="server" AutoUpdateAfterCallBack="True"
                            TabIndex="15" MaxLength="250" CssClass="form-control" ondrop="return false" TextMode="MultiLine" onpaste="return false"></Anthem:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Expertise: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="txtsplinterest" Enabled="true" runat="server" CssClass="form-control" MaxLength="500"
                            TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" onpaste="event.returnValue=false"
                            ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="16"></Anthem:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">Outstanding Achievements/ Honor/ Award/Position: </label>
                    <div class="col-sm-4 required">
                        <Anthem:TextBox ID="txtachievement" Enabled="true" runat="server" CssClass="form-control" MaxLength="500"
                            TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" onpaste="event.returnValue=false"
                            ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="17"></Anthem:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Gender: </label>
                    <div class="col-sm-4 required">
                        <Anthem:RadioButtonList ID="rdbGender" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbGender_SelectedIndexChanged" AutoCallBack="true" AutoUpdateAfterCallBack="true" Enabled="true">
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                            <asp:ListItem Text="Others" Value="O"></asp:ListItem>
                        </Anthem:RadioButtonList>
                    </div>
                    <label class="col-sm-2 control-label" style="padding-top: 0px"><%--Mentor:--%> </label>
                    <div class="col-sm-4">
                        <Anthem:CheckBox ID="chkmentor" runat="server" Enabled="false" AutoUpdateAfterCallBack="True" Visible="false" />
                    </div>
                </div>
            </div>

        </div>
    </fieldset>

    <fieldset class="fieldset-border">
        <legend>Special Ability Information </legend>
        <div class="panel-body pnl-body-custom" oncontextmenu="return false;">
            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Special Ability ?  </label>
                    <label class="col-sm-4 control-label custom-text-left">
                        <Anthem:RadioButtonList ID="rdbIsPersonDisability" runat="server" RepeatDirection="Horizontal" AutoCallBack="true" AutoUpdateAfterCallBack="true" TabIndex="19" OnSelectedIndexChanged="rdbIsPersonDisability_SelectedIndexChanged">
                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                        </Anthem:RadioButtonList>
                    </label>
                    <Anthem:Panel ID="isChkDisabilityPnl" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" Visible="false">
                        <label class="col-sm-2 control-label">Is Special Ability More than 40%  ?  </label>
                        <label class="col-sm-4 control-label custom-text-left">
                            <Anthem:CheckBox ID="isChkDisability" runat="server" OnCheckedChanged="isChkDisability_CheckedChanged" AutoUpdateAfterCallBack="true" AutoCallBack="true" TabIndex="20" />
                        </label>
                    </Anthem:Panel>
                </div>
            </div>
            <Anthem:Panel ID="disabilitySection" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" Visible="false">
                <div class="form-group form-group-sm">
                    <div class="row">
                        <div id="disabilityRemarkSection" runat="server" style="display: none;" updateaftercallback="true">
                            <label class="col-sm-2 control-label" for="fileDisabilityDoc">Upload Special Ability Document </label>
                            <div class="col-sm-4">
                                <Anthem:FileUpload ID="fileDisabilityDoc" TabIndex="22" CssClass="form-control" SkinID="none" runat="server" AutoUpdateAfterCallBack="true" onchange="previewFile()" />
                                <Anthem:Button ID="btnDisabilityDoc" runat="server" AutoUpdateAfterCallBack="true" Text="UPLOAD" Visible="true" OnClick="btnDisabilityDoc_Click" EnableCallBack="false" class="logbutt" />
                                <Anthem:Button ID="btnRemoveDD" runat="server" AutoUpdateAfterCallBack="true" Text="REMOVE" Visible="false" OnClick="btnRemoveDD_Click" class="logbutt" />
                                <br />
                                <asp:Label ID="Label2" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                    Text="[file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp, .png and .pdf] file types are supported]" />
                                <br />
                                <br />
                                <asp:Label ID="lblDDDoc" runat="server" AutoUpdateAfterCallBack="true" Visible="false" ForeColor="Blue"></asp:Label>
                                <Anthem:LinkButton ID="lnkDisabilityDoc" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton>
                                <Anthem:HyperLink ID="hyperLinkDDoc" runat="server" Visible="false" AutoUpdateAfterCallBack="true" Target="_blank"></Anthem:HyperLink>

                            </div>
                            <label class="col-sm-2 control-label" for="txtDisabilityRemark">Special Ability Description </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="txtDisabilityRemark" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="21">
                                </Anthem:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

            </Anthem:Panel>
        </div>
    </fieldset>

    <div class="box-body table-responsive">
        <table style="display: none">
            <tr>
                <td class="tdgap" colspan="4"></td>
            </tr>
            <tr>
                <td colspan="6" class="tablesubheading">Login Details
                </td>
            </tr>
            <tr>
                <td class="tdgap" colspan="4"></td>
            </tr>
            <tr>
                <td class="vtext" id="lblLoginName1">Login Name 
                </td>
                <td class="colon">:</td>
                <td class="required">
                    <Anthem:TextBox ID="R_txtLoginName1" runat="server"
                        TextDuringCallBack="Wait..." MaxLength="50" SkinID="textboxlong" AutoCallBack="true"
                        TabIndex="19" Enabled="False"></Anthem:TextBox>*
                </td>
                <td>
                    <Anthem:Label ID="lblloginmsg" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                        AssociatedControlID="R_txtLoginName1"></Anthem:Label></td>
            </tr>
            <tr>
                <td class="vtext" id="lblPassword">Password
                </td>
                <td class="colon">:</td>
                <td class="required">
                    <Anthem:TextBox ID="R_txtPassword" runat="server"
                        MaxLength="30" SkinID="textboxlong" TextMode="Password" TabIndex="20" Enabled="False"></Anthem:TextBox>*
                </td>
            </tr>
            <tr>
                <td style="width: 15%"></td>
                <td colspan="4" style="margin-left: 15px; float: left;">

                    <Anthem:Button ID="btnRegister" Visible="false" OnClick="btnRegister_Click" class="logbutt" runat="server" TextDuringCallBack="UPDATING....." AutoUpdateAfterCallBack="True"
                        Text="REGISTER" PreCallBackFunction="btnRegister_PreCallBack" OnClientClick="return validateEmail();" />
                    <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage"></Anthem:Label>
                </td>
            </tr>
        </table>
    </div>

    <fieldset class="fieldset-border">
        <legend>Education Details </legend>
        <div class="panel-body pnl-body-custom">
            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Highest Degree: </label>
                    <div class="col-sm-4 required">
                        <Anthem:DropDownList ID="ddldegree" runat="server" AppendDataBoundItems="True"
                            TabIndex="7" AutoCallBack="True" AutoUpdateAfterCallBack="true" CssClass="form-control"
                            TextDuringCallBack="" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged">
                        </Anthem:DropDownList>
                    </div>
                    <label class="col-sm-2 control-label">Subject</label>
                    <div class="col-sm-4">
                        <Anthem:DropDownList ID="subjectlist" runat="server" SkinID="dropdawnNowidth" CssClass="form-control" AutoUpdateAfterCallBack="true" TextDuringCallBack="">
                        </Anthem:DropDownList>
                        <span style="color: red">*</span>
                    </div>
                </div>
            </div>
            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Passing Year: </label>
                    <div class="col-sm-4 required">
                        <Anthem:DropDownList ID="D_ddlYeofPass" Enabled="true" runat="server" AppendDataBoundItems="True"
                            TabIndex="7" AutoCallBack="True" AutoUpdateAfterCallBack="true" CssClass="form-control"
                            TextDuringCallBack="">
                        </Anthem:DropDownList>*
                    </div>
                    <label class="col-sm-2 control-label">Department of the University/College/Inst.: </label>
                    <div class="col-sm-4 required">
                        <Anthem:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True"
                            TabIndex="7" AutoCallBack="True" AutoUpdateAfterCallBack="true" CssClass="form-control"
                            TextDuringCallBack="" Enabled="true">
                        </Anthem:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>

    <fieldset class="fieldset-border">
        <legend>Documents Details </legend>
        <div class="panel-body pnl-body-custom">
            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Upload Photo: </label>
                    <div class="col-sm-10">
                        <asp:Image ID="imgProfileimg" Enabled="false" runat="server" ImageUrl="~/Online/NoImage/default-icon.jpg"
                            Width="100px" Height="100px" /><span style="color: red">*</span>
                        <asp:FileUpload ID="flUpload" Enabled="true" CssClass="form-control" Style="width: 30% !important;" SkinID="none" runat="server" onchange="previewFile()" />
                        <Anthem:Button ID="btnImgUpload" runat="server" AutoUpdateAfterCallBack="true" Text="UPLOAD" Visible="true" OnClick="btnImgUpload_Click" EnableCallBack="false" class="logbutt" />
                        <Anthem:Button ID="btnRemovePP" runat="server" AutoUpdateAfterCallBack="true" Text="REMOVE" Visible="false" OnClick="btnRemovePP_Click" EnableCallBack="false" class="logbutt" />
                        <br />
                        <asp:Label ID="lblPhoto" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Text="[Photo (Passport) file size should not be more than 1 MB and supported file types are .jpg, .jpeg, .bmp and .png]" /><br />
                        <br />
                        <asp:Label ID="lblPPDoc" runat="server" AutoUpdateAfterCallBack="true" Visible="false" ForeColor="Blue"></asp:Label>
                        <Anthem:HyperLink ID="hyperLinkProfile" runat="server" Visible="false" AutoUpdateAfterCallBack="true" Target="_blank"></Anthem:HyperLink>

                        <Anthem:LinkButton ID="lnkprofile" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton>
                        <asp:TextBox ID="hdPath" runat="server" AutoUpdateAfterCallBack="true" BorderStyle="None" Width="0px" Height="0px" BorderWidth="0px" /><br />
                    </div>
                </div>
            </div>

            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Upload Your Documents(HPU Document): </label>
                    <div class="col-sm-10 required">
                        <asp:Panel ID="Panel4" Enabled="true" runat="server" Visible="true" AutoUpdateAfterCallBack="true" onchange="previewFile()" EnabledDuringCallBack="false">
                            <asp:FileUpload ID="uploadDocuments" Enabled="true" CssClass="form-control" Style="width: 30% !important;" SkinID="none" runat="server" AutoUpdateAfterCallBack="true" />
                            <Anthem:Button ID="btnDocUpload" runat="server" AutoUpdateAfterCallBack="true" Text="UPLOAD" Visible="true" OnClick="btnDocUpload_Click" EnableCallBack="false" class="logbutt" />
                            <Anthem:Button ID="btnRemoveAD" runat="server" AutoUpdateAfterCallBack="true" Text="REMOVE" Visible="false" OnClick="btnRemoveAD_Click" EnableCallBack="false" class="logbutt" />
                            <br />
                            <asp:Label ID="LblDoc" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                Text="[Document file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp, .png and .pdf]" />
                            <br />
                            <br />
                            <asp:Label ID="lblADDoc" runat="server" AutoUpdateAfterCallBack="true" Visible="false" ForeColor="Blue"></asp:Label>
                            <Anthem:LinkButton ID="lnkDoc" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton><br />
                            <span id="sp_uploadDocuments" runat="server" style="color: red"></span>
                            <asp:Label ID="lblDCMsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></asp:Label>
                            <span id="Span7" runat="server" visible="false" style="color: red"></span>
                            <a target="_blank" id="anchorPathDC" runat="server"></a>
                            <Anthem:HyperLink ID="hyperLinkADoc" runat="server" Visible="false" AutoUpdateAfterCallBack="true" Target="_blank"></Anthem:HyperLink>
                        </asp:Panel>
                    </div>
                </div>
            </div>

        </div>
    </fieldset>

    <fieldset class="fieldset-border">
        <legend>Lists of Uploaded Documents </legend>
        <div class="panel-body pnl-body-custom">

            <div class="form-group form-group-sm">
                <div class="row">
                    <div id="gvDivFile">
                        <label class="col-sm-2 control-label" for="Files"></label>
                        <div class="col-sm-10">
                            <Anthem:GridView runat="server" ID="GrdFile" Visible="false" AutoGenerateColumns="false" OnRowCommand="GrdFile_RowCommand" AutoUpdateAfterCallBack="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Uploaded Documents">
                                        <ItemTemplate>
                                            <Anthem:Label ID="lnkDoc" runat="server" Text='<%# Bind("lblDocs") %>' CommandName="Download"></Anthem:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document's Name">
                                        <ItemTemplate>
                                            <Anthem:Label ID="lnkDownload" runat="server" Text='<%# Bind("Files_Name") %>' CommandName="Download"></Anthem:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View File">
                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <a target="_blank" id="anchorPath" href='<%#SetServiceDoc(Eval("Files_Unique_Name").ToString()) %>'>View </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DELETE">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <Anthem:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Are you sure to delete this record?');" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="DELETEREC" CommandArgument='<%# Eval("Pk_FileId") %>'> <img src="../../Images/Remove.jpeg" alt="" border="0"></img></Anthem:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </Anthem:GridView>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </fieldset>
    <div class="" align="center">
        <div class="">
            <div class="row">
                <Anthem:Button ID="btnSubmit" runat="server" AutoUpdateAfterCallBack="true" Text="SUBMIT" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-sm" />
                <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnback_Click" CssClass="btn btn-primary btn-sm" />
                <Anthem:Label ID="lblOnlineFees" runat="server" AutoUpdateAfterCallBack="true" Visible="false"></Anthem:Label>
                <Anthem:HiddenField ID="hdnId" runat="server" AutoUpdateAfterCallBack="true" Visible="false"></Anthem:HiddenField>
            </div>
        </div>
    </div>

    <fieldset class="fieldset-border" style="visibility: hidden;">
        <legend>Transaction Details </legend>
        <div class="panel-body pnl-body-custom">
            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Payment Mode: </label>
                    <div class="col-md-4">
                        <Anthem:Label ID="lblMsgPMode" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                    </div>
                    <label class="col-sm-2 control-label">Amount: </label>
                    <div class="col-md-4 ">
                        <Anthem:Label ID="lblMsgPAmount" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                    </div>
                </div>
            </div>
            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Transaction No.: </label>
                    <div class="col-md-4 ">
                        <Anthem:Label ID="lblTransID" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                    </div>
                    <label class="col-sm-2 control-label">Transaction Status: </label>
                    <div class="col-md-4 ">
                        <Anthem:Label ID="lblTransStatus" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>

    <fieldset class="fieldset-border" style="visibility: hidden;">
        <legend>Action Taken </legend>
        <div class="panel-body pnl-body-custom">
            <div class="form-group form-group-sm">
                <div class="row">
                    <label class="col-sm-2 control-label">Action</label>
                    <div class="col-sm-10">
                        <Anthem:RadioButtonList ID="rbtnlst_approveRej" runat="server" OnSelectedIndexChanged="rbtnlst_approveRej_SelectedIndexChanged" AutoCallBack="true" AutoUpdateAfterCallBack="true" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                        </Anthem:RadioButtonList>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>