<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_PublishedViewJob_Details.aspx.cs" Inherits="Alumni_ALM_PublishedViewJob_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <%--<style>
        .modal-header {
            background: #016773;
        }

            .modal-header .close {
                margin-top: -25px;
                color: #fff;
                font-size: 25px;
            }

        .modal-title {
            color: #fff;
            font-size: 15px;
            font-weight: 500;
        }

        .popbutton {
            background-color: #f0ad4e;
            border: 1px solid #eea236;
            color: #FFFFFF;
            cursor: pointer;
            font-weight: 600;
            padding: 5px 6px;
            border-radius: 5px 5px 5px 5px;
        }

            .popbutton:hover {
                color: #fff;
            }
        .auto-style1 {
            width: 852px;
        }

    </style>--%>

    <div class="col-md-12">
        <div class="row">

            <%--<div class="box box-success">
                <div class="boxhead">
                    Publish Jobs
                </div>
                <div class="panel-body pnl-body-custom">
                
                <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblCompanyName" class="col-sm-2 control-label">Company Name : </label>
                            <div class="col-sm-10">
                                <Anthem:TextBox ID="R_txtCompanyName" runat="server" Enabled="false" MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true"></Anthem:TextBox>* 
                            </div>
                            
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblDesignation" class="col-sm-2 control-label">Designation : </label>
                            <div class="col-sm-10">
                                <Anthem:TextBox ID="R_txtDesignation" runat="server" Enabled="false" MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true"></Anthem:TextBox>* 
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblJobCat" class="col-sm-2 control-label">Job Category : </label>
                            <div class="col-sm-10">
                                <Anthem:DropDownList ID="D_ddlJobCat" runat="server" Enabled="false" AutoUpdateAfterCallBack="true" AutoCallBack="true" CssClass="dropdownlong">
                            </Anthem:DropDownList>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">                            
                            <label id="lblVacancyDtl" class="col-sm-2 control-label">Vacancy Details : </label>
                            <div class="col-sm-10">
                                <Anthem:TextBox ID="R_txtVacancyDtl" runat="server" MaxLength="250" CssClass="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblSkillReq" class="col-sm-2 control-label">Skill Required : </label>
                            <div class="col-sm-10">
                               <Anthem:TextBox ID="R_txtSkillReq" runat="server" MaxLength="250" CssClass="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">                            
                            <label id="lblSelectionProc" class="col-sm-2 control-label">Selection Procedure : </label>
                            <div class="col-sm-10">
                                <Anthem:TextBox ID="txt_SelectionProc" runat="server" MaxLength="250" CssClass="textboxmultiline" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblJobVacncyUrl" class="col-sm-2 control-label">Job Vacancy URL : </label>
                            <div class="col-sm-10">
                               <Anthem:TextBox ID="txt_JobVacncyUrl" runat="server" Enabled="false" MaxLength="100" CssClass="form-control" placeholder="http://www.xyz.com" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblStartDate" class="col-sm-2 control-label">Opening From Date : </label>
                            <div class="col-sm-10">
                                <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" Enabled="false" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control-date" TabIndex="4"></Anthem:TextBox>
                            <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;">
                                <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblEndDate" class="col-sm-2 control-label">Opening To Date : </label>
                            <div class="col-sm-10 required">
                               <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" Enabled="false" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control-date" TabIndex="4"></Anthem:TextBox>
                            <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;">
                                <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>*
                            </div>
                        </div>
                    </div>

                    <div class="clearfix"></div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-2">
                                <Anthem:Button ID="Btnapply" runat="server" AutoUpdateAfterCallBack="True" Visible="false" Text="Apply Job" TextDuringCallBack="Applying" OnClick="Btnapply_Click" data-toggle="modal" data-target="#ViewDiv" />
                            </div>
                        </div>
                    </div>
                
                </div>
            </div>--%>

            <div class="col-md-12">
                <div class="row">
                    <div class="box box-success">
                        <div class="box-body table-responsive">
                            <table class="table mobile_form" width="100%">
                                <%-- <tr>
                                    <td colspan="6" class="tableheading">Publish Jobs </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="6">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; text-align: left;">
                                            <tr>
                                                <td style="border: 0; width: 80%;" class="tablesubheading">Publish Jobs</td>
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
                                    <td id="lblVacancyDtl" class="vtext" style="width: 15%">Vacancy Details </td>
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
                                    <td id="lblJobVacncyUrl" class="vtext" style="width: 15%">Job Vacancy URL </td>
                                    <td class="colon">: </td>
                                    <td colspan="4">
                                        <Anthem:TextBox ID="txt_JobVacncyUrl" runat="server" Enabled="false" MaxLength="100" SkinID="textboxlong" placeholder="http://www.xyz.com" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="lblStartDate" class="vtext" style="width: 15%">Opening From Date </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" Enabled="false" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control-date" TabIndex="4"></Anthem:TextBox>
                                        <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;">
                                            <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>
                                    </td>
                                    <td id="lblEndDate" class="vtext" style="width: 15%">Opening To Date </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" Enabled="false" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control-date" TabIndex="4"></Anthem:TextBox>
                                        <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;">
                                            <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="lblDocs" class="vtext" style="width: 15%">Upload Documents </td>
                                    <td class="colon">: </td>
                                    <td>
                                        <Anthem:LinkButton ID="lnkViewDoc" Visible="false" runat="server" EnableCallBack="false" AutoUpdateAfterCallBack="true" OnClick="lnkViewDoc_Click"></Anthem:LinkButton>
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
    
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>

