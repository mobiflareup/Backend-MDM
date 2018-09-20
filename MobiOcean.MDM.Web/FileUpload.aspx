<%@ Page Title="Secure Storage" Language="C#" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="MobiOcean.MDM.Web.FileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
      <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab scrollbar" id="style-3">
                <div class="force-overflow">
                    <div class="bhoechie-tab-content active div">
                        <div class="profile1" style="margin: 0px;">
                           Secure Storage
                            
                                
                            
                        </div>
                        <br />
                        <div class=" row">
                            <div class="col-md-12">
                             <%--   <div class="form-group">
                                    <div class="col-md-6">
                                        <label for="bname" class="control-label col-lg-4">File Name : </label>
                                        <div class="col-lg-8">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="File Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv"  runat="server" ControlToValidate="txtName" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="form-group ">
                                    <div class="col-md-8">
                                        <label for="bname" class="control-label col-lg-3">Upload File : </label>
                                        <div class="col-lg-9">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btnd btncompt waves-effect waves-light" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ControlToValidate="FileUpload1" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                        </div>                                        
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 text-left">
                                        <div class="col-lg-12">
                                        <asp:Button ID="btnSaveForm" runat="server" Text="Upload" CssClass="btn btnd btncompt" OnClick="btnSaveForm_Click" ValidationGroup="upload" />  <%--OnClick="btnAdd_Click"--%>
                                    </div>
                                        </div>
                                </div>
                           </div>
                        </div>
                        <hr />
                        <div class="col-md-12 text-center">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <br />
                        
                         <div class=" row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                            <div class="form-group">
                             <label for="fname" class="control-label col-lg-4">File Name : </label>
                                        <div class="col-lg-8">
                                <asp:TextBox ID="FileName" runat="server" CssClass="form-control" placeholder="File Name"></asp:TextBox>
                                            </div>
                            </div>
                                </div>
                           
                            <div class="col-md-4">
                            <div class="form-group">
                             <label for="type" class="control-label col-lg-4">Type : </label>
                                        <div class="col-lg-8">
                                <asp:TextBox ID="Type" runat="server" CssClass="form-control" placeholder="Type"></asp:TextBox>
                                            </div>
                            </div>
                                </div>
                            <div class="form-group">
                            <div class="col-md-4 text-center">
                                <asp:Button ID="Search" runat="server" OnClick="Search_Click" Text="Search" CssClass="btn btnd btncompt" />
                            </div>
                                </div>
                       
                             </div>
                             </div>
                        <br />
                       
                        <div class="table-responsive">
                            <asp:GridView ID="grdAPKs" runat="server" GridLines="None" class="table mGrid" AutoGenerateColumns="false"
                                HeaderStyle-CssClass="protable" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No record found."
                                OnPageIndexChanging="grdAPKs_PageIndexChanging" Width="100%" PageSize="10" AllowPaging="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                         <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="UserFileName" runat="server" Text='<%#Eval("UserFileName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                        
                                     <asp:TemplateField HeaderText="File Type">
                                        <ItemTemplate>
                                            <asp:Label ID="FileType" runat="server" Text='<%#Eval("FileType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="File Size">
                                        <ItemTemplate>
                                            <asp:Label ID="FileSize" runat="server" Text='<%#Eval("FileSize")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Creation Date">
                                        <ItemTemplate>
                                            <asp:Label ID="CreationDate" runat="server" Text='<%#Eval("CreationDate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Download">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("FilePath") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Assign User">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="asgnUser" Text = "Assign" CommandArgument = '<%#Eval("Id")%>' runat="server" OnClick="asgnUser_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="dataTables_paginate paging_simple_numbers pagination-ys" />
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
</asp:Content>

