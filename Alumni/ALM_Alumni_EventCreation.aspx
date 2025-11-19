<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Alumni_EventCreation.aspx.cs" Inherits="Alumni_ALM_Alumni_EventCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../include/jquery.min.js"></script>

    <script type="text/javascript">
        //$(document).ready(function () {

        //    $("#spnShowHide").hide();
        //    $("#ctl00_ContentPlaceHolder1_R_txtEventCharge").hide();
        //    $("#ctl00_ContentPlaceHolder1_ChkPaidOrNot").on("click", function () {

        //        if($(this).is(":checked"))
        //        {
        //            //alert('checked');
        //            $("#ctl00_ContentPlaceHolder1_R_txtEventCharge").show();
        //            $("#spnShowHide").attr('style', 'display:block margin-left:10px');

        //        }
        //        else {
        //           // alert('not checked');
        //            $("#ctl00_ContentPlaceHolder1_R_txtEventCharge").hide();
        //            $("#spnShowHide").hide();
        //        }
        //    });

        //    $("#ctl00_ContentPlaceHolder1_btnSave").click(function () {

        //        $("#ctl00_ContentPlaceHolder1_ChkPaidOrNot").on("click", function () {

        //            if ($(this).is(":checked")) {
        //                //alert('checked');
        //                if ($("#ctl00_ContentPlaceHolder1_R_txtEventCharge").val() == "")
        //                {
        //                    alert("Event Charge is Required");
        //                    $("#ctl00_ContentPlaceHolder1_R_txtEventCharge").focus();
        //                    return false;

        //                }


        //            }

        //        });

        //    });
        //});
    </script>

    <table class="table mobile_form" width="100%">
        <tr>
            <td id="Td1" colspan="7" class="tableheading" runat="server" style="height: 19px">Alumni Event Creation </td>
        </tr>
        <tr>
            <td colspan="7" class="tdgap"></td>
        </tr>
        <tr>
            <td class="vtext">Event Name</td>
            <td class="colon" style="width: 2%">:</td>
            <td class="required" style="width: 10%; margin-left: 640px;">
                <Anthem:TextBox ID="R_txtEventTitle" runat="server" MaxLength="250" SkinID="textboxlong" AutoUpdateAfterCallBack="true" TabIndex="1" onkeypress="return CheckMaxLength(this, 250);" onkeyup="return CheckMaxLength(this, 250);" onpaste="return CheckMaxLength(this, 250);" ondrop="return CheckMaxLength(this, 250);" ></Anthem:TextBox>*
            </td>
            <td class="vtext" style="width: 15%">Event Description</td>
            <td class="colon" valign="top">:</td>
            <td class="required" style="width: 15%; margin-left: 40px;">
                <Anthem:TextBox ID="R_txtEventDescription" runat="server" MaxLength="500" SkinID="textboxmultiline" TextMode="MultiLine" AutoUpdateAfterCallBack="true" Height="90px" Width="250px" TabIndex="2" onkeypress="return CheckMaxLength(this, 500);" onkeyup="return CheckMaxLength(this, 500);" onpaste="return CheckMaxLength(this, 500);" ondrop="return CheckMaxLength(this, 500);"></Anthem:TextBox>*
            </td>
        </tr>
        <tr>
            <td id="lblStartDate" class="vtext" style="width: 10%">Start Date</td>
            <td class="colon">:</td>
            <td class="required" style="width: 10%">
                <Anthem:TextBox ID="V_txtStartDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;"
                    MaxLength="10" onkeydown="return false" SkinID="textboxdate" Style="width: 95px !important;" TabIndex="3"></Anthem:TextBox><a
                        href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;"><img
                            alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>'
                            width="34" /></a>*</td>


            <td id="lblEndDate" class="vtext" style="width: 15%">End Date</td>
            <td class="colon">:</td>
            <td class="required" style="width: 15%">
                <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;"
                    MaxLength="10" onkeydown="return false" SkinID="textboxdate" Style="width: 95px !important;" TabIndex="5"></Anthem:TextBox><a
                        href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;"><img
                            alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>'
                            width="34" /></a>*</td>
        </tr>
        <tr>
            <td class="vtext">Address/Location</td>
            <td class="colon" style="width: 2%">:</td>
            <td class="required" style="width: 10%">
                <Anthem:TextBox ID="TextAddress" runat="server" MaxLength="250" SkinID="textboxlong" AutoUpdateAfterCallBack="true" TabIndex="7" onkeypress="return CheckMaxLength(this, 250);" onkeyup="return CheckMaxLength(this, 250);" onpaste="return CheckMaxLength(this, 250);" ondrag="return false;" ondrop="return false;"></Anthem:TextBox>*</td>

            <td class="vtext" style="width: 15%">File</td>
            <td class="colon" valign="top">:</td>
            <td class="required" style="width: 10%">

                <Anthem:FileUpload ID="flUploadLogo" AutoUpdateAfterCallBack="true" runat="server" TabIndex="8" />
                &nbsp;
                             <span style="font-weight: normal">File size shouldn’t be greater than 2 Mb
                          <br />
                                 format type. as in (PNG, JPEG, JPG)</span>
                <Anthem:LinkButton runat="server" ID="lnkviewBrc" CausesValidation="False"
                    AutoUpdateAfterCallBack="True" OnClick="lnkviewBrc_Click"> </Anthem:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="vtext">IsActive</td>
            <td>
                <Anthem:CheckBox ID="Chk_IsActive" runat="server" AutoUpdateAfterCallBack="True" AutoCallBack="true" TabIndex="9" />
            </td>
        </tr>
        <tr>
            <td colspan="3" class="tdgap"></td>
        </tr>
        <tr style="text-align: center">
            <td colspan="6">
                <Anthem:Button ID="btnSave" runat="server" Text="SAVE"
                    CommandName="SAVE" OnClick="btnSave_Click" AutoUpdateAfterCallBack="true" EnableCallBack="false" PreCallBackFunction="btnSave_PreCallBack" TabIndex="10" />
                <Anthem:Button ID="btnReset" runat="server" Text="RESET"
                    OnClick="btnReset_Click" AutoUpdateAfterCallBack="true" TabIndex="11" />
                <asp:Label ID="lblMsg" runat="server" CssClass="lblmessage" />
            </td>
        </tr>
        <tr>
            <td colspan="7"></td>
        </tr>
        <tr>
            <td id="Td2" colspan="7" class="tablesubheading" runat="server">List of Events</td>
        </tr>
        <tr>
            <td colspan="7">
                <Anthem:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" Width="100%" AutoUpdateAfterCallBack="true"
                    PageSize="10" AllowPaging="true" OnRowCommand="gvDetails_RowCommand" OnPageIndexChanging="gvDetails_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Guest Category Id" Visible="false">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblEventsId" runat="server" AutoUpdateAfterCallBack="true" Text='<%# Eval("PK_Events_id") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Event name">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:Label ID="lbleventname" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Event_name") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblStartDate" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Start_date") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblEndDate" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("End_date") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblCreatedDate" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("CreatedDate") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Event Posted By">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblEventPostedBy" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("alumniName") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Is Active">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblIsActive" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("IsActive") %>'></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="EDIT">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:LinkButton ID="lnkbtnEdit" TextDuringCallBack="WAIT.." runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="EDITREC" CommandArgument='<%# Eval("PK_Events_id") %>'> <img src="../../Images/Edit.gif" alt="" border="0"></img></Anthem:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DELETE">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <Anthem:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Are you sure to delete this record?');" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="DELETEREC" CommandArgument='<%# Eval("PK_Events_id") %>'> <img src="../../Images/Delete.gif" alt="" border="0"></img></Anthem:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </Anthem:GridView>
            </td>
        </tr>
    </table>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>