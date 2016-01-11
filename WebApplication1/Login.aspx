<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.css" />
    <link href="Images/favicon.png" rel="icon" type="image/png" />
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).bind("mobileinit", function () {
            $.mobile.ajaxEnabled = false;
        });
    </script>
    <script src="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.js"></script>


    <script src="/js/script.js"></script>

    <script>
        $(document).ready(function () {
            loginPage();
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
                    <div class="nLoginBox">
                        <asp:TextBox ID="TextBoxUsername" runat="server" placeholder="Email" data-mini="true"></asp:TextBox>
                        <div class="nLoginValidationMessage invisible nLoginEmailError">Please enter valid email address</div>
                        <asp:TextBox ID="TextBoxPassword" runat="server" placeholder="Password" TextMode="Password" data-mini="true"></asp:TextBox>
                        <div class="nLoginValidationMessage invisible nLoginPasswordError">Please provide the password</div>

                        <div class="nLoginButton"><b>Log In</b></div>
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

                        <asp:Button ID="ButtonLogin" runat="server" Text="Log In" OnClick="ButtonLogin_Click" />
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Register" rel="external">Register</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/ForgotPassword/Request" rel="external">Forgot password?</asp:HyperLink>
                    </div>
                    <div class="nExploreFooter nLoginFooter ">
                        <div class="nLoginFooterSecondary">
                            <div class="nLoginForgot nFooterButton2small nFooterButton2SmallLeft">Forgot</div>
                            <div class="nLoginRegister nFooterButton2small nFooterButton2SmallRight">Register</div>
                        </div>
                    </div>
                    <!-- 
                    <asp:Label ID="LabelUsername" runat="server" Text="Email/Phone:" AssociatedControlID="TextBoxUsername"></asp:Label>
                    <asp:Label ID="LabelPassword" runat="server" Text="Password:" AssociatedControlID="TextBoxPassword"></asp:Label>
                    <asp:CheckBox ID="CheckBoxRemember" runat="server" Text="Remember?" />
        -->
                </form>
            </div>
        </div>
    </div>
</body>
</html>
