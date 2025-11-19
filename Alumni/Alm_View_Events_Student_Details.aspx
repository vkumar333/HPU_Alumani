<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_View_Events_Student_Details.aspx.cs" Inherits="Alumni_Alm_View_Events_Student_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .bg1 {
        z-index: -1;
        
        }
    </style>
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <!-- Courses Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-md-12">
                    <asp:Repeater ID="RepeventsAll" runat="server">
                        <ItemTemplate>
                            <div>
                                <div class="single-image">
                                    <img id="Imge" height="300" width="500" src='<%# Eval("Filepath") %>' alt="event">
                                </div>
                                <h5 style="margin: 10px 0; padding-top: 10px; text-decoration: underline; font-size: 1.25rem; color: #0a433d;"><%# Eval("Event_name") %></h5>
                                <span class="date">
                                    <i class="fa fa-calendar" aria-hidden="true"></i><%# Eval("Start_date") %>
                                </span>
                                <h5 style="margin: 10px 0; padding-top: 10px; text-decoration: underline; color: #0a433d;">Description</h5>
                                <p style="text-align: justify; margin-top: 0; margin-bottom: 1rem;">
                                    <%# Eval("Description") %>
                                </p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div style="border: 1px solid #ddd; padding: 15px 15px 15px 15px;">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <span>You Can Share It : </span>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <ul style="float: right;">
                                    <%--  <li style="float: left; list-style: none;">
                                        <a href="#" style="background: #3b5998; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;">--%>

                                    <%--  <asp:LinkButton ID="lnkfb"  runat="server"  aria-hidden="true"></asp:LinkButton> </a></li>--%>
                                    <li style="float: left; list-style: none;"><a href="https://www.facebook.com/HpuNewShimla" style="background: #3b5998; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-facebook" aria-hidden="true"></i>Facebook</a></li>
                                    <li style="float: left; list-style: none;"><a href="https://twitter.com/hpu_shimla?t=lRks6KUPBVeBjXTZgzNs_Q&s=08" style="background: #00acee; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-twitter" aria-hidden="true"></i>Twitter</a></li>
                                    <li style="float: left; list-style: none;"><a href="https://in.linkedin.com/school/himachal-pradesh-university/" style="background: #0A66C2; font-size: 13px; font-weight: 400; color: #fff; padding: 7px 8px; border: 1px solid #ddd; border-radius: 4px; margin-left: 14px;"><i class="fa fa-linkedin" aria-hidden="true"></i>Linkedin</a></li>
                                </ul>
                            </div>
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
    <!-- event End -->

    <!-- Main content End -->
</asp:Content>
