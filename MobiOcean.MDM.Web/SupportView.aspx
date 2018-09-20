<%@ Page Title="Suppert detail" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="SupportView.aspx.cs" Inherits="MobiOcean.MDM.Web.SupportView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- Start content -->
            <div class="bhoechie-tab-content active">
                <div class="profile1" style="margin: 0px;">
                    View Support Ticket
                      
                                <div class="clearfix"></div>
                </div>

                <br />
                <br />



               
                <!-- Start content -->
                <div class="m-t-20">
                    <%--<div class="container">--%>
                    <div class="panel">
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel-group panel-group-joined" id="accordion-test">
                                        <div class="panel panel-default margin-top-20">
                                            <div class="panel-body table-rep-plugin">
                                                <div class=" form">
                                                    <div class="col-lg-7">
                                                        <div class="form-group ">
                                                            <label for="company" class="control-label col-lg-4">User Name :</label>
                                                            <div class="col-lg-8">
                                                                <asp:TextBox ID="UtxtUserName" runat="server" ReadOnly="true" Text='<%#Eval("UserName")%>' CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">

                                                            <div class="col-lg-12">
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <label for="firstname" class="control-label col-lg-4">Defect Name *:</label>
                                                            <div class="col-lg-8">
                                                                <asp:TextBox ID="UtxtDefectName" runat="server" CssClass="form-control" Text='<%#Eval("DefectName")%>' ReadOnly="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                    ControlToValidate="UtxtDefectName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                        <div class="form-group ">
                                                            <label for="username" class="control-label col-lg-4">Error URL *:</label>
                                                            <div class="col-lg-8">
                                                                <asp:TextBox ID="UtxtErrorURL" runat="server" CssClass="form-control" Text='<%#Eval("ErrorURL")%>' ReadOnly="true"></asp:TextBox>

                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                                    ControlToValidate="UtxtErrorURL" Display="Dynamic" ErrorMessage="Enter A Valid URL"
                                                                    ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$"
                                                                    ValidationGroup="save" ForeColor="Red"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="col-lg-12">
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <label for="username" class="control-label col-lg-4">Defect Description *:</label>
                                                            <div class="col-lg-8">
                                                                <asp:TextBox ID="txtDefectDesc" runat="server" CssClass="form-control" Text='<%#Eval("DefectDesc")%>' ReadOnly="true"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="col-lg-12">
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <label for="username" class="control-label col-lg-4">Response *:</label>
                                                            <div class="col-lg-8">
                                                                <asp:TextBox ID="txtResponse" runat="server" CssClass="form-control" Text='<%#Eval("Response")%>' ReadOnly="true" TextMode="MultiLine"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="col-lg-12">
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <label for="email" class="control-label col-lg-4">Request Status :</label>
                                                            <div class="col-lg-8">
                                                                <asp:TextBox ID="UtxtReqStatus" runat="server" CssClass="form-control" Text='<%#Eval("RequestStatus")%>' ReadOnly="true"></asp:TextBox>
                                                                <asp:DropDownList ID="ddlReqStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true" Visible="false">

                                                                    <asp:ListItem Text="Open" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Closed" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                        <div class="form-group ">
                                                            <div class="col-lg-12">
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <div class="form-group " style="text-align: center">
                                                            <div class="col-lg-10">
                                                                <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" style="text-align: center">
                                                            <div class="col-lg-offset-2 col-lg-12">
                                                                <asp:Button ID="BtnSave" runat="server" CssClass="btn btnd btncompt" Text="Update" OnClick="BtnSave_Click" ValidationGroup="save" Visible="false" />
                                                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btnd btncompt" Text="Edit" OnClick="btnUpdate_Click" ValidationGroup="save" />
                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="btnCancel_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-5">
                                                    <div class="row">
                                                        <div class="col-md-12 portlets" style="text-align: center">
                                                            <asp:Image ID="profileImage" runat="server" Style="width: 95%;" Height="300px" />
                                                            <asp:Label ID="lblimagepath" runat="server" Visible="false"></asp:Label>
                                                            <div class="m-b-30">
                                                                <form action="#" class="dropzone" id="dropzone">
                                                                    <div class="fallback">
                                                                        &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" class="btn btnd btncompt" ClientIDMode="Static" Visible="false" /></br>
                                                          <asp:Button ID="btnupload" runat="server" Text="Upload" OnClick="btnupload_Click" class="btn btnd btncompt" Height="30px" ValidationGroup="UP" Visible="false" />
                                                                    </div>
                                                            </div>
                                                        </div>
                                                    </div>
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
                    <%--</div>--%>
                    <!-- container -->
                </div>
                <!-- content -->
                <!-- container -->
            </div>
            <!-- content -->

        </div>
    </div>


</asp:Content>


