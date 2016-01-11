<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Users.ascx.cs" Inherits="WebApplication1.AdminPages.Users" %>
<p>
    <asp:Label ID="Label16" runat="server" AssociatedControlID="TextBoxUser" Text="User Id / Username:"></asp:Label>
    <asp:TextBox ID="TextBoxUser" runat="server"></asp:TextBox>
    <asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Submit" />
</p>
<asp:Panel ID="PanelUserInfo" runat="server" Visible="False">
    <asp:Image ID="ImagePhoto" runat="server" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="User ID:"></asp:Label>
    <asp:Label ID="LabelUserId" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Email:"></asp:Label>
    <asp:Label ID="LabelEmail" runat="server"></asp:Label>
    <asp:Image ID="ImageEmailStatus" runat="server" />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Mobile:"></asp:Label>
    <asp:Label ID="LabelMobile" runat="server"></asp:Label>
    <asp:Image ID="ImageMobileStatus" runat="server" />
    <br />
    <asp:Label ID="Label4" runat="server" Text="Username:"></asp:Label>
    <asp:Label ID="LabelUsername" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label5" runat="server" Text="First Name:"></asp:Label>
    <asp:Label ID="LabelFirstName" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label6" runat="server" Text="Last Name:"></asp:Label>
    <asp:Label ID="LabelLastName" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label7" runat="server" Text="Birth Date:"></asp:Label>
    <asp:Label ID="LabelDOB" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label8" runat="server" Text="Member Since:"></asp:Label>
    <asp:Label ID="LabelMemberSince" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label9" runat="server" Text="Last Login:"></asp:Label>
    <asp:Label ID="LabelLastLogin" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label10" runat="server" Text="Gender:"></asp:Label>
    <asp:Label ID="LabelGender" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label11" runat="server" Text="Language:"></asp:Label>
    <asp:Label ID="LabelLanguage" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label12" runat="server" Text="Location:"></asp:Label>
    <asp:Label ID="LabelLocation" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label13" runat="server" Text="Last Location:"></asp:Label>
    <asp:Label ID="LabelLastLocation" runat="server"></asp:Label>
    <br />
    <asp:Label ID="LabelFollowersCount1" runat="server" Text="Location Detect:"></asp:Label>
    <asp:Label ID="LabelLocationDetect" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label14" runat="server" Text="Rate:"></asp:Label>
    <asp:Label ID="LabelRate" runat="server"></asp:Label>
    <asp:Label ID="LabelRateCount" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label15" runat="server" Text="Following:"></asp:Label>
    <asp:Label ID="LabelFollowingCount" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label18" runat="server" Text="Followers:"></asp:Label>
    <asp:Label ID="LabelFollowersCount" runat="server"></asp:Label>
    <br />
    <asp:Label ID="LabelFollowersCount0" runat="server" Text="About:"></asp:Label>
    <asp:Label ID="LabelAbout" runat="server"></asp:Label>
    <br />
    <asp:Label ID="LabelFollowersCount2" runat="server" Text="Events Created:"></asp:Label>
    <asp:Label ID="LabelEventsCreated" runat="server"></asp:Label>
    <br />
    <asp:Label ID="LabelFollowersCount3" runat="server" Text="Events Requested:"></asp:Label>
    <asp:Label ID="LabelEventsRequested" runat="server"></asp:Label>
    <br />
    <asp:Label ID="LabelFollowersCount4" runat="server" Text="Events Accepted:"></asp:Label>
    <asp:Label ID="LabelEventsAccepted" runat="server"></asp:Label>
    <br />
    <asp:Label ID="LabelFollowersCount5" runat="server" Text="Status:"></asp:Label>
    <asp:DropDownList ID="DropDownListStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListStatus_SelectedIndexChanged">
        <asp:ListItem Value="1">Active</asp:ListItem>
        <asp:ListItem Value="2">Disabled</asp:ListItem>
        <asp:ListItem Value="3">Deactivated</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:LinkButton ID="LinkButtonLog" runat="server" OnClick="LinkButtonLog_Click">Show Full Log</asp:LinkButton>
</asp:Panel>
<asp:Label ID="LabelMessage" runat="server"></asp:Label>
<p>
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Show Reports</asp:LinkButton>
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
    <asp:SqlDataSource ID="SqlDataSourceReports" runat="server" ConnectionString="Data Source=xdbs2.dailyrazor.com;Initial Catalog=iagrrsfr_appdb;User ID=iagrrsfr_appdbuser;Password=5%fxZk43#$n794!f;Connect Timeout=30" ProviderName="System.Data.SqlClient" SelectCommand="sp_reportsUsers" SelectCommandType="StoredProcedure" DeleteCommand="sp_reportsDelete" DeleteCommandType="StoredProcedure"></asp:SqlDataSource>
    <br />
    Fake account - 10<br /> Inappropriate - 11<br /> Hateful or racism - 12
    <br />
    Abusive - 13<br /> Other - 19</asp:Panel>
