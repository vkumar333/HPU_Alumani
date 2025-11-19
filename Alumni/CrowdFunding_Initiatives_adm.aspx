<%@ Page Title="" Language="C#" MasterPageFile="~/UMM/MasterPage.master" AutoEventWireup="true" CodeFile="CrowdFunding_Initiatives_adm.aspx.cs" Inherits="Alumni_CrowdFunding_Initiatives_adm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../include/jquery.min.js"></script>--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var specialKeys = new Array();

        specialKeys.push(8); //Backspace  

        function numericOnly(elementRef) {

            var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

            if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {

                return true;

            }

                // '.' decimal point...  

            else if (keyCodeEntered == 46) {

                // Allow only 1 decimal point ('.')...  

                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))

                    return false;

                else

                    return true;

            }

            return false;

        }
    </script>
    <%-- <script type="text/javascript">
        $(function () {
            debugger;
            $('#ctl00_ContentPlaceHolder1_Goal_amt_txt').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                    else {
                        alert
                    }
                }
            });
        });
    </script>--%>
    <div class="col-md-12">
        <div class="row">
            <div class="box box-success">
                <div class="box-body table-responsive">
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="12" class="tableheading">Crowd Funding Information</td>
                        </tr>
                        <tr>
                            <td colspan="2" id="lblAlbum" class="vtext">Categories</td>
                            <td class="required" colspan="2">
                                <Anthem:DropDownList ID="D_ddlCategories" CssClass="dropdown" runat="server"
                                    AutoUpdateAfterCallBack="true" AutoCallBack="true"
                                    OnSelectedIndexChanged="D_ddlCategories_SelectedIndexChanged" />*
                            </td>
                            <td colspan="2" id="lblHeading" class="vtext" style="width: 15%">Heading :</td>
                            <td class="required" colspan="2">
                                <Anthem:TextBox ID="text_heading" runat="server" AutoUpdateAfterCallBack="True"
                                    TextMode="SingleLine"
                                    SkinID="textbox"></Anthem:TextBox>*
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" id="lblDiscription" class="vtext" style="width: 15%">Description :</td>
                            <td class="required" colspan="2">

                                <Anthem:TextBox ID="R_txtDiscription" runat="server" AutoUpdateAfterCallBack="True"
                                    TextMode="MultiLine"
                                    SkinID="textbox"></Anthem:TextBox>*
                            </td>
                            <td colspan="2" id="lblgoalamt" class="vtext" style="width: 15%">Amount to be Raised :</td>
                            <td class="required" colspan="2">

                                <Anthem:TextBox ID="Goal_amt_txt" MaxLength="15" onkeypress="return numericOnly(this);" runat="server" AutoUpdateAfterCallBack="True"
                                    TextMode="SingleLine"
                                    SkinID="textbox"></Anthem:TextBox>*
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2" id="lblImage" class="vtext">Photo Uploads :</td>
                            <td colspan="2" class="required" style="padding-top: 45px;"><%--AllowMultiple="true"--%>
                                <Anthem:FileUpload ID="flUploadLogo" AutoUpdateAfterCallBack="true" runat="server" />*
                                &nbsp;
                                <asp:Label ID="lnkphotoname" runat="server" ForeColor="Red" AutoUpdateAfterCallBack="true"></asp:Label>
                                <span style="font-weight: normal">
                                    <br />
                                    File size shouldn’t be greater than 5 Mb
                                    <br />
                                    format type. as in (PNG, JPEG, JPG)</span>
                                <Anthem:LinkButton runat="server" ID="lnkviewBrc" CausesValidation="False"
                                    AutoUpdateAfterCallBack="True" OnClick="lnkviewBrc_Click"></Anthem:LinkButton>
                            </td>

                            <%--<td class="vtext" id="lbl_Active" style="width: 12%">Set On Album</td>
                            <td class="colon">:</td>
                            <td>
                                <Anthem:CheckBox ID="chkhomepage" runat="server" AutoUpdateAfterCallBack="True" />
                            </td>--%>
                        </tr>
                        <tr>
                            <td colspan="2" id="lbldImage" class="vtext"></td>
                            <td>
                                <Anthem:GridView runat="server" ID="GrdFile" Visible="false" AutoGenerateColumns="false" OnRowCommand="GrdFile_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Names">
                                            <ItemTemplate>
                                                <Anthem:LinkButton ID="LnkDownload" runat="server" Text='<%# Bind("Image") %>' CommandName="Download"></Anthem:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View File">
                                            <ItemStyle HorizontalAlign="Center" Width="25%" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <a target="_blank" id="anchorPath" href='<%#SetServiceDoc(Eval("Image").ToString()) %>'>View </a>
                                                <%--<a runat="server" target="_blank" id="anchorPath">View File</a>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SetOnlyOne">
                                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                                            <ItemTemplate>
                                                <Anthem:CheckBox ID="chkLeave" CssClass="chkboxs" AutoUpdateAfterCallBack="true" AutoPostBack="true" runat="server" OnCheckedChanged="chkLeave_CheckedChanged" />
                                                <Anthem:HiddenField ID="hdnspkid" runat="server" Value='<%# Eval("Pk_File_Detail_id") %>' AutoUpdateAfterCallBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DELETE">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <Anthem:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Are you sure to delete this record?');" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="DELETEREC" CommandArgument='<%# Eval("Pk_File_Detail_id") %>'> <img src="../../Images/Remove.jpeg" alt="" border="0"></img></Anthem:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </Anthem:GridView>
                            </td>


                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: center">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm"
                                    CommandName="SAVE" OnClick="btnSave_Click" Text="SAVE" />
                                <Anthem:Button ID="Reset" runat="server" CausesValidation="False" CssClass="btn btn-default btn-sm" Text="RESET" TextDuringCallBack="RESETING.." AutoUpdateAfterCallBack="True"
                                    OnClick="Reset_Click" />
                                <Anthem:Label ID="Label1" runat="server" AutoUpdateAfterCallBack="True"
                                    SkinID="lblmessage" Style="font-weight: normal" ForeColor="Red" UpdateAfterCallBack="True" />
                            </td>
                        </tr>
                    </table>
                    <table class="table mobile_form" width="100%">
                        <tr>
                            <td colspan="7" class="tablesubheading">List of Uploded Details </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="gridiv">
                                    <Anthem:GridView ID="gvDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="True" Width="100%" PageSize="10" OnPageIndexChanging="gvDetails_PageIndexChanging"
                                        OnRowCommand="gvDetails_RowCommand"
                                        UpdateAfterCallBack="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Headings">
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblHeading" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Heading") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblDiscription" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Description") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Goal Amount">
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblGoal_Amount" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("Goal_Amount") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Set On Album">
                                                <ItemTemplate>
                                                    <Anthem:Label ID="lblSetOnHomepage" runat="server" AutoUpdateAfterCallBack="true" Text='<%#Eval("SetOnHomepage") %>'></Anthem:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Upload Photos">
                                                <ItemTemplate>
                                                    <Anthem:Image ID="image1" runat="server" Height="100px" Width="100px" AutoUpdateAfterCallBack="true" ImageUrl='<%# "~/ALM_uploadimg/"+Eval("Image")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="EDIT">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton ID="lnkbtnEdit" TextDuringCallBack="WAIT.." runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="EDITREC" CommandArgument='<%# Eval("pk_contribution_ID") %>'> <img src="../../Images/Edit.gif" alt="" border="0"></img></Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DELETE">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <Anthem:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Are you sure to delete this record?');" runat="server" AutoUpdateAfterCallBack="true" CausesValidation="false" CommandName="DELETEREC" CommandArgument='<%# Eval("pk_contribution_ID") %>'> <img src="../../Images/Delete.gif" alt="" border="0"></img></Anthem:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </Anthem:GridView>
                                </div>
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



    <%--<input type="button"  value="Test button" class="btn btn-primary btn-sm"/>


    <script>
        $("#ctl00_ContentPlaceHolder1_btnUpload").click(function (evt) {
            debugger;
            var fileUpload = $("#f_UploadImage").get(0);
            var files = fileUpload.files;

            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            data.append("Categories", $("#ctl00_ContentPlaceHolder1_D_ddlCategories").val());
            data.append("Heading", $("#ctl00_ContentPlaceHolder1_text_heading").val());
            data.append("Description", $("#ctl00_ContentPlaceHolder1_R_txtDiscription").val());
            data.append("Amount", $("#ctl00_ContentPlaceHolder1_Goal_amt_txt").val());
            $.ajax({
                url: "fileUploader.ashx",
                type: "POST",
                data: data,
                contentType: false,
                processData: false,
                success: function (result) {
                    alert(result);
                    //Reload page
                },
                error: function (err) {
                    alert(err.statusText)
                }
            });

            evt.preventDefault();
        });

    </script>--%>
    <%-- <script type="text/javascript" src="../Script/jquery-1.5.1min.js"></script>--%>
    <%--    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.3/jquery.min.js"></script>--%>

    <script type="text/javascript">

        function toggleSelectionGrid(source) {
            debugger;
            var isChecked = source.checked;
            $("#GrdFile input[id*='chkLeave']").each(function (index) {
                $(this).attr('checked', false);
            });
            source.checked = isChecked;
        }

        //$(function () {
        //    $("[id*=GridView1] input[type=checkbox]").click(function () {
        //        if ($(this).is(":checked")) {
        //            $("[id*=GridView1] input[type=checkbox]").removeAttr("checked");
        //            $(this).attr("checked", "checked");
        //        }
        //    });
        //});

        //$(function () {
        //    $("[id*=GrdFile] input[type=checkbox]").click(function () {
        //        debugger;
        //        if ($(this).is(":checked")) {
        //            $("[id*=GrdFile] input[type=checkbox]").removeAttr("checked");
        //            $(this).attr("checked", "checked");
        //        }
        //    });
        //});

    </script>

</asp:Content>