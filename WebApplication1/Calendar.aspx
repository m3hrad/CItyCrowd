<%@ Page Title="Calendar" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="WebApplication1.Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="RepeaterCalendar" runat="server">
        <ItemTemplate>
            
        </ItemTemplate>
    </asp:Repeater>

    <asp:Label ID="LabelNoRecord" runat="server" Text="The calendar is empty!" Visible="False"></asp:Label>

</asp:Content>
