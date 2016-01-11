<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="WebApplication1.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem NavigateUrl="~/Admin/Admins" Text="Admins" Value="Admins"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/Admin/Blog" Text="Blog" Value="Blog"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/Admin/Events" Text="Events" Value="Events"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/Admin/Locations" Text="Locations" Value="Locations"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/Admin/Settings" Text="Settings" Value="Settings"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/Admin/Statistics" Text="Statistics" Value="Statistics"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/Admin/Users" Text="Users" Value="Users"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/Admin/Feedback" Text="Feedback" Value="Feedback"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <br />
        <asp:Panel ID="PanelPage" runat="server">
        </asp:Panel>
    </div>
    </form>
</body>
</html>
