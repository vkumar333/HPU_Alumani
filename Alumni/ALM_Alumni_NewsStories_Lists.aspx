<%@ Page Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_Alumni_NewsStories_Lists.aspx.cs" Inherits="Alumni_ALM_Alumni_NewsStories_Lists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .ovrflw .scroll-flw {
            padding: 0 22px;
            height: 422px;
        }
        .ovrflw .scroll-flw::-webkit-scrollbar-thumb {
            background: #266f68;
        }
        .btn-more a {
            display: inline-block;
            margin-left: auto;
            padding: 12px 25px;
            font-size: 13px;
            font-weight: 500;
            text-decoration: none;
            background-color: transparent;
            color: #f54828;
            border: 1px solid #f54828;
        }
            .btn-more a:hover {
                background-color: #f54828;
                color: #fff;
            }
    </style>

    <div class="rs-events-list newsandstorieslist-css sec-spacer">       
            <div class="container-fluid">
                <div class="boxhead mt-0 mb-2">
                    News & Stories
					<a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_Alumni_Home.aspx">Back </a>
                </div>
            </div>
         <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="sec-title">
                        <h2>
                            <img src="alumin-default-theme/images/news.png" style="width: 25px; height: 25px;" />&nbsp;&nbsp;News 
                        </h2>
                    </div>
                    <div class="news-list-block ovrflw">
                        <div class="scroll-flw">
                            <Anthem:Repeater ID="newsRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="row evnets-item">

                                        <div class="col-md-2">
                                            <div class="evnets-img">
                                                <img runat="server" id="Imge1" src='<%# Eval("ImageUrl")%>' alt="news" class="mx-auto" style="height: 50px;" />
                                            </div>
                                        </div>
                                        <div
                                            class="col-md-10">
                                            <div class="course-header">
                                                <h3 class="course-title"><%# Eval("Heading") %></h3>
                                                <div class="course-date">
                                                    <i class="fa fa-calendar-check-o"></i> <%# Eval("ConvertedDate") %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </Anthem:Repeater>
                        </div>
                        <div class="clearfix"></div>
                        <div class="view-more btn-more pull-right">
                            <a href="ALM_View_All_Newss.aspx?type=news">View All News <i class="fa fa-angle-double-right"></i></a>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="sec-title">
                        <h2>
                            <img src="alumin-default-theme/images/stories.png" style="width: 25px;" />&nbsp;&nbsp;Stories </h2>
                    </div>
                    <div class="clearfix"></div>
                    <div class="news-list-block ovrflw">
                        <div class="scroll-flw">
                            <Anthem:Repeater ID="storiesRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="row evnets-item">
                                        <div class="col-md-2">
                                            <div class="evnets-img">
                                                <img runat="server" id="Imge1" src='<%# Eval("ImageUrl")%>' alt="news" class="mx-auto" style="height: 50px;" />
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="course-header">
                                                <h3 class="course-title"><%# Eval("Heading") %></h3>
                                                <div class="course-date">
                                                    <i class="fa fa-calendar-check-o"></i> <%# Eval("ConvertedDate") %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </Anthem:Repeater>
                        </div>
                        <div class="clearfix"></div>
                        <div class="view-more btn-more pull-right">
                            <a href="ALM_View_All_Storiess.aspx?type=stories">View All Stories <i class="fa fa-angle-double-right"></i></a>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div id="EventDiv" class="white_content-new-1" style="display: none">
        <div class="popupboxouter">
            <div class="popupbox">
                <div onclick="document.getElementById('EventDiv').style.display='none';" class="close-1">
                    X
                </div>
                <div class="table-responsive">
                    <table class="mobile_form" width="100%">
                        <tr>
                            <td id="Td1" colspan="6" class="boxhead" runat="server" style="height: 19px">Add News  </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="tdgap"></td>
                        </tr>
                        <tr>
                            <td class="vtext">Categories </td>
                            <td class="colon" style="width: 2%">: </td>
                            <td class="required" style="width: 10%">
                                <Anthem:DropDownList ID="D_ddlCategories" CssClass="dropdownlong" runat="server" AutoUpdateAfterCallBack="true" AutoCallBack="true" />
                                *
                            </td>
                            <td class="vtext" style="width: 15%">Heading </td>
                            <td class="colon" valign="top">: </td>
                            <td class="required" style="width: 15%">
                                <Anthem:TextBox ID="text_heading" runat="server" AutoUpdateAfterCallBack="True" TextMode="SingleLine" SkinID="textbox"></Anthem:TextBox>
                                *
                            </td>
                        </tr>
                        <tr>
                            <td class="vtext">Description </td>
                            <td class="colon" style="width: 2%">: </td>
                            <td class="required" style="width: 10%">
                                <Anthem:TextBox ID="R_txtDiscription" runat="server" AutoUpdateAfterCallBack="True" TextMode="SingleLine" SkinID="textbox"></Anthem:TextBox>*
                            </td>
                            <td class="vtext" style="width: 15%">Photo Uploads </td>
                            <td class="colon" valign="top">: </td>
                            <td class="required" style="width: 10%">
                                <Anthem:FileUpload ID="flUploadLogo" AutoUpdateAfterCallBack="true" runat="server" />
                                <span style="font-weight: normal">File size shouldn’t be greater than 5 Mb
                                    format type. as in (PNG, JPEG, JPG)*
                                </span>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="vtext">Set On Album </td>
                            <td class="colon" style="width: 2%">: </td>
                            <td class="required" style="width: 10%" colspan="4">
                                <Anthem:CheckBox ID="chkhomepage" runat="server" AutoUpdateAfterCallBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="tdgap"></td>
                        </tr>
                        <tr style="text-align: center">
                            <td colspan="6">
                                <Anthem:Button ID="btnSave" runat="server" AutoUpdateAfterCallBack="true" OnClientClick="return checkValidation();"
                                    CommandName="SAVE" OnClick="btnSave_Click" Text="SAVE" EnableCallBack="false" CausesValidation="true" />
                                <Anthem:Button ID="btnReset" runat="server" CausesValidation="False" Text="RESET" TextDuringCallBack="RESETING.." AutoUpdateAfterCallBack="True" OnClick="btnReset_Click" />
                                <Anthem:Label ID="Label1" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("~/calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;"></iframe>
</asp:Content>