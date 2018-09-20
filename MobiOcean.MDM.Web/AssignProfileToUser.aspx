﻿<%@ Page Title="Assign Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AssignProfileToUser.aspx.cs" Inherits="MobiOcean.MDM.Web.AssignProfileToUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
    
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
            <div class="content-page">
                <!-- Start content -->
                <div class="content padding-top-none">
                    <!-- Page-Title -->
                    <div class="container whitebg padding-top-20">
                        <div class="row margin-none">
                            <div class="col-sm-12">
                                <div class="col-sm-10">
                                    <h1 class="pull-left page-title">Assign Profile</h1>
                                </div>
                                <div class="col-sm-2 pull-right">
                                    <h3 style="display: inline-block;">
                                        
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                   
                    <div class="content m-t-20">
                        <div class="container">
                            <div class="panel">
                                <div class="panel-body">
                                  

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel-group panel-group-joined" id="accordion-test">


                                                <div class=" form">
                                                    
                                                    <div class="form-group ">
                                                        <label for="company" class="control-label col-lg-4">User Name* : </label>
                                                        <div class="col-lg-6">
                                                            <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control input-sm" AppendDataBoundItems="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group ">

                                                        <div class="col-lg-10">
                                                            <br />
                                                        </div>
                                                    </div>

                                                    <div class="form-group ">
                                                        <label for="firstname" class="control-label col-lg-4">Profile Name* : </label>
                                                        <div class="col-lg-6">
                                                            <asp:DropDownList ID="ddlProfile" runat="server" CssClass="form-control input-sm" AppendDataBoundItems="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group ">

                                                        <div class="col-lg-10">
                                                            <br />
                                                        </div>
                                                    </div>
                                                    <div class="form-group ">

                                                        <div class="col-lg-10" style="text-align: center">
                                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group ">

                                                        <div class="col-lg-10">
                                                            <br />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-lg-offset-2 col-lg-10" style="text-align: center">
                                                            <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Assign" OnClick="btnAssign_Click" />
                                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" />
                                                        </div>
                                                    </div>
                                                     
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

       
</asp:Content>
