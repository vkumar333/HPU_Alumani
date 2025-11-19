<%@ Page Title="" Language="C#" MasterPageFile="../AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_ICard_Verification.aspx.cs" Inherits="Alumni_ALM_ICard_Verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .button-common {
            padding: 6px 20px;
        }

        .alumni-card-default {
            margin: 0 auto;
        }

        .custom-center {
            position: relative;
            display: block;
        }

        @media print {
            #Btnprint, .button-common, input#ctl00_ContentPlaceHolder1_Btnprint {
                display: none !important;
            }
        }
    </style>

    <script>
        function printDiv() {
            var divContents = document.getElementById("LiteralAlumniCard").innerHTML;
            var printWindow = window.open('', '', 'height=500, width=500');
            printWindow.document.write('<html><head><title>Print Alumni Card</title>');
            printWindow.document.write('<style>@media print { .no-print { display: none; } }</style>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<h1>Alumni Card</h1><br>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
	
    <style>
        .course-seats {
            margin: 0 !important;
        }

        .rs-courses-3 .course-footer {
            display: block;
        }

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

    <div id="GFG" class="alumni-card-GFG mt-60">
        <div class="container">
            <div class="">

                <div class="">

                    <Anthem:Panel ID="pnlICardNotVerified" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" Visible="false">
                        <div class="col-md-12 col-sm-12 col-xs-12 mt-4 mb-2">
                            <div class="card custom-card-alumni">
                                <div class="avatar">
                                    <img class="img-responsive" runat="server" src="../img/invalid-access.png" alt="" />
                                </div>
                                <div class="alumni-card-heading">
                                    <h6>Alumni Card could not be verified </h6>
                                </div>
                                <div class="details">
                                    <h2>Unable to verify this Card. This could be due
                                        <br />
                                        either user is blocked or deleted from your
                                        <br />
                                        plateform or you are scanning a wrong QR code.
                                    </h2>
                                </div>
                                <div class="custom-center">
                                    <Anthem:Button ID="btnScannerNV" runat="server" Text="Scan Again" OnClick="btnScannerNV_Click"
                                        AutoUpdateAfterCallBack="true" TextDuringCallBack="WAIT..." CssClass="btn btn-primary btn-sm custom-btn-scan" />
                                </div>
                            </div>

                        </div>
                    </Anthem:Panel>

                    <Anthem:Panel ID="pnlICardVerified" runat="server" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" Visible="false">
                        <div class="col-md-12 col-sm-12 col-xs-12 mt-4 mb-2">
                            <div class="card custom-card-alumni">
                                <div class="alumni-card-heading">
                                    <h6>Card Verified </h6>                                    
                                </div>
                                <div class="avatar">
                                    <img class="img-responsive" runat="server" src="../img/success.png" alt="" />
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
                                        Course/Degree :
                                    <Anthem:Label ID="lblDegree" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                    </p>
                                    <p>
                                        House :
                                    <Anthem:Label ID="lblDept" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                    </p>
                                    <p>
                                        Identity Number :
                                    <Anthem:Label ID="lblIdentityNo" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                    </p>
                                </div>
                                <div class="custom-center">
                                    <Anthem:Button ID="btnLogin" runat="server" Text="View Profile" OnClick="btnLogin_Click"
                                        AutoUpdateAfterCallBack="true" TextDuringCallBack="WAIT..." CssClass="btn btn-primary btn-sm" Style="margin-bottom:15px;" />
                                    <Anthem:Button ID="btnCardScannerV" runat="server" Text="Scan Again" OnClick="btnCardScannerV_Click"
                                        AutoUpdateAfterCallBack="true" TextDuringCallBack="WAIT..." CssClass="btn btn-primary btn-sm custom-btn-scan" />
                                </div>
                            </div>
                        </div>
                    </Anthem:Panel>
                </div>
                <div class="col-md-4">&nbsp;</div>

            </div>
        </div>
    </div>

</asp:Content>