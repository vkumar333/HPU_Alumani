<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Alumni_VacanciesLists.aspx.cs" Inherits="Alumni_ALM_Alumni_VacanciesLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        function ShowEvent(obj) {
            document.getElementById('VacancyDiv').style.display = 'block';
        }
        function HideSearch(obj) {
            document.getElementById('VacancyDiv').style.display = 'none';
        }
    </script>

    <div class="container-fluid mt-10">
        <div class="">
            <div class="box-success" runat="server">
                <div class="boxhead">
                    Lists of Vacancies Posted By Alumni
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="">
                        <div id="rs-blog" class="rs-blog main-home pb-70 md-pt-70 md-pb-70" style="background: none;">
                            <asp:Repeater ID="newsVacanciesRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3">                                        
                                        <div class="blog-item">
                                            <div class="blog-content cust-cnt custm-height-newvac">
                                                <h3 class="title">
                                                    <%# Eval("Designation")%>
                                                </h3>
                                                <div class="desc">
                                                    <ul>
                                                        <li>Skills Required:- <span><%# Eval("SkillsReq")%></span></li>
                                                        <li>Selection Process:- <span><%# Eval("SelectionProcess")%> </span></li>
                                                        <li>Job Vacancy URL:- <span><%# Eval("JobVacncyUrl")%></span></li>
                                                    </ul>
                                                </div>
                                                <div class="btn-btm btm-cut">
                                                    <div class="rs-view-btn">
                                                        <a name="AnchVacncy" href="Alm_ViewPublishedJobs.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="col-lg-1 col-md-12">
                    <div class="back-btn">
                        <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="VacancyDiv" class="white_content-new-1" style="display: none">
        <div class="popupboxouter">
            <div class="popupbox">
                <div onclick="document.getElementById('EventDiv').style.display='none';" class="close-1">
                    X
                </div>
                <div class="table-responsive">
                    <%--<table class="mobile_form" width="100%">
                        <tr>
                            <td id="Td1" colspan="6" class="boxhead" runat="server" style="height: 19px">Add News  </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td class="vtext">Categories </td>
                            <td class="colon" style="width: 2%">: </td>
                            <td class="required" style="width: 10%">
                                <Anthem:DropDownList ID="D_ddlCategories" CssClass="dropdownlong" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="true" />
                                *
                            </td>
                            <td class="vtext" style="width: 15%">Heading </td>
                            <td class="colon" valign="top">: </td>
                            <td class="required" style="width: 15%">
                                <Anthem:TextBox ID="text_heading" runat="server" AutoUpdateAfterCallBack="True" TextMode="SingleLine" SkinID="textbox"></Anthem:TextBox>
                                *
                            </td>
                        </tr>
                        <tr>
                            <td class="vtext">Description </td>
                            <td class="colon" style="width: 2%">: </td>
                            <td class="required" style="width: 10%">
                                <Anthem:TextBox ID="R_txtDiscription" runat="server" AutoUpdateAfterCallBack="True" TextMode="SingleLine" SkinID="textbox"></Anthem:TextBox>*
                            </td>
                            <td class="vtext" style="width: 15%">Photo Uploads </td>
                            <td class="colon" valign="top">: </td>
                            <td class="required" style="width: 10%">
                                <Anthem:FileUpload ID="flUploadLogo" AutoUpdateAfterCallBack="true" runat="server" />
                                <span style="font-weight: normal">File size shouldn’t be greater than 5 Mb
                                    format type. as in (PNG, JPEG, JPG)*
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="vtext">Set On Album </td>
                            <td class="colon" style="width: 2%">: </td>
                            <td class="required" style="width: 10%" colspan="4">
                                <Anthem:CheckBox ID="chkhomepage" runat="server" AutoUpdateAfterCallBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="tdgap"></td>
                        </tr>
                        <tr style="text-align: center">
                            <td colspan="6">
                                <Anthem:Button ID="btnSave" runat="server" AutoUpdateAfterCallBack="true"
                                    CommandName="SAVE" OnClick="btnSave_Click" Text="SAVE" EnableCallBack="false" CausesValidation="true" />
                                <Anthem:Button ID="btnReset" runat="server" CausesValidation="False" Text="RESET" TextDuringCallBack="RESETING.." AutoUpdateAfterCallBack="True" OnClick="btnReset_Click" />
                                <Anthem:Label ID="Label1" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6"></td>
                        </tr>
                    </table>--%>
                </div>
            </div>
        </div>
    </div>
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;">
    </iframe>
</asp:Content>