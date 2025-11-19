<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="ALM_Country_Mst.aspx.cs" Inherits="Alumni_ALM_Country_Mst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-12">
        <div class="row">
            <div class="box box-success form-horizontal">
                <div class="box-body">

                    <div class="tableheading mb-3">Country Master </div>

                    <div class="form-group">
                        <label class="col-sm-2 control-label" id="lblstate">Country : </label>
                        <div class="col-sm-4">
                            <Anthem:TextBox ID="txtCountry" runat="server" AutoUpdateAfterCallBack="True" MaxLength="150" SkinID="textbox"></Anthem:TextBox>
                        </div>
                        <label class="col-sm-2 control-label" id="lblcity">Code : </label>
                        <div class="col-sm-4 required">
                            <Anthem:TextBox ID="txtCountryCode" runat="server" AutoUpdateAfterCallBack="True" MaxLength="10" SkinID="textbox"></Anthem:TextBox>*
                    
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-10 col-sm-offset-2">
                            <Anthem:Button ID="btnSave" runat="server" AutoUpdateAfterCallBack="true" CssClass="btn btn-primary btn-sm"
                                CommandName="SAVEDATA" TextDuringCallBack="SAVING..." OnClick="btnSave_Click" Text="SAVE" />
                            <Anthem:Button ID="btnReset" runat="server" AutoUpdateAfterCallBack="true" Text="RESET" OnClick="btnReset_Click" />
                            <Anthem:Label ID="lblMsg" runat="server" AutoUpdateAfterCallBack="true"></Anthem:Label>
                        </div>
                    </div>

                    <div class="tablesubheading mb-3">
                        List of Countries
                        <Anthem:LinkButton ID="lnkSearch" CssClass="pull-right" runat="server" AutoUpdateAfterCallBack="true" OnClick="lnkSearch_Click">Search Criteria</Anthem:LinkButton>
                    </div>

                    <div>
                        <Anthem:Panel ID="pnlSearch" runat="server" AutoUpdateAfterCallBack="true">
                            
                            <div class="form-group">                                
                                <label class="col-sm-2 control-label">Country : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtCountryS" runat="server" SkinID="textboxlong" AutoUpdateAfterCallBack="true" MaxLength="100" />
                                </div>

                                <label class="col-sm-2 control-label">Code : </label>
                                <div class="col-sm-4 required">
                                    <Anthem:TextBox ID="txtCountryCodeS" runat="server" SkinID="textboxlong" AutoUpdateAfterCallBack="true" MaxLength="10" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-10 col-sm-offset-2">
                                    <Anthem:Button ID="btnSearch" runat="server" CommandName="SEARCH" OnClick="btnSearch_Click" Text="SEARCH" AutoUpdateAfterCallBack="true" 
                                        TextDuringCallBack="WAIT..." />
                                    &nbsp;&nbsp;
                                <Anthem:Button ID="btnClear" runat="server" CausesValidation="False" OnClick="btnClear_Click" Text="RESET" AutoUpdateAfterCallBack="true" 
                                    TextDuringCallBack="WAIT..." />

                                    <Anthem:Label ID="lblMsg1" runat="server" SkinID="lblmessage" AutoUpdateAfterCallBack="true" />
                                </div>
                            </div>

                        </Anthem:Panel>
                    </div>

                </div>

                <div class="gridiv" style="width: 100%;">
                    <Anthem:GridView ID="gvCountry" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCountry_PageIndexChanging"
                        OnRowCommand="gvCountry_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Country" HeaderText="Country">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Code" HeaderText="Code">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="EDIT">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <Anthem:LinkButton runat="server" ID="lnkEdit" CausesValidation="False" CommandArgument='<%# Eval("PK_Country_ID") %>'
                                        TextDuringCallBack="Loading..." AutoUpdateAfterCallBack="True" CommandName="Select">
                                <img src="../../Images/Edit.gif" alt="" border="0"/>
                                    </Anthem:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DELETE">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <Anthem:LinkButton runat="server" ID="lnkDelete" CausesValidation="False" CommandArgument='<%# Eval("PK_Country_ID") %>'
                                        TextDuringCallBack="Deleting..." AutoUpdateAfterCallBack="True"
                                        CommandName="DELETEREC">
                                <img src="../../Images/Delete.gif" alt="" border="0" />
                                    </Anthem:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </Anthem:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>