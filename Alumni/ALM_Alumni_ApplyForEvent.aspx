<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Alumni_ApplyForEvent.aspx.cs" Inherits="Alumni_ALM_Alumni_ApplyForEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="row">

            <div class="box-body table-responsive">
                <table  class="table mobile_form">
                    <tr>
                        <td id="Td1" colspan="3" class="tableheading" runat="server" style="height: 19px">Event Payment Details</td>
                    </tr>
                    <tr>
                        <td colspan="3" class="tdgap"></td>
                    </tr>
                    <tr>
                        <td class="vtext" id="lblName" style="width: 35%">Event Name</td>
                        <td class="colon" style="width: 2%">:</td>
                        <td class="">
                            <Anthem:Label ID="lblEventName" runat="server" AutoUpdateAfterCallBack="true" Text=""></Anthem:Label>


                        </td>
                    </tr>



                    <tr>
                        <td class="vtext" id="lblDesignation" style="width: 35%">Event Charges</td>
                        <td class="colon" style="width: 2%">:</td>
                        <td class="">
                            <Anthem:Label ID="lblEventCharges" runat="server" AutoUpdateAfterCallBack="true" Text=""></Anthem:Label>

                        </td>
                    </tr>

                    <tr>
                        <td class="vtext" id="lblJobCat" colspan="2">&nbsp;</td>
                        <td>
                            <Anthem:Button ID="btnPay" runat="server" Text="Pay"
                                CommandName="SAVE" AutoUpdateAfterCallBack="true" OnClick="btnPay_Click" />&nbsp;&nbsp

                 <Anthem:Button ID="btnBack" runat="server" Text="Cancel"
                     CommandName="SAVE" AutoUpdateAfterCallBack="true" OnClick="btnBack_Click" />
                        </td>


                    </tr>

                </table>
            </div>
        </div>
    </div>
</asp:Content>

