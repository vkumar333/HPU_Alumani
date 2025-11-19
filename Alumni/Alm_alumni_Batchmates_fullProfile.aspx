<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_alumni_Batchmates_fullProfile.aspx.cs" Inherits="Alumni_Alm_alumni_Batchmates_fullProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <style>
        .course-seats {
            margin: 0 !important;
        }

        .rs-courses-3 .course-footer {
            display: block;
        }
    </style>
    <style>
        .font-weight-bold {
            font-weight: 500 !important;
        }

        .custom-text-left {
            text-align: left !important;
        }

        .modal-sm {
            width: 400px;
        }

        @media (max-width:769px) and (min-width:300px) {
            .modal-sm {
                width: 100%;
            }
        }

        .card {
            width: 290px;
            border: 1px solid #ccc;
            border-radius: 8px;
            overflow: hidden;
            background: white;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .header {
            background-color: #0a433d;
            color: white;
            text-align: center;
            padding: 10px;
            min-height: 120px;
        }

        .avatar {
            display: flex;
            justify-content: center;
            margin: 0;
            margin-top: -55px;
        }

            .avatar img {
                width: 100px;
                height: 100px;
                border-radius: 50%;
                border: 3px solid #fff;
            }

        .details {
            text-align: center;
            padding: 10px;
            margin-bottom: 15px;
        }

            .details h2 {
                margin: 10px 0 5px;
                font-size: 20px;
                font-weight: 500;
            }

            .details p {
                margin: 7px;
                color: #555;
                font-size: 13px;
                text-align: left;
            }

        .alumni-class {
            text-align: center !important;
            margin-bottom: 20px !important;
        }

        .footer {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px;
            background-color: #424242;
            border-top: 1px solid #ddd;
        }

            .footer p {
                margin: 0;
                font-size: 12px;
                color: #fff;
            }

            .footer img {
                width: 50px;
            }

        .img-responsive {
            display: block;
            max-width: 100%;
        }

        /* ===== Education Section Styling ===== */
        .education-section {
            position: relative;
            border-radius: 8px;
        }

        .edu-title {
            font-weight: 600;
            border-left: 3px solid #c33;
            padding-left: 8px;
            color: #0a3d62;
        }

        /* Timeline Line */
        .edu-timeline {
            border-left: 3px solid #0066ff;
            padding-left: 25px;
            margin-left: 10px;
        }

        /* Timeline Dot */
        .edu-dot {
            position: absolute;
            left: -9px;
            width: 14px;
            height: 14px;
            background-color: #0066ff;
            border-radius: 50%;
            top: 15px;
        }

        /* Education Card */
        .edu-card {
            transition: all 0.3s ease-in-out;
            border-left: 3px solid transparent;
        }

            .edu-card:hover {
                background-color: #eef3ff;
                border-left: 3px solid #0066ff;
                transform: translateY(-2px);
            }

        .edu-degree {
            font-weight: 600;
            color: #000;
        }

        .edu-subject {
            font-size: 14px;
            color: #555;
        }

        .edu-year {
            color: #007bff;
            font-weight: 600;
        }

        .edu-dept {
            font-size: 13px;
            color: #666;
        }

        /* Responsive Fix */
        @media (max-width: 767px) {
            .edu-timeline {
                padding-left: 15px;
                margin-left: 5px;
            }
        }

        /***** Style End *****/

        /* Print-specific styles */
        @media print {
            body {
                background-color: white;
            }

            .card {
                box-shadow: none;
                border: 1px solid #ddd;
            }

            .header {
                background-color: #0a433d !important;
                color: white;
                -webkit-print-color-adjust: exact;
            }

            .footer {
                background-color: #424242 !important;
                -webkit-print-color-adjust: exact;
            }

                .footer p {
                    font-size: 12px;
                    color: #fff;
                }
        }
    </style>

    <script>
        function openModal() {
            $('#member').modal('show');
        }
    </script>

    <!-- Breadcrumbs Start -->

    <div class="rs-breadcrumbs bg3 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Members</h1>
                        <div class="back-btn-custom pull-right">
                            <a href="../Alumni/Alm_alumni_Batchmates.aspx">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Breadcrumbs End -->

    <main>

        <div class="main-wrapper">
            <%--<div class="profile-banner-large bg-img" data-bg="alumin-default-theme/images/bg3.jpg" style="background-image: url('alumin-default-theme/images/bg3.jpg');">--%>
        </div>

        <div class="profile-menu-area bg-white">
            <div class="container">
                <div class="row align-items-center">

                    <div class="col-lg-3 col-md-3 mb-4">
                        <asp:Repeater ID="ReprofileImg" runat="server">
                            <ItemTemplate>
                                <div class="profile-picture-box mt-4" style="margin-left: 37px; height: 157px;">
                                    <figure class="profile-picture" style="width: 168px">
                                        <a href="<%# Eval("Filepath") %>">
                                            <img id="Imge" src='<%# Eval("Filepath") %>' alt="" width="157" height="182">
                                            <%--  <img src="alumin-default-theme/images/p1.jpg" alt="profile picture">--%>
                                        </a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </figure>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <%--<div class="col-lg-6 col-md-6">
                            <div class="profile-menu-wrapper">
                                <div class="main-menu-inner header-top-navigation">
                                    <nav>
                                        <ul class="main-menu">
                                            <li class="active"><a href="#"><i class="fa fa-graduation-cap" aria-hidden="true"></i> Education</a></li>
                                            <li><a href="#"><i class="fa fa-briefcase" aria-hidden="true"></i> Work experience</a></li>
                                        </ul>
                                    </nav>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-3 d-none d-md-block">
                            <div class="profile-edit-panel">
                                <button class="edit-btn">edit profile</button>
                            </div>
                        </div>--%>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-lg-3 order-2 order-lg-1">
                    <aside class="widget-area profile-sidebar">
                        <!-- widget single item start -->
                        <asp:Repeater ID="profileRepeater" runat="server">
                            <ItemTemplate>
                                <div class="pro-card widget-item">
                                    <h4 class="edu-title"><%#Eval("alumni_name") %></h4>
                                    <div class="widget-body">
                                        <div class="about-author">
                                            <p><i class="fa fa-id-badge" aria-hidden="true"></i> Class of <%#Eval("yearofpassing") %></p>
                                            <p><i class="fa fa-bookmark-o" aria-hidden="true"></i> <%#Eval("degree") %></p>
                                            <p><i class="fa fa-shield" aria-hidden="true"></i> <%#Eval("isAuthenticUser") %> </p>
                                            <%--<p><i class="fa fa-shield" aria-hidden="true"></i> <%#Eval("yearofpassing") %> </p>--%>
                                            <%--<asp:Button ID="lnkCard" CssClass="logbutt btn-block custo-year-button" OnClick="lnkCard_Click" Text='<%# "+   " +  Eval("lnkbtnText") ?? string.Empty %>' runat="server" Enabled="true" />--%>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <!-- widget single item end -->

                        <!-- widget single item start -->

                        <div class="pro-card widget-item">
                            <h4 class="edu-title">Basic Information</h4>
                            <asp:Repeater ID="basicdetailsRep" runat="server">
                                <ItemTemplate>
                                    <div class="widget-body">
                                        <div class="about-author">
                                            <p><i class="fa fa-map-marker" aria-hidden="true"></i> <%#Eval("Current_Location") %>, Himachal Pradesh</p>
                                            <p><i class="fa fa-mars-stroke" aria-hidden="true"></i> <%#Eval("gender") %></p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- widget single item end -->

                    </aside>
                </div>

                <div class="col-lg-6 order-1 order-lg-2">
                    <!-- widget single item start -->
                    <div class="pro-card widget-item">
                        <%-- <h4 class="widget-title">Education</h4>--%>
                        <%-- <asp:Repeater ID="RepEducation" runat="server">
                            <ItemTemplate>
                               <div class="post-content">
                                    <p class="post-desc pb-0">
                                        <strong>Himachal Pradesh University </strong>
                                        <br />
                                        <span>
                                            <%#Eval("degree") %>
                                            <br />
                                            <%#Eval("yearofpassing") %>
                                        </span>
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>--%>

                        <%--<div class="timeline">
                            <asp:Repeater ID="RepEducation" runat="server">
                                <ItemTemplate>
                                    <div class="timeline-item">
                                        <div class="timeline-dot"></div>
                                        <div class="timeline-content">
                                            <h5 class="edu-degree"><%# Eval("Degree") %></h5>
                                            <p class="edu-subject"><%# Eval("Subject") %></p>
                                            <p class="edu-year"><%# Eval("PassingYear") %></p>
                                            <p class="edu-department"><%# Eval("Department") %></p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>--%>

                        <div class="education-section p-4 bg-white shadow-sm rounded">
                            <h4 class="edu-title mb-4">Educational Information </h4>

                            <div class="edu-timeline position-relative">
                                <asp:Repeater ID="RepEducation" runat="server">
                                    <ItemTemplate>
                                        <div class="edu-item mb-2">
                                            <div class="edu-dot"></div>
                                            <div class="edu-card p-3 bg-light rounded">
                                                <%--<h5 class="edu-degree mb-1"><%# Eval("Degree") %></h5>
                                                <p class="edu-subject mb-1"><%# Eval("Subject") %></p>
                                                <p class="edu-year text-primary fw-bold mb-1"><%# Eval("PassingYear") %></p>
                                                <p class="edu-dept text-muted mb-0"><%# Eval("Department") %></p>--%>
                                                <h5 class="degree mb-1"><%# Eval("Degree") %></h5>
                                            <p class="subject"><%# Eval("Subject") %></p>
                                            <p class="year"><i class="fa fa-calendar"></i> <%# Eval("PassingYear") %></p>
                                            <p class="dept"><i class="fa fa-university"></i> <%# Eval("Department") %></p>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <%--<div class="DivStyleWithScroll" style="width: 100%; height: 250px;">
                            <div class="grid-div">
                                <table style="width: 100%;" class="new-table">
                                    <tr>
                                        <td align="left" colspan="4" style="display: block;">
                                            <Anthem:GridView ID="RepEducation" runat="server" AutoGenerateColumns="False" Width="100%" AutoUpdateAfterCallBack="true" PageSize="10" AllowPaging="true" ShowHeader="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="degree" HeaderText="Degree" SortExpression="degree" />
                                                    <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                                                    <asp:BoundField DataField="PassingYear" HeaderText="Passing Year" SortExpression="PassingYear" />
                                                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                                </Columns>
                                            </Anthem:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>--%>
                    </div>
                    <!-- widget single item end -->

                    <!-- widget single item start -->
                    <div class="pro-card widget-item">
                        <h4 class="edu-title">Work Experience</h4>
                        <asp:Repeater ID="workRepeater" runat="server">
                            <ItemTemplate>
                                <div class="post-content">
                                    <p class="post-desc pb-0">
                                        <%#Eval("special_interest") %>
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- widget single item end -->
                    <!-- post status start -->
                    <%--<div class="pro-card">
                            
                            <div class="post-title d-flex align-items-center">                                
                                <div class="profile-thumb">
                                    <a href="#">
                                        <figure class="profile-thumb-middle">
                                            <img src="alumin-default-theme/images/p11.jpg" alt="profile picture">
                                        </figure>
                                    </a>
                                </div>                               

                                <div class="posted-author">
                                    <h6 class="author"><a href="#">Mr. Tikam Ram</a></h6>
                                </div>

                            </div>
                            
                            <div class="post-content">
                                <p class="post-desc pb-0">
                                    <strong>Himachal Pradesh University</strong> <br/>
									<span>M.COM, Commerce <br/> 2016 - 2018</span>
                                </p>
                            </div>
                        </div>--%>
                    <!-- post status end -->

                </div>

                <div class="col-lg-3 order-3">
                    <aside class="widget-area">
                        <!-- widget single item start -->
                        <div class="pro-card widget-item" style="width: max-content;">
                            <h4 class="edu-title">Contact Information</h4>
                            <asp:Repeater ID="ContactRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="widget-body">
                                        <div class="about-author">
                                            <p><i class="fa fa-envelope" aria-hidden="true"></i> <%#Eval("email") %></p>
                                            <p><i class="fa fa-phone" aria-hidden="true"></i> <%#Eval("contactno") %></p>
                                            <p>
                                                <i class="fa fa-link" aria-hidden="true"></i>
                                                <a href="https://alumni.hpushimla.in/" target="_blank">https://alumni.hpushimla.in/</a>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- widget single item end -->

                        <!-- widget single item start -->
                        <div class="pro-card widget-item" style="width: max-content;">
                            <h4 class="edu-title">Membership </h4>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div class="widget-body">
                                        <div class="about-author">
                                            <p class="post-desc pb-0">
                                                <i class="fa fa-users" aria-hidden="true"></i>
                                                Membership: Life Membership
                                            </p>
                                            <%--   <p class="post-desc pb-0">
                                            <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                            Membership No.:  N/A
                                        </p>--%>
                                            <p class="post-desc pb-0">
                                                <i class="fa fa-star-o" aria-hidden="true"></i>
                                                Membership Status: Active
                                            </p>
                                            <%--   <p class="post-desc pb-0">
                                            <i class="fa fa-credit-card" aria-hidden="true"></i>
                                            Payment Mode: N/A
                                        </p>
                                        <p class="post-desc pb-0">
                                            <i class="fa fa-credit-card" aria-hidden="true"></i>
                                            Amount: N/A
                                        </p>--%>
                                            <p class="post-desc pb-0">
                                                <i class="fa fa-calendar-o" aria-hidden="true"></i>
                                                Join Date: <%#Eval("InsertedDate") %>
                                            </p>
                                            <p class="post-desc pb-0">
                                                <i class="fa fa-calendar-o" aria-hidden="true"></i>
                                                Expiry Date: Lifetime
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- widget single item end -->

                    </aside>
                </div>
            </div>
        </div>

        <div id="member" style="display: none !important" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-md">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Digital Alumni Card</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="row">
                        <div class="col-md-6 mt-2 mb-2">
                            <div class="alumni-issue-card">
                                <h3>Digital Alumni Card</h3>
                                <h4>Introducing Himachal Pradesh University Alumni Card</h4>
                                <p>Exclusive for HPU alumni</p>
                                <p>Carry it on your phone or mailbox</p>
                                <p>Equipped with Authentication QR-CODE</p>
                                <p>Required for Alumni-exclusive privileges</p>
                                <%--<button type="button" class="btn btn-primary custom-primary-color mt-2">Issue Alumni Card</button>--%>
                                <asp:Button ID="btnIssueCard" class="btn btn-primary custom-primary-color mt-2" OnClick="btnIssueCard_Click" Text="Issue Alumni Card" runat="server" Enabled="true" />
                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" CssClass="lblmessage" Visible="false"></Anthem:Label>
                            </div>
                        </div>
                        <div class="col-md-6 mt-2 mb-2">
                            <div class="card">
                                <div class="header">
                                    <img src="alumin-default-theme/images/alumni-card-logo.jpg" class="img-responsive" alt="Alumni Association Logo">
                                </div>
                                <div class="avatar">
                                    <img class="img-responsive" runat="server" id="Imge1" src='<%# Eval("FileUrl") %>' alt="" />
                                </div>
                                <div class="details">
                                    <h2>
                                        <Anthem:Label ID="lblAlumni" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label></h2>
                                    <p class="alumni-class">
                                        Alumni, Class of
                                    <Anthem:Label ID="lblPassingYear" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                    </p>
                                    <p>
                                        Course/Degree:
                                    <Anthem:Label ID="lblDegree" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                    </p>
                                    <p>
                                        Division/Department:
                                    <Anthem:Label ID="lblDept" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                    </p>
                                </div>
                                <div class="footer">
                                    <p>
                                        CARDID:
                                    <Anthem:Label ID="lblCardID" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                        <br>
                                        ALUMNI.HPUSHIMLA.IN
                                    </p>
                                    <img id="imgQR" autoupdateaftercallback="true" cssclass="img-responsive" runat="server" style="float: right; height: 70px; width: 70px;" />
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>

        <br />
        <br />

    </main>
</asp:Content>