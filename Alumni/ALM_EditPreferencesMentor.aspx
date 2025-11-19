<%@ Page Title="" Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_EditPreferencesMentor.aspx.cs" Inherits="Alumni_ALM_EditPreferencesMentor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mb-20 {
            margin-bottom: 20px !important;
        }
		
        .labelname {
            font-size: 14px;
            margin-bottom: 5px;
            font-weight: 600;
        }
		
        .textname {
            font-size: 14px;
            margin-bottom: 5px;
        }

        .maindiv {
            border: 1px solid #016773;
            min-height: 790px;
        }

        hr {
            border-top: 2px solid #016773;
        }
    </style>
	
    <style>
        input[type="submit"], input[type="button"] {
            background: #016773;
            background-color: #016773;
            border: 1px solid #016773;
            color: #FFFFFF;
            cursor: pointer;
            font-weight: 600;
            padding: 5px 6px;
            border-radius: 5px 5px 5px 5px;
        }
    </style>

    <div class="container-custom mt-15">
        <div class="">
            <div class="box box-success" runat="server">
                <div class="boxhead mt-15 mb-0">
                    Become a Mentor
                    <a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_MyPreferences.aspx">Back </a>
                </div>
                <div class="panel-body pnl-body-custom">

                    <%-- <p class=" mb-20" style="font-weight: 700; margin-left: 15px; font-size: 14px;">Fill out this form to register yourself as a mentor. Once you register as a mentor, a list of matching mentors will be curated for you. You can choose your mentor, schedule a call and discuss your points with them.</p>
                    <hr />--%>
                    <div class="form-group">
                        <label class="col-sm-12 labelname">Your Name <span style="color: red;">*</span></label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:Label ID="Labelname" class="textname" runat="server" ForeColor="Black" Font-Size="Medium" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            <%-- <p class="textname">Name</p>--%>
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
                    <%--<div class="form-group" visible="false">
                        <label class="col-sm-12 labelname">3. Mention domains in which you can mentor others<span style="color: red;">*</span></label>
                        <label class="col-sm-12 labelname">Ex.: Information Tech, Advertisement, Medical, Education.</label>
                        <Anthem:Label class="col-sm-10 labelname" ID="lblmsg3" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:TextBox class="form-control" runat="server" ID="txtbckground" AutoUpdateAfterCallBack="true" placeholder="Enter your answer."></Anthem:TextBox>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <label class="col-sm-12 labelname">3. Mention your skills <span style="color: red;">*</span></label>
                        <label class="col-sm-12 labelname">Ex.: Leadership, Communication, Designing, Programming.</label>
                        <Anthem:Label class="col-sm-10 labelname" ID="lblmsg4" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:TextBox class="form-control" runat="server" ID="txtbckground2" AutoUpdateAfterCallBack="true" placeholder="Enter your answer."></Anthem:TextBox>
                        </div>
                    </div>
                    <%--<div class="form-group" visible="false">
                        <Anthem:Label class="col-sm-12 labelname" runat="server">5. What is your preferred time for a mentoring session? <span style="color: red;">*</span></Anthem:Label>
                        <Anthem:Label class="col-sm-4 labelname" ID="lblmsg5" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-12 mb-20">
                            <div class="form-check">
                                <div class="form-check-label textname">
                                    <Anthem:CheckBoxList ID="chktopic5" class="form-check-input" font-family="Helvetica" AutoUpdateAfterCallBack="true" AutoCallBack="true" color="#333" font-weight="600"
                                        Font-Size="14px" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" OnSelectedIndexChanged="chktopic5_SelectedIndexChanged">
                                    </Anthem:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <Anthem:Panel ID="pnll2" runat="server" Visible="false" AutoUpdateAfterCallBack="true">
                        <div runat="server" class="form-group">
                            <label class="col-sm-12 labelname">Please specify your preferred time for a mentoring session * <span style="color: red;">*</span></label>
                            <div class="col-sm-6 mb-20">
                                <Anthem:TextBox class="form-control" runat="server" ID="txt_specify_pre" AutoUpdateAfterCallBack="true" placeholder="Enter your answer."></Anthem:TextBox>
                            </div>
                        </div>
                    </Anthem:Panel>--%>
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
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>