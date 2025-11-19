<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_show_Registration_Certificate.aspx.cs" Inherits="Alumni_Alm_show_Registration_Certificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">registration-certificate</h1>
                        <!---<ul>
                            <li>
                                <a class="active" href="index.html">Home</a>
                            </li>
                            <li>Events</li>
                        </ul>--->
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_Default.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="sec-title mb-20 md-mb-30">
                        <h2 class="title mb-20">Registration Certificate of HPU AA</h2>
                    </div>
                </div>
                <%--  <div class="col-md-6">
                    <div style="float: right;">
                        <a class="readon2 banner-style" href=""><i class="fa fa-arrow-left" aria-hidden="true"></i>Back</a>
                    </div>
                </div>--%>
            </div>
            <div class="row">
                <asp:Repeater ID="Reppdf" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-12 col-md-12">
                            <%-- <object data='<%# Eval("Filepath") %>' id="pdf" runat="server" type="application/pdf" width="100%" height="800px"></object>--%>

                            <iframe id="if1" src='<%# Eval("Filepath") %>' width="100%" height="800px"></iframe>

                            <div class="text-center">
                                <a class="readon2 banner-style" href='<%# Eval("Filepath") %>'>Download <i class="fa fa-file-pdf-o" aria-hidden="true"></i></a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
