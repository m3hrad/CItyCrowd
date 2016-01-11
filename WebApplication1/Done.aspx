﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="Done.aspx.cs" Inherits="WebApplication1.Done" %>

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
            donePage();
            generalLook();
            menu();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nHeader">
        <div class="nMenuButton">
            <div class="nMenuButtonLogo">
            </div>
            <div class="nMenuNotification nMenuButtonNotification">
                <figure class="front">0</figure>
                <figure class="back">0</figure>
            </div>
        </div>
        <div class="nMenuAdd"></div>
        <div class="nPageTitle">Done!</div>
    </div>
    <div class="pageErrorContent invisible">
        <div class="nDoneSmiley">:)</div>
        <div class="nDoneMessage">Message</div>
        <div class="nDoneAllButtons">
            <div class="nDoneButton nDoneButton4 invisible">
                <div class="nDoneImage nDoneImage4"></div>
                <div class="nDoneInsideImage nDoneInsideImage4"></div>
                <div class="nDoneText nDoneText4">4</div>
            </div>
            <div class="nDoneButton nDoneButton3 invisible">
                <div class="nDoneImage nDoneImage3"></div>
                <div class="nDoneInsideImage nDoneInsideImage3"></div>
                <div class="nDoneText nDoneText3">3</div>
            </div>
            <div class="nDoneButton nDoneButton2 invisible">
                <div class="nDoneImage nDoneImage2"></div>
                <div class="nDoneInsideImage nDoneInsideImage2"></div>
                <div class="nDoneText nDoneText2">2</div>
            </div>
            <div class="nDoneButton nDoneButton1 invisible">
                <div class="nDoneImage nDoneImage1"></div>
                <div class="nDoneInsideImage nDoneInsideImage1"></div>
                <div class="nDoneText nDoneText1">1</div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HiddenFieldTitle" runat="server" />
    <asp:HiddenField ID="HiddenFieldSmiley" runat="server" />
    <asp:HiddenField ID="HiddenFieldMessage" runat="server" />
    <asp:HiddenField ID="HiddenFieldLinksNumber" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink1Image" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink1Text" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink1Url" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink1Color" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink2Image" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink2Text" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink2Url" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink2Color" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink3Image" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink3Text" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink3Url" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink3Color" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink4Image" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink4Text" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink4Url" runat="server" />
    <asp:HiddenField ID="HiddenFieldLink4Color" runat="server" />
</asp:Content>
