<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddOTAPackage.aspx.cs" Inherits="MobiOcean.MDM.Web.AddOTAPackage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <link href="Content/css/ApkStylee.css" rel="stylesheet" />
    <style>
        .msgpopup {
            background-color: #2a368b;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
            border: 2px solid white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">
                <div class="profile1">
                    <asp:Label ID="lblAppName" runat="server"></asp:Label>
                    OS Package
                </div>
                <br />
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <br />
                <%--  <div class="form-horizontal">
		<div class="row">--%>
                <div class="col-md-12" id="edvis" runat="server">
                    <div class="box1 box-danger">
                        <div class="box-body">
                            <div>
                                <label class="col-sm-2 control-label">Os File Upload <i>*</i></label>
                                <div class="col-sm-4">
                                    <asp:FileUpload ID="OsUpload" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="OsUpload" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>

                    </div>
                </div>

                <%--		</div>
	</div>--%>
                <div class="col-md-12">
                    <div class="box1 box-danger">
                        <div class="box-header with-border">
                            <h3 class="box-title">OS Package Info </h3>
                        </div>
                        <div class="box-body">

                            <div class="form-group">
                                <label class="col-sm-2 control-label">Package Name </label>

                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPackageName" runat="server" CssClass="form-control" placeholder="Please Enter Package Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtPackageName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>


                                </div>
                                <label class="col-sm-2 control-label">Version Name</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtVersion" runat="server" CssClass="form-control" placeholder="Please Enter Version Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtVersion" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>

                                </div>

                            </div>
                            <div class="clearfix">
                                <br />
                                <br />
                            </div>
                            <div class="form-group">

                                <label class="col-sm-2 control-label">Version No</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtVersionNo" runat="server" CssClass="form-control" placeholder="Plz Enter Version No"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVersionNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="clearfix">
                                <br />
                                <br />
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">Remark </label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtRemark" TextMode="multiline" Rows="5" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="clearfix">
                                <br />

                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <asp:Button ID="btnsubmit" CssClass="btn btn-lg btn-success pull-right ajax-submit" Text="Save" runat="server" OnClick="btnsubmit_Click" ValidationGroup="upload" />
                    </div>
                    <div class="col-sm-6">
                        <asp:Button ID="btnCancel" CssClass="btn btn-lg btn-danger pull-left ajax-submit" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="dummysuccess" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="successpnl"
        TargetControlID="dummysuccess" CancelControlID="btnsuccan"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>

    <asp:Panel runat="server" ID="successpnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

        <div class="modal-body" style="text-align: center; color: white;">
            <asp:Button ID="btnsuccan" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnsucok" runat="server" Text="OK" OnClick="btnsucok_Click" />
        </div>


    </asp:Panel>
</asp:Content>
