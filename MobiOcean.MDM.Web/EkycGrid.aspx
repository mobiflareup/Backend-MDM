<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EkycGrid.aspx.cs" Inherits="MobiOcean.MDM.Web.EkycGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <div class="bhoechie-tab-content active div">

                    <div class="profile1">&nbsp;&nbsp;EKYC Report
                        <%--<div class="col-lg-3 pull-right"><a href="AttendanceType.aspx" class="btn btn-sky text-uppercase custom-add-profile pull-right">Attendance Management</a></div>--%>
                    </div>

                    <br />

                    <div class="row" style="text-align:center">

                        <div class=" form">
                            <div class="form-group col-lg-8">

                                <div class="col-lg-4">
                                    <label>
                                        AadharNo :
                                                              <asp:TextBox ID="txtaadhar" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                 <div class="col-lg-4">
                                    <label>
                                        Name :
                                                               <asp:TextBox ID="txtnme" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                            
                                  
                                <div class="form-group col-lg-2" style="vertical-align:middle">
                                    <label><br />
                                        
                                       <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click"/>
                                    </label>
                                </div>
                            


                        </div>
                    </div>

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
                        <asp:GridView ID="grdEkyc" runat="server" DataKeyNames="Id" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." Width="100%" OnPageIndexChanging="grdEkyc_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id")%>'></asp:Label>                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Residence Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnme" runat="server" Text='<%#Eval("Name").ToString()==""?"---":Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Aadhar No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAadr" runat="server" Text='<%#Eval("AadharNo").ToString()==""?"---":Eval("AadharNo")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("Address").ToString()==""?"---":Eval("Address")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MobileNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMob" runat="server" Text='<%#Eval("MobileNo").ToString()==""?"---":Eval("MobileNo")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Parent Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParent" runat="server" Text='<%#Eval("ParentName").ToString()==""?"---":Eval("ParentName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGender" runat="server" Text='<%#Eval("gender").ToString()==""?"---":Eval("gender")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="DOB">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" runat="server" Text='<%#Eval("DOB").ToString()==""?"---":Eval("DOB")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               <%--  <asp:TemplateField HeaderText="Employee Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("Image").ToString()==""?"---":Eval("Image")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                               <asp:TemplateField HeaderText="Responce Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRes_Code" runat="server" Text='<%#Eval("Kyc_Res_Code").ToString()==""?"---":Eval("Kyc_Res_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Time Stamp">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKyc_Timestamp" runat="server" Text='<%#Eval("Kyc_Timestamp").ToString()==""?"---":Eval("Kyc_Timestamp")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                        </asp:GridView>

                    </div>
                    <!-- train section -->
                 </div>

            </div>
        </div>
        </div>

</asp:Content>
