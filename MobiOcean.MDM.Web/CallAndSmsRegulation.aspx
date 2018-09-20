<%@ Page Title="Call/Sms Management" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="CallAndSmsRegulation.aspx.cs" Inherits="MobiOcean.MDM.Web.CallAndSmsRegulation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="ContentHead">

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

                        <div class="profile1">
                            Call/ Sms Management
                                    <div class="clearfix"></div>
                        </div>

                        <br />
                        <div style="text-align: center; color: #FFFFFF" class="col-lg-4 profile2">
                            <h4 style="margin-top: 0px; margin-bottom: 0px;">Profile Name :
                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                        <br />

                        <div class="row" style="text-align: center">
                            <asp:Label ID="lblerrmsg" runat="server" ForeColor="Red"></asp:Label>
                        </div>

                        <div class="table-responsive">
                            <asp:GridView ID="grdDevice" runat="server" DataKeyNames="FeatureId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
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

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsManageNeed" runat="server" Visible="false" Text='<%#Eval("IsManageNeed")%>'></asp:Label>
                                            <asp:LinkButton ID="Manage" runat="server" Text="Manage" CssClass="btn btnd btncompt" CommandName="Manage" />
                                            <asp:TextBox ID="txtfreq" runat="server" placeholder="Frequency" Visible="false" Text='<%#Eval("Duration")%>'></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="fte" runat="server" TargetControlID="txtfreq" FilterType="Numbers" />
                                            <asp:Label ID="lblDays" Text="Days" runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>

                            </asp:GridView>

                        </div>
                        <div>
                            <asp:Button ID="btnsubmit" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnsubmit_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="btncancel_Click" />

                        </div>

                    </div>
                </div>
                <!-- train section -->
            </div>

            
                    <asp:Button ID="btnCheckFeature" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="MPCheckFeature" runat="server" PopupControlID="CheckFeaturePnl"
        TargetControlID="btnCheckFeature" CancelControlID="btnccl"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="CheckFeaturePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" BackColor="#2a368b" aria-hidden="true">

        <div class="modal-body" style="text-align: center;">
            <asp:Button ID="Button5" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label2" runat="server" Text="You don't have license to enable this feature. Please purchase more license." ForeColor="Red"></asp:Label>
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
                        <div class="col-lg-6 col-md-6">
                            <h4 class="modal-title" id="myModalLabel">
                                <asp:Label ID="lblFeatureId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblProfilePopId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblHdrId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblCategoryIdMp" runat="server" Visible="false"></asp:Label>
                                <asp:Label runat="server" Font-Bold="true" ID="lblFeatureN"></asp:Label>
                            </h4>
                        </div>
                        <div class="col-lg-6 col-md-6" style="text-align: right">
                            <asp:Button ID="btnClose1" runat="server" CssClass="close btn btnd btncompt" Text="x" OnClick="btnSave_Click" Style="margin: -5px -18px;" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btnd btncompt" Text="Save" OnClick="btnSave_Click" Style="display: none;" />
                            <asp:Button ID="btnClose" runat="server" CssClass="btn btnd btncompt" Text="Close" OnClick="btnClose_Click" Style="display: none;" />

                        </div>
                    </div>
                    <div class="modal-header">
                        <div class="row">
                            <div class="col-sm-4 hadjust" style="vertical-align: middle">
                                <asp:Label ID="lblAppHead" runat="server" Text="Application Group : " Visible="false"></asp:Label>
                            </div>
                            <div class="col-sm-8 hadjust">
                                <asp:DropDownList ID="ddlAppGroupMain" Visible="false" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" Height="30px"></asp:DropDownList>
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
                                        </asp:DropDownList></label>

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
                                        </asp:DropDownList></label><label>
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





            <asp:Button ID="dummy_btnmanage" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpemanage" runat="server" TargetControlID="dummy_btnmanage"
                PopupControlID="PanelManage" PopupDragHandleControlID="dragi1" CancelControlID="btnC" BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelManage" runat="server" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" class="modal-lg modal-md modal-xs">
                <div class="modal-content">

                    <div class="modal-header" id="dragi1">
                        <h4 class="modal-title" id="myModalLabel1">
                            <asp:Label ID="lblKey" runat="server" Visible="true"></asp:Label>
                            <asp:Button ID="btnC" runat="server" class="close btn btnd btncompt waves-effect waves-light" Text="x" />
                        </h4>
                    </div>
                    <div class="modal-header" style="text-align: center">
                        <asp:Label ID="lblNotMsg" runat="server"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <asp:MultiView ID="Multiview1" runat="server" ActiveViewIndex="-1">

                            <asp:View ID="tabkey" runat="server">
                                <div>
                                    <div class=" form">
                                        <div class="form-group">

                                            <div class="col-lg-6" style="text-align: left">
                                                <asp:Button ID="btnAddKeywordForWeb" runat="server" Text="Add Keyword" class="btn btnd btncompt waves-effect waves-light" OnClick="btnAddKeywordForWeb_Click" />
                                            </div>
                                        </div>
                                        <div class="form-group ">

                                            <div class="col-lg-6" style="text-align: right">
                                                <asp:Button ID="btnApplyChangesForKeyword" runat="server" Text="Apply Changes" class="btn btnd btncompt waves-effect waves-light" OnClick="btnApplyChangesForKeyword_Click" />
                                            </div>
                                        </div>

                                    </div>


                                </div>
                                <br />
                                <br />
                                <asp:Panel ID="PanelKeyword" runat="server" Visible="false">
                                    <div class=" row">
                                        <div class="col-lg-7 col-md-12">
                                            <div class="form-group ">
                                                <label for="lastname" class="control-label col-lg-4">Keyword Code* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtAddKeywordCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="txtAddKeywordCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="firstname" class="control-label col-lg-4">Keyword Name* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtAddKeywordName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                        ControlToValidate="txtAddKeywordName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="firstname" class="control-label col-lg-4">Description* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtAddDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtAddDescription" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-offset-4 col-lg-6">
                                                    <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Add" ValidationGroup="save" OnClick="btnAssign_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-5">
                                        </div>

                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="KeyWordPnl" CssClass="modal-body" Style="height: 250px; overflow: auto" runat="server">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdKey" runat="server" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="false" CssClass="table mGrid" HeaderStyle-CssClass="protable" OnRowDataBound="grdKey_RowDataBound">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKeywordId" runat="server" Text='<%#Eval("KeywordId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKWStatus" runat="server" Text='<%#Eval("Status")%>' Visible="false"></asp:Label>
                                                        <asp:CheckBox ID="chkKeyWordNo" runat="server" Checked="true" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkKeywordHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkKeywordHeader_OnCheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Keyword Code">
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKeywordCode" runat="server" Text='<%#Eval("KeywordCode")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtKeywordCode" runat="server" Text='<%#Eval("KeywordCode")%>' CssClass="form-control input-sm"></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Keyword Name">
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKeywordName" runat="server" Text='<%#Eval("KeywordName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtKeywordName" runat="server" Text='<%#Eval("KeywordName")%>' CssClass="form-control input-sm"></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </asp:Panel>

                            </asp:View>
                            <asp:View ID="tabalert" runat="server">
                                <div>

                                    <div class=" form">
                                        <div class="form-group">
                                            <div class="col-lg-6" style="text-align: left">
                                                <asp:Button ID="btnAlertMgmt" runat="server" Text="Add Phone No" class="btn btnd btncompt waves-effect waves-light" OnClick="btnAlertMgmt_Click" />
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <div class="col-lg-6" style="text-align: right">
                                                <asp:Button ID="btnApplyChangesForAlert" runat="server" Text="Apply Changes" class="btn btnd btncompt waves-effect waves-light" OnClick="btnApplyChangesForAlert_Click" />
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                <br />
                                <br />
                                <asp:Panel ID="PanelAlert" runat="server" Visible="false">

                                    <div class=" row">
                                        <div class="col-lg-12 col-md-12">
                                            <br />
                                            <div class="form-group ">
                                                <div class="col-lg-9">
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtAddMobileNo" runat="server" CssClass="form-control" placeholder="Phone No" MaxLength="15"></asp:TextBox>

                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtAddMobileNo" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                                        <br />
                                                        <asp:Label ID="alertlbl" runat="server"></asp:Label>
                                                        </br> 
                                                    </div>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:Button ID="btnaddalert" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnaddalert_Click" />
                                                </div>
                                            </div>



                                        </div>

                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="alertPnl" CssClass="modal-body" Style="height: 250px; overflow: auto" runat="server">

                                    <div class="table-responsive">
                                        <asp:GridView ID="grdAlert" runat="server" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="false" CssClass="table mGrid" HeaderStyle-CssClass="protable" OnRowDataBound="grdAlert_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblalertId" runat="server" Text='<%#Eval("AlertId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkalertNo" runat="server" Checked="true" />
                                                        <asp:Label ID="alertmobStatus" runat="server" Text='<%#Eval("Status")%>' Visible="false" />
                                                    </ItemTemplate>
                                                      <HeaderTemplate>
                                                        <asp:CheckBox ID="chkalertHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkalertHeader_OnCheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Phone No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("Country") +" "+ Eval("MobileNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="tabcall" runat="server">

                                <div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:RadioButtonList ID="rbtnCall" runat="server" OnSelectedIndexChanged="rbtnCall_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Black List" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="White List" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                    <div class=" form">
                                        <div class="form-group">
                                            <div class="col-lg-6" style="text-align: left">
                                                <asp:Button ID="btnaddCall" runat="server" Text="Add Phone No" class="btn btnd btncompt waves-effect waves-light" OnClick="btnaddCall_Click" />
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <div class="col-lg-6" style="text-align: right">
                                                <asp:Button ID="btnCallManagement" runat="server" Text="Apply Changes" class="btn btnd btncompt waves-effect waves-light" OnClick="btnCallManagement_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <br />
                                <asp:Panel ID="panelCall" runat="server" Visible="false">
                                    <div class=" row">
                                        <div class="col-lg-7 col-md-10">
                                            <div class="form-group ">
                                                <label for="lastname" class="control-label col-lg-4">Name* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtCallName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvcallname" runat="server"
                                                        ControlToValidate="txtCallName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="CallM"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="firstname" class="control-label col-lg-4">Mobile No* : </label>
                                                <div class="col-lg-8">
                                                    <div class="col-md-6">
                                                <asp:DropDownList ID="ddlCallMCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ControlToValidate="ddlCallMCountry" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="CallM"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6">
                                                    <asp:TextBox ID="txtAddNo" runat="server" CssClass="form-control" placeholder="Mobile Number" MaxLength="16"></asp:TextBox>

                                                    <asp:FilteredTextBoxExtender ID="femobilr" runat="server" TargetControlID="txtAddNo" FilterType="Numbers" />
                                                    <asp:RequiredFieldValidator ID="RequiredrfvaddfsdfaFieldValidator5" runat="server"
                                                        ControlToValidate="txtAddNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="CallM"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:Label ID="lblCAllSms" runat="server"></asp:Label>
                                                    </br>                           
                                                </div>
                                                    </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lastname" class="control-label col-lg-4"></label>
                                                <div class="col-lg-8">
                                                    <asp:CheckBox ID="chkIncoming" runat="server" Text="&nbsp;Incoming Call" />
                                                    &nbsp; &nbsp;
                                                     <asp:CheckBox ID="chkOutgoing" runat="server" Text="&nbsp;Outgoing Call" />
                                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                                     <asp:CheckBox ID="chkSms" runat="server" Text="&nbsp;Sms" Visible="false" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-offset-4 col-lg-6">
                                                    <asp:Button ID="btnAddNo" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Add" OnClick="btnAddNo_Click" ValidationGroup="CallM" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="CallPnlTbl" CssClass="modal-body" Style="height: 250px; overflow: auto" runat="server">

                                    <div class="table-responsive">
                                        <asp:GridView ID="grdNo" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table mGrid" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" HeaderStyle-CssClass="protable" OnRowDataBound="grdNo_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Incoming Call">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkCallNo" runat="server" />
                                                        <asp:Label ID="lblIsIncoming" runat="server" Visible="false" Text='<%#Eval("IsIncoming") %>'></asp:Label>
                                                        <asp:Label ID="lblIsOutgoing" runat="server" Visible="false" Text='<%#Eval("IsOutGoing") %>'></asp:Label>
                                                        <asp:Label ID="lblProfileAllowedNoId" runat="server" Visible="false" Text='<%#Eval("ProfileAllowedPhNoId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkIncomingHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkIncomingHeader_CheckedChanged" Text="Incoming Call" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Outgoing Call">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkoutgoingCallNo" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkOutgoingHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutgoingHeader_CheckedChanged" Text="Outgoing Call"/>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCallNameNobj" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNobj" runat="server" Text='<%#Eval("Country")+" "+ Eval("MobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="tabsms" runat="server">
                                <div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:RadioButtonList ID="rbtnsms" runat="server" OnSelectedIndexChanged="rbtnsms_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Black List" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="White List" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                    <div class=" form">
                                        <div class="form-group">
                                            <div class="col-lg-6" style="text-align: left">
                                                <asp:Button ID="btnAddSMS" runat="server" Text="Add Phone No" class="btn btnd btncompt waves-effect waves-light" OnClick="btnAddSMS_Click" />
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <div class="col-lg-6" style="text-align: right">
                                                <asp:Button ID="btnManageSms" runat="server" Text="Apply Changes" class="btn btnd btncompt waves-effect waves-light" OnClick="btnManageSms_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <br />
                                <asp:Panel ID="panelSms" runat="server" Visible="false">
                                    <div class=" row">
                                        <div class="col-lg-7 col-md-10">
                                            <div class="form-group ">
                                                <label for="lastname" class="control-label col-lg-4">Name* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtsmsname" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatotxtsmsname" runat="server"
                                                        ControlToValidate="txtsmsname" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="savemo"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="firstname" class="control-label col-lg-4">Phone No* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="fe" runat="server" TargetControlID="txtMobileNo" FilterType="Numbers" />
                                                    <br />
                                                    <asp:Label ID="SmsLabl" runat="server"></asp:Label>
                                                    </br> 
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-offset-4 col-lg-6">
                                                    <asp:Button ID="btnaddSMS12" runat="server" class="btn btnd btncompt waves-effect waves-light" ValidationGroup="savemo" Text="Add" OnClick="btnaddSMS12_Click" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PnlSMS" CssClass="modal-body" Style="height: 250px; overflow: auto" runat="server">

                                    <div class="table-responsive">
                                        <asp:GridView ID="gridsms" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table mGrid" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" HeaderStyle-CssClass="protable" OnRowDataBound="gridsms_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkCallNosms" runat="server" />
                                                        <asp:Label ID="lblIsSMS" runat="server" Visible="false" Text='<%#Eval("IsSms") %>'></asp:Label>
                                                        <asp:Label ID="lblProfileAllowedNoIdsms" runat="server" Visible="false" Text='<%#Eval("ProfileAllowedPhNoId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSmsHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkSmsHeader_OnCheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCallNameNobjsms" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Phone No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNobjsms" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </div>

                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                    </div>


                    <div class="modal-footer">
                    </div>
                </div>
            </asp:Panel>


            <!-- ============================================================== -->
            <!-- End Right content here -->
            <!-- ============================================================== -->
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
            <asp:Button ID="Button3" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label3" runat="server" Text="Are you sure you want to leave the page?"></asp:Label>
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
    <script type="text/javascript">
        function pageLoad(sender, args) {
            if (!args.get_isPartialLoad()) {
                //  adding handler to the document's keydown event
                $addHandler(document, "keydown", onKeyDown);
            }
        }
        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                $find("<% =mp.ClientID%>").hide();
            document.getElementById('<%= btnSave.ClientID %>').click();
            $find("<% =mpemanage.ClientID%>").hide();
            $find("<% =MP1.ClientID%>").hide();
            $find("<% =mpcancel.ClientID%>").hide();
        }
    }
    </script>
    <script>
        function HideLabel() {
            var seconds = 7;
            setTimeout(function () {
                document.getElementById("<%=lblManFields.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
