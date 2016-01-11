<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Introduction.aspx.cs" Inherits="WebApplication1.Introduction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Introduction</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.css" />-->
    <link href="Images/favicon.png" rel="icon" type="image/png" />
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).bind("mobileinit", function () {
            $.mobile.ajaxEnabled = false;
        });
    </script>
    <!-- <script src="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.js"></script> -->


    <script src="/js/script.js"></script>

    <script>
        $(document).ready(function () {
            introductionPage();
        });
    </script>
    <link rel="stylesheet" href="Styles/style.css" />
</head>
<body>
    <div class="loginMainWindow">
        <div class="nIntroductionBG">
            <form id="form1" runat="server">
                <div class="nIntroductionPage nIntroductionFirstPage" data-page="1">
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="nIntroductionPagePhoto"></div>
                    <br />
                    <div class="nIntroductionPageTitle">Welcome to CityCrowd!</div>
                    <br />
                    <div class="nIntroductionPageText">Your buddy to match you up with people in your city with same interests to do awesome activities!</div>
                    <div class="clear"></div>
                    <br />
                    <div class="nExploreFooter">
                        <div class="nIntroductionPagePogress">
                            <div class="nOrangeEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                        </div>
                        <br />
                        <div class="nFooterButton nNotButton">
                            <div class="nFooter1Image nProfileImageOrange"></div>
                            <b>&nbsp Skip</b>
                        </div>
                        <div class="nFooterButton nRequestButton nProfileNextButton">
                            <div class="nFooter1Image nProfileImageGreen"></div>
                            <b>&nbsp Next</b>
                        </div>
                    </div>
                </div>
                <div class="nIntroductionPage nIntroductionSecondPage invisible" data-page="2">
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="nIntroductionPagePhoto"></div>
                    <br />
                    <div class="nIntroductionPageTitle">Find</div>
                    <br />
                    <div class="nIntroductionPageText">Explore, Nearby, Following, and Search sections are the ones who help you to find activities which suits you!</div>
                    <div class="clear"></div>
                    <br />
                    <div class="nExploreFooter">
                        <div class="nIntroductionPagePogress">
                            <div class="nGreyEllipse"></div>
                            <div class="nOrangeEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                        </div>
                        <br />
                        <div class="nFooterButton nNotButton">
                            <div class="nFooter1Image nProfileImageOrange"></div>
                            <b>&nbsp Skip</b>
                        </div>
                        <div class="nFooterButton nRequestButton nProfileNextButton">
                            <div class="nFooter1Image nProfileImageGreen"></div>
                            <b>&nbsp Next</b>
                        </div>
                    </div>
                </div>
                <div class="nIntroductionPage nIntroductionThirdPage invisible" data-page="3">
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="nIntroductionPagePhoto"></div>
                    <br />
                    <div class="nIntroductionPageTitle">Add</div>
                    <br />
                    <div class="nIntroductionPageText">Your buddy to match you up with people in your city with same interests to do awesome activities!</div>
                    <div class="clear"></div>
                    <br />
                    <div class="nExploreFooter">
                        <div class="nIntroductionPagePogress">
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nOrangeEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                        </div>
                        <br />
                        <div class="nFooterButton nNotButton">
                            <div class="nFooter1Image nProfileImageOrange"></div>
                            <b>&nbsp Skip</b></div>
                        <div class="nFooterButton nRequestButton nProfileNextButton">
                            <div class="nFooter1Image nProfileImageGreen"></div>
                            <b>&nbsp Next</b></div>
                    </div>
                </div>
                <div class="nIntroductionPage nIntroductionFourthPage invisible" data-page="4">
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="nIntroductionPagePhoto"></div>
                    <br />
                    <div class="nIntroductionPageTitle">Review</div>
                    <br />
                    <div class="nIntroductionPageText">Write review for people who you've done activities to make CityCrowd a safe and friendly place!</div>
                    <div class="clear"></div>
                    <br />
                    <div class="nExploreFooter">
                        <div class="nIntroductionPagePogress">
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nOrangeEllipse"></div>
                            <div class="nGreyEllipse"></div>
                        </div>
                        <br />
                        <div class="nFooterButton nNotButton">
                            <div class="nFooter1Image nProfileImageOrange"></div>
                            <b>&nbsp Skip</b></div>
                        <div class="nFooterButton nRequestButton nProfileNextButton">
                            <div class="nFooter1Image nProfileImageGreen"></div>
                            <b>&nbsp Next</b></div>
                    </div>
                </div>
                <div class="nIntroductionPage nIntroductionFifthPage invisible" data-page="5">
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="nIntroductionPagePhoto"></div>
                    <br />
                    <div class="nIntroductionPageTitle">Enjoy!</div>
                    <br />
                    <div class="nIntroductionPageText">Enjoy your time here and feel free to send us your feedback!</div>
                    <div class="clear"></div>
                    <br />
                    <div class="nExploreFooter">
                        <div class="nIntroductionPagePogress">
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nGreyEllipse"></div>
                            <div class="nOrangeEllipse"></div>
                        </div>
                        <br />
                        <div class="nFooterButton nNotButton">
                            <div class="nFooter1Image nProfileImageOrange"></div>
                            <b>&nbsp Skip</b></div>
                        <div class="nFooterButton nRequestButton nProfileNextButton">
                            <div class="nFooter1Image nProfileImageGreen"></div>
                            <b>&nbsp Let's Get Started</b></div>
                    </div>
                </div>

            </form>
        </div>
    </div>
</body>
</html>
