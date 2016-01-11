<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Statistics.ascx.cs" Inherits="WebApplication1.AdminPages.Statistics" %>
<p>
        <asp:DropDownList ID="DropDownListStats" runat="server"
            DataSourceID="SqlDataSourceStats" DataTextField="SubmitDate" 
            DataValueField="StatisticId" AutoPostBack="True" 
            onselectedindexchanged="DropDownListStats_SelectedIndexChanged">
        </asp:DropDownList>
                                        <asp:ImageButton ID="ImageButtonDelete" runat="server" 
                                            ImageUrl="~/Images/Admin/Buttons/delete-off.png" 
                                            onclick="ImageButtonDelete_Click" />
        <asp:SqlDataSource ID="SqlDataSourceStats" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AppConnectionString %>" 
            SelectCommand="sp_adminStats" 
            SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
            </p>
<p>
                                        <asp:ImageButton ID="ImageButtonAdd" runat="server" 
                                            ImageUrl="~/Images/Admin/Buttons/add-off.png" 
                                            onclick="ImageButtonAdd_Click" />
                                    </p>
<asp:Panel ID="PanelStats" runat="server" Visible="False">
                <asp:Label ID="Label14" runat="server" Text="Admins:"></asp:Label>
                <br />
                <asp:Label ID="LabelAdmins" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label26" runat="server" Text="Bookmarks:"></asp:Label>
                <br />
                <asp:Label ID="LabelBookmarks" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label7" runat="server" Text="Bookmark Distinct Users"></asp:Label>
                <br />
                <asp:Label ID="LabelBookmarkDistinctUsers" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label10" runat="server" Text="Events"></asp:Label>
                <br />
                <asp:Label ID="LabelUEvents" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label15" runat="server" Text="Admins number:"></asp:Label>
               <br />
                <asp:Label ID="Label17" runat="server" Text="Events Available:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsAvailable" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label1" runat="server" Text="Events Distinct Owners:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsDistinctOwners" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label21" runat="server" Text="Events Participants Average:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsParticipantsAverage" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label22" runat="server" Text="Events Participants Accepted Average:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsParticipantsAcceptedAverage" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label23" runat="server" Text="Events Distinct Locations:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsDistinctLocations" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label56" runat="server" Text="Events Type 1:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsType1" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label57" runat="server" Text="Events Type 2:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsType2" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label58" runat="server" Text="Events Type 3:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsType3" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label59" runat="server" Text="Events Type 4:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsType4" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label60" runat="server" Text="Events Type 5:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsType5" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label61" runat="server" Text="Events Type 6:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsType6" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label62" runat="server" Text="Events Type 7:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsType7" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label63" runat="server" Text="Events Type 8:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsType8" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label19" runat="server" Text="Events View"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsView" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label20" runat="server" Text="Events Requests:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsRequests" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label64" runat="server" Text="Events Boards Messages:"></asp:Label>
                <br />
                <asp:Label ID="LabelEventsBoardsMessages" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label65" runat="server" Text="Followers:"></asp:Label>
                <br />
                <asp:Label ID="LabelFollowers" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label66" runat="server" Text="Reviews:"></asp:Label>
                <br />
                <asp:Label ID="LabelReviews" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label67" runat="server" Text="Reviews Rate Average:"></asp:Label>
                <br />
                <asp:Label ID="LabelReviewsRateAverage" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label68" runat="server" Text="Users:"></asp:Label>
                <br />
                <asp:Label ID="LabelUsers" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label69" runat="server" Text="Users Gender Male:"></asp:Label>
                <br />
                <asp:Label ID="LabelUsersGenderMale" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label70" runat="server" Text="Users Gender Female:"></asp:Label>
                <br />
                <asp:Label ID="LabelUsersGenderFemale" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label71" runat="server" Text="Users Gender Unknown:"></asp:Label>
                <br />
                <asp:Label ID="LabelUsersGenderUnknown" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label72" runat="server" Text="Users With Photo:"></asp:Label>
                <br />
                <asp:Label ID="LabelUsersHasPhoto" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label73" runat="server" Text="Users Messages:"></asp:Label>
                <br />
                <asp:Label ID="LabelUsersMessages" runat="server"></asp:Label>
    <br />
</asp:Panel>