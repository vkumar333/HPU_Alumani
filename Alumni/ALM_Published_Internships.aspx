<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ALM_Published_Internships.aspx.cs" MasterPageFile="~/AlumniMasterPage.master" Inherits="Alumni_ALM_Published_Internships" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../include/jquery-1.2.1.min.js"></script>
    <style>
        .rs-blog {
            background: #ffffff;
        }
    </style>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Internships </h1>
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
                                                            <li>Job Vacancy URL:- <span><%# Eval("JobVacancyUrl")%></span></li>
                                                        </ul>
                                                    </div>
                                                    <div class="btn-btm btm-cut">
                                                        <div class="rs-view-btn">
                                                            <a name="AnchVacncy" href="../Alumni/ALM_Internship_Details.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
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