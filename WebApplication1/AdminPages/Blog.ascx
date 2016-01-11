<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Blog.ascx.cs" Inherits="WebApplication1.AdminPages.Blog" %>
            <asp:LinkButton ID="LinkButtonAdd" runat="server" onclick="LinkButtonAdd_Click"
                 >Add Entry</asp:LinkButton>
            <asp:Panel ID="PanelAdd" runat="server" Visible="False" Width="100%">
                <asp:Label ID="LabelLastName" runat="server" AssociatedControlID="TextBoxTitle" Text="Subject:"></asp:Label>
            <br />
                <asp:TextBox ID="TextBoxTitle" runat="server" Width="350px"></asp:TextBox>
                <br />
                <asp:Label ID="LabelDetails" runat="server" AssociatedControlID="TextBoxBody" Text="Body:"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxBody" runat="server" Height="100px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                <br />
                <asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/Images/Admin/Buttons/add-off.png" onclick="ImageButtonAdd_Click" ValidationGroup="Add" />
                <br />
                <asp:Label ID="LabelAddMessage" runat="server" Visible="False"></asp:Label>
                <br />
                <br />
            </asp:Panel>
            <asp:LinkButton ID="LinkButtonManage" runat="server" 
                onclick="LinkButtonManage_Click"
                 >Manage Entries</asp:LinkButton>
            <br />
            <asp:Panel ID="PanelManage" runat="server" Visible="False" Width="100%">
                <br />
                <table class="style1">
                    <tr>
                        <td>
                            <asp:GridView ID="GridViewBlog" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                AllowSorting="True" DataSourceID="SqlDataSourceBlog" 
                                DataKeyNames="EntryId" 
                                onselectedindexchanged="GridViewBlog_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="EntryId" 
                                        HeaderText="Entry Id" SortExpression="EntryId" >
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                    </asp:BoundField>
                                    <asp:HyperLinkField DataNavigateUrlFields="EntryId" 
                                        DataNavigateUrlFormatString="~/Blog/{0}" 
                                        DataTextField="Title" HeaderText="Subject" SortExpression="Title" 
                                        Target="_blank">
                                    <ItemStyle Width="300px" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField HeaderText="View" DataField="ViewCount" 
                                        SortExpression="ViewCount" >
                                    <ItemStyle Width="75px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date" />
                                    <asp:CommandField HeaderText="Edit" SelectText="Edit" ShowSelectButton="True">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSourceBlog" runat="server" 
                                ConnectionString="Data Source=xdbs2.dailyrazor.com;Initial Catalog=iagrrsfr_appdb;User ID=iagrrsfr_appdbuser;Password=5%fxZk43#$n794!f;Connect Timeout=30" 
                                
                                SelectCommand="sp_blog" SelectCommandType="StoredProcedure" 
                                ProviderName="System.Data.SqlClient">
                            </asp:SqlDataSource>
                            <br />
                            <asp:Panel ID="PanelEdit" runat="server" Visible="False">
                                            <asp:Label ID="LabelLastName1" runat="server" 
                                                AssociatedControlID="TextBoxEditTitle" Text="Entry Id:"></asp:Label>
                                            <br />
                                            <asp:Label ID="LabelEditEntryId" runat="server" ></asp:Label>
                                            <br />
                                            <asp:Label ID="LabelLastName0" runat="server" 
                                                AssociatedControlID="TextBoxEditTitle" Text="Subject:"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="TextBoxEditTitle" runat="server" Width="350px"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="LabelDetails0" runat="server" 
                                                AssociatedControlID="TextBoxEditBody" 
                                                Text="Body:"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="TextBoxEditBody" runat="server" Height="100px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                            <br />
                                            <asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/Images/Admin/Buttons/save-off.png" onclick="ImageButtonEdit_Click" ValidationGroup="Edit" />
                                            <br />
                                            <br />
                                            <asp:ImageButton ID="ImageButtonDelete" runat="server" ImageUrl="~/Images/Admin/Buttons/delete-off.png" OnClick="ImageButtonDelete_Click" />
                                            <br />
                                            <asp:Label ID="LabelEditMessage" runat="server" Visible="False"></asp:Label>
                            </asp:Panel>
            </asp:Panel>