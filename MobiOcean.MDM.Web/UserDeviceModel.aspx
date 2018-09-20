<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" 
    CodeBehind="UserDeviceModel.aspx.cs" Inherits="MobiOcean.MDM.Web.UserDeviceModel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->

            <div class="profile1">Asset Tracking</div>
            <br />
            <br />

            <div class="row">
                <asp:Button ID="btnchangeUser" runat="server" OnClick="btnchange_Click" Style="display: none" />
                <asp:Button ID="btnchangeBranch" runat="server" OnClick="btnchangeBranch_Click" Style="display: none" />
                <asp:Button ID="btnchangeDepartment" runat="server" OnClick="btnchangeDepartment_Click" Style="display: none" />
                <asp:Button ID="btnchangeProfile" runat="server" OnClick="btnchangeProfile_Click" Style="display: none" />
                <asp:Button ID="btnDeviceOwnership" runat="server" OnClick="btnDeviceOwnership_Click" Style="display: none" />
                <asp:Button ID="btnDeviceInfo" runat="server" OnClick="btnDeviceInfo_Click" Style="display: none" />
            </div>

            <div class="row" style="text-align: center">
                <div class="col-md-2 col-sm-4">
                    <a data-toggle="tab" id="flipu">
                        <asp:Image ID="UserImg" runat="server" ImageUrl="image/user2-hover.png" ToolTip="User" />
                        <%--<span class="spandash" style="text-align:center">User</span>--%>
                    </a>
                </div>
                <div class="col-md-2 col-sm-4">
                    <a data-toggle="tab" id="flip1">
                        <asp:Image ID="BranchImg" runat="server" ImageUrl="image/branch2.png" ToolTip="Branch" />
                        <%--<span class="spandash" style="text-align:center">Branch</span>--%>
                    </a>
                </div>
                <div class="col-md-2 col-sm-4">
                    <a data-toggle="tab" id="depart">
                        <asp:Image ID="DepartImg" runat="server" ImageUrl="image/department.png" ToolTip="Department" />
                        <%--<span class="spandash" style="text-align:center">Department</span>--%>
                    </a>
                </div>
                <div class="col-md-2 col-sm-4">
                    <a data-toggle="tab" id="flipp">
                        <asp:Image ID="ProfileImg" runat="server" ImageUrl="image/profile2.png" ToolTip="Profile" />
                        <%--<span class="spandash" style="text-align:center">Profile</span>--%>
                    </a>
                </div>
                <div class="col-md-2 col-sm-4">
                    <a data-toggle="tab" id="flipDO">
                        <asp:Image ID="DevOwnerImg" runat="server" ImageUrl="image/Device-Ownership.png" ToolTip="Device Ownership" />
                        <%--<span class="spandash" style="text-align:center">Device Ownership</span>--%>
                    </a>
                </div>
                <div class="col-md-2 col-sm-4">
                    <a data-toggle="tab" id="flipInfo">
                        <asp:Image ID="DeviceInfImg" runat="server" ImageUrl="image/Device-info.png" ToolTip="Device Information" />
                        <%--<span class="spandash" style="text-align:center">Device Information</span>--%>
                    </a>
                </div>

            </div>

            <br />
            <br />
            <div style="text-align: center">
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
            </div>
            <br />
            <div class="tab-content">


                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

                    <asp:View ID="Tab1" runat="server">
                        <div id="home" class="tab-pane fade in active">

                            <div class=" form">
                                <div class="col-lg-3">
                                    <label>
                                        By User Name :
                                                                         <div class="input-group stylish-input-group">
                                                                             <asp:TextBox ID="txtSrchUserName" runat="server" class="form-control ps"></asp:TextBox>
                                                                             <span class="input-group-addon">
                                                                                 <asp:ImageButton ID="btnSrch" runat="server" OnClick="btnSrch_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                             </span>
                                                                         </div>
                                    </label>
                                </div>
                                <div class="col-lg-3">
                                    <label>
                                        By Device Name :
                                                                     <div class="input-group stylish-input-group">
                                                                         <asp:TextBox ID="txtSrchDeviceNAme" runat="server" class="form-control ps"></asp:TextBox>
                                                                         <span class="input-group-addon">
                                                                             <asp:ImageButton ID="btnSrch1" runat="server" OnClick="btnSrch1_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                         </span>
                                                                     </div>

                                    </label>
                                </div>
                                <div class="col-lg-3">
                                    <label>
                                        By Mobile Number :
                                                                       <div class="input-group stylish-input-group">
                                                                           <asp:TextBox ID="txtSrchMobileNo" runat="server" class="form-control ps"></asp:TextBox>
                                                                           <span class="input-group-addon">
                                                                               <asp:ImageButton ID="btnSrch2" runat="server" OnClick="btnSrch2_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                           </span>
                                                                       </div>
                                    </label>
                                </div>
                                <div class="col-lg-3">
                                    <label>
                                        By App Status :
                                                <div class="input-group stylish-input-group">

                                                    <asp:DropDownList ID="ddlappstus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="searchdropdown_SelectedIndexChanged" class="form-control ps" Style="color: black;">
                                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Unregistered" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="Registered" Value="2"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                    </label>

                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />

                        </div>
                    </asp:View>


                    <asp:View ID="Tab2" runat="server">
                        <div id="menu2" class="tab-pane fade in active">
                            <div class="col-md-2 branuser"><b>By Branch : </b></div>
                            <div class="push-profile">
                                <asp:DropDownList ID="dtBranch" runat="server" class="push-sel selectpicker" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AppendDataBoundItems="true" Style="color: black;">
                                </asp:DropDownList>
                            </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <div class="col-md-2 branuser">
     <asp:Label ID="lblbranchCount" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
 </div>
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </asp:View>

                    <asp:View ID="Tab3" runat="server">
                        <div id="menu3" class="tab-pane fade in active">
                            <a runat="server" id="ascaleUpgarde">
                                <div class="col-md-2 "><b>By Department : </b></div>
                                <li class="push-profile">
                                    <asp:DropDownList ID="dtDepartment" runat="server" class="push-sel selectpicker" AppendDataBoundItems="true" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" Style="color: black;">
                                    </asp:DropDownList>
                                </li>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <div class="col-md-2 branuser">
     <asp:Label ID="lbldepcount" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
 </div>
                            </a>
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </asp:View>

                    <asp:View ID="Tab4" runat="server">
                        <div id="menu4" class="tab-pane fade in active">
                            <div class="col-md-2 branuser"><b>By Profile : </b></div>
                            <div class="push-profile">
                                <asp:DropDownList ID="dtProfile" runat="server" class="push-sel selectpicker" AppendDataBoundItems="true" Style="color: black;" OnSelectedIndexChanged="ddlProfile_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <div class="col-md-2 branuser">
     <asp:Label ID="lblProfileCount" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
 </div>
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </asp:View>
                    <asp:View ID="Tab5" runat="server">
                        <div id="menu5" class="tab-pane fade in active">
                            <div class="col-md-2 branuser"><b>By Device Ownership </b></div>
                            <div class="selectbranch selectbrancht selectbranchm">
                                <asp:DropDownList ID="drpOwner" runat="server" CssClass="sel selt selm addselt addselm selectpicker" AppendDataBoundItems="true" Style="color: black;" OnSelectedIndexChanged="drpOwner_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <div class="col-md-2 branuser">
     <asp:Label ID="lblDevOWnerShip" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
 </div>
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </asp:View>

                    <asp:View ID="Tab6" runat="server">
                        <div id="menu6" class="tab-pane fade in active">
                            <div class="col-md-2 branuser"><b>By OS Version : </b></div>
                            <div class="selectbranch selectbrancht selectbranchm">
                                <asp:DropDownList ID="drpDeviInfo" runat="server" CssClass="sel selt selm addselt addselm selectpicker" AppendDataBoundItems="true" Style="color: black;" OnSelectedIndexChanged="drpDeviInfo_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <div class="col-md-2 branuser">
     <asp:Label ID="lblDevInf" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
 </div>
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </asp:View>
                </asp:MultiView>
                <br />
                <br />
                <div class="table-responsive col-lg-12">
                    <asp:GridView ID="GridUser" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="False"
                        AllowPaging="true" OnRowDataBound="GridUser_OnDataBound"
                        ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found." OnPageIndexChanging="GridUser_PageIndexChanging" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText=" User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceId" runat="server" Text='<%#Eval("DeviceId")%>' Visible="false" />
                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId")%>' Visible="false" />
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName").ToString()%>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Device Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName").ToString()==""?"---":Eval("DeviceName").ToString()%>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo1").ToString()==""?"---":Eval("MobileNo1").ToString()%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Device Model">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceModel" runat="server" Text='<%#Eval("DeviceModel").ToString()==""?"---":Eval("DeviceModel")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OS Version">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOSVersion" runat="server" Text='<%#Eval("OSVersion").ToString()==""?"---":Eval("OSVersion")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BranchName").ToString()==""?"---":Eval("BranchName").ToString()%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department Name">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartName" runat="server" Text='<%#Eval("DeptName").ToString()==""?"---":Eval("DeptName").ToString()%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Profile Name">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Profile Status">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable").ToString()=="1"?"Enabled":"Disabled"%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MDM APP status">
                                <ItemTemplate>
                                    <asp:Label ID="lblAppStatus" runat="server" Text='<%#Eval("IsAppInstalled")%>' Visible="false" />
                                    <asp:Label ID="lblAPPInstallationstatus" runat="server" Text='<%#Eval("IsAppInstalled").ToString()=="0"?"Registered":"Unregistered" %>' Visible="false" ForeColor="Green" />
                                    <asp:LinkButton ID="btnpush" runat="server" CssClass="LinkBtn" OnClick="btnpush_Click" Text="Send Link To Register" Visible="false" ForeColor="Red"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnAppStatus" runat="server" CssClass="LinkBtn" Text="View Device Info" OnClick="lkbtnAppStatus_Click" ToolTip="View All Apps Status">
                                        <i class="fa fa-mobile fa-2x" ></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnview" runat="server" CssClass="LinkBtn" Text="View Device Info" OnClick="btnview_Click" ToolTip="View Device Info">
                                        <i class="fa fa-eye fa-2x" ></i>
                                    </asp:LinkButton>
                                     <asp:LinkButton ID="lbtnLogout" runat="server" CssClass="LinkBtn" Text="LogOut" OnClick="lbtnLogout_Click" ToolTip="SignOut">
                                        <i class="fa fa-sign-out fa-2x" ></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                    </asp:GridView>



                </div>
                <div class="row" style="text-align: right">
                    <asp:Panel runat="server" ID="MessagePnl" Height="160px" CssClass="msgpopup" Visible="false">

                        <div class="modal-body" style="text-align: center; color: green;">
                            <asp:Button ID="btnccl" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                            <asp:RadioButton ID="RbtnYou" GroupName="Group1" Text="Send To Yourself" Value="Yes" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                    <asp:RadioButton ID="RbtnOther" GroupName="Group1" Text="Send To Other" Value="No" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                            <br />
                            <asp:Label ID="lblmessage" runat="server" Text="Mail To :" Style="margin: 0px auto" ForeColor="Black"></asp:Label>
                            <asp:TextBox ID="txtMailTo" runat="server" ForeColor="Black"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server"
                                ControlToValidate="txtMailTo" ErrorMessage="Required!" ValidationGroup="mailsend"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ControlToValidate="txtMailTo" Display="Dynamic" ErrorMessage="Enter Valid Email-Id"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="mailsend" ForeColor="Red"></asp:RegularExpressionValidator>
                            <br />
                            <asp:Label ID="lblerrorMailTo" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer" style="text-align: center;">
                            <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btnd btncompt" OnClick="Send_Click" ValidationGroup="mailsend" />&nbsp;
                            <asp:Button ID="CancelMail" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="CancelMail_Click" />
                        </div>


                    </asp:Panel>
                </div>
                <div class="row" style="text-align: right">
                    <asp:Button ID="btnsavetopdf" runat="server" CssClass="btn btnd btncompt" Text="Save To PDF" align="right" OnClick="btnsavetopdf_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btnd btncompt" Text="Print" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnSendtomail" runat="server" CssClass="btn btnd btncompt" Text="Send To Mail" OnClick="btnSendtomail_Click" />
                </div>
            </div>
        </div>

    </div>

    <asp:Button ID="dummy_BtnAsgnGp" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mp" runat="server" PopupControlID="myModal"
        PopupDragHandleControlID="dragi" TargetControlID="dummy_BtnAsgnGp" CancelControlID="btnclose"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>


    <asp:Panel runat="server" ID="myModal" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" class="modal-lg modal-md modal-xs">

        <div class="modal-content">
            <div class="modal-header" id="dragi">
                <div class="col-sm-6" style="text-align: left">
                    <asp:Label ID="lblPdeviceId" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblPuserId" runat="server" Visible="false"></asp:Label>
                    <h4>User Name : <b><asp:Label ID="lblPUserName" runat="server"></asp:Label></b>
                        Device Name :  <b><asp:Label ID="lblPDeviceName" runat="server"></asp:Label></b>
                    </h4>
                </div>
                <div class="col-sm-6" style="text-align: right">
                    <asp:Button ID="btnclose" runat="server" Text="x" class="close btn btnd btncompt" Style="margin-top: 3px; margin-right: -15px;" />
                </div>
            </div>
<%--            <div class="modal-header">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Label ID="lblPopMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>--%>

            <div class="modal-body">
                <div class="row" style="height: 250px; overflow: auto">
                    <div class="col-sm-12">
                        <br />
                        <div class="table-responsive" data-pattern="priority-columns">
                            <asp:GridView ID="grdappInstalledStatus" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable" RowStyle-HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAId" runat="server" Text='<%#Bind("AppTypeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="App Name">
                                        <ItemTemplate>
                                            <asp:Label ID="AlblAppName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Installed App Version">
                                        <ItemTemplate>
                                            <asp:Label ID="AlblAppVersion" runat="server" Text='<%#string.IsNullOrWhiteSpace(Eval("AppVersion").ToString())?"---":Eval("AppVersion").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Installed Status">
                                        <ItemTemplate>
                                            <asp:Label ID="AlblAppVersion" runat="server" Text='<%#string.IsNullOrWhiteSpace(Eval("InstallStatus").ToString())?"No":"Yes"%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>

            </div>
            <div class="modal-footer">
            </div>
        </div>

    </asp:Panel>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(function () {
                $("#flipu").bind("click", function () {
                    document.getElementById('<%= btnchangeUser.ClientID %>').click();
                });
                $("#flip1").bind("click", function () {
                    document.getElementById('<%= btnchangeBranch.ClientID %>').click();
                });
                $("#depart").bind("click", function () {
                    document.getElementById('<%= btnchangeDepartment.ClientID %>').click();
                });
                $("#flipp").bind("click", function () {
                    document.getElementById('<%= btnchangeProfile.ClientID %>').click();
                });
                $("#flipDO").bind("click", function () {
                    document.getElementById('<%= btnDeviceOwnership.ClientID %>').click();
                });
                $("#flipInfo").bind("click", function () {
                    document.getElementById('<%= btnDeviceInfo.ClientID %>').click();
                });
            });
        }
    </script>

</asp:Content>
