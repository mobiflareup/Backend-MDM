<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AttendanceType.aspx.cs" Inherits="MobiOcean.MDM.Web.AttendanceType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">
                <div class="bhoechie-tab-content active div">

                    <div class="profile1">&nbsp;&nbsp;Check Camera is Enable
                        <div class="col-lg-1 pull-right"><a href="AttendanceReport.aspx" class="btn btn-sky text-uppercase custom-add-profile pull-right">back</a></div>
                    </div>
                    <br />

                    <div class="row" style="text-align:center">

                        <div class=" form">
                            <div class="form-group col-lg-8">

                                <div class="col-lg-6">
                                    <label>
                                        By Employee Name :
                                                               <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>
                                 <%--<div class="col-lg-6">
                                    <label>
                                        By Employee Id :
                                                               <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control"></asp:TextBox>
                                    </label>
                                </div>--%>
                            
                                  
                                
                                <div class="form-group col-lg-4" style="vertical-align:middle">
                                    <label>
                                        <br />
                                       <asp:Button ID="btnSrch" runat="server" CssClass="btn btnd btncompt" Text="Search" OnClick="btnSrch_Click"/>
                                    </label>
                                </div>
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
                        <asp:GridView ID="grdUser" runat="server" DataKeyNames="UserId" GridLines="None" CssClass="table mGrid" HeaderStyle-CssClass="protable"
                            PageSize="20" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record found." Width="100%"  OnPageIndexChanging="grdUser_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                        <asp:Label ID="AttendanceType" runat="server" Text='<%#Eval("AttendanceTypeId")%>'></asp:Label>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Camera Enable?">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CameraEnbl" runat="server" AutoPostBack="true" OnCheckedChanged="CameraEnbl_CheckedChanged" Checked='<%#Eval("AttendanceTypeId").ToString().Contains("7")?true:false%>'/>
                                    </ItemTemplate>
                                     <HeaderTemplate>
                                         <asp:CheckBox ID="CameraEnbl_header" runat="server" AutoPostBack="true"  OnCheckedChanged="CameraEnbl_header_CheckedChanged" />
                                         <br /><label>Is Camera Enable?</label>
                                     </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="UserName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName").ToString()==""?"---":Eval("UserName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="MobileNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo").ToString()==""?"---":Eval("MobileNo")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="EmailId">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId").ToString()==""?"---":Eval("EmailId")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBrch" runat="server" Text='<%#Eval("BranchName").ToString()==""?"---":Eval("BranchName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDept" runat="server" Text='<%#Eval("DeptName").ToString()==""?"---":Eval("DeptName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                        </asp:GridView>

                    </div>
                    <%--<div class="col-lg-12">
                        <asp:Button ID="Save" CssClass="btn btnd btncompt" runat="server" Text="Save" OnClick="Save_Click" /></div>--%>
                    


                </div>

            </div>
        </div>

            </ContentTemplate>

    </asp:UpdatePanel>
     <center>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divProcessing">
                            <asp:Image runat="server" ID="progressImg2" ImageUrl="~/images/Processing.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </center>

</asp:Content>
