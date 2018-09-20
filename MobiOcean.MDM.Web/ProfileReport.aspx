<%@ Page Title="Profile Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ProfileReport.aspx.cs" Inherits="MobiOcean.MDM.Web.ProfileReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
    <style type="text/css">
        .custom-add-profile:hover {
            color: none !important;
            background: none !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">

                    <div class="bhoechie-tab-content active div">
                        <li class="profile1">Profile Report</li>
                        <br />
                        <div class="row">
                            <div style="text-align: center; color: #FFFFFF" class="col-lg-4 profile2">
                                <h4 style="margin-top: 0px; margin-bottom: 0px;">Profile Name :
                                        <asp:Label ID="txtProfileName" runat="server"></asp:Label></h4>
                            </div>
                            <div class="col-md-8" style="text-align: right">
                                <asp:Button ID="btnManage" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Manage Feature" PostBackUrl="~/feature.aspx" />
                                &nbsp;
                                        <asp:Button ID="btnback" runat="server" CssClass="btn btn-sky text-uppercase custom-add-profile pull-right" Text="Back" PostBackUrl="~/profilemaster.aspx" />
                            </div>
                            <br />
                            <br />
                        </div>

                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="grdCategory" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryId" ShowHeader="true" GridLines="None" Width="100%" class="table mGrid" HeaderStyle-CssClass="protable" OnRowDataBound="OnRowDataBound">
                                
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("CategoryId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="javascript:expandcollapse('div<%# Eval("CategoryId") %>', 'one');">
                                                <img id='imgdiv<%# Eval("CategoryId") %>' alt="Click to show/hide Feature for <%# Eval("CategoryName") %>" src="image/plus.png" />
                                            </a>
                                        </ItemTemplate>
                                      <ItemStyle Width="25%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Categories">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("CategoryName")%>' Font-Bold="true" Font-Size="Large"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="75%" />
                                    </asp:TemplateField>

                                   

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr style="background-color: whitesmoke">
                                                <td colspan="100%" style="text-align: center; width: 100%">
                                                    <div id='div<%# Eval("CategoryId") %>' class="table-responsive" style="display: none; position: relative">
   
                                                        <asp:GridView ID="GridFeature" runat="server" AutoGenerateColumns="False" DataKeyNames="FeatureId" ShowHeader="true" GridLines="None" Width="100%" class="table mGrid" HeaderStyle-CssClass="protable"
                                                            OnRowDataBound="GridFeature_RowDataBound">
                                                           
                                                             <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <a href="javascript:expandcollapse('fid<%# Eval("FeatureId") %>', 'one');">
                                                                            <img id='imgfid<%# Eval("FeatureId") %>' alt="Click to show/hide Feature for <%# Eval("CategoryName") %>" src="image/plus.png" />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                     <ItemStyle Width="15%" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Id" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFId" runat="server" Text='<%#Eval("FeatureId")%>'></asp:Label>
                                                                        <asp:Label ID="lblCId" runat="server" Text='<%#Eval("CategoryId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="ProfileId" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId")%>'></asp:Label>
                                                                        <asp:Label ID="lblProfileFeatureId" runat="server" Text='<%#Eval("ProfileFeatureMappingId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Feature">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblClientCode" runat="server" Text='<%#Eval("FeatureName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                     <ItemStyle Width="50%" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Enabled">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="switchsize" runat="server" CssClass="toggleswitch" data-on-text="Yes" data-off-text="No" data-size="small" Visible="false" Checked="false" />
                                                                        <asp:ImageButton ID="btnyes" runat="server" ImageAlign="Middle" ImageUrl="~/image/bigYesNew.png" Visible="false" CommandName="Yes" />
                                                                        <asp:ImageButton ID="btnNo" runat="server" ImageAlign="Middle" ImageUrl="~/image/BigNoNew.png" Visible="false" CommandName="No" />
                                                                        <asp:Label ID="lblChanged" runat="server" Text='<%#Eval("IsChanged")%>' Visible="false" />
                                                                    </ItemTemplate>
                                                                     <ItemStyle Width="35%" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <tr style="background-color: whitesmoke">
                                                                            <td style="text-align: center; width: 80%" colspan="100%">
                                                                                <div id='fid<%# Eval("FeatureId") %>' class="table-responsive" data-pattern="priority-columns" style="display: none; position: relative">
                                                                                   
                                                                                     <asp:GridView ID="grdrpt" runat="server" class="table mGrid" HeaderStyle-CssClass="protable" GridLines="None"
                                                                                        DataKeyNames="ProfileFeatureTimingId" AutoGenerateColumns="false" EmptyDataText="No Schedule Available!">
                                                                                        <Columns>

                                                                                            <asp:TemplateField HeaderText="Profile Id" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblProfileFeatureMappingId" runat="server" Text='<%#Eval("ProfileFeatureTimingId")%>'></asp:Label>
                                                                                                </ItemTemplate>

                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Day">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblFromDay" runat="server" Text='<%#Eval("FromDay").ToString()=="1"?"Monday":Eval("FromDay").ToString()=="2"?"Tuesday":Eval("FromDay").ToString()=="3"?"Wednesday":Eval("FromDay").ToString()=="4"?"Thursday":Eval("FromDay").ToString()=="5"?"Friday":Eval("FromDay").ToString()=="6"?"Saturday":"Sunday"%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="From Time">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblFromTime" runat="server" Text='<%#Eval("FromTime")%>'></asp:Label>
                                                                                                </ItemTemplate>

                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="To Time">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblToTime" runat="server" Text='<%#Eval("ToTime")%>'></asp:Label>
                                                                                                </ItemTemplate>

                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Duration">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblDuration" runat="server" Text='<%#(string.IsNullOrEmpty(Eval("Duration").ToString()))?"---":Eval("Duration")%>'></asp:Label>
                                                                                                </ItemTemplate>

                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>


                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>



                        <!-- train section -->

                    </div>
                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function expandcollapse(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "image/minus.png";
                }
                else {
                    img.src = "image/minus.png";
                }
                img.alt = "Close to view other Features";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "image/plus.png";
                }
                else {
                    img.src = "image/plus.png";
                }
                img.alt = "Expand to show Features";
            }
        }
    </script>
</asp:Content>
