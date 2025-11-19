<%@ Page Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Alumni_Achievers_Lists.aspx.cs" Inherits="Alumni_ALM_Alumni_Achievers_Lists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        
        .custom-blog-image img {
            height: 100px;
        }
    </style>
    <div class="container-fluid mt-0">
        <div class="">
            <div class="box-success" runat="server">
                <div class="boxhead mt-10 mb-10">
                    Alumni Achievers Lists
					<a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_Alumni_Home.aspx">Back </a>
                </div>
                <div class="panel-body pnl-body-custom">
                    <div class="">
                        <div id="rs-blog" class="rs-blog main-home">
                            <asp:Repeater ID="galleryAchieversRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3">
                                    <div class="blog-item custom-blog-item text-center"> 
                                                <div class="custom-blog-image"> 
                                                    <img class="img-circle" src="<%# Eval("file_Url")%>" alt="" /> 
                                                </div> 
                                                <div class="blog-content"> 
                                                    <h3 class="title cust-title"> 
                                                        <%# Eval("alumni_name")%>
                                                    </h3> 
                                                    <div class="desc" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"> 
                                                        <%# Eval("Achievement")%>
                                                    </div> 
                                                    <div class="btn-btm btm-cut"> 
                                                        <div class="rs-view-btn"> 
                                                            <a id="alumni" href="Alm_View_Alumni_Achiever.aspx?id=<%# Eval("encId")%>">View More</a> 
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
                <%--<div class="col-lg-1 col-md-12">
                    <div class="back-btn">
                        <a href="../Alumni/ALM_Alumni_Home.aspx">Back</a>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>
