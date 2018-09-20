<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="GeoFenceMgmt.aspx.cs" Inherits="MobiOcean.MDM.Web.GeoFenceMgmt" %>

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


    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
        <div class="force-overflow">
            <!-- flight section -->
            <div class="bhoechie-tab-content active">
                <div class="profile1">
                    Geo Fence Management
                     <a href="javascript:;" class=" pull-right">
                         <asp:Button ID="btn" runat="server" class="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Add GeoFence" OnClick="btn_Click" />
                     </a>
                </div>
                <br />
                <div style="text-align: center; color: #FFFFFF" class="profile2 col-lg-4 ">
                    <h4 style="margin-top: 0px; margin-bottom: 0px;">Profile Name :
                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                </div>
                <br />
                <br />



                <div id="Add" class="row" visible="false" runat="server">
                    <div class="col-md-12">
                        <asp:RadioButtonList ID="rbtnaddGeoFence" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnaddGeoFence_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="&nbsp; By Route&nbsp;&nbsp;"></asp:ListItem>
                            <asp:ListItem Text="&nbsp; By Area"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-12">
                        <div id="panel" class="flipkey" runat="server" visible="false">
                            <asp:HiddenField ID="hidden" runat="server" />
                            <div class=" form">
                                <div class="col-lg-7">

                                    <div class="form-group ">
                                        <label for="firstname" class="control-label col-lg-4">Route Code* :</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" PlaceHolder="Route Code"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Rsave"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label for="company" class="control-label col-lg-4">Route Name* : </label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" PlaceHolder="Route Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtMobileNo" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Rsave"></asp:RequiredFieldValidator>
                                            <%--<div id="info" style="position: absolute; font-family: Arial; font-size: 14px;">
                                                                      </div>--%>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label for="firstname" class="control-label col-lg-4">Description :</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" PlaceHolder="Description"></asp:TextBox>
                                            <br />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="firstname" class="control-label col-lg-4">Start Location* :</label>
                                        <div class="col-lg-8">
                                            <input type="text" id="txtPlaces" name="startlocation" class="form-control StartLocation" placeholder="Start Location" onkeydown="if (event.keyCode == 13) {return false;}" />
                                            <!-- onfocus="getStartplace();" -->
                                            <br />

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="firstname" class="control-label col-lg-4">Destination* :</label>
                                        <div class="col-lg-8">
                                            <input type="text" id="txtPlaces1" name="deslocation" class="form-control EndLocation" placeholder="Destination" onkeydown="if (event.keyCode == 13) {return false;}" />
                                            <!-- onfocus="getEndplace();" -->
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label for="firstname" class="control-label col-lg-4"></label>
                                        <div class="col-lg-8">
                                            <br />
                                            <asp:Button ID="Draw" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" ValidationGroup="Rsave" Text="Draw Route" OnClick="Draw_Click" />
                                            <asp:Button ID="btnC" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btnC_Click" />
                                        </div>
                                    </div>

                                    <div class="form-group ">

                                        <div class="col-lg-7">
                                            <br />
                                            <%--<a id="Drawpolygon" >Draw</a>--%>
                                        </div>
                                    </div>


                                </div>
                                <br />
                                <div class="form-group" style="text-align: center">

                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMessage" runat="server" align="center"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group ">

                                        <div class="col-lg-7">
                                            <br />
                                        </div>
                                    </div>



                                    <div class="form-group">
                                        <div class="col-lg-offset-2 col-lg-7" style="text-align: center">
                                            <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" OnClick="btnAdd_Click" ValidationGroup="save" Visible="false" />

                                        </div>
                                    </div>

                                    <div id="info" style="visibility: hidden"></div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
                <br />

                <div id="paneladdarea" visible="false" runat="server" class="row">
                    <div class=" form">
                        <div class="col-lg-7 col-md-12">
                            <div class="form-group ">
                                <label for="bname" class="control-label col-lg-4">Area Name* : </label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" PlaceHolder="Area Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtArea" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="lastname" class="control-label col-lg-4">Location* : </label>
                                <div class="col-lg-8">
                                    <input type="text" id="txtAreaLocation" name="AreaLocation" class="form-control AreaLocation" placeholder="Enter a location" onkeydown="if (event.keyCode == 13) {return false;}" />
                                    <!-- onfocus="getplace();" -->
                                    <br />
                                </div>
                            </div>
                            <div class="form-group ">
                                <label for="lastname" class="control-label col-lg-4">Radius (In Meter)* : </label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtRadius" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="txtRadius" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Regex2" runat="server" ValidationExpression="((\d+)((\.\d{1,15})?))$"
                                        ErrorMessage="Wrong!" ControlToValidate="txtRadius" ValidationGroup="save" ForeColor="Red" />
                                </div>
                            </div>
                            <div class="form-group" style="text-align: right">
                                <div class="col-lg-offset-4 col-lg-6">
                                    <asp:Button ID="btnAssignArea" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Draw Area" ValidationGroup="save" OnClick="DrawArea_Click" />
                                    <asp:Button ID="btncncl" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" OnClick="btncncl_Click" />
                                </div>
                            </div>
                            <div class="form-group">

                                <div class="col-lg-offset-4 col-lg-6">
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12" style="text-align: center">
                            <asp:Label ID="lblMessage2" runat="server"></asp:Label>
                        </div>

                        <div class="row">
                            <div class="form-group ">

                                <div class="col-lg-7">
                                    <br />
                                </div>
                            </div>
                            <div class="form-group" style="text-align: right">
                                <div class="col-lg-offset-2 col-lg-7" style="text-align: center">
                                    <asp:Button ID="btnSaveArea" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" OnClick="btnsavearea_Click" ValidationGroup="save" Visible="false" />

                                </div>
                            </div>
                            <div id="info1" style="visibility: hidden"></div>
                        </div>



                    </div>
                </div>
                <div class="col-lg-12" style="text-align: center">
                    <asp:Label ID="lblFinalmsg" runat="server"></asp:Label>
                </div>
                <br />
                <div class="row" id="map" runat="server" visible="false">
                    <div class="col-lg-12">

                        <%--<div id="map_canvas"></div>--%>
                        <div id="maps" style="height: 500px; z-index: 1;"></div>
                    </div>
                </div>
                <br />

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

                            <asp:TemplateField HeaderText="Manage">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Frequency" runat="server" Text="Manage" CssClass="btn btnd btncompt" CommandName="Frequency" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                </div>
                <div>
                    <asp:Button ID="btnsubmit" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnsubmit_Click" />
                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="btncancel_Click" />

                </div>
                <asp:HiddenField ID="hdnStartAddress" runat="server" />
                <asp:HiddenField ID="hdnStartLat" runat="server" />
                <asp:HiddenField ID="hdnStartLong" runat="server" />
                <asp:HiddenField ID="hdnRadius" runat="server" />
                <asp:HiddenField ID="RouteStartAddr" runat="server" />
                <asp:HiddenField ID="RouteStartLat" runat="server" />
                <asp:HiddenField ID="RouteStartLong" runat="server" />
                <asp:HiddenField ID="RouteEndAddr" runat="server" />
                <asp:HiddenField ID="RouteEndLat" runat="server" />
                <asp:HiddenField ID="RouteEndLong" runat="server" />

                <asp:HiddenField ID="SetLocationName" runat="server" />
                <asp:HiddenField ID="SetLat" runat="server" />
                <asp:HiddenField ID="SetLongi" runat="server" />
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
            <asp:Button ID="Button2" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label2" runat="server" Text="You don't have license to enable this feature. Please purchase more license." ForeColor="Red"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnCheckFeatureCancel" runat="server" Text="OK" OnClick="btnCheckFeatureCancel_Click" />
        </div>


    </asp:Panel>

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
            <asp:Button ID="Button1" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label3" runat="server" Text="Are you sure you want to leave the page?"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btncancelok" runat="server" Text="OK" OnClick="btncancelok_Click" />
            <asp:Button ID="btncancelcan" runat="server" Text="Cancel" OnClick="btncancelcan_Click" />
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
                                    <ItemStyle HorizontalAlign="Center" />
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

    <asp:Button ID="dummybtnfreq" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpfreq" runat="server" TargetControlID="dummybtnfreq" PopupControlID="Panel1" PopupDragHandleControlID="dragi1"
        CancelControlID="Close" BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="Panel1" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" class="modal-lg modal-md modal-xs">
        <div class="modal-content">
            <div class="modal-header" id="dragi1">
                <div class="col-lg-6 col-md-6">
                    <h4 class="modal-title" id="myModalLabel1">
                        <asp:Label ID="lblFeatureId1" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblProfilePopId1" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblHdrId1" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblCategoryIdMp1" runat="server" Visible="false"></asp:Label>

                        <asp:Label runat="server" Font-Bold="true" ID="lblFeatureN1"></asp:Label>
                    </h4>
                </div>
                <div class="col-lg-6 col-md-6" style="text-align: right">
                    <asp:Button ID="Save" runat="server" CssClass="btn btnd btncompt" Text="Save" OnClick="Save_Click" Style="display: none;" />
                    <asp:Button ID="Close" runat="server" CssClass="btn btnd btncompt" Text="Close" Style="display: none;" />
                    <asp:Button ID="Button3" runat="server" CssClass="close btn btnd btncompt" Text="x" OnClick="Save_Click" />

                </div>
            </div>
            <div class="modal-header">
                <div class="row">
                    <div class="col-sm-6 fsize">
                        <label>From</label>
                        <asp:DropDownList ID="ddlFromHrFreq" runat="server">
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
                        <asp:DropDownList ID="ddlFromMinFreq" runat="server">
                            <asp:ListItem>MM</asp:ListItem>
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-6 fsize">
                        <label>To</label>
                        <asp:DropDownList ID="ddlToHourFreq" runat="server">
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
                        <asp:DropDownList ID="ddlToMinFreq" runat="server">
                            <asp:ListItem>MM</asp:ListItem>
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-6 fsize">
                        <asp:TextBox ID="txtFreq" runat="server" CssClass="form-control" MaxLength="4" PlaceHolder="Time Interval (In Minutes)"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtTotal"></asp:FilteredTextBoxExtender>
                    </div>
                    <div class="col-sm-6 fsize" style="text-align: left">
                        <asp:Button ID="btnAddFrequency" runat="server" CssClass="btn btnd btncompt" OnClick="btnAddFrequency_Click" Text="Add" UseSubmitBehavior="false" data-dismiss="modal" />
                    </div>
                    <div class="col-sm-12" style="text-align: center">
                        <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>


            <div class="modal-body">
                <div class="row" style="height: 200px; overflow: auto">
                    <div class="table-responsive" data-pattern="priority-columns">


                        <asp:GridView ID="grdLocReq" runat="server" class="table table-small-font table-bordered table-striped mGrid" AutoGenerateColumns="false" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="false" OnRowCancelingEdit="grdLocReq_RowCancelingEdit" OnRowDataBound="grdLocReq_RowDataBound" OnRowDeleting="grdLocReq_RowDeleting" OnRowEditing="grdLocReq_RowEditing" OnRowUpdating="grdLocReq_RowUpdating" CssClass="table mGrid" HeaderStyle-CssClass="protable">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("LocReqFreqId")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Start Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStrtTime" runat="server" Text='<%#Eval("StrtTime")%>'></asp:Label>

                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEStrtTimeFreq" runat="server" Text='<%#Eval("StrtTime")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlEFromHourFreq" runat="server">
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
                                        <asp:DropDownList ID="ddlEFromMinFreq" runat="server">
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
                                <asp:TemplateField HeaderText="End Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("EndTime")%>'></asp:Label>

                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEEndTimeFreq" runat="server" Text='<%#Eval("EndTime")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlEToHourFreq" runat="server">
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
                                        <asp:DropDownList ID="ddlEToMinFreq" runat="server">
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
                                <asp:TemplateField HeaderText="Time Interval (In Minutes)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocReqFrequency" runat="server" Text='<%#Eval("LocReqFrequency")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtELocReqFrequency" runat="server" Text='<%#Eval("LocReqFrequency")%>'
                                            MaxLength="4" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_2" runat="server" ControlToValidate="txtELocReqFrequency"
                                            Display="None" ErrorMessage="Please enter Frequency" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator_2"
                                            runat="server">
                                        </asp:ValidatorCalloutExtender>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                            TargetControlID="txtELocReqFrequency"></asp:FilteredTextBoxExtender>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Frequency">
                                    <ItemTemplate>
                                        <asp:Label ID="lblttlNoOfReq" runat="server" Text='<%#Eval("ttlNoOfReq")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEttlNoOfReq" runat="server" Text='Auto calculated' ForeColor="Maroon"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit"
                                            ToolTip="Edit"><i><img src="image/edit.png"></i></asp:LinkButton>

                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Update"
                                            ToolTip="Update" ValidationGroup="Update"><i  class="fa fa-save"></i></asp:LinkButton>
                                        &nbsp;
                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Cancel"
                                                Text="Cancel" ToolTip="Canecl"><i  class="fa fa-close"></i></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lbDelete" runat="server" CssClass="btn-link" Text="Delete"></asp:LinkButton>--%>
                                        <%--<asp:ImageButton ID="lbldelete" runat="server" CssClass="bg-img"/>--%>
                                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Delete" CssClass="btn-link"
                                            ToolTip="Delete"><i><img src="image/Delete.png" class="iconview"></i></asp:LinkButton>
                                        <%--OnClientClick="return confirm('The Profile will be deleted. Are you sure want to continue?');"--%>
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

    <asp:Button ID="dummybtngeo" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpgeo" runat="server" TargetControlID="dummybtngeo" PopupControlID="Panel2" PopupDragHandleControlID="dragi2"
        CancelControlID="Closegeo" BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="Panel2" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true" class="modal-lg modal-md modal-xs">
        <div class="modal-content">
            <div class="modal-header" id="dragi2">
                <h4 class="modal-title" id="myModalLabel2">
                    <asp:Label ID="lblFeatureId2" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblProfilePopId2" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblHdrId2" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblCategoryIdMp2" runat="server" Visible="false"></asp:Label>

                    <asp:Label runat="server" Font-Bold="true" ID="lblFeatureN2"></asp:Label>
                    <asp:Button ID="Closegeo" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" Style="display: none;" />
                    <asp:Button ID="btnCloseGeo" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" OnClick="btnCloseGeo_Click" />
                </h4>

            </div>
            <div class="modal-header" style="text-align: center">
                <div class="row">
                    <div class="col-md-6" style="text-align: left">
                        <asp:RadioButtonList ID="radbtnlisst" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="radbtnlisst_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="&nbsp; By Route&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="&nbsp; By Area"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-6" style="text-align: right">
                        <asp:Button ID="btnSaveGeo" runat="server" CssClass="btn btnd btncompt" Text="Apply Changes" OnClick="btnSaveGeo_Click" Visible="true" />
                        <asp:Button ID="btnSaveGeoFenceByRadius" runat="server" CssClass="btn btnd btncompt" Text="Apply Changes" OnClick="btnSaveGeoFenceByRadius_Click" Visible="false" />
                    </div>
                </div>

            </div>

            <div class="row" style="text-align: center">
                <asp:Label ID="lblMsgPop" runat="server"></asp:Label>

            </div>
            <div class="modal-body">
                <div class="row" style="height: 300px; overflow: auto">
                    <div class="table-responsive">
                        <asp:GridView ID="grdGeo" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="false" CssClass="table mGrid" HeaderStyle-CssClass="protable" OnRowDataBound="grdGeo_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("RouteId")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkbox" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="grdGeo_parent" runat="server" AutoPostBack="true" OnCheckedChanged="grdGeo_parent_CheckedChanged" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRouteName" runat="server" Text='<%#Eval("RouteName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                    </div>
                    <asp:Panel ID="Panel3" runat="server" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true" Visible="false" class="modal-lg modal-md modal-xs">

                        <div class="table-responsive">
                            <asp:GridView ID="grdArea" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="false" CssClass="table mGrid" HeaderStyle-CssClass="protable" OnPageIndexChanging="grdArea_PageIndexChanging" OnRowDataBound="grdArea_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaId" runat="server" Text='<%#Eval("AreaId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkboxArea" runat="server" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="grdArea_parent" runat="server" AutoPostBack="true" OnCheckedChanged="grdArea_parent_CheckedChanged" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Area Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaName" runat="server" Text='<%#Eval("AreaName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Radius (In Meter)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRadius" runat="server" Text='<%#Eval("Radius") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="dummypopupbtn" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpdelete" runat="server" PopupControlID="pnlpopup"
        TargetControlID="dummypopupbtn" CancelControlID="InvisibleNo"
        BackgroundCssClass="modalBackgroundTemp">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px">
        <table style="border: Solid 2px; width: 100%; height: 100%">
            <tr>
                <td colspan="2" align="left" style="padding: 5px; font-family: Calibri; font-weight: bold; background-color: #2a368b; color: #FFFFFF; height: 10px;">
                    <asp:Label ID="lblalert" runat="server" Text="Alert" />
                    <asp:Label ID="lblalertTimingid" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblalertfeatureid" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblalertlocreqid" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblname" runat="server" Visible="false"></asp:Label>
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

    <asp:Button ID="dummysetlocation" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpsetlocation" runat="server" TargetControlID="dummysetlocation" PopupControlID="setlocation" PopupDragHandleControlID="dragi4"
        CancelControlID="closelocation" BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="setlocation" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel4" aria-hidden="true" class="modal-lg modal-md modal-xs">
        <div class="modal-content">
            <div class="modal-header" id="dragi4">
                <h4 class="modal-title" id="myModalLabel4">
                    <asp:Label ID="lblFeatureN3" runat="server" Visible="true"></asp:Label>

                    <asp:Label runat="server" Font-Bold="true" ID="Label7"></asp:Label>
                    <asp:Button ID="closelocation" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" Style="display: none;" />
                    <asp:Button ID="Button5" runat="server" CssClass="close btn btnd btncompt waves-effect waves-light" Text="x" OnClick="btnCloseGeo_Click" />
                </h4>

            </div>
            <div class="modal-header" style="text-align: center">
                <div class="row">
                    <div class="col-md-6" style="text-align: center">
                        <input type="text" id="txtsetlocation" name="startlocation" class="form-control GetSetLocationPlace" placeholder="Enter Location" onkeydown="if (event.keyCode == 13) {return false;}" />
                        <!--onfocus="getsetlocationplace();" -->
                    </div>

                    <div class="col-md-6" style="text-align: center">
                        <asp:Button ID="btnaddlocation" runat="server" CssClass="btn btnd btncompt" Text="Add" Visible="true" OnClick="btnaddlocation_Click" />

                    </div>
                </div>
            </div>


            <div class="row" style="text-align: center">
                <asp:Label ID="msgpop" runat="server"></asp:Label>

            </div>
            <div class="modal-body">
                <div class="row" style="height: 300px; overflow: auto">
                    <div class="table-responsive">
                        <asp:GridView ID="grdsetlocation" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found!" AllowPaging="false" CssClass="table mGrid" HeaderStyle-CssClass="protable" Width="100%" OnRowDataBound="grdsetlocation_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkboxrow" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="grdsetlocation_parent" runat="server" AutoPostBack="true" OnCheckedChanged="grdsetlocation_parent_CheckedChanged" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocationName" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Latitude">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLatitude" runat="server" Text='<%#Eval("Latitude") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Longitude">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLongitude" runat="server" Text='<%#Eval("Longitude") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsetlocationId" runat="server" Text='<%#Eval("SetLocationId")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblisuploadlocation" runat="server" Text='<%#Eval("isuploadlocation")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="btnapply" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btnapply_Click" />
                        <asp:Button ID="Button7" runat="server" Text="Cancel" CssClass="btn btnd btncompt" />
                    </div>

                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
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
            //getplace();
            //getStartplace();
            //getEndplace();
        }
        function onKeyDown(e) {

            if (e && e.keyCode == Sys.UI.Key.esc) {
                // if the key pressed is the escape key, then close the dialog
                $find("<% =mp.ClientID%>").hide();
                document.getElementById('<% = btnSave.ClientID %>').click();
                $find("<% =mpfreq.ClientID%>").hide();
                $find("<% =mpgeo.ClientID%>").hide();
                $find("<% =MP1.ClientID%>").hide();
                $find("<% =mpcancel.ClientID%>").hide();

            }
        }
    </script>
    <%-- <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key= AIzaSyDBhbtcproq7ZpDQKi6k_q2J7IBYJuFt3w&libraries=places"></script>--%>
    <!-- <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false&libraries=places"></script>-->


    <script>
        $(document).ready(function () {


            if (document.getElementById('<%=RouteEndAddr.ClientID %>').value != null && document.getElementById('<%=RouteEndAddr.ClientID %>').value != "" && document.getElementById('<%=RouteStartAddr.ClientID %>').value != null && document.getElementById('<%=RouteStartAddr.ClientID %>').value != "") {
                document.getElementById('txtPlaces1').value = document.getElementById('<%=RouteEndAddr.ClientID %>').value;
                document.getElementById('txtPlaces').value = document.getElementById('<%=RouteStartAddr.ClientID %>').value;
            }
            if (document.getElementById('<%=hdnStartAddress.ClientID %>').value != null && document.getElementById('<%=hdnStartAddress.ClientID %>').value != "") {
                document.getElementById('txtAreaLocation').value = document.getElementById('<%=hdnStartAddress.ClientID %>').value;
            }

        });
    </script>
    <script type="text/javascript">
        <%-- function getplace() {
            var Places = new google.maps.places.Autocomplete(document.getElementById('txtAreaLocation'));
            google.maps.event.addListener(Places, 'place_changed', function () {
                var place = Places.getPlace();
                document.getElementById('<%=hdnStartAddress.ClientID %>').value = place.formatted_address;
                document.getElementById('<%=hdnStartLat.ClientID %>').value = place.geometry.location.lat();
                document.getElementById('<%=hdnStartLong.ClientID %>').value = place.geometry.location.lng();
                document.getElementById('<%=hdnRadius.ClientID%>').value = 100;
            });
        }--%>
    </script>
    <script type="text/javascript">
        <%--function getEndplace() {
            var Places = new google.maps.places.Autocomplete(document.getElementById('txtPlaces1'));
            google.maps.event.addListener(Places, 'place_changed', function () {
                var place = Places.getPlace();
                document.getElementById('<%=RouteEndAddr.ClientID %>').value = place.formatted_address;
                document.getElementById('<%=RouteEndLat.ClientID %>').value = place.geometry.location.lat();
                document.getElementById('<%=RouteEndLong.ClientID %>').value = place.geometry.location.lng();
            });
        }--%>
    </script>
    <script type="text/javascript">
        //function getStartplace() {
         <%--   var txtPlaces = new google.maps.places.Autocomplete(document.getElementById('txtPlaces'));
            google.maps.event.addListener(txtPlaces, 'place_changed', function () {
                var place1 = txtPlaces.getPlace();
                document.getElementById('<%=RouteStartAddr.ClientID %>').value = place1.formatted_address;
                document.getElementById('<%=RouteStartLat.ClientID %>').value = place1.geometry.location.lat();
                document.getElementById('<%=RouteStartLong.ClientID %>').value = place1.geometry.location.lng();
            });--%>
        //}
    </script>
    <script type="text/javascript">
        //function getsetlocationplace() {
           <%-- var txtPlaces = new google.maps.places.Autocomplete(document.getElementById('txtsetlocation'));
            google.maps.event.addListener(txtPlaces, 'place_changed', function () {
                var place1 = txtPlaces.getPlace();
               // alert(place1.formatted_address+" "+place1);
                document.getElementById('<%=SetLocationName.ClientID %>').value = place1.formatted_address;
                document.getElementById('<%=SetLat.ClientID %>').value = place1.geometry.location.lat();
                document.getElementById('<%=SetLongi.ClientID %>').value = place1.geometry.location.lng();
            });--%>
        //}
    </script>
    <script type="text/javascript">
        var bermudaTriangle;
        var pts = [];
        function initialize() {
            var data = JSON.parse('<%=getdata() %>');
            var center = [data[0].Latiude, data[0].Longitude]
            LoadMap('maps', center);
            for (i = 0; i < data.length; i++) {
                pts.push([data[i].Latiude, data[i].Longitude]);
            }
            create_polygons_editable(pts);
        }
        $(document).ready(function () {
            $('.StartLocation').keyup(function () {
                var TxtBoxid = $(this).attr('id');
                var HiddenStartAddr = document.getElementById('<%=RouteStartAddr.ClientID %>').id;
                var HiddenStartLat = document.getElementById('<%=RouteStartLat.ClientID %>').id;
                var HiddenStartLng = document.getElementById('<%=RouteStartLong.ClientID %>').id;
                GetLocationFromApi(TxtBoxid, HiddenStartLat, HiddenStartLng, HiddenStartAddr);
            });
            $('.EndLocation').keyup(function () {
                var TxtBoxid = $(this).attr('id');
                var HiddenEndAddr = document.getElementById('<%=RouteEndAddr.ClientID %>').id;
                var HiddenEndLat = document.getElementById('<%=RouteEndLat.ClientID %>').id;
                var HiddenEndLng = document.getElementById('<%=RouteEndLong.ClientID %>').id;
                GetLocationFromApi(TxtBoxid, HiddenEndLat, HiddenEndLng, HiddenEndAddr);
            });

            $('.AreaLocation').keyup(function () {

                var TxtBoxid = $(this).attr('id');
                var HiddenAddr = document.getElementById('<%=hdnStartAddress.ClientID %>').id;
                var HiddenLat = document.getElementById('<%=hdnStartLat.ClientID %>').id;
                var HiddenLng = document.getElementById('<%=hdnStartLong.ClientID %>').id;
                document.getElementById('<%=hdnRadius.ClientID%>').value = 100;
                GetLocationFromApi(TxtBoxid, HiddenLat, HiddenLng, HiddenAddr);
            });
            $('.GetSetLocationPlace').keyup(function () {

                var TxtBoxid = $(this).attr('id');
                var HiddenAddr = document.getElementById('<%=SetLocationName.ClientID %>').id;
                var HiddenLat = document.getElementById('<%=SetLat.ClientID %>').id;
                var HiddenLng = document.getElementById('<%=SetLongi.ClientID %>').id;
                GetLocationFromApi(TxtBoxid, HiddenLat, HiddenLng, HiddenAddr);
            });
        })
        <%--   function initialize() {
            var data = JSON.parse('<%=getdata() %>');
            var myLatLng = new google.maps.LatLng(data[0].Latiude, data[0].Longitude);
            var mapOptions = {
                zoom: 12,
                center: myLatLng,
                mapTypeId: google.maps.MapTypeId.RoadMap
            };

            var map = new google.maps.Map(document.getElementById('map_canvas'),
            mapOptions);


            var triangleCoords = [
            new google.maps.LatLng(data[0].Latiude, data[0].Longitude),
            new google.maps.LatLng(data[1].Latiude, data[1].Longitude),
            new google.maps.LatLng(data[2].Latiude, data[2].Longitude)

            ];

            // Construct the polygon
            bermudaTriangle = new google.maps.Polygon({
                paths: triangleCoords,
                draggable: true,
                editable: true,
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: '#FF0000',
                fillOpacity: 0.35
            });

            bermudaTriangle.setMap(map);
            google.maps.event.addListener(bermudaTriangle, "dragend", getPolygonCoords);
            google.maps.event.addListener(bermudaTriangle.getPath(), "insert_at", getPolygonCoords);
            google.maps.event.addListener(bermudaTriangle.getPath(), "remove_at", getPolygonCoords);
            google.maps.event.addListener(bermudaTriangle.getPath(), "set_at", getPolygonCoords);
        }--%>

        <%-- function getPolygonCoords() {
            var len = bermudaTriangle.getPath().getLength();
            var htmlStr = "";
            for (var i = 0; i < len; i++) {
                htmlStr += bermudaTriangle.getPath().getAt(i).toUrlValue(5) + ",";
            }
            document.getElementById('info').innerHTML = htmlStr;
            document.getElementById('<%=hidden.ClientID %>').value = htmlStr;

        }--%>
    </script>
    <script type="text/javascript">

        // LOCATION IN LATITUDE AND LONGITUDE.
        //var center = new google.maps.LatLng(19.0822507, 72.8812041);
        var circle;
        function area() {

            var data = JSON.parse('<%=getAreaData() %>');
            var center = [data[0].Latiude, data[0].Longitude]
            LoadMap('maps', center);
            pts.push(data[0].Latiude, data[0].Longitude);
            DrawCircle(pts, data[0].Radius);
            document.getElementById('txtAreaLocation').value = document.getElementById('<%=hdnStartAddress.ClientID %>').value;
            <%--var data = JSON.parse('<%=getAreaData() %>');
            document.getElementById('txtAreaLocation').value = document.getElementById('<%=hdnStartAddress.ClientID %>').value;
            var myLatLng = new google.maps.LatLng(data[0].Latiude, data[0].Longitude);
            var myradius = parseInt(data[0].Radius);
            // MAP ATTRIBUTES.
            var mapAttr = {
                center: myLatLng,
                zoom: 15,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            // THE MAP TO DISPLAY.
            var map = new google.maps.Map(document.getElementById("map_canvas"), mapAttr);

            circle = new google.maps.Circle({
                center: myLatLng,
                map: map,
                draggable: true,
                editable: true,
                radius: myradius,          // IN METERS.
                fillColor: '#FF6600',
                fillOpacity: 0.3,
                strokeColor: "#FFF",
                strokeWeight: 0         // DON'T SHOW CIRCLE BORDER.
            });
            google.maps.event.addListener(circle, 'radius_changed', onCircleComplete);
            google.maps.event.addListener(circle, 'center_changed', onCircleComplete);--%>
        }
        function onCircleComplete() {
            document.getElementById('<%=hdnStartLat.ClientID %>').value = circle.getCenter().lat();
            document.getElementById('<%=hdnStartLong.ClientID %>').value = circle.getCenter().lng();
            document.getElementById('<%=hdnRadius.ClientID %>').value = circle.getRadius();
            document.getElementById('<%=txtRadius.ClientID %>').value = document.getElementById('<%=hdnRadius.ClientID %>').value;
            document.getElementById('txtAreaLocation').value = document.getElementById('<%=hdnStartAddress.ClientID %>').value;
        }

    </script>
</asp:Content>





