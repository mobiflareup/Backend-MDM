<%@ Page Title="Manage Conveyance" Language="C#" MasterPageFile="~/MasterPage.master"
     AutoEventWireup="true" CodeBehind="EmpConveyance.aspx.cs" Inherits="MobiOcean.MDM.Web.EmpConveyance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3"> 
              <div class="force-overflow">  
            <!-- flight section -->
            <div class="bhoechie-tab-content active">
                <div class="profile1">Manage Conveyance</div>
                <br />
                <br />

                <div class="table-responsive">
                    <asp:GridView ID="ConveyKM" runat="server" DataKeyNames="UserId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                        AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No record found." Width="100%" >

                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conveyance Rate/km">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtkm" runat="server"  Text='<%#Eval("KM").ToString() == "" ?"0" : Eval("KM")%>'></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="txtkm" ForeColor="Red" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid data" ValidationGroup="save" ValidationExpression="\d+(\.\d{2})?|\.\d{2}"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                        
                    </asp:GridView>

                </div>
                <div>
                     <asp:Label ID="lblerror" runat="server" Visible="false" style="text-align:center"></asp:Label>
                </div>
                <div>
                     <asp:Button ID="btnsubmit" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnsubmit_Click" ValidationGroup="save" />
                 <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="btncancel_Click" />

                </div>
                
            </div>

            <!-- train section -->
        </div>

            </div>

    <asp:Button ID="dummydelete" runat="server" style="display:none;"/>

                <asp:ModalPopupExtender ID="mpcancel" runat="server" PopupControlID="DeleteMessagePnl"
                    TargetControlID="dummydelete" CancelControlID="btnccl" 
                    BackgroundCssClass="modalbackground">
                </asp:ModalPopupExtender>
                <asp:Panel runat="server" ID="DeleteMessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">
                              
                     <div class="modal-body" style="text-align:center";>
                                    <asp:Button ID="Button2" runat="server" Text="x" class="close btn btnd btncompt" style="display:none;" />
                                    <asp:Label ID="Label2" runat="server" Text="Are you sure you want to leave the page?"></asp:Label>
                         </div>
                        <div class="modal-footer">
                            <asp:Button ID="btncancelok" runat="server" Text="OK" OnClick="btncancelok_Click" />
                            <asp:Button ID="btncancelcan" runat="server" Text="Cancel" OnClick="btncancelcan_Click" />
                        </div>
                    

                </asp:Panel>

    <asp:Button ID="dummy" runat="server" style="display:none;"/>

                <asp:ModalPopupExtender ID="MP1" runat="server" PopupControlID="MessagePnl"
                    TargetControlID="dummy" CancelControlID="btnccl" 
                    BackgroundCssClass="modalbackground">
                </asp:ModalPopupExtender>

                <asp:Panel runat="server" ID="MessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">
                              
                     <div class="modal-body" style="text-align:center;color:green;">
                                    <asp:Button ID="btnccl" runat="server" Text="x" class="close btn btnd btncompt" style="display:none;" />
                                    <asp:Label ID="message" runat="server" Text="Changes saved successfully!"></asp:Label>
                         </div>
                        <div class="modal-footer">
                            <asp:Button ID="ok" runat="server" Text="OK" OnClick="ok_Click" />
                        </div>
                     </asp:Panel>
      <script type="text/javascript">
          function pageLoad(sender, args) {
              if (!args.get_isPartialLoad()) {
                  //  adding handler to the document's keydown event
                  $addHandler(document, "keydown", onKeyDown);
              }
          }
          function onKeyDown(e) {
              if (e && e.keyCode == Sys.UI.Key.esc) {
                  // if the key pressed is the escape key, then close the dialog
                  $find("<% = MP1.ClientID%>").hide();
                $find("<% = mpcancel.ClientID%>").hide();

            }
        }

    </script>

</asp:Content>

