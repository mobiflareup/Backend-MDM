<%@ Page Title="Push Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="PushProfile.aspx.cs" Inherits="MobiOcean.MDM.Web.PushProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHead" runat="Server">
 

    <style type="text/css">
        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active div">
                        <li class="profile1">&nbsp;&nbsp;Push Profile</li>
                        <br />
                         <div style="text-align: center; color: #FFFFFF" class="col-lg-4 profile2">
                            <h4 style="margin-top: 0px; margin-bottom: 0px;">Profile Name :
                                <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                        </div>
                        <br />
                        <br />
                        <div class="row">

                            <asp:Button ID="btnchangeUser" runat="server" OnClick="btnchangeUser_Click" Style="display: none" />
                            <asp:Button ID="btnchangeBranch" runat="server" OnClick="btnchangeBranch_Click" Style="display: none" />
                            <asp:Button ID="btnchangeSensor" runat="server" OnClick="btnchangeSensor_Click" Style="display: none" />
                            <asp:Button ID="btnchangeDevice" runat="server" OnClick="btnchangeDevice_Click" style="display:none" />

                        </div>
                        <div class="row">
                            <ul class="nav nav-tabs">
                                <li class="active sdmtm" id="firsttab" runat="server"><a id="ToBranch">To Branch / Department</a></li>
                                <li class="sdmtm" id="Secondtab" runat="server"><a id="ToUser">To User</a></li>
                                <li class="sdmtm" id="Thirdtab" runat="server"><a id="toSensor">To Mobiocean Sensor</a></li>
                               <li class="sdmtm" id="Fourthtab" runat="server"><a id="toDevice" style="display:none">To Device</a></li>
                            </ul>
                        </div>

                        <br />
                        <br />

                        <div class="row" style="text-align: center">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <br>
                        <div class="tab-content">

                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="Tab1" runat="server">
                                    <div id="home" class="tab-pane fade in active">
                                        <div class="row">
 
                                                <div class="col-lg-4">
                                                    <label>Branch Name : </label>

                                                    <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlBranchName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                                </div>
                                          
                                                <div class="col-lg-4">
                                                    <label>Department Name : </label>

                                                    <asp:DropDownList ID="ddlDepartmentName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlDepartmentName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                                </div>
                                            

                                          
                                                <div class="col-lg-4">
                                                    <br />

                                                    <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Add" ValidationGroup="Save" OnClick="btnAssign_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancel_Click" />

                                                </div>
                                            
                                         </div>
                                        <div class="row">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grdbranchdept" runat="server" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                                    EmptyDataText="No Record Found!!!" CssClass="table mGrid" HeaderStyle-CssClass="protable" Width="100%" OnRowDataBound="grdbranchdept_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBDId" runat="server" Text='<%#Bind("ProfileBranchDeptId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chk" AutoPostBack="true" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="ChkBranchHeader" runat="server" AutoPostBack="true" OnCheckedChanged="ChkBranchHeader_OnCheckedChanged" Text="All" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBranchId" runat="server" Text='<%#Bind("BranchId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblBranchName" runat="server" Text='<%#Bind("BranchName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeptId" runat="server" Text='<%#Bind("DeptId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblDeptName" runat="server" Text='<%#Bind("DeptName") %>'></asp:Label>
                                                            </ItemTemplate>                                                     
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Profile Name">
                                                            <ItemTemplate>                                                             
                                                                <asp:Label ID="lblPName" runat="server" Text='<%#Bind("ProfileName") %>'></asp:Label>
                                                            </ItemTemplate>                                                     
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Read" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEnable" runat="server" Text='<%#Eval("isenable")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                  </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Button ID="ApplyChanges" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Apply Changes" OnClick="ApplyChanges_Click"/>
                                                <asp:Button ID="btnBack" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="Tab2" runat="server">
                                    <div id="menu2" class="tab-pane fade in active">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdUser" runat="server" DataKeyNames="UserId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                                AllowPaging="false" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                                EmptyDataText="No record found." OnPageIndexChanging="grdUser_PageIndexChanging" Width="100%" OnRowDataBound="grdUser_RowDataBound">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="AchkRow_Parents" AutoPostBack="true" OnCheckedChanged="AchkRow_Parents_CheckedChanged" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox runat="server" ID="ChkUserHeader" Text="All" OnCheckedChanged="ChkUserHeader_Click" AutoPostBack="true" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmployeeId" runat="server" Text='<%#Eval("EmpCompanyId")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                                            <asp:Label ID="lblIsChanged" runat="server" Text="0" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Branch">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("BranchId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("DeptId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblDept" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Button ID="Assign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Apply" ValidationGroup="save" OnClick="Assign_Click" />
                                                <asp:Button ID="Cancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="Cancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="Tab3" runat="server">
                                    <div id="menu3" class="tab-pane fade in active">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label>Sensor Name :</label>
                                                <asp:DropDownList ID="ddlSensorName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                <asp:CompareValidator ID="comp" runat="server" ControlToValidate="ddlSensorName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Profile Name :</label>
                                                <asp:DropDownList ID="ddlProfileName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlProfileName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                            <div class="col-sm-4">
                                                <br />
                                                <asp:Button ID="btnSaveSensor" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Add" ValidationGroup="save" OnClick="btnSaveSensor_Click" />
                                                <asp:Button ID="btnCancelSensor" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnCancelSensor_Click" />
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="table-responsive">
                                                <asp:GridView ID="grdsensor" runat="server" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                                    EmptyDataText="No Record Found!!!" CssClass="table mGrid" HeaderStyle-CssClass="protable" DataKeyNames="WifiSensorId" Width="100%" OnRowCancelingEdit="grdsensor_RowCancelingEdit" OnRowDataBound="grdsensor_RowDataBound" OnRowDeleting="grdsensor_RowDeleting" OnRowEditing="grdsensor_RowEditing" OnRowUpdating="grdsensor_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%#Bind("WifiSensorId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chksensor" AutoPostBack="true" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkboxSensorHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSensorHeader_OnCheckedChanged" Text="All" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sensor Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSensorId" runat="server" Text='<%#Bind("SensorId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblSensorName" runat="server" Text='<%#Bind("SensorName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Profile Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("ProfileId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblProfileName" runat="server" Text='<%#Bind("ProfileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblEProfileName" runat="server" Text='<%#Bind("ProfileName") %>' Visible="false"></asp:Label>
                                                                <asp:DropDownList ID="ddlEProfileName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlEProfileName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="Update" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Read" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIsRead" runat="server" Text='<%#Eval("isenable")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnApplyChanges" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Apply Changes" OnClick="btnApplyChanges_Click" />
                                                <asp:Button ID="btnBackToProfile" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnBackToProfile_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="Tab4" runat="server">
                                    <div id="menu4" class="tab-pane fade in active">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdDevice" runat="server" AutoGenerateColumns="false" DataKeyNames="DeviceId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable" AllowPaging="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found!!!" Width="100%" OnRowDataBound="grdDevice_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkheader" runat="server" Text="All" OnCheckedChanged="chkheader_Click" AutoPostBack="true"/>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEId" runat="server" Text='<%#Eval("EmpCompanyId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUName" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Device Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeviceEnable" runat="server" Text='<%#Eval("IsEnable") %>' Visible="false"></asp:Label>
                                                              <asp:Label ID="lblDId" runat="server" Text='<%#Eval("DeviceId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblDName" runat="server" Text='<%#Eval("DeviceName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Mobile No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMno" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="row">
                                               <div class="col-sm-12">
                                                <asp:Button ID="btnAssignDevice" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Apply" ValidationGroup="save" OnClick="btnAssignDevice_Click" />
                                                <asp:Button ID="btnCancelDevice" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="Cancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>


                        </div>




                        <br />
                        <!-- train section -->
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="pnlpopup"
        TargetControlID="dummypopupbtn" CancelControlID="InvisibleNo"
        BackgroundCssClass="modalBackgroundTemp">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px">
        <table width="100%" style="border: Solid 2px; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #2a368b; color: #FFFFFF; height: 10px">
                    <asp:Label ID="lblalert" runat="server" Text="Alert" />
                    <asp:Label ID="lblalertid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #e5e5e5; color: #000000">
                    <asp:Label ID="lblUser" runat="server" Text="Are you sure to delete?" />
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td></td>
                <td align="right" style="padding-right: 15px; color: #000000; background-color: #e5e5e5;">
                    <asp:Button ID="Yes" runat="server" CssClass="btn btn-sm btnd btncompt" Text="OK" OnClick="Yes_Click" />
                    <asp:Button ID="No" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" OnClick="No_Click" />
                    <asp:Button ID="InvisibleNo" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" Style="display: none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>


   <script type="text/javascript">
        function pageLoad(sender, args) {
            $(function () {
                $("#ToBranch").bind("click", function () {
                    document.getElementById('<%= btnchangeBranch.ClientID %>').click();

                });
                $("#ToUser").bind("click", function () {
                    document.getElementById('<%= btnchangeUser.ClientID %>').click();

                });
                $("#toSensor").bind("click", function () {
                    document.getElementById('<%= btnchangeSensor.ClientID %>').click();
                });
                $("#toDevice").bind("click", function () {
                    document.getElementById('<%=btnchangeDevice.ClientID%>').click();
                });
            });
        }
    </script>

</asp:Content>
