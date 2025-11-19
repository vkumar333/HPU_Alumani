<%@ Page Language="C#" MasterPageFile="~/Alumni/Alm_registration_master.master" AutoEventWireup="true" CodeFile="ALM_AlumniRegistration.aspx.cs"
    Inherits="Alumni_ALM_AlumniRegistration" Title="Alumni Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="Javascript" type="text/javascript">

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32)
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        function onlyNumbers(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
		
        function FillAddressInput() {
            debugger;
            let checkBox = document.getElementById('ctl00_ContentPlaceHolder1_permanentaddchk');
            let pAddressLine1 = document.getElementById("ctl00_ContentPlaceHolder1_txtperadd");

            let curAddressLine1 = document.getElementById("ctl00_ContentPlaceHolder1_txtCurrentAddress");

            if (ctl00_ContentPlaceHolder1_permanentaddchk.checked == true) {
                let pAddressLine1Value = pAddressLine1.value;
                curAddressLine1.value = pAddressLine1Value;
            }
            else {
                curAddressLine1.value = "";
            }
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

            var Mnth = 0;
            var Dte = 0;
            var dobtxt = document.getElementById('ctl00_ContentPlaceHolder1_R_txtPostedDate').value;
            if (dobtxt != "") {
                var GetDate = dobtxt.split('/')[0];
                var GetMonth = (dobtxt.split('/')[1]) - 1;// as month start from 0 and end to 11
                var GetYear = dobtxt.split('/')[2];
                var current = new Date();
                var d1 = new Date(GetYear, GetMonth, GetDate);  // get the selected dob

                Mnth = 6; //// put here 1 st july of current year as month start from 0 to 11 so 6 is the month of july
                Dte = 1;
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

    </script>

    <style>
        .alumnilohinpagelink a {
            color: #fff;
            font-size: 13px;
            font-weight: bold;
            margin-top: 0;
        }

        .chzn-container, .chzn-select.form-control {
            width: 300px !important;
        }

        .fieldset-border {
            border: solid 1px #dbdbdb;
            padding: 10px;
            margin: 0 0 15px 0;
        }

        .form-control {
            display: inline-block !important;
            width: 94%;
        }

        .alert-warning {
            width: 88%;
        }

        table.radio {
            padding-top: 0 !important;
        }

        .fieldset-border legend {
            font-weight: 600;
        }
    </style>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="Stylesheet" />

    <script type="text/javascript">

        var a = jQuery.noConflict();
        a(document).ready(BindEvents);

        function BindEvents() {
            try {
                a(".ChosenSelector").chosen();
            }
            catch (err) {
            }
        }

        $(".ChosenSelector").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true })

        function toggleDisabilityFields() {
            var alumniRadio = document.getElementById('<%= rdbIsPersonDisability.ClientID %>');
            var disabilitySection = document.getElementById('<%= disabilitySection.ClientID %>');
            if (alumniRadio.checked) {
                disabilitySection.style.display = 'block';
            } else {
                disabilitySection.style.display = 'none';
            }
        }

        function toggleDisabilityRemark() {
            var disabilityCheckbox = document.getElementById('<%= isChkDisability.ClientID %>');
            var disabilityRemarkSection = document.getElementById('<%= disabilityRemarkSection.ClientID %>');
            if (disabilityCheckbox.checked) {
                disabilityRemarkSection.style.display = 'block';
            } else {
                disabilityRemarkSection.style.display = 'none';
            }
        }

    </script>

    <div class="col-md-12" oncontextmenu="return false;">
        <div class="row">
            <div class="box box-success">
                <img class="img-responsive img-rounded" style="width: 100%;" src="../images/alumni-bannerr.jpg" />
                <div class="">
                    <%--<div class="boxhead" style="margin-top:10px;">
                    Alumni Registration
                </div>--%>

                    <fieldset class="fieldset-border" style="margin-top: 10px;" oncontextmenu="return false;">
                        <legend>Profile Details </legend>

                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">Membership Type : </label>
                                <label class="col-md-9 control-label custom-text-left-alumni-registration">
                                    <Anthem:RadioButtonList ID="rdalumnitype" runat="server" CssClass="form-control" RepeatDirection="Vertical" AutoCallBack="true" OnSelectedIndexChanged="rdalumnitype_SelectedIndexChanged" AutoUpdateAfterCallBack="true" Enabled="false">
                                        <asp:ListItem Value="LM"><%--<b style="font:500">Life Membership(2000)</b>--%>
                                            <div class="alert alert-warning" style="margin-top:5px;">
                                                <strong>Life-time Membership(₹ 2000)</strong> Those who have obtained their degree from Himanchal Pradesh University (which includes Faculty, Employees, and Alumni spread over all of the world)
                                            </div>
                                        </asp:ListItem>
                                        <asp:ListItem Value="SM"><%--<b style="font:500">Student Membership(1000)</b>--%>
                                            <div class="alert alert-warning" style="margin-top:5px;">
                                                <strong>Student Membership(₹ 1000)</strong> Currently pursuing a Degree (SRF, JRF, Ph.D. & Equivalent Courses) from Himachal Pradesh University.
                                            </div>
                                        </asp:ListItem>
                                        <asp:ListItem Value="SC"><%--<b style="font:500">Student Chapter Membership(100)</b>--%>
                                            <div class="alert alert-warning" style="margin-top:5px;">
                                                <strong>Student Chapter Membership(₹ 100)</strong> for students currently pursuing any degree (UG & PG) from Himachal Pradesh University.
												<br /><br />
                                                <span style="color: red;">Note:- This membership shall be applicable for only five (5) years.</span>
                                            </div>
                                        </asp:ListItem>
                                    </Anthem:RadioButtonList>
                                </label>
                            </div>
                        </div>
						
                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">Alumni Name : </label>
                                <div class="col-sm-3">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <Anthem:DropDownList ID="Drp_Alumni_Name" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoUpdateAfterCallBack="true" TabIndex="1" TextDuringCallBack="" />
                                        </div>
                                        <div class="col-sm-8">
                                            <Anthem:TextBox ID="txtAlumniName" onkeypress="return CheckMaxLength(this,100);" runat="server"
                                                MaxLength="100" CssClass="form-control" TabIndex="2" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                            <span style="color: red">*</span>
                                        </div>
                                    </div>
                                </div>
                                <label class="col-sm-2 control-label">Email : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="E_txtEmail" runat="server" AutoUpdateAfterCallBack="True" MaxLength="50" TextDuringCallBack="" AutoCallBack="true"
                                        CssClass="form-control" TabIndex="8" ondrop="return false" OnTextChanged="E_txtEmail_TextChanged" onkeypress="return CheckMaxLength(this,49);">
                                    </Anthem:TextBox>
                                    <span style="color: red">*</span>
                                    <Anthem:Label ID="lblEmailMsg" runat="server" Width="276px" AutoUpdateAfterCallBack="true" SkinID="lblmessage"></Anthem:Label>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">Date of Birth : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="R_txtPostedDate" Style="width: 120px" Enabled="true" runat="server" AutoUpdateAfterCallBack="True" MaxLength="19"
                                        CssClass="form-control" TabIndex="7" ondrop="return false" onkeydown="return false;" ondrag="return false;" onkeypress="return false;"></Anthem:TextBox>
                                    <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_R_txtPostedDate);return false;">
                                        <img align="absMiddle" alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>' width="34" />
                                    </a>
                                    <span style="color: red">*</span>
                                    <br />
                                    <%--<Anthem:Label ID="lbldob" SkinID="lblmessage" runat="server" ForeColor="Red" Text="Alumni age should be greater than 18 years as per Passing year!"></Anthem:Label>--%>
                                </div>
                                <label class="col-sm-2 control-label">Mobile No. : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="R_txtContactno" runat="server" AutoUpdateAfterCallBack="True" MaxLength="10" TextDuringCallBack="" AutoCallBack="true" CssClass="form-control" ondrop="return false" onkeydown="return IntegerOnly(event,this);" ondrag="return false" OnTextChanged="R_txtContactno_TextChanged" TabIndex="9" onkeypress="return CheckMaxLength(this,10);"></Anthem:TextBox>
                                    <span style="color: red">*</span>
                                    <Anthem:Label ID="lblMobleNoMsg" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"></Anthem:Label>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">Permanent Address : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtperadd" runat="server" AutoUpdateAfterCallBack="True" TabIndex="8" MaxLength="250" CssClass="form-control" OnTextChanged="txtperadd_TextChanged" AutoCallBack="true" onkeypress="return CheckMaxLength(this,250);" TextMode="MultiLine">
                                    </Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Gender : </label>
                                <label class="col-sm-3 control-label custom-text-left-alumni-registration">
                                    <Anthem:RadioButtonList ID="rdbGender" runat="server" RepeatDirection="Horizontal" AutoUpdateAfterCallBack="true" TabIndex="7">
                                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                    </Anthem:RadioButtonList>
                                    <Anthem:CheckBox ID="chkmentor" runat="server" AutoUpdateAfterCallBack="True" Visible="false" />
                                </label>                               
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">Present Address : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtCurrentAddress" runat="server" AutoUpdateAfterCallBack="True" AutoCallBack="true" TabIndex="9" MaxLength="250" CssClass="form-control" OnTextChanged="txtCurrentAddress_TextChanged" onkeypress="return CheckMaxLength(this,250);" TextMode="MultiLine">
                                    </Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Same as Permanent Address : </label>
                                <label class="col-sm-3 control-label custom-text-left-alumni-registration">
                                    <Anthem:CheckBox ID="permanentaddchk" runat="server" AutoUpdateAfterCallBack="True" OnCheckedChanged="permanentaddchk_CheckedChanged" AutoCallBack="true" TabIndex="10" />
                                </label>  
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">
                                    Presently Working In
                                    <br />
                                    (Organization/Institution) :
                                </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtCurrentOccupation" runat="server" AutoUpdateAfterCallBack="True"
                                        MaxLength="100" CssClass="form-control" onkeypress="return AllowAlphabet(event)" TabIndex="16" ondrop="return false"></Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Designation : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtDesignation" runat="server" AutoUpdateAfterCallBack="True"
                                        MaxLength="100" CssClass="form-control" onkeypress="return CheckMaxLength(this,100);" TabIndex="10" ondrop="return false"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">Present Location : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtLocation" runat="server" CssClass="form-control" MaxLength="25" onkeypress="return CheckMaxLength(this,25);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" placeholder="e.g., Shimla"></Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Country : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtCountry" runat="server" CssClass="form-control" MaxLength="25" onkeypress="return CheckMaxLength(this,25);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" placeholder="e.g., India"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">Outstanding Achievements/ Honor/ Award/Position : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtachievement" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="15"></Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Expertise : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtsplinterest" runat="server" Width="276px" CssClass="form-control" MaxLength="250" TextMode="MultiLine" onkeypress="return CheckMaxLength(this,250);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="14"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>
						
                        <div class="form-group form-group-sm">
                            <div class="">
                                <label class="col-sm-3 control-label">Remarks : </label>
                                <div class="col-sm-3">
                                    <Anthem:TextBox ID="txtremarks" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="17"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>

                    </fieldset>
                </div>

                <!-- Disability Information Section -->
                <fieldset class="fieldset-border" oncontextmenu="return false;">
                    <legend>Special Ability Information </legend>

                    <div class="form-group form-group-sm">
                        <div class="">
                            <label class="col-sm-3 control-label">Special Ability ? : </label>
                            <label class="col-sm-3 control-label custom-text-left-alumni-registration">
                                <Anthem:RadioButtonList ID="rdbIsPersonDisability" runat="server" RepeatDirection="Horizontal" AutoCallBack="true" AutoUpdateAfterCallBack="true" TabIndex="19" OnSelectedIndexChanged="rdbIsPersonDisability_SelectedIndexChanged">
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                </Anthem:RadioButtonList>
                            </label>
                            <Anthem:Panel ID="isChkDisabilityPnl" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" Visible="false">
                                <label class="col-sm-2 control-label">Is Special Ability More than 40%  ? :  </label>
                                <label class="col-sm-3 control-label custom-text-left-alumni-registration">
                                    <Anthem:CheckBox ID="isChkDisability" runat="server" OnCheckedChanged="isChkDisability_CheckedChanged" AutoUpdateAfterCallBack="true" AutoCallBack="true" TabIndex="20" />
                                </label>
                            </Anthem:Panel>
                        </div>
                    </div>

                    <Anthem:Panel ID="disabilitySection" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" Visible="false">
                        <div class="form-group form-group-sm">
                            <div class="">
                                <div id="disabilityRemarkSection" runat="server" style="display: none;" updateaftercallback="true">
                                    <label class="col-sm-3 control-label" for="fileDisabilityDoc">Upload Special Ability Document : </label>
                                    <div class="col-sm-3">
                                        <Anthem:FileUpload ID="fileDisabilityDoc" TabIndex="22" SkinID="none" runat="server" AutoUpdateAfterCallBack="true" onchange="previewFile()" />
                                        <br />
                                        <Anthem:Button ID="btnDisabilityDoc" runat="server" AutoUpdateAfterCallBack="true" Text="UPLOAD" Visible="true" OnClick="btnDisabilityDoc_Click" EnableCallBack="false" class="logbutt" />
                                        <br />
                                        <asp:Label ID="Label1" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                            Text="[file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp .png and .pdf] file types are supported]" />
                                        <br />
                                        <Anthem:HyperLink ID="hyperDisabilityDoc" runat="server" Target="_blank" Visible="false" AutoUpdateAfterCallBack="true"></Anthem:HyperLink>

                                    </div>
                                    <label class="col-sm-2 control-label" for="txtDisabilityRemark">Special Ability Description : </label>
                                    <div class="col-sm-3">
                                        <Anthem:TextBox ID="txtDisabilityRemark" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="21">
                                        </Anthem:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Anthem:Panel>

                </fieldset>

                <!-- Education Details -->
                <%--<fieldset class="fieldset-border">
                    <legend>Education Details </legend>
                    <Anthem:Panel ID="panelDegreeAndSubject" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True">
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-3 control-label">Highest Degree Form the University </label>
                                <div class="col-sm-3">
                                    <Anthem:DropDownList ID="ddldegree" runat="server" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged" AutoCallBack="true" AutoUpdateAfterCallBack="true" CssClass="ChosenSelector" Style="width: 95% !important;" TabIndex="19">
                                    </Anthem:DropDownList>
                                    <span style="color: red">*</span>
                                    <Anthem:TextBox ID="txtdegree" runat="server" AutoUpdateAfterCallBack="true" Visible="false" CssClass="form-control" onkeypress="return onlyAlphabets   (event,this);"></Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Subject </label>
                                <div class="col-sm-3">
                                    <Anthem:DropDownList ID="subjectlist" runat="server" CssClass="ChosenSelector" AutoUpdateAfterCallBack="true" TextDuringCallBack="" Style="width: 95% !important;" TabIndex="20"></Anthem:DropDownList>
                                    <span style="color: red">*</span>
                                </div>
                            </div>
                        </div>
                    </Anthem:Panel>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-3 control-label">Passing Year </label>
                            <div class="col-sm-3">
                                <Anthem:DropDownList ID="D_ddlYeofPass" runat="server" AutoCallBack="false" CssClass="ChosenSelector" AutoUpdateAfterCallBack="true" Style="width: 95% !important;" TabIndex="21">
                                </Anthem:DropDownList>
                                <span style="color: red">*</span>
                            </div>
                            <label class="col-sm-2 control-label">Department of the University/College/Institution</label>
                            <div class="col-sm-3 ">
                                <Anthem:DropDownList ID="ddlDepartment" AutoUpdateAfterCallBack="true" AutoCallBack="false" CssClass="ChosenSelector" runat="server" Style="width: 95% !important;" TabIndex="22">
                                </Anthem:DropDownList>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                    </div>

                </fieldset>--%>

                <fieldset class="fieldset-border" oncontextmenu="return false;">
                    <legend>Education Details</legend>
                    <div class="form-group">
                        <div>
                            <div class="">
                                <Anthem:GridView ID="gvQualifications" runat="server" AutoGenerateColumns="False" Width="100%" AutoUpdateAfterCallBack="true"
                                    PageSize="10" AllowPaging="true" ShowHeader="True" OnRowCommand="gvQualifications_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Degree" HeaderStyle-Width="25%">
                                            <ItemTemplate>
                                                <Anthem:DropDownList ID="ddldegree" runat="server"
                                                    CssClass="ChosenSelector"
                                                    AutoCallBack="true"
                                                    AutoUpdateAfterCallBack="true"
                                                    OnSelectedIndexChanged="ddldegree_SelectedIndexChanged"
                                                    Style="width: 95% !important;" TabIndex="19">
                                                </Anthem:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject" HeaderStyle-Width="25%">
                                            <ItemTemplate>
                                                <Anthem:DropDownList ID="subjectlist" runat="server"
                                                    CssClass="ChosenSelector"
                                                    AutoUpdateAfterCallBack="true"
                                                    Style="width: 95% !important;" TabIndex="20">
                                                </Anthem:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Passing Year" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <Anthem:DropDownList ID="D_ddlYeofPass" runat="server"
                                                    CssClass="ChosenSelector"
                                                    AutoCallBack="false"
                                                    AutoUpdateAfterCallBack="true"
                                                    Style="width: 95% !important;" TabIndex="21">
                                                </Anthem:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department" HeaderStyle-Width="25%">
                                            <ItemTemplate>
                                                <Anthem:DropDownList ID="ddlDepartment" runat="server"
                                                    CssClass="ChosenSelector"
                                                    AutoCallBack="false"
                                                    AutoUpdateAfterCallBack="true"
                                                    Style="width: 95% !important;" TabIndex="22">
                                                </Anthem:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="EDIT">
                                            <ItemTemplate>
                                                <Anthem:LinkButton ID="lnkEdit" runat="server" Enabled="true" CommandName="SELECT" CausesValidation="False" AutoUpdateAfterCallBack="true" CommandArgument="<%# Container.DataItemIndex+1 %>" Text="&lt;img src=&quot;../Images/Edit.gif&quot; alt=&quot;&quot; border=&quot;0&quot;&gt;&lt;/img&gt;">
                                                </Anthem:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="DELETE" HeaderStyle-Width="5%">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <Anthem:LinkButton ID="lnkDelete" runat="server" AutoUpdateAfterCallBack="True" CausesValidation="False" CommandArgument="<%# Container.DataItemIndex + 1%>" CommandName="DELETEREC" TextDuringCallBack="Deleting..." PreCallBackFunction="btnDelete_PreCallBack">
                                                    <img src="../Images/Delete.gif" alt="" border="0" />
                                                </Anthem:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </Anthem:GridView>
                            </div>
                        </div>
                    </div>

                    <!-- Add More Button -->
                    <div class="form-group text-right" style="margin-top: 15px; margin-right: 10px;">
                        <Anthem:Button ID="btnAddRow" runat="server" CssClass="btn btn-success"
                            Text="+ Add More" CommandName="ADD" OnClick="btnAddRow_Click" AutoUpdateAfterCallBack="true" />
                    </div>

                </fieldset>

                <div id="dvMsg" runat="server" class="row" style="display: none;">
                    <div class="col-md-10 col-sm-offset-2" id="divmgs">
                        <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage"></Anthem:Label>
                    </div>
                    <div class="col-md-10 col-sm-offset-2">
                        <Anthem:Label Style="color: red;" ID="lblUserID" runat="server" AutoUpdateAfterCallBack="true" Font-Bold="true" Font-Size="14px"></Anthem:Label>
                        <Anthem:Label ID="lblPassword" Style="color: red;" runat="server" AutoUpdateAfterCallBack="true" Font-Bold="true" Font-Size="14px" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-9 col-sm-offset-3">
                        <Anthem:Label ID="lblSmsEmailMsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                    </div>
                </div>

                <!-- Upload Documents -->
                <fieldset class="fieldset-border" oncontextmenu="return false;">
                    <legend>Upload Documents </legend>

                    <div class="form-group form-group-sm">
                        <div class="">
                            <label class="col-sm-3 control-label">Upload Photo : </label>
                            <div class="col-sm-3 required">
                                <Anthem:FileUpload ID="flUpload" TabIndex="19" SkinID="none" runat="server" AutoUpdateAfterpostBack="true" onchange="previewFile()" />
                                <br />
                                <Anthem:Button ID="btnImgUpload" runat="server" AutoUpdateAfterCallBack="true" Text="UPLOAD" Visible="true" OnClick="btnImgUpload_Click" EnableCallBack="false" class="logbutt" />
                                <br />
                                <span style="color: red;">*</span>
                                <asp:Label ID="lblPhoto" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="color:#ff0000!important;"
                                    Text="[Photo (Passport) file size should not be more than 1 MB and supported file types are .jpg, .jpeg, .bmp and .png]" />
                                <Anthem:HyperLink ID="anchorPath" runat="server" Target="_blank" Visible="false" AutoUpdateAfterCallBack="true"></Anthem:HyperLink>
                            </div>
                            <label class="col-sm-2 control-label"></label>
                            <div class="col-sm-3">
                                <asp:Image ID="imgProfileimg" runat="server" ImageUrl="~/Online/NoImage/default-icon.jpg" Width="100px" Height="100px" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="">
                            <label class="col-sm-3 control-label">Upload Your Documents (HPU Document) : </label>
                            <div class="col-sm-3">
                                <Anthem:FileUpload ID="uploadDocuments" TabIndex="20" SkinID="none" runat="server" AutoUpdateAfterCallBack="true" />
                                <br />
                                <Anthem:Button ID="btnDocUpload" runat="server" AutoUpdateAfterCallBack="true" Text="UPLOAD" Visible="true" OnClick="btnDocUpload_Click" EnableCallBack="false" class="logbutt" />
                                <br />
                                <asp:Label ID="LblDoc" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage" Style="color:#ff0000!important;"
                                    Text="[file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp, .png and .pdf] file types are supported]" />
                                <asp:TextBox ID="hdExservicemanPath" runat="server" AutoUpdateAfterCallBack="true" BorderStyle="None" Width="0" Height="0px" BorderWidth="0px" /><br />
                                <span id="sp_Exserviceman" runat="server" style="color: red"></span>
                                <asp:Label ID="lblExMsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></asp:Label>
                                <span id="Span5" style="color: red"></span>
                                <a target="_blank" id="anchorPathEX" runat="server"></a>

                                <Anthem:HyperLink ID="HyperLink1" runat="server" Target="_blank" Visible="false" AutoUpdateAfterCallBack="true"></Anthem:HyperLink>

                            </div>
                        </div>
                    </div>

                    <Anthem:Panel ID="disabilityDocuments" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" Visible="false">
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <div id="divDisabilityDoc" runat="server" style="display: none;" updateaftercallback="true">
                                </div>
                            </div>
                        </div>
                    </Anthem:Panel>

                </fieldset>

                <!-- Fee Details -->
                <fieldset class="fieldset-border" oncontextmenu="return false;">
                    <legend>Fee Details </legend>

                    <Anthem:RadioButtonList ID="rdPaymentOption" runat="server" Visible="false" RepeatDirection="Horizontal" AutoPostBack="true" AutoUpdateAfterCallBack="true" EnableCallBack="false" OnSelectedIndexChanged="rdPaymentOption_SelectedIndexChanged" Height="22px">
                        <asp:ListItem Value="O"> <b>Online</b></asp:ListItem>
                    </Anthem:RadioButtonList>

                    <Anthem:Panel ID="pnlOnlinePayment" runat="server" AutoUpdateAfterCallBack="true">
                        <div id="Table2">
                            <div class="form-group form-group-sm">
                                <div class="">
                                    <label class="col-sm-3 control-label">
                                        Details of Fee Deposited
                                                        <br />
                                        शुल्क का विवरण जमा</label>
                                    <div class="col-sm-3">
                                        <div class="fee-dtl">
                                            <Anthem:Label ID="lblOnlineFees" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                            <asp:HiddenField ID="hdnId" runat="server"></asp:HiddenField>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group form-group-sm">
                                <div class="">
                                    <div class="col-sm-9 col-sm-offset-3">
                                        <Anthem:Label ID="lblPaymentMsg" runat="server" AutoUpdateAfterCallBack="true" Font-Bold="true" ForeColor="Red" Text=""></Anthem:Label>
                                        <Anthem:Button ID="btnpayment" class="logbutt" Enabled="true" runat="server" AutoUpdateAfterCallBack="True"
                                            Text="PROCEED" Width="80px" OnClick="btnpayment_Click" TabIndex="100" Visible="false" />
                                    </div>
                                    <div class="form-group form-group-sm">
                                        <div class="">
                                            <div class="col-sm-10 col-sm-offset-2">
                                                <Anthem:Button ID="btnRegister" CssClass="logbutt" ValidationGroup="" Text="REGISTER AND PAY" TabIndex="22" EnableCallBack="false" runat="server" OnClick="btnRegister_Click" Enabled="true" />
                                                <Anthem:Button ID="btnReset" runat="server" CausesValidation="false" OnClick="btnReset_Click" EnableCallBack="false"
                                                    Text="RESET" class="logbutt" TextDuringCallBack="Reseting.." TabIndex="23" AutoUpdateAfterCallBack="true" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Anthem:Panel>

                </fieldset>

            </div>
        </div>
    </div>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;">
    </iframe>
    <script type="text/javascript">

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        function Confirm() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
</asp:Content>