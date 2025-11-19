<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_ViewPublishedJobs.aspx.cs" Inherits="Alumni_Alm_ViewPublishedJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function disableRightClick(event) {
            event.preventDefault();
        }

        window.onload = function () {
            document.addEventListener("contextmenu", disableRightClick);
        };
    </script>

    <script type="text/javascript">

        function CheckUrl() {
            var url = document.getElementById('<%=txt_JobVacncyUrl.ClientID%>');
        }

        function ShowRoomDetails(obj) {
            document.getElementById('ViewDiv').style.display = 'block';
        }

        function HideRoomDetails() {
            document.getElementById('ViewDiv').style.display = 'none';
        }

    </script>
    

    <div class="container-fluid mt-10">
        <div class="">
            <div class="box box-success">
                <div class="boxhead">
                    Publish Jobs
                </div>
                <div class="panel-body pnl-body-custom">

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblCompanyName" class="col-sm-2 control-label custom-text-right">Company Name : </label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="R_txtCompanyName" runat="server" MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true" TabIndex="1" Enabled="false"></Anthem:TextBox>*
                            </div>
                            <label id="lblDesignation" class="col-sm-2 control-label custom-text-right">Designation : </label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="R_txtDesignation" runat="server" MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true" TabIndex="2" Enabled="false"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblJobCat" class="col-sm-2 control-label custom-text-right">Job Category : </label>
                            <div class="col-sm-4 required">
                                <Anthem:DropDownList ID="D_ddlJobCat" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="true" CssClass="form-control" TabIndex="3" Enabled="false"></Anthem:DropDownList>*
                            </div>
                            <label id="lblVacancyDtl" class="col-sm-2 control-label custom-text-right">Vacancy Details : </label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="R_txtVacancyDtl" runat="server" CssClass="form-control" MaxLength="250" TextMode="MultiLine" onpaste="event.returnValue=false" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" TabIndex="4" Enabled="false"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblSkillReq" class="col-sm-2 control-label custom-text-right">Skill Required : </label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="R_txtSkillReq" runat="server" MaxLength="250" CssClass="form-control" TextMode="MultiLine" AutoUpdateAfterCallBack="true" TabIndex="5" Enabled="false"></Anthem:TextBox>*
                            </div>
                            <label id="lblSelectionProc" class="col-sm-2 control-label custom-text-right">Selection Procedure : </label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txt_SelectionProc" runat="server" MaxLength="250" CssClass="form-control" TextMode="MultiLine" AutoUpdateAfterCallBack="true" TabIndex="6" Enabled="false"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblJobVacncyUrl" class="col-sm-2 control-label custom-text-right">Job Vacancy URL : </label>
                            <div class="col-sm-4 required">
                                <Anthem:TextBox ID="txt_JobVacncyUrl" runat="server" MaxLength="100" CssClass="form-control" placeholder="http://www.xyz.com" AutoUpdateAfterCallBack="true" TabIndex="7" Enabled="false"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblStartDate" class="col-sm-2 control-label custom-text-right">Opening From Date : </label>
                            <div class="col-sm-4 required custom-cal">
                                <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control" TabIndex="8" Width="25%" Enabled="false"></Anthem:TextBox>*
                            </div>
                            <label id="lblEndDate" class="col-sm-2 control-label custom-text-right">Opening To Date : </label>
                            <div class="col-sm-4 required custom-cal">
                                <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control" TabIndex="9" Width="25%" Enabled="false"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblReqDocs" class="col-sm-2 control-label custom-text-right">Upload Documents : </label>
                            <div class="col-sm-4">
                                <Anthem:LinkButton ID="lnkViewDoc" Visible="false" runat="server" EnableCallBack="false" AutoUpdateAfterCallBack="true" OnClick="lnkViewDoc_Click"></Anthem:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-2">
                                <Anthem:Button ID="Btnapply" runat="server" AutoUpdateAfterCallBack="True" EnableCallBack="true" Text="Apply Job" TextDuringCallBack="Applying" OnClick="Btnapply_Click" data-toggle="modal" data-target="#ViewDiv" />
                                <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" OnClick="btnback_Click" />
                                <asp:Label ID="lblMsg" runat="server" CssClass="lblmessage" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<div class="col-md-12">
        <div class="row">
            <div class="box-body table-responsive">
                <table class="table mobile_form" style="border: 1px solid #016773 !important;">
                    <tr>
                        <td id="Td1" colspan="3" class="tableheading" runat="server" style="height: 19px; background: #016773 !important">Publish Jobs</td>
                    </tr>
                    <tr>
                        <td colspan="3" class="tdgap"></td>
                    </tr>
                    <tr>
                        <td class="vtext" id="lblCompanyName" style="width: 35%">Company Name</td>
                        <td class="colon" style="width: 2%">:</td>
                        <td class="required">
                            <Anthem:TextBox ID="R_txtCompanyName" runat="server" Enabled="false" MaxLength="100" SkinID="textboxlong" AutoUpdateAfterCallBack="true"></Anthem:TextBox>*</td>
                    </tr>
                    <tr>
                        <td class="vtext" id="lblDesignation" style="width: 35%">Designation</td>
                        <td class="colon" style="width: 2%">:</td>
                        <td class="required">
                            <Anthem:TextBox ID="R_txtDesignation" runat="server" Enabled="false" MaxLength="100" SkinID="textboxlong" AutoUpdateAfterCallBack="true"></Anthem:TextBox>*</td>
                    </tr>
                    <tr>
                        <td class="vtext" id="lblJobCat" style="width: 35%">Job Category</td>
                        <td class="colon" style="width: 2%">:</td>
                        <td class="required">

                            <Anthem:DropDownList ID="D_ddlJobCat" runat="server" Enabled="false" AutoUpdateAfterCallBack="true" AutoCallBack="true">
                            </Anthem:DropDownList>
                            *</td>
                    </tr>
                    <tr>
                        <td class="vtext" id="lblVacancyDtl" valign="top">Vacancy Details</td>
                        <td class="colon" valign="top">:</td>
                        <td class="required">
                            <Anthem:TextBox ID="R_txtVacancyDtl" runat="server"
                                MaxLength="250" SkinID="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>*</td>
                    </tr>
                    <tr>
                        <td class="vtext" id="lblSkillReq" valign="top">Skill Required</td>
                        <td class="colon" valign="top">:</td>
                        <td class="required">

                            <Anthem:TextBox ID="R_txtSkillReq" runat="server"
                                MaxLength="250" SkinID="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>*</td>
                    </tr>
                    <tr>
                        <td class="vtext" id="lblSelectionProc" valign="top">Selection Procedure</td>
                        <td class="colon" valign="top">:</td>
                        <td class="required">

                            <Anthem:TextBox ID="txt_SelectionProc" runat="server"
                                MaxLength="250" SkinID="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox></td>

                    </tr>
                    <tr>
                        <td class="vtext" id="lblJobVacncyUrl" style="width: 35%">Job Vacancy URL</td>
                        <td class="colon" style="width: 2%">:</td>
                        <td class="required">
                            <Anthem:TextBox ID="txt_JobVacncyUrl" runat="server" Enabled="false" MaxLength="100" SkinID="textboxlong"
                                placeholder="http://www.xyz.com"
                                AutoUpdateAfterCallBack="true"></Anthem:TextBox></td>
                    </tr>
                    <tr>
                        <td id="lblStartDate" class="vtext">Opening From Date</td>

                        <td class="colon">:</td>
                        <td class="required">
                            <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" Enabled="false" ondrag="return false;" ondrop="return false;"
                                MaxLength="10" onkeydown="return false" SkinID="textboxlong" TabIndex="4"></Anthem:TextBox><a
                                    href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;"><img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>'
                                        width="34" /></a>*</td>

                    </tr>
                    <tr>
                        <td id="lblEndDate" class="vtext">Opening To Date</td>

                        <td class="colon">:</td>
                        <td class="required">
                            <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" Enabled="false" onpaste="return false;" ondrag="return false;" ondrop="return false;"
                                MaxLength="10" onkeydown="return false" SkinID="textboxlong" TabIndex="4"></Anthem:TextBox><a
                                    href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;"><img
                                        alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>'
                                        width="34" /></a>*</td>

                    </tr>
                    <tr>
                        <td colspan="3" class="tdgap" style="text-align: center">
                            <Anthem:Button ID="Btnapply" runat="server" AutoUpdateAfterCallBack="True" EnableCallBack="true"
                                Text="Apply Job" TextDuringCallBack="Applying" OnClick="Btnapply_Click" data-toggle="modal" data-target="#ViewDiv" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>--%>
    <%--<div id="ViewDiv" class="white_content-new-1" style="overflow: scroll;">
        <div class="popupboxouter">
            <div class="popupbox" style="width: 100%;">
                <div onclick="document.getElementById('ViewDiv').style.display='none';" class="close-1">
                    X
                </div>
                
            </div>
        </div>
    </div>--%>
    <div class="modal fade white_content-new-1" id="ViewDiv" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: max-content;">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Apply For Job</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="box-body table-responsive">
                        <table style="margin-left: auto; margin-right: auto;">
                            <tr>
                                <td class="vtext">Job Number</td>
                                <td class="colon" style="padding: 4px">:</td>
                                <td class="Required" style="padding: 6px">
                                    <Anthem:Label ID="lbl_ReqNo" runat="server" AutoUpdateAfterCallBack="true" ondelete="return false" onDrop="blur();return false;" SkinID="textboxlong"></Anthem:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="vtext">Full Name</td>
                                <td class="colon" style="padding: 4px">:</td>
                                <td class="Required">
                                    <Anthem:TextBox ID="lbl_Fullname" runat="server" Enabled="false" AutoUpdateAfterCallBack="true" ondelete="return false" onDrop="blur();return false;" SkinID="textboxlong"></Anthem:TextBox>
                                </td>
                                <td class="vtext">Email</td>
                                <td class="colon" style="padding: 4px">:</td>
                                <td class="Required">
                                    <Anthem:TextBox ID="lbl_mail" runat="server" AutoUpdateAfterCallBack="true" Enabled="false" ondelete="return false" onDrop="blur();return false;" SkinID="textboxlong"></Anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="vtext">Contact No.</td>
                                <td class="colon" style="padding: 4px">:</td>
                                <td class="Required">
                                    <Anthem:TextBox ID="lblcontact" runat="server" AutoUpdateAfterCallBack="true" Enabled="false" ondelete="return false" onDrop="blur();return false;" SkinID="textboxlong"></Anthem:TextBox>
                                </td>
                                <td class="vtext">Address</td>
                                <td class="colon">:</td>
                                <td class="Required">
                                    <Anthem:TextBox ID="lbl_address" runat="server" AutoUpdateAfterCallBack="true" Enabled="false" ondelete="return false" onDrop="blur();return false;" SkinID="textboxlong"></Anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="vtext">Degree</td>
                                <td class="colon" style="padding: 4px">:</td>
                                <td class="Required">
                                    <Anthem:TextBox ID="lbldegree" runat="server" AutoUpdateAfterCallBack="true" ondelete="return false" onDrop="blur();return false;" Enabled="false" SkinID="textboxlong"></Anthem:TextBox>
                                </td>
                                <td class="vtext">Upload CV</td>
                                <td class="colon">:</td>
                                <td class="required">

                                    <Anthem:FileUpload ID="flUploadCV" AutoUpdateAfterCallBack="true" runat="server" SkinID="textboxlong" />*
                                    &nbsp;
                             <span style="font-weight: normal">File size shouldn’t be greater than 5 Mb
                          <br />
                                 format type. as in (PDF)</span>
                                    <Anthem:LinkButton runat="server" ID="lnkviewBrc" CausesValidation="False" AutoUpdateAfterCallBack="True" OnClick="lnkviewBrc_Click"> </Anthem:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="8" style="text-align: center;">
                                    <Anthem:LinkButton ID="lnkbtn_Save" runat="server" AutoUpdateAfterCallBack="true" CssClass="popbutton" Text="Submit " Enabled="true" OnClick="lnkbtn_Save_Click" EnableCallBack="false" UpdateAfterCallBack="True"></Anthem:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;">
    </iframe>
</asp:Content>
