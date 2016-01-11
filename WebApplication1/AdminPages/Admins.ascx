<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admins.ascx.cs" Inherits="WebApplication1.AdminPages.Admins" %>
<asp:GridView ID="GridViewAdmins" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="UserId" DataSourceID="SqlDataSourceAdmins" OnSelectedIndexChanged="GridViewAdmins_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="UserId" HeaderText="User Id" SortExpression="UserId"></asp:BoundField>
        <asp:TemplateField HeaderText="Permissions">
            <ItemTemplate>
                <asp:Image ID="ImagePermAdmins" runat="server" Height="24px" ImageUrl='<%# Eval("PermAdmins", "~/Images/Admin/Permissions/Admins{0}.png") %>' ToolTip="Admins" Width="24px" />
                <asp:Image ID="ImagePermBlog" runat="server" Height="24px" ImageUrl='<%# Eval("PermBlog", "~/Images/Admin/Permissions/Blog{0}.png") %>' ToolTip="Blog" Width="24px" />
                <asp:Image ID="ImagePermEvents" runat="server" Height="24px" ImageUrl='<%# Eval("PermEvents", "~/Images/Admin/Permissions/Events{0}.png") %>' ToolTip="Events" Width="24px" />
                <asp:Image ID="ImagePermLocations" runat="server" Height="24px" ImageUrl='<%# Eval("PermLocations", "~/Images/Admin/Permissions/Locations{0}.png") %>' ToolTip="Locations" Width="24px" />
                <asp:Image ID="ImagePermSettings" runat="server" Height="24px" ImageUrl='<%# Eval("PermSettings", "~/Images/Admin/Permissions/Settings{0}.png") %>' ToolTip="Settings" Width="24px" />
                <asp:Image ID="ImagePermStats" runat="server" Height="24px" ImageUrl='<%# Eval("PermStats", "~/Images/Admin/Permissions/Stats{0}.png") %>' ToolTip="Stats" Width="24px" />
                <asp:Image ID="ImagePermUsers" runat="server" Height="24px" ImageUrl='<%# Eval("PermUsers", "~/Images/Admin/Permissions/Users{0}.png") %>' ToolTip="Users" Width="24px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:ImageField DataImageUrlField="Status" DataImageUrlFormatString="~/Images/Admin/Permissions/status{0}.png" HeaderText="Status" SortExpression="Status">
        </asp:ImageField>
        <asp:CommandField HeaderText="Edit" SelectText="Edit" ShowSelectButton="True"></asp:CommandField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceAdmins" runat="server" ConnectionString="<%$ ConnectionStrings:AppConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="sp_admins" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
<br />
<asp:Panel ID="PanelEdit" runat="server" Visible="False">
                <asp:Label ID="LabelUserId0" runat="server" Text="User Id:"></asp:Label>
            <br />
                <asp:Label ID="LabelEditUserId" runat="server" ></asp:Label>
            <br />
                <asp:Label ID="LabelPremissions0" runat="server" AssociatedControlID="CheckBoxListPremissions" Text="Permissions:"></asp:Label>
            <br />
                <asp:CheckBoxList ID="CheckBoxListEditPremissions" runat="server" >
                    <asp:ListItem Value="Admins">Admins</asp:ListItem>
                    <asp:ListItem Value="Blog">Blog</asp:ListItem>
                    <asp:ListItem Value="Events">Events</asp:ListItem>
                    <asp:ListItem Value="Locations">Locations</asp:ListItem>
                    <asp:ListItem Value="Settings">Settings</asp:ListItem>
                    <asp:ListItem Value="Statistics">Statistics</asp:ListItem>
                    <asp:ListItem Value="Users">Users</asp:ListItem>
                </asp:CheckBoxList>
                <asp:Button ID="ButtonEdit" runat="server" ValidationGroup="Add" Text="Edit" OnClick="ButtonEdit_Click" />
            <br />
    <asp:Label ID="LabelEditMessage" runat="server"></asp:Label>
</asp:Panel>
<asp:Label ID="Label1" runat="server" Text="Add an Admin"></asp:Label>
<br />
<br />
            <asp:Label ID="LabelUserId" runat="server" AssociatedControlID="TextBoxUserId" Text="User Id:"></asp:Label>
        <br />
            <asp:TextBox ID="TextBoxUserId" runat="server" ValidationGroup="Regeant" Width="100px"></asp:TextBox>
            <br />
            <asp:Label ID="LabelPremissions" runat="server" AssociatedControlID="CheckBoxListPremissions" Text="Permissions:"></asp:Label>
        <br />
            <asp:CheckBoxList ID="CheckBoxListPremissions" runat="server">
                <asp:ListItem Value="Admins">Admins</asp:ListItem>
                <asp:ListItem Value="Blog">Blog</asp:ListItem>
                <asp:ListItem Value="Events">Events</asp:ListItem>
                <asp:ListItem Value="Locations">Locations</asp:ListItem>
                <asp:ListItem Value="Settings">Settings</asp:ListItem>
                <asp:ListItem Value="Statistics">Statistics</asp:ListItem>
                <asp:ListItem Value="Users">Users</asp:ListItem>
            </asp:CheckBoxList>
            <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" />
<br />
<asp:Label ID="LabelAddMessage" runat="server"></asp:Label>