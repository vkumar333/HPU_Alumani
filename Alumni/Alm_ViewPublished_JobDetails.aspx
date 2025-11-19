<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/Alm_registration_master.master" AutoEventWireup="true" CodeFile="Alm_ViewPublished_JobDetails.aspx.cs" Inherits="Alumni_Alm_ViewPublished_JobDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function CheckUrl() {
            var url = document.getElementById('<%=txt_JobVacncyUrl.ClientID%>');
        }

        function ShowRoomDetails(obj) {
            debugger;
            document.getElementById('ViewDiv').style.display = 'block';
        }

        function HideRoomDetails() {
            document.getElementById('ViewDiv').style.display = 'none';
        }

    </script>
    <style>
        .modal-header {
            background: #016773;
        }

        /*.modal-header .close {
                margin-top: -25px;
                color: #fff;
                font-size: 25px;
            }
        */

        .close {
            background: none;
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

        textarea.form-control {
            max-width: 95%;
            min-width: 0;
        }
    </style>

    <script src='<%=Page.ResolveUrl("~/include/jquery.1.9.1.min.js")%>' type="text/javascript"></script>
    <script src='<%=Page.ResolveUrl("~/include/bootstrap/bootstrap.js")%>' type="text/javascript"></script>
    <script src='<%=Page.ResolveUrl("~/include/CommonJS.js")%>' type="text/javascript"></script>
    <script src='<%=Page.ResolveUrl("~/include/krutiDev.js")%>' type="text/javascript"></script>

    <link href="../include/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="boxhead">
                    Vacancies Details
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Company Name : </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="R_txtCompanyName" runat="server" Enabled="false" MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                <span style="color: red">*</span>
                            </div>
                            <label class="col-sm-2 control-label">Designation : </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="R_txtDesignation" runat="server" Enabled="false" MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Job Category : </label>
                            <div class="col-sm-4">
                                <Anthem:DropDownList ID="D_ddlJobCat" runat="server" Enabled="false" AutoUpdateAfterCallBack="true" AutoCallBack="true" CssClass="form-control">
                                </Anthem:DropDownList>
                                <span style="color: red">*</span>
                            </div>
                            <label class="col-sm-2 control-label">Vacancy Details : </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="R_txtVacancyDtl" runat="server" MaxLength="250" CssClass="form-control" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px" Width="250px"></Anthem:TextBox>
                                <span style="color: red">*</span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Skill Required : </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="R_txtSkillReq" runat="server" MaxLength="250" CssClass="form-control" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px"></Anthem:TextBox>
                                <span style="color: red">*</span>
                            </div>
                            <label class="col-sm-2 control-label">Selection Procedure : </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="txt_SelectionProc" runat="server" MaxLength="250" CssClass="form-control" TextMode="MultiLine" Enabled="false" AutoUpdateAfterCallBack="true" Height="90px"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Job Vacancy URL : </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="txt_JobVacncyUrl" runat="server" Enabled="false" MaxLength="100" CssClass="form-control"
                                    placeholder="http://www.xyz.com" AutoUpdateAfterCallBack="true"></Anthem:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">Opening From Date : </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" Enabled="false" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control" TabIndex="4" Style="width: 30%!important;"></Anthem:TextBox>
                                <%--<a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;">
                                    <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" />
                                </a>--%>
                            </div>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Opening To Date : </label>
                            <div class="col-sm-4">
                                <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" Enabled="false" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control" TabIndex="4" Style="width: 30%!important;"></Anthem:TextBox>
                                <%--<a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;">
                                    <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" />
                                </a>--%>
                            </div>
                            <label class="col-sm-2 control-label"></label>
                            <div class="col-sm-4">
                            </div>
                        </div>
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblReqDocs" class="col-sm-2 control-label">Upload Documents : </label>
                            <div class="col-sm-6 ">
                              <Anthem:LinkButton ID="lnkViewDoc" Visible="false" runat="server" EnableCallBack="false" AutoUpdateAfterCallBack="true" OnClick="lnkViewDoc_Click"></Anthem:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>
