<%@ Page Language="C#" MasterPageFile="~/Alumni/PendingAlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_AlumniProfileDetails.aspx.cs" Inherits="Alumni_ALM_AlumniProfileDetails" Title="Pending Alumni Profile Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("#ctl00_ContentPlaceHolder1_R_txtPassword").attr("type", type);
            });
        });
    </script>
    <script src='<%=Request.ApplicationPath%>/include/CommonJS.js' type="text/javascript"></script>
    <script src='<%=Request.ApplicationPath%>/include/jquery.min.js' type="text/javascript"></script>
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
    </script>

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

    </script>
    <style>
        input[type="submit"], input[type="button"] {
            background: #016773;
            background-color: #016773;
            border: 1px solid #016773;
            color: #FFFFFF;
            cursor: pointer;
            font-weight: 600;
            padding: 5px 6px;
            border-radius: 5px 5px 5px 5px;
        }
    </style>
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">

                <div class="boxhead">
                    Alumni Profile
                </div>

                <div class="panel-body pnl-body-custom">

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Membership Type</label>
                            <div class="col-md-10">
                                <Anthem:RadioButtonList ID="rbdAlumniMemType" runat="server" CssClass="form-control" RepeatDirection="Vertical" AutoCallBack="true" OnSelectedIndexChanged="rbdAlumniMemType_SelectedIndexChanged" AutoUpdateAfterCallBack="true" Enabled="false">
                                    <asp:ListItem Value="LM">
                                  <div class="alert alert-warning" style="margin-top:5px;">
                                    <strong>Life Membership(2000)</strong> Those who have obtained their degree from Himanchal Pradesh University (which includes Faculty, Employees, and Alumni spread over all of the world)
                                  </div>
                                    </asp:ListItem>

                                    <asp:ListItem Value="SM">
                                        <div class="alert alert-warning" style="margin-top:5px;">
                                    <strong>Student Membership(1000)</strong> Currently pursuing a Degree from Himanchal Pradesh University
                                 
                                </div>
                                    </asp:ListItem>
                                </Anthem:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel-body pnl-body-custom">

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Alumni Registration No.</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="R_txtAlumnino" runat="server" AutoUpdateAfterCallBack="True"
                                    MaxLength="100" CssClass="form-control" Enabled="false" Font-Bold="true"></Anthem:TextBox>*
                            </div>
                            <%--<label class="col-sm-2 control-label">Alumni Name</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txtAlumniName" onkeypress="return AllowAlphabet(event)" runat="server"
                                    MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true"></Anthem:TextBox>*
                            </div>--%>

                            <label class="col-sm-2 control-label">Alumni Name:</label>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <Anthem:DropDownList ID="Drp_Alumni_Name" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoUpdateAfterCallBack="true"
                                            TextDuringCallBack="" TabIndex="1" />
                                    </div>
                                    <div class="col-sm-8">
                                        <Anthem:TextBox ID="txtAlumniName" onkeypress="return AllowAlphabet(event)" runat="server"
                                            MaxLength="100" CssClass="form-control" TabIndex="2" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                        <span style="color: red">*</span>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Father's Name</label>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <Anthem:DropDownList ID="Drp_FatherName" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoUpdateAfterCallBack="true"
                                            TextDuringCallBack="" TabIndex="3" />
                                    </div>
                                    <div class="col-sm-8">
                                        <Anthem:TextBox ID="R_txtFatherName" onkeypress="return AllowAlphabet(event)" runat="server" AutoUpdateAfterCallBack="True"
                                            MaxLength="100" CssClass="form-control" ondrop="return false" TabIndex="4"></Anthem:TextBox>
                                        <span style="color: red">*</span>
                                    </div>
                                </div>
                            </div>
                            <label class="col-sm-2 control-label">Mother's Name</label>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <Anthem:DropDownList ID="Drp_MotherName" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoUpdateAfterCallBack="true"
                                            TextDuringCallBack="" TabIndex="5" />
                                    </div>
                                    <div class="col-sm-8">
                                        <Anthem:TextBox ID="R_txtMotherName" onkeypress="return AllowAlphabet(event)" runat="server" AutoUpdateAfterCallBack="True"
                                            MaxLength="100" CssClass="form-control" TabIndex="6"></Anthem:TextBox>
                                        <span style="color: red">*</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Date of Birth</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="R_txtPostedDate" Style="width: 120px !important" Enabled="true" runat="server" AutoUpdateAfterCallBack="True" MaxLength="19"
                                    onMouseDown="DisableRightClick(event);" onpaste="event.returnValue=false" ondrop="event.returnValue=false"
                                    CssClass="form-control" onkeydown="alert('Please Select Date from Calendar');" onkeypress="return false;" TabIndex="7"></Anthem:TextBox>
                                <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_R_txtPostedDate);return false;">
                                    <img align="absMiddle" alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                                        width="34" />
                                </a>*
                            </div>
                            <label class="col-sm-2 control-label">Email ID</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="E_txtEmail" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="True" MaxLength="50" TextDuringCallBack="" AutoCallBack="true"
                                    ondrop="return false" OnTextChanged="E_txtEmail_TextChanged" TabIndex="8"></Anthem:TextBox>
                                <Anthem:Label ID="lblEmailMsg" ForeColor="Red" runat="server" AutoUpdateAfterCallBack="true"
                                    AssociatedControlID="E_txtEmail"></Anthem:Label>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Mobile No.</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="R_txtContactno" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" MaxLength="10" TextDuringCallBack="" AutoCallBack="true"
                                    ondrop="return false" ondrag="return false" OnTextChanged="R_txtContactno_TextChanged"
                                    onkeydown="return IntegerOnly(event,this);" TabIndex="9"></Anthem:TextBox>

                                <Anthem:Label ID="lblMobleNoMsg" ForeColor="Red" runat="server" AutoUpdateAfterCallBack="true"
                                    AssociatedControlID="R_txtContactno"></Anthem:Label>*
                            </div>
                            <label class="col-sm-2 control-label">Remarks </label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txtremarks" runat="server" CssClass="form-control" MaxLength="500"
                                    TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" onpaste="event.returnValue=false"
                                    ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="10"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Presently Working In (Organization/Institution)</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txtCurrentOccupation" onkeypress="return AllowAlphabet(event)" ondrop="return false" runat="server" AutoUpdateAfterCallBack="True"
                                    MaxLength="100" CssClass="form-control" TabIndex="11"></Anthem:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">Designation</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txtDesignation" onkeypress="return AllowAlphabet(event)" runat="server" AutoUpdateAfterCallBack="True"
                                    MaxLength="100" CssClass="form-control" ondrop="return false" TabIndex="12"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Permanent Address</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txtperadd" runat="server" AutoUpdateAfterCallBack="True" TabIndex="13"
                                    MaxLength="250" CssClass="form-control" TextMode="MultiLine" ondrop="return false"></Anthem:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">Present Address</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txtCurrentAddress" runat="server" AutoUpdateAfterCallBack="True"
                                    TabIndex="14" MaxLength="250" CssClass="form-control" ondrop="return false" TextMode="MultiLine"></Anthem:TextBox>
                                <br />
                                <Anthem:CheckBox ID="permanentaddchk" runat="server" AutoUpdateAfterCallBack="True" OnCheckedChanged="permanentaddchk_CheckedChanged" AutoCallBack="true" TabIndex="15" /><span style="color: red">Same as Permanent Address</span>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Expertise</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txtsplinterest" runat="server" CssClass="form-control" MaxLength="500"
                                    TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);"
                                    ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="16"></Anthem:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">Outstanding Achievements/ Honor/ Award/Position</label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txtachievement" runat="server" CssClass="form-control" MaxLength="500"
                                    TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);"
                                    ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="17"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Gender</label>
                            <div class="col-sm-4 required">
                                <Anthem:RadioButtonList ID="rdbGender" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbGender_SelectedIndexChanged" AutoCallBack="true" AutoUpdateAfterCallBack="true" TabIndex="18">
                                    <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="Others" Value="O"></asp:ListItem>
                                </Anthem:RadioButtonList>
                            </div>
                            <label class="col-sm-2 control-label" style="padding-top: 0px"></label>
                            <div class="col-sm-3">
                                <Anthem:CheckBox ID="chkmentor" runat="server" Enabled="false" AutoUpdateAfterCallBack="True" TabIndex="19" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="boxhead">
                    Special Ability Information
                </div>

                <div class="panel-body pnl-body-custom">

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
                                        <br />
                                        <Anthem:Button ID="btnDisabilityDoc" runat="server" AutoUpdateAfterCallBack="true" Text="upload" Visible="true" OnClick="btnDisabilityDoc_Click" EnableCallBack="false" class="logbutt" />
                                        <br />
                                        <asp:Label ID="Label2" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                            Text="[file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp, .gif and .png, .Pdf] file types are supported]" />
                                        <br />
                                        <Anthem:HyperLink ID="hyperDisabilityDoc" runat="server" AutoUpdateAfterCallBack="true" Visible="false" Text="" Target="_blank"></Anthem:HyperLink>
                                        <Anthem:LinkButton ID="lnkDisabilityDoc" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton>
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

                <div class="boxhead">
                    Education Details
                </div>

                <div class="panel-body pnl-body-custom">

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Highest Degree </label>
                            <div class="col-sm-4 required">
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
                                <%--<Anthem:DropDownList ID="ddldegree" runat="server" TabIndex="22" AutoUpdateAfterCallBack="true" AutoCallBack="True" CssClass="ChosenSelector" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged">
                                        </Anthem:DropDownList>--%>
                                <asp:DropDownList ID="ddldegree" Width="300px" runat="server" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged" AutoPostBack="true" CssClass="ChosenSelector" Style="width: 95% !important;">
                                </asp:DropDownList>
                                <span style="color: red">*</span>
                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <%--<Anthem:TextBox ID="txtdegree" CssClass="form-control" runat="server" Visible="false" AutoUpdateAfterCallBack="True" MaxLength="50" TextDuringCallBack="" AutoCallBack="true"
                                    ondrop="return false" OnTextChanged="E_txtEmail_TextChanged"></Anthem:TextBox>--%>
                            </div>
                            <label class="col-sm-2 control-label">Subject</label>
                            <div class="col-sm-4">
                                <%-- <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                    <ContentTemplate>--%>
                                <%--<Anthem:DropDownList ID="subjectlist" runat="server" AutoUpdateAfterCallBack="true" TextDuringCallBack="" TabIndex="23" CssClass="form-control">
                                </Anthem:DropDownList>--%>
                                <asp:DropDownList ID="subjectlist" runat="server" CssClass="ChosenSelector" AutoPostBack="false" Style="width: 95% !important;"></asp:DropDownList>
                                <span style="color: red">*</span>
                                <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Year of Passing</label>
                            <div class="col-sm-4 required">
                                <%-- <Anthem:DropDownList ID="D_ddlYeofPass" runat="server" TabIndex="24" AutoCallBack="True" AutoUpdateAfterCallBack="true" CssClass="ChosenSelector" TextDuringCallBack="">
                                </Anthem:DropDownList>--%>
                                <asp:DropDownList ID="D_ddlYeofPass" runat="server" Width="300px" TabIndex="8" AutoPostBack="false" CssClass="ChosenSelector" TextDuringCallBack="" Style="width: 95% !important;">
                                </asp:DropDownList>
                                <span style="color: red">*</span>
                            </div>
                            <label class="col-sm-2 control-label">Department of the University/College/Inst.</label>
                            <div class="col-sm-4 required">
                                <%--<Anthem:DropDownList ID="ddlDepartment" runat="server" TabIndex="25" AutoUpdateAfterCallBack="true" AutoCallBack="True" CssClass="ChosenSelector" TextDuringCallBack="">
                                </Anthem:DropDownList>--%>
                                <asp:DropDownList ID="ddlDepartment" runat="server" TabIndex="25" AutoPostBack="false" CssClass="ChosenSelector" Style="width: 95% !important;">
                                </asp:DropDownList>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="boxhead">
                    Documents Details
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Upload Photo</label>
                            <div class="col-sm-10">
                                <asp:Image ID="imgProfileimg" runat="server" ImageUrl="~/Alumni/StuImage/default-user.jpg"
                                    Width="100px" Height="100px" TabIndex="21" /><span style="color: red">*</span>
                                <br />

                                <asp:FileUpload ID="flUpload" CssClass="form-control" Style="width: 30% !important;" SkinID="none" runat="server" onchange="previewFile()" />
                                <br />
                                <Anthem:LinkButton ID="lnkprofile" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton>
                                <asp:TextBox ID="hdPath" runat="server" AutoUpdateAfterCallBack="true" BorderStyle="None" Width="0px" Height="0px" BorderWidth="0px" /><br />
                                <asp:Label ID="lblPhoto" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                    Text="[Photo (Passport) file size should not be more than 1MB and supported file types are .jpg, .jpeg, .bmp, .gif and .png]" />

                            </div>
                            <%--       <td>
                <Anthem:FileUpload ID="flUpload" runat="server" onpaste="event.returnValue=false"
                    ondrop="event.returnValue=false" AutoUpdateAfterCallBack="true" Width="220px" />
                <Anthem:LinkButton ID="lnkImage" runat="server" OnClick="lnkImage_Click" CausesValidation="False"
                    EnableCallBack="false" />
                <Anthem:TextBox ID="hdPath" runat="server" Visible="false" AutoUpdateAfterCallBack="true" BorderStyle="None"
                    Width="0px" Height="0px" BorderWidth="0px" />
            </td>--%>
                            <%--        <td class="vtext" rowspan="5" style="float: left">
                <Anthem:Image ID="stuimage" runat="server" AutoUpdateAfterCallBack="true" Width="100px" />

            </td>--%>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Upload Your Documents(HPU Document)</label>
                            <div class="col-sm-10 required">
                                <asp:Panel ID="Panel4" runat="server" AutoUpdateAfterCallBack="true" onchange="previewFile()" EnabledDuringCallBack="false">
                                    <asp:FileUpload ID="uploadDocuments" CssClass="form-control" Style="width: 30% !important;" SkinID="none" runat="server" AutoUpdateAfterCallBack="true" TabIndex="20" /><br />
                                    <Anthem:LinkButton ID="lnkDoc" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton>
                                    <asp:TextBox ID="TextBox1" runat="server" AutoUpdateAfterCallBack="true" BorderStyle="None" Width="0" Height="0px" BorderWidth="0px" /><br />
                                    <span id="sp_uploadDocuments" runat="server" style="color: red"></span>
                                    <asp:Label ID="LblDoc" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                        Text="[ file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp, .gif and .png, .Pdf] file types are supported]" />
                                    <%-- <asp:Label ID="lblDCMsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></asp:Label>--%>
                                    <span id="Span7" runat="server" visible="false" style="color: red"></span>
                                    <a target="_blank" id="anchorPathDC" runat="server"></a>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="boxhead">
                    Login Details
                </div>

                <div class="panel-body pnl-body-custom">

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label" id="lblLoginName1">Login Name </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="R_txtLoginName1" runat="server" TextDuringCallBack="Wait..." MaxLength="50" CssClass="form-control" AutoCallBack="true"
                                    TabIndex="26" Enabled="False"></Anthem:TextBox>
                            </div>
                            <label class="col-sm-2 control-label" id="lblPassword">Password </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="R_txtPassword" runat="server" MaxLength="30" CssClass="form-control" TextMode="Password" TabIndex="27" Enabled="False"></Anthem:TextBox>
                                <span id="toggle_pwd" class="fa fa-fw fa-eye field_icon" style="cursor: pointer;"></span>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label" id=""></label>
                            <div class="col-sm-10">
                                <Anthem:Label ID="Label1" runat="server" AutoUpdateAfterCallBack="true" CssClass="lblmessage"
                                    AssociatedControlID="R_txtLoginName1"></Anthem:Label>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="boxhead">
                    Fee Details
                </div>

                <div class="panel-body pnl-body-custom">
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-2">
                                <Anthem:RadioButtonList ID="rdPaymentOption" runat="server" Visible="false" RepeatDirection="Horizontal" AutoPostBack="true" AutoUpdateAfterCallBack="true" EnableCallBack="false" OnSelectedIndexChanged="rdPaymentOption_SelectedIndexChanged" Height="22px">
                                    <asp:ListItem Value="O"> <b>Online</b></asp:ListItem>
                                </Anthem:RadioButtonList>
                                <Anthem:Panel ID="pnlOnlinePayment" runat="server" AutoUpdateAfterCallBack="true">
                                    <div id="Table2">
                                        <div class="form-group form-group-sm">
                                            <div class="row">
                                                <label class="col-sm-2 control-label">
                                                    Details of Fee Deposited
                                                        <br />
                                                    शुल्क का विवरण जमा</label>
                                                <div class="col-sm-3">
                                                    <div class="fee-dtl">
                                                        <Anthem:Label ID="lblOnlineFees" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <%--         <div class="form-group form-group-sm">
                                            <div class="row">
                                                <div class="col-sm-10 col-sm-offset-2">
                                                    <Anthem:Label ID="lblPaymentMsg" runat="server" AutoUpdateAfterCallBack="true" Font-Bold="true" ForeColor="Red" Text=""></Anthem:Label>
                                                    <Anthem:Button ID="Button1" class="logbutt" Enabled="true" runat="server" AutoUpdateAfterCallBack="True"
                                                        Text="PROCEED" Width="80px" OnClick="btnpayment_Click" TabIndex="100" Visible="false" />
                                                </div>
                                                <div class="form-group form-group-sm">
                                                    <div class="row">
                                                        <div class="col-sm-10 col-sm-offset-2">
                                                            <Anthem:Button ID="Button2" CssClass="logbutt" ValidationGroup="" Text="REGISTER AND PAY" TabIndex="22" EnableCallBack="false" runat="server" OnClick="btnRegister_Click" Enabled="true" />
                                                            <Anthem:Button ID="btnReset" runat="server" CausesValidation="false" OnClick="btnReset_Click" EnableCallBack="false"
                                                                Text="RESET" class="logbutt" TextDuringCallBack="Reseting.." TabIndex="23" AutoUpdateAfterCallBack="true" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <div class="form-group form-group-sm">
                                            <div class="row">
                                                <div class="col-sm-10 col-sm-offset-2">
                                                    <Anthem:Button ID="btnPayment" CssClass="logbutt" ValidationGroup="" Text="PAY NOW" TabIndex="22" EnableCallBack="false" runat="server" OnClick="btnPayment_Click" Enabled="true" />
                                                    <Anthem:Button ID="btnRegister" OnClick="btnRegister_Click" class="logbutt" runat="server" TextDuringCallBack="UPDATING....." AutoUpdateAfterCallBack="True" Text="REGISTER" PreCallBackFunction="btnRegister_PreCallBack" OnClientClick="return validateEmail();" Visible="false" />
                                                    <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnback_Click" />
                                                    <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" CssClass="lblmessage"></Anthem:Label>
                                                    <asp:HiddenField ID="hdnId" runat="server"></asp:HiddenField>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </Anthem:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- <div class="boxhead">
                    Transaction Details
                </div>

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
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-2">
                                <Anthem:Button ID="btnPayment" CssClass="logbutt" ValidationGroup="" Text="PAY NOW" TabIndex="22" EnableCallBack="false" runat="server" OnClick="btnPayment_Click" Enabled="true" />
                                <Anthem:Button ID="btnRegister" OnClick="btnRegister_Click" class="logbutt" runat="server" TextDuringCallBack="UPDATING....." AutoUpdateAfterCallBack="True" Text="REGISTER" PreCallBackFunction="btnRegister_PreCallBack" OnClientClick="return validateEmail();" Visible="false" />
                                <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnback_Click" />
                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" CssClass="lblmessage"></Anthem:Label>
                            </div>

                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
    <script type="text/javascript">

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        function Confirm() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
</asp:Content>