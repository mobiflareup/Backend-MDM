<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="WebManagement.aspx.cs" Inherits="MobiOcean.MDM.Web.WebManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="coh" runat="server" ContentPlaceHolderID="ContentHead">

    <style type="text/css">
        .modalBackgroundTemp {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        .chkChoice input 
        { 
         margin-left: 10px; 
        }
        .chkChoice td 
        { 
        padding-left: 20px; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active div">

                        <div class="panel panel-default">
                            <div class="profile1" style="margin: 0px;">
                                &nbsp;&nbsp;Web Management
                                    <a href="javascript:;" class=" pull-right">
                                        <asp:Button ID="Button4" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Manage Websites" OnClick="btnBlackListUrls_Click" /></a>
                            </div>
                        </div>

                        <div style="text-align: center; color: #FFFFFF" class="col-lg-4 profile2">
                            <h4 style="margin-top: 0px; margin-bottom: 0px;">Profile Name :
                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                        </div>
                        <br />
                        <asp:Panel ID="PanelBlackListUrl" runat="server" Visible="false">
                            <div class="row" style="text-align: center">
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </div>
                            <div class="row" style="text-align: right">
                                <a href="#" id="flip" class="btn btnd btncompt waves-effect waves-light">
                                    <i>
                                        <img src="image/plus-4.png" class="iconview1"></i>&nbsp;&nbsp;<span class="creatsp">Add Website</span></a>
                            </div>
                            <br />
                            <div id="panel" class="flipkey">
                                <div class="row">
                                    <div class="col-lg-7 col-md-12">
                                        <div class="form-group ">
                                            <label for="bname" class="control-label col-lg-4">Category Name* : </label>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                <asp:CompareValidator ID="comp" runat="server" ControlToValidate="ddlGroupName" ValueToCompare="0" Operator="NotEqual" ValidationGroup="save" ErrorMessage="Required!" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                        </div>

                                        <div class="form-group ">
                                            <label for="firstname" class="control-label col-lg-4">Website* : </label>
                                            <div class="col-lg-8">
                                                <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtApplicationName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-lg-4"></label>
                                            <div class="col-lg-8">
                                                <asp:Label ID="lblerror" runat="server"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-lg-offset-4 col-lg-6">
                                                <asp:Button ID="Btncatsave" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="Btncatsave_Click" />
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" />

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <asp:Button ID="addappmaster" runat="server" class="btn btnd btncompt" Text="Add Website Category" OnClick="addappmaster_Click" />


                            <asp:Panel ID="pnladdappMas" class="form-group" runat="server">
                                <div class="panel-body table-rep-plugin">
                                    <div class=" form">

                                        <div class="col-lg-7">
                                            <div class="form-group ">
                                                <label for="bname" class="control-label col-lg-4">Category Code* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtKCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                        ControlToValidate="txtKCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                            </div>
                                            <div class="form-group ">
                                                <label for="firstname" class="control-label col-lg-4">Category Name* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtKName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtKName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <label for="lastname" class="control-label col-lg-4">Description* : </label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="txtKDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtKDesc" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <label class="control-label col-lg-4"></label>
                                                <div class="col-lg-8">
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-offset-4 col-lg-6">
                                                    <asp:Button ID="btnAssign" runat="server" class="btn btnd btncompt waves-effect waves-light" Text="Save" ValidationGroup="save" OnClick="btnAssign_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt waves-effect" Text="Cancel" CommandName="Cancel" OnClick="btnCancel_Click" />

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-5">
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>



                            <div class="row">
                                <div class="col-sm-12" style="text-align: center">
                                    <div class="dataTables_length" id="datatable-editable_length">
                                        <label>
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <div class="table-responsive">
                                <asp:GridView ID="GridFeature" runat="server" DataKeyNames="CategoryId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                                    PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No record found." OnRowEditing="GridFeature_RowEditing" OnRowCancelingEdit="GridFeature_RowCancelingEdit" OnRowDeleting="GridFeature_RowDeleting" OnRowUpdating="GridFeature_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("CategoryId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryCode" runat="server" Text='<%#Eval("CtegoryCode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEGrpCode" runat="server" Text='<%#Eval("CtegoryCode") %>' CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtEGrpCode" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%#Eval("CategoryName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEGrpName" runat="server" Text='<%#Eval("CategoryName") %>' CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtEGrpName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryDesc" runat="server" Text='<%#Eval("CategoryDesc")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEDesc" runat="server" Text='<%#Eval("CategoryDesc") %>' CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvnametxtEDesc" runat="server" ControlToValidate="txtEDesc" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="Update" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manage">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPopUp" runat="server" OnClick="lnkPopUp_Click"><i class="fa fa-cogs custom-table-fa" aria-hidden="true"></i></asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                                </asp:GridView>

                            </div>
                        </asp:Panel>
                        <br />
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Label ID="lblSave" runat="server"></asp:Label>

                            </div>
                        </div>
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
                                    <asp:TemplateField HeaderText ="Manage">
                                        <ItemTemplate>
                                      
                                             <asp:Label ID="IsManageNeed" runat="server"  Visible="false" Text='<%#Eval("IsManageNeed")%>' ></asp:Label>
                                           <asp:Label ID="Duration" runat="server"  Visible="false" Text='<%#Eval("Duration")%>' ></asp:Label>
                                            <asp:Label ID="NA" runat="server" Visible="false" Text="N/A"></asp:Label>
                                       
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" align="center" RepeatLayout="Flow" CssClass="chkChoice" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Black List" Value="0"  ></asp:ListItem>
                                                 <asp:ListItem Text="White List" Value="1" ></asp:ListItem>
                                             </asp:RadioButtonList>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>

                        </div>

                        <div>
                            <asp:Button ID="btnsubmit" runat="server" Text="Apply Changes" CssClass="btn btnd btncompt" OnClick="btn1submit_Click" />
                            <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btnd btncompt" OnClick="btn1cancel_Click" />

                        </div>
                    </div>
                </div>

            </div>

                    <asp:Button ID="btnCheckFeature" runat="server" Style="display: none;" />

    <asp:ModalPopupExtender ID="MPCheckFeature" runat="server" PopupControlID="CheckFeaturePnl"
        TargetControlID="btnCheckFeature" CancelControlID="btnccl"
        BackgroundCssClass="modalbackground">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="CheckFeaturePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" BackColor="#2a368b" aria-hidden="true">

        <div class="modal-body" style="text-align: center;">
            <asp:Button ID="Button5" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
            <asp:Label ID="Label3" runat="server" Text="You don't have license to enable this feature. Please purchase more license." ForeColor="Red"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnCheckFeatureCancel" runat="server" Text="OK" OnClick="btnCheckFeatureCancel_Click" />
        </div>


    </asp:Panel>

            <asp:Button ID="dummy_Btn1AsgnGp" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="myModal1"
                PopupDragHandleControlID="dragi1" TargetControlID="dummy_Btn1AsgnGp" CancelControlID="btn1Close"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="myModal1" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" class="modal-lg modal-md modal-xs">
                <div class="modal-content">
                    <div class="modal-header" id="dragi1">
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
                            <asp:Button ID="btnClose1" runat="server" CssClass="close btn btnd btncompt" Text="x" OnClick="btn1Save_Click" Style="margin: -5px -18px;" />
                            <asp:Button ID="btn1Save" runat="server" CssClass="btn btnd btncompt" Text="Save" OnClick="btn1Save_Click" Style="display: none;" />
                            <asp:Button ID="btn1Close" runat="server" CssClass="btn btnd btncompt" Text="Close" OnClick="btn1Close_Click" Style="display: none;" />


                        </div>
                    </div>
                    <div class="modal-header">
                        <div class="row">
                            <div class="col-sm-4 hadjust" style="vertical-align: middle">
                                <asp:Label ID="lblAppHead" runat="server" Text="Website Category : " Visible="false"></asp:Label>
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
                            <br>
                            </br> 
                            <br> </br>
                                                <div class="col-sm-12" style="text-align: center">
                                                    <asp:Label runat="server" ID="lblManFields" ForeColor="Red"></asp:Label>
                                                    <asp:Label runat="server" ID="Label4" ForeColor="Red"></asp:Label>
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
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Web Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppGroupId" runat="server" Text='<%#Eval("GroupId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblAppGroupName" runat="server" Text='<%#Eval("CategoryName")%>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="dummy_BtnAsgnGp" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mp" runat="server" PopupControlID="myModal"
                PopupDragHandleControlID="dragi" TargetControlID="dummy_BtnAsgnGp" CancelControlID="btnclose"
                BackgroundCssClass="modalbackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="myModal" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" class="modal-lg modal-md modal-xs">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="col-sm-8" style="text-align: left">
                            <asp:Label ID="lblGrpId" runat="server" Visible="false"></asp:Label>
                            <h4><b></b>Category Name :
                                    <asp:Label ID="lblGroupName" runat="server"></asp:Label></b></h4>
                        </div>
                        <div class="col-sm-4" style="text-align: right">
                            <asp:Button ID="btnclose" runat="server" Text="x" class="close btn btnd btncompt" />
                        </div>
                    </div>
                    <div class="modal-header">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblPopMsg" runat="server"></asp:Label>
                            </div>

                        </div>

                    </div>

                    <div class="modal-body">
                        <div class="row" style="height: 250px; overflow: auto">
                            <div class="col-sm-6">
                                <asp:Button ID="btnaddselected" runat="server" class="btn btnd btncompt" Text="Add Selected ==>>" Width="200px" OnClick="btnaddselected_Click" />
                                &nbsp; 
                                    <br />
                                <br />
                                <%-- Search --%>

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
                                    <asp:GridView ID="grdaddselected" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAId" runat="server" Text='<%#Bind("UrlId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="AchkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="AchkHeader_Parents" runat="server" AutoPostBack="true" OnCheckedChanged="AchkHeader_Parents_CheckedChanged" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Website">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppName" runat="server" Text='<%#Eval("Url")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="AlblAppGroupName" runat="server" Text='<%#string.IsNullOrEmpty(Eval("CategoryName").ToString())?"---":Eval("CategoryName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <asp:Button ID="btnremoveselected" runat="server" class="btn btnd btncompt" Text="<<==Remove Selected" Width="200px" OnClick="btnremoveselected_Click" />
                                &nbsp;
                                    <br />
                                <br />
                                <%-- Search --%>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtremoveSearch" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnremoveSearch" runat="server" class="btn btnd btncompt" Text="Search" Width="100px" OnClick="btnremoveSearch_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="table-responsive" data-pattern="priority-columns">
                                    <asp:GridView ID="grdremoveselected" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record found" ShowHeader="true" ShowHeaderWhenEmpty="true" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="RlblAId" runat="server" Text='<%#Bind("UrlId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="RachkRow_Parents" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="RachkHeader_Parents" AutoPostBack="true" OnCheckedChanged="RachkHeader_Parents_CheckedChanged" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Website">
                                                <ItemTemplate>
                                                    <asp:Label ID="RlblAppName" runat="server" Text='<%#Eval("Url")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#string.IsNullOrEmpty(Eval("CategoryName").ToString())?"---":Eval("CategoryName")%>'></asp:Label>
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

            </asp:Panel>

            <asp:Button ID="dummy" runat="server" Style="display: none;" />

            <asp:ModalPopupExtender ID="MP2" runat="server" PopupControlID="MessagePnl"
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

                <div class="modal-body" style="text-align: center;color: green;">
                    <asp:Button ID="Button3" runat="server" Text="x" class="close btn btnd btncompt" Style="display: none;" />
                    <asp:Label ID="Label6" runat="server" Text="Are you sure you want to leave the page?"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btncancelok" runat="server" Text="OK" OnClick="btncancelok_Click" />
                    <asp:Button ID="btncancelcan" runat="server" Text="Cancel" OnClick="btncancelcan_Click" />
                </div>


            </asp:Panel>



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
                // if the key pressed is the escape key, then close the dialog
                $find("<% =mp.ClientID%>").hide();
                $find("<% =mp1.ClientID%>").hide();
                $find("<% =MP2.ClientID%>").hide();
                $find("<% =mpcancel.ClientID%>").hide();
                document.getElementById('<%= btn1Save.ClientID %>').click();

            }
        }
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblManFields.ClientID %>").style.display = "none";
             }, seconds * 1000);
         };
    </script>

    <script>
        function pageLoad(sender, args) {

            if (!args.get_isPartialLoad()) {
                //  adding handler to the document's keydown event
                $addHandler(document, "keydown", onKeyDown);
            }
            $(document).ready(function () {
                $("#panel").hide();
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





