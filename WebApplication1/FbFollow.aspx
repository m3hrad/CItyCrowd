﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="FbFollow.aspx.cs" Inherits="WebApplication1.FbFollow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../../Styles/themes/moody.min.css" />
    <link rel="stylesheet" href="../../../Styles/themes/jquery.mobile.icons.min.css" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.3/jquery.mobile.structure-1.4.3.min.css" />
    <link rel="stylesheet" href="../../../Styles/font-awesome-4.2.0/css/font-awesome.min.css" />
    <link href="Images/favicon.png" rel="icon" type="image/png" />

    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).on("mobileinit", function () {
            $.mobile.selectmenu.prototype.options.nativeMenu = false;
        });
        $(document).bind("mobileinit", function () {
            $.mobile.ajaxEnabled = false;
        });
    </script>
    <script src="../../../JS/script.js"></script>
    <script src="../../../JS/jquery-ui.min.js"></script>
    <script src="../../../JS/jquery.cookie.js"></script>

    <script src="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>
    <link rel="stylesheet" href="../../../Styles/style.css" />
    <link rel="stylesheet" href="../../../Styles/jquery-ui.min.css" />

    <!-- signalR -->
    <!--Reference the SignalR library. -->
    <script src="../../../Scripts/jquery.signalR-2.2.0.min.js"></script>
    <!--Reference the autogenerated SignalR hub script. -->
    <script src="../../../signalr/hubs"></script>
    <script src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script>
        $(document).ready(function () {
            generalLook();
            menu();
            fBFollowPage();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nHeader">
        <div class="nMenuButton ">
            <div class="nMenuButtonLogo">
            </div>
            <div class="nMenuNotification nMenuButtonNotification">
                <figure class="front">0</figure>
                <figure class="back">0</figure>
            </div>
        </div>
        <div class="nMenuAdd"></div>
        <div class="nPageTitle">Facebook Following</div>
    </div>
    <br />
    <asp:Repeater ID="RepeaterFriends" runat="server">
        <ItemTemplate>
            <div class="nMessagesPicture nEVMessagesPicture">
                <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                <div class='nEVX invisible' data-id='<%# Eval("UserId") %>'>X</div>
            </div>
            <asp:HyperLink ID="HyperLinkParticipant" runat="server" NavigateUrl='<%#"~/Profile/"+ Eval("UserId") %>' CssClass="nEVparticipantName nNoDecoration"><%#Eval("FirstName") + " " + Eval("LastName") %></asp:HyperLink>
            <div class="nProfileFollowButton nFbFollowProfileFollowButton">FOLLOW
                <div class='nEVX invisible' data-id='<%# Eval("UserId") %>'>X</div>
            </div>
            <br />
            <br />
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
