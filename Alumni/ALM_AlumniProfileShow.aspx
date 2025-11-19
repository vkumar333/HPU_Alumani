<%@ Page Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_AlumniProfileShow.aspx.cs" Inherits="Alumni_ALM_AlumniProfileShow"
    Title="Alumni Profile Show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
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
            $('#ctl00_ContentPlaceHolder1_imgProfileimg').attr("src", "../../Alumni/StuImage/default-user.jpg");
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
    
    <div class="container-custom mt-10">
        <div class="">
            <div class="box box-success">

                <fieldset class="fieldset-border" oncontextmenu="return false;" style="margin-top: 10px; padding: 24px 28px; margin-bottom: 24px; border: 1px solid #ccc; border-radius: 8px;">
                    <legend>Profile Details </legend>
                    <div class="panel-body pnl-body-custom">
                        						
						<div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label" style="margin-top: 15px;">Alumni Type : </label>
                                <div class="col-sm-4 required" style="margin-bottom: 50px;">
                                    <Anthem:RadioButtonList ID="rdalumnitype" runat="server" RepeatDirection="Vertical" AutoCallBack="true" AutoUpdateAfterCallBack="true" Height="22px" Enabled="false" contextmenu="preventDefault();" TabIndex="1">
                                        <asp:ListItem Value="F"> <b class="spanLoginReg">&nbsp;Faculty</b></asp:ListItem>
                                        <asp:ListItem Value="S"> <b class="spanLoginReg">&nbsp;Current Student</b></asp:ListItem>
                                        <asp:ListItem Value="ExStu"> <b class="spanLoginReg">&nbsp;Ex-Student</b></asp:ListItem>
                                        <asp:ListItem Value="StuCh"> <b class="spanLoginReg">&nbsp;Student Chapter</b></asp:ListItem>
                                    </Anthem:RadioButtonList>
                                </div>
                                <label class="col-sm-2 control-label" style="margin-top: 15px;">Membership Type : </label>
                                <div class="col-sm-4" style="margin-bottom: 35px;">
                                    <Anthem:RadioButtonList ID="rbdAlumniMemtype" runat="server" CssClass="form-control" RepeatDirection="Vertical" AutoCallBack="true" OnSelectedIndexChanged="rbdAlumniMemtype_SelectedIndexChanged" AutoUpdateAfterCallBack="true" Enabled="false" TabIndex="2">
                                        <asp:ListItem Value="LM"><b class="spanLoginReg">&nbsp;Life Membership(₹ 2000)</b> </asp:ListItem>
                                        <asp:ListItem Value="SM"><b class="spanLoginReg">&nbsp;Student Membership(₹ 1000)</b> </asp:ListItem>
                                        <asp:ListItem Value="SC"><b class="spanLoginReg">&nbsp;Student Chapter Membership(₹ 100)</b> </asp:ListItem>
                                    </Anthem:RadioButtonList>
                                </div>                               
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Alumni Registration No. : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="R_txtAlumnino" runat="server" AutoUpdateAfterCallBack="True"
                                        MaxLength="100" CssClass="form-control" Enabled="false" Font-Bold="true" TabIndex="3"></Anthem:TextBox>
										<span style="color: red">*</span>
                                </div>
                                <label class="col-sm-2 control-label">Alumni Name : </label>
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <Anthem:DropDownList ID="Drp_Alumni_Name" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoUpdateAfterCallBack="true"
                                                TextDuringCallBack="" TabIndex="4" />
                                        </div>
                                        <div class="col-sm-8">
                                            <Anthem:TextBox ID="txtAlumniName" onkeypress="return AllowAlphabet(event)" runat="server"
                                                MaxLength="100" CssClass="form-control" TabIndex="5" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                            <span style="color: red">*</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Email : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="E_txtEmail" CssClass="form-control" runat="server" AutoUpdateAfterCallBack="True" MaxLength="50" TextDuringCallBack="" AutoCallBack="true" ondrop="return false" OnTextChanged="E_txtEmail_TextChanged" TabIndex="6"></Anthem:TextBox>
                                    <Anthem:Label ID="lblEmailMsg" ForeColor="Red" runat="server" AutoUpdateAfterCallBack="true"
                                        AssociatedControlID="E_txtEmail"></Anthem:Label>*
                                </div>
                                <label class="col-sm-2 control-label">Date of Birth : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="R_txtPostedDate" Style="width: 120px !important" Enabled="true" runat="server" AutoUpdateAfterCallBack="True" MaxLength="19"
                                        onMouseDown="DisableRightClick(event);" onpaste="event.returnValue=false" ondrop="event.returnValue=false"
                                        CssClass="form-control" onkeydown="alert('Please Select Date from Calendar');" onkeypress="return false;" TabIndex="7"></Anthem:TextBox>
                                    <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_R_txtPostedDate);return false;">
                                        <img align="absMiddle" alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("~/calendar/calbtn.gif")%>'
                                            width="34" />
                                    </a>*
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Mobile No. : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="R_txtContactno" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True" MaxLength="10" TextDuringCallBack="" AutoCallBack="true" ondrop="return false" ondrag="return false" OnTextChanged="R_txtContactno_TextChanged"
                                        onkeydown="return IntegerOnly(event,this);" TabIndex="8"></Anthem:TextBox>
                                    <Anthem:Label ID="lblMobleNoMsg" ForeColor="Red" runat="server" AutoUpdateAfterCallBack="true"
                                        AssociatedControlID="R_txtContactno"></Anthem:Label>*
                                </div>
                                <label class="col-sm-2 control-label">Gender : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:RadioButtonList ID="rdbGender" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbGender_SelectedIndexChanged" AutoCallBack="true" AutoUpdateAfterCallBack="true" TabIndex="9">
                                        <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        <asp:ListItem Text="Others" Value="O"></asp:ListItem>
                                    </Anthem:RadioButtonList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Permanent Address : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtperadd" runat="server" AutoUpdateAfterCallBack="True" TabIndex="10"
                                        MaxLength="250" CssClass="form-control" TextMode="MultiLine" ondrop="return false"></Anthem:TextBox>
                                </div>                                
                                <label class="col-sm-2 control-label" style="padding-top: 0px"></label>
                                <div class="col-sm-4">
                                    <Anthem:CheckBox ID="chkmentor" runat="server" Enabled="false" AutoUpdateAfterCallBack="True" Visible="false" />
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">                                
                                <label class="col-sm-2 control-label">Present Address : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtCurrentAddress" runat="server" AutoUpdateAfterCallBack="True"
                                        TabIndex="11" MaxLength="250" CssClass="form-control" ondrop="return false" TextMode="MultiLine">
                                    </Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Same as Permanent Address: </label>
                                <label class="col-sm-4 control-label custom-text-left">
                                    <Anthem:CheckBox ID="permanentaddchk" runat="server" AutoUpdateAfterCallBack="True" OnCheckedChanged="permanentaddchk_CheckedChanged" AutoCallBack="true" TabIndex="12" />
                                </label>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Presently Working In (Organization/Institution) : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtCurrentOccupation" onkeypress="return AllowAlphabet(event)" ondrop="return false" runat="server" AutoUpdateAfterCallBack="True" MaxLength="100" CssClass="form-control" TabIndex="13"></Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Designation : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtDesignation" onkeypress="return AllowAlphabet(event)" runat="server" AutoUpdateAfterCallBack="True"
                                        MaxLength="100" CssClass="form-control" ondrop="return false" TabIndex="14"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Present Location : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtLocation" runat="server" CssClass="form-control" MaxLength="25" onkeypress="return CheckMaxLength(this,25);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" placeholder="e.g., Shimla" TabIndex="15"></Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label" style="padding-top: 0px">Country : </label>
                                <div class="col-sm-4">
                                    <Anthem:TextBox ID="txtCountry" runat="server" CssClass="form-control" MaxLength="25" onkeypress="return CheckMaxLength(this,25);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" placeholder="e.g., India" TabIndex="16"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Outstanding Achievements/ Honor/ Award/Position : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtachievement" runat="server" CssClass="form-control" MaxLength="500"
                                        TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);"
                                        ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="17"></Anthem:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Expertise : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtsplinterest" runat="server" CssClass="form-control" MaxLength="500"
                                        TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);"
                                        ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="18"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Remarks </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtremarks" runat="server" CssClass="form-control" MaxLength="500"
                                        TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" onpaste="event.returnValue=false"
                                        ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="19"></Anthem:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>

                </fieldset>

                <fieldset class="fieldset-border" oncontextmenu="return false;" style="padding: 24px 28px; margin-bottom: 24px; border: 1px solid #ccc; border-radius: 8px;">
                    <legend>Special Ability Information </legend>

                    <div class="panel-body pnl-body-custom" oncontextmenu="return false;">
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Special Ability ?  </label>
                                <label class="col-sm-4 control-label custom-text-left">
                                    <Anthem:RadioButtonList ID="rdbIsPersonDisability" runat="server" RepeatDirection="Horizontal" AutoCallBack="true" AutoUpdateAfterCallBack="true" TabIndex="20" OnSelectedIndexChanged="rdbIsPersonDisability_SelectedIndexChanged">
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                    </Anthem:RadioButtonList>
                                </label>
                                <Anthem:Panel ID="isChkDisabilityPnl" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" Visible="false">
                                    <label class="col-sm-2 control-label">Is Special Ability More than 40%  ?  </label>
                                    <label class="col-sm-4 control-label custom-text-left">
                                        <Anthem:CheckBox ID="isChkDisability" runat="server" OnCheckedChanged="isChkDisability_CheckedChanged" AutoUpdateAfterCallBack="true" AutoCallBack="true" TabIndex="21" />
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
                                            <Anthem:FileUpload ID="fileDisabilityDoc" TabIndex="22" SkinID="none" runat="server" AutoUpdateAfterCallBack="true" onchange="previewFile()" />
                                            <br />
                                            <Anthem:Button ID="btnDisabilityDoc" runat="server" AutoUpdateAfterCallBack="true" Text="UPLOAD" Visible="true" OnClick="btnDisabilityDoc_Click" EnableCallBack="false" class="logbutt" />
                                            <br />
                                            <asp:Label ID="Label2" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                                Text="[file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp, .png and .pdf] file types are supported]" />
                                            <br />
                                            <%--<Anthem:HyperLink ID="hyperDisabilityDoc" runat="server" AutoUpdateAfterCallBack="true" Visible="false" Text="" Target="_blank"></Anthem:HyperLink>--%>
                                            <Anthem:LinkButton ID="lnkDisabilityDoc" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton>

                                        </div>
                                        <label class="col-sm-2 control-label" for="txtDisabilityRemark">Special Ability Description </label>
                                        <div class="col-sm-4">
                                            <Anthem:TextBox ID="txtDisabilityRemark" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="23">
                                            </Anthem:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </Anthem:Panel>
                    </div>

                </fieldset>

                <fieldset class="fieldset-border" oncontextmenu="return false;" style="padding: 24px 28px; margin-bottom: 24px; border: 1px solid #ccc; border-radius: 8px;">                  
					<legend>Education Details</legend>
						<div class="gridiv">
							<Anthem:GridView ID="gvQualifications" runat="server" AutoGenerateColumns="False" Width="100%" AutoUpdateAfterCallBack="true"
								PageSize="10" AllowPaging="true" ShowHeader="True" OnRowCommand="gvQualifications_RowCommand">
								<Columns>
									<asp:TemplateField HeaderText="S.No.">
										<ItemStyle HorizontalAlign="Center" Width="5%" />
										<ItemTemplate>
											<%# Container.DataItemIndex + 1 %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Degree" HeaderStyle-Width="25%">
										<ItemTemplate>
											<Anthem:DropDownList ID="ddldegree" runat="server"
												CssClass="ChosenSelector"
												AutoCallBack="true"
												AutoUpdateAfterCallBack="true"
												OnSelectedIndexChanged="ddldegree_SelectedIndexChanged"
												Style="width: 95% !important;" TabIndex="24">
											</Anthem:DropDownList>
										</ItemTemplate>
									</asp:TemplateField>

									<asp:TemplateField HeaderText="Subject" HeaderStyle-Width="25%">
										<ItemTemplate>
											<Anthem:DropDownList ID="subjectlist" runat="server"
												CssClass="ChosenSelector"
												AutoUpdateAfterCallBack="true"
												Style="width: 95% !important;" TabIndex="25">
											</Anthem:DropDownList>
										</ItemTemplate>
									</asp:TemplateField>

									<asp:TemplateField HeaderText="Passing Year" HeaderStyle-Width="8%">
										<ItemTemplate>
											<Anthem:DropDownList ID="D_ddlYeofPass" runat="server"
												CssClass="ChosenSelector"
												AutoCallBack="false"
												AutoUpdateAfterCallBack="true"
												Style="width: 95% !important;" TabIndex="26">
											</Anthem:DropDownList>
										</ItemTemplate>
									</asp:TemplateField>

									<asp:TemplateField HeaderText="Department" HeaderStyle-Width="25%">
										<ItemTemplate>
											<Anthem:DropDownList ID="ddlDepartment" runat="server"
												CssClass="ChosenSelector"
												AutoCallBack="false"
												AutoUpdateAfterCallBack="true"
												Style="width: 95% !important;" TabIndex="27">
											</Anthem:DropDownList>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="DELETE" HeaderStyle-Width="5%">
										<ItemStyle HorizontalAlign="Center" Width="18%" />
										<ItemTemplate>
											<Anthem:LinkButton ID="lnkDelete" runat="server" AutoUpdateAfterCallBack="True" CausesValidation="False" CommandArgument="<%# Container.DataItemIndex + 1%>" CommandName="DELETEREC" TextDuringCallBack="Deleting..." PreCallBackFunction="btnDelete_PreCallBack">
												<img src="../Images/Delete.gif" alt="" border="0" />
											</Anthem:LinkButton>
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
							</Anthem:GridView>
						</div>
                    <!-- Add More Button -->
                    <div class="form-group text-right" style="margin-top: 15px; margin-right: 10px;">
                        <Anthem:Button ID="btnAddRow" runat="server" CssClass="btn btn-success" Text="+ Add More" CommandName="ADD" OnClick="btnAddRow_Click" AutoUpdateAfterCallBack="true" />
                    </div>

                </fieldset>

                <fieldset class="fieldset-border" oncontextmenu="return false;" style="padding: 24px 28px; margin-bottom: 24px; border: 1px solid #ccc; border-radius: 8px;">
                    <legend>Upload Documents </legend>
                    <div class="panel-body pnl-body-custom">
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Upload Photo</label>
                                <div class="col-sm-10">
                                    <asp:Image ID="imgProfileimg" runat="server" ImageUrl="../../StuImage/default-user.jpg"
                                        Width="100px" Height="100px" /><span style="color: red">*</span>
                                    <br />
                                    <br />
                                    <asp:FileUpload ID="flUpload" CssClass="form-control" Style="width: 30% !important;" SkinID="none" runat="server" onchange="previewFile()"  TabIndex="28" />
                                    <br />
                                    <Anthem:LinkButton ID="lnkprofile" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton>
                                    <asp:TextBox ID="hdPath" runat="server" AutoUpdateAfterCallBack="true" BorderStyle="None" Width="0px" Height="0px" BorderWidth="0px" /><br />
                                    <asp:Label ID="lblPhoto" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                        Text="[Photo (Passport) file size should not be more than 1MB and supported file types are .jpg, .jpeg, .bmp and .png]" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <div class="row">
                                <label class="col-sm-2 control-label">Upload Your Documents(HPU Document)</label>
                                <div class="col-sm-10 required">
                                    <asp:Panel ID="Panel4" runat="server" Visible="true" AutoUpdateAfterCallBack="true" onchange="previewFile()" EnabledDuringCallBack="false">
                                        <asp:FileUpload ID="uploadDocuments" CssClass="form-control" Style="width: 30% !important;" SkinID="none" runat="server" AutoUpdateAfterCallBack="true" TabIndex="29" /><br />
                                        <Anthem:LinkButton ID="lnkDoc" Visible="false" runat="server" AutoUpdateAfterCallBack="true"></Anthem:LinkButton>
                                        <asp:TextBox ID="TextBox1" runat="server" AutoUpdateAfterCallBack="true" BorderStyle="None" Width="0" Height="0px" BorderWidth="0px" /><br />
                                        <span id="sp_uploadDocuments" runat="server" style="color: red"></span>
                                        <asp:Label ID="LblDoc" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                            Text="[ file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp, .png and .pdf] file types are supported]" />
                                        <%-- <asp:Label ID="lblDCMsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></asp:Label>--%>
                                        <span id="Span7" runat="server" visible="false" style="color: red"></span>
                                        <a target="_blank" id="anchorPathDC" runat="server"></a>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>

                </fieldset>

                <fieldset class="fieldset-border" oncontextmenu="return false;" style="padding: 24px 28px; margin-bottom: 24px; border: 1px solid #ccc; border-radius: 8px;">
                    <legend>Login Details </legend>
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
                </fieldset>

                <div class="panel-body pnl-body-custom">
                    <div class="form-group form-group-sm">
                        <div class="row" align="center">
                            <div class="col-sm-12" style="margin-top: 25px;">
                                <Anthem:Button ID="btnRegister" OnClick="btnRegister_Click" class="logbutt" runat="server" TextDuringCallBack="UPDATING....." AutoUpdateAfterCallBack="True" Text="REGISTER" PreCallBackFunction="btnRegister_PreCallBack" OnClientClick="return validateEmail();" />
                                <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnback_Click" />
                                <Anthem:Button ID="btnPrint" runat="server" Text="PRINT" AutoUpdateAfterCallBack="true" CommandName="PRINT" OnClick="btnPrint_Click" Class="logbutt" EnableCallBack="false" />
                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" CssClass="lblmessage"></Anthem:Label>
                                <Anthem:Label ID="lblOnlineFees" runat="server" AutoUpdateAfterCallBack="true" Visible="false"></Anthem:Label>
                                <Anthem:HiddenField ID="hdnId" runat="server" AutoUpdateAfterCallBack="true" Visible="false"></Anthem:HiddenField>
                            </div>
                        </div>
                    </div>
                </div>
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