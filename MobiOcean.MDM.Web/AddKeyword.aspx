<%@ Page Title="Add Keyword" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeBehind="AddKeyword.aspx.cs" Inherits="MobiOcean.MDM.Web.AddKeyword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="content-page">
            <!-- Start content -->
            <div class="content padding-top-none">
                <!-- Page-Title -->
                <div class="container whitebg padding-top-20">
                    <div class="row margin-none">
                        <div class="col-sm-12">
                            <div class="col-sm-10">
                                <h1 class="pull-left page-title">Add Keyword</h1>
                            </div>
                            <div class="col-sm-2 pull-right">
                                <h3 style="display: inline-block;"></h3>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Start content -->
                <div class="content m-t-20">
                    <div class="container">
                        <div class="panel">
                            <div class="panel-body">


                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="panel-group panel-group-joined" id="accordion-test">

                                            <div class=" form">

                                                <div class="col-lg-7">
                                                    <div class="form-group ">
                                                        <label for="bname" class="control-label col-lg-4">Keyword Code* : </label>
                                                        <div class="col-lg-8">
                                                            <asp:TextBox ID="txtKCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                ControlToValidate="txtKCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group ">
                                                    </div>
                                                    <div class="form-group ">
                                                        <label for="firstname" class="control-label col-lg-4">Keyword Name* : </label>
                                                        <div class="col-lg-8">
                                                            <asp:TextBox ID="txtKName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtKName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                    <div class="form-group ">
                                                        <label for="lastname" class="control-label col-lg-4">Description* : </label>
                                                        <div class="col-lg-8">
                                                            <asp:TextBox ID="txtKDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtKDesc" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>


                                                    <div class="form-group">
                                                        <label class="control-label col-lg-4"></label>
                                                        <div class="col-lg-8">
                                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-lg-offset-4 col-lg-6">
                                                            <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt" Text="Save" ValidationGroup="save" OnClick="btnAssign_Click" />

                                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" CommandName="Cancel" OnClick="btnCancel_Click" />

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-5">
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <!-- end: page -->
                            </div>
                            <!-- end Panel -->
                        </div>
                        <!-- container -->
                    </div>
                    <!-- content -->
                    <!-- container -->
                </div>
                <!-- content -->

            </div>
        </div>
    </div>

</asp:Content>



