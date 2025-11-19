<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_AlumniChangeMobileNo.aspx.cs" Inherits="Alumni_ALM_AlumniChangeMobileNo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../include/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#ctl00_ContentPlaceHolder1_btnSave").click(function () {



                if ($("#ctl00_ContentPlaceHolder1_R_txtRegNo").val() == '') {
                alert('Registration No. is Required');
                $("#ctl00_ContentPlaceHolder1_R_txtRegNo").focus();
                return false;
            }
            if ($("#ctl00_ContentPlaceHolder1_R_txtNewMobileNo").val() == '') {
                alert('New Mobile No. is Required');
                $("#ctl00_ContentPlaceHolder1_R_txtNewMobileNo").focus();
                return false;
            }

             if ($("#ctl00_ContentPlaceHolder1_R_txtNewMobileNo").val().length < 10) {
                alert('Mobile No is not Valid');
                $("#ctl00_ContentPlaceHolder1_R_txtNewMobileNo").focus();
                return false;
            }


            }));

            //if ($("#ctl00_ContentPlaceHolder1_btnReset").click(function () {

            //    $("#ctl00_ContentPlaceHolder1_R_txtNewMobileNo").val('');
            //    $("#ctl00_ContentPlaceHolder1_lblMobleNoMsg").text('');
            //    $("#ctl00_ContentPlaceHolder1_R_txtNewMobileNo").focus();



            //}));


        });
    </script>
    <div class="col-md-12">
        <div class="row">

            <div class="box box-success">
                <div class="boxhead">
                    Request To Change The Mobile No.
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Registration No.</label>
                            <div class="col-sm-3 required">
                                <Anthem:TextBox ID="R_txtRegNo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True"
                                    MaxLength="20" Enabled="false"></Anthem:TextBox>*
                            </div>
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">New Mobile No.</label>
                            <div class="col-sm-3">
                                <Anthem:TextBox ID="R_txtNewMobileNo" runat="server" CssClass="form-control" AutoUpdateAfterCallBack="True"
                                    AutoCallBack="true"
                                    SkinID="textboxlong" ondrop="return false" onpaste="return false" ondrag="return false"
                                    onkeydown="return IntegerOnly(event,this);"
                                    MaxLength="10" OnTextChanged="R_txtNewMobileNo_TextChanged"></Anthem:TextBox>*

                  <Anthem:Label ID="lblMobleNoMsg" ForeColor="Red" runat="server" AutoUpdateAfterCallBack="true" SkinID="lblmessage"
                      AssociatedControlID="R_txtNewMobileNo"></Anthem:Label>
                            </div>
                        
                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row"> 
                            <div class="col-sm-10 col-sm-offset-2">
                                <Anthem:Button ID="btnSave" runat="server" CssClass="btn btn-warning btn-sm" OnClick="btnSave_Click" AutoUpdateAfterCallBack="true"
                                PreCallBackFunction="btnSave_PreCallBack" Text="REQUEST" />
                            <Anthem:Button ID="btnReset" runat="server" CssClass="button" Text="RESET" OnClick="btnReset_Click" AutoUpdateAfterCallBack="true" EnableCallBack="false" />
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </div> 
                        </div>
                    </div>


                </div>
            </div>


            <%--  <div class="box box-success">--%>
           
            <table class="table">
                <tr>
                    <td class="tablesubheading" colspan="4">Request Status</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div class="gridiv">
                            <Anthem:GridView ID="gvDetails" runat="server" AutoUpdateAfterCallBack="true" PageSize="10"
                                AllowPaging="true" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvDetails_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="false">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="New Mobile No." DataField="NewMobileNo" ItemStyle-Font-Bold="false">
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                        <ItemStyle VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="true" Font-Italic="true" Font-Underline="true" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Requested Date" DataField="RequestedAt" ItemStyle-Font-Bold="false">
                                        <ItemStyle HorizontalAlign="Left" Width="65%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Changed Date" DataField="MobileNoUpdatedAt" ItemStyle-Font-Bold="false">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="false" Font-Italic="true" Font-Underline="false" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Convert.ToBoolean(Eval("IsRequestDone").ToString())==false?"Pending":"Changed" %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <%-- <asp:BoundField HeaderText="Status" DataField="IsRequestDone"  ItemStyle-Font-Bold="false">
                            <itemstyle horizontalalign="Center" width="15%" />
                            <itemstyle verticalalign="Top" />
                            <itemstyle font-bold="false" font-italic="true" font-underline="false" />
                        </asp:BoundField>--%>
                                </Columns>
                            </Anthem:GridView>
                        </div>
                    </td>
                </tr>
            </table>

        </div>
    </div>
    <%-- </div>--%>
</asp:Content>

