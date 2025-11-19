<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ALM_Published_Jobs.aspx.cs" MasterPageFile="~/AlumniMasterPage.master" Inherits="Alumni_ALM_Published_Jobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../include/jquery-1.2.1.min.js"></script>
    <style>
        .rs-blog {
                background: #ffffff;
            }
        
    </style> 
    <script type="text/javascript">

        //to bind all the Records on Home page

        //$(document).ready(function () {
        //    debugger;
        //    GetCurrentVacancy();
        //    $("[name='AnchVacncy']").click(function () {
        //        Get_ClickedElement_Dtls(($(this).attr("id")), "V");
        //        return false;
        //    });
        //});

        function GetCurrentVacancy() {
            debugger;
            $.ajax({
                url: 'ALM_Approved_Jobs.aspx/GetCurrentVaCancy',
                type: "POST",
                contentType: 'application/json; charset-utf-8',
                async: false,
                success: function (data) {

                    $("#inner-content-div5").html('');
                    for (var i = 0; i < data.d.length; i++) {
                        $("#inner-content-div5").append("<div class='comment-text global_bullet'><span class='username'><a name='AnchVacncy' id=" + data.d[i].Pk_JobPostedId + " href='Alm_ViewPublishedJobs.aspx?id=" + data.d[i].Pk_JobPostedId + "'>" + data.d[i].CompanyName + " &nbsp;For&nbsp;  " + data.d[i].Designation + " &nbsp;Opening From  " + data.d[i].OpenFrom + " To  " + data.d[i].OpenTo + "</a> </span></div>");
                    }
                },
                error: function (xhr) {

                }
            });
        }

        //function hideDive() {
        //    $("#DivVancyPopUp").css({ "display": "block" });
        //}

    </script>

    <%--    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Vacancies/Jobs </h1>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <!-- Breadcrumbs End -->
    <%-- <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="6" class="tablesubheading">Lists of Vaccancies </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvVaccancies" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" Width="100%" UpdateAfterCallBack="True" OnRowCommand="gvVaccancies_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblCompID" runat="server" Text='<%# Eval("CompanyName") %>' AutoUpdateAfterCallBack="True"></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Designation" HeaderText="Designation">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="JobOpenFrom" HeaderText="Job Apply From">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="JobOpenTo" HeaderText="Last Date for Applying for a Job">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Details">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkApplyJobs" Text="View" CausesValidation="False" CommandArgument='<%# Eval("Pk_JobPostedId") %>' TextDuringCallBack="Loading..." CommandName="Apply" OnClick="lnkApplyJobs_Click" AutoUpdateAfterCallBack="True">
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </Anthem:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>--%>

    <%--    <div id="DivVancyPopUp" class="white_content-new-1" style="display: none;">
        <div class="popupboxouter">
            <div class="popupbox" style="height: auto; overflow-y: scroll">
                <div onclick="document.getElementById('DivVancyPopUp').style.display='none';document.getElementById('fade').style.display='block';" class="close-1">
                    X  
                </div>
                <table id="tbl" class="table mobile_form">
                </table>
            </div>
        </div>
    </div>--%>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Vaccancies </h1>
                         <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <!-- Vaccancies Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container-fluid">
            
            <div class="box box-success" runat="server">
                <%--<div class="boxhead">
                    Lists of Jobs Posted By Alumni
                </div>--%>
                <div class="panel-body pnl-body-custom">
                    <div class="">
                        <div id="rs-blog" class="rs-blog main-home pb-70 md-pt-70 md-pb-70">
                            <div class="row">
                                <asp:Repeater ID="vacanciesRepeater" runat="server">
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
                                                            <a name="AnchVacncy" href="../Alumni/ALM_PublishedJobs_Details.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
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
                </div>                
            </div>

        </div>
    </div>
    <!-- Vaccancies End -->
</asp:Content>