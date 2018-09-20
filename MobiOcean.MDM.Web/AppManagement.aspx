<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AppManagement.aspx.cs" Inherits="MobiOcean.MDM.Web.AppManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="coh" runat="server" ContentPlaceHolderID="ContentHead">

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
                    <!-- flight section -->
                    <div class="bhoechie-tab-content active">

                        <div class="panel panel-default">
                            <div class="profile1" style="margin: 0px;">
                                &nbsp;&nbsp;App Management<a href="javascript:;" class=" pull-right">
                                    <asp:Button ID="Button1" runat="server" class="btn btn-sky text-uppercase custom-add-profile pull-right" Text="App Management" OnClick="btnAppmgmt_Click" /></a>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <br />
                        <div style="text-align: center; color: #FFFFFF" class="profile2 col-lg-4 ">
                            <h4 style="margin-top: 0px; margin-bottom: 0px;">Profile Name :
                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                        </div>


                        <div class="row" style="text-align: center;">
                            <div class="col-lg-12">
                                <asp:Label ID="lblSave" runat="server"></asp:Label>

                            </div>
                        </div>
                        <br />

                        <asp:Panel ID="PanelAppMgmt" runat="server" Visible="false">
                            <div class="row">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                            <div class="row" style="text-align: right">
                                <a href="#" id="flip" class="btn btnd btncompt waves-effect waves-light"><i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Add Application</span></a>
                            </div>
                            <div id="panel" class="flipkey">
                                <div class=" row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label for="bname" class="control-label col-md-4">Group Name* : </label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                <asp:CompareValidator ID="comp" runat="server" ControlToValidate="ddlGroupName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                        </div>

                                        <div class="form-group ">
                                            <label for="lastname" class="control-label col-md-4">Application Code* : </label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtApplicationCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtApplicationCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label for="firstname" class="control-label col-md-4">Application Name* : </label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                    ControlToValidate="txtApplicationName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-offset-4 col-md-6">
                                                <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnsave_Click" />
                                                <asp:Button ID="Cancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                    </div>

                                </div>

                            </div>
                            <br />
                            <div class="table-responsive">
                                <asp:GridView ID="grdAppGrp" runat="server" DataKeyNames="AppGroupId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                    PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No record found." OnPageIndexChanging="grdAppGrp_PageIndexChanging" Width="100%"
                                    OnRowCancelingEdit="grdAppGrp_RowCancelingEdit" OnRowDeleting="grdAppGrp_RowDeleting"
                                    OnRowEditing="grdAppGrp_RowEditing" OnRowUpdating="grdAppGrp_RowUpdating" RowStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Bind("AppGroupId") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrpCode" runat="server" Text='<%#Bind("AppGroupCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGrpCode" runat="server" Text='<%#Bind("AppGroupCode") %>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrpName" runat="server" Text='<%#Bind("AppGroupName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGrpName" runat="server" Text='<%#Bind("AppGroupName") %>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manage App">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnManageApp" runat="server" CommandName="Manage App" ToolTip="Manage App" CssClass="btn-link" OnClick="lnkbtnManageApp_Click"><i class="fa fa-cogs custom-table-fa" aria-hidden="true"></i></asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" ToolTip="Edit" CssClass="btn-link"></asp:LinkButton>
                                                &nbsp;
                                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete" ToolTip="Delete" CssClass="btn-link"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="btn-link"
                                                    Text="Update" ToolTip="Update" ValidationGroup="Update" />
                                                &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel" CssClass="btn-link"
                                                Text="Cancel" ToolTip="Canecl" />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                                </asp:GridView>

                            </div>
                        </asp:Panel>

                        <br />
                        <br />


                        <div class="table-responsive">
                            <asp:GridView ID="grdDevice" runat="server" DataKeyNames="FeatureId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No record found." Width="100%" OnRowDataBound="grdDevice_RowDataBound" OnRowCommand="grdDevice_RowCommand">


                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("FeatureId")%>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ProfileId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId")%>'></asp:Label>
                                            <asp:Label ID="lblProfileFeatureId" runat="server" Text='<%#Eval("ProfileFeatureMappingId")%>'></asp:Label>
                                             <asp:Label ID="lblDurationReq" runat="server" Text='<%#Eval("DeviceId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Feature Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeatureName" runat="server" Text='<%#Eval("FeatureName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Enabled">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="switchsize" runat="server" CssClass="toggleswitch" data-on-text="Yes" data-off-text="No" data-size="small" Visible="false" Checked="false" />
                                            <asp:ImageButton ID="btnyes" runat="server" ImageAlign="Middle" ImageUrl="~/image/bigYesNew.png" Visible="false" CommandName="Yes" />
                                            <asp:ImageButton ID="btnNo" runat="server" ImageAlign="Middle" ImageUrl="~/image/BigNoNew.png" Visible="false" CommandName="No" />
                                            <asp:Label ID="lblChanged" runat="server" Text='<%#Eval("IsChanged")%>' Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Schedule">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsScheduleNeed" runat="server" Visible="false" Text='<%#Eval("IsScheduleNeed")%>'></asp:Label>
                                            <asp:LinkButton ID="Schedule" runat="server" Text="Schedule" CssClass="btn btnd btncompt" CommandName="Schedule" />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>

                        </div>
                        <div>
                            <asp:Button ID="btnsubmit" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnsubmit_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="btncancel_Click" />

                        </div>

                    </div>

                    <!-- train section -->
                </div>

            </div>

        <asp:Button ID="btnCheckFeature" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="MPCheckFeature" runat="server" PopupControlID="CheckFeaturePnl"
        TargetControlID="btnCheckFeature" CancelControlID="btnccl"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="CheckFeaturePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" BackColor="#2a368b" aria-hidden="true">

        <div class="modal-body" style="text-align: center;">
            <asp:Button ID="Button3" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label3" runat="server" Text="You don't have license to enable this feature. Please purchase more license." ForeColor="Red"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnCheckFeatureCancel" runat="server" Text="OK" OnClick="btnCheckFeatureCancel_Click" />
        </div>


    </asp:Panel>

            <asp:Button ID="dummy_BtnAsgnGp" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="mp" runat="server" PopupControlID="myModal"
                PopupDragHandleControlID="dragi" TargetControlID="dummy_BtnAsgnGp" CancelControlID="btnClose"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="myModal" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" class="modal-lg modal-md modal-xs">
                <div class="modal-content">
                    <div class="modal-header" id="dragi">
                        <div class="col-lg-6 col-md-6 col-sm-6">
                            <h4 class="modal-title" id="myModalLabel">
                                <!--Schedule For:-->
                                <asp:Label ID="lblFeatureId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblProfilePopId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblHdrId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblCategoryIdMp" runat="server" Visible="false"></asp:Label>

                                <asp:Label runat="server" Font-Bold="true" ID="lblFeatureN"></asp:Label>
                            </h4>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6" style="text-align: right">


                            <asp:Button ID="btnSave" runat="server" CssClass="btn btnd btncompt" Text="Save" OnClick="btnSave_Click" Style="display: none;" />
                            <asp:Button ID="btnClose" runat="server" CssClass="btn btnd btncompt" Text="Close" OnClick="btnClose_Click" Style="display: none;" />
                            <asp:Button ID="btnClose1" runat="server" CssClass="close btn btnd btncompt" Text="x" OnClick="btnSave_Click" Style="margin: -5px -18px;" />

                        </div>
                    </div>
                    <div class="modal-header">
                        <div class="row">
                            <div class="col-sm-4 hadjust" style="vertical-align: middle">
                                <asp:Label ID="lblAppHead" runat="server" Text="Application Group : " Visible="false"></asp:Label>
                            </div>
                            <div class="col-sm-8 hadjust">
                                <asp:DropDownList ID="ddlAppGroupMain" Visible="false" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" Height="30px" OnSelectedIndexChanged="ddlAppGroupMain_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-sm-4 ">
                                <label>Day</label>&nbsp;<label>
                                    <asp:CheckBox ID="cball" runat="server" OnCheckedChanged="cball_CheckedChanged" Text="&nbsp;All" AutoPostBack="true" /></label>&nbsp;<label>
                                        <asp:CheckBox ID="cbWeekdays" runat="server" OnCheckedChanged="cbWeekdays_CheckedChanged" Text="&nbsp;Weekdays" AutoPostBack="true" /></label>&nbsp;<label>
                                            <asp:CheckBox ID="cbWeekEnd" runat="server" OnCheckedChanged="cbWeekEnd_CheckedChanged" Text="&nbsp;Weekend" AutoPostBack="true" /></label>&nbsp;
                            </div>
                            <div class="col-sm-8">
                                <asp:CheckBoxList ID="chkDay" runat="server" AutoPostBack="false" onClick="SelectDay();"
                                    RepeatColumns="7" RepeatDirection="Horizontal" Width="100%" ForeColor="#002200" CssClass="form-group">
                                    <asp:ListItem Text="&nbsp; Mon" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp; Tue" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp; Wed" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp; Thu" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp; Fri" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp; Sat" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp; Sun" Value="7"></asp:ListItem>
                                </asp:CheckBoxList>

                            </div>
                            <div class="col-sm-6">
                                <label>From</label>&nbsp;<label>
                                    <asp:DropDownList ID="ddlFromHour" runat="server">
                                        <asp:ListItem>HH</asp:ListItem>
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList></label><label>
                                        <asp:DropDownList ID="ddlFromMin" runat="server">
                                            <asp:ListItem>MM</asp:ListItem>
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                            </div>
                            <div class="col-sm-6">
                                <label>To</label>&nbsp;<label>
                                    <asp:DropDownList ID="ddlToHour" runat="server">
                                        <asp:ListItem>HH</asp:ListItem>
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList></label><label>
                                        <asp:DropDownList ID="ddlToMin" runat="server">
                                            <asp:ListItem>MM</asp:ListItem>
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" MaxLength="4" PlaceHolder="Allowed Duration (In minute)" Width="50%"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="mobileextender" runat="server" FilterType="Numbers" TargetControlID="txtTotal"></asp:FilteredTextBoxExtender>
                            </div>
                            <div class="col-sm-6">
                                &nbsp;&nbsp;
                                <asp:Button ID="btnPhSave" runat="server" CssClass="btn btnd btncompt" OnClick="btnPhSave_Click" Text="Add" UseSubmitBehavior="false" data-dismiss="modal" />
                            </div>
                            <br />
                            <div class="col-sm-12" style="text-align: center">
                                <asp:Label runat="server" ID="lblManFields" ForeColor="Red"></asp:Label>
                                <asp:Label runat="server" ID="Label1" ForeColor="Red"></asp:Label>
                                <asp:Label runat="server" ID="lblMultipleSlotSlctnMSGDayNumber" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblMultipleSlotSlctnMSG" ForeColor="Red"></asp:Label>
                            </div>
                        </div>

                    </div>
                    <div class="modal-body">
                        <div class="row" style="height: 200px; overflow: auto">
                            <div class="table-responsive">


                                <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" GridLines="None" EmptyDataText="No Record Found" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                    Width="100%" ShowHeader="true" OnRowDeleting="gdv_RowDeleting" OnRowEditing="gdv_RowEditing" OnRowDataBound="gdv_RowDataBound" OnRowCancelingEdit="gdv_RowCancelingEdit" OnRowUpdating="gdv_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S.No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTimingId" runat="server" Text='<%#Eval(" ProfileFeatureTimingTempId")%>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Group">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppGroupId" runat="server" Text='<%#Eval("GroupId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblAppGroupName" runat="server" Text='<%#Eval("AppGroupName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDayNumber" runat="server" Text='<%#Eval("FromDay")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInCallAlowFromDay" runat="server" Text='<%#Eval("FromDay").ToString()=="1"?"Monday":Eval("FromDay").ToString()=="2"?"Tuesday":Eval("FromDay").ToString()=="3"?"Wednesday":Eval("FromDay").ToString()=="4"?"Thursday":Eval("FromDay").ToString()=="5"?"Friday":Eval("FromDay").ToString()=="6"?"Saturday":Eval("FromDay").ToString()=="7"?"Sunday":""%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From">
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="lblInCallAlowFromTime" runat="server" Text='<%#Eval("FromTime")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEInCallAlowFromTime" runat="server" Text='<%#Eval("FromTime")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlEFromHour" runat="server">
                                                    <asp:ListItem>HH</asp:ListItem>
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEFromMin" runat="server">
                                                    <asp:ListItem>MM</asp:ListItem>
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>59</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To">
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="lblInCallAlowToTime" runat="server" Text='<%#Eval("ToTime")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEInCallAlowToTime" runat="server" Text='<%#Eval("ToTime")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlEToHour" runat="server">
                                                    <asp:ListItem>HH</asp:ListItem>
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlEToMin" runat="server">
                                                    <asp:ListItem>MM</asp:ListItem>
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>59</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Duration">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInCallTotalDuration" runat="server" Text='<%#Eval("Duration").ToString()==""?"--":Eval("Duration")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEInCallTotalDuration" runat="server" Text='<%#Eval("Duration").ToString()==""?"--":Eval("Duration")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtETotal" runat="server" CssClass="form-control" MaxLength="4" PlaceHolder="Duration" Text='<%#Eval("Duration")%>'></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="mobileextender1" runat="server" FilterType="Numbers" TargetControlID="txtETotal"></asp:FilteredTextBoxExtender>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit"
                                                    ToolTip="Edit"><i class="fa fa-pencil-square-o custom-table-fa"></i></asp:LinkButton>

                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update"
                                                    ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save"></i></asp:LinkButton>
                                                &nbsp;
                                            <asp:LinkButton ID="CancelUpdateButton" runat="server" CommandName="Cancel"
                                                Text="Cancel" ToolTip="Canecl"><i  class="fa fa-close"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn-link"
                                                    ToolTip="Delete"><i class="fa fa-trash-o custom-table-fa"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </asp:Panel>



            <asp:Button ID="dummy_BtnAsgnGp1" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="PanelManageApp"
                PopupDragHandleControlID="dragi1" TargetControlID="dummy_BtnAsgnGp1" CancelControlID="Close"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>

            <asp:Panel runat="server" ID="PanelManageApp" aria-hidden="true" class="modal-lg modal-md modal-xs">

                <div class="modal-content">
                    <div class="modal-header" style="text-align: center" id="dragi1">

                        <asp:Label ID="lblGrpId" runat="server" Visible="false"></asp:Label>
                        Group Name :
                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                        <asp:Button ID="Close" runat="server" Text="x" class="close btn btnd btncompt" />
                    </div>
                    <div class="modal-header">
                        <div class="row">
                            <div class="col-sm-8">
                                <asp:Label ID="lblPopMsg" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4" style="text-align: right">
                            </div>
                        </div>

                    </div>

                    <div class="modal-body" style="height: 250px; overflow: auto;">
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:Button ID="btnaddselected" runat="server" class="btn btnd btncompt" Text="Add Selected App==>>" Width="200px" OnClick="btnaddselected_Click" />
                                &nbsp; 
                                <br />
                                <br />
                                <%--Search option --%>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSelectedSearch" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnSelectedSearch" runat="server" class="btn btnd btncompt" Text="Search" Width="100px" OnClick="btnSelectedSearch_Click" />

                                    </div>

                                </div>
                                <br />
                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="grdaddselected" runat="server" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" CssClass="table mGrid" HeaderStyle-CssClass="protable">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="AchkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="chkAddHeader" AutoPostBack="true" Text="All" OnCheckedChanged="chkHeader_CheckedChanged"/>
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppName" runat="server" Text='<%#Eval("AppName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppGroupName" runat="server" Text='<%#string.IsNullOrEmpty(Eval("GroupName").ToString())?"---":Eval("GroupName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                            <div class="col-sm-6">
                                <asp:Button ID="btnremoveselected" runat="server" class="btn btnd btncompt" Text="<<==Remove Selected App" Width="200px" OnClick="btnremoveselected_Click" />
                                &nbsp;
                                <br />
                                <br />


                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtRemoveSearch" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnremoveSearch" runat="server" class="btn btnd btncompt" Text="Search" Width="100px" OnClick="btnremoveSearch_Click" />

                                    </div>

                                </div>
                                <br />
                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="grdremoveselected" runat="server" GridLines="None" AutoGenerateColumns="false" 
EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" CssClass="table mGrid" 
HeaderStyle-CssClass="protable">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="RachkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="chkRemoveHeader" AutoPostBack="true" OnCheckedChanged="chkRemoveHeader_CheckedChanged" Text="All" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="RlblAppName" runat="server" Text='<%#Eval("AppName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppGroupName" runat="server" Text='<%#string.IsNullOrEmpty(Eval("GroupName").ToString())?"---":Eval("GroupName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
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
                <!-- /.modal-content -->


            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="dummy" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="MP1" runat="server" PopupControlID="MessagePnl"
        TargetControlID="dummy" CancelControlID="btnccl"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>

    <asp:Panel runat="server" ID="MessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

        <div class="modal-body" style="text-align: center; color: green;">
            <asp:Button ID="btnccl" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="message" runat="server" Text="Changes saved successfully!"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="ok" runat="server" Text="OK" OnClick="ok_Click" />
        </div>


    </asp:Panel>
    <asp:Button ID="dummydelete" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="mpcancel" runat="server" PopupControlID="DeleteMessagePnl"
        TargetControlID="dummydelete" CancelControlID="btnccl"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>

    <asp:Panel runat="server" ID="DeleteMessagePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">

        <div class="modal-body" style="text-align: center;">
            <asp:Button ID="Button2" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label2" runat="server" Text="Are you sure you want to leave the page?"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btncancelok" runat="server" Text="OK" OnClick="btncancelok_Click" />
            <asp:Button ID="btncancelcan" runat="server" Text="Cancel" OnClick="btncancelcan_Click" />
        </div>


    </asp:Panel>
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
                    <asp:Label ID="lblalertTimingid" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblalertfeatureid" runat="server" Visible="false"></asp:Label>
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
    <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>

    <script>
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                // if the key pressed is the escape key, then close the dialog
                $find("<% =mp.ClientID%>").hide();
                document.getElementById('<%= btnSave.ClientID %>').click();
                $find("<% =mpe.ClientID%>").hide();
                $find("<% =MP1.ClientID%>").hide();
                $find("<% =mpcancel.ClientID%>").hide();
            }
        }
        function pageLoad(sender, args) {
            if (!args.get_isPartialLoad()) {
                //  adding handler to the document's keydown event
                $addHandler(document, "keydown", onKeyDown);
            }
            $(document).ready(function () {
                $("#flip").click(function () {
                    $("#panel").slideToggle("slow");
                });
                $("#Cancel").click(function () {
                    $("#panel").slideToggle("slow");
                });
            });
        }
    </script>
</asp:Content>
