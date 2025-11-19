<%@ Page Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alumni_NewsEvents_View.aspx.cs" Inherits="Alumni_Alumni_NewsEvents_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
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
            <div class="box-success" runat="server">
                <div class="boxhead mt-10 mb-10">
                    Events
					<a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_Alumni_Home.aspx">Back </a>
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="">
                        <div id="rs-blog" class="rs-blog main-home pb-70 md-pt-70 md-pb-70" style="background:none;">
                            <asp:Repeater ID="newsEventsRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3">
                                        <div class="blog-item custom-height">
                                            <div class="image-part">
                                                <asp:Image ID="imgCover" runat="server" ImageUrl='<%# Eval("File_name") %>' style="height:220px; width:100%;" />
                                            </div>
                                            <div class="blog-content">
                                                <h3 class="title" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="lblEvent" runat="server" Text='<%# Eval("Event_name") %>'></asp:Label>
                                                    </a>
                                                </h3>
                                                <div class="btn-btm">
                                                    <div class="rs-view-btn">
                                                        <a name="anchEvents" href="Alm_View_Events_student.aspx?id=<%# Eval("encId").ToString() %>">View More</a>
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
                <div class="col-lg-1 col-md-12">
                    <div class="back-btn">
                        <Anthem:LinkButton ID="lnkAddEvent" AutoUpdateAfterCallBack="true" EnableCallBack="true" OnClick="lnkAddEvent_Click" runat="server" data-toggle="tooltip" title="Add New Events"><img src="alumin-default-theme/images/add.png" width="18" height="18" data-toggle="tooltip" title="Add New Events" /></Anthem:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="EventDiv" class="white_content-new-1" style="display: none">
        <div class="popupboxouter">
            <div class="popupbox">
                <div onclick="document.getElementById('EventDiv').style.display='none';" class="close-1">
                    X
                </div>
                <div class="table-responsive">
                <table class="mobile_form" width="100%">
                    <tr>
                        <td id="Td1" colspan="6" class="boxhead" runat="server" style="height: 19px">Add Events  </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="tdgap"></td>
                    </tr>
                    <tr>
                        <td class="vtext">Event Name</td>
                        <td class="colon" style="width: 2%">:</td>
                        <td class="required" style="width: 10%">
                            <Anthem:TextBox ID="R_txtEventTitle" runat="server" MaxLength="100" SkinID="textboxlong" AutoUpdateAfterCallBack="true"></Anthem:TextBox>*
                        </td>
                        <td class="vtext" style="width: 15%">Event Description</td>
                        <td class="colon">:</td>
                        <td class="required" style="width: 15%">
                            <Anthem:TextBox ID="R_txtEventDescription" runat="server" MaxLength="250" SkinID="textboxmultiline" TextMode="Singleline" AutoUpdateAfterCallBack="true" Width="200px"></Anthem:TextBox> *
                        </td>
                    </tr>
                    <tr>
                        <td id="lblStartDate" class="vtext" style="width: 10%">Start Date </td>
                        <td class="colon">: </td>
                        <td class="required" style="width: 10%">
                            <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;"
                                MaxLength="10" onkeydown="return false" SkinID="textboxdate" Style="width: 95px !important;" TabIndex="4"></Anthem:TextBox><a
                                    href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;"><img
                                        alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>'
                                        width="34" /></a> * 
                        </td>
                        <td id="lblEndDate" class="vtext" style="width: 15%">End Date</td>
                        <td class="colon">:</td>
                        <td class="required" style="width: 15%">
                            <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;"
                                MaxLength="10" onkeydown="return false" SkinID="textboxdate" Style="width: 95px !important;" TabIndex="4"></Anthem:TextBox><a
                                    href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;"><img
                                        alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>'
                                        width="34" /></a>*</td>
                    </tr>
                    <tr>
                        <td class="vtext">Address/Location </td>
                        <td class="colon" style="width: 2%">: </td>
                        <td class="required" style="width: 10%">
                            <Anthem:TextBox ID="TextAddress" runat="server" MaxLength="100" SkinID="textboxlong" AutoUpdateAfterCallBack="true"></Anthem:TextBox> *
                        </td>
                        <td class="vtext" style="width: 15%">File </td> 
                        <td class="colon" align="top">: </td>
                        <td class="required" style="width: 10%">
                            <Anthem:FileUpload ID="flUploadLogo" AutoUpdateAfterCallBack="true" runat="server" />
                            &nbsp;
                             <span style="font-weight: normal">File size shouldn’t be greater than 5 Mb
                          <br />
                                 format type. as in (PNG, JPEG, JPG)</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="tdgap"></td>
                    </tr>
                    <tr style="text-align: center">
                        <td colspan="6">
                            <Anthem:Button ID="btnSave" runat="server" Text="SAVE" CommandName="SAVE" OnClick="btnSave_Click" AutoUpdateAfterCallBack="true" 
                                EnableCallBack="false" PreCallBackFunction="btnSave_PreCallBack" OnClientClick="return checkValidation();" CausesValidation="true" />
                            <Anthem:Button ID="btnReset" runat="server" Text="RESET" OnClick="btnReset_Click" AutoUpdateAfterCallBack="true" />
                            <asp:Label ID="lblMsg" runat="server" CssClass="lblmessage" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6"></td>
                    </tr>
                </table>
                </div>
            </div>
        </div>
    </div>
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 9999; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>