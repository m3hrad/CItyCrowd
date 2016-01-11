﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageUser.Master" AutoEventWireup="true" CodeBehind="EventsView.aspx.cs" Inherits="WebApplication1.EventsView" %>

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
            eventsView();
            generalLook();
            menu();
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
        <div class="nPageTitle">View Event</div>
    </div>
    <div class="pageNormalContent ">

        <div class="nContentCover">
            <div class="nContentCoverTransparent">
                <div class="nContentTitle">
                    <b>
                        <asp:Label ID="LabelName" runat="server"></asp:Label>
                    </b>
                </div>

                <!-- <div class="nDots"><i class="fa fa-ellipsis-h"></i>&nbsp</div> -->
                <div class="nDots nDotsEvent">
                    <i class="fa fa-ellipsis-h nDotsLogo"></i>
                    <div class="clear"></div>
                    <div class="nDotsBox nDotBoxLinks">
                        <div class="nDotsBoxEachLogo nDotsBoxEachLogoShare"></div>
                        <div class="nDotsBoxEach">Share</div>
                        <div class="nDotsBoxEachLogo nDotsBoxEachLogoBookmark"></div>
                        <div class="nDotsBoxEach nEVbookmarkButton">Bookmark</div>
                        <div class="nDotsBoxEachLogo nDotsBoxEachLogoReport"></div>
                        <div class="nDotsBoxEach nEVReportButton">Report Event</div>
                    </div>
                    <div class="nDotsBox nDotsBoxShare hide">
                        <a class="nEVShareButtonFB" target="_blank" href="#">
                            <img class="nEVshareImg" src="../Images/Icons/fb.png" alt="facebook" width="32" height="32" /></a>
                        <a class="nEVShareButtonTwitter" target="_blank" href="#">
                            <img class="nEVshareImg" src="../Images/Icons/twitter.png" alt="Twitter" width="32" height="32" /></a>
                    </div>
                </div>
                <div class="clear"></div>
                <span class="nEVBy">by</span>
                <span class="nEVEventName"></span>

                <div class="clear"></div>
                <div class="nEventCoverBox">
                    <div class="nRoundPicture nActivityPicture">
                    </div>
                </div>
                <div class="nEventCoverBox">
                    <div class="nRoundPicture nEVOwnerPicture">
                        <canvas id="canvas2" width="115" height="115"></canvas>
                    </div>
                </div>
                <div class="nEventCoverBox">
                    <div class="nRoundPicture nParticipantNumberPicture">
                        <div class="nParticipantNumberPictureTop">
                            <asp:Label ID="LabelParticipantsAvailable" runat="server"></asp:Label>/<asp:Label ID="LabelParticipants" runat="server"></asp:Label>
                            <canvas id="EVcanvas" width="115" height="115"></canvas>
                        </div>
                        <div class="nParticipantNumberPictureBot">
                        </div>
                    </div>
                    <!-- old graphs
                <div class="nRoundPicture nNewGraph">
                    <div class="nNewGraphTop"></div>
                    <div class="nNewGraphBottom"></div>
                    <div class="nNewGraphText">
                        <b>
                            1</b> participant needed (<span class="nParticipantAvailable"></span>/3)
                    </div>
                </div>
                    -->

                </div>
            </div>
        </div>
        <div class="nTimeAndLocation nEVTimeAndLocation">
            <div class="nExploreLocation nEVLocation">
                <i class="fa fa-map-marker"></i>&nbsp
            <asp:Label ID="LabelAddress" runat="server"></asp:Label>
                <span class="nEVAddressFiller"></span>
                <asp:Label ID="LabelLocation" runat="server"></asp:Label>
            </div>
            <div class="nExploreTime nEVTime"><i class="fa fa-clock-o"></i>&nbsp <span class="nEVdate">Time unknown</span> </div>
        </div>

        <div class=" nEVTabbedContent">
            <div class="nTabsHolder">
                <div class="nTabs nTabs4 nExploreSelected" ntabnumber="1">
                    <b>DESCRIPTION</b>
                </div>
                <div class="nTabs nTabs4" ntabnumber="2">
                    <b>BOARD</b>
                </div>
                <div class="nTabs nTabs4" ntabnumber="3">
                    <b>PARTICIPANTS</b>
                </div>
                <div class="nTabs nTabs4" ntabnumber="4">
                    <b>MAP</b>
                </div>
            </div>
            <div class="nExploreContentHolder">
                <!-- description -->
                <div class="nDescriptionContent nTabContents nEVTabContents">
                    <asp:Label ID="LabelDescriptions" runat="server"></asp:Label>
                </div>
                <!-- board -->
                <div class="nBoardContent nTabContents nEVTabContents hidden">
                    <div class="nEVMessagesContainer">
                        <div class="nDoneMessage nDoneMessageShortSc nDoneMessageNoMessage invisible">Looks empty? You can post the first message right now!</div>
                        <div class="nDoneSmiley nDoneSmileyNotAllowed invisible">:)</div>
                        <div class="nDoneMessage nDoneMessageNotAllowed invisible">Only participants are allowed to view the board. Send the request to be able to view the discussions.</div>
                        <div class="nEVMessagesContainerInside">
                            <asp:Repeater ID="RepeaterBoardMessages" runat="server">
                                <ItemTemplate>
                                    <div class="MessageSender">
                                        <asp:HiddenField ID="HiddenFieldSenderName" runat="server" Value='<%# Eval("SenderName") %>' />
                                    </div>
                                    <div class="eachMessage">
                                        <div class="nMessagesPicture nEVMessagesPicture nMessagesShowPicture">
                                            <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                                        </div>
                                        <asp:HiddenField ID="HiddenFieldMessageId" runat="server" Value='<%# Eval("MessageId") %>' />
                                        <div class="nMessageBody">
                                            <span><%# Eval("Message") %></span>
                                            <div class="nMessageDecoration"></div>
                                        </div>
                                        <div class="nEVIsOwner">
                                            <asp:HiddenField ID="HiddenFieldIsOwner" runat="server" Value='<%# Eval("IsOwner") %>' />
                                        </div>

                                        <div class="clear"></div>
                                        <div class="MessageListDate nMSDate">
                                            <div class="nSmallIcon nBlogClock nMessagesClock"></div>
                                            <asp:Label ID="LabelDate" runat="server" Text='<%# Eval("PassedDate") %>' CssClass="EachMessageDate ViewClass"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clear"></div>
                                    <asp:HiddenField ID="HiddenFieldSenderId" runat="server" Value='<%# Eval("SenderId") %>' />
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- <div class="nDoneSmiley nDoneSmileyNoMessage invisible"></div> -->

                    </div>
                    <div class="nEVPanel invisible">
                        <asp:Panel ID="PanelBoardMessageAdd" CssClass="nMSPanel " runat="server">
                            <div class="nMSSendButton">
                                <b>SEND</b>
                            </div>
                            <div class="nMSTextArea">
                                <!-- textbox -->
                                <asp:TextBox ID="TextBoxBoardMessageAdd" CssClass="TextBoxMultiR2L" TextMode="MultiLine" data-autogrow="false" runat="server"></asp:TextBox>
                                <!-- <textarea class="TextBoxMultiR2L"></textarea> -->
                            </div>
                            <div class="invisible">
                                <!-- button -->
                                <asp:Button ID="ButtonBoardMessageAdd" runat="server" Text="Button" OnClick="ButtonBoardMessageAdd_Click" />
                                <asp:Button ID="ButtonRemoveParticipant" runat="server" OnClick="ButtonRemoveParticipant_Click" />
                            </div>
                        </asp:Panel>
                    </div>

                    <br />
                </div>
                <!-- participants -->
                <div class="nParticipantsContent nTabContents nEVTabContents hidden">
                    <div class="invisible">
                        <asp:Label ID="LabelNoRecord" runat="server" Text="No participants yet!" Visible="False"></asp:Label>
                    </div>
                    <div class="nParticipantsListContainer">
                        <asp:Repeater ID="RepeaterParticipants" runat="server">
                            <ItemTemplate>
                                <div class="nMessagesPicture nEVMessagesPicture">
                                    <asp:HiddenField ID="HiddenFieldProfilePicUrl" runat="server" Value='<%# Eval("ProfilePicUrl") %>' />
                                </div>
                                <asp:HyperLink ID="HyperLinkParticipant" runat="server" NavigateUrl='<%#"~/Profile/"+ Eval("UserId") %>' CssClass="nEVparticipantName nNoDecoration"><%#Eval("FirstName") + " " + Eval("LastName") %></asp:HyperLink>
                                <div class='nEVX' data-id='<%# Eval("UserId") %>'>X</div>
                                <br />
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="nDoneSmiley nDoneSmileyParticipants invisible">:)</div>
                    <div class="nDoneMessage nDoneMessageParticipants invisible">You are honored to be the first person to join this event in the history.</div>
                    <div class="nDoneSmiley nDoneSmileyNotAllowed invisible">:)</div>
                    <div class="nDoneMessage nDoneMessageNotAllowed invisible">Only participants are allowed to view the participants list. Send the request to be able to view the participants list.</div>
                </div>
                <div class="nParticipantMap nTabContents nEVTabContents hidden">
                    <div id="map_canvas"></div>
                </div>
            </div>
        </div>
        <div class="hiddenButtons">
            <asp:Button ID="ButtonRequest" runat="server" Text="Join" OnClick="ButtonRequest_Click" />
        </div>
        <div class="invisible">

            <asp:HyperLink ID="HyperLinkModify" runat="server" Visible="False">Modify event</asp:HyperLink>
            <asp:Label ID="LabelLanguages" runat="server"></asp:Label>
            <asp:Label ID="LabelStatus" runat="server"></asp:Label>
            <asp:Button ID="ButtonBookmark" runat="server" OnClick="ButtonBookmark_Click" Text="Bookmark" />
            <asp:Label ID="LabelMessage" runat="server"></asp:Label>
        </div>


        <div class="nExploreFooter nThickFooter">
            <div class="nEVRequestMessageError nGeneralErrorMessage invisible">Please enter the request message!</div>
            <div class="nEVRequestMessageContainer invisible">
                <asp:TextBox ID="TextBoxMessage" runat="server" CssClass="nEVRequestMessage" data-role="none" placeholder="type your message here" MaxLength="85"></asp:TextBox>
            </div>
            <div class="nFooterButton nNotButton invisible">
                <div class="nFooter1Image nProfileImageOrange"></div>
                <span>NOT INTERESTED</span>
            </div>
            <div class="nFooterButton nRequestButton invisible">
                <div class="nFooter1Image nProfileImageGreen"></div>
                <span>SEND REQUEST</span>
            </div>
            <div class="nFooterButton nProfileMessageButton invisible ">
                <div class="nFooter1Image nProfileImageGrey"></div>
                <span>EDIT EVENT</span>
            </div>
        </div>

        <asp:HiddenField ID="HiddenFieldOwnerId" runat="server" />
        <asp:HiddenField ID="HiddenFieldEventId" runat="server" />
        <asp:HiddenField ID="HiddenFieldDate" runat="server" />
        <asp:HiddenField ID="HiddenFieldOwnerRateScore" runat="server" />
        <asp:HiddenField ID="HiddenFieldOwnerPhotoUrl" runat="server" />
        <asp:HiddenField ID="HiddenFieldUsername" runat="server" />
        <asp:HiddenField ID="HiddenFieldOwnerFullname" runat="server" />
        <asp:HiddenField ID="HiddenFieldOwnerRateCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldEventPhotoUrl" runat="server" />
        <asp:HiddenField ID="HiddenFieldTypeId" runat="server" />
        <asp:HiddenField ID="HiddenFieldCoverId" runat="server" />
        <asp:HiddenField ID="HiddenFieldButtonStatus" runat="server" />
        <asp:HiddenField ID="HiddenFieldStep" runat="server" />
        <asp:HiddenField ID="HiddenFieldBoardStatus" runat="server" />
        <asp:HiddenField ID="HiddenFieldRequestStatus" runat="server" />
        <asp:HiddenField ID="HiddenFieldParticipantValue" runat="server" />

        <asp:HiddenField ID="HiddenFieldToastStatus" runat="server" Value="0" />
        <asp:HiddenField ID="HiddenFieldToastMode" runat="server" />
        <asp:HiddenField ID="HiddenFieldToastSmiley" runat="server" />
        <asp:HiddenField ID="HiddenFieldToastMessage" runat="server" />
        <asp:HiddenField ID="HiddenFieldButton1Text" runat="server" />
        <asp:HiddenField ID="HiddenFieldButton2Text" runat="server" />
        <asp:HiddenField ID="HiddenFieldButton1Color" runat="server" />
        <asp:HiddenField ID="HiddenFieldButton2Color" runat="server" />
        <asp:HiddenField ID="HiddenFieldButton1Url" runat="server" />
        <asp:HiddenField ID="HiddenFieldButton2Url" runat="server" />
    </div>
</asp:Content>
