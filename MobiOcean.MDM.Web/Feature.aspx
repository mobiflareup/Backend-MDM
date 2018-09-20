<%@ Page Title="Feature Management" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="Feature.aspx.cs" Inherits="MobiOcean.MDM.Web.Feature" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">


        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">
                    <div class="profile1">Feature Management</div>
                    <br />
                    <div style="text-align: center; background-color: ; color: #FFFFFF" class="col-lg-4 profile2 ">
                        <h4 style="margin-top:0px;margin-bottom:0px;">Profile Name :
                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                    </div>
                    <div class="col-lg-8" style="text-align: right">
                        <b>To push the final changes click here :</b>
                        <asp:Button ID="Button1" CssClass="btn btnd btncompt" runat="server" Text="Push Changes" OnClick="btnpush_Click" />
                    </div><br />
                    <div class="clearfix"></div>
                    
                   <br />

                    <br />
                    <div class="row" style="text-align: center">
                        <asp:Repeater ID="rptr1" runat="server" OnItemDataBound="rptr1_ItemDataBound" OnItemCommand="rptr1_ItemCommand">
                            <ItemTemplate>
                                <div class="col-md-3">
                                    <div class="custom-features" style="border:4px solid #ccc;margin-bottom:20px;padding:10px;border-radius:5px;">
                                    <asp:Label ID="lblIdr" runat="server" Text='<%#Eval("CategoryId") %>' Visible="false"></asp:Label>
                                    <%--<asp:Label ID="lblHoverr" runat="server" Text='<%#Eval("ImageHoverUrl") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblSimr" runat="server" Text='<%#Eval("ImageUrl") %>' Visible="false"></asp:Label>--%>
                                    <asp:Label ID="lblSub" runat="server" Text='<%#Eval("AppliedSubscriptionId")%>'  Visible="false"></asp:Label>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status")%>'  Visible="false"></asp:Label>
                                    <asp:ImageButton ID="imgr" runat="server" ImageUrl='<%#Eval("ImageUrl") %>' CommandName="Image" />
                                    <br />
                                    <asp:Label ID="lblCategoryNamer" style ="font-weight:bold;font-size:14px;" runat="server" Text='<%#Eval("CategoryName") %>'></asp:Label>
                                        </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="row" style="text-align: center">

                    </div>
                    <!-- train section -->
                </div>

            </div>

        </div>


        <asp:Button ID="dummy" runat="server" Style="display: none;" />

        <asp:ModalPopupExtender ID="MP2" runat="server" PopupControlID="MessagePnl"
            TargetControlID="dummy" CancelControlID="btnccl"
            BackgroundCssClass="modalbackground">
        </asp:ModalPopupExtender>

        <asp:Panel runat="server" ID="MessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

            <div class="modal-body" style="text-align: center; color: green;background:#fff;">
                <asp:Button ID="btnccl" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                <asp:Label ID="message" runat="server" Text="Changes pushed to devices successfully!"></asp:Label>
            </div>
            <div class="modal-footer">
                <asp:Button ID="ok" runat="server" Text="OK" OnClick="ok_Click" />
            </div>


        </asp:Panel>
    <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>
</asp:Content>




