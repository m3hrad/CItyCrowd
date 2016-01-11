<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="WebApplication1.AdminPages.Settings" %>
<p>
    <asp:Label ID="LabelLogin" runat="server" Text="Allow Login:"></asp:Label>
    <asp:DropDownList ID="DropDownListLogin" runat="server">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Value="False">No</asp:ListItem>
    </asp:DropDownList>
</p>
<p>
    <asp:Label ID="LabelRegister0" runat="server" Text="Allow Register:"></asp:Label>
    <asp:DropDownList ID="DropDownListRegister" runat="server">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Value="False">No</asp:ListItem>
    </asp:DropDownList>
</p>
<p>
    <asp:Label ID="LabelRegister" runat="server" Text="Allow Activities:"></asp:Label>
    <asp:DropDownList ID="DropDownListActivities" runat="server">
        <asp:ListItem Value="True">Yes</asp:ListItem>
        <asp:ListItem Value="False">No</asp:ListItem>
    </asp:DropDownList>
</p>
<p>
    <asp:Label ID="LabelStatus" runat="server" Text="Site Status:"></asp:Label>
    <asp:DropDownList ID="DropDownListStatus" runat="server">
        <asp:ListItem Value="1">Online</asp:ListItem>
        <asp:ListItem Value="2">Maintenance</asp:ListItem>
    </asp:DropDownList>
</p>
<asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Change Settings" />
<p>
    &nbsp;</p>
<asp:Label ID="LabelMessage" runat="server" Visible="False"></asp:Label>

