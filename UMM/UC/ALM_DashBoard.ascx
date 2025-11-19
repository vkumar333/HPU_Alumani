<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ALM_DashBoard.ascx.cs" Inherits="UMM_UC_ALM_DashBoard" %>

   <div class="col-md-12">
    <div class="row">
        <div class="box box-success">
            <div class="box-body table-responsive">
                <table class="table mobile_form">
                    <tr>
                        <td class="vtext" align="left">Chart Type :</td>
                        <td >
                            <Anthem:DropDownList runat="server" SkinID="dropdown" Width="100px" ID="ddlChartType"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlChartType_SelectedIndexChanged"
                                Enabled="true">
                                <asp:ListItem Value="Column3D.swf" Text="Column3D" />
                                <asp:ListItem Value="SSGrid.swf" Text="SSGrid" />
                                <asp:ListItem Value="Pie3D.swf" Text="Pie3D" />
                                <asp:ListItem Value="Doughnut3D.swf" Text="Doughnut3D" />
                                <asp:ListItem Value="Pie2D.swf" Text="Pie2D" />
                            </Anthem:DropDownList>
                        </td>
                        <td class="vtext">From Date:</td>
                        <td class="required">
                            <Anthem:TextBox ID="V_txtfrmdate" runat="server" SkinID="textboxdate" AutoUpdateAfterCallBack="True"
                                MaxLength="10" onkeydown="return NoEntry();" Style="width: 75px!important" />
                            <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(ctl00_ContentPlaceHolder1_ctl01_V_txtfrmdate);return false;">
                                <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>*</td>

                        <td class="vtext">To Date:</td>
                        <td class="required">
                            <Anthem:TextBox ID="V_txttodate" runat="server" Style="width: 75px!important" SkinID="textboxdate" AutoUpdateAfterCallBack="True" MaxLength="10" onkeydown="return NoEntry();" />
                            <a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(ctl00_ContentPlaceHolder1_ctl01_V_txttodate);return false;">
                                <img alt="" border="0" class="PopcalTrigger" height="20" src='<%=Page.ResolveUrl("../calendar/calbtn.gif")%>' width="34" /></a>*
                        </td>



                    </tr>
                    <%--<tr>
                        
                    </tr>--%>
                    <tr>
                        <td class="gap"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <Anthem:Button AutoUpdateAfterCallBack="True" CssClass="btn btn-primary btn-sm" ID="btnSave" runat="server" CommandName="VIEW"
                                Text="VIEW"
                                OnClick="btnSave_Click" EnableCallBack="False" />
                            <Anthem:Button ID="btnReset" runat="server" CssClass="btn btn-default btn-sm" CausesValidation="False" OnClick="btnReset_Click"
                                Text="RESET" TextDuringCallBack="CLEARING.." AutoUpdateAfterCallBack="True" EnableCallBack="False" />
                            <Anthem:Label ID="lblMsg" runat="server"
                                AutoUpdateAfterCallBack="True" SkinID="lblmessage"
                                ForeColor="Red" UpdateAfterCallBack="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="gap"></td>
                    </tr>
                </table>
                 </div>
               
                <div class="box-body table-responsive" style="width: 90%; margin: auto">
                    <table class="table mobile_form"  >
                        <tr>
                            <td class="dashTabdet"   >
                                <table>
                                    <tr>
                                        <td style="font-weight: bold;color:white">No. of Student Participated in event Session Wise
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <%=Create_Chart_StudentEventSession_Wise_Count()%>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="dashTabdet">
                                <table>
                                    <tr>
                                        <td style="font-weight: bold;color:white">Student Participated in event gender wise
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%=Create_Chart_EventParticipation_GenderWise()%> 
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="gap"></td>
                        </tr>
                        


                    </table>
                </div>


            
        </div>
    </div>
    </div>


<iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
        src='<%=Page.ResolveUrl("../calendar/ipopeng.htm")%>' scrolling="no" frameborder="0"
        style="visibility: visible; z-index: 119; position: absolute; top: -500px; left: -500px;">
    </iframe>