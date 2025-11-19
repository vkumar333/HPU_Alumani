<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="Alm_viewMenteeRequestform_Admin.aspx.cs" Inherits="Alumni_Alm_viewMenteeRequestform_Admin" %>

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

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success" runat="server">
                <div class="tableheading" runat="server">
                    Become a Mentee
                </div>

                <div class="panel-body pnl-body-custom">
                    <p class=" mb-20" style="font-weight: 700; margin-left: 15px; font-size: 14px;">Fill out this form to register yourself as a mentee. Once you register as a mentee, a list of matching mentors will be curated for you. You can choose your mentor, schedule a call and discuss your points with them.</p>
                    <hr />
                    <div class="form-group">
                        <label class="col-sm-12 labelname">Your Name <span style="color: red;">*</span></label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:Label ID="Labelname" class="textname" runat="server" ForeColor="Black" Font-Size="Medium" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            <%-- <p class="textname">Name</p>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <Anthem:Label class="col-sm-12 labelname" runat="server">1.Which problem are you currently facing?<span style="color: red;">*</span></Anthem:Label>
                        <Anthem:Label class="col-sm-4 labelname" ID="lblmsg1" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-12 mb-20">
                            <div class="form-check">
                                <div class="form-check-label textname">
                                    <Anthem:CheckBoxList ID="chkproblems" class="form-check-input" font-family="Helvetica" AutoUpdateAfterCallBack="true" AutoCallBack="true" color="#333" font-weight="600" Font-Size="14px" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow"></Anthem:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <Anthem:Panel ID="pnl" runat="server" Visible="false" AutoUpdateAfterCallBack="true">
                        <div runat="server" class="form-group">
                            <label class="col-sm-12 labelname">Please specify<span style="color: red;">*</span></label>
                            <div class="col-sm-6 mb-20">
                                <Anthem:TextBox class="form-control" runat="server" ID="txtspcfy" AutoUpdateAfterCallBack="true" placeholder="Please specify *"></Anthem:TextBox>
                            </div>
                        </div>
                    </Anthem:Panel>
                    <div class="form-group">
                        <label class="col-sm-12 labelname">2. What is your Professional Background?<span style="color: red;">*</span></label>
                        <label class="col-sm-12 labelname">Ex. I am a Student or I am Software Developer at Accenture.</label>
                        <Anthem:Label class="col-sm-10 labelname" ID="Label1" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:TextBox class="form-control" runat="server" ID="txtbckground" AutoUpdateAfterCallBack="true" placeholder="2. What is your Professional Background?"></Anthem:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-12 labelname">3. Mentors from which domains would you prefer to connect with? (Optional)</label>
                        <label class="col-sm-12 labelname">For ex : Information Tech, Advertisement, Medical, Education</label>
                        <div class="col-sm-6 mb-20">
                            <Anthem:TextBox class="form-control" runat="server" ID="txtdomains" AutoUpdateAfterCallBack="true" placeholder="3. Mentors from which domains would you prefer to connect with? (Optional)"></Anthem:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-12 labelname">4. What will be the most comfortable way for you to connect with your mentors?<span style="color: red;">*</span></label>
                        <Anthem:Label class="col-sm-4 labelname" ID="lblmsg3" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        <div class="col-sm-12 mb-20">
                            <div class="form-check">
                                <div class="form-check-label textname">
                                    <Anthem:CheckBoxList ID="connectList" class="form-check-input" font-family="Helvetica" AutoUpdateAfterCallBack="true" color="#333" font-weight="600" Font-Size="14px" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow"></Anthem:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-sm-12 mb-20">
            <div class="form-check" style="text-align: left">
                <Anthem:Button ID="btnSave" runat="server" Text="Send Request" OnClick="btnSave_Click" TextDuringCallBack="Requesting"
                    CommandName="SAVE" AutoUpdateAfterCallBack="true"  />
                <Anthem:Label ID="lblmsg" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></Anthem:Label>
            </div>
        </div>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="" align="center">
        <div class="">
            <div class="row">
                <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnback_Click" CssClass="btn btn-primary btn-sm" />
            </div>
        </div>
    </div>
    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>
