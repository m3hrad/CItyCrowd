<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta property="og:type" content="article" />
    <meta property="og:title" content="CityCrowd" />
    <meta property="og:description" content="The main idea of the CityCrowd is providing a platform to users to let them create and share events/gathering/activities/hanging outs with the other users with the same passion.
To do so properly, Citycrowd is a social network with features such as following, exploring, searching, messaging, notification and so on.
" />
    <meta property="og:image" content="http://citycrowd.azurewebsites.net/Start/wp-content/uploads/2014/02/slider-11.jpg" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).bind("mobileinit", function () {
            $.mobile.ajaxEnabled = false;
        });
    </script>
    <script src="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.js"></script>

    <script src="../JS/script.js"></script>
    <script>
        $(document).ready(function () {
            registerPage();
        });
    </script>
    <link rel="stylesheet" href="Styles/style.css" />
</head>
<body>

    <div class="loginMainWindow">
        <div class="nLoginTransparent">
            <div class="nMiddleWindow">
                <form id="form1" runat="server">
                    <div class="nLoginLogo"></div>
                    <div class="nRegisterInviteContainer invisible">
                        <div class="nRegisterInviteLogo nPanelProfilePicture">
                        </div>
                        <div class="nRegisterInviteMessage">
                            <span class="nRegisterInviteSpan"></span>&nbsp;invited you to become a CityCrowder!
                        </div>
                    </div>
                    <div class="nLoginBox">
                        <asp:TextBox ID="TextBoxEmail" runat="server" placeholder="Email" data-mini="true"></asp:TextBox>
                        <div class="nLoginValidationMessage invisible nRegisterEmailError">Please enter valid email address</div>
                        <asp:TextBox ID="TextBoxPassword1" runat="server" TextMode="Password" placeholder="Password" data-mini="true"></asp:TextBox>
                        <div class="nLoginValidationMessage invisible nRegisterPasswordError">Please enter the password</div>
                        <asp:TextBox ID="TextBoxPassword2" runat="server" TextMode="Password" placeholder="Re-type password" data-mini="true"></asp:TextBox>
                        <div class="nLoginValidationMessage invisible nRegisterRepeatPassError">Please retype the password</div>
                        <div class="nRegisterButton"><b>Register</b></div>
                        <br />
                        <div class="nLoginFacebookButton">
                            <div class="nLoginFacebookButtonLogo"></div>
                            <div class="nLoginFacebookButtonText">Login with Facebook</div>
                        </div>
                        <div class="nWrongLogin hidden">
                            <div class="nWrongLoginIcon"></div>
                            <b>
                                <asp:Label ID="LabelError" runat="server"></asp:Label></b>
                        </div>
                    </div>
                    <div class="hidden">
                        <asp:Button ID="ButtonRegister" runat="server" Text="Register" OnClick="ButtonRegister_Click" />
                    </div>
                    <div class="nExploreFooter nRegisterFooter">
                        <div class="nLoginFooterSecondary">
                            <div class="nRegisterLogin nFooterButton2small nFooterButton2SmallRight">Login</div>
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenFieldInvite" runat="server" Value="0" />
                    <asp:HiddenField ID="HiddenFieldInviteStatus" runat="server" />
                    <asp:HiddenField ID="HiddenFieldInviteName" runat="server" />
                    <asp:HiddenField ID="HiddenFieldInvitePhotoUrl" runat="server" />
                </form>
            </div>
        </div>
    </div>
</body>
</html>
