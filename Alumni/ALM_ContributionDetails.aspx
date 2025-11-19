<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniMasterPage.master" AutoEventWireup="true" CodeFile="ALM_ContributionDetails.aspx.cs" Inherits="Alumni_ALM_ContributionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--    <div id="tblreceipt" runat="server">
        <div class="tablesubheading">
            Lists of Contribution
        </div>
        <div class="table-responsive">
            <Anthem:GridView ID="gvDetails" runat="server" SkinID="Nill" CssClass="table" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" HeaderStyle-CssClass="header-style" AutoUpdateAfterCallBack="True" OnRowCommand="gvDetails_RowCommand"
                ShowFooter="True" UpdateAfterCallBack="True" Width="100%" OnPageIndexChanging="gvDetails_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contributor">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <Anthem:LinkButton ID="lnkView" runat="server" AutoUpdateAfterCallBack="True" CausesValidation="False"
                                CommandArgument='<%# Eval("PK_Fund_ID") %>' CommandName="SELECT" EnableCallBack="false"
                                Text='<%# Eval("Contributor") %>' TextDuringCallBack="Loading...">                                
                            </Anthem:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Email" HeaderText="Email ID" />
                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                    <asp:BoundField DataField="ContributedAmt" HeaderText="Amount" />
                </Columns>
            </Anthem:GridView>
        </div>

        <div class="tablesubheading">
            Online Contribution Transactions History
        </div>

        <div class="table-responsive">

            <div class="gridiv">
                <Anthem:GridView ID="gvConfPayHistory" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
                    AutoUpdateAfterCallBack="True" Width="100%" UpdateAfterCallBack="True" HeaderStyle-CssClass="header-style">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Contributor Name">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <Anthem:Label ID="lblConID" runat="server" Text='<%# Eval("Contributor") %>' AutoUpdateAfterCallBack="True"></Anthem:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Email" HeaderText="Email ID">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No.">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ContribitionAmount" HeaderText="Contribition Amount">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                </Anthem:GridView>
            </div>
        </div>
    </div>--%>

    <!-- Breadcrumbs Start -->
    <div class="rs-breadcrumbs bg1 breadcrumbs-overlay">
        <div class="breadcrumbs-inner">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <h1 class="page-title">Lists of Contributor's </h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <!-- Contribution Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="gridiv">
                        <Anthem:GridView ID="gvDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoUpdateAfterCallBack="True" Width="100%" UpdateAfterCallBack="True" OnRowCommand="gvDetails_RowCommand" OnPageIndexChanging="gvDetails_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contributor Name">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <Anthem:Label ID="lblContrID" runat="server" Text='<%# Eval("Contributor") %>' AutoUpdateAfterCallBack="True"></Anthem:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Email" HeaderText="Email ID">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ContributedAmt" HeaderText="Contribution Amount">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Transaction Details">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <Anthem:LinkButton runat="server" ID="lnkView" Text="View" CausesValidation="False" CommandArgument='<%# Eval("PK_Fund_ID") %>' CommandName="SELECT" AutoUpdateAfterCallBack="True" TextDuringCallBack="Loading...">
                                        </Anthem:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </Anthem:GridView>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- Contribution End -->

    <!-- Contributor Payment History Start -->
    <div class="rs-events-list sec-spacer">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="gridiv">
                        <Anthem:GridView ID="gvConfPayHistory" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoUpdateAfterCallBack="True" Width="100%" UpdateAfterCallBack="True">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contributor Name">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <Anthem:Label ID="lblContrID" runat="server" Text='<%# Eval("Contributor") %>' AutoUpdateAfterCallBack="True"></Anthem:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Tranid" HeaderText="Transaction No">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TransDate" HeaderText="Payment Date">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ContribitionAmount" HeaderText="Contribution Amount">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                            </Columns>
                        </Anthem:GridView>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- Contributor Payment History End -->

</asp:Content>