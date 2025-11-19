<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="Certificate_upload.aspx.cs" Inherits="Alumni_Certificate_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="7" class="tableheading">Certificate Upload</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="7" class="tableheading">Links </td>
                        </tr>
                        <tr>
                            <td id="wtext" class="vtext" style="width: 15%">Website Link </td>
                            <td class="colon">: </td>
                            <td class="required">
                                <Anthem:TextBox ID="Webtext" runat="server" AutoUpdateAfterCallBack="True" MaxLength="100" ondrop="event.returnValue=false" SkinID="textbox" TextMode="SingleLine">
                                </Anthem:TextBox>
                            </td>
                            <td id="ftext" class="vtext" style="width: 15%">Facebook Link </td>
                            <td class="colon">:</td>
                            <td class="required">
                                <Anthem:TextBox ID="fbtext" runat="server" AutoUpdateAfterCallBack="True" MaxLength="100" SkinID="textbox" TextMode="SingleLine">
                                </Anthem:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="Ltext" class="vtext" style="width: 15%">LinkedIn Link </td>
                            <td class="colon">: </td>
                            <td class="required">
                                <Anthem:TextBox ID="linktext" runat="server" AutoUpdateAfterCallBack="True" MaxLength="100" SkinID="textbox" TextMode="SingleLine">
                                </Anthem:TextBox>
                            </td>
                            <td id="Ttext" class="vtext" style="width: 15%">Twitter Link </td>
                            <td class="colon">: </td>
                            <td class="required">
                                <Anthem:TextBox ID="twittertext" runat="server" AutoUpdateAfterCallBack="True" MaxLength="100" SkinID="textbox" TextMode="SingleLine">
                                </Anthem:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="Ytext" class="vtext" style="width: 15%">Youtube Link </td>
                            <td class="colon">: </td>
                            <td class="required" colspan="4">
                                <Anthem:TextBox ID="txtYoutubeLink" runat="server" AutoUpdateAfterCallBack="True" MaxLength="100" SkinID="textbox" TextMode="SingleLine">
                                </Anthem:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="vtext" id="lblorde">Registration Certificate</td>
                            <td class="colon">:</td>
                            <td class="required">
                                <asp:FileUpload ID="UploadImg" runat="server" AllowMultiple="true" AutoUpdateAfterCallBack="True" AutoCallBack="true" maxRequestLength="10 MB" />
                                <Anthem:Label ID="Label1" runat="server" ForeColor="Red" AutoCallBack="true" AutoUpdateAfterCallBack="true"
                                    maxRequestLength="10 MB">File size should be 10 MB</Anthem:Label><br />
                                <Anthem:Label ID="Label2" runat="server" ForeColor="Red" Text="only accept .jpg,.jpeg,.png,.pdf,.docs"></Anthem:Label>
                            </td>
                            <td class="vtext">
                                <Anthem:Label ID="lbl1" Text="View File" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </td>
                            <td class="colon">
                                <Anthem:Label ID="lblcolon" Text=":" runat="server" AutoUpdateAfterCallBack="true" />
                            </td>
                            <td class="required">
                                <Anthem:LinkButton ID="lnk_Download" EnableCallBack="false" runat="server" Text="" OnClick="lnk_Download_Click" ForeColor="blue" AutoUpdateAfterCallBack="true">
                                </Anthem:LinkButton>
                        </tr>
                        <tr>
                            <td class="vtext" id="lblordes">Memorandum Of association</td>
                            <td class="colon">:</td>
                            <td class="required">
                                <asp:FileUpload ID="UploadFilemoa" runat="server" AllowMultiple="true" AutoUpdateAfterCallBack="True" AutoCallBack="true" maxRequestLength="10 MB" />
                                <Anthem:Label ID="Label4" runat="server" ForeColor="Red" AutoCallBack="true" AutoUpdateAfterCallBack="true" maxRequestLength="10 MB">
                                    File size should be 10 MB
                                </Anthem:Label><br />
                                <Anthem:Label ID="Label6" runat="server" ForeColor="Red" Text="only accept .jpg,.jpeg,.png,.pdf,.docs"></Anthem:Label>
                            </td>
                            <td class="vtext">
                                <Anthem:Label ID="Label8" Text="View File" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                            </td>
                            <td class="colon">
                                <Anthem:Label ID="Label" Text=":" runat="server" AutoUpdateAfterCallBack="true" />
                            </td>
                            <td class="required">
                                <Anthem:LinkButton ID="LinkButton1" EnableCallBack="false" runat="server" Text="" OnClick="LinkButton1_Click" ForeColor="blue"
                                    AutoUpdateAfterCallBack="true">
                                </Anthem:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="vtext" id="lbl_Active" style="width: 12%">Active</td>
                            <td class="colon">: </td>
                            <td>
                                <Anthem:CheckBox ID="chkActive" runat="server" AutoUpdateAfterCallBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td colspan="4">
                                <Anthem:Button AutoUpdateAfterCallBack="True" ID="btnSave" CssClass="btn btn-primary btn-sm" runat="server" CommandName="SAVE"
                                    OnClick="btnSave_Click" Text="SAVE" TextDuringCallBack="SAVING..." EnableCallBack="false" />

                                <Anthem:Button ID="btnReset" runat="server" CssClass="btn btn-default btn-sm" CausesValidation="False" OnClick="btnReset_Click"
                                    Text="RESET" TextDuringCallBack="RESETING.."
                                    AutoUpdateAfterCallBack="True" />
                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage" UpdateAfterCallBack="True" ForeColor="Red" />
                            </td>
                        </tr>
                    </table>
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="7" class="tablesubheading">List of Links </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="GvDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoUpdateAfterCallBack="True" Width="100%" PageSize="10" OnRowCommand="GvDetails_RowCommand" OnPageIndexChanging="GvDetails_PageIndexChanging" UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Website Link" DataField="Website_link" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="LinkedIn Link" DataField="Linkdin_Link" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Facebook Link" DataField="FB_Link" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Twitter Link" DataField="Twitter_Link" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Youtube Link" DataField="YoutubeLink" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Active" DataField="IsActive" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="EDIT">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkEdit" CommandArgument='<%# Eval("PK_Alm_Mst_Info") %>' CommandName="EDITREC"> 
                                                        <img src="../../Images/Edit.gif" alt="" border="0"></img>
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DELETE">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton runat="server" ID="lnkDelete" CommandArgument='<%# Eval("PK_Alm_Mst_Info") %>' CommandName="DELETEREC"
                                                        PreCallBackFunction="btnDelete_PreCallBack">
                                                        <img src="../../Images/Delete.gif" alt="" border="0" ></img>
                                                    </Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </Anthem:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>