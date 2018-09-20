<%@  Page Title="Company Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ClientProfile.aspx.cs" Inherits="MobiOcean.MDM.Web.ClientProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">



        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">   
              <div class="force-overflow">
                <!-- flight section -->
                <div class="bhoechie-tab-content active">
           
                 <div class="profile1">Company Profile</div>

               <br /><br />
            <!-- Start content -->
            <div class="content padding-top-none">
                <!-- Page-Title -->
                <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <div class="dataTables_length" id="datatable-editable_length">
                                    <label>
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                
                <!-- Start content -->
               <div class="module form-module">
							  
							  <div class="formm">
								
								<div class="col-md-6">
                                  
                                     <div class="form-group">
                                        <label>Client Code</label><br>
                                       <asp:TextBox ID="UtxtClientcode" runat="server" Enabled="false" style="width: 70%;" Text='<%#Eval("ClientCode")%>' CssClass="form-control"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="UtxtClientcode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>First Name</label>
                                         <asp:TextBox ID="UtxtFirst" runat="server" style="width: 70%;" Enabled="false" Text='<%#Eval("FirstName")%>' CssClass="form-control" placeholder="Mandatory "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="UtxtFirst"
                                             ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                   
								    <div class="form-group">
                                        <label>Designation</label>
                                         <asp:TextBox ID="UtxtDesg" runat="server" Enabled="false" style="width: 70%;" Text='<%#Eval("Designation")%>' CssClass="form-control" placeholder="Recommended "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="UtxtDesg"
                                             ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                     <div class="form-group">
                                        <label>Country</label>
                                         <asp:DropDownList ID="DDLrole" runat="server" style="width: 70%;" AppendDataBoundItems="True" Enabled="false" placeholder="Country" CssClass="form-control">
                                             <asp:ListItem>India</asp:ListItem>
                                             <asp:ListItem>Afghanistan</asp:ListItem>
                                             <asp:ListItem>Albania</asp:ListItem>
                                             <asp:ListItem>Brazil</asp:ListItem>
                                             <asp:ListItem>Pakistan</asp:ListItem>
                                                </asp:DropDownList>
                                         <br />
                                    </div>
								    <div class="form-group">
                                        <label>Number Of Employees</label>
                                        <asp:TextBox ID="UtxtNOE" runat="server" Enabled="false" style="width: 70%;" Text='<%#Eval("NumberOfEmployees")%>' CssClass="form-control" placeholder="Recommended "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="UtxtNOE"
                                             ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                   
                                    <div class="form-group">
                                        <label>User ID</label><br>
                                        <asp:TextBox ID="UtxtUId" runat="server" Enabled="false" style="width: 70%;" Text='<%#Eval("UserId")%>' CssClass="form-control" placeholder="Recommended "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="UtxtUId"
                                             ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="UtxtManagerName" runat="server" CssClass="form-control" style="width: 70%;" Text='<%#Eval("ManagerName")%>' Enabled="false" Visible="false" placeholder="Recommended "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="UtxtManagerName"
                                             ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                
                                </div>
                                
                                <div class="col-md-6">  
                                    <div class="form-group">
                                        <label>Company Name</label>
                                         <asp:TextBox ID="UtxtClientName" runat="server" Enabled="false" style="width: 70%;" Text='<%#Eval("ClientName")%>' CssClass="form-control" placeholder="mandatory "></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                          ControlToValidate="UtxtClientName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group">
                                        <label>Last Name</label>
                                         <asp:TextBox ID="UtxtLastN" runat="server" style="width: 70%;" Text='<%#Eval("LastName")%>' CssClass="form-control" placeholder="mandatory " Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="UtxtLastN"
                                             ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group">
                                        <label>Email ID</label>
                                        <asp:TextBox ID="UtxtEmailid" runat="server" style="width: 70%;" CssClass="form-control" Text='<%#Eval("EmailId")%>' Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server"
                                        ControlToValidate="UtxtEmailid" ErrorMessage="Required!" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        ControlToValidate="UtxtEmailid" Display="Dynamic" ErrorMessage="abc@abc.abc"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="save" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                    
                                     <div class="form-group">
                                        <label>Mobile Number</label>
                                        <asp:TextBox ID="UtxtManagerContactNo" runat="server" style="width: 70%;" Enabled="false" Text='<%#Eval("ManagerContactNo")%>' CssClass="form-control" placeholder="mandatory "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="UtxtManagerContactNo"  ErrorMessage="Required!" 
                                            ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                   
                                    
                                    <div class="form-group">
                                        <label>Type Of Industry</label>
                                        <asp:TextBox ID="UtxtTOIn" runat="server" Enabled="false" style="width: 70%;" Text='<%#Eval("TypeOfClient")%>' CssClass="form-control" placeholder="recommended "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="UtxtTOIn"
                                             ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>Address</label><br>
                                         <asp:TextBox ID="UtxtAddress" runat="server" style="width: 70%;" Text='<%#Eval("Address")%>'  CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </div>

									</div>
                                   
                                    
								  
                                    
								</div>
								</div>
                  <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                               <asp:Button ID="btnEdit" runat="server" Visible="false" Text="Edit" OnClick="btnEdit_Click" style="width:20%" CssClass="btn btnd btncompt waves-effect"/>
                                <asp:Button ID="btnUpdate" runat="server" Visible="false" Text="Update" OnClick="btnUpdate_Click" style="width:20%" CssClass="btn btnd btncompt waves-effect"/>
                                <asp:Button ID="btncancle" runat="server" Visible="false" Text="Cancel" OnClick="btncancle_Click" style="width:20%" CssClass="btn btnd btncompt waves-effect"/>
                            </div>
                        </div>
                

												</div>
											</div>
											</div>
											
											</div>                                                      
                                                     
      
    <script>
function HideLabel() {
            var seconds = 8;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
};
        </script>
</asp:Content>


