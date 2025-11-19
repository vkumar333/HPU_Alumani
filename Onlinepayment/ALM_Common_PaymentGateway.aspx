<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ALM_Common_PaymentGateway.aspx.cs" MasterPageFile="~/Alumni/Alumni_gateway_master.master" Inherits="Onlinepayment_ALM_Common_PaymentGateway" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <style type="text/css">
        .flash {
            animation-name: flash;
            animation-duration: 0.4s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
            animation-direction: alternate;
            animation-play-state: running;
            font-size: 16px;
            font-weight: bold;
            display: block;
        }

        @keyframes flash {
            from {
                color: red;
            }

            to {
                color: #05ca1f;
            }
        }

        #tblbankdetails td {
            font-size: 12px;
        }

        #ctl00_ContentPlaceHolder1_ddlBankGatway {
            margin: auto !important;
        }

            #ctl00_ContentPlaceHolder1_ddlBankGatway input {
                padding: 10px;
            }

            #ctl00_ContentPlaceHolder1_ddlBankGatway td {
                padding: 10px;
                width: 50px;
            }
    </style>

    <div class="">
        <fieldset class="fieldset-border">
            <legend><i class="fa fa-fw fa-user"></i>Alumni Registration</legend>
            <Anthem:Button ID="btnBack" Visible="false" Font-Size="Larger" AutoUpdateAfterCallBack="true"
                runat="server" Text="BACK" OnClick="btnBack_Click"></Anthem:Button>

            <h3 class="payment-head">Choose Your Payment Gateway to pay...</h3>
            <input type="hidden" runat="server" id="key" name="key" />
            <input type="hidden" runat="server" id="hash" name="hash" />
            <input type="hidden" runat="server" id="txnid" name="txnid" />
            <center style="background-color: yellow">
        <span class="flash">While making the payment it is advised that do not press back button or do not referesh from the browser,keyboard or do not close the browser until get response from bank site to our website .</span><br />
        <span class="flash">भुगतान करते समय यह सलाह दी जाती है कि बैंक साइट से हमारी वेबसाइट पर वापस न आने तक कोई भी बटन  न दबाएं या फिर रीफ़्रेश न करें </span>
        <br />
        <span class="flash" style="color: red;">Please  note that Maximum limit of  payment transactions per/single card(Debit/Credit) for 24 hrs should not exceed 8 attempts.</span><br />
        <span class="flash" style="color: red;">कृपया ध्यान दें कि प्रति कार्ड (डेबिट / क्रेडिट) भुगतान लेनदेन की अधिकतम सीमा  24 घंटे  में 8 प्रयासों से अधिक नहीं होनी चाहिए।</span>
    </center>
            <br />
            <center style="text-align: center!important;">
        <Anthem:RadioButtonList ID="ddlBankGatway" AutoUpdateAfterCallBack="true"
             RepeatDirection="Horizontal" runat="server" AutoCallBack="true"
             OnSelectedIndexChanged="ddlBankGatway_SelectedIndexChanged"></Anthem:RadioButtonList>
    </center>
            <br />
            <div class="table-responsive" style="width: 105%">
                <Anthem:DataList ID="dlGatwayDtl" CssClass="table table-hover" runat="server" RepeatColumns="3"
                    AutoUpdateAfterCallBack="true">
                    <ItemTemplate>
                        <div class="text-center">
                            <div class="bank-name">
                                <img width="200" class="" src="../../App_Themes/Darkgreen/online/<%#Eval("ImageName") %>" />
                                <h5 class="text-center"><strong><%#Eval("P_GatwayName") %> </strong></h5>
                                <Anthem:Panel ID="pnl" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Panel>
                            </div>
                        </div>
                    </ItemTemplate>
                </Anthem:DataList>
            </div>
            <center>
        <Anthem:Button ID="btnPay" Visible="false" runat="server" EnableCallBack="false" AutoUpdateAfterCallBack="true"
            Text="CONTINUE FOR PAYMENT" CssClass="btn btn-warning btn-sm"
            OnClientClick="document.getElementById('divLoadGif').style.display = 'block';"  class="btn btn-warning btn-sm" OnClick="btnPay_Click" />
    </center>
            <br />
            <center>
        <Anthem:Label ID="lblPaymentMsg" BackColor="Yellow" Font-Size="Large" ForeColor="Red" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
    </center>
            <h3 runat="server" id="headrPayHistory" class="payment-head" style="font-size: 16px; font-weight: bold; color: #0300ff; border-bottom: solid 1px #0300ff; text-align: center; background: yellow; padding-top: 5px;">Previous Payment Status</h3>
            <div class="box-body table-responsive">
                <div class="gridiv">
                    <asp:GridView ID="dgDetails" CssClass="table" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                    <Anthem:HiddenField ID="hdfGatewayId" runat="server" Value='<%#Eval("Fk_PaymentGId") %>' AutoUpdateAfterCallBack="true" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="amount" HeaderText="Amount">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusUpdatedDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Transaction Date">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="P_GatwayName" HeaderText="Payment Gateway Name">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="Bank Transaction Status">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bank_ref_num" HeaderText="Reference No.">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="true" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </fieldset>
    </div>

</asp:Content>