<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UMM/MasterPage.master" CodeFile="ALM_Alumni_Registration_Details.aspx.cs" Inherits="Alumni_ALM_Alumni_Registration_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .item-style td, .dgitem-style td, .rowselect td {
            border: 1px solid #666;
            font-weight: normal;
            padding: 4px;
        }

        .form-control {
            width: 94%;
            display: inline-block;
        }
    </style>
    <div class="form-horizontal">
        <h2 class="tableheading">List of Registered Alumni Details</h2>
        <div class="form-group form-group-sm">
            <label class="col-sm-2 col-xs-12 control-label" id="lblMemShipType">Membership Type</label>
            <div class="col-sm-4 col-xs-12 required">
                <Anthem:RadioButtonList ID="rblMemShipType" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                    <asp:ListItem Text="Life-time Membership" Value="LM" />
                    <asp:ListItem Text="Student Membership" Value="SM" />
                </Anthem:RadioButtonList>
            </div>
            <label class="col-sm-2 col-xs-12 control-label" id="lblGender">Gender</label>
                <div class="col-sm-4">
                    <Anthem:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                        <asp:ListItem Text="Male" Value="M" />
                        <asp:ListItem Text="Female" Value="F" />
                    </Anthem:RadioButtonList>
                </div>
        </div>
            
          <div class="form-group form-group-sm">
              <label class="col-sm-2 col-xs-12 control-label">Is Old Portal ?</label>
              <div class="col-sm-4 col-xs-12">
                  <Anthem:RadioButtonList ID="rblIsOldPortal" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                      <asp:ListItem Text="YES" Value="1" />
                      <asp:ListItem Text="NO" Value="0" />
                  </Anthem:RadioButtonList>
              </div>
              <label class="col-sm-2 col-xs-12 control-label" id="lblStartDate">From Date</label>
               <div class="col-sm-4 required">
                   <Anthem:TextBox ID="V_txtStartDate" runat="server" CssClass="form-control" Width="30%" AutoUpdateAfterCallBack="True" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" SkinID="textboxdate" Style="width: 95px !important;" TabIndex="3"></Anthem:TextBox>
                   <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtStartDate);return false;">
                       <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>*
               </div>
          </div>
         

           <div class="form-group form-group-sm">
               <label class="col-sm-2 col-xs-12 control-label" id="lblEndDate">To Date</label>
               <div class="col-sm-4 required">
                   <Anthem:TextBox ID="V_txtEndDate" runat="server" AutoUpdateAfterCallBack="True" CssClass="form-control" Width="30%" onpaste="return false;" ondrag="return false;" ondrop="return false;" MaxLength="10" onkeydown="return false" SkinID="textboxdate" Style="width: 95px !important;" TabIndex="5"></Anthem:TextBox>
                   <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.aspnetForm.ctl00_ContentPlaceHolder1_V_txtEndDate);return false;">
                       <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>*
               </div>
           </div>

            
        <div class="form-group form-group-sm">
            <div class="col-sm-10 col-sm-offset-2">
                <Anthem:Button ID="btnView" runat="server" AutoUpdateAfterCallBack="true" EnableCallBack="false" Text="VIEW" CommandName="UPDATE" OnClick="btnView_Click"/>
                <Anthem:Button ID="btnreset" runat="server" AutoUpdateAfterCallBack="true" EnableCallBack="false" Text="RESET" OnClick="btnreset_Click" />
                <Anthem:Label ID="lblMsg1" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </div>
        </div>
           
        
               <div class="tablesubheading mb-3">
                        List of Registration 
                        <Anthem:LinkButton ID="lnkSearch" CssClass="pull-right" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkSearch_Click">Search Criteria</Anthem:LinkButton>
                    </div>

                    <div>
                        <Anthem:Panel ID="pnlSearch" runat="server" AutoUpdateAfterCallBack="true" Visible="false">
                            <div class="form-group form-group-sm">
                               <label class="col-sm-2 control-label" id="lblAlumni">Alumni Name</label>
                             <div class="col-sm-4">
                             <Anthem:TextBox ID="txtAlumni" runat="server" AutoUpdateAfterCallBack="True" MaxLength="100" Style="width:80%;" CssClass="form-control"></Anthem:TextBox>
                             </div>

                                <label class="col-sm-2 control-label">Registration No.</label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtRegistrationNo" runat="server" CssClass="form-control" Style="width:80%;" AutoUpdateAfterCallBack="true" MaxLength="100" />
                                </div>

                            </div>

                               <div class="form-group form-group-sm">
                               <label class="col-sm-2 control-label" id="lblMobile">Mobile No</label>
                             <div class="col-sm-4">
                             <Anthem:TextBox ID="Txt_Mobile" runat="server" AutoUpdateAfterCallBack="True" Style="width:80%;" MaxLength="100" CssClass="form-control"></Anthem:TextBox>
                             </div>

                                <label class="col-sm-2 control-label">Email</label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="Txt_Email" runat="server" CssClass="form-control" Style="width:80%;" AutoUpdateAfterCallBack="true" MaxLength="100" />
                                </div>

                            </div>

                             <div class="form-group form-group-sm">
                                <div class="col-sm-10 col-sm-offset-2">
                                    <Anthem:Button ID="btnSearch" runat="server" CommandName="SEARCH" OnClick="btnSearch_Click"
                                        Text="SEARCH" AutoUpdateAfterCallBack="true" TextDuringCallBack="WAIT..." />
                                    &nbsp;&nbsp;
                                <Anthem:Button ID="btnclear" runat="server" CausesValidation="False" OnClick= "btnclear_Click"
                                    Text="RESET" AutoUpdateAfterCallBack="true" TextDuringCallBack="WAIT..." />
                                    <Anthem:Label ID="Label1" runat="server" SkinID="lblmessage" AutoUpdateAfterCallBack="true" />
                                </div>
                            </div>


                        </Anthem:Panel>
                    </div>
        <div class="form-group form-group-sm">
            <div class="col-sm-10 col-sm-offset-2">
                <Anthem:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" AutoUpdateAfterCallBack="true" Enabled="false" CommandName="ExportToExcel" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary btn-sm" EnableCallBack="false" />
                <Anthem:Label ID="lblMsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </div>
        </div>
       <div class="form-group form-group-sm">
            <div class="col-sm-12">
                <div class="table-responsive">
                    <div style="height:350px; overflow:scroll;">
                    <Anthem:GridView ID="dgv" runat="server" AutoGenerateColumns="False" EnableCallBack="false"  PageIndex="0" PageSize="10"
                        OnRowCommand="dgv_RowCommand" Width="100%" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True" meta:resourcekey="dgvResource1" OnSelectedIndexChanging="dgv_SelectedIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="id" Visible="false">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_pk_id" runat="server" Text='<%# Eval("Reqid") %>'></asp:Label>
                                     </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Alumni Registation No" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAlumniNo" runat="server" Text='<%# Eval("alumnino") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Alumni Name" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Membership Type" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMembership" runat="server" Text='<%# Eval("alumniMemtype") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gender" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblGender" runat="server" Text='<%# Eval("gender") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Year of Passing" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblYearOfPassing" runat="server" Text='<%# Eval("yearofpassing") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Degree" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DegreeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mobile No" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("Mobile_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DOB" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDob" runat="server" Text='<%# Eval("dob") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount Paid" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPayAmount" runat="server" Text='<%# Eval("Amount").ToString() == "0.00" ? "" : Eval("Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Transaction No" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTransactionNo" runat="server" Text='<%# Eval("TransactionID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Transaction Date" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Payment Mode" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentMode" runat="server" Text='<%# Eval("PaymentMode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Transaction Status" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTransactionStatus" runat="server" Text='<%# Eval("TransactionStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Registration Date" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRegistrationDate" runat="server" Text='<%# Eval("RegsDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IsOldPortal" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIsOldPortal" runat="server" Text='<%# Eval("isOld") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Special Ability" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIsDisabled" runat="server" Text='<%# Eval("isDisabled") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Is Special Ability More than 40%" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIsDisabilityPercentagewise" runat="server" Text='<%# Eval("isDisabilityPercentage") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Special Ability Description" Visible="True">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDisabiltyRemarks" runat="server" Text='<%# Eval("disabiltyRemarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Print Profile">
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                <ItemTemplate>
                                    <Anthem:LinkButton ID="lnkPrint" runat="server" CommandArgument='<%# Eval("pk_alumniid") %>' Text="PRINT" CausesValidation="False" EnableCallBack="false" TextDuringCallBack="Print..." AutoUpdateAfterCallBack="True" CommandName="PRINT"
                                        Visible='<%# Eval("isVisiblePrint") %>'>
                                    </Anthem:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </Anthem:GridView>
                    </div>
                </div>
            </div>
        </div>
        
        <%--<div class="form-group form-group-sm">
            <label class="col-sm-3 control-label"></label>
            <div class="col-sm-3"></div>
            <label class="col-sm-3 control-label"></label>
            <div class="col-sm-3"></div>
        </div>--%>
    </div>



    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>
