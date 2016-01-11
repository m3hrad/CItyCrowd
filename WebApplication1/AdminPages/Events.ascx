<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Events.ascx.cs" Inherits="WebApplication1.AdminPages.Events" %>
<p>
    <asp:Label ID="Label16" runat="server" AssociatedControlID="TextBoxEventId" Text="Event Id:"></asp:Label>
    <asp:TextBox ID="TextBoxEventId" runat="server"></asp:TextBox>
    <asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Submit" />
</p>
<asp:Panel ID="PanelInfo" runat="server" Visible="False">
    <asp:Label ID="Label1" runat="server" Text="Event Id:"></asp:Label>
    <asp:Label ID="LabelEventId" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Owner Id:"></asp:Label>
    <asp:Label ID="LabelOwnerId" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Name:"></asp:Label>
    <asp:Label ID="LabelName" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Date:"></asp:Label>
    <asp:Label ID="LabelDate" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label5" runat="server" Text="Participants:"></asp:Label>
    <asp:Label ID="LabelParticipants" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label6" runat="server" Text="Participants Accepted:"></asp:Label>
    <asp:Label ID="LabelParticipantsAccepted" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label7" runat="server" Text="Participants Remained:"></asp:Label>
    <asp:Label ID="LabelParticipantsRemained" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label8" runat="server" Text="LocationId:"></asp:Label>
    <asp:Label ID="LabelLocationId" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label9" runat="server" Text="Type Id:"></asp:Label>
    <asp:Label ID="LabelTypeId" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label10" runat="server" Text="Descriptions:"></asp:Label>
    <asp:Label ID="LabelDescriptions" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label11" runat="server" Text="View Count:"></asp:Label>
    <asp:Label ID="LabelViewCount" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label12" runat="server" Text="Requests Count:"></asp:Label>
    <asp:Label ID="LabelRequestsCount" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label13" runat="server" Text="Report Count:"></asp:Label>
    <asp:Label ID="LabelReportCount" runat="server"></asp:Label>
    <br />
    <asp:Label ID="LabelFollowersCount5" runat="server" Text="Status:"></asp:Label>
    <asp:DropDownList ID="DropDownListStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListStatus_SelectedIndexChanged">
        <asp:ListItem Value="1">Available</asp:ListItem>
        <asp:ListItem Value="2">Full</asp:ListItem>
        <asp:ListItem Value="3">Passed</asp:ListItem>
        <asp:ListItem Value="4">Banned</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" />
    <br />
</asp:Panel>
<p>
<asp:Label ID="LabelMessage" runat="server"></asp:Label>
</p>

<p>
    <asp:LinkButton ID="LinkButtonReports" runat="server" OnClick="LinkButtonReports_Click">Show Reports</asp:LinkButton>
<asp:Panel ID="PanelReports" runat="server" Visible="False">
    <asp:GridView ID="GridViewReports" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ReportId" DataSourceID="SqlDataSourceReports">
        <Columns>
            <asp:BoundField DataField="ReportId" HeaderText="ReportId" SortExpression="ReportId"></asp:BoundField>
            <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" SortExpression="ItemId" />
            <asp:BoundField DataField="ReportType" HeaderText="Type" SortExpression="ReportType" />
            <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
            <asp:BoundField DataField="SubmitDate" HeaderText="SubmitDate" SortExpression="SubmitDate" />
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceReports" runat="server" ConnectionString="Data Source=xdbs2.dailyrazor.com;Initial Catalog=iagrrsfr_appdb;User ID=iagrrsfr_appdbuser;Password=5%fxZk43#$n794!f;Connect Timeout=30" ProviderName="System.Data.SqlClient" SelectCommand="sp_reportsEvents" SelectCommandType="StoredProcedure" DeleteCommand="sp_reportsDelete" DeleteCommandType="StoredProcedure"></asp:SqlDataSource>
    <br />
    Spam - 50<br /> Inappropriate - 51<br /> Hateful or racism - 52<br /> Abusive - 53<br /> Other - 59</asp:Panel>


