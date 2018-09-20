<%@ Page Title="Support Form" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="SupportForm.aspx.cs" Inherits="MobiOcean.MDM.Web.SupportForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">

                <div class="bhoechie-tab-content active">

                    <div class="profile1">&nbsp;&nbsp;Support Ticket</div>

                    <br />
                    <br />
                    <div class="row center-block">
                        <div class="col-md-4">
                            <div class=" supicon2" style="background:#c7f1e3;min-height:234px;">
                            <a href="#" class="">
                                <img src="image/Phone-Ico.png" onmouseover="this.src='image/Phone-Icon-hover.png'" onmouseout="this.src='image/Phone-Ico.png'" /><br>
                                <b>Call Helpdesk</b><br/>
                                <b>+91-995-842-1037</b></a></div>
                        </div>
                        <div class="col-md-4 "> 
                            <div class="supicon2" style="background:#dcd4c7;min-height:234px;">
                            <a href="mailto:<%=MobiOcean.MDM.BAL.Model.Constant.supportEmail %>" >
                                <img src="image/Msg.png" onmouseover="this.src='image/Msg-hover.png'" onmouseout="this.src='image/Msg.png'" /><br />
                                <b>Email Support</b><br/>
                                <b><%=MobiOcean.MDM.BAL.Model.Constant.supportEmail %></b></a></div>
                        </div>
                        <div class="col-md-4 ">
                             <div class="supicon2"  style="background:#d7f1c7;min-height:234px;">
                            <a href="<%=MobiOcean.MDM.BAL.Model.Constant.MobiURL%>faq-new.php" target="_blank">
                                <img src="image/Fax.png" onmouseover="this.src='image/Fax-hover.png'" onmouseout="this.src='image/Fax.png'" /><br>
                                <b>FAQ</b></a></div>
                        </div>
                    </div><br />
                    <div class="row" style="text-align: right">
                        <asp:Button ID="btnViewAll" runat="server" CssClass="btn btnd btncompt" Text="View All" OnClick="btnViewAll_Click" />
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel-group panel-group-joined" id="accordion-test">
                                <div class="panel panel-default margin-top-20">
                                    <div class="panel-body table-rep-plugin">
                                        <div class=" form">
                                            <div class="col-lg-7">
                                                <div class="form-group ">
                                                    <label for="company" class="control-label col-lg-4">Defect Name *:</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtDefectName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="txtDefectName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="firstname" class="control-label col-lg-4">Error Screen/URL *:</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtErrorScreen" runat="server" CssClass="form-control"></asp:TextBox>
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                                        ControlToValidate="txtErrorScreen" Display="Dynamic" ErrorMessage="Enter A Valid URL"
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
                                                    <label for="email" class="control-label col-lg-4">Defect Description* :</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtDefectDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="re5" ForeColor="Red" runat="server" ControlToValidate="txtDefectDesc" ValidationGroup="save" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="col-lg-12">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="phone" class="control-label col-lg-4">Defect Type:</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtDefectType" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <div class="col-lg-12">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="phone" class="control-label col-lg-4">Attach Screen Shot:</label>
                                                    <div class="col-lg-8">
                                                        <div class="col-md-12 portlets" style="text-align: center">
                                                            <asp:Image ID="profileImage" runat="server" Visible="false" />
                                                            <asp:Label ID="lblimagepath" runat="server" Visible="false"></asp:Label>
                                                            <div class="m-b-30">
                                                               <%-- <form action="#" class="dropzone" id="Form2">--%>
                                                                    <div class="fallback">
                                                                        &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" class="btn btnd btncompt" ClientIDMode="Static" /><br />
                                                                        <%-- <asp:Button ID="btnupload" runat="server" Text="Upload" OnClick="btnupload_Click" class="btn btn-success waves-effect waves-light" Height="30px" ValidationGroup="UP"/>--%>
                                                                    </div>
                                                                <%--</form>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group " style="text-align:center">
                                                    <div class="col-lg-12">
                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="text-align:center">
                                                    <div class="col-lg-offset-2 col-lg-12">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt" Text="Save" OnClick="btnAssign_Click" ValidationGroup="save" />
                                                        &nbsp;&nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="btnCancel_Click" />

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
                        </div>
                   
   
</asp:Content>

