<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_ShowPublished_Internship_Details.aspx.cs" Inherits="Alumni_ALM_ShowPublished_Internship_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <div class="col-md-12">
        <div class="row">

            <div class="col-md-12">
                <div class="row">
                    <div class="box box-success">
                        <div class="box-body table-responsive">
                            <table class="table mobile_form" width="100%">
                                <tr>
                                    <td colspan="6">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; text-align: left;">
                                            <tr>
                                                <td style="border: 0; width: 80%;" class="tablesubheading">Published Internships </td>
                                                <td style="border: 0; text-align: left; padding-right: 30px; width: 80%" class="tablesubheading">
                                                    <Anthem:LinkButton ID="lnkSearch" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkSearch_Click">Back </Anthem:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" class="tdgap"></td>
                                </tr>
                                <tr>
                                    <td id="lblCompanyName" class="vtext" style="width: 15%">Company Name </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="R_txtCompanyName" runat="server" Enabled="false" MaxLength="100" SkinID="textbox" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                    </td>
                                    <td id="lblDesignation" class="vtext" style="width: 15%">Designation </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="R_txtDesignation" runat="server" Enabled="false" MaxLength="100" SkinID="textbox" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="lblJobCat" class="vtext" style="width: 15%">Job Category </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:DropDownList ID="D_ddlJobCat" runat="server" Enabled="false" AutoUpdateAfterCallBack="true" AutoCallBack="true" CssClass="dropdownlong"></Anthem:DropDownList>
                                    </td>
                                    <td id="lblVacancyDtl" class="vtext" style="width: 15%">Internship Details </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="R_txtVacancyDtl" runat="server" MaxLength="250" SkinID="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="lblSkillReq" class="vtext" style="width: 15%">Skill Required </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="R_txtSkillReq" runat="server" MaxLength="250" SkinID="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>
                                    </td>
                                    <td id="lblSelectionProc" class="vtext" style="width: 15%">Selection Procedure </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="txt_SelectionProc" runat="server" MaxLength="250" SkinID="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="lblJobVacncyUrl" class="vtext" style="width: 15%">Internship URL </td>
                                    <td class="colon">: </td>
                                    <td colspan="4">
                                        <Anthem:TextBox ID="txt_JobVacncyUrl" runat="server" Enabled="false" MaxLength="100" SkinID="textboxlong" placeholder="http://www.xyz.com" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="lblStartDate" class="vtext" style="width: 15%">Start Date </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" Enabled="false" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control-date" TabIndex="4"></Anthem:TextBox>
                                        <%--<a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;">
                                            <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>--%>
                                    </td>
                                    <td id="lblEndDate" class="vtext" style="width: 15%">End Date </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" Enabled="false" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control-date" TabIndex="4"></Anthem:TextBox>
                                        <%--<a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;">
                                            <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="lblStipend" class="vtext" style="width: 15%">Stipends </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="txtStipends" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" Enabled="false" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control-date" TabIndex="5"></Anthem:TextBox>
                                    </td>
                                    <td id="lblDuration" class="vtext" style="width: 15%">Duration </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="txtDuration" runat="server" AutoUpdateAfterCallBack="True" Enabled="false" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control-date" TabIndex="4"></Anthem:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="lblDocs" class="vtext" style="width: 15%">Upload Documents </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:LinkButton ID="lnkViewDoc" Visible="false" runat="server" EnableCallBack="false" AutoUpdateAfterCallBack="true" OnClick="lnkViewDoc_Click"></Anthem:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <Anthem:Button ID="Btnapply" runat="server" AutoUpdateAfterCallBack="True" Visible="false" Text="Apply Job" TextDuringCallBack="Applying" OnClick="Btnapply_Click" data-toggle="modal" data-target="#ViewDiv" />
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
                    <h5 class="modal-title" id="exampleModalLabel">Apply For Internship </h5>
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
                                    <Anthem:LinkButton runat="server" ID="lnkviewBrc" CausesValidation="False"
                                        AutoUpdateAfterCallBack="True" OnClick="lnkviewBrc_Click"> </Anthem:LinkButton>
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
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>