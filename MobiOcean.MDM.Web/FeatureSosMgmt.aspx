<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="FeatureSosMgmt.aspx.cs" Inherits="MobiOcean.MDM.Web.FeatureSosMgmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="coh" runat="server" ContentPlaceHolderID="ContentHead">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active div">
                <div class="panel panel-default">
                    <div class="profile1" style="margin: 0px;">
                        Sos Management
                    <a href="javascript:;" class=" pull-right">
                        <asp:Button ID="btnAppmgmt" runat="server" class="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Sos Contact Management" OnClick="btnAppmgmt_Click" />
                    </a>
                    </div>
                </div>

                <div style="text-align: center; color: #FFFFFF" class="profile2 col-lg-4 ">
                    <h4 style="margin-top: 0px; margin-bottom: 0px;">Profile Name :
                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                </div>
                <div class="clearfix"></div>
                <br />
                <asp:Panel ID="PanelAppMgmt" runat="server" Visible="false">
                    <div class="row" style="text-align: center">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                    <div class="row" style="text-align: right">
                        <a href="#" id="flip" class="btn btnd btncompt waves-effect waves-light"><i class="fa fa-plus">
                            </i>&nbsp;&nbsp;<span class="creatsp">Add Contact</span></a>
                    </div>
                    <div id="panel" class="flipkey">
                        <div class=" row">
                            <div class="col-lg-7 col-md-12">
                                <div class="form-group ">
                                    <label for="bname" class="control-label col-lg-4">Name : </label>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv" Display="Dynamic" runat="server" ControlToValidate="txtName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label for="firstname" class="control-label col-lg-4">Designation : </label>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="Designation"></asp:TextBox>
                                        <br />
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label for="firstname" class="control-label col-lg-4">Contact No : </label>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Contact No" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="MOoldsf" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender ID="fmob" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers" />
                                        <br />
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label for="firstname" class="control-label col-lg-4">Email Id : </label>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" placeholder="Email Id"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="xyz@xyz.xyz" ForeColor="Red" ValidationGroup="save"
                                            ControlToValidate="txtEmailId" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <br />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-offset-4 col-lg-6">
                                        <asp:Button ID="btnSaveForm" runat="server" Text="Save" CssClass="btn btnd btncompt" OnClick="btnSaveForm_Click" ValidationGroup="save" />&nbsp;
                                            <asp:Button ID="CancelForm" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="CancelForm_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" style="text-align: center">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <br />

                    <div class="table-responsive">
                        <asp:GridView ID="grdAppGrp" runat="server" DataKeyNames="ProfileSosId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            AllowPaging="false" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." Width="100%" OnRowCancelingEdit="grdAppGrp_RowCancelingEdit" OnRowDeleting="grdAppGrp_RowDeleting"
                            OnRowEditing="grdAppGrp_RowEditing" OnRowUpdating="grdAppGrp_RowUpdating" OnRowDataBound="grdAppGrp_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Bind("ProfileSosId") %>'></asp:Label>
                                        <asp:Label ID="lblProfileID" runat="server" Text='<%#Bind("ProfileId") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkbox" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="Sos_parent" runat="server" AutoPostBack="true" OnCheckedChanged="Sos_parent_CheckedChanged" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Person Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrpCode" runat="server" Text='<%#Eval("ContactPersonName").ToString()==""?"---":Eval("ContactPersonName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrpName" runat="server" Text='<%#Eval("Designation").ToString()==""?"---":Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmobNo" runat="server" Text='<%#Eval("ContactNo").ToString()==""?"---":Eval("ContactNo") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EmailId").ToString()==""?"---":Eval("EmailId") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="row" style="text-align: right">
                            <asp:Button ID="btnApplyProfileChanges" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnApplyProfileChanges_Click" />
                        </div>
                    </div>
                </asp:Panel>





                <br />
                <div class="table-responsive">
                    <asp:GridView ID="grdDevice" runat="server" DataKeyNames="FeatureId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." Width="100%" OnRowDataBound="grdDevice_RowDataBound" OnRowCommand="grdDevice_RowCommand">


                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("FeatureId")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ProfileId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId")%>'></asp:Label>
                                    <asp:Label ID="lblProfileFeatureId" runat="server" Text='<%#Eval("ProfileFeatureMappingId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Feature Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFeatureName" runat="server" Text='<%#Eval("FeatureName")%>'></asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                                <asp:TextBox id="txtProfileCode" runat="server" Text='<%#Eval("ProfileCode")%>' cssclass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtProfileCode" ErrorMessage="*" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>--%>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enabled">
                                <ItemTemplate>
                                    <asp:CheckBox ID="switchsize" runat="server" CssClass="toggleswitch" data-on-text="Yes" data-off-text="No" data-size="small" Visible="false" Checked="false" />
                                    <asp:ImageButton ID="btnyes" runat="server" ImageAlign="Middle" ImageUrl="~/image/bigYesNew.png" Visible="false" CommandName="Yes" />
                                    <asp:ImageButton ID="btnNo" runat="server" ImageAlign="Middle" ImageUrl="~/image/BigNoNew.png" Visible="false" CommandName="No" />
                                    <asp:Label ID="lblChanged" runat="server" Text='<%#Eval("IsChanged")%>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Schedule" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsScheduleNeed" runat="server" Visible="false" Text='<%#Eval("IsScheduleNeed")%>'></asp:Label>
                                    <asp:LinkButton ID="Schedule" runat="server" Text="Schedule" CssClass="btn btnd btncompt" CommandName="Schedule" />
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                    </asp:GridView>


                    <div>
                        <asp:Button ID="btnsubmit" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnsubmit_Click" />
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="btncancel_Click" />

                    </div>
                </div>
            </div>
        </div>
        <!-- train section -->
    </div>
      <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>

      <asp:Button ID="btnCheckFeature" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="MPCheckFeature" runat="server" PopupControlID="CheckFeaturePnl"
        TargetControlID="btnCheckFeature" CancelControlID="btnccl"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="CheckFeaturePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" BackColor="#2a368b" aria-hidden="true">

        <div class="modal-body" style="text-align: center;">
            <asp:Button ID="Button5" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label2" runat="server" Text="You don't have license to enable this feature. Please purchase more license." ForeColor="Red"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnCheckFeatureCancel" runat="server" Text="OK" OnClick="btnCheckFeatureCancel_Click" />
        </div>


    </asp:Panel>

    <asp:Button ID="dummy" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="MP1" runat="server" PopupControlID="MessagePnl"
        TargetControlID="dummy" CancelControlID="btnccl"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>

    <asp:Panel runat="server" ID="MessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

        <div class="modal-body" style="text-align: center; color: green;">
            <asp:Button ID="btnccl" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="message" runat="server" Text="Changes saved successfully!"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="ok" runat="server" Text="OK" OnClick="ok_Click" />
        </div>


    </asp:Panel>
    <asp:Button ID="dummydelete" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="mpcancel" runat="server" PopupControlID="DeleteMessagePnl"
        TargetControlID="dummydelete" CancelControlID="btnccl"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>

    <asp:Panel runat="server" ID="DeleteMessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

        <div class="modal-body" style="text-align: center">
            <asp:Button ID="Button3" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label3" runat="server" Text="Are you sure you want to leave the page?"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btncancelok" runat="server" Text="OK" OnClick="btncancelok_Click" />
            <asp:Button ID="btncancelcan" runat="server" Text="Cancel" OnClick="btncancelcan_Click" />
        </div>


    </asp:Panel>
    <!-- ============================================================== -->
    <!-- End Right content here -->
    <!-- ============================================================== -->
  
    <script>
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $("#flip").click(function () {
                    $("#panel").slideToggle("slow");
                    $('#<%=lblMsg.ClientID%>').html("");
                         $('#<%=lblMessage.ClientID%>').html("");
                     });
                     $("#CancelForm").click(function () {
                         $("#panel").slideToggle("slow");
                         $('#<%=lblMessage.ClientID%>').html("");
                         $('#<%=lblMsg.ClientID%>').html("");
                     });
                 });
             }
    </script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            if (!args.get_isPartialLoad()) {
                //  adding handler to the document's keydown event
                $addHandler(document, "keydown", onKeyDown);
            }
            $(document).ready(function () {
                $("#flip").click(function () {
                    $("#panel").slideToggle("slow");
                });
                $("#Cancel").click(function () {
                    $("#panel").slideToggle("slow");
                });
            });
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find("<% =MP1.ClientID%>").hide();
                $find("<% =mpcancel.ClientID%>").hide();
            }
        }

    </script>
</asp:Content>
