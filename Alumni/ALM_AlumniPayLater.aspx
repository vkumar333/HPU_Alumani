<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ALM_AlumniPayLater.aspx.cs" MasterPageFile="~/Alumni/PendingAlumniMasterPage.master" Inherits="Alumni_ALM_AlumniPayLater" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="../../include/jquery.min.js"></script>

    <script>
        $(document).ready(function () {
            // Open the modal on page load
            $('#member').modal('show');
        });
    </script>
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-97553048-1', 'auto');
        ga('send', 'pageview');

    </script>
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
    </style>
	
    <div id="member" style="display: block !important" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                   <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Payment Link for Pending Alumni</h4>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="mb-4 mt-3">
                        <img class="img-responsive" src="alumin-default-theme/images/online-pay.jpg" />
                    </div>
                    <form id="contact-form" method="post" action="#">
                        <div class="form-group form-group-sm">
                            <label class="control-label col-sm-5 font-weight-bold">Name :</label>
                            <div class="control-label col-sm-7 custom-text-left">
                                <Anthem:Label ID="lblAlumni" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="control-label col-sm-5 font-weight-bold">Mobile No :</label>
                            <div class="control-label col-sm-7 custom-text-left">
                                <Anthem:Label ID="lblMobileNo" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="control-label col-sm-5 font-weight-bold">Email :</label>
                            <div class="control-label col-sm-7 custom-text-left">
                                <Anthem:Label ID="lblEmail" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="control-label col-sm-5 font-weight-bold">Alumni Type : </label>
                            <div class="control-label col-sm-7 custom-text-left">
                                <Anthem:Label ID="lblAlumniType" runat="server" AutoUpdateAfterCallBack="true">
                                </Anthem:Label>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="control-label col-sm-5 font-weight-bold">Membership : </label>
                            <div class="control-label col-sm-7 custom-text-left">
                                <Anthem:Label ID="lblMembership" runat="server" AutoUpdateAfterCallBack="true">
                                </Anthem:Label>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <div class="col-sm-12">
                                <Anthem:Button ID="lnkPay" CssClass="logbutt btn-block" Text="PAY NOW" runat="server" OnClick="lnkPay_Click" Enabled="true" />
                                <Anthem:Label ID="lblmessage" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </div>
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div class="row">

            <div class="table-responsive">

                <div>
                    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalScrollableTitle" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-dialog-scrollable modal-lg dialmodel" role="document">
                            <div class="modal-content" id="popup">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalScrollableTitle">Payment Link for Pending Alumni </h5>
                                </div>
                                <div class="modal-body">
                                    <div class="contact-page-section">
                                        <div class="container" style="width: 400px !important">

                                            <div class="contact-comment-section">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- <div class="gridiv">

                    <Anthem:GridView ID="dgv" runat="server" AutoGenerateColumns="False" EnableCallBack="false" AllowPaging="True" PageIndex="10" PageSize="10" OnRowCommand="dgv_RowCommand" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True">
                        <Columns>
                            <asp:TemplateField HeaderText="S.NO.">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NAME" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAlumni" runat="server" Text='<%# Eval("alumni_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MOBILE" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("contactno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="E-MAIL" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Alumni/ALM_AlumniProfileDetails.aspx?ID={0}", HttpUtility.UrlEncode(Eval("encId").ToString())) %>' Text="PAY NOW" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </Anthem:GridView>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />

        <div id="dvPay" runat="server">
            <div class="boxhead">Fee Details</div>
            <div class="form-group form-group-sm">
                <div class="row">
                    <div class="col-sm-10 col-sm-offset-2">
                        <Anthem:RadioButtonList ID="rdPaymentOption" runat="server" RepeatDirection="Horizontal" AutoCallBack="true" AutoUpdateAfterCallBack="true" OnSelectedIndexChanged="rdPaymentOption_SelectedIndexChanged" Height="22px">
                            <asp:ListItem Value="O"> <b>Online Payment</b></asp:ListItem>
                        </Anthem:RadioButtonList>
                        <Anthem:Panel ID="pnlOnlinePayment" runat="server" AutoUpdateAfterCallBack="true" Visible="false">
                            <div id="Table2">
                                <div class="form-group form-group-sm">
                                    <div class="row">
                                        <label class="col-sm-2 control-label">
                                            Details of Fee Deposited
                                                        <br />
                                            शुल्क का विवरण जमा</label>
                                        <div class="col-sm-3">
                                            <div class="fee-dtl">
                                                <Anthem:Label ID="lblOnlineFees" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group form-group-sm">
                                    <div class="row">
                                        <div class="col-sm-10 col-sm-offset-2">
                                            <Anthem:Label ID="lblPaymentMsg" runat="server" AutoUpdateAfterCallBack="true" Font-Bold="true" ForeColor="Red" Text=""></Anthem:Label>

                                            <Anthem:Button ID="btnpayment" class="logbutt" Enabled="true" runat="server" AutoUpdateAfterCallBack="True"
                                                Text="PROCEED" Width="80px" OnClick="btnpayment_Click1" TabIndex="100" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </Anthem:Panel>
                    </div>
                </div>
            </div>
        </div>

        <br />

        <div class="gridiv">
                    <asp:DataGrid ID="dgDetails" CssClass="table" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateColumn HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataSetIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn Visible="false" DataField="merchantdescription" HeaderText="Merchant Description">
                                <ItemStyle HorizontalAlign="left" Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="false" DataField="code" HeaderText="Merchant Code">
                                <ItemStyle HorizontalAlign="left" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="false" DataField="transactionid" HeaderText="Transaction Id">
                                <ItemStyle HorizontalAlign="left" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="amount" HeaderText="Amount (Rs.)">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="banktrnid" Visible="false" HeaderText="Bank Transaction Id">
                                <ItemStyle HorizontalAlign="left" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="description" HeaderText="Description">
                                <ItemStyle HorizontalAlign="Center" Width="25%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="trndate" HeaderText="Transaction Date">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>--%>
    </div>
    <%--<div class="do-not-click" id="divLoadGif" style="display: none">
        <img src="../../App_Themes/Darkgreen/online/gif-load.gif" />
    </div>--%>
</asp:Content>