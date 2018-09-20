<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="CorporateDataSecurity.aspx.cs" Inherits="MobiOcean.MDM.Web.CorporateDataSecurity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
     <%--<form id="form1" runat="server">--%>
        <script type="text/javascript">
            function pageLoad(sender, args) {
                if (!args.get_isPartialLoad()) {
                    //  adding handler to the document's keydown event
                    $addHandler(document, "keydown", onKeyDown);
                }
            }
            function onKeyDown(e) {
                if (e && e.keyCode == Sys.UI.Key.esc) {
                    $find("<% =MP1.ClientID%>").hide();
                    $find("<% =mpcancel.ClientID%>").hide();
             }
         }
        </script>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->

        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">
                    <li class="profile1"><i>
                        <img src="image/feature.png" /></i>Corporate Data Security</li>
                    <br />
                    <div style="text-align: center; background-color: #2A368B; color: #FFFFFF" class="col-lg-4">
                        <h4>Profile Name :
                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                    </div>
                    <br />
                    <br />
                    <br />

                    <div class="row">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-12">
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

                            </div>
                        </div>
                        <%--<div class="col-sm-2"></div>--%>
                    </div>
                    <div>
                        <asp:Button ID="btnsubmit" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnsubmit_Click" />
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="btncancel_Click" />

                    </div>

                </div>
            </div>
            <!-- train section -->
        </div>

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
     <asp:Button ID="dummydelete" runat="server" style="display:none;"/>

                <asp:ModalPopupExtender ID="mpcancel" runat="server" PopupControlID="DeleteMessagePnl"
                    TargetControlID="dummydelete" CancelControlID="btnccl" 
                    BackgroundCssClass="modalbackground">
                </asp:ModalPopupExtender>

                <asp:Panel runat="server" ID="DeleteMessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">
                              
                     <div class="modal-body" style="text-align:center";>
                                    <asp:Button ID="Button3" runat="server" Text="x" class="close btn btnd btncompt" style="display:none;" />
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
    <%--</form>--%>
</asp:Content>
