<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="Alm_BecomeMentor.aspx.cs" Inherits="Alumni_Alm_BecomeMentor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-custom mt-15">
        <div class="">
            <div class="box box-success">
                <div class="boxhead">
                    Become a Mentor
                    <a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_Mentorship.aspx">Back </a>
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="form-group">
                        <label class="col-sm-12 labelname">Your Name <span style="color: red;">*</span></label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:Label ID="Labelname" class="textname" runat="server" ForeColor="Black" Font-Size="Medium" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <Anthem:Label class="col-sm-12 labelname" runat="server">1. On what topics do you think you can mentor? <span style="color: red;">*</span></Anthem:Label>
                        <Anthem:Label class="col-sm-4 labelname" ID="lblmsg1" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-12 mb-20">
                            <div class="form-check">
                                <div class="form-check-label textname">
                                    <Anthem:CheckBoxList ID="chktopic" class="form-check-input" font-family="Helvetica" AutoUpdateAfterCallBack="true" AutoCallBack="true" color="#333"
                                        font-weight="600" Font-Size="14px" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" OnSelectedIndexChanged="chktopic_SelectedIndexChanged">
                                    </Anthem:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <Anthem:Panel ID="pnl" runat="server" Visible="false" AutoUpdateAfterCallBack="true">
                        <div runat="server" class="form-group">
                            <label class="col-sm-12 labelname">Please specify<span style="color: red;">*</span></label>
                            <div class="col-sm-6 mb-20">
                                <Anthem:TextBox class="form-control" runat="server" ID="txtspcfy" AutoUpdateAfterCallBack="true" placeholder="Enter your answer."></Anthem:TextBox>
                            </div>
                        </div>
                    </Anthem:Panel>
                    <div class="form-group">
                        <Anthem:Label class="col-sm-12 labelname" runat="server">2. Who would you like to mentor? <span style="color: red;">*</span></Anthem:Label>
                        <Anthem:Label class="col-sm-4 labelname" ID="lblmsg2" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-12 mb-20">
                            <div class="form-check">
                                <div class="form-check-label textname">
                                    <Anthem:CheckBoxList ID="chktopic2" class="form-check-input" font-family="Helvetica" AutoUpdateAfterCallBack="true" AutoCallBack="true" color="#333" font-weight="600" Font-Size="14px" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow"></Anthem:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-12 labelname">3. Mention your skills <span style="color: red;">*</span></label>
                        <label class="col-sm-12 labelname">Ex.: Leadership, Communication, Designing, Programming.</label>
                        <Anthem:Label class="col-sm-10 labelname" ID="lblmsg4" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:TextBox class="form-control" runat="server" ID="txtbckground2" AutoUpdateAfterCallBack="true" placeholder="Enter your answer."></Anthem:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <Anthem:Label class="col-sm-12 labelname" runat="server">4. What will be the most comfortable way in which you can mentor others? <span style="color: red;">*</span></Anthem:Label>
                        <Anthem:Label class="col-sm-4 labelname" ID="lblmsg6" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-12 mb-20">
                            <div class="form-check">
                                <div class="form-check-label textname">
                                    <Anthem:CheckBoxList ID="chktopic6" class="form-check-input" font-family="Helvetica" AutoUpdateAfterCallBack="true" AutoCallBack="true" color="#333" font-weight="600"
                                        Font-Size="14px" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" OnSelectedIndexChanged="chktopic6_SelectedIndexChanged">
                                    </Anthem:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <Anthem:Panel ID="pnl3" runat="server" Visible="false" AutoUpdateAfterCallBack="true">
                        <div runat="server" class="form-group">
                            <label class="col-sm-12 labelname">Please specify comfortable way in which you can mentor others <span style="color: red;">*</span></label>
                            <div class="col-sm-6 mb-20">
                                <Anthem:TextBox class="form-control" runat="server" ID="txt_specify3" AutoUpdateAfterCallBack="true" placeholder="Enter your answer."></Anthem:TextBox>
                            </div>
                        </div>
                    </Anthem:Panel>
                    <div class="form-group">
                        <label class="col-sm-12 labelname">5. A message to your mentee <span style="color: red;">*</span></label>
                        <label class="col-sm-12 labelname">Ex.: Tell something about yourself and how you can help mentees.</label>
                        <Anthem:Label class="col-sm-10 labelname" ID="lblmsg7" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:TextBox class="form-control" runat="server" ID="txtbckground7" AutoUpdateAfterCallBack="true" placeholder="Enter your answer."></Anthem:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 mb-20">
                            <label for="lblDeclaration">
                                <Anthem:CheckBox ID="chkDeclaration" runat="server" AutoUpdateAfterCallBack="true" />
                                <strong>Declaration:</strong> I hereby declare that the information provided is true and correct to the best of my knowledge. I understand that after submission, the record cannot be modified.
                            </label>
                            <span style="color: red;">*</span>
                        </div>
                    </div>
                    <div class="col-sm-12 mb-20">
                        <div class="form-check" style="text-align: left">
                            <Anthem:Button ID="btnSave" runat="server" Text="SUBMIT" OnClick="btnSave_Click" TextDuringCallBack="Submiting..."
                                CommandName="SUBMIT" AutoUpdateAfterCallBack="true" />
                            <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnback_Click" Visible="false" />
                            <Anthem:Label ID="lblmsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;">
    </iframe>
</asp:Content>