<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CS.aspx.cs" Inherits="MobiOcean.MDM.Web.CS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $("[id*=lnkView]").live("click", function () {
        var subject = $(this).text();
        var row = $(this).closest("tr");
        $("#body").html($(".body", row).html());
        $("#attachments").html($(".Attachments", row).html());
        $("#dialog").dialog({
            title: subject,
            buttons: {
                Ok: function () {
                    $(this).dialog('close');
                }
            }
        });
        return false;
    });
</script>
</head>
<body style="font-family: Arial; font-size: 10pt">
    <form id="form1" runat="server">
<asp:GridView ID="gvEmails" runat="server" OnRowDataBound="OnRowDataBound" DataKeyNames="MessageNumber"
    AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField HeaderText="From" DataField="From" HtmlEncode="false" />
        <asp:TemplateField HeaderText="Subject">
            <ItemTemplate>
                <asp:LinkButton ID="lnkView" runat="server" Text='<%# Eval("Subject") %>' />
                <span class="body" style="display: none">
                    <%# Eval("Body") %></span>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Date" DataField="DateSent" />
        <asp:TemplateField ItemStyle-CssClass="Attachments">
            <ItemTemplate>
                <asp:Repeater ID="rptAttachments" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkAttachment" runat="server" OnClick="Download" Text='<%# Eval("FileName") %>' />
                    </ItemTemplate>
                    <SeparatorTemplate>
                        <br>
                    </SeparatorTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<div id="dialog" style="display: none">
    <span id="body"></span>
    <br />
    <span id="attachments"></span>
</div>
    </form>
</body>
</html>
