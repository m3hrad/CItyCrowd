<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Locations.ascx.cs" Inherits="WebApplication1.AdminPages.Locations" %>
<p>
    <asp:Panel ID="PanelAdd" runat="server" Width="100%">
                <asp:Label ID="LabelLastName" runat="server" AssociatedControlID="TextBoxCountryCode" Text="Contry code:"></asp:Label>
            <br />
                <asp:TextBox ID="TextBoxCountryCode" runat="server" Width="350px"></asp:TextBox>
                <br />
                <asp:Label ID="LabelDetails" runat="server" AssociatedControlID="TextBoxCountryName" Text="Country name:"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxCountryName" runat="server" Width="350px"></asp:TextBox>
                <br />
                <asp:Label ID="LabelDetails0" runat="server" AssociatedControlID="TextBoxCityName" Text="City name:"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxCityName" runat="server" Width="350px"></asp:TextBox>
                <br />
                <asp:Label ID="LabelDetails1" runat="server" AssociatedControlID="TextBoxPopulation" Text="Population:"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxPopulation" runat="server" Width="350px"></asp:TextBox>
                <br />
                <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="Add" />
                <br />
                <asp:Label ID="LabelAddMessage" runat="server" Visible="False"></asp:Label>
                <br />
                <br />
            </asp:Panel>
    <asp:Panel ID="PanelNotification" runat="server" Width="100%">
                <asp:Label ID="LabelDetails3" runat="server" AssociatedControlID="TextBoxUserId" Text="User Id:"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxUserId" runat="server" Width="350px"></asp:TextBox>
                <br />
                <asp:Label ID="LabelDetails4" runat="server" AssociatedControlID="TextBoxLocationId" Text="Location Id:"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxLocationId" runat="server" Width="350px"></asp:TextBox>
                <br />
                <asp:Button ID="ButtonNotification" runat="server" Text="Send Notification" OnClick="ButtonNotification_Click"/>
                <br />
                <asp:Label ID="LabelNotificationMessage" runat="server" Visible="False"></asp:Label>
                <br />
                <br />
            </asp:Panel>
<asp:GridView ID="GridViewRequests" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="RequestId" DataSourceID="SqlDataSourceRequests">
    <Columns>
        <asp:BoundField DataField="RequestId" HeaderText="RequestId" SortExpression="RequestId"></asp:BoundField>
        <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True"></asp:CommandField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceRequests" runat="server" ConnectionString="Data Source=xdbs2.dailyrazor.com;Initial Catalog=iagrrsfr_appdb;User ID=iagrrsfr_appdbuser;Password=5%fxZk43#$n794!f;Connect Timeout=30" ProviderName="System.Data.SqlClient" SelectCommand="sp_locationRequests" SelectCommandType="StoredProcedure" DeleteCommand="sp_locationRequestsDelete" DeleteCommandType="StoredProcedure"></asp:SqlDataSource>
