<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"   Title="User Details"  AutoEventWireup="true" 
    CodeBehind="UserMaster.aspx.cs" Inherits="MobiOcean.MDM.Web.UserMaster" %>

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


        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">

                <div class="profile1">User Details</div>
                <br/>
                <br/>

                <div class="row">
                    <asp:Button ID="btnchangeUser" runat="server" OnClick="btnchange_Click" Style="display: none" />
                    <asp:Button ID="btnchangeBranch" runat="server" OnClick="btnchangeBranch_Click" Style="display: none" />
                    <asp:Button ID="btnchangeDepartment" runat="server" OnClick="btnchangeDepartment_Click" Style="display: none" />
                    <asp:Button ID="btnchangeProfile" runat="server" OnClick="btnchangeProfile_Click" Style="display: none" />
                </div>

                <div class="row"  style="text-align:center">
                    <div class="col-md-3 col-sm-6">
                        <a data-toggle="tab" id="flipu">
                            <asp:Image ID="UserImg" runat="server" ImageUrl="image/user2-hover.png" ToolTip="User"/>
                            <%--<span class="spandash">User</span>--%>
                        </a>
                        </div>
                     <div class="col-md-3 col-sm-6">
                        <a data-toggle="tab" class="col-md-3" id="flip1">
                            <asp:Image ID="BranchImg" runat="server" ImageUrl="image/branch2.png"  ToolTip="Branch"/>
                            <%--<span class="spandash">Branch</span>--%>
                        </a>
                          </div>
                     <div class="col-md-3 col-sm-6">
                        <a data-toggle="tab" class="col-md-3" id="depart">
                            <asp:Image ID="DepartImg" runat="server" ImageUrl="image/department.png"  ToolTip="Department"/>
                            <%--<span class="spandash">Department</span>--%>
                        </a>
                          </div>
                     <div class="col-md-3 col-sm-6">
                        <a data-toggle="tab" class="col-md-3" id="flipp">
                            <asp:Image ID="ProfileImg" runat="server" ImageUrl="image/profile2.png"  ToolTip="Profile"/>
                            <%--<span class="spandash">Profile</span>--%>
                        </a>

                     </div>
                     
                </div>

                <br/>
                <br/>
                <div style="text-align: center">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                </div>
                <br />
                <div class="tab-content">


                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

                        <asp:View ID="Tab1" runat="server">
                            <div id="home" class="tab-pane fade in active">

                                <div class="row">


                                    <div class="col-lg-3">
                                        <label>
                                            By Name :
                                                                         <div class="input-group stylish-input-group">
                                                                             <asp:TextBox ID="txtSrchUserName" runat="server" class="form-control ps"></asp:TextBox>
                                                                             <span class="input-group-addon">
                                                                                 <asp:ImageButton ID="ImageButton2" runat="server" OnClick="btnSrch_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                             </span>
                                                                         </div>
                                        </label>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>
                                            By Employee Id :
                                                                     <div class="input-group stylish-input-group">
                                                                         <asp:TextBox ID="txtSrchUserCode" runat="server" class="form-control ps"></asp:TextBox>
                                                                         <span class="input-group-addon">
                                                                             <asp:ImageButton ID="ImageButton3" runat="server" OnClick="btnSrch1_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                         </span>
                                                                     </div>

                                        </label>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>
                                            By Phone :
                                                                       <div class="input-group stylish-input-group">
                                                                           <asp:TextBox ID="txtSrchMobileNo" runat="server" class="form-control ps"></asp:TextBox>
                                                                           <span class="input-group-addon">
                                                                               <asp:ImageButton ID="ImageButton4" runat="server" OnClick="btnSrch2_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                           </span>
                                                                       </div>
                                        </label>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>
                                            By Email :
                                                                      <div class="input-group stylish-input-group">
                                                                          <asp:TextBox ID="txtSrchEmailId" runat="server" class="form-control ps"></asp:TextBox>
                                                                          <span class="input-group-addon">
                                                                              <asp:ImageButton ID="ImageButton8" runat="server" OnClick="btnSrch3_Click" Height="18px" Width="20px" ImageUrl="~/image/search.png" />
                                                                          </span>
                                                                      </div>

                                        </label>

                                    </div>





                                </div>
                                <br />
                                <br />
                                <div class="table-responsive">
                                </div>

                            </div>
                        </asp:View>


                        <asp:View ID="Tab2" runat="server">
                            <div id="menu2" class="tab-pane fade in active">
                                <div class="col-md-2 branuser"><b>By Branch : </b></div>
                                <div class="selectbranch selectbrancht selectbranchm">
                                    <asp:DropDownList ID="dtBranch" runat="server" CssClass="sel selt selm addselt addselm selectpicker" AutoPostBack="True" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Style="color: black;">
                                    </asp:DropDownList>
                                </div>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <div class="col-md-2 branuser">
     <asp:Label ID="lblbranchCount" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </div>
                                <br/>
                                <br/>
                                <br/>
                                <br/>
                            </div>
                        </asp:View>

                        <asp:View ID="Tab3" runat="server">
                            <div id="menu3" class="tab-pane fade in active">
                                <div class="col-md-2 branuser"><b>By Department : </b></div>
                                <li class="selectbranch selectbrancht selectbranchm">
                                    <asp:DropDownList ID="dtDepartment" runat="server" CssClass="sel selt selm addselt addselm selectpicker" AppendDataBoundItems="true" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" Style="color: black;">
                                    </asp:DropDownList>
                                </li>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <div class="col-md-2 branuser">
     <asp:Label ID="lbldepcount" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </div>

                                <br/>
                                <br/>
                                <br/>
                                <br/>
                            </div>
                        </asp:View>

                        <asp:View ID="Tab4" runat="server">
                            <div id="menu4" class="tab-pane fade in active">
                                <div class="col-md-2 branuser"><b>By Profile : </b></div>
                                <div class="selectbranch selectbrancht selectbranchm">
                                    <asp:DropDownList ID="dtProfile" runat="server" CssClass="sel selt selm addselt addselm selectpicker" AppendDataBoundItems="true" Style="color: black;" OnSelectedIndexChanged="ddlProfile_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <div class="col-md-2 branuser">
     <asp:Label ID="lblProfileCount" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </div>
                                <br/>
                                <br/>
                                <br/>
                                <br/>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                    <br />
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="grdUsr" runat="server" CssClass="table mGrid" HeaderStyle-CssClass="protable" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                            OnRowCancelingEdit="grdUsr_RowCancelingEdit" OnRowDeleting="grdUsr_RowDeleting" OnRowEditing="grdUsr_RowEditing" OnRowDataBound="grdUsr_RowDataBound"
                            OnRowUpdating="grdUsr_RowUpdating" OnPageIndexChanging="grdUsr_PageIndexChanging" AllowPaging="true" DataKeyNames="UserId" PageSize="20" Width="100%">

                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Id">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserCode" runat="server" Text='<%#Eval("UserCode")%>'></asp:Label>
                                    </ItemTemplate>                                 
                                    <ItemStyle HorizontalAlign="Center" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                    </ItemTemplate>                                   
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mobile No">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                    </ItemTemplate>                                   
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email Id">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId")%>'></asp:Label>
                                    </ItemTemplate>                                    
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRole" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
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
                                        <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName").ToString()==""?"---":Eval("ProfileName")%>'></asp:Label>
                                        <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profile Status">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsEnable" runat="server" Text='<%#Eval("IsEnable")%>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lbtnactivate" runat="server" CssClass="LinkBtn" OnClick="lbtnactivate_Click" Text='<%#Eval("IsEnable").ToString()=="1"?"Active":Eval("IsEnable").ToString()=="0"?"Inactive":"---"%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="Dashboard" runat="server" CssClass="btn-link"
                                            Text="DashBoard" ToolTip="User DashBoard" OnClick="Dashboard_Click"  /> 
                                        <asp:Label ID="lblPassword" runat="server" Text='<%#Convert.ToInt32(Eval("IsPasswordVisible").ToString())>= Convert.ToInt32(Session["Role"].ToString()) ?  " / "+ Eval("Password").ToString():""%>'></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />

                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Edit">                                   
                                    <ItemTemplate>                                       
                                        
                                       
                                         <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit" OnClick="EditButton_Click"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton> &nbsp;
                                      

                                    </ItemTemplate>
                                  
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>                                        
                                        <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                            ToolTip="Delete"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>

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

        <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>

     <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
                        <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="pnlpopup"
                            TargetControlID="dummypopupbtn" CancelControlID="InvisibleNo"
                            BackgroundCssClass="modalBackgroundTemp">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px">
                            <table width="100%" style="border: Solid 2px; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr>                                   
                                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold;background-color:#2a368b;color:#FFFFFF;height:10px">
                                        <asp:Label ID="lblalert" runat="server" Text="Alert" />
                                        <asp:Label ID="lblkeyid" runat="server" Visible="false"></asp:Label> 
                                         <asp:Label ID="lblroleid" runat="server" Visible="false"></asp:Label>                                                                       
                                    </td>                                   
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold;background-color:#e5e5e5;color:#000000">
                                        <asp:Label ID="lblUser" runat="server" Text="Are you sure to delete?" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td align="right" style="padding-right: 15px;color:#000000;background-color:#e5e5e5;">
                                        <asp:Button ID="Yes" runat="server" CssClass="btn btn-sm btnd btncompt" Text="OK" OnClick="Yes_Click" />
                                        <asp:Button ID="No" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" OnClick="No_Click"/>
                                        <asp:Button ID="InvisibleNo" runat="server" CssClass="btn btn-sm btn-warning" Text="Cancel" Style="display: none;"/>                                       
                                    </td>
                                </tr>
                            </table>
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
            });
        }
        function HideLabel() {
            var seconds = 7;
            setTimeout(function () {
                document.getElementById("<%=lblmsg.ClientID %>").style.display = "none";
    }, seconds * 1000);
};
    </script>
</asp:Content>


