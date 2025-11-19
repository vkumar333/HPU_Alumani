<%@ Page Language="C#" MasterPageFile="~/Alumni/AlumniMasterPage.master" AutoEventWireup="true"
    CodeFile="ALM_AlumniSuggestion.aspx.cs" Inherits="Alumni_ALM_AlumniSuggestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="container-fluid mt-10">
        <div class="">
            <div class="box box-success" runat="server">
                <div class="boxhead mt-0 mb-10">
                    Alumni Suggestion
					<a class="btn btn-warning btn-sm back-button pull-right" href="../Alumni/ALM_Alumni_Home.aspx">Back </a>
                </div>
                <div class="panel-body pnl-body-custom">

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <label class="col-sm-2 control-label">Suggestion</label>
                            <div class="col-sm-6 required">
                                <Anthem:TextBox ID="R_txtSuggession" runat="server" CssClass="form-control" MaxLength="500"
                                    TextMode="MultiLine" onkeypress="return CheckMaxLength(this,500);" onpaste="event.returnValue=false"
                                    ondrop="event.returnValue=false" AutoUpdateAfterCallBack="True" Width="425px"
                                    Height="70px"></Anthem:TextBox>
                            </div>

                        </div>
                    </div>

                    <div class="form-group form-group-sm">
                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-2">
                                <Anthem:Button ID="btnSave" runat="server" CssClass="btn btn-warning btn-sm" AutoUpdateAfterCallBack="True" CausesValidation="True"
                                    Text="SAVE & SUBMIT" OnClick="btnSave_Click" PreCallBackFunction="btnSave_PreCallBack" Width ="15%"
                                    TextDuringCallBack="Saving..." />
                                <Anthem:Button ID="btnReset" runat="server" CssClass="btn btn-warning btn-sm" CausesValidation="False" OnClick="btnReset_Click"
                                    Text="RESET" TextDuringCallBack="CLEARING.." />
                                <Anthem:Button ID="btnback" runat="server" AutoUpdateAfterCallBack="true" Text="BACK" TextDuringCallBack="SUBMITING..." EnableCallBack="false" OnClick="btnback_Click" CssClass="btn btn-primary btn-sm" />
                                <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="True" SkinID="lblmessage"></Anthem:Label>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <Anthem:GridView ID="gvAlumni" runat="server" CssClass="table-bordered table table-hover" AutoGenerateColumns="False" AutoUpdateAfterCallBack="True"
                            DataKeyNames="fk_alumniid" Width="100%" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvAlumni_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="SI No.">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Alumni Name" DataField="alumni_name">
                                    <ItemStyle Width="25%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Suggestion" DataField="Suggestion">
                                    <ItemStyle Width="60%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="On Date" DataField="suggestiondate">
                                    <ItemStyle Width="15%" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </Anthem:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>