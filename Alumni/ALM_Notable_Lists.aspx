<%@ Page Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Notable_Lists.aspx.cs" Inherits="Alumni_ALM_Notable_Lists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .rs-blog {
            background: none;
        }

        .box {
            box-shadow: none !important;
        }
    </style>
    <script type="text/javascript">
        function ShowEvent(obj) {
            document.getElementById('EventDiv').style.display = 'block';
        }
        function HideSearch(obj) {
            document.getElementById('EventDiv').style.display = 'none';
        }

        function checkValidation() {
            debugger;
            var eventTitle = document.getElementById('ctl00_ContentPlaceHolder1_R_txtEventTitle');
            var eventName = eventTitle.value;

            if (eventName == "") {
                alert('Event Name is Required!');
                document.getElementById('ctl00_ContentPlaceHolder1_R_txtEventTitle').focus();
                return false;
            }

            var eventDescription = document.getElementById('ctl00_ContentPlaceHolder1_R_txtEventDescription').value;
            if (eventDescription == "") {
                alert('Description is Required!');
                document.getElementById('ctl00_ContentPlaceHolder1_R_txtEventDescription').focus();
                return false;
            }

            var startDate = document.getElementById('ctl00_ContentPlaceHolder1_V_txtStartDate').value;
            if (startDate == "") {
                alert('Start Date is Required!');
                document.getElementById('ctl00_ContentPlaceHolder1_V_txtStartDate').focus();
                return false;
            }

            var endDate = document.getElementById('ctl00_ContentPlaceHolder1_V_txtEndDate').value;
            if (endDate == "") {
                alert('End Date is Required!');
                document.getElementById('ctl00_ContentPlaceHolder1_V_txtEndDate').focus();
                return false;
            }

            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(endDate)) {
                alert('Event Start Date Can not be greated than End Date.');
                document.getElementById('ctl00_ContentPlaceHolder1_V_txtStartDate').focus();
                return false;
            }

            var eventAddress = document.getElementById('ctl00_ContentPlaceHolder1_TextAddress').value;
            if (eventAddress == "") {
                alert('Address is Required!');
                document.getElementById('ctl00_ContentPlaceHolder1_TextAddress').focus();
                return false;
            }

            var fileInput = document.getElementById("flUploadLogo");

            // Check if file is selected
            if (fileInput.files.length === 0) {
                return true;
            }

            var file = fileInput.files[0];
            var fileSize = file.size; // in bytes
            var fileName = file.name;
            var fileType = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();

            // Convert fileSize to KB and round it
            var fileSizeKB = Math.round(fileSize / 1024);

            // Check file name length
            if (fileName.length > 100) {
                alert("Upload Document Should not be more than 100 characters!");
                return false;
            }

            // Check file size (5 MB limit)
            if (fileSizeKB > (5 * 1024)) {
                alert("Upload Document is " + fileSizeKB + " KB, it should not be more than 5 MB !");
                return false;
            }

            // Check file type
            switch (fileType) {
                case 'jpg':
                case 'jpeg':
                case 'png':
                    return true; // Allowed file types
                default:
                    alert("Only files PNG, JPEG, JPG extension are allowed");
                    return false;
            }

            return true;
        }

    </script>

    <div class="container-fluid mt-0">
        <div class="">
            <div class="box box-success" runat="server">
                <div class="boxhead mt-10 mb-10">
                    Lists of Notable Alumni
                    <a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_Alumni_Home.aspx">Back </a>
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="">
                        <div id="rs-blog" class="rs-blog main-home pb-70 md-pt-70 md-pb-70">
                            <asp:Repeater ID="rptNotableAll" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3">
                                        <div class="blog-item">
                                            <div class="image-part">
                                                <asp:Image ID="imgCover" runat="server" ImageUrl='<%# Eval("PicUrl") %>' style="height:220px; width:100%;" />
                                            </div>
                                            <%--<div class="alumni-slider-thumb" style='<%# "background:url(" + Eval("PicUrl") + ") scroll no-repeat center center;" %>'></div>--%>
                                            <div class="blog-content">
                                                <h3 class="title">
                                                    <asp:Label ID="lblEvent" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    </a>
                                                </h3>
                                                <div class="btn-btm">
                                                    <div class="rs-view-btn">
                                                        <a name="anchEvents" href="ALM_Notable_Details.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
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
				 <%--<br />
                <div class="col-lg-1 col-md-12">
                    <div class="back-btn">
                        <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
                    </div>
                </div>
               <div class="col-lg-1 col-md-12">
                    <div class="back-btn">
                    </div>
                </div>--%>
            </div>
        </div>
    </div>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 9999; position: absolute; top: -500px; left: -500px;">
    </iframe>
</asp:Content>