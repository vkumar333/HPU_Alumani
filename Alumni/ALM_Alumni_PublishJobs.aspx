<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Alumni_PublishJobs.aspx.cs" Inherits="Alumni_ALM_Alumni_PublishJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CheckUrl() {
            var url = document.getElementById('<%=txt_JobVacncyUrl.ClientID%>');
        }
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
    <div class="container-fluid mt-10">
        <div class="">
            <div class="box box-success">
                <div class="boxhead mt-0 mb-10">
                    Publish Jobs					
					<a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_Alumni_Home.aspx">Back </a>
                </div>
                <div class="panel-body pnl-body-custom">

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblCompanyName" class="col-sm-2 control-label custom-text-right">Company Name : </label>
                            <div class="col-sm-3 required">
                                <Anthem:TextBox ID="R_txtCompanyName" runat="server" MaxLength="100" CssClass="form-control" AutoUpdateAfterCallBack="true" TabIndex="1"></Anthem:TextBox>*
                            </div>
                            <label id="lblDesignation" class="col-sm-2 control-label custom-text-right">Designation : </label>
                            <div class="col-sm-3 required">
                                <Anthem:TextBox ID="R_txtDesignation" runat="server" MaxLength="35" CssClass="form-control" AutoUpdateAfterCallBack="true" TabIndex="2"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblJobCat" class="col-sm-2 control-label custom-text-right">Job Category : </label>
                            <div class="col-sm-3 required">
                                <Anthem:DropDownList ID="D_ddlJobCat" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="true" CssClass="form-control" TabIndex="3"></Anthem:DropDownList>*
                            </div>
                            <label id="lblVacancyDtl" class="col-sm-2 control-label custom-text-right">Vacancy Details : </label>
                            <div class="col-sm-3 required">
                                <Anthem:TextBox ID="R_txtVacancyDtl" runat="server" CssClass="form-control" MaxLength="250" TextMode="MultiLine" onpaste="event.returnValue=false" ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" Style="width: 95% !important;" Height="90px" TabIndex="4"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblSkillReq" class="col-sm-2 control-label custom-text-right">Skill Required : </label>
                            <div class="col-sm-3 required">
                                <Anthem:TextBox ID="R_txtSkillReq" runat="server" MaxLength="100" CssClass="form-control" TextMode="MultiLine" AutoUpdateAfterCallBack="true" Height="90px" Style="width: 95% !important;" TabIndex="5"></Anthem:TextBox>*
                            </div>
                            <label id="lblSelectionProc" class="col-sm-2 control-label custom-text-right">Selection Procedure : </label>
                            <div class="col-sm-3 required">
                                <Anthem:TextBox ID="txt_SelectionProc" runat="server" MaxLength="100" CssClass="form-control" TextMode="MultiLine" AutoUpdateAfterCallBack="true" Height="90px" Style="width: 95% !important;" TabIndex="6"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblJobVacncyUrl" class="col-sm-2 control-label custom-text-right">Job Vacancy URL : </label>
                            <div class="col-sm-3 required">
                                <Anthem:TextBox ID="txt_JobVacncyUrl" runat="server" MaxLength="25" CssClass="form-control"
                                    placeholder="http://www.xyz.com" AutoUpdateAfterCallBack="true" TabIndex="7"></Anthem:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblStartDate" class="col-sm-2 control-label custom-text-right">Opening From Date : </label>
                            <div class="col-sm-3 required custom-cal">
                                <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control" TabIndex="8" Width="30%"></Anthem:TextBox>
                                <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;">
                                    <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" />
                                </a>*
                            </div>
                            <label id="lblEndDate" class="col-sm-2 control-label custom-text-right">Opening To Date : </label>
                            <div class="col-sm-3 required custom-cal">
                                <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" CssClass="form-control" TabIndex="9" Width="30%"></Anthem:TextBox>
                                <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;">
                                    <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" />
                                </a>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label id="lblReqDocs" class="col-sm-2 control-label custom-text-right">Upload Documents : </label>
                            <div class="col-sm-3 required custom-cal">
                                <Anthem:FileUpload ID="flUploadDocs" CssClass="form-control" TabIndex="20" SkinID="none" runat="server" AutoUpdateAfterCallBack="true" onchange="previewFile()" />*
                                <asp:Label ID="lblDocs" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                                    Text=" [file size should not be more than 2 MB and supported file types are .jpg, .jpeg, .bmp, .gif and .png, .Pdf] file types are supported]" />
                            </div>
                            <label id="lblFile" class="col-sm-2 control-label custom-text-right"></label>
                            <div class="col-sm-3 required custom-cal">
                                <Anthem:HyperLink Target="_blank" ID="hlnk" runat="server" AutoUpdateAfterCallBack="true"></Anthem:HyperLink>
                                <Anthem:LinkButton ID="lnkViewDoc" Visible="false" runat="server" CausesValidation="False" AutoUpdateAfterCallBack="true" OnClick="lnkViewDoc_Click"></Anthem:LinkButton>

                            </div>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-2">
                                <Anthem:Button ID="btnSave" runat="server" Text="PUBLISH" OnClick="btnSave_Click" CommandName="SAVE" AutoUpdateAfterCallBack="true" CausesValidation="true" EnableCallBack="false" />
                                &nbsp;&nbsp;
                            <Anthem:Button ID="btnReset" runat="server" Text="RESET" OnClick="btnReset_Click" AutoUpdateAfterCallBack="true" EnableCallBack="false" />
                                &nbsp;&nbsp;
                               <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" EnableCallBack="false" TextDuringCallBack="SUBMITING..." OnClick="btnback_Click" />
                                <asp:Label ID="lblMsg" runat="server" CssClass="lblmessage" />
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <div class="boxhead">
                            List of Published Jobs
                        </div>
                        <Anthem:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" Width="100%" AutoUpdateAfterCallBack="true"
                            PageSize="10" AllowPaging="true" OnRowCommand="gvDetails_RowCommand" OnPageIndexChanging="gvDetails_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Company Name" DataField="CompanyName" />
                                <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                <asp:BoundField HeaderText="Vacancy Detail" DataField="VacancyDtl" />
                                <asp:BoundField HeaderText="Opening From Date" DataField="JobOpenFrom" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Opening To Date" DataField="JobOpenTo" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField HeaderText="Job Post Status">
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <Anthem:Label ID="lnkApprovedStatus" runat="server" CssClass="DataGriditemLink" Text='<%# Convert.ToString(Eval("IsApprovedByPCell")).Length>0? Convert.ToBoolean(Eval("IsApprovedByPCell"))==true? "Approved":"Rejected" :"Pending" %>'>
                                        </Anthem:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EDIT">
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <Anthem:LinkButton ID="lnkEdit" Visible='<%#Convert.ToString(Eval("IsApprovedByPCell")).Length>0?false:true %>' runat="server" CssClass="DataGriditemLink" CommandName="SELECT" CommandArgument='<%# Eval("Pk_JobPostedId") %>'>
                                        <img src="../Images/Edit.gif" alt="" border="0"></img>
                                        </Anthem:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </Anthem:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>