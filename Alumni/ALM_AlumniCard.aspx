<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_AlumniCard.aspx.cs" Inherits="Alumni_ALM_AlumniCard" %>

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

            .alumni-issue-card {
                display: none;
            }
        }

        #alumniCard {
            display: block; /* Ensures proper rendering */
            overflow: hidden; /* Prevents extra background */
            background: white; /* Forces a clean background */
            padding: 0;
            margin: 0 auto; /* Centers the card */
            max-width: 400px; /* Prevents excessive width */
            box-shadow: none; /* Avoids capturing unnecessary shadows */
        }

        .card {
            border: none; /* Removes borders that may add extra space */
            padding: 0;
            margin: 0;
            width: 100%; /* Ensures no extra width */
        }

        .img-responsive {
            max-width: 100%;
            height: auto;
        }

        .footer {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
    </style>

    <script type="text/javascript">

        function showPopUpCard() {
            debugger;
            document.getElementById('SearchDiv').style.display = 'block';
        }

    </script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/dom-to-image-more@2.9.4/dist/dom-to-image-more.min.js"></script>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>--%>

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

    <div id="GFG" class="mt-60">
        <div class="container">
            <div class="">
                <div class="row">
                    <div class="col-md-6 mt-2 mb-2">
                        <div class="alumni-issue-card">
                            <h3>Digital Alumni Card</h3>
                            <h4>Introducing Himachal Pradesh University Alumni Card</h4>
                            <Anthem:Label ID="lblAleardyAppliedForCard" AutoUpdateAfterCallBack="true" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"></Anthem:Label>
                            <br />
                            <br />
                            <asp:Button ID="btnIssueCard" class="btn btn-primary custom-primary-color mt-2" OnClick="btnIssueCard_Click" Text="Apply For Alumni Card" runat="server" Enabled="false" Visible="false" />
                            <Anthem:Button ID="btnDownloadCard" class="btn btn-primary custom-primary-color mt-2" OnClick="btnDownloadCard_Click" Text="Download" runat="server" Enabled="false" Visible="false" AutoUpdateAfterCallBack="true" EnableCallBack="false" />
                            <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" CssClass="lblmessage" Visible="false"></Anthem:Label>
                            <Anthem:Label ID="lblAlumniCard" AutoUpdateAfterCallBack="true" runat="server" Visible="true"></Anthem:Label>

                            <Anthem:Button ID="btnCardDownload" class="btn btn-primary custom-primary-color mt-2" OnClick="btnCardDownload_Click" Text="Download" runat="server" Enabled="true" AutoUpdateAfterCallBack="true" EnableCallBack="false" TextDuringCallBack="Downloading..." Visible="false" />
                            <br />
                            <Anthem:Label ID="lblPhotoUnAvailable" AutoUpdateAfterCallBack="true" runat="server" Visible="true" Text="* Note: If alumni profile photo not available, candidate can not apply for I-Card." ForeColor="Red"></Anthem:Label>
                        </div>
                    </div>
                    <div id="alumniCard1" class="col-md-6 mt-2 mb-2" runat="server">
                        <div class="card" id="alumniCard" runat="server">
                            <div class="header">
                                <!-- Corrected img tag -->
                                <img id="alumnilogo" src='../alumni/alumin-default-theme/images/alumni-card-logo.jpg' class="img-responsive" alt="Alumni Association Logo" runat="server" autoupdateaftercallback="true">
                            </div>
                            <div class="avatar">
                                <img id="Imge1" src='<%# Eval("FileUrl") %>' class="img-responsive" alt="Alumni Image" runat="server" autoupdateaftercallback="true" />
                            </div>
                            <div class="details">
                                <h2>
                                    <Anthem:Label ID="lblAlumni" AutoUpdateAfterCallBack="true" runat="server"></Anthem:Label>
                                </h2>
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
                                <span id="qrPlaceholder" style="display: none; float: right; color: gray; font-size: 12px;">QR Code not available</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="SearchDiv" class="white_content-new-1" style="display: none;">
        <div class="popupboxouter">
            <div class="popupbox">
                <div onclick="document.getElementById('SearchDiv').style.display = 'none';" class="close">
                    X
                </div>
                <div class="">
                    <div class="">
                        <div class="">
                            <div class="custom-center">
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">
        function printContent() {
            var content = document.getElementById('<%= alumniCard.ClientID %>');

            if (content) {
                var scaleFactor = 3;

                domtoimage.toPng(content, {
                    width: content.offsetWidth * scaleFactor,
                    height: content.offsetHeight * scaleFactor,
                    style: {
                        transform: "scale(" + scaleFactor + ")",
                        transformOrigin: "top left",
                        width: content.offsetWidth + "px",
                        height: content.offsetHeight + "px",
                        background: "white"
                    }
                }).then(function (dataUrl) {
                    var link = document.createElement('a');
                    link.href = dataUrl;
                    link.download = 'AlumniCard.png';
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                }).catch(function (error) {
                    console.error('Error capturing image:', error);
                });
            } else {
                alert("Element alumniCard not found.");
            }
        }

        window.onload = function () {
            document.getElementById("btnDownloadCard").onclick = function () {
                setTimeout(printContent, 800);
            };
        };
    </script>

</asp:Content>